using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomFieldData : DataSet
	{
		public const string CUSTOMFIELDID_FIELD = "CustomFieldID";

		public const string CUSTOMFIELDSETUPID_FIELD = "CustomFieldSetupID";

		public const string RECORDID_FIELD = "RecordID";

		public const string STRING1_FIELD = "String1";

		public const string STRING2_FIELD = "String2";

		public const string STRING3_FIELD = "String3";

		public const string STRING4_FIELD = "String4";

		public const string STRING5_FIELD = "String5";

		public const string STRING6_FIELD = "String6";

		public const string STRING7_FIELD = "String7";

		public const string STRING8_FIELD = "String8";

		public const string STRING9_FIELD = "String9";

		public const string STRING10_FIELD = "String10";

		public const string MEMO1_FIELD = "Memo1";

		public const string MEMO2_FIELD = "Memo2";

		public const string MEMO3_FIELD = "Memo3";

		public const string MEMO4_FIELD = "Memo4";

		public const string DECIMAL1_FIELD = "Decimal1";

		public const string DECIMAL2_FIELD = "Decimal2";

		public const string DECIMAL3_FIELD = "Decimal3";

		public const string DECIMAL4_FIELD = "Decimal4";

		public const string DECIMAL5_FIELD = "Decimal5";

		public const string DECIMAL6_FIELD = "Decimal6";

		public const string BOOL1_FIELD = "Bool1";

		public const string BOOL2_FIELD = "Bool2";

		public const string BOOL3_FIELD = "Bool3";

		public const string BOOL4_FIELD = "Bool4";

		public const string BOOL5_FIELD = "Bool5";

		public const string BOOL6_FIELD = "Bool6";

		public const string DATETIME1_FIELD = "DateTime1";

		public const string DATETIME2_FIELD = "DateTime2";

		public const string DATETIME3_FIELD = "DateTime3";

		public const string DATETIME4_FIELD = "DateTime4";

		public const string DATETIME5_FIELD = "DateTime5";

		public const string DATETIME6_FIELD = "DateTime6";

		public const string COMBO1_FIELD = "Combo1";

		public const string COMBO2_FIELD = "Combo2";

		public const string COMBO3_FIELD = "Combo3";

		public const string COMBO4_FIELD = "Combo4";

		public const string COMBO5_FIELD = "Combo5";

		public const string COMBO6_FIELD = "Combo6";

		public const string COMBO7_FIELD = "Combo7";

		public const string COMBO8_FIELD = "Combo8";

		public const string COMBO9_FIELD = "Combo9";

		public const string COMBO10_FIELD = "Combo10";

		public const string CUSTOMFIELDS_TABLE = "[Custom Fields]";

		public DataTable CustomFieldTable => base.Tables["[Custom Fields]"];

		public CustomFieldData()
		{
			BuildDataTables();
		}

		public CustomFieldData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Custom Fields]");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("CustomFieldID", typeof(int));
			columns.Add("CustomFieldSetupID", typeof(int));
			columns.Add("RecordID", typeof(int));
			columns.Add("String1", typeof(string));
			columns.Add("String2", typeof(string));
			columns.Add("String3", typeof(string));
			columns.Add("String4", typeof(string));
			columns.Add("String5", typeof(string));
			columns.Add("String6", typeof(string));
			columns.Add("String7", typeof(string));
			columns.Add("String8", typeof(string));
			columns.Add("String9", typeof(string));
			columns.Add("String10", typeof(string));
			columns.Add("Memo1", typeof(string));
			columns.Add("Memo2", typeof(string));
			columns.Add("Memo3", typeof(string));
			columns.Add("Memo4", typeof(string));
			columns.Add("Decimal1", typeof(decimal));
			columns.Add("Decimal2", typeof(decimal));
			columns.Add("Decimal3", typeof(decimal));
			columns.Add("Decimal4", typeof(decimal));
			columns.Add("Decimal5", typeof(decimal));
			columns.Add("Decimal6", typeof(decimal));
			columns.Add("Bool1", typeof(bool));
			columns.Add("Bool2", typeof(bool));
			columns.Add("Bool3", typeof(bool));
			columns.Add("Bool4", typeof(bool));
			columns.Add("Bool5", typeof(bool));
			columns.Add("Bool6", typeof(bool));
			columns.Add("DateTime1", typeof(DateTime));
			columns.Add("DateTime2", typeof(DateTime));
			columns.Add("DateTime3", typeof(DateTime));
			columns.Add("DateTime4", typeof(DateTime));
			columns.Add("DateTime5", typeof(DateTime));
			columns.Add("DateTime6", typeof(DateTime));
			columns.Add("Combo1", typeof(int));
			columns.Add("Combo2", typeof(int));
			columns.Add("Combo3", typeof(int));
			columns.Add("Combo4", typeof(int));
			columns.Add("Combo5", typeof(int));
			columns.Add("Combo6", typeof(int));
			columns.Add("Combo7", typeof(int));
			columns.Add("Combo8", typeof(int));
			columns.Add("Combo9", typeof(int));
			columns.Add("Combo10", typeof(int));
			base.Tables.Add(dataTable);
		}
	}
}
