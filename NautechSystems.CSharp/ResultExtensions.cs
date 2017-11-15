// -------------------------------------------------------------------------------------------------
// <copyright file="ResultExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

using NautechSystems.CSharp.Annotations;

namespace NautechSystems.CSharp
{
    using System;

    /// <summary>
    /// The immutable static <see cref="ResultExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class ResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            return result.IsFailure 
                ? Result.Fail<K>(result.Error) 
                : Result.Ok(func(result.Value));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : Result.Ok(func());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            return result.IsFailure 
                ? Result.Fail<K>(result.Error) 
                : func(result.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            return result.IsFailure 
                ? Result.Fail<K>(result.Error) 
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            return result.IsFailure 
                ? Result.Fail(result.Error) 
                : func(result.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            return result.IsFailure 
                ? result 
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return Result.Ok(result.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Result Ensure(this Result result, Func<bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return Result.Fail(result.Error);

            if (!predicate())
                return Result.Fail(errorMessage);

            return Result.Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            return result.IsFailure 
                ? Result.Fail<K>(result.Error) 
                : Result.Ok(func(result.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T> Map<T>(this Result result, Func<T> func)
        {
            return result.IsFailure 
                ? Result.Fail<T>(result.Error) 
                : Result.Ok(func());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static K OnBoth<T, K>(this Result<T> result, Func<Result<T>, K> func)
        {
            return func(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }

            return result;
        }
    }
}
