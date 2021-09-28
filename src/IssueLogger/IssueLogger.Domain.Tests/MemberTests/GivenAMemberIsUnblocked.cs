﻿using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.MemberTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAMemberIsUnblocked
    {
        [TestMethod]
        public void WhenBlockIsCalled_ThenTheMembersBlockedStatusShouldUpdateToShowBlocked()
        {
            // Arrange
            var memberUnderTest = CreateMember();

            // Act
            memberUnderTest.Block();

            // Assert
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeTrue();
            memberUnderTest.BlockedStatus.BlockedOn.Should().BeOnOrBefore(DateTime.Now);
        }

        [TestMethod]
        public void WhenUnblockIsCalled_ThenTheirBlockedStatusShouldStillBeUnblocked()
        {
            // Arrange
            var memberUnderTest = CreateMember();

            // Act
            memberUnderTest.Unblock();

            // Assert
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeFalse();
            memberUnderTest.BlockedStatus.BlockedOn.Should().Be(new DateTime());

        }

        private static Member CreateMember()
        {
            return Member.Create("USER_ID", Guid.NewGuid());
        }
    }
}
