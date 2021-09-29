using FluentAssertions;
using IssueLogger.Domain.Enums;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.TeamMemberInvitationTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAnInvitationHasBeenMade
    {
        [TestMethod]
        public void WhenTheCurrentStatusIsPendingAndTheInviteIsAccepted_ThenTheStatusShouldShowAccepted()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();

            // Act
            inviteUnderTest.Accept();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Accepted);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsRevokedAndTheInviteIsAccepted_ThenTheStatusShouldNotShowAccepted()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();
            inviteUnderTest.Revoke();

            // Act
            inviteUnderTest.Accept();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Revoked);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsDeclinedAndTheInviteIsAccepted_ThenTheStatusShouldNotShowAccepted()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();
            inviteUnderTest.Decline();

            // Act
            inviteUnderTest.Accept();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Declined);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsPendingAndTheInviteIsDeclined_ThenTheStatusShouldShowDeclined()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();

            // Act
            inviteUnderTest.Decline();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Declined);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsRevokedAndTheInviteIsDeclined_ThenTheStatusShouldNotShowDeclined()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();
            inviteUnderTest.Revoke();

            // Act
            inviteUnderTest.Decline();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Revoked);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsPendingAndTheInviteIsRevoked_ThenTheStatusShouldShowRevoked()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();

            // Act
            inviteUnderTest.Revoke();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Revoked);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsDeclinedAndTheInviteIsRevoked_ThenTheStatusShouldShowRevoked()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();
            inviteUnderTest.Decline();

            // Act
            inviteUnderTest.Revoke();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Revoked);
        }

        [TestMethod]
        public void WhenTheCurrentStatusIsAcceptedAndTheInviteIsRevoked_ThenTheStatusShouldShowAccepted()
        {
            // Arrange
            var inviteUnderTest = CreateInvite();
            inviteUnderTest.Accept();

            // Act
            inviteUnderTest.Revoke();

            // Assert
            inviteUnderTest.Status.Should().Be(TeamMemberInvitationStatus.Accepted);
        }

        private static TeamMemberInvitation CreateInvite()
        {
            return TeamMemberInvitation.Create(Guid.NewGuid(), "INVITED_USER_ID", "INVITED_BY_USER_ID", "INVITE_CODE");
        }
    }
}
