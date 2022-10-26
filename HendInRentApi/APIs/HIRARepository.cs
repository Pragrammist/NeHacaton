using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendInRentApi
{

    /// <summary>
    /// HIRA - пристаквка означает что тип связан с работай апи
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TArg"></typeparam>
    public interface HIRARepository<TResult, TArg>
    {
        Task<TResult> MakePostJsonTypeRequest(string relativePath, string token, TArg? arg);
        Task<TResult> MakePutJsonTypeRequest(string relativePath, string token, TArg arg);
    }
    public interface HIRARepository<TResult>
    {
        Task<TResult> MakePostJsonTypeRequest(string relativePath, string token);
        Task<TResult> MakePutJsonTypeRequest(string relativePath, string token);
    }
}
