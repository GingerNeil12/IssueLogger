using Ardalis.GuardClauses;
using IssueLogger.Domain.Models.ValueObjects;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class Member
    {
        private Team _team;

        public string UserId { get; private set; }
        public Guid TeamId { get; private set; }
        public DateTime JoinedOn { get; private set; }
        public BlockedStatus BlockedStatus { get; set; }

        public virtual Team Team => _team;

        private Member(string userId, Guid teamId, DateTime joinedOn)
        {
            UserId = Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            TeamId = teamId;
            JoinedOn = joinedOn;
        }
    }
}
