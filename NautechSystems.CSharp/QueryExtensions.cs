// -------------------------------------------------------------------------------------------------
// <copyright file="QueryExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System;
    using NautechSystems.CSharp.Annotations;

    /// <summary>
    /// The immutable static <see cref="QueryExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class QueryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<T, K> func)
        {
            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : Query<K>.Ok(func(query.Value));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<T> OnSuccess<T>(this Command command, Func<T> func)
        {
            return command.IsFailure 
                ? Query<T>.Fail(command.Error) 
                : Query<T>.Ok(func());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<T, Query<K>> func)
        {
            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : func(query.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<T> OnSuccess<T>(this Command command, Func<Query<T>> func)
        {
            return command.IsFailure 
                ? Query<T>.Fail(command.Error) 
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<K> OnSuccess<T, K>(this Query<T> query, Func<Query<K>> func)
        {
            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Query<T> OnSuccess<T>(this Query<T> query, Action<T> action)
        {
            if (query.IsSuccess)
            {
                action(query.Value);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Query<T> OnFailure<T>(this Query<T> query, Action action)
        {
            if (query.IsFailure)
            {
                action();
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Query<T> OnFailure<T>(this Query<T> query, Action<string> action)
        {
            if (query.IsFailure)
            {
                action(query.Error);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static K OnBoth<T, K>(this Query<T> query, Func<Query<T>, K> func)
        {
            return func(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Query<T> Ensure<T>(this Query<T> query, Func<T, bool> predicate, string errorMessage)
        {
            if (query.IsFailure)
                return Query<T>.Fail(query.Error);

            if (!predicate(query.Value))
                return Query<T>.Fail(errorMessage);

            return Query<T>.Ok(query.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<K> Map<T, K>(this Query<T> query, Func<T, K> func)
        {
            return query.IsFailure 
                ? Query<K>.Fail(query.Error) 
                : Query<K>.Ok(func(query.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Query<T> Map<T>(this Command command, Func<T> func)
        {
            return command.IsFailure 
                ? Query<T>.Fail(command.Error) 
                : Query<T>.Ok(func());
        }
    }
}