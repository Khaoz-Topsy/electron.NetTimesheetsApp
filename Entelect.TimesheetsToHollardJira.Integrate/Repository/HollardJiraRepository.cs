using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Domain.Entity;
using Entelect.TimesheetsToHollardJira.Domain.Result;
using Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface;
using Newtonsoft.Json;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository
{
    public class HollardJiraRepository : IHollardJiraRepository
    {
        private readonly IHttpRepository _httpRepo;
        private readonly string _username;
        private readonly string _password;
        private readonly string _remainingEstimate;

        public HollardJiraRepository(IHttpRepository httpRepo, string username, string password, string remainingEstimate)
        {
            _httpRepo = httpRepo;
            _username = username;
            _password = password;
            _remainingEstimate = remainingEstimate;
        }

        public async Task<ResultWithValue<List<JiraTimesheet>>> GetHollardTimesheets(DateTime startDate, DateTime endDate)
        {
            string requestUrl = $@"";

            ResultWithValue<string> result = await _httpRepo.Get(requestUrl,
                httpClient => {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}")));
                });
            if (result.HasFailed) return new ResultWithValue<List<JiraTimesheet>>(false, new List<JiraTimesheet>(), result.ExceptionMessage);

            try
            {
                List<JiraTimesheet> timesheets = JsonConvert.DeserializeObject<List<JiraTimesheet>>(result.Value);
                return new ResultWithValue<List<JiraTimesheet>>(true, timesheets, result.ExceptionMessage);
            }
            catch (JsonException ex)
            {
                return new ResultWithValue<List<JiraTimesheet>>(false, new List<JiraTimesheet>(), ex.Message);
            }
        }


        public async Task<ResultBase> InsertTimesheets(DateTime insertDate, string issue, decimal hours, string comment)
        {
            string requestUrl = $"";

            ResultWithValue<string> result = await _httpRepo.PostFormUrlContent(requestUrl,
                httpClient => {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_username}:{_password}")));
                }, new FormUrlEncodedContent(new Dictionary<string, string>()));
            if (result.HasFailed) return new ResultBase(false, result.ExceptionMessage);

            try
            {
                return new ResultBase(result.Value.Contains("valid=\"true\""), result.ExceptionMessage);
            }
            catch (JsonException ex)
            {
                return new ResultBase(false, ex.Message);
            }
        }

    }
}
