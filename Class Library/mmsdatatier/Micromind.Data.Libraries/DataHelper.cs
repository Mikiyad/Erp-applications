using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Micromind.Data.Libraries
{
	public class DataHelper : StoreObject
	{
		public DataHelper(Config config)
			: base(config)
		{
		}

		public string GetMonthNameAbr(int monthNumber)
		{
			switch (monthNumber)
			{
			case 1:
				return "Jan";
			case 2:
				return "Feb";
			case 3:
				return "Mar";
			case 4:
				return "Apr";
			case 5:
				return "May";
			case 6:
				return "Jun";
			case 7:
				return "Jul";
			case 8:
				return "Aug";
			case 9:
				return "Sep";
			case 10:
				return "Oct";
			case 11:
				return "Nov";
			case 12:
				return "Dec";
			default:
				return "NON";
			}
		}

		public static DateTime ConvertToGregorian(DateTime obj)
		{
			return new DateTime(obj.Year, obj.Month, obj.Day, obj.Hour, obj.Minute, obj.Second, new GregorianCalendar());
		}

		public static DateTime ConvertToPersian(DateTime obj)
		{
			try
			{
				PersianCalendar persianCalendar = new PersianCalendar();
				int year = persianCalendar.GetYear(obj);
				int month = persianCalendar.GetMonth(obj);
				int dayOfMonth = persianCalendar.GetDayOfMonth(obj);
				return new DateTime(year, month, dayOfMonth, obj.Hour, obj.Minute, obj.Second, new PersianCalendar());
			}
			catch
			{
				throw;
			}
		}

		public CustomType GetCompanyOption<CustomType>(DataSet companyOptionsData, CompanyOptionsEnum optionID, CustomType defaultValue)
		{
			int num = (int)optionID;
			if (companyOptionsData == null)
			{
				return defaultValue;
			}
			DataRow[] array = companyOptionsData.Tables["Company_Option"].Select("OptionID = '" + num.ToString() + "'");
			if (array.Length == 0)
			{
				return defaultValue;
			}
			object obj = array[0]["OptionValue"].ToString();
			if (typeof(CustomType) == typeof(bool))
			{
				return (CustomType)(object)bool.Parse(obj.ToString());
			}
			if (typeof(CustomType) == typeof(int))
			{
				return (CustomType)(object)int.Parse(obj.ToString());
			}
			return (CustomType)obj;
		}

		public DateTime ToBeginDate(DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day);
		}

		public DateTime ToEndDate(DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
		}

		public string ReplaceSystemParameters(string query)
		{
			return ReplaceSystemParameters(query, (DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue);
		}

		public string ReplaceSystemParameters(string query, DateTime fromDate, DateTime toDate)
		{
			string userID = base.DBConfig.UserID;
			string replacement = "";
			string replacement2 = "";
			string replacement3 = "";
			string replacement4 = CommonLib.ToSqlDateTimeString(fromDate);
			string replacement5 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(DefaultSalespersonID,'') AS CurrentSalesPerson,ISNULL(DefaultInventoryLocationID,'') AS CurrentInventoryLocation,\r\n                                ISNULL(DefaultTransactionLocationID,'') AS CurrentTransactionLocation FROM Users  WHERE UserID = '" + userID + "'";
			FillDataSet(dataSet, "Users", textCommand);
			DataRow dataRow = dataSet.Tables["Users"].Rows[0];
			if (dataSet != null && dataSet.Tables["Users"].Rows.Count > 0)
			{
				replacement = dataRow["CurrentSalesPerson"].ToString();
				replacement2 = dataRow["CurrentInventoryLocation"].ToString();
				replacement3 = dataRow["CurrentTransactionLocation"].ToString();
			}
			query = Regex.Replace(query, "@CurrentSalesPerson", replacement, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentInventoryLocation", replacement2, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentTransactionLocation", replacement3, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@CurrentUser", userID, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@FromDate", replacement4, RegexOptions.IgnoreCase);
			query = Regex.Replace(query, "@EndDate", replacement5, RegexOptions.IgnoreCase);
			return query;
		}
	}
}
