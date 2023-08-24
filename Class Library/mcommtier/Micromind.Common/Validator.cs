using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Micromind.Common
{
	public class Validator
	{
		public enum CompareCriteria
		{
			Equal,
			Greater,
			GreaterEqual,
			Less,
			LessEqual
		}

		private const string REGEXP_ISVALIDEMAIL = "^\\w+((-\\w+)|(\\.\\w+))*\\@\\w+((\\.|-)\\w+)*\\.\\w+$";

		private Validator()
		{
		}

		public static string GetErrorCols(DataRow dataRow)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataColumn[] columnsInError = dataRow.GetColumnsInError();
			foreach (DataColumn dataColumn in columnsInError)
			{
				stringBuilder.AppendFormat("\n{0}: {1} ", dataColumn.ColumnName, dataRow.GetColumnError(dataColumn.ColumnName));
			}
			return stringBuilder.ToString();
		}

		public static void ThrowValidatorException(params string[] messages)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in messages)
			{
				stringBuilder.Append(value).Append('\n');
			}
			throw new ApplicationException(stringBuilder.ToString())
			{
				Source = "Validator"
			};
		}

		private static string MustBeBetweenFormat(string first, string second)
		{
			return "Must be between '" + first + "' and '" + second + "'";
		}

		private static string MustBeGreateOrEqualFormat(string strExp)
		{
			return "Must be greater or equal than '" + strExp + "'";
		}

		private static string MustBeLessOrEqualFormat(string strExp)
		{
			return "Must be less or equal than '" + strExp + "'";
		}

		public static bool IsValidStringField(DataRow dataRow, string fieldName, float minLen, float maxLen)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				if (minLen == 0f)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Must have a value.");
				return false;
			}
			short num = checked((short)dataRow[fieldName].ToString().Trim().Length);
			if ((float)num < minLen || (float)num > maxLen)
			{
				dataRow.SetColumnError(fieldName, MustBeBetweenFormat(minLen.ToString(), maxLen.ToString()));
				return false;
			}
			return true;
		}

		public static bool IsValidDecimalField(DataRow dataRow, string fieldName)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			bool result = true;
			try
			{
				decimal.Parse(dataRow[fieldName].ToString().Trim());
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidIntegerField(string fieldName, string data, int min, int max, StringBuilder errorMessage)
		{
			bool result = true;
			try
			{
				int num = int.Parse(data.Trim());
				if (num < min || num > max)
				{
					errorMessage.Append(fieldName + " " + MustBeBetweenFormat(min.ToString(), max.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				errorMessage.Append(data + " is an Invalid " + fieldName);
				return false;
			}
		}

		public static bool IsValidDecimalField(DataRow dataRow, string fieldName, decimal min, decimal max)
		{
			bool result = true;
			try
			{
				decimal d = decimal.Parse(dataRow[fieldName].ToString().Trim());
				if (d < min || d > max)
				{
					dataRow.SetColumnError(fieldName, MustBeBetweenFormat(min.ToString(), max.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidPositiveDecimalField(string fieldName)
		{
			bool result = true;
			try
			{
				if (decimal.Parse(fieldName.Trim()) < 0m)
				{
					return false;
				}
				return result;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsValidPositiveDecimalField(DataRow dataRow, string fieldName, bool DBNullOK)
		{
			bool result = true;
			if (IsDBNull(dataRow, fieldName))
			{
				if (DBNullOK)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Cannot be null.");
				return false;
			}
			try
			{
				if (decimal.Parse(dataRow[fieldName].ToString().Trim()) < 0m)
				{
					dataRow.SetColumnError(fieldName, "Must be greater or equal than zero.");
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidPositiveSingleField(DataRow dataRow, string fieldName)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			bool result = true;
			try
			{
				if (float.Parse(dataRow[fieldName].ToString().Trim()) < 0f)
				{
					dataRow.SetColumnError(fieldName, "Negaitve PARAM");
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidSingleField(string fieldName, string data, float min, float max, StringBuilder errorMessage)
		{
			bool result = true;
			try
			{
				float num = float.Parse(data.Trim());
				if (num < min || num > max)
				{
					errorMessage.Append(fieldName + " " + MustBeBetweenFormat(min.ToString(), max.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				errorMessage.Append(data + " is an Invalid " + fieldName);
				return false;
			}
		}

		public static bool IsValidSingleField(DataRow dataRow, string fieldName, float min, float max)
		{
			bool result = true;
			try
			{
				float num = float.Parse(dataRow[fieldName].ToString().Trim());
				if (num < min || num > max)
				{
					dataRow.SetColumnError(fieldName, MustBeBetweenFormat(min.ToString(), max.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidSingleField(DataRow dataRow, string fieldName, float min)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			bool result = true;
			try
			{
				if (IsDBNull(dataRow, fieldName) && min == 0f)
				{
					return true;
				}
				if (float.Parse(dataRow[fieldName].ToString().Trim()) < min)
				{
					dataRow.SetColumnError(fieldName, MustBeGreateOrEqualFormat(min.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidIntegerField(DataRow dataRow, string fieldName)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			bool result = true;
			try
			{
				if (int.Parse(dataRow[fieldName].ToString().Trim()) < 0)
				{
					dataRow.SetColumnError(fieldName, "Negaitve PARAM");
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidIntegerField(DataRow dataRow, string fieldName, int min, int max)
		{
			bool result = true;
			try
			{
				int num = int.Parse(dataRow[fieldName].ToString().Trim());
				if (num < min || num > max)
				{
					dataRow.SetColumnError(fieldName, MustBeBetweenFormat(min.ToString(), max.ToString()));
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidPositiveDecimalField(DataRow dataRow, string fieldName)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			bool result = true;
			try
			{
				if (decimal.Parse(dataRow[fieldName].ToString().Trim()) < 0m)
				{
					dataRow.SetColumnError(fieldName, "Negaitve PARAM");
					return false;
				}
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Field");
				return false;
			}
		}

		public static bool IsValidDate(string fieldName)
		{
			bool result = true;
			try
			{
				DateTime.Parse(fieldName.Trim());
				return result;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsValidDate(DataRow dataRow, string fieldName, bool dbNullOk)
		{
			if (IsDBNull(dataRow, fieldName))
			{
				if (dbNullOk)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Date must be entered.");
				return false;
			}
			bool result = true;
			try
			{
				DateTime.Parse(dataRow[fieldName].ToString());
				return result;
			}
			catch
			{
				dataRow.SetColumnError(fieldName, "Invalid Date Filed");
				return false;
			}
		}

		public static bool IsValidMinMaxDate(DataRow dataRow, string fieldName, DateTime minDate, DateTime maxDate, bool dbNullOk)
		{
			bool flag = true;
			if (IsDBNull(dataRow, fieldName))
			{
				if (dbNullOk)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Date for " + fieldName + " must be entered.");
				return false;
			}
			if (flag)
			{
				DateTime t = DateTime.Parse(dataRow[fieldName].ToString());
				if (t < minDate || t > maxDate)
				{
					dataRow.SetColumnError(fieldName, MustBeBetweenFormat(minDate.ToShortDateString(), maxDate.ToShortDateString()));
					flag = false;
				}
			}
			return flag;
		}

		public static bool IsValidMaxDate(DataRow dataRow, string fieldName, DateTime maxDate, bool dbNullOk)
		{
			bool flag = true;
			if (IsDBNull(dataRow, fieldName))
			{
				if (dbNullOk)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Must be entered.");
				return false;
			}
			if (flag && DateTime.Parse(dataRow[fieldName].ToString()) > maxDate)
			{
				dataRow.SetColumnError(fieldName, MustBeLessOrEqualFormat(maxDate.ToShortDateString()));
				flag = false;
			}
			return flag;
		}

		public static bool IsValidMinDate(DataRow dataRow, string fieldName, DateTime minDate, bool dbNullOk)
		{
			bool flag = true;
			if (IsDBNull(dataRow, fieldName))
			{
				if (dbNullOk)
				{
					return true;
				}
				dataRow.SetColumnError(fieldName, "Must be entered.");
				return false;
			}
			if (flag && DateTime.Parse(dataRow[fieldName].ToString()) < minDate)
			{
				dataRow.SetColumnError(fieldName, MustBeGreateOrEqualFormat(minDate.ToShortDateString()));
				flag = false;
			}
			return flag;
		}

		public static bool IsValidEmail(string emailAddress)
		{
			if (emailAddress.Trim().Length == 0)
			{
				return true;
			}
			return new Regex("^\\w+((-\\w+)|(\\.\\w+))*\\@\\w+((\\.|-)\\w+)*\\.\\w+$").IsMatch(emailAddress);
		}

		public static bool IsValidEmail(DataRow dataRow, string fieldName, bool isRequired)
		{
			if (!isRequired && IsDBNull(dataRow, fieldName))
			{
				return true;
			}
			string text = dataRow[fieldName].ToString();
			if (!isRequired && text.Trim().Length == 0)
			{
				return true;
			}
			bool flag = IsValidStringField(dataRow, fieldName, 0f, 64f);
			if (flag)
			{
				flag = new Regex("^\\w+((-\\w+)|(\\.\\w+))*\\@\\w+((\\.|-)\\w+)*\\.\\w+$").IsMatch(dataRow[fieldName].ToString().Trim());
				if (!flag)
				{
					dataRow.SetColumnError(fieldName, "Email Invalid Format");
				}
			}
			return flag;
		}

		public static bool IsValidCompareSingle(DataRow dataRow, string strA, string strB, CompareCriteria criteria)
		{
			bool flag = true;
			float num = 0f;
			float num2 = 0f;
			try
			{
				num = float.Parse(dataRow[strA].ToString().Trim());
			}
			catch
			{
				dataRow.SetColumnError(strA, "Invalid Field");
				flag = false;
			}
			try
			{
				num2 = float.Parse(dataRow[strB].ToString().Trim());
			}
			catch
			{
				dataRow.SetColumnError(strB, "Invalid Field");
				flag = false;
			}
			if (flag)
			{
				switch (criteria)
				{
				case CompareCriteria.Equal:
					flag = (num == num2);
					break;
				case CompareCriteria.Greater:
					flag = (num > num2);
					break;
				case CompareCriteria.GreaterEqual:
					flag = (num >= num2);
					break;
				case CompareCriteria.Less:
					flag = (num < num2);
					break;
				case CompareCriteria.LessEqual:
					flag = (num <= num2);
					break;
				default:
					flag = false;
					break;
				}
			}
			return flag;
		}

		public static bool IsDBNull(DataRow dataRow, string fieldName)
		{
			if (dataRow[fieldName] == DBNull.Value)
			{
				return true;
			}
			return false;
		}

		public static bool HasValueExactlyXTimes(int x, object objValue, DataRow dataRow, params string[] paramFieldNames)
		{
			int num = 0;
			string[] array = paramFieldNames;
			foreach (string columnName in array)
			{
				if (dataRow[columnName].Equals(objValue))
				{
					num = checked(num + 1);
				}
			}
			if (num == x)
			{
				return true;
			}
			array = paramFieldNames;
			foreach (string columnName2 in array)
			{
				dataRow.SetColumnError(columnName2, "Only one field can have the value '" + objValue.ToString() + "'.");
			}
			return false;
		}

		public static string RemoveChar(string str, char c)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			while (true)
			{
				int num = str.IndexOf(c);
				if (num < 0)
				{
					break;
				}
				str = str.Remove(num, 1);
			}
			return str;
		}
	}
}
