// -------------------------------------------------------------------------------------------------
// <copyright file="DecimalExtensionsTests.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2015-2017 Nautech Systems Pty Ltd. All rights reserved.
//   http://www.nautechsystems.net
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common.Tests.ExtensionsTests
{
    using System.Diagnostics.CodeAnalysis;
    using NautechSystems.Common.Extensions;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Reviewed. Suppression is OK within the Test Suite.")]
    public class DecimalExtensionsTests
    {
        [Theory]
        [InlineData(1, 0.1)]
        [InlineData(3, 0.001)]
        [InlineData(5, 0.00001)]
        internal void GetTickSizeFromInt_VariousInputs_ReturnsExpectedDecimal(int fromInt, decimal expectedResult)
        {
            // Arrange

            // Act
            var result = fromInt.ToTickSize();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}