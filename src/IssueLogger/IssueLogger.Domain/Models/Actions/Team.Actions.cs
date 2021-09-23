using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;

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
    }
}
