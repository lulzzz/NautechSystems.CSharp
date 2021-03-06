﻿// -------------------------------------------------------------------------------------------------
// <copyright file="QueryExtensionsTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Tests.ExtensionsTests
{
    using System.Diagnostics.CodeAnalysis;
    using NautechSystems.CSharp.Extensions;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class QueryExtensionsTests
    {
        private const string errorMessage = "this failed";

        [Fact]
        public void OnFailure_WithQueryFailed_ExecutesChangeValueAction()
        {
            // Arrange
            var testBool = false;

            // Act
            var myResult = Query<TestClass>.Fail(errorMessage);
            myResult.OnFailure(() => testBool = true);

            // Assert
            Assert.True(testBool);
        }

        [Fact]
        public void OnFailure_WithQueryFailed_ExecutesChangeStringAction()
        {
            // Arrange
            var testError = string.Empty;

            // Act
            var result = Query<TestClass>.Fail(errorMessage);
            result.OnFailure(error => testError = error);

            // Assert
            Assert.Equal($"Query Failure ({errorMessage}).", testError);
        }

        private class TestClass
        {
            public string Property { get; set; }
        }
    }
}