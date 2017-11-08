// -------------------------------------------------------------------------------------------------
// <copyright file="ImmutableAttribute.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Annotations
{
    using System;

    /// <summary>
    /// This decoration indicates that the annotated class or structure should be completely immutable
    /// (to fulfill its design specification). Once instantiated the public properties of the object 
    /// should not change.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ImmutableAttribute : Attribute
    {
    }
}