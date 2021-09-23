using IssueLogger.Domain.Models.ValueObjects;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class Member
    {
        public void Block()
        {
            BlockedStatus = new BlockedStatus
            {
                IsBlocked = true,
                BlockedOn = DateTime.Now
            };
        }

        public void Unblock()
        {
            BlockedStatus = new BlockedStatus
            {
                IsBlocked = false,
                BlockedOn = new DateTime()
            };
        }
    }
}
