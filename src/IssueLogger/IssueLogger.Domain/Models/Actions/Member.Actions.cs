using IssueLogger.Domain.Models.ValueObjects;
using System;
using System.Linq;

namespace IssueLogger.Domain.Models
{
    public partial class Member
    {
        public void Block()
        {
            if(!BlockedStatus.IsBlocked)
            {
                BlockedStatus = new BlockedStatus
                {
                    IsBlocked = true,
                    BlockedOn = DateTime.Now
                };
            }
        }

        public void Unblock()
        {
            if (BlockedStatus.IsBlocked)
            {
                BlockedStatus = new BlockedStatus
                {
                    IsBlocked = false,
                    BlockedOn = new DateTime()
                };
            }
        }

        public void AddRole(string roleId)
        {
            var role = _roles
                .Where
                (
                    role =>
                        role.RoleId == roleId &&
                        role.TeamId == TeamId
                )
                .FirstOrDefault();

            if (role is null)
            {
                role = MemberRole.Create(UserId, TeamId, roleId);
                _roles.Add(role);
            }
        }

        public void RemoveRole(string roleId)
        {
            _roles.RemoveAll
            (
                role =>
                    role.RoleId == roleId &&
                    role.TeamId == TeamId
            );
        }
    }
}
