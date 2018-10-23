namespace Entelect.TimesheetsToHollardJira.Domain.Entity
{
    public class TimesheetEntry
    {
        public int EntryId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float DurationInHours { get; set; }
        public bool Billable { get; set; }
        public int SentimentId { get; set; }
        public string Description { get; set; }
        public object TicketReference { get; set; }
        public bool IsSignedOff { get; set; }
        public string SignOffEmployeeName { get; set; }
    }
}
