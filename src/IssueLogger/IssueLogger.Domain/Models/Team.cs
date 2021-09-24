﻿using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using IssueLogger.Domain.Common;

namespace IssueLogger.Domain.Models
{
    public partial class Team : AuditableEntity
    {
        private string _normalizedCode;
        private string _normalizedName;

        private List<Member> _members = new();
        private List<MemberRole> _memberRoles = new();

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string NormalizedCode => _normalizedCode;
        public string Name { get; private set; }
        public string NormalizedName => _normalizedName;

        public virtual IReadOnlyList<Member> Members => _members.AsReadOnly();
        public virtual IReadOnlyList<MemberRole> MemberRoles => _memberRoles.AsReadOnly();

        public Team(Guid id, string code, string name)
        {
            Id = id;
            Code = Guard.Against.NullOrWhiteSpace(code, nameof(code));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            _normalizedCode = code.Normalize();
            _normalizedName = name.Normalize();
        }
    }
}
