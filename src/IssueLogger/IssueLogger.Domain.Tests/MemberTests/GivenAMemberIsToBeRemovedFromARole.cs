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
    public class GivenAMemberIsToBeRemovedFromARole
    {
        [TestMethod]
        public void WhenTheyHaveThatRole_ThenItShouldBeRemoved()
        {
            // Arrange
            var memberUnderTest = CreateMember();
            var roleIdOne = Guid.NewGuid().ToString();
            var roleIdTwo = Guid.NewGuid().ToString();
            memberUnderTest.AddRole(roleIdOne);
            memberUnderTest.AddRole(roleIdTwo);

            // Act
            memberUnderTest.RemoveRole(roleIdOne);

            // Assert
            memberUnderTest.Roles.Should().NotBeNull();
            memberUnderTest.Roles.Count(role => role.RoleId == roleIdOne).Should().Be(0);
            memberUnderTest.Roles.Should().HaveCount(1);
            memberUnderTest.Roles.First().RoleId.Should().Be(roleIdTwo);
        }

        [TestMethod]
        public void WhenTheyDoNotHaveThatRole_ThenTheirRolesShouldBeUnaffected()
        {
            // Arrange
            var memberUnderTest = CreateMember();
            var roleId = Guid.NewGuid().ToString();
            memberUnderTest.AddRole(roleId);

            // Act
            memberUnderTest.RemoveRole(Guid.NewGuid().ToString());

            // Assert
            memberUnderTest.Roles.Should().NotBeNull();
            memberUnderTest.Roles.Count(role => role.RoleId == roleId).Should().Be(1);
            memberUnderTest.Roles.First().RoleId.Should().Be(roleId);
        }

        private static Member CreateMember()
        {
            return Member.Create("USER_ID", Guid.NewGuid());
        }
    }
}
