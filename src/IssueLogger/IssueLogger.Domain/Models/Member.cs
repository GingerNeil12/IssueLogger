using Ardalis.GuardClauses;
using IssueLogger.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;

namespace IssueLogger.Domain.Models
{
    public partial class Member
    {
        private Team _team;
        private List<MemberRole> _roles = new();
        private List<TeamMemberInvitation> _invitationsSent = new();

        public string UserId { get; private set; }
        public Guid TeamId { get; private set; }
        public DateTime JoinedOn { get; private set; }
        public BlockedStatus BlockedStatus { get; set; }

        public virtual Team Team => _team;
        public virtual IReadOnlyList<MemberRole> Roles => _roles.AsReadOnly();
        public virtual IReadOnlyList<TeamMemberInvitation> InvitationsSent => _invitationsSent.AsReadOnly();

        private Member(string userId, Guid teamId, DateTime joinedOn)
        {
            UserId = Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            TeamId = teamId;
            JoinedOn = joinedOn;
        }
    }
}
