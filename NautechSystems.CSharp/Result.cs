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
    using System;
    using System.Diagnostics;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable abstract <see cref="Result"/> class. The base class for all result types.
    /// </summary>
    [Immutable]
    public abstract class Result
    {
        private readonly string error;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isFailure">The is failure boolean flag.</param>
        /// <param name="error">The error string.</param>
        protected Result(bool isFailure, [CanBeNull] string error)
        {
            if (isFailure)
            {
                Validate.NotNull(error, nameof(error));

                this.error = error;
            }

            this.IsFailure = isFailure;
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
        /// Gets the result error.
        /// </summary>
        public string Error
        {
            [DebuggerStepThrough]
            get
            {
                if (this.IsSuccess)
                {
                    throw new InvalidOperationException("There is no error message for success.");
                }

                return this.error;
            }
        }
    }
}