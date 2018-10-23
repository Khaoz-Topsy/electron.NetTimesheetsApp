namespace Entelect.TimesheetsToHollardJira.Domain.Configuration.Interface
{
    public interface IAppConfiguration
    {
        string EntelectUsername { get; set; }
        string EntelectPassword { get; set; }
        int EntelectId { get; set; }
        string HollardUsername { get; set; }
        string HollardPassword { get; set; }
        string HollardRemainingEstimate { get; set; }
    }
}