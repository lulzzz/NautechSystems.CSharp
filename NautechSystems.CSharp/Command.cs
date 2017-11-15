// -------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp
{
    using System.Linq;
    using System.Runtime.Serialization;
    using NautechSystems.CSharp.Annotations;
    using NautechSystems.CSharp.Validation;

    /// <summary>
    /// The immutable sealed <see cref="Command"/> <see cref="Result"/> class.
    /// </summary>
    [Immutable]
    public sealed class Command : Result, ISerializable
    {
        private static readonly Command OkCommand = new Command(false, null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFailure"></param>
        /// <param name="error"></param>
        private Command(bool isFailure, string error)
            : base(isFailure, error)
        {
        }

        /// <summary>
        /// Returns a success command result.
        /// </summary>
        /// <returns>A <see cref="Command"/></returns>
        public static Command Ok()
        {
            return OkCommand;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns>A <see cref="Command"/></returns>
        public static Command Fail(string error)
        {
            return new Command(true, error);
        }

        /// <summary>
        /// Returns first failure in the list of <paramref name="commands"/>. 
        /// If there is no failure returns success.
        /// </summary>
        /// <param name="commands">Array of commands.</param>
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
        /// <param name="commands">List of commands.</param>
        public static Command Combine(params Command[] commands)
        {
            Validate.NotNull(commands, nameof(commands));

            var failedResults = commands.Where(x => x.IsFailure).ToList();

            if (!failedResults.Any())
            {
                return Ok();
            }              

            var errorMessage = string.Join("; ", failedResults.Select(x => x.Error).ToArray());

            return Fail(errorMessage);
        }

        void ISerializable.GetObjectData(SerializationInfo oInfo, StreamingContext oContext)
        {
            oInfo.AddValue(nameof(IsFailure), IsFailure);
            oInfo.AddValue(nameof(IsSuccess), IsSuccess);

            if (IsFailure)
            {
                oInfo.AddValue(nameof(Error), Error);
            }
        }
    }
}