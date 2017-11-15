// -------------------------------------------------------------------------------------------------
// <copyright file="QueryTests.cs" company="Nautech Systems Pty Ltd.">
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
    public class QueryTests
    {
        [Fact]
        public void Ok_WithGenericResult_ReturnsOk()
        {
            // Arrange
            var testClass = new TestClass();

            // Act
            var result = Query<TestClass>.Ok(testClass);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.Equal(testClass, result.Value);
        }

        [Fact]
        public void ActionInvoked_WithGenericNoValue_Throws()
        {
            // Arrange
            Action action = () => { Query<TestClass>.Ok(null); };

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => action.Invoke());
        }

        [Fact]
        public void ActionInvoked_ResultWithNoError_Throws()
        {
            // Arrange
            var result = Query<TestClass>.Ok(new TestClass());

            Action action = () =>
            {
                var error = result.Error;
            };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }

        [Fact]
        public void Fail_GenericWithValueInputs_ReturnsExpectedResult()
        {
            // Arrange
            // Act
            var result = Query<TestClass>.Fail("Error message");

            // Assert
            Assert.Equal("Error message", result.Error);
            Assert.True(result.IsFailure);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void ActionInvoke_AttemptingToAccessValueWithNoValue_Throws()
        {
            // Arrange
            var result = Query<TestClass>.Fail("Error message");

            Action action = () => { TestClass testClass = result.Value; };

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }

        [Fact]
        public void Fail_AllTypesWithNoErrorMessage_Throws()
        {
            // Arrange
            Action action1 = () => { Query<TestClass>.Fail(null); };
            Action action2 = () => { Query<TestClass>.Fail(string.Empty); };

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => action1.Invoke());
            Assert.Throws<ArgumentNullException>(() => action2.Invoke());
        }

        private class TestClass
        {
        }
    }
}
