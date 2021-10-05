using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace IssueLogger.Domain.Tests.PriorityTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAPriorityIsNeeded
    {
        [TestMethod]
        public void WhenConstructedWithValidValues_ThenShouldNotBeNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var teamId = Guid.NewGuid();
            var name = "NAME";
            var colour = "COLOUR";
            var description = "DESCRIPTION";

            // Act
            var priorityUnderTest = new Priority(id, teamId, name, colour)
            {
                Description = description
            };

            // Assert
            priorityUnderTest.Should().NotBeNull();
            priorityUnderTest.Id.Should().Be(id);
            priorityUnderTest.TeamId.Should().Be(teamId);
            priorityUnderTest.Name.Should().Be(name);
            priorityUnderTest.NormalizedName.Should().Be(name.Normalize());
            priorityUnderTest.NormalizedName.IsNormalized().Should().BeTrue();
            priorityUnderTest.Colour.Should().Be(colour);
        }

        [TestMethod]
        [DynamicData(nameof(GetNullValues), DynamicDataSourceType.Method)]
        public void WhenConstructedWithNullValues_ThenShouldThrowArgumentNullException
        (
            string paramName,
            string name,
            string colour,
            string errorMessage
        )
        {
            // Arrange
            // Act
            Action action = () => new Priority(Guid.NewGuid(), Guid.NewGuid(), name, colour);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(paramName)
                .WithMessage(errorMessage);
        }

        [TestMethod]
        [DynamicData(nameof(GetEmptyValues), DynamicDataSourceType.Method)]
        public void WhenConstructedWithEmptyParameters_ThenShouldThrowArgumentException
        (
            string paramName,
            string name,
            string colour,
            string errorMessage
        )
        {
            // Arrange
            // Act
            Action action = () => new Priority(Guid.NewGuid(), Guid.NewGuid(), name, colour);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(paramName)
                .WithMessage(errorMessage);
        }

        private static IEnumerable<object[]> GetNullValues()
        {
            var name = "NAME";
            var colour = "COLOUR";

            yield return new object[] { nameof(name), null, colour, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(name)}')" };
            yield return new object[] { nameof(colour), name, null, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(colour)}')" };
        }

        private static IEnumerable<object[]> GetEmptyValues()
        {
            var name = "NAME";
            var colour = "COLOUR";

            yield return new object[] { nameof(name), string.Empty, colour, $"{string.Format(Resources.ValueCannotBeEmpty, nameof(name))} (Parameter '{nameof(name)}')" };
            yield return new object[] { nameof(colour), name, string.Empty, $"{string.Format(Resources.ValueCannotBeEmpty, nameof(colour))} (Parameter '{nameof(colour)}')" };
        }
    }
}
