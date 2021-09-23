using IssueLogger.Domain.Models.ValueObjects;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class Member
    {
        public static Member Create(string userId, Guid teamId)
        {
            return new Member(userId, teamId, DateTime.Now)
            {
                BlockedStatus = new BlockedStatus
                {
                    IsBlocked = false,
                    BlockedOn = new DateTime()
                }
            };
        }
    }
}
