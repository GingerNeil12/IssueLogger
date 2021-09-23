using System;
using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;

namespace IssueLogger.Domain.Models
{
    public partial class Team : AuditableEntity
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
    }
}
