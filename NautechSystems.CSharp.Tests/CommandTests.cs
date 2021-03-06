﻿// -------------------------------------------------------------------------------------------------
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
    using NautechSystems.CSharp.Validation;
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
        public void Ok_WithMessage_ReturnsOkWithMessage()
        {
            // Arrange
            var message = "The command was successful.";

            // Act
            var result = Command.Ok(message);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void Fail_WithValidInputs_ReturnsExpectedResult()
        {
            // Arrange
            // Act           
            var command = Command.Fail("error message");

            // Assert
            Assert.Equal("Command Failure (error message).", command.Message);
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
            Assert.Throws<ValidationException>(() => action1.Invoke());
            Assert.Throws<ValidationException>(() => action2.Invoke());
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
            Assert.Equal("Command Failure (Failure 1).", result.Message);
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
            var result2 = Command.Fail("error 1");
            var result3 = Command.Fail("error 2");

            // Act
            var result = Command.Combine(result1, result2, result3);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Command Failure (error 1; error 2).", result.Message);
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