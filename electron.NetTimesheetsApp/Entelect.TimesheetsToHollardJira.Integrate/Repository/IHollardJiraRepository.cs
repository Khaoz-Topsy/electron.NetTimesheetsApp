using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Domain.Entity;
using Entelect.TimesheetsToHollardJira.Domain.Result;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository
{
    public interface IHollardJiraRepository
    {
        Task<ResultWithValue<List<JiraTimesheet>>> GetHollardTimesheets(DateTime startDate, DateTime endDate);
        Task<ResultBase> InsertTimesheets(DateTime insertDate, string issue, decimal hours, string comment);
    }
}