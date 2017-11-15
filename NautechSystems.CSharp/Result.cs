// -------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

using NautechSystems.CSharp.Annotations;
using NautechSystems.CSharp.Validation;

namespace NautechSystems.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.Serialization;

    internal sealed class ResultCommonLogic
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string error;

        public string Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error message for success.");
                }                   

                return error;
            }
        }

        [DebuggerStepThrough]
        public ResultCommonLogic(bool isFailure, string error)
        {
            if (isFailure)
            {
                Validate.NotNull(error, nameof(error));

                this.error = error;
            }

            IsFailure = isFailure;           
        }
    }

    /// <summary>
    /// The immutable <see cref="Result"/> structure.
    /// </summary>
    [Immutable]
    public struct Result : ISerializable
    {
        private static readonly Result OkResult = new Result(false, null);

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            oInfo.AddValue(nameof(IsFailure), IsFailure);
            oInfo.AddValue(nameof(IsSuccess), IsSuccess);

            if (IsFailure)
            {
                oInfo.AddValue(nameof(Error), Error);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic logic;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFailure => this.logic.IsFailure;

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess => this.logic.IsSuccess;

        /// <summary>
        /// 
        /// </summary>
        public string Error => this.logic.Error;

        [DebuggerStepThrough]
        private Result(bool isFailure, string error)
        {
            logic = new ResultCommonLogic(isFailure, error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
       
        public static Result Ok()
        {
            return OkResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result Fail(string error)
        {
            return new Result(true, error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result<T> Ok<T>(T value)
        {
            Validate.NotNull(value, nameof(value));

            return new Result<T>(false, value, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(true, default(T), error);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="results"/>. If there is no failure returns success.
        /// </summary>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result FirstFailureOrSuccess(params Result[] results)
        {
            Validate.NotNull(results, nameof(results));

            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    return Fail(result.Error);
                }               
            }

            return Ok();
        }

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="results"/> list. Error messages are separated by <paramref name="errorMessagesSeparator"/>. 
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="errorMessagesSeparator">Separator for error messages.</param>
        /// <param name="results">List of results.</param>
        [DebuggerStepThrough]
        public static Result Combine(string errorMessagesSeparator, params Result[] results)
        {
            Validate.NotNull(results, nameof(results));

            var failedResults = results.Where(x => x.IsFailure).ToList();

            if (!failedResults.Any())
                return Ok();

            var errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.Error).ToArray());

            return Fail(errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result Combine(params Result[] results)
        {
            Validate.NotNull(results, nameof(results));

            return Combine(", ", results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result Combine<T>(params Result<T>[] results)
        {
            Validate.NotNull(results, nameof(results));

            return Combine(", ", results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorMessagesSeparator"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Result Combine<T>(string errorMessagesSeparator, params Result<T>[] results)
        {
            Validate.NotNull(results, nameof(results));

            var untyped = results.Select(result => (Result)result).ToArray();
            return Combine(errorMessagesSeparator, untyped);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Result<T> : ISerializable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultCommonLogic logic;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFailure => logic.IsFailure;

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess => logic.IsSuccess;

        /// <summary>
        /// 
        /// </summary>
        public string Error => logic.Error;

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            Validate.NotNull(oInfo, nameof(oInfo));
            Validate.NotNull(oContext, nameof(oContext));

            oInfo.AddValue(nameof(IsFailure), IsFailure);
            oInfo.AddValue(nameof(IsSuccess), IsSuccess);
            if (IsFailure)
            {
                oInfo.AddValue(nameof(Error), Error);
            }
            else
            {
                oInfo.AddValue(nameof(Value), Value);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T value;

        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("There is no value for failure.");

                return value;
            }
        }

        [DebuggerStepThrough]
        internal Result(bool isFailure, T value, string error)
        {
            if (!isFailure)
            {
                Validate.NotNull(value, nameof(value));
            }

            logic = new ResultCommonLogic(isFailure, error);

            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator Result(Result<T> result)
        {
            Validate.NotNull(result, nameof(result));

            return result.IsSuccess 
                ? Result.Ok() 
                : Result.Fail(result.Error);
        }
    }
}
