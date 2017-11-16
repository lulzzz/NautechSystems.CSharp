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
        /// Initializes a new instance of the <see cref="Query{T}"/> class.
        /// </summary>
        /// <param name="isFailure">The is failure flag.</param>
        /// <param name="value">The value.</param>
        /// <param name="error">The error.</param>
        private Query(
            bool isFailure, 
            [CanBeNull] T value, 
            [CanBeNull] string error) 
            : base(isFailure, error)
        {
            if (!isFailure)
            {
                Validate.NotNull(value, nameof(value));
            }

            this.value = value;
        }

        /// <summary>
        /// Gets the result of the query.
        /// </summary>
        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (!this.IsSuccess)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return this.value;
            }
        }

        /// <summary>
        /// Returns a success <see cref="Query{T}"/> <see cref="Result"/> with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Query{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        public static Query<T> Ok(T value)
        {
            Validate.NotNull(value, nameof(value));

            return new Query<T>(false, value, null);
        }

        /// <summary>
        /// Returns a failure <see cref="Query{T}"/> <see cref="Result"/> with the given error message.
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <returns>A <see cref="Query{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        public static Query<T> Fail(string error)
        {
            Validate.NotNull(error, nameof(error));

            return new Query<T>(true, default(T), error);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Validate.NotNull(info, nameof(info));
            Validate.NotNull(context, nameof(context));

            info.AddValue(nameof(this.IsFailure), this.IsFailure);
            info.AddValue(nameof(this.IsSuccess), this.IsSuccess);

            if (this.IsFailure)
            {
                info.AddValue(nameof(this.Error), this.Error);
            }
            else
            {
                info.AddValue(nameof(this.Value), this.Value);
            }
        }
    }
}