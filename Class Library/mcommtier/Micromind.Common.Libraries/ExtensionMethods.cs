using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Micromind.Common.Libraries
{
	public static class ExtensionMethods
	{
		public static bool IsNullOrEmpty(this object value)
		{
			if (value == null || value.ToString().Trim() == "")
			{
				return true;
			}
			return false;
		}

		public static bool IsDBNullOrEmpty(this object value)
		{
			if (value == null || value == DBNull.Value || value.ToString().Trim() == "")
			{
				return true;
			}
			return false;
		}

		public static bool IsNumeric(this string theValue)
		{
			long result;
			return long.TryParse(theValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
		}

		public static bool IsNumeric(this object theValue)
		{
			long result;
			return long.TryParse(theValue as string, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
		}

		public static bool IsNumericGreaterThanZero(this object theValue)
		{
			if (theValue == null || theValue.ToString() == "")
			{
				return false;
			}
			if (decimal.TryParse(theValue.ToString(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out decimal result))
			{
				if (result > 0m)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public static bool IsDate(this string input)
		{
			DateTime result;
			if (!string.IsNullOrEmpty(input))
			{
				return DateTime.TryParse(input, out result);
			}
			return false;
		}

		public static string IsNull(this string inString, string defaultString)
		{
			if (inString == null)
			{
				return defaultString;
			}
			return inString;
		}

		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}

		public static T Parse<T>(this string value)
		{
			T result = default(T);
			if (!string.IsNullOrEmpty(value))
			{
				return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
			}
			return result;
		}

		public static TValue ConvertTo<TValue>(this string text)
		{
			TValue val = default(TValue);
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
			if (converter.CanConvertFrom(text.GetType()))
			{
				return (TValue)converter.ConvertFrom(text);
			}
			converter = TypeDescriptor.GetConverter(text.GetType());
			if (converter.CanConvertTo(typeof(TValue)))
			{
				return (TValue)converter.ConvertTo(text, typeof(TValue));
			}
			throw new NotSupportedException();
		}

		public static DateTime EndOfDay(this DateTime value)
		{
			return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
		}

		public static decimal ToDecimal(this string value)
		{
			decimal result = default(decimal);
			if (value.IsNullOrEmpty())
			{
				return 0m;
			}
			decimal.TryParse(value, out result);
			return result;
		}

		public static DateTime BeginOfDay(this DateTime value)
		{
			return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
		}

		public static int ToInt(this string current)
		{
			int.TryParse(current, out int result);
			return result;
		}

		public static string IsNullThenEmpty(this string inString)
		{
			if (inString == null)
			{
				return string.Empty;
			}
			return inString;
		}

		public static bool Contains(this string @this, string value, StringComparison comparisonType)
		{
			if (@this == null)
			{
				throw new ArgumentNullException("this");
			}
			return @this.IndexOf(value, comparisonType) >= 0;
		}

		public static bool In(this string s, params string[] values)
		{
			values.Contains(s);
			return values.Any((string x) => x.Equals(s));
		}

		public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
		{
			if (condition)
			{
				return source.Where(predicate);
			}
			return source;
		}

		public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
		{
			if (condition)
			{
				return source.Where(predicate);
			}
			return source;
		}

		public static IEnumerable<T> SplitTo<T>(this string str, params char[] separator) where T : IConvertible
		{
			return from s in str.Split(separator, StringSplitOptions.None)
				select (T)Convert.ChangeType(s, typeof(T));
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
			{
				action(item);
			}
		}
	}
}
