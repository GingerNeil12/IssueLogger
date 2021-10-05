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
    public class GivenAUserWantsToDeclineAnInviteToATeam
    {
        [TestMethod]
        public void WhenTheInviteIsCurrentlyPending_ThenTheInviteShouldBeSetToDeclined()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);

            // Act
            teamUnderTest.DeclineInvite(inviteCode);

            // Assert
            teamUnderTest.Invitations.Should().NotBeNull();
            teamUnderTest.Invitations.Should().HaveCount(1);
            teamUnderTest.Invitations.First().InvitedUserId.Should().Be(invitedUserId);
            teamUnderTest.Invitations.First().InvitedByUserId.Should().Be(invitedByUserId);
            teamUnderTest.Invitations.First().InviteCode.Should().Be(inviteCode);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Declined);
        }

        [TestMethod]
        public void WhenTheInviteHasBeenAccepted_ThenTheInviteCannotBeDeclined()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);
            teamUnderTest.AcceptInvite(inviteCode);

            // Act
            teamUnderTest.DeclineInvite(inviteCode);

            // Assert
            teamUnderTest.Invitations.Should().NotBeNull();
            teamUnderTest.Invitations.Should().HaveCount(1);
            teamUnderTest.Invitations.First().InvitedUserId.Should().Be(invitedUserId);
            teamUnderTest.Invitations.First().InvitedByUserId.Should().Be(invitedByUserId);
            teamUnderTest.Invitations.First().InviteCode.Should().Be(inviteCode);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Accepted);
        }

        [TestMethod]
        public void WhenTheInviteHasBeenRevoked_ThenTheInviteCannotBeDeclined()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);
            teamUnderTest.RevokeInvite(invitedUserId);

            // Act
            teamUnderTest.DeclineInvite(inviteCode);

            // Assert
            teamUnderTest.Invitations.Should().NotBeNull();
            teamUnderTest.Invitations.Should().HaveCount(1);
            teamUnderTest.Invitations.First().InvitedUserId.Should().Be(invitedUserId);
            teamUnderTest.Invitations.First().InvitedByUserId.Should().Be(invitedByUserId);
            teamUnderTest.Invitations.First().InviteCode.Should().Be(inviteCode);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Revoked);
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "CODE", "NAME");
        }
    }
}
