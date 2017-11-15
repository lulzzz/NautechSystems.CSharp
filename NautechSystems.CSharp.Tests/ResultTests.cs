// -------------------------------------------------------------------------------------------------
// <copyright file="ResultTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
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
    public class ResultTests
    {
        [Fact]
        public void Ok_ReturnsOk()
        {
            // Arrange
            // Act
            var result = Result.Ok();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

        [Fact]
        public void Ok_WithGenericResult_ReturnsOk()
        {
            // Arrange
            var testClass = new TestClass();

            // Act
            var result = Result.Ok(testClass);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(testClass, result.Value);
        }

        [Fact]
        public void ActionInvoked_WithGenericNoValue_Throws()
        {
            // Arrange
            Action action = () => { Result.Ok((TestClass)null); };

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => action.Invoke());
        }

        [Fact]
        public void ActionInvoked_WithNoValue_Throws()
        {
            // Arrange
            var result = Result.Ok();

            Action action = () =>
            {
                var error = result.Error;
            };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }

        [Fact]
        public void ActionInvoked_ResultWithNoError_Throws()
        {
            // Arrange
            var result = Result.Ok(new TestClass());

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
            Result result = Result.Fail("Error message");

            // Assert
            Assert.Equal("Error message", result.Error);
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Fail_GenericWithValueInputs_ReturnsExpectedResult()
        {
            // Arrange
            // Act
            var result = Result.Fail<TestClass>("Error message");

            // Assert
            Assert.Equal("Error message", result.Error);
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void ActionInvoke_AttemptingToAccessValueWithNoValue_Throws()
        {
            // Arrange
            var result = Result.Fail<TestClass>("Error message");

            Action action = () => { TestClass testClass = result.Value; };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }

        [Fact]
        public void Fail_AllTypesWithNoErrorMessage_Throws()
        {
            // Arrange
            Action action1 = () => { Result.Fail(null); };
            Action action2 = () => { Result.Fail(string.Empty); };
            Action action3 = () => { Result.Fail<TestClass>(null); };
            Action action4 = () => { Result.Fail<TestClass>(string.Empty); };

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => action1.Invoke());
            Assert.Throws<ArgumentNullException>(() => action2.Invoke());
            Assert.Throws<ArgumentNullException>(() => action3.Invoke());
            Assert.Throws<ArgumentNullException>(() => action4.Invoke());
        }

        [Fact]
        public void FirstFailureOrSuccess_WithFailures_ReturnsFirstResult()
        {
            // Arrange
            var result1 = Result.Ok();
            var result2 = Result.Fail("Failure 1");
            var result3 = Result.Fail("Failure 2");

            // Act
            var result = Result.FirstFailureOrSuccess(result1, result2, result3);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Failure 1", result.Error);
        }

        [Fact]
        public void FirstFailureOrSuccess_WithNoFailures_ReturnsSuccess()
        {
            // Arrange
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var result3 = Result.Ok();

            // Act
            var result = Result.FirstFailureOrSuccess(result1, result2, result3);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Combine_WithErrors_ReturnsExpectedResult()
        {
            // Arrange
            var result1 = Result.Ok();
            var result2 = Result.Fail("Failure 1");
            var result3 = Result.Fail("Failure 2");

            // Act
            var result = Result.Combine("; ", result1, result2, result3);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Failure 1; Failure 2", result.Error);
        }

        [Fact]
        public void Combine_AllOk_ReturnsExpectedResult()
        {
            // Arrange
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var result3 = Result.Ok("Some string");

            // Act
            var result = Result.Combine("; ", result1, result2, result3);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Combine_InArray_ReturnsExpectedResult()
        {
            // Arrange
            Result<string>[] results = { Result.Ok(""), Result.Ok("") };

            // Act
            var result = Result.Combine("; ", results);

            // Assert
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public void Combine_GenericsInArray_ReturnsExpectedResult()
        {
            // Arrange
            Result<string>[] results = { Result.Ok(""), Result.Fail<string>("m") };

            // Act
            var result = Result.Combine("; ", results);

            // Assert
            Assert.True(result.IsFailure);
        }

        private class TestClass
        {
        }
    }
}