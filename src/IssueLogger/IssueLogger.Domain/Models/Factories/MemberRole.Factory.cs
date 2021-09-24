using System;

namespace IssueLogger.Domain.Models
{
    public partial class MemberRole
    {
        public static MemberRole Create(string userId, Guid teamId, string roleId)
        {
            return new MemberRole(userId, teamId, roleId, DateTime.Now);
        }
    }
}
