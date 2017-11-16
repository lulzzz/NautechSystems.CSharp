// -------------------------------------------------------------------------------------------------
// <copyright file="CommandTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class CommandTests
    {
        [Fact]
        public void Ok_ReturnsOk()
        {
            // Arrange
            // Act
            var result = Command.Ok();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

        [Fact]
        public void ActionInvoked_WithNoValue_Throws()
        {
            // Arrange
            var result = Command.Ok();

            Action action = () =>
            {
                var error = result.Error;
            };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }

        [Fact]
        public void Fail_WithValidInputs_ReturnsExpectedResult()
        {
            // Arrange
            // Act           
            var command = Command.Fail("Error message");

            // Assert
            Assert.Equal("Error message", command.Error);
            Assert.True(command.IsFailure);
            Assert.False(command.IsSuccess);
        }

        [Fact]
        public void Fail_WithNoErrorMessage_Throws()
        {
            // Arrange
            Action action1 = () => { Command.Fail(null); };
            Action action2 = () => { Command.Fail(string.Empty); };

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => action1.Invoke());
            Assert.Throws<ArgumentNullException>(() => action2.Invoke());
        }

        [Fact]
        public void FirstFailureOrSuccess_WithFailures_ReturnsFirstResult()
        {
            // Arrange
            var result1 = Command.Ok();
            var result2 = Command.Fail("Failure 1");
            var result3 = Command.Fail("Failure 2");

            // Act
            var result = Command.FirstFailureOrSuccess(result1, result2, result3);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Failure 1", result.Error);
        }

        [Fact]
        public void FirstFailureOrSuccess_WithNoFailures_ReturnsSuccess()
        {
            // Arrange
            var result1 = Command.Ok();
            var result2 = Command.Ok();
            var result3 = Command.Ok();

            // Act
            var result = Command.FirstFailureOrSuccess(result1, result2, result3);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Combine_WithErrors_ReturnsExpectedResult()
        {
            // Arrange
            var result1 = Command.Ok();
            var result2 = Command.Fail("Failure 1");
            var result3 = Command.Fail("Failure 2");

            // Act
            var result = Command.Combine(result1, result2, result3);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Failure 1; Failure 2", result.Error);
        }

        [Fact]
        public void Combine_AllOk_ReturnsExpectedResult()
        {
            // Arrange
            var result1 = Command.Ok();
            var result2 = Command.Ok();
            var result3 = Command.Ok();

            // Act
            var result = Command.Combine(result1, result2, result3);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Combine_InArray_ReturnsExpectedResult()
        {
            // Arrange
            Command[] commands = { Command.Ok(), Command.Ok() };

            // Act
            var result = Command.Combine(commands);

            // Assert
            Assert.True(result.IsSuccess);
        }

        private class TestClass
        {
        }
    }
}