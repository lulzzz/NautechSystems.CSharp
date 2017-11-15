// -------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Nautech Systems Pty Ltd.">
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
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable abstract <see cref="Result"/> class.
    /// </summary>
    [Immutable]
    public abstract class Result
    {
        /// <summary>
        /// Returns a result indicating whether the result is a failure.
        /// </summary>
        public bool IsFailure { get; }

        /// <summary>
        /// Returns a result indicating whether the result is a success.
        /// </summary>
        public bool IsSuccess => !IsFailure;

        private readonly string error;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Instantiates a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isFailure">The is failure boolean.</param>
        /// <param name="error">The error string.</param>
        protected Result(bool isFailure, string error)
        {
            if (isFailure)
            {
                Validate.NotNull(error, nameof(error));

                this.error = error;
            }

            IsFailure = isFailure;
        }
    }
}