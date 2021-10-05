using FluentAssertions;
using IssueLogger.Domain.Common;
using IssueLogger.Domain.Models;
using IssueLogger.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IssueLogger.Domain.Tests.PriorityTests
{
    [TestClass]
    [TestCategory(TestCategories.Unit)]
    public class GivenAPriorityWantsToChangeTheName
    {
        [TestMethod]
        public void WhenTheNewNameIsValid_ThenShouldChangeToNewName()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            var newName = "NEW_NAME";

            // Act
            priorityUnderTest.ChangeName(newName);

            // Assert
            priorityUnderTest.Name.Should().Be(newName);
            priorityUnderTest.NormalizedName.Should().Be(newName.Normalize());
            priorityUnderTest.NormalizedName.IsNormalized().Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheNewNameIsNull_ThenShouldThrowArgumentNullException()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            string newName = null;

            // Act
            Action action = () => priorityUnderTest.ChangeName(newName);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newName))
                .WithMessage($"{Resources.ValueCannotBeNull} (Parameter '{nameof(newName)}')");
        }

        [TestMethod]
        public void WhenTheNewNameIsEmpty_ThenShouldThrowArgumentException()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            var newName = string.Empty;

            // Act
            Action action = () => priorityUnderTest.ChangeName(newName);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(newName))
                .WithMessage($"{string.Format(Resources.ValueCannotBeEmpty, nameof(newName))} (Parameter '{nameof(newName)}')");
        }

        private static Priority CreatePriority()
        {
            return new Priority(Guid.NewGuid(), Guid.NewGuid(), "NAME", "COLOUR");
        }
    }
}
