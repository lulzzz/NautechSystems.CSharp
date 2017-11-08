// -------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NautechSystems.Common.Annotations;
    using NautechSystems.Common.Validation;

    /// <summary>
    /// The immutable static <see cref="CollectionExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns a boolean indication whether the collections count is zero.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>A <see cref="bool"/>.</returns>
        public static bool IsCountZero<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        /// <summary>
        /// Returns the value of the last collection index (if collection empty returns -1).
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>An <see cref="int"/>.</returns>
        public static int LastIndex<T>(this ICollection<T> collection)
        {
            return collection.Count - 1;
        }

        /// <summary>
        /// Returns the element at the given reverse index of a collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="index">The index.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>The element type.</returns>
        public static T GetByReverseIndex<T>(this ICollection<T> collection, int index)
        {
            Validate.Int32NotOutOfRange(index, nameof(index), 0, int.MaxValue);

            return collection.ElementAtOrDefault(collection.LastIndex() - index);
        }

        /// <summary>
        /// Returns the element at the given shifted reverse index of a collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="index">The index.</param>
        /// <param name="shift">The shift.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>The element type.</returns>
        public static T GetByShiftedReverseIndex<T>(this ICollection<T> collection, int index, int shift)
        {
            Validate.Int32NotOutOfRange(index, nameof(index), 0, int.MaxValue);
            Validate.Int32NotOutOfRange(shift, nameof(shift), 0, int.MaxValue);

            return collection.ElementAtOrDefault(collection.LastIndex() - index - shift);
        }

        /// <summary>
        /// Performs the action on each element in the <see cref="IEnumerable{T}"/> source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        /// <typeparam name="T">The type.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}