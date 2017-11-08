// -------------------------------------------------------------------------------------------------
// <copyright file="Validate.cs" company="Nautech Systems Pty Ltd.">
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
    /// The static <see cref="Validate"/> class.
    /// </summary>
    public static class Validate
    {
        private const string ExMessage = "Validation Failed";

        /// <summary>
        /// The validation passes if the predicate is true.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void True(bool predicate, string paramName)
        {
            if (!predicate)
            {
                throw new ArgumentException(
                    $"{ExMessage} (The predicate based on {paramName} is false).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the condition is false, or both the condition and predicate are true.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void TrueIf(bool condition, bool predicate, string paramName)
        {
            if (condition && !predicate)
            {
                throw new ArgumentException(
                    $"{ExMessage} (The conditional predicate based on {paramName} is false).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the argument is not null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">The arguments type.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void NotNull<T>(T argument, string paramName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(
                    paramName, $"{ExMessage} (The {paramName} argument is null).");
            }
        }

        /// <summary>
        /// The validation passes if the <see cref="string"/> argument is not null.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void NotNull(string argument, string paramName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(
                    paramName, $"{ExMessage} (The {paramName} string argument is null or white space).");
            }
        }

        /// <summary>
        /// The validation passes if the <see cref="ICollection{T}"/> is not null, or empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">he type of collection.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void CollectionNotNullOrEmpty<T>(ICollection<T> collection, string paramName)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(
                    paramName, $"{ExMessage} (The {paramName} collection is null).");
            }

            if (collection.Count == 0)
            {
                throw new ArgumentException(
                    $"{ExMessage} (The {paramName} collection is empty).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the <see cref="ICollection{T}"/> is not null, and is empty (count zero).
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if validation fails.</exception>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void CollectionEmpty<T>(ICollection<T> collection, string paramName)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(
                    paramName, $"{ExMessage} (The {paramName} collection is null).");
            }

            if (collection.Count != 0)
            {
                throw new ArgumentException(
                    $"{ExMessage} (The {paramName} collection is not empty).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the collection contains the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void CollectionContains<T>(T element, string paramName, ICollection<T> collection)
        {
            if (!collection.Contains(element))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The collection does not contain the {paramName} element).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the collection does not contain the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void CollectionDoesNotContain<T>(T element, string paramName, ICollection<T> collection)
        {
            if (collection.Contains(element))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The collection already contains the {paramName} element).", paramName);
            }
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
        [DebuggerStepThrough]
        public static void DictionaryContainsKey<T1, T2>(T1 key, string paramName, IDictionary<T1, T2> collection)
        {
            if (!collection.ContainsKey(key))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The collection does not contain the {paramName} element).", paramName);
            }
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
        [DebuggerStepThrough]
        public static void DictionaryDoesNotContainKey<T1, T2>(T1 key, string paramName, IDictionary<T1, T2> collection)
        {
            if (collection.ContainsKey(key))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The collection already contains the {paramName} element).", paramName);
            }
        }

        /// <summary>
        /// The validation passes if the object is not equal to the other object.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="objNotToEqual">The other object not to equal.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void NotEqualTo(object obj, string paramName, object objNotToEqual)
        {
            if (obj.Equals(objNotToEqual))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The {paramName} should not be equal to {objNotToEqual}. Value = {obj}).", paramName);
            }
        }

        /// <summary>
        /// The condition passes if the object is equal to the other object.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="objToEqual">The other object to be equal to.</param>
        /// <exception cref="ArgumentException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void EqualTo(object obj, string paramName, object objToEqual)
        {
            if (!obj.Equals(objToEqual))
            {
                throw new ArgumentException(
                    $"{ExMessage} (The {paramName} should be equal to {objToEqual}. Value = {obj}).", paramName);
            }
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
        [DebuggerStepThrough]
        public static void Int32NotOutOfRange(
            int value,
            string paramName,
            int lowerBound,
            int upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
            switch (endPoints)
            {
                case RangeEndPoints.Inclusive:
                    if (value < lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.LowerExclusive:
                    if (value <= lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.UpperExclusive:
                    if (value < lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}). Value = {value}).");
                    }
                    break;

                case RangeEndPoints.Exclusive:
                    if (value <= lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}). Value = {value}).");
                    }
                    break;
            }
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
        [DebuggerStepThrough]
        public static void DoubleNotOutOfRange(
            double value,
            string paramName,
            double lowerBound,
            double upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
            if (value.IsInvalidNumber())
            {
                throw new ArgumentOutOfRangeException(
                    paramName, $"{ExMessage} (The {paramName} value is an invalid number).");
            }

            switch (endPoints)
            {
                case RangeEndPoints.Inclusive:
                    if (value < lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.LowerExclusive:
                    if (value <= lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.UpperExclusive:
                    if (value < lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}). Value = {value}.");
                    }
                    break;

                case RangeEndPoints.Exclusive:
                    if (value <= lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}). Value = {value}).");
                    }
                    break;
            }
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
        [DebuggerStepThrough]
        public static void DecimalNotOutOfRange(
            decimal value,
            string paramName,
            decimal lowerBound,
            decimal upperBound,
            RangeEndPoints endPoints = RangeEndPoints.Inclusive)
        {
            switch (endPoints)
            {
                case RangeEndPoints.Inclusive:
                    if (value < lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.LowerExclusive:
                    if (value <= lowerBound || value > upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}]. Value = {value}).");
                    }
                    break;

                case RangeEndPoints.UpperExclusive:
                    if (value < lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range [{lowerBound}, {upperBound}). Value = {value}.");
                    }
                    break;

                case RangeEndPoints.Exclusive:
                    if (value <= lowerBound || value >= upperBound)
                    {
                        throw new ArgumentOutOfRangeException(
                            paramName, $"{ExMessage} (The {paramName} is not within the specified range ({lowerBound}, {upperBound}). Value = {value}).");
                    }
                    break;
            }
        }

        /// <summary>
        /// The validation passes if the value is not an invalid number.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if validation fails.</exception>
        [DebuggerStepThrough]
        public static void DoubleNotInvalidNumber(double value, string paramName)
        {
            if (value.IsInvalidNumber())
            {
                throw new ArgumentOutOfRangeException(
                    paramName, $"{ExMessage} (The {paramName} is an invalid number).");
            }
        }

        /// <summary>
        /// Returns a value indicating whether the given <see cref="double"/> is a valid number.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        private static bool IsInvalidNumber(this double value)
        {
            return double.IsNaN(value)
                || double.IsNegativeInfinity(value)
                || double.IsPositiveInfinity(value);
        }
    }
}