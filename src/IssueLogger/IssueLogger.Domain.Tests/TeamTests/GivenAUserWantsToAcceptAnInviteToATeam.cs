using System;
using System.Linq;
using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueLogger.Domain.Tests.TeamTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAUserWantsToAcceptAnInviteToATeam
    {
        [TestMethod]
        public void WhenTheInviteCodeIsValid_ThenTheUserShouldBeAddedToTheMemberList()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);

            // Act
            teamUnderTest.AcceptInvite(inviteCode);

            // Assert
            teamUnderTest.Members.Should().NotBeNull();
            teamUnderTest.Members.Should().HaveCount(1);
            teamUnderTest.Members.First().UserId.Should().Be(invitedUserId);
            teamUnderTest.Members.First().TeamId.Should().Be(teamUnderTest.Id);
            teamUnderTest.Members.First().BlockedStatus.Should().NotBeNull();
            teamUnderTest.Members.First().BlockedStatus.IsBlocked.Should().BeFalse();
            teamUnderTest.Members.First().JoinedOn.Should().BeOnOrBefore(DateTime.Now);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Accepted);
        }

        [TestMethod]
        public void WhenTheInviteCodeIsNotValid_ThenNoNewUserShouldBeAdded()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);

            // Act
            teamUnderTest.AcceptInvite("INVALID_INVITE_CODE");

            // Assert
            teamUnderTest.Members.Should().HaveCount(0);
        }

        [TestMethod]
        public void WhenTheInviteHasAlreadyBeenAccepted_ThenTheMemberShouldNotBeAddedTwice()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);
            teamUnderTest.AcceptInvite(inviteCode);

            // Act
            teamUnderTest.AcceptInvite(inviteCode);

            // Assert
            teamUnderTest.Members.Should().NotBeNull();
            teamUnderTest.Members.Should().HaveCount(1);
            teamUnderTest.Members.First().UserId.Should().Be(invitedUserId);
            teamUnderTest.Members.First().TeamId.Should().Be(teamUnderTest.Id);
            teamUnderTest.Members.First().BlockedStatus.Should().NotBeNull();
            teamUnderTest.Members.First().BlockedStatus.IsBlocked.Should().BeFalse();
            teamUnderTest.Members.First().JoinedOn.Should().BeOnOrBefore(DateTime.Now);
            teamUnderTest.Invitations.First().Status.Should().Be(Enums.TeamMemberInvitationStatus.Accepted);
        }

        [TestMethod]
        public void WhenTheInviteHasAlreadyBeenDeclined_ThenTheMemberShouldNotBeAdded()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);
            teamUnderTest.DeclineInvite(inviteCode);

            // Act
            teamUnderTest.AcceptInvite(inviteCode);

            // Assert
            teamUnderTest.Members.Should().HaveCount(0);
        }

        [TestMethod]
        public void WhenTheInviteHasAlreadyBeenRevoked_ThenTheMemberShouldNotBeAdded()
        {
            // Arrange
            var teamUnderTest = CreateTeam();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";
            teamUnderTest.InviteMember(invitedUserId, invitedByUserId, inviteCode);
            teamUnderTest.RevokeInvite(invitedUserId);

            // Act
            teamUnderTest.AcceptInvite(inviteCode);

            // Assert
            teamUnderTest.Members.Should().HaveCount(0);
        }

        private static Team CreateTeam()
        {
            return new Team(Guid.NewGuid(), "CODE", "NAME");
        }
    }
}
