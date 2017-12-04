// -------------------------------------------------------------------------------------------------
// <copyright file="CommandExtensions.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Extensions
{
    using System;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable static <see cref="CommandExtensions"/> class. Provides useful 
    /// <see cref="Command"/> extension methods.
    /// </summary>
    [Immutable]
    public static class CommandExtensions
    {
        /// <summary>
        /// Invokes the given action for a successful <see cref="Command"/>, 
        /// then returns the <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="action">The action to invoke (cannot be null).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command OnSuccess(this Command command, Action action)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(action, nameof(action));

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
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="func">The function to call (cannot be null).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command OnSuccess(this Command command, Func<Command> func)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(func, nameof(func));

            return command.IsFailure
                ? command
                : func();
        }

        /// <summary>
        /// Invokes the given action for a failed <see cref="Command"/>, 
        /// then returns the <see cref="Command"/>. 
        /// </summary>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="action">The action to invoke (cannot be null).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command OnFailure(this Command command, Action action)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(action, nameof(action));

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
        /// <param name="command">The command to evaluated (cannot be null).</param>
        /// <param name="action">The action to invoke (cannot be null).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command OnFailure(this Command command, Action<string> action)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(action, nameof(action));

            if (command.IsFailure)
            {
                action(command.Message);
            }

            return command;
        }

        /// <summary>
        /// Calls the given function on either a success or failure command result.
        /// </summary>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="func">The function (cannot be null).</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A type.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static T OnBoth<T>(this Command command, Func<Command, T> func)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(func, nameof(func));

            return func(command);
        }

        /// <summary>
        /// If the command is successful then calls the given function and returns a <see cref="Query{T}"/>.
        /// </summary>
        /// <typeparam name="T">The query type.</typeparam>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="func">The function to return (cannot be null).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> OnSuccess<T>(this Command command, Func<T> func)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(func, nameof(func));

            return command.IsFailure
                       ? Query<T>.Fail(command.Message)
                       : Query<T>.Ok(func());
        }

        /// <summary>
        /// On success returns the given function, otherwise returns a failure <see cref="Query{T}"/>
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="func">The function to return (cannot be null).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> OnSuccess<T>(this Command command, Func<Query<T>> func)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(func, nameof(func));

            return command.IsFailure
                       ? Query<T>.Fail(command.Message)
                       : func();
        }

        /// <summary>
        /// On success returns a success <see cref="Query{T}"/> with the given function
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="func">The function to return (cannot be null).</param>
        /// <returns>A <see cref="Query{T}"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Query<T> Map<T>(this Command command, Func<T> func)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(func, nameof(func));

            return command.IsFailure
                       ? Query<T>.Fail(command.Message)
                       : Query<T>.Ok(func());
        }

        /// <summary>
        /// Ensures a failed <see cref="Command"/> result is returned on error, otherwise returns ok.
        /// </summary>
        /// <param name="command">The command to evaluate (cannot be null).</param>
        /// <param name="predicate">The predicate to evaluate (cannot be null).</param>
        /// <param name="errorMessage">The error message string (cannot be null).</param>
        /// <returns>A <see cref="Command"/> result.</returns>
        /// <exception cref="ValidationException">Throws if the validation fails.</exception>
        public static Command Ensure(this Command command, Func<bool> predicate, string errorMessage)
        {
            Validate.NotNull(command, nameof(command));
            Validate.NotNull(predicate, nameof(predicate));
            Validate.NotNull(errorMessage, nameof(errorMessage));

            if (command.IsFailure)
            {
                return Command.Fail(command.Message);
            }

            if (!predicate())
            {
                return Command.Fail(errorMessage);
            }

            return Command.Ok();
        }
    }
}