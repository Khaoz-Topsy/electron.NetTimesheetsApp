using System.Runtime.InteropServices.ComTypes;

namespace Entelect.TimesheetsToHollardJira.Portal.Models
{
    public class ErrorTimesheetViewModel
    {
        public ErrorTimesheetViewModel(string title, string message, string id, string url)
        {
            Title = title;
            Message = message;
            Id = id;
            Url = url;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }
}
