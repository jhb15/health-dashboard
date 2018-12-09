﻿namespace health_dashboard.Models
{
    public class Challenge
    {
        public int challengeId { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public int activityId { get; set; }
        public ChallengeActivity activity { get; set; }
        public int percentComplete { get; set; }
        public bool isGroupChallenge { get; set; }
        public int goal { get; set; }
        public bool repeat { get; set; }
        public string goalMetric { get; set; }
    }

    public class ChallengeActivity
    {
        public int activityId { get; set; }
        public string activityName { get; set; }
    }

    public class UserChallenge
    {
        public int userChallengeId { get; set; }
        public string userId { get; set; }
        public int challengeId { get; set; }
        public int percentageComplete { get; set; }
    }
}

