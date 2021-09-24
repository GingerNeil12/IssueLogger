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
            memberUnderTest.Unblock();

            // Assert
            memberUnderTest.BlockedStatus.Should().NotBeNull();
            memberUnderTest.BlockedStatus.IsBlocked.Should().BeFalse();
            memberUnderTest.BlockedStatus.BlockedOn.Should().Be(new DateTime());
        }
    }
}
