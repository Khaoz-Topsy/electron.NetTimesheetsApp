using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Entelect.TimesheetsToHollardJira.Domain.Entity;
using Entelect.TimesheetsToHollardJira.Domain.Result;
using Entelect.TimesheetsToHollardJira.Integrate.Helper;
using Entelect.TimesheetsToHollardJira.Integrate.Mapper;
using Entelect.TimesheetsToHollardJira.Integrate.Repository;
using Entelect.TimesheetsToHollardJira.Integrate.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Entelect.TimesheetsToHollardJira.Portal.Models;

namespace Entelect.TimesheetsToHollardJira.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntelectTimesheetRepository _entelectTimesheetRepository;
        private readonly IHollardJiraRepository _hollardTimeSheetRepository;

        public HomeController(IEntelectTimesheetRepository entelectTimesheetRepository, IHollardJiraRepository hollardTimeSheetRepository)
        {
            _entelectTimesheetRepository = entelectTimesheetRepository;
            _hollardTimeSheetRepository = hollardTimeSheetRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }

        public async Task<IActionResult> Entelect(DateTime startDate, DateTime endDate)
        {
            DateTime startDateClean = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            DateTime endDateClean = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);
            ResultWithValue<int> entelectIdResult = await _entelectTimesheetRepository.EntelectSignIn();
            if (entelectIdResult.HasFailed)
            {
                return PartialView("ErrorTimesheet", new ErrorTimesheetViewModel("Entelect", "Failed to retrieve the list of Timesheet entries", "#entelect", "/Home/Entelect"));
            }

            ResultWithValue<Timesheet> timesheetResult = await _entelectTimesheetRepository.GetEntelectTimesheets(entelectIdResult.Value, startDateClean, endDateClean);
            if (timesheetResult.HasFailed)
            {
                return PartialView("ErrorTimesheet", new ErrorTimesheetViewModel("Entelect", "Failed to retrieve the list of Timesheet entries", "#entelect", "/Home/Entelect"));
            }

            HomeViewModel model = new HomeViewModel {Entelect = timesheetResult.Value.Days.RemoveUnnecessaryTimesheetEntries() };
            return PartialView("EntelectTimesheet", model);
        }

        public async Task<IActionResult> Hollard(DateTime startDate, DateTime endDate)
        {
            DateTime startDateClean = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            DateTime endDateClean = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);
            ResultWithValue<List<JiraTimesheet>> timesheet = await _hollardTimeSheetRepository.GetHollardTimesheets(startDateClean, endDateClean);
            if (timesheet.HasFailed)
            {
                return PartialView("ErrorTimesheet", new ErrorTimesheetViewModel("Hollard", "Failed to retrieve the list of Timesheet entries", "#hollard", "/Home/Hollard"));
            }

            HomeViewModel model = new HomeViewModel { Hollard = timesheet.Value };
            return PartialView("HollardTimesheet", model);
        }

        public async Task<IActionResult> SendToHollard(DateTime startDate, DateTime endDate, DateTime timesheetEntry, string categoryName, decimal hours, string comment)
        {
            string issue = categoryName.ToHollardIssue();
            ResultBase timesheetInsert = await _hollardTimeSheetRepository.InsertTimesheets(timesheetEntry, issue, hours, comment);
            if (timesheetInsert.HasFailed)
            {
                return PartialView("ErrorTimesheet", new ErrorTimesheetViewModel("Hollard", "Failed to insert timesheet entry into Hollard Jira", "#hollard", "/Home/Hollard"));
            }

            DateTime startDateClean = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            DateTime endDateClean = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);
            ResultWithValue<List<JiraTimesheet>> timesheet = await _hollardTimeSheetRepository.GetHollardTimesheets(startDateClean, endDateClean);
            if (timesheet.HasFailed)
            {
                return PartialView("ErrorTimesheet", new ErrorTimesheetViewModel("Hollard", "Failed to retrieve the list of Timesheet entries", "#hollard", "/Home/Hollard"));
            }

            HomeViewModel model = new HomeViewModel { Hollard = timesheet.Value };
            return PartialView("HollardTimesheet", model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
