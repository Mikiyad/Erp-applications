using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.ClientLibraries
{
	public static class CompanyOptions
	{
		private static DataSet companyOptions;

		private static DataSet sysDocCompanyOptions;

		public static void LoadCompanyOptions()
		{
			try
			{
				if (Factory.IsDBConnected)
				{
					companyOptions = Factory.CompanyOptionSystem.GetCompanyOptionList();
				}
			}
			catch
			{
				throw;
			}
		}

		public static void LoadSysDocCompanyOptions()
		{
			try
			{
				if (Factory.IsDBConnected)
				{
					sysDocCompanyOptions = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				}
			}
			catch
			{
				throw;
			}
		}

		public static CustomType GetCompanyOption<CustomType>(CompanyOptionsEnum optionID, CustomType defaultValue)
		{
			int num = (int)optionID;
			if (companyOptions == null)
			{
				LoadCompanyOptions();
			}
			if (companyOptions == null)
			{
				return defaultValue;
			}
			DataRow[] array = companyOptions.Tables["Company_Option"].Select("OptionID = '" + num.ToString() + "'");
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
			if (typeof(CustomType) == typeof(byte))
			{
				return (CustomType)(object)byte.Parse(obj.ToString());
			}
			if (typeof(CustomType) == typeof(decimal))
			{
				return (CustomType)(object)decimal.Parse(obj.ToString());
			}
			if (typeof(CustomType) == typeof(DateTime))
			{
				return (CustomType)(object)DateTime.Parse(obj.ToString());
			}
			return (CustomType)obj;
		}

		public static CustomType GetCompanyOption<CustomType>(CompanyOptionsEnum optionID, byte docType, string sysDocID, CustomType defaultValue)
		{
			int num = (int)optionID;
			if (sysDocCompanyOptions == null)
			{
				LoadSysDocCompanyOptions();
			}
			if (sysDocCompanyOptions == null)
			{
				return defaultValue;
			}
			DataRow[] array = null;
			array = sysDocCompanyOptions.Tables["Company_Option"].Select("OptionID = '" + num.ToString() + "' AND SysDocID= '" + sysDocID + "' AND SysDocType=" + docType.ToString() + " ");
			if (array.Length == 0)
			{
				array = sysDocCompanyOptions.Tables["Company_Option"].Select("OptionID = '" + num.ToString() + "'  AND SysDocType=" + docType.ToString() + " AND SysDocID ='" + DBNull.Value + "'");
			}
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
			if (typeof(CustomType) == typeof(byte))
			{
				return (CustomType)(object)byte.Parse(obj.ToString());
			}
			if (typeof(CustomType) == typeof(decimal))
			{
				return (CustomType)(object)decimal.Parse(obj.ToString());
			}
			return (CustomType)obj;
		}
	}
}
