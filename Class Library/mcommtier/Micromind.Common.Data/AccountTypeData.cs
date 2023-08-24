using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AccountTypeData : DataSet
	{
		public const string TYPEID_FIELD = "TypeID";

		public const string SHORTNAME_FIELD = "AccountTypeName";

		public const string DESCRIPTIONS_FIELD = "Descriptions";

		public const string ISBANK_FIELD = "IsBank";

		public const string ISAR_FIELD = "IsAR";

		public const string ISOTHERCURRENTASSET_FILED = "IsOtherCurrentAsset";

		public const string ISFIXEDASSET_FIELD = "IsFixedAsset";

		public const string ISAP_FIELD = "IsAP";

		public const string ISOTHERCURRENTLIABILITY_FIELD = "IsOtherCurrentLiability";

		public const string ISLONGTERMLIABILITY_FIELD = "IsLongTermLiability";

		public const string ISCREDITCARD_FIELD = "IsCreditCard";

		public const string ISINCOME_FIELD = "IsIncome";

		public const string ISLIABILITY_FIELD = "IsLiability";

		public const string ISEQUITY_FIELD = "IsEquity";

		public const string IsCOGS_FIELD = "IsCOGS";

		public const string ISEXPENSE_FIELD = "IsExpense";

		public const string ISOTHEREXPENSE_FIELD = "IsOtherExpense";

		public const string ISOTHERINCOME_FIELD = "IsOtherIncome";

		public const string ACCOUNTTYPES_TABLE = "[Account Types]";

		public DataTable AccountTypeTable => base.Tables["[Account Types]"];

		public AccountTypeData()
		{
			BuildDataTables();
		}

		public AccountTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Account Types]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("AccountTypeName", typeof(string));
			columns.Add("Descriptions", typeof(string));
			columns.Add("IsBank", typeof(bool)).DefaultValue = false;
			columns.Add("IsAR", typeof(bool)).DefaultValue = false;
			columns.Add("IsOtherCurrentAsset", typeof(bool)).DefaultValue = false;
			columns.Add("IsFixedAsset", typeof(bool)).DefaultValue = false;
			columns.Add("IsAP", typeof(bool)).DefaultValue = false;
			columns.Add("IsOtherCurrentLiability", typeof(bool)).DefaultValue = false;
			columns.Add("IsLongTermLiability", typeof(bool)).DefaultValue = false;
			columns.Add("IsCreditCard", typeof(bool)).DefaultValue = false;
			columns.Add("IsIncome", typeof(bool)).DefaultValue = false;
			columns.Add("IsLiability", typeof(bool)).DefaultValue = false;
			columns.Add("IsEquity", typeof(bool)).DefaultValue = false;
			columns.Add("IsCOGS", typeof(bool)).DefaultValue = false;
			columns.Add("IsExpense", typeof(bool)).DefaultValue = false;
			columns.Add("IsOtherExpense", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}

		public static string GetAccountTypeName(CompanyAccountTypes type)
		{
			switch (type)
			{
			case CompanyAccountTypes.CostofGoodsSold:
				return "Cost of Goods Sold";
			case CompanyAccountTypes.AccountsPayable:
				return "Accounts Payable";
			case CompanyAccountTypes.AccountReceivable:
				return "Accounts Receivable";
			case CompanyAccountTypes.CreditCard:
				return "Credit Card";
			case CompanyAccountTypes.FixedAsset:
				return "Fixed Asset";
			case CompanyAccountTypes.LongTermLiability:
				return "Long Term Liability";
			case CompanyAccountTypes.OtherAsset:
				return "Other Asset";
			case CompanyAccountTypes.OtherCurrentAsset:
				return "Other Current Asset";
			case CompanyAccountTypes.OtherCurrentLiability:
				return "Other Current Liability";
			case CompanyAccountTypes.OtherExpense:
				return "Other Expense";
			case CompanyAccountTypes.OtherIncome:
				return "Other Income";
			case CompanyAccountTypes.Cash:
				return "Cash";
			default:
				return type.ToString();
			}
		}
	}
}
