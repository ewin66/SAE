using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.Common.Check
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssertExtension
    {

        #region IsNullOrWhiteSpace
        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>由空null或一连串空格组成
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<string> IsNullOrWhiteSpace(this IAssert<string> assert)
        {
            return assert.IsNullOrWhiteSpace($"{assert.Name},不是空null或一连串空格组成");
        }

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>由空null或一连串空格组成
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<string> IsNullOrWhiteSpace(this IAssert<string> assert, string message)
        {
            if (!string.IsNullOrWhiteSpace(assert.Current))
            {
                throw new SAEException(message);
            }
            return assert;
        }
        #endregion

        #region NotNullOrWhiteSpace

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>不为空null或一连串空格
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<string> NotNullOrWhiteSpace(this IAssert<string> assert)
        {
            return assert.NotNullOrWhiteSpace($"{assert.Name},是null空或一连串连续空格组成");
        }

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>不为空null或一连串空格
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<string> NotNullOrWhiteSpace(this IAssert<string> assert, string message)
        {
            if (string.IsNullOrWhiteSpace(assert.Current))
            {
                throw new SAEException(message);
            }
            return assert;
        }
        #endregion


        #region True
        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为<see cref="bool.TrueString"/>
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<bool> True(this IAssert<bool> assert)
        {
            return assert.True($"{assert.Name},不满足true条件");
        }

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为<see cref="bool.TrueString"/>
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<bool> True(this IAssert<bool> assert, string message)
        {
            if (!assert.Current)
            {
                throw new SAEException(message);
            }
            return assert;
        }
        #endregion

        #region False

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为<see cref="bool.FalseString"/>
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<bool> False(this IAssert<bool> assert) => assert.False($"{assert.Name},不满足false条件");
        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为<see cref="bool.FalseString"/>
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<bool> False(this IAssert<bool> assert, string message)
        {
            if (assert.Current)
            {
                throw new SAEException(message);
            }
            return assert;
        }
        #endregion

        #region Null
        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为null
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<TAssert> Null<TAssert>(this IAssert<TAssert> assert) where TAssert : class
        {
            return assert.Null($"{assert.Name},不为null");
        }

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>为null
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<TAssert> Null<TAssert>(this IAssert<TAssert> assert, string message) where TAssert : class
        {
            if (assert.Current != null)
            {
                throw new SAEException(message);
            }
            return assert;
        }

        #endregion

        #region NotNull

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>不为null
        /// </summary>
        /// <param name="assert"></param>
        /// <returns></returns>
        public static IAssert<TAssert> NotNull<TAssert>(this IAssert<TAssert> assert) where TAssert : class
        {
            return assert.NotNull($"{assert.Name},不能为null");
        }

        /// <summary>
        /// <see cref="IAssert{TAssert}.Current"/>不为null
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IAssert<TAssert> NotNull<TAssert>(this IAssert<TAssert> assert, string message) where TAssert : class
        {
            if (assert.Current == null)
            {
                throw new SAEException(message);
            }
            return assert;
        } 
        #endregion
    }
}
