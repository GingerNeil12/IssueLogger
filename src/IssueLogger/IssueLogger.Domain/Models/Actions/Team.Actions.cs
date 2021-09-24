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
    }
}
