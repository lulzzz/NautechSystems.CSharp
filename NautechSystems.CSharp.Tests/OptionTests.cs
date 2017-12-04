﻿// -------------------------------------------------------------------------------------------------
// <copyright file="OptionTests.cs" company="Nautech Systems Pty Ltd.">
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
    using NautechSystems.CSharp;
    using NautechSystems.CSharp.Validation;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class OptionTests
    {
        [Fact]
        internal void Create_WithNull_HasNoValue()
        {
            // Arrange
            // Act
            Option<TestClass> option = null;

            // Assert
            Assert.True(option.HasNoValue);
            Assert.False(option.HasValue);
        }

        [Fact]
        internal void Create_WithImplicitOperator_HasValue()
        {
            // Arrange
            var instance = new TestClass();

            // Act
            Option<TestClass> option = instance;

            // Assert
            Assert.True(option.HasValue);
            Assert.False(option.HasNoValue);
            Assert.Equal(instance, option.Value);
        }

        [Fact]
        internal void None_WithTestClass_ReturnsOptionWithNoValue()
        {
            // Arrange
            // Act
            var result = Option<TestClass>.None();

            // Assert
            Assert.True(result.HasNoValue);
            Assert.False(result.HasValue);
        }

        [Fact]
        internal void None_WithNullableStruct_ReturnsOptionWithNoValue()
        {
            // Arrange
            // Act
            var result = Option<DateTime?>.None();

            // Assert
            Assert.True(result.HasNoValue);
            Assert.False(result.HasValue);
        }

        [Fact]
        internal void Value_WithNoValue_Throws()
        {
            // Arrange
            Option<TestClass> option = null;

            // Act
            Assert.Throws<ValidationException>(() => option.Value);
        }

        [Fact]
        internal void From_WithDateTime_ReturnsOptionWithTheValue()
        {
            // Arrange
            var time = new DateTime(2000, 1, 1);

            // Act
            var result = Option<DateTime>.Some(time);

            // Assert
            Assert.Equal(time, result.Value);
        }

        [Fact]
        internal void Equals_OptionCreatedWithNullAndNone_ReturnsTrue()
        {
            // Arrange
            Option<TestClass> option1 = null;
            Option<TestClass> option2 = Option<TestClass>.None();

            // Act
            var result = option1.Equals(option2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        internal void Equals_WhenOneOptionNull_ReturnsFalse()
        {
            // Arrange
            var option1 = Option<TestClass>.Some(new TestClass());
            var option2 = Option<TestClass>.None();

            // Act
            var result = option1.Equals(option2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        internal void Equals_WhenOptionsHaveSameValue_ReturnsTrue()
        {
            // Arrange
            var dateTime1 = new DateTime(2000, 1, 1);
            var dateTime2 = new DateTime(2000, 1, 1);

            var option1 = Option<DateTime>.Some(dateTime1);
            var option2 = Option<DateTime>.Some(dateTime2);

            // Act - Ignore suspcicious comparison warning (this is to call the equals overrride).
            var result1 = option1.Equals((object)option2);
            var result2 = option1.Equals(option2);
            var result3 = option1.Equals((object)dateTime2);
            var result4 = option1.Equals(dateTime2);
            var result5 = option1 == option2;
            var result6 = option1 == dateTime2;

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.True(result6);
        }

        [Fact]
        internal void Equals_WhenOptionsHaveDifferentValues_ReturnsFalse()
        {
            // Arrange
            var dateTime1 = new DateTime(2000, 1, 1);
            var dateTime2 = new DateTime(2000, 1, 2);

            var option1 = Option<DateTime>.Some(dateTime1);
            var option2 = Option<DateTime>.Some(dateTime2);

            // Act - Ignore suspcicious comparison warning (this is to call the equals overrride).
            var result1 = option1.Equals((object)option2);
            var result2 = option1.Equals(option2);
            var result3 = option1.Equals((object)dateTime2);
            var result4 = option1.Equals(dateTime2);
            var result5 = option1 != option2;
            var result6 = option1 != dateTime2;

            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
            Assert.False(result4);
            Assert.True(result5);
            Assert.True(result6);
        }

        [Fact]
        internal void Equals_WithString_ReturnsFalse()
        {
            // Arrange
            var option = Option<TestClass>.Some(new TestClass());

            // Act - Ignore suspcious comparison warning (this is part of the test).
            var result = option.Equals("string");

            // Assert
            Assert.False(result);
        }

        [Fact]
        internal void GetHashCode_WithNoValue_ReturnsZero()
        {
            // Arrange
            Option<TestClass> option = null;

            // Act
            var result = option.GetHashCode();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        internal void GetHashCode_WithValue_ReturnsExpectedInt()
        {
            // Arrange
            var option = Option<TestClass>.Some(new TestClass());

            // Act
            var result = option.GetHashCode();

            // Assert
            Assert.Equal(42, result);
        }

        [Fact]
        internal void ToString_WithNoValue_ReturnsExpectedString()
        {
            // Arrange
            Option<TestClass> option = null;

            // Act
            var result = option.ToString();

            // Assert
            Assert.Equal("HasNoValue", result);
        }

        [Fact]
        internal void ToString_WithValue_ReturnsValueObjectsToString()
        {
            // Arrange
            Option<TestClass> option = new TestClass();

            // Act
            var result = option.ToString();

            // Assert
            Assert.Equal("test_value", result);
        }


        private class TestClass
        {
            public override int GetHashCode() => 42;

            public override string ToString() => "test_value";
        }
    }
}