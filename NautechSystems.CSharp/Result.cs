// -------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable abstract <see cref="Result"/> class. The base class for all result types.
    /// </summary>
    [Immutable]
    public abstract class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isFailure">The is failure boolean flag.</param>
        /// <param name="message">The message string.</param>
        protected Result(bool isFailure, string message)
        {
            Debug.NotNull(message, nameof(message));

            this.IsFailure = isFailure;
            this.Message = message;
        }

        /// <summary>
        /// Gets a value indicating whether the result is a failure.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Gets a value indicating whether the result is a success.
        /// </summary>
        public bool IsSuccess => !this.IsFailure;

        /// <summary>
        /// Gets the result message.
        /// </summary>
        public string Message { get; }
    }
}