using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.MemberTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenANewMemberIsNeeded
    {
        [TestMethod]
        public void WhenConstructedWithValidProperties_ThenShouldNotBeNull()
        {
            // Arrange
            var userId = "USER ID";
            var teamId = Guid.NewGuid();

            // Act
            var memberUnderTest = Member.Create(userId, teamId);

            // Assert
            memberUnderTest.Should().NotBeNull();
            memberUnderTest.UserId.Should().Be(userId);
            memberUnderTest.TeamId.Should().Be(teamId);
            memberUnderTest.JoinedOn.Should().BeOnOrBefore(DateTime.Now);
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeFalse();
            memberUnderTest.BlockedStatus.BlockedOn.Should().Be(new DateTime());
        }

        [TestMethod]
        public void WhenConstructedWithNullProperties_ThenThrowsArgumentNullException()
        {
            // Arrange
            string userId = null;
            var errorMessage = $"{Resources.ValueCannotBeNull} (Parameter '{nameof(userId)}')";

            // Act
            Action action = () => Member.Create(userId, Guid.NewGuid());

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(userId))
                .WithMessage(errorMessage);
        }

        [TestMethod]
        public void WhenConstructedWithEmptyProperties_ThenThrowsArgumentException()
        {
            // Arrange
            var userId = string.Empty;
            var errorMessage = $"{string.Format(Resources.ValueCannotBeEmpty, nameof(userId))} (Parameter '{nameof(userId)}')";

            // Act
            Action action = () => Member.Create(userId, Guid.NewGuid());

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(userId))
                .WithMessage(errorMessage);
        }
    }
}
