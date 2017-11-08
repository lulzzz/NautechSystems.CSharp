// -------------------------------------------------------------------------------------------------
// <copyright file="Debug.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The static <see cref="Debug"/> class.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// The validation passes if the predicate is true.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void True(bool predicate, string paramName)
        {
#if DEBUG
            Validate.True(predicate, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the condition is false, or both the condition and predicate are true.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void TrueIf(bool condition, bool predicate, string paramName)
        {
#if DEBUG
            Validate.TrueIf(condition, predicate, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the argument is not null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">The arguments type.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void NotNull<T>(T argument, string paramName)
        {
#if DEBUG
            Validate.NotNull(argument, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the <see cref="string"/> argument is not null.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void NotNull(string argument, string paramName)
        {
#if DEBUG
            Validate.NotNull(argument, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the <see cref="ICollection{T}"/> is not null, or empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">he type of collection.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void CollectionNotNullOrEmpty<T>(ICollection<T> collection, string paramName)
        {
#if DEBUG
            Validate.CollectionNotNullOrEmpty(collection, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the <see cref="ICollection{T}"/> is not null, and is empty (count zero).
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void CollectionEmpty<T>(ICollection<T> collection, string paramName)
        {
#if DEBUG
            Validate.CollectionEmpty(collection, paramName);
#endif
        }

        /// <summary>
        /// The validation passes if the collection contains the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void CollectionContains<T>(T element, string paramName, ICollection<T> collection)
        {
#if DEBUG
            Validate.CollectionContains(element, paramName, collection);
#endif
        }

        /// <summary>
        /// The validation passes if the collection does not contain the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void CollectionDoesNotContain<T>(T element, string paramName, ICollection<T> collection)
        {
#if DEBUG
            Validate.CollectionDoesNotContain(element, paramName, collection);
#endif
        }

        /// <summary>
        /// The validation passes if the dictionary contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T1">The type of the keys.</typeparam>
        /// <typeparam name="T2">The type of the values</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void DictionaryContainsKey<T1, T2>(T1 key, string paramName, IDictionary<T1, T2> collection)
        {
#if DEBUG
            Validate.DictionaryContainsKey(key, paramName, collection);
#endif
        }

        /// <summary>
        /// The validation passes if the dictionary does not contain the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T1">The type of the keys.</typeparam>
        /// <typeparam name="T2">The type of the values.</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void DictionaryDoesNotContainKey<T1, T2>(T1 key, string paramName, IDictionary<T1, T2> collection)
        {
#if DEBUG
            Validate.DictionaryDoesNotContainKey(key, paramName, collection);
#endif
        }

        /// <summary>
        /// The validation passes if the object is not equal to the other object.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="objNotToEqual">The other object not to equal.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void NotEqualTo(object obj, string paramName, object objNotToEqual)
        {
#if DEBUG
            Validate.NotEqualTo(obj, paramName, objNotToEqual);
#endif
        }

        /// <summary>
        /// The condition passes if the object is equal to the other object.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="objToEqual">The other object to be equal to.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void EqualTo(object obj, string paramName, object objToEqual)
        {
#if DEBUG
            Validate.EqualTo(obj, paramName, objToEqual);
#endif
        }

        /// <summary>
        /// The validation passes if the value is not out of the specified range.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="lowerBound">The range lower bound.</param>
        /// <param name="upperBound">The range upper bound.</param>
        /// <param name="endPoints">The range end points literal.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void Int32NotOutOfRange(
            int value,
            string paramName,
            int lowerBound,
            int upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
#if DEBUG
            Validate.Int32NotOutOfRange(value, paramName, lowerBound, upperBound, endPoints);
#endif
        }

        /// <summary>
        /// The validation passes if the value is not out of the specified range.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="lowerBound">The range lower bound.</param>
        /// <param name="upperBound">The range upper bound.</param>
        /// <param name="endPoints">The range end points literal.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void DoubleNotOutOfRange(
            double value,
            string paramName,
            double lowerBound,
            double upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
#if DEBUG
            Validate.DoubleNotOutOfRange(value, paramName, lowerBound, upperBound, endPoints);
#endif
        }

        /// <summary>
        /// The validation passes if the value is not out of the specified range.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="lowerBound">The range lower bound.</param>
        /// <param name="upperBound">The range upper bound.</param>
        /// <param name="endPoints">The range end points literal.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void DecimalNotOutOfRange(
            decimal value,
            string paramName,
            decimal lowerBound,
            decimal upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
#if DEBUG
            Validate.DecimalNotOutOfRange(value, paramName, lowerBound, upperBound, endPoints);
#endif
        }

        /// <summary>
        /// The validation passes if the value is not an invalid number [or throws].
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if validation fails.</exception>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void DoubleNotInvalidNumber(double value, string paramName)
        {
#if DEBUG
            Validate.DoubleNotInvalidNumber(value, paramName);
#endif
        }
    }
}