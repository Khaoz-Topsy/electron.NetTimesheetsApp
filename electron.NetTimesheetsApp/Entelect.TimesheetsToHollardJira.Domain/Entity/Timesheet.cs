using System.Collections.Generic;
using Newtonsoft.Json;

namespace Entelect.TimesheetsToHollardJira.Domain.Entity
{
    public class Timesheet
    {
        [JsonProperty("days")]
        public List<TimesheetDay> Days { get; set; }
    }
}

