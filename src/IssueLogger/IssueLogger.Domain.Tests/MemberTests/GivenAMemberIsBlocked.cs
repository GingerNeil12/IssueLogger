using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.MemberTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAMemberIsBlocked
    {
        [TestMethod]
        public void WhenUnblockIsCalled_ThenTheirBlockedStatusShouldShowThemUnblocked()
        {
            // Arrange
            var memberUnderTest = Member.Create("USER_ID", Guid.NewGuid());

            // Act
            memberUnderTest.Block();
            memberUnderTest.Unblock();

            // Assert
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeFalse();
            memberUnderTest.BlockedStatus.BlockedOn.Should().Be(new DateTime());
        }

        [TestMethod]
        public void WhenBlockIsCalled_ThenTheirBlockedStatusShouldStillBeBlocked()
        {
            // Arrange
            var memberUnderTest = Member.Create("USER_ID", Guid.NewGuid());

            // Act
            memberUnderTest.Block();
            memberUnderTest.Block();

            // Assert
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeTrue();
            memberUnderTest.BlockedStatus.BlockedOn.Should().BeOnOrBefore(DateTime.Now);
        }
    }
}
