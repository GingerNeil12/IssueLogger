using System;

namespace IssueLogger.Domain.Models
{
    public partial class TeamMemberInvitation
    {
        public static TeamMemberInvitation Create
        (
            Guid teamId,
            string invitedUserId,
            string invitedByUserId,
            string inviteCode
        )
        {
            return new TeamMemberInvitation
            (
                teamId,
                invitedUserId,
                invitedByUserId,
                inviteCode,
                Enums.TeamMemberInvitationStatus.Pending,
                DateTime.Now
            );
        }
    }
}
