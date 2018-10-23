using Entelect.TimesheetsToHollardJira.Domain.Configuration.Interface;

namespace Entelect.TimesheetsToHollardJira.Domain.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public string EntelectUsername { get; set; }
        public string EntelectPassword { get; set; }
        public int EntelectId { get; set; }
        public string HollardUsername { get; set; }
        public string HollardPassword { get; set; }
        public string HollardRemainingEstimate { get; set; }
    }
}
