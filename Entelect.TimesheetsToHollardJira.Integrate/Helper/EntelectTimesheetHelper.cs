using System.Collections.Generic;
using Entelect.TimesheetsToHollardJira.Domain.Entity;

namespace Entelect.TimesheetsToHollardJira.Integrate.Helper
{
    public static class EntelectTimesheetHelper
    {
        public static List<TimesheetDay> RemoveUnnecessaryTimesheetEntries(this List<TimesheetDay> days)
        {
            List<TimesheetDay> filteredDays = new List<TimesheetDay>();
            foreach (TimesheetDay timesheetDay in days)
            {
                List<TimesheetEntry> filteredEntries = new List<TimesheetEntry>();
                foreach (TimesheetEntry timesheetDayTimesheetEntry in timesheetDay.TimesheetEntries)
                {
                    if (timesheetDayTimesheetEntry.ProjectName.Equals("FP - Hollard - Broker Quotes"))
                    {
                        filteredEntries.Add(timesheetDayTimesheetEntry);
                    }
                }

                timesheetDay.TimesheetEntries = filteredEntries;
                filteredDays.Add(timesheetDay);
            }

            return filteredDays;
        }
    }
}
