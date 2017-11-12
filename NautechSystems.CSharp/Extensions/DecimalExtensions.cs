// -------------------------------------------------------------------------------------------------
// <copyright file="DecimalExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Extensions
{
    using System;
    using NautechSystems.CSharp.Annotations;

    /// <summary>
    /// The immutable static <see cref="DecimalExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class DecimalExtensions
    {
        /// <summary>
        /// Returns the number of decimal places of the given decimal number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns> An <see cref="int"/>.</returns>
        public static int DecimalPlaces(this decimal value)
        {
            return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
        }

        /// <summary>
        /// Returns the decimal tick size from an integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="decimal"/>.</returns>
        public static decimal ToTickSize(this int value)
        {
            decimal divisor = 1;

            for (int i = 0; i < value; i++)
            {
                divisor = divisor * 10;
            }

            return 1 / divisor;
        }
    }
}