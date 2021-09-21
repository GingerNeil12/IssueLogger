using System;
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
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), Resources.PropertyNullOrBlank);
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), Resources.PropertyNullOrBlank);
            }

            Id = id;
            Code = code;
            Name = name;
            _normalizedCode = code.Normalize();
            _normalizedName = name.Normalize();
        }

        public void ChangeCode(string newCode)
        {
            if (string.IsNullOrWhiteSpace(newCode))
            {
                throw new ArgumentNullException(nameof(newCode), Resources.PropertyNullOrBlank);
            }

            Code = newCode;
            _normalizedCode = newCode.Normalize();
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException(nameof(newName), Resources.PropertyNullOrBlank);
            }

            Name = newName;
            _normalizedName = newName.Normalize();
        }
    }
}
