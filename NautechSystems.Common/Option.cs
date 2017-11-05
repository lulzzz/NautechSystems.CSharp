﻿// -------------------------------------------------------------------------------------------------
// <copyright file="Option.cs" company="Nautech Systems Pty Ltd.">
//   Copyright (C) 2015-2017 Nautech Systems Pty Ltd. All rights reserved.
//   http://www.nautechsystems.net
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace NautechSystems.Common
{
    using System;
    using NautechSystems.Common.Annotations;

    /// <summary>
    /// The option immutable structure. Wraps a potentially null value.
    /// </summary>
    /// <typeparam name="T">The option type.</typeparam>
    [Immutable]
    public struct Option<T> : IEquatable<Option<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Option{T}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        private Option(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Returns an <see cref="Option{T}"/> with no value.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public static Option<T> None => new Option<T>();

        /// <summary>
        /// Gets the value of the <see cref="Option{T}"/>.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Returns a <see cref="bool"/> indicating whether the <see cref="Option{T}"/> has a value.
        /// </summary>
        public bool HasValue => this.Value != null;

        /// <summary>
        /// Returns a <see cref="bool"/> indicating whether the <see cref="Option{T}"/> has NO value.
        /// </summary>
        public bool HasNoValue => !this.HasValue;

        /// <summary>
        /// Returns the given object wrapped in an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public static Option<T> From(T obj)
        {
            return new Option<T>(obj);
        }

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public static implicit operator Option<T>(T value)
        {
            return new Option<T>(value);
        }

        /// <summary>
        /// The == operator.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/>.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="bool"/></returns>
        public static bool operator ==(Option<T> option, T value)
        {
            return !option.HasNoValue && option.Value.Equals(value);
        }

        /// <summary>
        /// The != operator.
        /// </summary>
        /// <param name="option">The <see cref="Option{T}"/>.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="bool"/></returns>
        public static bool operator !=(Option<T> option, T value)
        {
            return !(option == value);
        }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="left">The left <see cref="Option{T}"/>.</param>
        /// <param name="right">The right <see cref="Option{T}"/>.</param>
        /// <returns>A <see cref="bool"/></returns>
        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="left">The left <see cref="Option{T}"/>.</param>
        /// <param name="right">The right <see cref="Option{T}"/>.</param>
        /// <returns>A <see cref="bool"/></returns>
        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                obj = new Option<T>((T)obj);
            }

            if (!(obj is Option<T>))
            {
                return false;
            }

            var other = (Option<T>)obj;
            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Option{T}"/> 
        /// is equal to the current <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="other">The other object.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        public bool Equals(Option<T> other)
        {
            if (this.HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (this.HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return this.Value.Equals(other.Value);
        }

        /// <summary>
        /// Returns the hash code of the wrapped object.
        /// </summary>
        /// <returns>An <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            if (this.HasNoValue)
            {
                return 0;
            }

            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the wrapped value.
        /// </summary>
        /// <returns>A <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (this.HasNoValue)
            {
                return nameof(this.HasNoValue);
            }

            return this.Value.ToString();
        }
    }
}