using Entelect.TimesheetsToHollardJira.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository
{
    public class HttpRepository : IHttpRepository
    {

        public async Task<ResultWithValue<string>> Get(string requestUrl, Action<HttpClient> httpClientAction, Action<HttpResponseHeaders> headersAction = null)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    httpClientAction(client);
                    using (HttpResponseMessage response = await client.GetAsync(requestUrl))
                    {
                        response.EnsureSuccessStatusCode();
                        headersAction?.Invoke(response.Headers);
                        return new ResultWithValue<string>(true, await response.Content.ReadAsStringAsync(), string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultWithValue<string>(false, string.Empty, ex.Message);
            }
        }

        public async Task<ResultWithValue<string>> PostFormUrlContent(string requestUrl, Action<HttpClient> httpClientAction, FormUrlEncodedContent content, Action<HttpResponseHeaders> headersAction = null, Action<IEnumerable<Cookie>> cookieAction = null)
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler {CookieContainer = cookies};
            try
            {

                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClientAction(client);
                    using (HttpResponseMessage response = await client.PostAsync(requestUrl, content))
                    {
                        response.EnsureSuccessStatusCode();
                        headersAction?.Invoke(response.Headers);

                        Uri uri = new Uri(requestUrl);
                        cookieAction?.Invoke(cookies.GetCookies(uri).Cast<Cookie>());

                        return new ResultWithValue<string>(true, await response.Content.ReadAsStringAsync(), string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultWithValue<string>(false, string.Empty, ex.Message);
            }
        }
    }
}
