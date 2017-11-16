// -------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable static <see cref="CollectionExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns the value of the last collection index.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>An <see cref="int"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if collection is null.</exception>
        /// <exception cref="ArgumentException">Throws if collection is empty.</exception>
        public static int LastIndex<T>(this ICollection<T> collection)
        {
            Validate.CollectionNotNullOrEmpty(collection, nameof(collection));

            return collection.Count - 1;
        }

        /// <summary>
        /// Returns the element at the given reverse index of a collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="index">The index.</param>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <returns>The element type.</returns>
        /// <exception cref="ArgumentNullException">Throws if collection null.</exception>
        /// <exception cref="ArgumentException">Throws if collection empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index out of range.</exception>
        public static T GetByReverseIndex<T>(this ICollection<T> collection, int index)
        {
            Validate.CollectionNotNullOrEmpty(collection, nameof(collection));
            Validate.Int32NotOutOfRange(index, nameof(index), 0, collection.LastIndex());

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
        /// <exception cref="ArgumentNullException">Throws if collection null.</exception>
        /// <exception cref="ArgumentException">Throws if collection empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if shift out of range.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if index + shift is greater than 
        /// collection count.</exception>
        public static T GetByShiftedReverseIndex<T>(this ICollection<T> collection, int index, int shift)
        {
            Validate.CollectionNotNullOrEmpty(collection, nameof(collection));
            Validate.Int32NotOutOfRange(index, nameof(index), 0, collection.LastIndex());
            Validate.Int32NotOutOfRange(shift, nameof(shift), 0, collection.LastIndex());
            Validate.Int32NotOutOfRange(index + shift, nameof(index) + nameof(shift), 0, collection.LastIndex());

            return collection.ElementAt(collection.LastIndex() - index - shift);
        }

        /// <summary>
        /// Performs the action on each element in the <see cref="IEnumerable{T}"/> source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Validate.NotNull(source, nameof(source));
            Validate.NotNull(action, nameof(action));

            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}