// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2015-2017 Nautech Systems Pty Ltd. All rights reserved.
//   http://www.nautechsystems.net
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Extensions
{
    using System;
    using System.Linq;

    using NautechSystems.Common.Annotations;

    /// <summary>
    /// The immutable static <see cref="StringExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all whitespace from this string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public static string RemoveAllWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Returns an enum of the given type (parsed from the given string).
        /// </summary>
        /// <param name="enumerationString">The enumeration string.</param>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>An enum type {T}.</returns>
        public static T ToEnum<T>(this string enumerationString)
        {
            if (string.IsNullOrWhiteSpace(enumerationString))
            {
                return default(T);
            }

            return (T)Enum.Parse(typeof(T), enumerationString);
        }
    }
}