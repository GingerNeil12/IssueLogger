using System;
using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueLogger.Domain.Tests.TestTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenATeamHasBeenCreated
    {
        [TestMethod]
        public void WhenChangeCodeIsCalledWithValidProperties_ThenTheCodeShouldChange()
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
        [DataRow("")]
        [DataRow(null)]
        public void WhenChangeCodeIsCalledWithInvalidProperties_ThenThrowsArgumentNullException(string newCode)
        {
            // Arrange
            var teamUnderTest = CreateTeam();

            // Act
            Action action = () => teamUnderTest.ChangeCode(newCode);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newCode))
                .WithMessage($"{Resources.PropertyNullOrBlank} (Parameter '{nameof(newCode)}')");
        }

        [TestMethod]
        public void WhenCallingChangeNameWithValidProperties_ThenTheNameShouldChange()
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
        [DataRow("")]
        [DataRow(null)]
        public void WhenCallingChangeNameWithInvalidProperties_ThenThrowsArgumentNullException(string newName)
        {
            // Arrange
            var teamUnderTest = CreateTeam();

            // Act
            Action action = () => teamUnderTest.ChangeName(newName);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newName))
                .WithMessage($"{Resources.PropertyNullOrBlank} (Parameter '{nameof(newName)}')");
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "Code", "Name");
        }
    }
}
