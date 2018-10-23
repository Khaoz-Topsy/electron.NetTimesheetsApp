using System;
using Newtonsoft.Json;

namespace Entelect.TimesheetsToHollardJira.Domain.Entity
{
    public class JiraTimesheet
    {
        [JsonProperty("timeSpentSeconds")]
        public int TimeSpentSeconds { get; set; }

        public decimal TimeSpentHours => TimeSpentSeconds / 60.0M / 60.0M;

        [JsonProperty("dateStarted")]
        public DateTime DateStarted { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("dateUpdated")]
        public DateTime DateUpdated { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("jiraWorklogId")]
        public int JiraWorklogId { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("issue")]
        public Issue Issue { get; set; }

        [JsonProperty("worklogAttributes")]
        public object[] WorklogAttributes { get; set; }

        [JsonProperty("workAttributeValues")]
        public object[] WorkAttributeValues { get; set; }
    }

    public class Author
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }

    public class Issue
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("remainingEstimateSeconds")]
        public int RemainingEstimateSeconds { get; set; }

        [JsonProperty("issueType")]
        public Issuetype IssueType { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class Issuetype
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
    }

}

