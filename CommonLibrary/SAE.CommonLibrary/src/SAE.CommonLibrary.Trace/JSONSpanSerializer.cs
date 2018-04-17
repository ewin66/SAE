﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Utils;

namespace SAE.CommonLibrary.Trace
{
    internal class JSONSpanSerializer : ISpanSerializer
    {
        private const char openingBrace = '{';
        private const char closingBrace = '}';
        internal const char comma = ',';
        private const string annotations = "annotations";
        private const string binaryAnnotations = "binaryAnnotations";
        private const string endpoint = "endpoint";
        private const string timestamp = "timestamp";
        private const string duration = "duration";
        private const string key = "key";
        private const string value = "value";
        private const string id = "id";
        private const string traceId = "traceId";
        private const string parentId = "parentId";
        private const string debug = "debug";
        private const string name = "name";
        private const string ipv4 = "ipv4";
        private const string port = "port";
        private const string serviceName = "serviceName";


        public void SerializeTo(Stream stream, Span span)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(WriterExtensions.openingBracket);
                SerializeSpan(writer, span);
                writer.Write(WriterExtensions.closingBracket);
            }

        }

        private void SerializeSpan(StreamWriter writer, Span span)
        {
            var serviceName = SerializerUtils.GetServiceNameOrDefault(span);
            var endPoint = span.Endpoint ?? SerializerUtils.DefaultEndPoint;
            writer.Write(openingBrace);
            if (span.Annotations.Count != 0)
            {
                writer.WriteList(
                    SerializeAnnotation,
                    annotations,
                    span.Annotations,
                    endPoint,
                    serviceName
                );
            }
            if (span.BinaryAnnotations.Count != 0)
            {
                writer.Write(comma);
                writer.WriteList(
                    SerializeBinaryAnnotation,
                    binaryAnnotations,
                    span.BinaryAnnotations,
                    endPoint,
                    serviceName
                );
            }
            writer.Write(comma);
            writer.WriteField(debug, false);
            writer.Write(comma);
            writer.WriteField(id, NumberUtils.EncodeLongToLowerHexString(span.SpanState.SpanId));
            writer.Write(comma);
            writer.WriteField(name, span.Name ?? SerializerUtils.DefaultRpcMethodName);
            writer.Write(comma);
            writer.WriteField(traceId, NumberUtils.EncodeLongToLowerHexString(span.SpanState.TraceId));

            if (span.SpanStarted.HasValue)
            {
                writer.Write(comma);
                writer.WriteField(timestamp, span.SpanStarted.Value.ToUnixTimestamp());
            }
            if (span.Duration.HasValue && span.Duration.Value.TotalMilliseconds > 0)
            {
                writer.Write(comma);
                writer.WriteField(duration, (long)(span.Duration.Value.TotalMilliseconds * 1000)); // microseconds
            }
            if (!span.IsRoot)
            {
                writer.Write(comma);
                writer.WriteField(parentId, NumberUtils.EncodeLongToLowerHexString(span.SpanState.ParentSpanId.Value));
            }
            writer.Write(closingBrace);
        }

        private static void SerializeBinaryAnnotation(StreamWriter writer, BinaryAnnotation binaryAnnotation, IPEndPoint endPoint, string serviceName)
        {
            writer.Write(openingBrace);
            writer.WriteField(key, binaryAnnotation.Key);
            writer.Write(comma);
            writer.WriteField(value, Encoding.UTF8.GetString(binaryAnnotation.Value));
            writer.Write(comma);
            writer.WriteAnchor(endpoint);
            SerializeEndPoint(writer, endPoint, serviceName);
            writer.Write(closingBrace);
        }

        private static void SerializeAnnotation(StreamWriter writer, ZipkinAnnotation annotation, IPEndPoint endPoint, string serviceName)
        {
            writer.Write(openingBrace);
            writer.WriteField(timestamp, annotation.Timestamp.ToUnixTimestamp());
            writer.Write(comma);
            writer.WriteField(value, annotation.Value);
            writer.Write(comma);
            writer.WriteAnchor(endpoint);
            SerializeEndPoint(writer, endPoint, serviceName);
            writer.Write(closingBrace);
        }

        private static void SerializeEndPoint(StreamWriter writer, IPEndPoint endPoint, string serviceName)
        {
            writer.Write(openingBrace);
            writer.WriteField(ipv4, endPoint.Address.ToString());
            writer.Write(comma);
            writer.WriteField(port, (short)endPoint.Port);
            writer.Write(comma);
            writer.WriteField(JSONSpanSerializer.serviceName, serviceName);
            writer.Write(closingBrace);
        }
    }

    delegate void SerializeMethod<T>(StreamWriter writer, T element, IPEndPoint endPoint, string serviceName);

    internal static class WriterExtensions
    {
        private const char quotes = '"';
        private const char colon = ':';
        internal const char openingBracket = '[';
        internal const char closingBracket = ']';
        internal static void WriteList<U>
        (
            this StreamWriter writer,
            SerializeMethod<U> serializer,
            string fieldName,
            ICollection<U> elements,
            IPEndPoint endPoint,
            string serviceName
        )
        {
            bool firstElement = true;
            WriteAnchor(writer, fieldName);
            writer.Write(openingBracket);
            foreach (var element in elements)
            {
                if (firstElement == true)
                {
                    firstElement = false;
                }
                else
                {
                    writer.Write(JSONSpanSerializer.comma);
                }
                serializer(writer, element, endPoint, serviceName);
            }
            writer.Write(closingBracket);
        }

        internal static void WriteField
        (
            this StreamWriter writer,
            string fieldName,
            bool fieldValue
        )
        {
            WriteAnchor(writer, fieldName);
            writer.Write(fieldValue);
        }

        internal static void WriteField
        (
            this StreamWriter writer,
            string fieldName,
            string fieldValue
        )
        {
            WriteAnchor(writer, fieldName);
            writer.Write(quotes);
            writer.Write(fieldValue);
            writer.Write(quotes);
        }
        internal static void WriteField
        (
            this StreamWriter writer,
            string fieldName,
            long fieldValue
        )
        {
            WriteAnchor(writer, fieldName);
            writer.Write(quotes);
            writer.Write(fieldValue);
            writer.Write(quotes);
        }

        internal static void WriteAnchor(this StreamWriter writer, string anchor)
        {
            writer.Write(quotes);
            writer.Write(anchor);
            writer.Write(quotes);
            writer.Write(colon);
        }
    }
}
