using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Enums;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class TeamMemberInvitation : AuditableEntity
    {
        private Team _team;
        private Member _invitedBy;

        public Guid TeamId { get; private set; }
        public string InvitedUserId { get; private set; }
        public string InvitedByUserId { get; private set; }
        public string InviteCode { get; private set; }
        public TeamMemberInvitationStatus Status { get; private set; }
        public DateTime InvitedOn { get; private set; }

        public virtual Team Team => _team;
        public virtual Member InvitedBy => _invitedBy;

        private TeamMemberInvitation
        (
            Guid teamId,
            string invitedUserId,
            string invitedByUserId,
            string inviteCode,
            TeamMemberInvitationStatus status,
            DateTime invitedOn
        )
        {
            TeamId = teamId;
            InvitedUserId = Guard.Against.NullOrWhiteSpace(invitedUserId, nameof(invitedUserId));
            InvitedByUserId = Guard.Against.NullOrWhiteSpace(invitedByUserId, nameof(invitedByUserId));
            InviteCode = Guard.Against.NullOrWhiteSpace(inviteCode, nameof(inviteCode));
            Status = status;
            InvitedOn = invitedOn;
        }
    }
}
