using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;
using System;

namespace IssueLogger.Domain.Models
{
    public partial class Priority : AuditableEntity
    {
        private string _normalizedName;
        private Team _team;

        public Guid Id { get; private set; }
        public Guid TeamId { get; private set; }
        public string Name { get; private set; }
        public string NormalizedName => _normalizedName;
        public string Description { get; set; }
        public string Colour { get; private set; }

        public virtual Team Team => _team;

        public Priority(Guid id, Guid teamId, string name, string colour)
        {
            Id = id;
            TeamId = teamId;
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Colour = Guard.Against.NullOrWhiteSpace(colour, nameof(colour));
            _normalizedName = name.Normalize();
        }
    }
}
