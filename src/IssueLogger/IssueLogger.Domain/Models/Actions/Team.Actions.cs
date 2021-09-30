using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;
using System.Linq;

namespace IssueLogger.Domain.Models
{
    public partial class Team
    {
        public void ChangeCode(string newCode)
        {
            Code = Guard.Against.NullOrWhiteSpace(newCode, nameof(newCode), Resources.ValueCannotBeNull);
            _normalizedCode = newCode.Normalize();
        }

        public void ChangeName(string newName)
        {
            Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName), Resources.ValueCannotBeNull);
            _normalizedName = newName.Normalize();
        }

        public void BlockMember(string userId)
        {
            var member = _members
                .Where(member => member.UserId == userId)
                .FirstOrDefault();

            if (member is not null)
            {
                member.Block();
            }
        }

        public void UnblockMember(string userId)
        {
            var member = _members
                .Where(member => member.UserId == userId)
                .FirstOrDefault();

            if (member is not null)
            {
                member.Unblock();
            }
        }

        public void InviteMember
        (
            string invitedUserId,
            string invitedByUserId,
            string inviteCode
        )
        {
            var invite = _invitations
                .Where
                (
                    invite =>
                        invite.InvitedUserId == invitedUserId ||
                        invite.InviteCode == inviteCode
                )
                .FirstOrDefault();

            if (invite is null)
            {
                invite = TeamMemberInvitation.Create(Id, invitedUserId, invitedByUserId, inviteCode);
                _invitations.Add(invite);
            }
        }

        public void AcceptInvite(string inviteCode)
        {
            var invite = _invitations
                .Where(invite => invite.InviteCode == inviteCode)
                .FirstOrDefault();

            if (invite is not null && invite.Status == Enums.TeamMemberInvitationStatus.Pending)
            {
                invite.Accept();
                _members.Add(Member.Create(invite.InvitedUserId, Id));
            }
        }

        public void DeclineInvite(string inviteCode)
        {
            var invite = _invitations
                .Where(invite => invite.InviteCode == inviteCode)
                .FirstOrDefault();

            if (invite is not null && invite.Status == Enums.TeamMemberInvitationStatus.Pending)
            {
                invite.Decline();
            }
        }

        public void RevokeInvite(string invitedUserId)
        {
            var invite = _invitations
                .Where(invite => invite.InvitedUserId == invitedUserId)
                .FirstOrDefault();

            if (invite is not null)
            {
                invite.Revoke();
            }
        }
    }
}
