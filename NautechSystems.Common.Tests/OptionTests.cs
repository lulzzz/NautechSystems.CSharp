// -------------------------------------------------------------------------------------------------
// <copyright file="OptionTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class OptionTests
    {
        [Fact]
        internal void None_WhenString_ReturnsOptionWithNoValue()
        {
            // Arrange
            // Act
            var result = Option<string>.None;

            // Assert
            Assert.True(result.HasNoValue);
            Assert.False(result.HasValue);
        }

        [Fact]
        internal void None_WhenNullableStruct_ReturnsOptionWithNoValue()
        {
            // Arrange
            // Act
            var result = Option<DateTime?>.None;

            // Assert
            Assert.True(result.HasNoValue);
            Assert.False(result.HasValue);
        }
    }
}