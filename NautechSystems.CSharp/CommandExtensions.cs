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
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Command OnSuccess(this Command command, Action action)
        {
            if (command.IsSuccess)
            {
                action();
            }

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Command OnSuccess(this Command command, Func<Command> func)
        {
            return command.IsFailure
                ? command
                : func();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Command OnFailure(this Command command, Action action)
        {
            if (command.IsFailure)
            {
                action();
            }

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Command OnFailure(this Command command, Action<string> action)
        {
            if (command.IsFailure)
            {
                action(command.Error);
            }

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="predicate"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Command Ensure(this Command command, Func<bool> predicate, string errorMessage)
        {
            if (command.IsFailure)
                return Command.Fail(command.Error);

            if (!predicate())
                return Command.Fail(errorMessage);

            return Command.Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T OnBoth<T>(this Command command, Func<Command, T> func)
        {
            return func(command);
        }
    }
}