using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendInRentApi.APIs
{

    /// <summary>
    /// HIRA - Hend In Rent Api. So it repository for api. you could get token from auth class or interface.
    /// </summary>
    /// <typeparam name="TResult">serializable type as type of result, that you read from body request</typeparam>
    /// <typeparam name="TArg">serializable type that you use when write arg in body request as input arg for api</typeparam>
    public interface HIRARepository<TResult, TArg>
    {
        Task<TResult> MakePostJsonTypeRequest(string relativePath, string token, TArg? arg);
        Task<TResult> MakePostJsonTypeRequest(string relativePath, string token);
        Task<TResult> MakePutJsonTypeRequest(string relativePath, string token);
        Task<TResult> MakePutJsonTypeRequest(string relativePath, string token, TArg arg);
    }
}
