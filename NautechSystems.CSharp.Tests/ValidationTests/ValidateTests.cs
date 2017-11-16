﻿// -------------------------------------------------------------------------------------------------
// <copyright file="ValidateTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Tests.ValidationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using NautechSystems.CSharp.Validation;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class ValidateTests
    {
        [Fact]
        internal void True_WhenFalse_Throws()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.True(false, "some_evaluation"));
        }

        [Fact]
        internal void True_WhenTrue_ReturnsNull()
        {
            // Arrange
            // Act
            // Assert
            Validate.True(true, "some_evaluation");
        }

        [Fact]
        internal void TrueIf_WhenConditionTrueAndPredicateFalse_Throws()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.TrueIf(true, false, "some_evaluation"));
        }

        [Fact]
        internal void TrueIf_WhenConditionTrueAndPredicateTrue_DoesNothing()
        {
            // Arrange
            // Act
            // Assert
            Validate.TrueIf(true, true, "some_evaluation");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        internal void NotNull_WithVariousObjectAndInvalidStrings_Throws(string value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Validate.NotNull(value, nameof(value)));
        }

        [Fact]
        internal void NotNull_WithAString_DoesNothing()
        {
            // Arrange
            var obj = "something";

            // Act
            // Assert
            Validate.NotNull(obj, nameof(obj));
        }

        [Fact]
        internal void NotNullTee_WithNullObject_Throws()
        {
            // Arrange
            object obj = null;

            // Act
            // Assert - Ignore expression is always null warning (the point of the test is to catch this condition).
            Assert.Throws<ArgumentNullException>(() => Validate.NotNull(obj, nameof(obj)));
        }

        [Fact]
        internal void NotNullTee_WithAnObject_DoesNothing()
        {
            // Arrange
            object obj = new EventArgs();

            // Act
            // Assert
            Validate.NotNull(obj, nameof(obj));
        }

        [Fact]
        internal void CollectionEmpty_WhenCollectionEmpty_DoesNothing()
        {
            // Arrange
            List<string> collection = new List<string>();

            // Act
            // Assert
            Validate.CollectionEmpty(collection, nameof(collection));
        }

        [Fact]
        internal void CollectionEmpty_WhenCollectionNotEmpty_Throws()
        {
            // Arrange
            List<string> collection = new List<string> { "anElement" };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.CollectionEmpty(collection, nameof(collection)));
        }

        [Fact]
        internal void CollectionEmpty_WhenCollectionNull_Throws()
        {
            // Arrange
            List<string> collection = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Validate.CollectionEmpty(collection, nameof(collection)));
        }

        [Fact]
        internal void CollectionNotNullOrEmpty_WhenCollectionNotEmpty_DoesNothing()
        {
            // Arrange
            List<string> collection = new List<string> { "foo" };

            // Act
            // Assert
            Validate.CollectionNotNullOrEmpty(collection, nameof(collection));
        }

        [Fact]
        internal void CollectionNotNullOrEmpty_WhenCollectionNull_Throws()
        {
            // Arrange
            List<string> collection = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Validate.CollectionNotNullOrEmpty(collection, nameof(collection)));
        }

        [Fact]
        internal void CollectionNotNullOrEmpty_WhenCollectionEmpty_Throws()
        {
            // Arrange
            var collection = new List<string>();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.CollectionNotNullOrEmpty(collection, nameof(collection)));
        }

        [Fact]
        internal void CollectionNotNullOrEmpty_WhenDictionaryNull_Throws()
        {
            // Arrange
            Dictionary<string, int> dictionary = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Validate.CollectionNotNullOrEmpty(dictionary, nameof(dictionary)));
        }

        [Fact]
        internal void CollectionNotNullOrEmpty_WhenDictionaryEmpty_Throws()
        {
            // Arrange
            var dictionary = new Dictionary<string, int>();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.CollectionNotNullOrEmpty(dictionary, nameof(dictionary)));
        }

        [Fact]
        internal void CollectionContains_WhenCollectionDoesNotContainElement_Throws()
        {
            // Arrange
            var element = "the_fifth_element";
            var collection = new List<string>();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.CollectionContains(element, nameof(element), collection));
        }

        [Fact]
        internal void CollectionContains_WhenCollectionContainsElement_DoesNothing()
        {
            // Arrange
            var element = "the_fifth_element";
            var collection = new List<string> { element };

            // Act
            // Assert
            Validate.CollectionContains(element, nameof(element), collection);
        }

        [Fact]
        internal void CollectionDoesNotContain_WhenCollectionDoesNotContainElement_DoesNothing()
        {
            // Arrange
            var element = "the_fifth_element";
            var collection = new List<string>();

            // Act
            // Assert
            Validate.CollectionDoesNotContain(element, nameof(element), collection);
        }

        [Fact]
        internal void CollectionDoesNotContain_WhenCollectionContainsElement_Throws()
        {
            // Arrange
            var element = "the_fifth_element";
            var collection = new List<string> { element };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.CollectionDoesNotContain(element, nameof(element), collection));
        }

        [Fact]
        internal void DictionaryContainsKey_WhenDictionaryDoesContainKey_DoesNothing()
        {
            // Arrange
            var key = "the_key";
            var collection = new Dictionary<string, int> { { key, 1 } };

            // Act
            // Assert
            Validate.DictionaryContainsKey(key, nameof(key), collection);
        }

        [Fact]
        internal void DictionaryContainsKey_WhenDictionaryContainsKey_DoesNothing()
        {
            // Arrange
            var key = "the_key";
            var collection = new Dictionary<string, int> { { key, 0 } };

            // Act
            // Assert
            Validate.DictionaryContainsKey(key, nameof(key), collection);
        }

        [Fact]
        internal void DictionaryContainsKey_WhenDictionaryDoesNotContainKey_Throws()
        {
            // Arrange
            var key = "the_key";
            var collection = new Dictionary<string, int>();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.DictionaryContainsKey(key, nameof(key), collection));
        }

        [Fact]
        internal void DictionaryDoesNotContainKey_WhenDictionaryDoesNotContainsKey_DoesNothing()
        {
            // Arrange
            var key = "the_key";
            var collection = new Dictionary<string, int> { { "another_key", 2 } };

            // Act
            // Assert
            Validate.DictionaryDoesNotContainKey(key, nameof(key), collection);
        }

        [Fact]
        internal void DictionaryDoesNotContainKey_WhenDictionaryContainsKey_Throws()
        {
            // Arrange
            var key = "the_key";
            var collection = new Dictionary<string, int> { { key, 1 } };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.DictionaryDoesNotContainKey(key, nameof(key), collection));
        }

        [Fact]
        internal void EqualTo_ValuesAreEqual_DoesNothing()
        {
            // Arrange
            var object1 = 1;
            var object2 = 1;

            // Act
            // Assert
            Validate.EqualTo(object1, nameof(object1), object2);
        }

        [Fact]
        internal void EqualTo_ObjectsAreEqual_DoesNothing()
        {
            // Arrange
            var object1 = "object";
            var object2 = "object";

            // Act
            // Assert
            Validate.EqualTo(object1, nameof(object1), object2);
        }

        [Fact]
        internal void EqualTo_ValuesAreNotEqual_Throws()
        {
            // Arrange
            var object1 = 1;
            var object2 = 2;

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.EqualTo(object1, nameof(object1), object2));
        }

        [Fact]
        internal void EqualTo_ObjectsAreNotEqual_Throws()
        {
            // Arrange
            var object1 = "object1";
            var object2 = "object2";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.EqualTo(object1, nameof(object1), object2));
        }

        [Fact]
        internal void NotEqualTo_ValuesAreNotEqual_DoesNothing()
        {
            // Arrange
            var object1 = 1;
            var object2 = 2;

            // Act
            // Assert
            Validate.NotEqualTo(object1, nameof(object1), object2);
        }

        [Fact]
        internal void NotEqualTo_ObjectsAreNotEqual_DoesNothing()
        {
            // Arrange
            var object1 = "object1";
            var object2 = "object2";

            // Act
            // Assert
            Validate.NotEqualTo(object1, nameof(object1), object2);
        }

        [Fact]
        internal void NotEqualTo_ValuesAreEqual_Throws()
        {
            // Arrange
            var object1 = 1;
            var object2 = 1;

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.NotEqualTo(object1, nameof(object1), object2));
        }

        [Fact]
        internal void NotEqualTo_ObjectsAreEqual_Throws()
        {
            // Arrange
            var object1 = "object";
            var object2 = "object";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Validate.NotEqualTo(object1, nameof(object1), object2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        internal void Int32NotOutOfRange_VariousInInclusiveRangeValues_DoesNothing(int value)
        {
            // Arrange
            // Act
            // Assert
            Validate.Int32NotOutOfRange(value, nameof(value), 0, 3);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        [InlineData(-1)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousOutOfInclusiveRangeValues_Throws(int value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Int32NotOutOfRange(value, nameof(value), 0, 1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        internal void Int32NotOutOfRange_VariousInLowerExclusiveRangeValues_DoesNothing(int value)
        {
            // Arrange
            // Act
            // Assert
            Validate.Int32NotOutOfRange(value, nameof(value), 0, 3, RangeEndPoints.LowerExclusive);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        [InlineData(0)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousOutOfLowerExclusiveRangeValues_Throws(int value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Int32NotOutOfRange(value, nameof(value), 0, 1, RangeEndPoints.LowerExclusive));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousInUpperExclusiveRangeValues_DoesNothing(int value)
        {
            // Arrange
            // Act
            // Assert
            Validate.Int32NotOutOfRange(value, nameof(value), 0, 3, RangeEndPoints.UpperExclusive);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        [InlineData(-1)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousOutOfUpperExclusiveRangeValues_Throws(int value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Int32NotOutOfRange(value, nameof(value), 0, 2, RangeEndPoints.UpperExclusive));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousInExclusiveRangeValues_DoesNothing(int value)
        {
            // Arrange
            // Act
            // Assert
            Validate.Int32NotOutOfRange(value, nameof(value), 0, 3, RangeEndPoints.Exclusive);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        internal void Int32NotOutOfRange_VariousOutOfExclusiveRangeValues_Throws(int value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Int32NotOutOfRange(value, nameof(value), 0, 1, RangeEndPoints.Exclusive));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.1)]
        [InlineData(1.9)]
        [InlineData(2)]
        internal void DoubleNotOutOfRange_VariousInInclusiveRangeValues_DoesNothing(double value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2);
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        [InlineData(0.99999999999)]
        [InlineData(2.00000000001)]
        internal void DoubleNotOutOfRange_VariousOutOfInclusiveRangeValues_Throws(double value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2));
        }

        [Theory]
        [InlineData(1.1)]
        [InlineData(1.9)]
        [InlineData(2)]
        internal void DoubleNotOutOfRange_VariousInLowerExclusiveRangeValues_DoesNothing(double value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.LowerExclusive);
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        [InlineData(1)]
        [InlineData(2.00000000001)]
        internal void DoubleNotOutOfRange_VariousOutOfLowerExclusiveRangeValues_Throws(double value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.LowerExclusive));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.9)]
        internal void DoubleNotOutOfRange_VariousInUpperExclusiveRangeValues_DoesNothing(double value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.UpperExclusive);
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        [InlineData(0)]
        [InlineData(2)]
        internal void DoubleNotOutOfRange_VariousOutOfUpperExclusiveRangeValues_Throws(double value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.UpperExclusive));
        }

        [Theory]
        [InlineData(1.0000000001)]
        [InlineData(1.9999999999)]
        internal void DoubleNotOutOfRange_VariousInExclusiveRangeValues_DoesNothing(double value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.Exclusive);
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.Epsilon)]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        [InlineData(0.9)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(2.1)]
        internal void DoubleNotOutOfRange_VariousOutOfExclusiveRangeValues_Throws(double value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DoubleNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.Exclusive));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        internal void DoubleNotOutOfBounds_ValueInBounds_DoesNothing(double value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DoubleNotInvalidNumber(value, nameof(value));
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        internal void DoubleNotInvalidNumber_VariousOutOfBoundsValuesAndValueAtBounds_Throws(double value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DoubleNotInvalidNumber(value, nameof(value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.000000000000000000000000000000000001)]
        [InlineData(1.999999999999999999999999999999999999)]
        [InlineData(2)]
        internal void DecimalNotOutOfRange_VariousInInclusiveRangeValues_DoesNothing(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2.1)]
        internal void DecimalNotOutOfRange_VariousOutOfInclusiveRangeValues_Throws(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2));
        }

        [Theory]
        [InlineData(1.1)]
        [InlineData(1.999999999999999999999999999999999999)]
        [InlineData(2)]
        internal void DecimalNotOutOfRange_VariousInLowerExclusiveRangeValues_DoesNothing(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.LowerExclusive);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2.1)]
        internal void DecimalNotOutOfRange_VariousOutOfLowerExclusiveRangeValues_Throws(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.LowerExclusive));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.9)]
        internal void DecimalNotOutOfRange_VariousInUpperExclusiveRangeValues_DoesNothing(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.UpperExclusive);
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(2)]
        internal void DecimalNotOutOfRange_VariousOutOfUpperExclusiveRangeValues_Throws(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.UpperExclusive));
        }

        [Theory]
        [InlineData(1.000000001)]
        [InlineData(1.999999999)]
        internal void DecimalNotOutOfRange_VariousInExclusiveRangeValues_DoesNothing(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.Exclusive);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.9)]
        [InlineData(2)]
        [InlineData(2.1)]
        internal void DecimalNotOutOfRange_VariousOutOfExclusiveRangeValues_Throws(decimal value)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Validate.DecimalNotOutOfRange(value, nameof(value), 1, 2, RangeEndPoints.Exclusive));
        }
    }
}