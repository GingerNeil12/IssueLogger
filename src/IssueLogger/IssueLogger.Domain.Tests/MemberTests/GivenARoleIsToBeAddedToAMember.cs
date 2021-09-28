using FluentAssertions;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IssueLogger.Domain.Tests.MemberTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenARoleIsToBeAddedToAMember
    {
        [TestMethod]
        public void WhenTheyAreNotAlreadyInThatRole_ThenTheRoleShouldBeAddedToThem()
        {
            // Arrange
            var memberUnderTest = CreateMember();
            var roleId = Guid.NewGuid().ToString();

            // Act
            memberUnderTest.AddRole(roleId);

            // Assert
            memberUnderTest.Roles.Should().NotBeNull();
            memberUnderTest.Roles.Should().HaveCount(1);
            memberUnderTest.Roles.First().RoleId.Should().Be(roleId);
            memberUnderTest.Roles.First().UserId.Should().Be(memberUnderTest.UserId);
            memberUnderTest.Roles.First().TeamId.Should().Be(memberUnderTest.TeamId);
            memberUnderTest.Roles.First().Addedon.Should().BeOnOrBefore(DateTime.Now);
        }

        [TestMethod]
        public void WhenTheyAreAlreadyInTheRole_ThenTheRoleShouldNotBeAddedTwice()
        {
            // Arrange
            var memberUnderTest = CreateMember();
            var roleId = Guid.NewGuid().ToString();

            // Act
            memberUnderTest.AddRole(roleId);
            memberUnderTest.AddRole(roleId);

            // Assert
            memberUnderTest.Roles.Should().NotBeNull();
            memberUnderTest.Roles.Count(role => role.RoleId == roleId).Should().Be(1);
            memberUnderTest.Roles.First().RoleId.Should().Be(roleId);
            memberUnderTest.Roles.First().UserId.Should().Be(memberUnderTest.UserId);
            memberUnderTest.Roles.First().TeamId.Should().Be(memberUnderTest.TeamId);
            memberUnderTest.Roles.First().Addedon.Should().BeOnOrBefore(DateTime.Now);
        }

        private static Member CreateMember()
        {
            return Member.Create("USER_ID", Guid.NewGuid());
        }
    }
}
