// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Extensions
{
    using System;
    using System.Linq;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

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
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        public static string RemoveAllWhitespace(this string input)
        {
            Validate.NotNull(input, nameof(input));

            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Returns an enumerator of the given type (parsed from the given string).
        /// </summary>
        /// <param name="enumerationString">The enumeration string.</param>
        /// <typeparam name="T">The enumerator type.</typeparam>
        /// <returns>An enumerator type.</returns>
        public static T ToEnum<T>([CanBeNull] this string enumerationString)
        {
            if (string.IsNullOrWhiteSpace(enumerationString))
            {
                return default(T);
            }

            return (T)Enum.Parse(typeof(T), enumerationString);
        }
    }
}