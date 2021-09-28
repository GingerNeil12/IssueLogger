using IssueLogger.Domain.Enums;

namespace IssueLogger.Domain.Models
{
    public partial class TeamMemberInvitation
    {
        public void Accept()
        {
            if (Status == TeamMemberInvitationStatus.Pending)
            {
                Status = TeamMemberInvitationStatus.Accepted;
            }
        }

        public void Decline()
        {
            if (Status == TeamMemberInvitationStatus.Pending)
            {
                Status = TeamMemberInvitationStatus.Declined;
            }
        }

        public void Revoke()
        {
            if (Status == TeamMemberInvitationStatus.Pending)
            {
                Status = TeamMemberInvitationStatus.Revoked;
            }
        }
    }
}
