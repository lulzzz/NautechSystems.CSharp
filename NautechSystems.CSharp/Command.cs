// -------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System;
    using System.Linq;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable sealed <see cref="Command"/> <see cref="Result"/> class. A type which wraps the
    /// result of a command.
    /// </summary>
    [Immutable]
    public sealed class Command : Result
    {
        private static readonly Command OkCommand = new Command(false, null);

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class. 
        /// </summary>
        /// <param name="isFailure">The command is failure boolean flag.</param>
        /// <param name="error">The command error string (can be null).</param>
        private Command(bool isFailure, [CanBeNull] string error)
            : base(isFailure, error)
        {
        }

        /// <summary>
        /// Returns a success <see cref="Command"/> <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Command"/> result</returns>
        public static Command Ok()
        {
            return OkCommand;
        }

        /// <summary>
        /// Returns a failure <see cref="Command"/> <see cref="Result"/>.
        /// </summary>
        /// <param name="error">The command error string.</param>
        /// <returns>A <see cref="Command"/> result</returns>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        public static Command Fail(string error)
        {
            Validate.NotNull(error, nameof(error));

            return new Command(true, error);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="commands"/> 
        /// (if there is no failure returns success.)
        /// </summary>
        /// <param name="commands">The commands array.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        public static Command FirstFailureOrSuccess(params Command[] commands)
        {
            Validate.NotNull(commands, nameof(commands));

            foreach (var result in commands)
            {
                if (result.IsFailure)
                {
                    return Fail(result.Error);
                }
            }

            return Ok();
        }

        /// <summary>
        /// Returns failure which combined from all failures in the <paramref name="commands"/> list.
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="commands">The commands array.</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ArgumentNullException">Throws if the argument is null.</exception>
        /// <exception cref="ArgumentException">Throws if the commands array is empty.</exception>
        public static Command Combine(params Command[] commands)
        {
            Validate.CollectionNotNullOrEmpty(commands, nameof(commands));

            var failedResults = commands
                .Where(x => x.IsFailure)
                .ToList();

            if (!failedResults.Any())
            {
                return Ok();
            }              

            var errorMessage = string.Join("; ", failedResults.Select(x => x.Error).ToArray());

            return Fail(errorMessage);
        }
    }
}