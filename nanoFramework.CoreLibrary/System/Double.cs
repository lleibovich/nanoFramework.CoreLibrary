//
// Copyright (c) .NET Foundation and Contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.
//

namespace System
{
    using Globalization;
    using Runtime.CompilerServices;

    /// <summary>
    /// Represents a double-precision floating-point number.
    /// </summary>
    [Serializable]
    public struct Double
    {
        internal const string _naNSymbol = "NaN";
        internal const string _negativeInfinitySymbol = "-" + _positiveInfinitySymbol;
        internal const string _positiveInfinitySymbol = "Infinity";

        // this field is required in the native end
#pragma warning disable 0649
        internal double _value;
#pragma warning restore 0649

        /// <summary>
        /// Represents the smallest possible value of a Double. This field is constant.
        /// </summary>
        /// <remarks>The value of this constant is negative 1.7976931348623157E+308.</remarks>
        public const double MinValue = -1.7976931348623157E+308;
        /// <summary>
        /// Represents the largest possible value of a Double. This field is constant.
        /// </summary>
        /// <remarks>The value of this constant is positive 1.7976931348623157E+308.</remarks>
        public const double MaxValue = 1.7976931348623157E+308;

        /// <summary>
        /// Represents the smallest positive Double value that is greater than zero. This field is constant.
        /// </summary>
        /// <remarks>The value of this constant is 4.94065645841247e-324.</remarks>
        public const double Epsilon = 4.9406564584124650E-324;

        /// <summary>
        /// Represents negative infinity. This field is constant.
        /// </summary>
        public const double NegativeInfinity = -1.0 / 0.0;

        /// <summary>
        /// Represents positive infinity. This field is constant.
        /// </summary>
        public const double PositiveInfinity = 1.0 / 0.0;

#pragma warning disable S1764 // Identical expressions should not be used on both sides of a binary operator
        // intended as the purpose is to a NaN value

        /// <summary>
        /// Represents a value that is not a number (NaN). This field is constant.
        /// </summary>
        public const double NaN = 0.0 / 0.0;
#pragma warning restore S1764 // Identical expressions should not be used on both sides of a binary operator

        /// <summary>
        /// Compares this instance to a specified double-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified double-precision floating-point number.
        /// </summary>
        /// <param name="value">A double-precision floating-point number to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.
        /// Less than zero: This instance is less than value. -or- This instance is not a number (<see cref="NaN"/>) and value is a number.
        /// Zero: This instance is equal to value. -or- Both this instance and value are not a number (<see cref="NaN"/>), <see cref="PositiveInfinity"/>, or <see cref="NegativeInfinity"/>. 
        /// Greater than zero: This instance is greater than value. -or- This instance is a number and value is not a number (<see cref="NaN"/>). 
        /// </returns>
        public int CompareTo(double value)
        {
            return CompareTo(this, value);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern int CompareTo(double d, double value);

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity
        /// </summary>
        /// <param name="d">A double-precision floating-point number. </param>
        /// <returns>
        /// true if d evaluates to PositiveInfinity or NegativeInfinity; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsInfinity(double d);

        /// <summary>
        /// Returns a value that indicates whether the specified value is not a number (NaN).
        /// </summary>
        /// <param name="d">A double-precision floating-point number. </param>
        /// <returns>
        /// true if d evaluates to NaN; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsNaN(double d);

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        /// <returns>
        /// true if d evaluates to NegativeInfinity; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsNegativeInfinity(double d);

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="d">A double-precision floating-point number. </param>
        /// <returns>
        /// true if d evaluates to PositiveInfinity; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPositiveInfinity(double d);

        /// <summary>
        /// Converts the string representation of a number to its double-precision floating-point number equivalent.
        /// </summary>
        /// <param name="s">A string that contains a number to convert. </param>
        /// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
        public static double Parse(string s)
        {
            // check for null string is carried out in native code
            return Convert.ToDouble(s);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString("G");
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        public string ToString(string format)
        {
            if (IsPositiveInfinity(this))
            {
                return _positiveInfinitySymbol;
            }
            else if (IsNegativeInfinity(this))
            {
                return _negativeInfinitySymbol;
            }
            else if (IsNaN(this))
            {
                return _naNSymbol;
            }

            return Number.Format(_value, false, format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the string representation of a number to its double-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string containing a number to convert. </param>
        /// <param name="result">When this method returns, contains the double-precision floating-point number equivalent of the <paramref name="s"/> parameter, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is <see langword="null"/> or <see cref="string.Empty"/> or is not a number in a valid format. It also fails if <paramref name="s"/> represents a number less than <see cref="MinValue"/> or greater than <see cref="MaxValue"/>. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="s"/> was converted successfully; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// <para>
        /// Values that are too large to represent are rounded to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/> as required by the IEEE 754 specification.
        /// </para>
        /// <para>
        /// This overload differs from the <see cref="Parse"/>(String) method by returning a <see cref="bool"/> value that indicates whether the parse operation succeeded instead of returning the parsed numeric value. It eliminates the need to use exception handling to test for a <see cref="FormatException"/> in the event that <paramref name="s"/> is invalid and cannot be successfully parsed.
        /// </para>
        /// </remarks>
        public static bool TryParse(
            string s,
            out double result)
        {
            result = Convert.NativeToDouble(
              s,
              false,
              out bool success);

            return success;
        }
    }
}
