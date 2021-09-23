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
    public class GivenATeamWantsToChangeCode
    {
        [TestMethod]
        public void WhenAValidNewCodeIsProvided_ThenTheCodeShouldChangeToTheNewOne()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var newCode = "New Code";

            // Act
            teamUnderTest.ChangeCode(newCode);

            // Assert
            teamUnderTest.Code.Should().Be(newCode);
            teamUnderTest.NormalizedCode.Should().Be(newCode.Normalize());
            teamUnderTest.NormalizedCode.IsNormalized().Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheNewCodeIsNull_ThenChangeCodeThrowsArgumentNullException()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            string newCode = null;
            var errorMessage = $"{Resources.ValueCannotBeNull} (Parameter '{nameof(newCode)}')";

            // Act
            Action action = () => teamUnderTest.ChangeCode(newCode);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newCode))
                .WithMessage(errorMessage);
        }

        [TestMethod]
        public void WhenTheNewCodeIsEmpty_ThenChangeCodeThrowsArgumentException()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var newCode = string.Empty;
            var errorMessage = $"{string.Format(Resources.ValueCannotBeEmpty, nameof(newCode))} (Parameter '{nameof(newCode)}')";

            // Act
            Action action = () => teamUnderTest.ChangeCode(newCode);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(newCode))
                .WithMessage(errorMessage);
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "Code", "Name");
        }
    }
}
