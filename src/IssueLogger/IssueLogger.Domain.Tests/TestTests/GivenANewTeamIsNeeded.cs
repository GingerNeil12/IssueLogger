using System;
using System.Collections.Generic;
using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssueLogger.Domain.Tests.TestTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenANewTeamIsNeeded
    {
        [TestMethod]
        public void WhenConstructedWithValidProperties_ThenShouldNotBeNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Code";
            var name = "Name";

            // Act
            var teamUnderTest = new Team(id, code, name);

            // Assert
            teamUnderTest.Should().NotBeNull();
            teamUnderTest.Id.Should().Be(id);
            teamUnderTest.Code.Should().Be(code);
            teamUnderTest.Name.Should().Be(name);
            teamUnderTest.NormalizedCode.Should().Be(code.Normalize());
            teamUnderTest.NormalizedCode.IsNormalized().Should().BeTrue();
            teamUnderTest.NormalizedName.Should().Be(name.Normalize());
            teamUnderTest.NormalizedName.IsNormalized().Should().BeTrue();
        }

        [TestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void WhenConstructedWithInvalidProperties_ThenThrowsArgumentNullException
        (
            string paramName,
            string code,
            string name,
            string errorMessage
        )
        {
            // Arrange
            // Act
            Action action = () => new Team(Guid.NewGuid(), code, name);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(paramName)
                .WithMessage(errorMessage);
        }

        private static IEnumerable<object[]> GetData()
        {
            var code = "Code";
            var name = "Name";

            yield return new object[] { nameof(code), string.Empty, name, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(code)}')" };
            yield return new object[] { nameof(code), null, name, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(code)}')" };
            yield return new object[] { nameof(name), code, string.Empty, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(name)}')" };
            yield return new object[] { nameof(name), code, null, $"{Resources.ValueCannotBeNull} (Parameter '{nameof(name)}')" };
        }
    }
}
