// -------------------------------------------------------------------------------------------------
// <copyright file="CanBeNullAttribute.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2017. All rights reserved.
//   https://github.com/nautechsystems/NautechSystems.Common
//   the use of this source code is governed by the Apache 2.0 license
//   as found in the LICENSE.txt file.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.CSharp.Annotations
{
    using System;

    /// <summary>
    /// This decoration indicates that null is a possible and expected value of the annotated parameter
    /// (therefor an explicit check for null is not required).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CanBeNullAttribute : Attribute
    {
    }
}
