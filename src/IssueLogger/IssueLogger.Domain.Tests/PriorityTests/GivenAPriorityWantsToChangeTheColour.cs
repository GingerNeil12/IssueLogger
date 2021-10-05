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
    public class GivenAPriorityWantsToChangeTheColour
    {
        [TestMethod]
        public void WhenTheNewColourIsValid_ThenTheColourShouldChangeToTheNewOne()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            var newColour = "NEW_COLOUR";

            // Act
            priorityUnderTest.ChangeColour(newColour);

            // Assert
            priorityUnderTest.Colour.Should().Be(newColour);
        }

        [TestMethod]
        public void WhenTheNewColourIsNull_ThenShouldThrowArgumentNullException()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            string newColour = null;

            // Act
            Action action = () => priorityUnderTest.ChangeColour(newColour);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(newColour))
                .WithMessage($"{Resources.ValueCannotBeNull} (Parameter '{nameof(newColour)}')");
        }

        [TestMethod]
        public void WhenTheNewColourIsEmpty_ThenShouldThrowArgumentException()
        {
            // Arrange
            var priorityUnderTest = CreatePriority();
            var newColour = string.Empty;

            // Act
            Action action = () => priorityUnderTest.ChangeColour(newColour);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithParameterName(nameof(newColour))
                .WithMessage($"{string.Format(Resources.ValueCannotBeEmpty, nameof(newColour))} (Parameter '{nameof(newColour)}')");

        }

        private static Priority CreatePriority()
        {
            return new Priority(Guid.NewGuid(), Guid.NewGuid(), "NAME", "COLOUR");
        }
    }
}
