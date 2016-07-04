﻿#region License
// Copyright (c) 2016 Tyler Brinkley
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ExtraConstraints;

namespace EnumsNET
{
    /// <summary>
    /// Static class that provides efficient type-safe flag enum operations through the use of cached enum names, values, and attributes.
    /// Many operations are exposed as C# extension methods for convenience.
    /// </summary>
    public static class FlagEnums
    {
        internal const string DefaultDelimiter = ", ";

        #region "Properties"
        /// <summary>
        /// Indicates if <typeparamref name="TEnum"/> is marked with the <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns>Indication if <typeparamref name="TEnum"/> is marked with the <see cref="FlagsAttribute"/>.</returns>
        [Pure]
        public static bool IsFlagEnum<[EnumConstraint] TEnum>()
            where TEnum : struct => Enums<TEnum>.Info.IsFlagEnum;

        /// <summary>
        /// Retrieves all the flags defined by <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns>All the flags defined by <typeparamref name="TEnum"/>.</returns>
        [Pure]
        public static TEnum GetAllFlags<[EnumConstraint] TEnum>()
            where TEnum : struct => Enums<TEnum>.Info.AllFlags;
        #endregion

        #region Main Methods
        /// <summary>
        /// Indicates whether <paramref name="value"/> is a valid flag combination of the defined enum values.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Indication of whether <paramref name="value"/> is a valid flag combination of the defined enum values.</returns>
        [Pure]
        public static bool IsValidFlagCombination<[EnumConstraint] TEnum>(TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.IsValidFlagCombination(value);

        /// <summary>
        /// Returns the names of <paramref name="value"/>'s flags delimited with commas or if empty returns the name of the zero flag if defined otherwise "0".
        /// If <paramref name="value"/> is not a valid flag combination null is returned.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>The names of <paramref name="value"/>'s flags delimited with commas or if empty returns the name of the zero flag if defined otherwise "0".
        /// If <paramref name="value"/> is not a valid flag combination null is returned.</returns>
        [Pure]
        public static string FormatFlags<[EnumConstraint] TEnum>(TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.FormatFlags(value);

        /// <summary>
        /// Returns <paramref name="value"/>'s flags formatted with <paramref name="formatOrder"/> and delimited with commas
        /// or if empty return the zero flag formatted with <paramref name="formatOrder"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="formatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static string FormatFlags<[EnumConstraint] TEnum>(TEnum value, params EnumFormat[] formatOrder)
            where TEnum : struct => Enums<TEnum>.Info.FormatFlags(value, null, formatOrder);

        /// <summary>
        /// Returns the names of <paramref name="value"/>'s flags delimited with <paramref name="delimiter"/> or if empty returns the name of the zero flag if defined otherwise "0".
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter">The delimiter to use to separate individual flag names. Cannot be null or empty.</param>
        /// <returns>The names of <paramref name="value"/>'s flags delimited with <paramref name="delimiter"/> or if empty returns the name of the zero flag if defined otherwise "0".
        /// If <paramref name="value"/> is not a valid flag combination null is returned.</returns>
        [Pure]
        public static string FormatFlags<[EnumConstraint] TEnum>(TEnum value, string delimiter)
            where TEnum : struct => Enums<TEnum>.Info.FormatFlags(value, delimiter);

        /// <summary>
        /// Returns <paramref name="value"/>'s flags formatted with <paramref name="formatOrder"/> and delimited with <paramref name="delimiter"/>
        /// or if empty return the zero flag formatted with <paramref name="formatOrder"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="formatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static string FormatFlags<[EnumConstraint] TEnum>(TEnum value, string delimiter, params EnumFormat[] formatOrder)
            where TEnum : struct => Enums<TEnum>.Info.FormatFlags(value, delimiter, formatOrder);

        /// <summary>
        /// Returns an array of the flags that compose <paramref name="value"/>.
        /// If <paramref name="value"/> is not a valid flag combination null is returned.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Array of the flags that compose <paramref name="value"/>.
        /// If <paramref name="value"/> is not a valid flag combination null is returned.</returns>
        [Pure]
        public static IEnumerable<TEnum> GetFlags<[EnumConstraint] TEnum>(this TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.GetFlags(value);

        /// <summary>
        /// Indicates if <paramref name="value"/> has any flags set.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Indication if <paramref name="value"/> has any flags set.</returns>
        [Pure]
        public static bool HasAnyFlags<[EnumConstraint] TEnum>(this TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.HasAnyFlags(value);

        /// <summary>
        /// Indicates if <paramref name="value"/> has any flags set that are also set in <paramref name="otherFlags"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns>Indication if <paramref name="value"/> has any flags set that are also set in <paramref name="otherFlags"/>.</returns>
        [Pure]
        public static bool HasAnyFlags<[EnumConstraint] TEnum>(this TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.HasAnyFlags(value, otherFlags);

        /// <summary>
        /// Indicates if <paramref name="value"/> has all flags set that are defined in <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns>Indication if <paramref name="value"/> has all flags set that are defined in <typeparamref name="TEnum"/>.</returns>
        [Pure]
        public static bool HasAllFlags<[EnumConstraint] TEnum>(this TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.HasAllFlags(value);

        /// <summary>
        /// Indicates if <paramref name="value"/> has all of the flags set that are also set in <paramref name="otherFlags"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns>Indication if <paramref name="value"/> has all of the flags set that are also set in <paramref name="otherFlags"/>.</returns>
        [Pure]
        public static bool HasAllFlags<[EnumConstraint] TEnum>(this TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.HasAllFlags(value, otherFlags);

        /// <summary>
        /// Returns <paramref name="value"/> with all of it's flags toggled. Equivalent to the bitwise "xor" operator with <see cref="GetAllFlags{TEnum}()"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns><paramref name="value"/> with all of it's flags toggled.</returns>
        [Pure]
        public static TEnum ToggleFlags<[EnumConstraint] TEnum>(TEnum value)
            where TEnum : struct => Enums<TEnum>.Info.ToggleFlags(value);

        /// <summary>
        /// Returns <paramref name="value"/> with all of it's flags toggled. If <paramref name="toggleValidFlagsOnly"/> is <c>true</c> then equivalent to the bitwise "xor" operator with <see cref="GetAllFlags{TEnum}()"/>
        /// else is equivalent to the bitwise "not" operator.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="toggleValidFlagsOnly"></param>
        /// <returns><paramref name="value"/> with all of it's flags toggled.</returns>
        [Pure]
        public static TEnum ToggleFlags<[EnumConstraint] TEnum>(TEnum value, bool toggleValidFlagsOnly)
            where TEnum : struct => Enums<TEnum>.Info.ToggleFlags(value, toggleValidFlagsOnly);

        /// <summary>
        /// Returns <paramref name="value"/> while toggling the flags that are set in <paramref name="otherFlags"/>. Equivalent to the bitwise "xor" operator.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns><paramref name="value"/> while toggling the flags that are set in <paramref name="otherFlags"/>.</returns>
        [Pure]
        public static TEnum ToggleFlags<[EnumConstraint] TEnum>(TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.ToggleFlags(value, otherFlags);

        /// <summary>
        /// Returns <paramref name="value"/> with only the flags that are also set in <paramref name="otherFlags"/>. Equivalent to the bitwise "and" operation.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns><paramref name="value"/> with only the flags that are also set in <paramref name="otherFlags"/>.</returns>
        [Pure]
        public static TEnum CommonFlags<[EnumConstraint] TEnum>(this TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.CommonFlags(value, otherFlags);

        /// <summary>
        /// Returns <paramref name="value"/> with the flags specified in <paramref name="otherFlags"/> set. Equivalent to the bitwise "or" operation.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns><paramref name="value"/> with the flags specified in <paramref name="otherFlags"/> set.</returns>
        [Pure]
        public static TEnum CombineFlags<[EnumConstraint] TEnum>(this TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.CombineFlags(value, otherFlags);

        /// <summary>
        /// Returns all of <paramref name="flags"/> combined with the bitwise "or" operation.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flags">Must be valid flag combinations.</param>
        /// <returns></returns>
        [Pure]
        public static TEnum CombineFlags<[EnumConstraint] TEnum>(params TEnum[] flags)
            where TEnum : struct => Enums<TEnum>.Info.CombineFlags(flags);

        /// <summary>
        /// Returns <paramref name="value"/> with the flags specified in <paramref name="otherFlags"/> cleared.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="otherFlags"></param>
        /// <returns><paramref name="value"/> with the flags specified in <paramref name="otherFlags"/> cleared.</returns>
        [Pure]
        public static TEnum ExcludeFlags<[EnumConstraint] TEnum>(this TEnum value, TEnum otherFlags)
            where TEnum : struct => Enums<TEnum>.Info.ExcludeFlags(value, otherFlags);
        #endregion

        #region Parsing
        /// <summary>
        /// Converts the string representation of one or more enum members to an equivalent enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value);

        /// <summary>
        /// Converts the string representation of one or more enum members to an equivalent enum value.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, false, null, parseFormatOrder);

        /// <summary>
        /// Converts the string representation of one or more enum members to an equivalent enum value.
        /// The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, ignoreCase);

        /// <summary>
        /// Converts the string representation of one or more enum members to an equivalent enum value.
        /// The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, ignoreCase, null, parseFormatOrder);

        /// <summary>
        /// Converts the string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, string delimiter)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, false, delimiter);

        /// <summary>
        /// Converts the string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, string delimiter, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, false, delimiter, parseFormatOrder);

        /// <summary>
        /// Converts the string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value.
        /// The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, ignoreCase, delimiter);

        /// <summary>
        /// Converts the string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value.
        /// The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space
        /// -or-
        /// <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="TEnum"/>.</exception>
        [Pure]
        public static TEnum ParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.ParseFlags(value, ignoreCase, delimiter, parseFormatOrder);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, false, null, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, TEnum defaultValue, params EnumFormat[] parseFormatOrder)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, false, null, out result, parseFormatOrder) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, bool ignoreCase, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, null, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, bool ignoreCase, TEnum defaultValue, params EnumFormat[] parseFormatOrder)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, null, out result, parseFormatOrder) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, string delimiter, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, false, delimiter, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, string delimiter, TEnum defaultValue, params EnumFormat[] parseFormatOrder)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, false, delimiter, out result, parseFormatOrder) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, delimiter, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value but if it fails
        /// returns the specified <paramref name="defaultValue"/>. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static TEnum ParseFlagsOrDefault<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter, TEnum defaultValue, params EnumFormat[] parseFormatOrder)
            where TEnum : struct
        {
            TEnum result;
            return Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, delimiter, out result, parseFormatOrder) ? result : defaultValue;
        }

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, out TEnum result)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, false, null, out result);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, out TEnum result, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, false, null, out result, parseFormatOrder);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, out TEnum result)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, null, out result);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, out TEnum result, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, null, out result, parseFormatOrder);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, string delimiter, out TEnum result)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, false, delimiter, out result);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <param name="result"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, string delimiter, out TEnum result, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, false, delimiter, out result, parseFormatOrder);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter, out TEnum result)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, delimiter, out result);

        /// <summary>
        /// Tries to convert the specified string representation of one or more enum members delimited with the specified <paramref name="delimiter"/> to an equivalent enum value. The return value
        /// indicates whether the conversion succeeded. The parameter <paramref name="ignoreCase"/> specifies whether the operation is case-insensitive.
        /// The parameter <paramref name="parseFormatOrder"/> specifies the order of enum parsing formats.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="delimiter"></param>
        /// <param name="result"></param>
        /// <param name="parseFormatOrder"></param>
        /// <returns></returns>
        [Pure]
        public static bool TryParseFlags<[EnumConstraint] TEnum>(string value, bool ignoreCase, string delimiter, out TEnum result, params EnumFormat[] parseFormatOrder)
            where TEnum : struct => Enums<TEnum>.Info.TryParseFlags(value, ignoreCase, delimiter, out result, parseFormatOrder);
        #endregion
    }
}
