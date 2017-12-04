// -------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System;
    using System.Diagnostics;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable sealed <see cref="Query{T}"/> <see cref="Result"/> class. A type which wraps
    /// the result of a query.
    /// </summary>
    /// <typeparam name="T">The query type.</typeparam>
    [Immutable]
    public sealed class Query<T> : Result
    {
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Query{T}"/> class.
        /// </summary>
        /// <param name="isFailure">The is failure flag.</param>
        /// <param name="value">The query value (cannot be null if successful).</param>
        /// <param name="error">The query error (cannot be null or white space).</param>
        /// <exception cref="ValidationException"> Throws if the validation fails.</exception>
        private Query(
            bool isFailure, 
            [CanBeNull] T value, 
            string error) 
            : base(isFailure, error)
        {
            if (!isFailure)
            {
                Validate.NotNull(value, nameof(value));
            }

            this.value = value;
        }

        /// <summary>
        /// Gets the value (value cannot be null).
        /// </summary>
        /// <returns>The query value.</returns>
        /// <exception cref="InvalidOperationException">Throws if the value is null.</exception>
        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                return this.IsSuccess 
                    ? this.value
                    : throw new InvalidOperationException("There is no value for failure.");
            }
        }

        /// <summary>
        /// Returns a success <see cref="Query{T}"/> <see cref="Result"/> with the given value.
        /// </summary>
        /// <param name="value">The query value (cannot be null).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> Ok(T value)
        {
            Validate.NotNull(value, nameof(value));

            return new Query<T>(false, value, "None");
        }

        /// <summary>
        /// Returns a success <see cref="Query{T}"/> <see cref="Result"/> with the given value and
        /// message.
        /// </summary>
        /// <param name="value">The query value (cannot be null).</param>
        /// <param name="message">The query message (cannot be null or white space).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> Ok(T value, string message)
        {
            Validate.NotNull(value, nameof(value));
            Validate.NotNull(message, nameof(message));

            return new Query<T>(false, value, message);
        }

        /// <summary>
        /// Returns a failure <see cref="Query{T}"/> <see cref="Result"/> with the given error message.
        /// </summary>
        /// <param name="error">The query error message (cannot be null or white space).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> Fail(string error)
        {
            Validate.NotNull(error, nameof(error));

            return new Query<T>(true, default(T), $"Query Failure ({error}).");
        }
    }
}