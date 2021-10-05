using Ardalis.GuardClauses;

namespace IssueLogger.Domain.Models
{
    public partial class Priority
    {
        public void ChangeName(string newName)
        {
            Name = Guard.Against.NullOrWhiteSpace(newName, nameof(newName));
            _normalizedName = newName.Normalize();
        }

        public void ChangeColour(string newColour)
        {
            Colour = Guard.Against.NullOrWhiteSpace(newColour, nameof(newColour));
        }
    }
}
