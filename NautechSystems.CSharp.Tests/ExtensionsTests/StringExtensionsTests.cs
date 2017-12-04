// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionsTests.cs" company="Nautech Systems Pty Ltd.">
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
    public class StringExtensionsTests
    {
        internal enum TestEnum
        {
            Unknown = 0,
            Test1 = 1,
            Test2 = 2
        }

        [Theory]
        [InlineData("A", "A")]
        [InlineData("123 123 1adc \n 222", "1231231adc222")]
        [InlineData("  123 123 1adc \n 222   ", "1231231adc222")]
        internal void RemoveWhiteSpace_WithVariousInputs_ReturnsStringWithNoWhiteSpace(string input, string expected)
        {
            // Arrange
            // Act
            var s = input.RemoveAllWhitespace();

            // Assert
            Assert.Equal(expected, s);
        }

        [Fact]
        internal void ToEnum_WhenWhiteSpaceString_ReturnsDefaultEnum()
        {
            // Arrange
            var someString = " ";

            // Act
            var result = someString.ToEnum<TestEnum>();

            // Assert
            Assert.Equal(TestEnum.Unknown, result);
        }

        [Fact]
        internal void ToEnum_WhenString_ReturnsExpectedEnum()
        {
            // Arrange
            var someString = "Test1";

            // Act
            var result = someString.ToEnum<TestEnum>();

            // Assert
            Assert.Equal(TestEnum.Test1, result);
        }
    }
}