// -------------------------------------------------------------------------------------------------
// <copyright file="CommandExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System;
    using NautechSystems.CSharp.Annotations;

    /// <summary>
    /// The immutable static <see cref="CommandExtensions"/> class.
    /// </summary>
    [Immutable]
    public static class CommandExtensions
    {
        /// <summary>
        /// Invokes the given action for a successful <see cref="Command"/>, 
        /// then returns the <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The command to be evaluated.</param>
        /// <param name="action">The action to be invoked.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        public static Command OnSuccess(this Command command, Action action)
        {
            if (command.IsSuccess)
            {
                action();
            }

            return command;
        }

        /// <summary>
        /// Calls the given function for a successful <see cref="Command"/>, 
        /// then returns the <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The command to be evaluated.</param>
        /// <param name="func">The function to be called.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        public static Command OnSuccess(this Command command, Func<Command> func)
        {
            return command.IsFailure
                ? command
                : func();
        }

        /// <summary>
        /// Invokes the given action for a failed <see cref="Command"/>, 
        /// then returns the <see cref="Command"/>. 
        /// </summary>
        /// <param name="command">The command to be evaluated.</param>
        /// <param name="action">The action to be invoked.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        public static Command OnFailure(this Command command, Action action)
        {
            if (command.IsFailure)
            {
                action();
            }

            return command;
        }

        /// <summary>
        /// Invokes the given action string for a failed <see cref="Command"/>,
        /// then returns the <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The command to be evaluated.</param>
        /// <param name="action">The action string to be invoked.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        public static Command OnFailure(this Command command, Action<string> action)
        {
            if (command.IsFailure)
            {
                action(command.Error);
            }

            return command;
        }

        /// <summary>
        /// Calls the given function on either a success or failure command result.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A type.</returns>
        public static T OnBoth<T>(this Command command, Func<Command, T> func)
        {
            return func(command);
        }

        /// <summary>
        /// Ensures a failed <see cref="Command"/> result is returned on error, otherwise returns ok.
        /// </summary>
        /// <param name="command">The command to evaluate.</param>
        /// <param name="predicate">The predicate to evaluate.</param>
        /// <param name="errorMessage">The error message string.</param>
        /// <returns>A <see cref="Command"/>.</returns>
        public static Command Ensure(this Command command, Func<bool> predicate, string errorMessage)
        {
            if (command.IsFailure)
            {
                return Command.Fail(command.Error);
            }

            if (!predicate())
            {
                return Command.Fail(errorMessage);
            }

            return Command.Ok();
        }
    }
}