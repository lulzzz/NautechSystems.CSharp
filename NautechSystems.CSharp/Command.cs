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
    using System.Collections.Generic;
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
        private static readonly Command OkCommand = new Command(false, "None");

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class. 
        /// </summary>
        /// <param name="isFailure">The command is failure boolean flag.</param>
        /// <param name="message">The command message string.</param>
        private Command(bool isFailure, string message)
            : base(isFailure, message)
        {
        }

        /// <summary>
        /// Returns a success <see cref="Command"/> <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Command"/> result.</returns>
        public static Command Ok()
        {
            return OkCommand;
        }

        /// <summary>
        /// Returns a success <see cref="Command"/> <see cref="Result"/> with the given message.
        /// </summary>
        /// <param name="message">The command result message (cannot be null or white space).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command Ok(string message)
        {
            Validate.NotNull(message, nameof(message));

            return new Command(false, message);
        }

        /// <summary>
        /// Returns a failure <see cref="Command"/> <see cref="Result"/>.
        /// </summary>
        /// <param name="errorMessage">The command result error message (cannot be null or white
        /// space).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command Fail(string errorMessage)
        {
            Validate.NotNull(errorMessage, nameof(errorMessage));

            return new Command(true, $"Command Failure ({errorMessage}).");
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="commands"/> 
        /// (if there is no failure returns success.)
        /// </summary>
        /// <param name="commands">The commands array (cannot be null or empty).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command FirstFailureOrSuccess(params Command[] commands)
        {
            Validate.CollectionNotNullOrEmpty(commands, nameof(commands));

            return commands.FirstOrDefault(c => c.IsFailure) ?? Ok();
        }

        /// <summary>
        /// Returns combined result from all failures in the <paramref name="commands"/> list.
        /// If there is no failure then returns success.
        /// </summary>
        /// <param name="commands">The commands array (cannot be null or empty).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command Combine(params Command[] commands)
        {
            Validate.CollectionNotNullOrEmpty(commands, nameof(commands));

            var failedResults = commands
                .Where(x => x.IsFailure)
                .ToList();

            return failedResults.Any() 
                ? Fail(CombineErrorMessages(failedResults)) 
                : Ok();
        }

        private static string CombineErrorMessages(IList<Command> failedResults)
        {
            return string.Join("; ", failedResults.Select(x => x.Message.Split('(', ')')[1]));
        }
    }
}