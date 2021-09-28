using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IssueLogger.Domain.Tests.TeamMemberInvitationTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenATeamMemberInvitationIsNeeded
    {
        [TestMethod]
        public void WhenConstructedWithValidProperties_ThenShouldNotBeNull()
        {
            // Arrange
            var teamId = Guid.NewGuid();
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";

            // Act
            var inviteUnderTest = TeamMemberInvitation.Create(teamId, invitedUserId, invitedByUserId, inviteCode);

            // Assert
            inviteUnderTest.Should().NotBeNull();
            inviteUnderTest.TeamId.Should().Be(teamId);
            inviteUnderTest.InvitedUserId.Should().Be(invitedUserId);
            inviteUnderTest.InvitedByUserId.Should().Be(invitedByUserId);
            inviteUnderTest.InviteCode.Should().Be(inviteCode);
            inviteUnderTest.Status.Should().Be(Enums.TeamMemberInvitationStatus.Pending);
            inviteUnderTest.InvitedOn.Should().BeOnOrBefore(DateTime.Now);
        }

        [TestMethod]
        [DynamicData(nameof(GetNullData), DynamicDataSourceType.Method)]
        public void WhenConstructedWithNullValues_ThenThrowsNullArgumentException
        (
            string paramName,
            string invitedUserId,
            string invitedByUserId,
            string inviteCode,
            string errorMessage
        )
        {
            // Arrange
            // Act
            Action action = () => TeamMemberInvitation.Create(Guid.NewGuid(), invitedUserId, invitedByUserId, inviteCode);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(paramName)
                .WithMessage(errorMessage);
        }

        [TestMethod]
        [DynamicData(nameof(GetEmptyData), DynamicDataSourceType.Method)]
        public void WhenConstructedWithEmptyValues_ThenThrowsArgumentException
        (
            string paramName,
            string invitedUserId,
            string invitedByUserId,
            string inviteCode,
            string errorMessage
        )
        {
            // Arrange
            // Act
            Action action = () => TeamMemberInvitation.Create(Guid.NewGuid(), invitedUserId, invitedByUserId, inviteCode);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(paramName)
                .WithMessage(errorMessage);
        }

        private static IEnumerable<object[]> GetNullData()
        {
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";

            yield return new object[] { nameof(invitedUserId), null, invitedByUserId, inviteCode, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(invitedUserId)}')" };
            yield return new object[] { nameof(invitedByUserId), invitedUserId, null, inviteCode, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(invitedByUserId)}')" };
            yield return new object[] { nameof(inviteCode), invitedUserId, invitedByUserId, null, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(inviteCode)}')" };
        }

        private static IEnumerable<object[]> GetEmptyData()
        {
            var invitedUserId = "INVITED_USER_ID";
            var invitedByUserId = "INVITED_BY_USER_ID";
            var inviteCode = "INVITE_CODE";

            yield return new object[] { nameof(invitedUserId), string.Empty, invitedByUserId, inviteCode, $"{string.Format(Resources.ValueCannotBeEmpty, nameof(invitedUserId))} (Parameter '{nameof(invitedUserId)}')" };
            yield return new object[] { nameof(invitedByUserId), invitedUserId, string.Empty, inviteCode, $"{string.Format(Resources.ValueCannotBeEmpty, nameof(invitedByUserId))} (Parameter '{nameof(invitedByUserId)}')" };
            yield return new object[] { nameof(inviteCode), invitedUserId, invitedByUserId, string.Empty, $"{string.Format(Resources.ValueCannotBeEmpty, nameof(inviteCode))} (Parameter '{nameof(inviteCode)}')" };
        }
    }
}
