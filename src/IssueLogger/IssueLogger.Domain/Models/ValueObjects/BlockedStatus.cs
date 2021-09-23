using System;

namespace IssueLogger.Domain.Models.ValueObjects
{
    public class BlockedStatus
    {
        public bool IsBlocked { get; set; }
        public DateTime BlockedOn { get; set; }
    }
}
