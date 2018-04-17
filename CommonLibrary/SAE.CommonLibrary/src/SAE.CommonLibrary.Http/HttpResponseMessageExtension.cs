using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Http
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> AsModelAsync<T>(this HttpResponseMessage response)where T:class
        {
            return Json.JsonHelper.Deserialize<T>(await response.AsStringAsync());
        }

        public static Task<string> AsStringAsync(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync();
        }

        public static Task<Stream> AsStreamAsync(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStreamAsync();
        }
    }
}
