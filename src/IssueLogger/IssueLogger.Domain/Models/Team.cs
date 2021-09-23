using System;
using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;

namespace IssueLogger.Domain.Models
{
    public class Team : AuditableEntity
    {
        private string _normalizedCode;
        private string _normalizedName;

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string NormalizedCode => _normalizedCode;
        public string Name { get; private set; }
        public string NormalizedName => _normalizedName;

        public Team(Guid id, string code, string name)
        {
            Id = id;
            Code = Guard.Against.NullOrWhiteSpace(code, nameof(code), Resources.ValueCannotBeNull);
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name), Resources.ValueCannotBeNull);
            _normalizedCode = code.Normalize();
            _normalizedName = name.Normalize();
        }

        public void ChangeCode(string newCode)
        {
            if (string.IsNullOrWhiteSpace(newCode))
            {
                throw new ArgumentNullException(nameof(newCode), Resources.ValueCannotBeNull);
            }

            Code = newCode;
            _normalizedCode = newCode.Normalize();
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException(nameof(newName), Resources.ValueCannotBeNull);
            }

            Name = newName;
            _normalizedName = newName.Normalize();
        }
    }
}
