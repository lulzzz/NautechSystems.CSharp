// -------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.CSharp
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Validation
{
    using System;
    using NautechSystems.CSharp.Annotations;

    /// <summary>
    /// The immutable sealed <see cref="ValidationException"/> class. Wraps all validation exceptions.
    /// </summary>
    [Immutable]
    public sealed class ValidationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="ex">The inner exception.</param>
        public ValidationException(ArgumentException ex)
            : base(ex.Message, ex)
        {            
        }
    }
}