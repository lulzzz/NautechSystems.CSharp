// -------------------------------------------------------------------------------------------------
// <copyright file="QueryExtensions.cs" company="Nautech Systems Pty Ltd.">
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
    /// The immutable static <see cref="QueryExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class QueryExtensions
    {
        /// <summary>
        /// On success returns a query with the function value, otherwise returns a failure query.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <typeparam name="K">The K type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="func">The function.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<T, K> func)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(func, nameof(func));

            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : Query<K>.Ok(func(query.Value));
        }

        /// <summary>
        /// On success returns the function with the query value, otherwise returns a failure query.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <typeparam name="K">The K type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="func">The function.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<T, Query<K>> func)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(func, nameof(func));

            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : func(query.Value);
        }

        /// <summary>
        /// On success returns the function, otherwise returns a failure query.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <typeparam name="K">The K type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="func">The function.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<Query<K>> func)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(func, nameof(func));

            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : func();
        }

        /// <summary>
        /// On success performs the given action, then returns the query.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<T> OnSuccess<T>(this Query<T> query, Action<T> action)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(action, nameof(action));

            if (query.IsSuccess)
            {
                action(query.Value);
            }

            return query;
        }

        /// <summary>
        /// On failure performs the given action, then returns the query.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<T> OnFailure<T>(this Query<T> query, Action action)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(action, nameof(action));

            if (query.IsFailure)
            {
                action();
            }

            return query;
        }

        /// <summary>
        /// On failure performs the given action, then returns the query.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<T> OnFailure<T>(this Query<T> query, Action<string> action)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(action, nameof(action));

            if (query.IsFailure)
            {
                action(query.Error);
            }

            return query;
        }

        /// <summary>
        /// On success or failure returns the function.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <typeparam name="K">The K type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="func">The function.</param>
        /// <returns>A type.</returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static K OnBoth<T, K>(this Query<T> query, Func<Query<T>, K> func)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(func, nameof(func));

            return func(query);
        }

        /// <summary>
        /// Evaluates the query and predicate.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if any argument is null.</exception>
        public static Query<T> Ensure<T>(this Query<T> query, Func<T, bool> predicate, string errorMessage)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(errorMessage, nameof(errorMessage));

            if (query.IsFailure)
            {
                return Query<T>.Fail(query.Error);
            }


            if (!predicate(query.Value))
            {
                return Query<T>.Fail(errorMessage);
            }                

            return Query<T>.Ok(query.Value);
        }

        /// <summary>
        /// Maps the query result to successful query.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <typeparam name="K">The K type.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="func">The function.</param>
        /// <returns>A <see cref="Query{T}"/></returns>
        /// <exception cref="ArgumentNullException">Throws if either argument is null.</exception>
        public static Query<K> Map<T, K>(this Query<T> query, Func<T, K> func)
        {
            Validate.NotNull(query, nameof(query));
            Validate.NotNull(func, nameof(func));

            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : Query<K>.Ok(func(query.Value));
        }
    }
}