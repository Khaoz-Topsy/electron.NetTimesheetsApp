using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Entelect.TimesheetsToHollardJira.Domain.Entity;
using Entelect.TimesheetsToHollardJira.Domain.Result;
using Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface;
using Newtonsoft.Json;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository
{
    public class EntelectTimesheetRepository : IEntelectTimesheetRepository
    {
        private readonly IHttpRepository _httpRepo;
        private readonly string _username;
        private readonly string _password;

        public EntelectTimesheetRepository(IHttpRepository httpRepo, string username, string password)
        {
            _httpRepo = httpRepo;
            _username = username;
            _password = password;
        }

        public async Task<ResultWithValue<int>> EntelectSignIn()
        {
            const string requestUrl = "";
            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "ReturnUrl", "/" },
                { "UserIdentifier", _username },
                { "Password", _password },
                { "PersistantLogin", "false" }
            });
            ResultWithValue<string> result = await _httpRepo.PostFormUrlContent(requestUrl, httpClient => { }, content);
            if (result.HasFailed) return new ResultWithValue<int>(false, 0, result.ExceptionMessage);

            try
            {
                int employeeIdIndex = result.Value.IndexOf("employeeId: ", StringComparison.Ordinal);
                string employeeIdSearchString = result.Value.Substring(employeeIdIndex, 18);
                int startOfEmployeeIdIndex = employeeIdSearchString.IndexOf(":", StringComparison.Ordinal);
                int endOfEmployeeIdIndex = employeeIdSearchString.IndexOf(",", StringComparison.Ordinal);
                string employeeIdString = employeeIdSearchString.Substring(startOfEmployeeIdIndex + 1, (endOfEmployeeIdIndex - startOfEmployeeIdIndex));
                int employeeIdResult = int.Parse(employeeIdString.Replace(",", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty));
                return new ResultWithValue<int>(true, employeeIdResult, result.ExceptionMessage);
            }
            catch (JsonException ex)
            {
                return new ResultWithValue<int>(false, 0, ex.Message);
            }
        }

        public async Task<ResultWithValue<Timesheet>> GetEntelectTimesheets(int employeeId, DateTime startDate, DateTime endDate)
        {
            string requestUrl = $@"";
            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "UserIdentifier", _username },
                { "Password", _password },
                { "PersistantLogin", "false" }
            });
            ResultWithValue<string> result = await _httpRepo.PostFormUrlContent(requestUrl,
                httpClient => {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }, content);
            if (result.HasFailed) return new ResultWithValue<Timesheet>(false, new Timesheet(), result.ExceptionMessage);

            try
            {
                Timesheet timesheet = JsonConvert.DeserializeObject<Timesheet>(result.Value);
                return new ResultWithValue<Timesheet>(true, timesheet, result.ExceptionMessage);
            }
            catch (JsonException ex)
            {
                return new ResultWithValue<Timesheet>(false, new Timesheet(), ex.Message);
            }
        }
    }
}
