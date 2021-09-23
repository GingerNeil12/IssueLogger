using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.TestTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenATeamWantsToChangeName
    {
        [TestMethod]
        public void WhenAValidNewNameIsProvided_ThenTheNameShouldChangeToTheNewOne()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var newName = "New Name";

            // Act
            teamUnderTest.ChangeName(newName);

            // Assert
            teamUnderTest.Name.Should().Be(newName);
            teamUnderTest.NormalizedName.Should().Be(newName.Normalize());
            teamUnderTest.NormalizedName.IsNormalized().Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheNewNameIsNull_ThenChangeNameThrowsArgumentNullException()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            string newName = null;
            var errorMessage = $"{Resources.ValueCannotBeNull} (Parameter '{nameof(newName)}')";

            // Act
            Action action = () => teamUnderTest.ChangeName(newName);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newName))
                .WithMessage(errorMessage);
        }

        [TestMethod]
        public void WhenTheNewNameIsEmpty_ThenChangeNameThrowsArgumentException()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var newName = string.Empty;
            var errorMessage = $"{string.Format(Resources.ValueCannotBeEmpty, nameof(newName))} (Parameter '{nameof(newName)}')";

            // Act
            Action action = () => teamUnderTest.ChangeName(newName);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(newName))
                .WithMessage(errorMessage);
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "Code", "Name");
        }
    }
}
