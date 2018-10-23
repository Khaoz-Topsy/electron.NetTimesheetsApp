using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Domain.Result;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface
{
    public interface IHttpRepository
    {
        Task<ResultWithValue<string>> Get(string requestUrl, Action<HttpClient> httpClientAction, Action<HttpResponseHeaders> headersAction = null);
        Task<ResultWithValue<string>> PostFormUrlContent(string requestUrl, Action<HttpClient> httpClientAction, FormUrlEncodedContent content, Action<HttpResponseHeaders> headersAction = null, Action<IEnumerable<Cookie>> cookieAction = null);
    }
}