using System;

namespace IssueLogger.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
