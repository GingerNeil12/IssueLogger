using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.TeamTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAnInviteCodeIsNeeded
    {
        [TestMethod]
        public void WhenNotInUseAlready_ThenIsAlreadyInUseShouldReturnFalse()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            teamUnderTest.InviteMember("INVITED_USER_ID", "INVITED_BY_USER_ID", "INVITE_CODE");

            // Act
            var isInUse = teamUnderTest.IsInviteCodeInUse("NOT_IN_USE_INVITE_CODE");

            // Assert
            isInUse.Should().BeFalse();
        }

        [TestMethod]
        public void WhenTheCodeIsAlreadyInUse_ThenIsAlreadyInUseShouldReturnTrue()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var inUseInviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember("INVITED_USER_ID", "INVITED_BY_USER_ID", inUseInviteCode);

            // Act
            var isInUse = teamUnderTest.IsInviteCodeInUse(inUseInviteCode);

            // Assert
            isInUse.Should().BeTrue();
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "CODE", "NAME");
        }
    }
}
