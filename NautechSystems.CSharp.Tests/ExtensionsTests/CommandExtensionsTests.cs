﻿// -------------------------------------------------------------------------------------------------
// <copyright file="CommandExtensionsTests.cs" company="Nautech Systems Pty Ltd.">
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
    public class CommandExtensionsTests
    {
        private static readonly string _errorMessage = "this failed";

        [Fact]
        public void OnSuccess_WithAnonymousFunction_PerformsFunction()
        {
            // Arrange
            var testBool = false;

            // Act
            var command = Command.Ok();
            command.OnSuccess(() => testBool = true);

            // Assert
            Assert.True(testBool);
        }

        [Fact]
        public void OnFailure_WithFailure_InvokesAction()
        {
            // Arrange
            var testBool = false;

            // Act
            var command = Command.Fail(_errorMessage);
            command.OnFailure(() => testBool = true);

            // Assert
            Assert.True(testBool);
        }

        private class TestClass
        {
            public string Property { get; set; }
        }
    }
}
