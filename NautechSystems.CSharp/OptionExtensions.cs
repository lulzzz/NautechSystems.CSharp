// -------------------------------------------------------------------------------------------------
// <copyright file="MaybeExtensions.cs" company="Nautech Systems Pty Ltd.">
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
    /// The immutable static <see cref="OptionExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class OptionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Query<T> ToResult<T>(this Option<T> option, string errorMessage)
        {
            return option.HasNoValue 
                ? Query<T>.Fail(errorMessage) 
                : Query<T>.Ok(option.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Unwrap<T>(this Option<T> option, T defaultValue = default(T))
        {
            return option.Unwrap(x => x, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static K Unwrap<T, K>(this Option<T> option, Func<T, K> selector, K defaultValue = default(K))
        {
            return option.HasValue 
                ? selector(option.Value) 
                : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
        {
            if (option.HasNoValue)
            {
                return default(T);
            }

            return predicate(option.Value) 
                ? option 
                : default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Option<K> Select<T, K>(this Option<T> option, Func<T, K> selector)
        {
            return option.HasNoValue 
                ? default(K) 
                : selector(option.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="option"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Option<K> Select<T, K>(this Option<T> option, Func<T, Option<K>> selector)
        {
            return option.HasNoValue 
                ? default(K) 
                : selector(option.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="action"></param>
        public static void Execute<T>(this Option<T> option, Action<T> action)
        {
            if (option.HasNoValue)
            {
                return;
            }              

            action(option.Value);
        }
    }
}