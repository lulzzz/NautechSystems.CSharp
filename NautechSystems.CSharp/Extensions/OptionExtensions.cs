// -------------------------------------------------------------------------------------------------
// <copyright file="OptionExtensions.cs" company="Nautech Systems Pty Ltd.">
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
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable static <see cref="OptionExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class OptionExtensions
    {
        /// <summary>
        /// Converts an <see cref="Option{T}"/> to a <see cref="Query{T}"/> result.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A <see cref="Query{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<T> ToResult<T>(this Option<T> option, string errorMessage)
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(errorMessage, nameof(errorMessage));

            return option.HasNoValue 
                ? Query<T>.Fail(errorMessage) 
                : Query<T>.Ok(option.Value);
        }

        /// <summary>
        /// Unwraps the <see cref="Option{T}"/> returning the given default value if there is no value.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="defaultValue">The default Value.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A type.</returns>
        /// <exception cref="ArgumentNullException">Throws if the option argument is null.</exception>
        public static T Unwrap<T>(this Option<T> option, [CanBeNull] T defaultValue = default(T))
        {
            Validate.NotNull(option, nameof(option));
            
            return option.Unwrap(x => x, defaultValue);
        }

        /// <summary>
        /// Unwraps the <see cref="Option{T}"/> returning the given default value if there is no value.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <typeparam name="K">The key type.</typeparam>
        /// <returns>A type.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static K Unwrap<T, K>(
            this Option<T> option, 
            Func<T, K> selector, 
            [CanBeNull] K defaultValue = default(K))
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(selector, nameof(selector));

            return option.HasValue 
                ? selector(option.Value) 
                : defaultValue;
        }

        /// <summary>
        /// Where the <see cref="Option{T}"/> has no value then returns the default value for the type. 
        /// Otherwise evaluates the given predicate and returns the <see cref="Option{T}"/> value, 
        /// or the default value for the type.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="predicate">The predicate.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(predicate, nameof(predicate));

            if (option.HasNoValue)
            {
                return default(T);
            }

            return predicate(option.Value) 
                ? option 
                : default(T);
        }

        /// <summary>
        /// Selects the default value of the selector type if the <see cref="Option{K}"/> has no value,
        /// otherwise returns an <see cref="Option{K}"/>.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="selector">The selector.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <typeparam name="K">The key type.</typeparam>
        /// <returns>An <see cref="Option{K}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Option<K> Select<T, K>(this Option<T> option, Func<T, K> selector)
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(selector, nameof(selector));

            return option.HasNoValue 
                ? default(K) 
                : selector(option.Value);
        }

        /// <summary>
        /// Selects ?
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="selector">The selector.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <typeparam name="K">The option type.</typeparam>
        /// <returns>An <see cref="Option{K}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Option<K> Select<T, K>(this Option<T> option, Func<T, Option<K>> selector)
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(selector, nameof(selector));

            return option.HasNoValue 
                ? default(K) 
                : selector(option.Value);
        }

        /// <summary>
        /// Executes the given <see cref="Action{T}"/> where the <see cref="Option{T}"/> has a value.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="action">The action.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static void Execute<T>(this Option<T> option, Action<T> action)
        {
            Validate.NotNull(option, nameof(option));
            Validate.NotNull(action, nameof(action));

            if (option.HasNoValue)
            {
                return;
            }              

            action(option.Value);
        }
    }
}