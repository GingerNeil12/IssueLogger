using Ardalis.GuardClauses;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class MemberRole
    {
        private Member _member;
        private Team _team;

        public string UserId { get; private set; }
        public Guid TeamId { get; private set; }
        public string RoleId { get; private set; }
        public DateTime Addedon { get; private set; }

        public virtual Member Member => _member;
        public virtual Team Team => _team;

        private MemberRole(string userId, Guid teamId, string roleId, DateTime addedOn)
        {
            UserId = Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            TeamId = teamId;
            RoleId = Guard.Against.NullOrWhiteSpace(roleId, nameof(roleId));
            Addedon = addedOn;
        }
    }
}
