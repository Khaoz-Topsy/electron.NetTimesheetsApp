using System;
using System.Collections.Generic;
using Entelect.TimesheetsToHollardJira.Domain.Entity;

namespace Entelect.TimesheetsToHollardJira.Portal.Models
{
    public class HomeViewModel
    {
        public List<TimesheetDay> Entelect { get; set; }
        public List<JiraTimesheet> Hollard { get; set; }
    }
}
