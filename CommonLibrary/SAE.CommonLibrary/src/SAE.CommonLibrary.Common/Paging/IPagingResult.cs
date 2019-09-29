using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace SAE.CommonLibrary.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPagingResult:IPaging,IEnumerable
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IPagingResult<out TResult> : IPagingResult,IEnumerable<TResult>
    {

    }


}
