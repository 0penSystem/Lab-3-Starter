/*
 * Sample Implementation
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    /// <summary>Provides some common verification functionality.</summary>
    public static class Verify
    {
        /// <summary>Verifies an argument meets a certain condition.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message.</param>
        public static void ArgumentIs<T> ( string name, T value, Func<T, bool> condition, string message )
        {
            if (!condition(value))
                throw new ArgumentException(message, name);
        }

        /// <summary>Verifies an argument is greater than a given value..</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="message">The message.</param>
        public static void ArgumentIsGreaterThanOrEqualTo<T> ( string name, T value, T minValue ) where T : IComparable<T>
        {
            ArgumentIsGreaterThanOrEqualTo(name, value, minValue, $"{name} must be >= {minValue}");
        }

        /// <summary>Verifies an argument is greater than a given value..</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="message">The message.</param>
        public static void ArgumentIsGreaterThanOrEqualTo<T> ( string name, T value, T minValue, string message ) where T: IComparable<T>
        {
            if (value.CompareTo(minValue) < 0)
                throw new ArgumentOutOfRangeException(name, message);
        }

        /// <summary>Verifies an argument is not null.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        public static void ArgumentIsNotNull<T> ( string name, T value ) where T: class
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>Verifies an argument is valid as defined by IValidatableObject.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        public static void ArgumentIsValid<T> ( string name, T value ) where T: IValidatableObject
        {
            Validator.ValidateObject(value, new ValidationContext(value));
        }
    }
}
