using System;
using System.Collections.Generic;

namespace Entelect.TimesheetsToHollardJira.Domain.Entity
{
    public class TimesheetDay
    {
        public List<TimesheetEntry> TimesheetEntries { get; set; }
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
    }
}
