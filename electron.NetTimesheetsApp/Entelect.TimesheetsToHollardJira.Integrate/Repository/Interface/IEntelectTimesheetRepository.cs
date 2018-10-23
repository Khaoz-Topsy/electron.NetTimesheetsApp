using System;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Domain.Entity;
using Entelect.TimesheetsToHollardJira.Domain.Result;

namespace Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface
{
    public interface IEntelectTimesheetRepository
    {
        Task<ResultWithValue<Timesheet>> GetEntelectTimesheets(int employeeId, DateTime startDate, DateTime endDate);
        Task<ResultWithValue<int>> EntelectSignIn();
    }
}