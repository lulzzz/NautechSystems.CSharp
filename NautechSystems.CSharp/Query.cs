// -------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable sealed <see cref="Query{T}"/> <see cref="Result"/> class.
    /// </summary>
    /// <typeparam name="T">The query type.</typeparam>
    [Immutable]
    public sealed class Query<T> : Result, ISerializable
    {
        private readonly T value;

        /// <summary>
        /// The result of the query.
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFailure"></param>
        /// <param name="value"></param>
        /// <param name="error"></param>
        private Query(bool isFailure, T value, string error) 
            : base(isFailure, error)
        {
            if (!isFailure)
            {
                Validate.NotNull(value, nameof(value));
            }

            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Query<T> Ok(T value)
        {
            Validate.NotNull(value, nameof(value));

            return new Query<T>(false, value, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Query<T> Fail(string error)
        {
            return new Query<T>(true, default(T), error);
        }

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
    }
}