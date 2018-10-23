using System.Collections.Generic;

namespace Entelect.TimesheetsToHollardJira.Integrate.Mapper
{
    public static class EntelectCategoryNameToHollardIssueMapper
    {
        private static readonly Dictionary<string, string> EntelectHollardIssue = new Dictionary<string, string>
        {
            { "Software Development", "NPI-27" },
            { "Analysis", "NPI-28" },
            { "Project Meeting", "NPI-29" },
            { "Support", "NPI-30" },
            { "General", "NPI-31" }
        };

        public static string ToHollardIssue(this string entelectCategory)
        {
            return EntelectHollardIssue.ContainsKey(entelectCategory) ? EntelectHollardIssue[entelectCategory] : "NPI-27";
        }
    }
}
