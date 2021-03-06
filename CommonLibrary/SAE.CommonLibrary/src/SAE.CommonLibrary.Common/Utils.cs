using SAE.CommonLibrary.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 生成有序的Guid
        /// </summary>
        /// <returns></returns>
        public static Guid GenerateId()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);

        }
    }
}
