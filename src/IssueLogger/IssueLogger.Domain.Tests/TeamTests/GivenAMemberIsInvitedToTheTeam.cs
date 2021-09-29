using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IssueLogger.Domain.Tests.TeamTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAMemberIsInvitedToTheTeam
    {
        [TestMethod]
        public void WhenTheMemberHasntBeenInvitedAlready_ThenTheInviteShouldBeAddedToTheInvitesList()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";

            // Act
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);

            // Assert
            teamUnderTest.Invitations.Should().NotBeNull();
            teamUnderTest.Invitations.Should().HaveCount(1);
            teamUnderTest.Invitations.First().TeamId.Should().Be(teamUnderTest.Id);
            teamUnderTest.Invitations.First().InvitedUserId.Should().Be(invitedUserId);
            teamUnderTest.Invitations.First().InvitedByUserId.Should().Be(invitedByUserId);
            teamUnderTest.Invitations.First().InviteCode.Should().Be(inviteCode);
            teamUnderTest.Invitations.First().InvitedOn.Should().BeOnOrBefore(DateTime.Now);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Pending);
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "CODE", "NAME");
        }
    }
}
