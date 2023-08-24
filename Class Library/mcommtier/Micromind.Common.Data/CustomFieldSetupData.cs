using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomFieldSetupData : DataSet
	{
		public const string CUSTOMFIELDSETUPS_TABLE = "[Custom Field Setups]";

		public const string CUSTOMFIELDSETUPID_FIELD = "CustomFieldSetupID";

		public const string TABLENAME_FIELD = "TableName";

		public const string CUSTOMTYPE_FIELD = "CustomType";

		public const string ISINACTIVESTRING1_FIELD = "IsInactiveString1";

		public const string STRING1GROUPID_FIELD = "String1GroupID";

		public const string STRING1LABEL_FIELD = "String1Label";

		public const string ISINACTIVESTRING2_FIELD = "IsInactiveString2";

		public const string STRING2GROUPID_FIELD = "String2GroupID";

		public const string STRING2LABEL_FIELD = "String2Label";

		public const string ISINACTIVESTRING3_FIELD = "IsInactiveString3";

		public const string STRING3GROUPID_FIELD = "String3GroupID";

		public const string STRING3LABEL_FIELD = "String3Label";

		public const string ISINACTIVESTRING4_FIELD = "IsInactiveString4";

		public const string STRING4GROUPID_FIELD = "String4GroupID";

		public const string STRING4LABEL_FIELD = "String4Label";

		public const string ISINACTIVESTRING5_FIELD = "IsInactiveString5";

		public const string STRING5GROUPID_FIELD = "String5GroupID";

		public const string STRING5LABEL_FIELD = "String5Label";

		public const string ISINACTIVESTRING6_FIELD = "IsInactiveString6";

		public const string STRING6GROUPID_FIELD = "String6GroupID";

		public const string STRING6LABEL_FIELD = "String6Label";

		public const string ISINACTIVESTRING7_FIELD = "IsInactiveString7";

		public const string STRING7GROUPID_FIELD = "String7GroupID";

		public const string STRING7LABEL_FIELD = "String7Label";

		public const string ISINACTIVESTRING8_FIELD = "IsInactiveString8";

		public const string STRING8GROUPID_FIELD = "String8GroupID";

		public const string STRING8LABEL_FIELD = "String8Label";

		public const string ISINACTIVESTRING9_FIELD = "IsInactiveString9";

		public const string STRING9GROUPID_FIELD = "String9GroupID";

		public const string STRING9LABEL_FIELD = "String9Label";

		public const string ISINACTIVESTRING10_FIELD = "IsInactiveString10";

		public const string STRING10GROUPID_FIELD = "String10GroupID";

		public const string STRING10LABEL_FIELD = "String10Label";

		public const string ISINACTIVEMEMO1_FIELD = "IsInactiveMemo1";

		public const string MEMO1GROUPID_FIELD = "Memo1GroupID";

		public const string MEMO1LABEL_FIELD = "Memo1Label";

		public const string ISINACTIVEMEMO2_FIELD = "IsInactiveMemo2";

		public const string MEMO2GROUPID_FIELD = "Memo2GroupID";

		public const string MEMO2LABEL_FIELD = "Memo2Label";

		public const string ISINACTIVEMEMO3_FIELD = "IsInactiveMemo3";

		public const string MEMO3GROUPID_FIELD = "Memo3GroupID";

		public const string MEMO3LABEL_FIELD = "Memo3Label";

		public const string ISINACTIVEMEMO4_FIELD = "IsInactiveMemo4";

		public const string MEMO4GROUPID_FIELD = "Memo4GroupID";

		public const string MEMO4LABEL_FIELD = "Memo4Label";

		public const string ISINACTIVEDECIMAL1_FIELD = "IsInactiveDecimal1";

		public const string DECIMAL1GROUPID_FIELD = "Decimal1GroupID";

		public const string DECIMAL1LABEL_FIELD = "Decimal1Label";

		public const string ISINACTIVEDECIMAL2_FIELD = "IsInactiveDecimal2";

		public const string DECIMAL2GROUPID_FIELD = "Decimal2GroupID";

		public const string DECIMAL2LABEL_FIELD = "Decimal2Label";

		public const string ISINACTIVEDECIMAL3_FIELD = "IsInactiveDecimal3";

		public const string DECIMAL3GROUPID_FIELD = "Decimal3GroupID";

		public const string DECIMAL3LABEL_FIELD = "Decimal3Label";

		public const string ISINACTIVEDECIMAL4_FIELD = "IsInactiveDecimal4";

		public const string DECIMAL4GROUPID_FIELD = "Decimal4GroupID";

		public const string DECIMAL4LABEL_FIELD = "Decimal4Label";

		public const string ISINACTIVEDECIMAL5_FIELD = "IsInactiveDecimal5";

		public const string DECIMAL5GROUPID_FIELD = "Decimal5GroupID";

		public const string DECIMAL5LABEL_FIELD = "Decimal5Label";

		public const string ISINACTIVEDECIMAL6_FIELD = "IsInactiveDecimal6";

		public const string DECIMAL6GROUPID_FIELD = "Decimal6GroupID";

		public const string DECIMAL6LABEL_FIELD = "Decimal6Label";

		public const string ISINACTIVEBOOL1_FIELD = "IsInactiveBool1";

		public const string BOOL1GROUPID_FIELD = "Bool1GroupID";

		public const string BOOL1LABEL_FIELD = "Bool1Label";

		public const string ISINACTIVEBOOL2_FIELD = "IsInactiveBool2";

		public const string BOOL2GROUPID_FIELD = "Bool2GroupID";

		public const string BOOL2LABEL_FIELD = "Bool2Label";

		public const string ISINACTIVEBOOL3_FIELD = "IsInactiveBool3";

		public const string BOOL3GROUPID_FIELD = "Bool3GroupID";

		public const string BOOL3LABEL_FIELD = "Bool3Label";

		public const string ISINACTIVEBOOL4_FIELD = "IsInactiveBool4";

		public const string BOOL4GROUPID_FIELD = "Bool4GroupID";

		public const string BOOL4LABEL_FIELD = "Bool4Label";

		public const string ISINACTIVEBOOL5_FIELD = "IsInactiveBool5";

		public const string BOOL5GROUPID_FIELD = "Bool5GroupID";

		public const string BOOL5LABEL_FIELD = "Bool5Label";

		public const string ISINACTIVEBOOL6_FIELD = "IsInactiveBool6";

		public const string BOOL6GROUPID_FIELD = "Bool6GroupID";

		public const string BOOL6LABEL_FIELD = "Bool6Label";

		public const string ISINACTIVEDATETIME1_FIELD = "IsInactiveDateTime1";

		public const string DATETIME1GROUPID_FIELD = "DateTime1GroupID";

		public const string DATETIME1LABEL_FIELD = "DateTime1Label";

		public const string ISINACTIVEDATETIME2_FIELD = "IsInactiveDateTime2";

		public const string DATETIME2GROUPID_FIELD = "DateTime2GroupID";

		public const string DATETIME2LABEL_FIELD = "DateTime2Label";

		public const string ISINACTIVEDATETIME3_FIELD = "IsInactiveDateTime3";

		public const string DATETIME3GROUPID_FIELD = "DateTime3GroupID";

		public const string DATETIME3LABEL_FIELD = "DateTime3Label";

		public const string ISINACTIVEDATETIME4_FIELD = "IsInactiveDateTime4";

		public const string DATETIME4GROUPID_FIELD = "DateTime4GroupID";

		public const string DATETIME4LABEL_FIELD = "DateTime4Label";

		public const string ISINACTIVEDATETIME5_FIELD = "IsInactiveDateTime5";

		public const string DATETIME5GROUPID_FIELD = "DateTime5GroupID";

		public const string DATETIME5LABEL_FIELD = "DateTime5Label";

		public const string ISINACTIVEDATETIME6_FIELD = "IsInactiveDateTime6";

		public const string DATETIME6GROUPID_FIELD = "DateTime6GroupID";

		public const string DATETIME6LABEL_FIELD = "DateTime6Label";

		public const string ISINACTIVECOMBO1_FIELD = "IsInactiveCombo1";

		public const string COMBO1GROUPID_FIELD = "Combo1GroupID";

		public const string COMBO1LABEL_FIELD = "Combo1Label";

		public const string COMBO1TYPE_FIELD = "Combo1Type";

		public const string ISINACTIVECOMBO2_FIELD = "IsInactiveCombo2";

		public const string COMBO2GROUPID_FIELD = "Combo2GroupID";

		public const string COMBO2LABEL_FIELD = "Combo2Label";

		public const string COMBO2TYPE_FIELD = "Combo2Type";

		public const string ISINACTIVECOMBO3_FIELD = "IsInactiveCombo3";

		public const string COMBO3GROUPID_FIELD = "Combo3GroupID";

		public const string COMBO3LABEL_FIELD = "Combo3Label";

		public const string COMBO3TYPE_FIELD = "Combo3Type";

		public const string ISINACTIVECOMBO4_FIELD = "IsInactiveCombo4";

		public const string COMBO4GROUPID_FIELD = "Combo4GroupID";

		public const string COMBO4LABEL_FIELD = "Combo4Label";

		public const string COMBO4TYPE_FIELD = "Combo4Type";

		public const string ISINACTIVECOMBO5_FIELD = "IsInactiveCombo5";

		public const string COMBO5GROUPID_FIELD = "Combo5GroupID";

		public const string COMBO5LABEL_FIELD = "Combo5Label";

		public const string COMBO5TYPE_FIELD = "Combo5Type";

		public const string ISINACTIVECOMBO6_FIELD = "IsInactiveCombo6";

		public const string COMBO6GROUPID_FIELD = "Combo6GroupID";

		public const string COMBO6LABEL_FIELD = "Combo6Label";

		public const string COMBO6TYPE_FIELD = "Combo6Type";

		public const string ISINACTIVECOMBO7_FIELD = "IsInactiveCombo7";

		public const string COMBO7GROUPID_FIELD = "Combo7GroupID";

		public const string COMBO7LABEL_FIELD = "Combo7Label";

		public const string COMBO7TYPE_FIELD = "Combo7Type";

		public const string ISINACTIVECOMBO8_FIELD = "IsInactiveCombo8";

		public const string COMBO8GROUPID_FIELD = "Combo8GroupID";

		public const string COMBO8LABEL_FIELD = "Combo8Label";

		public const string COMBO8TYPE_FIELD = "Combo8Type";

		public const string ISINACTIVECOMBO9_FIELD = "IsInactiveCombo9";

		public const string COMBO9GROUPID_FIELD = "Combo9GroupID";

		public const string COMBO9LABEL_FIELD = "Combo9Label";

		public const string COMBO9TYPE_FIELD = "Combo9Type";

		public const string ISINACTIVECOMBO10_FIELD = "IsInactiveCombo10";

		public const string COMBO10GROUPID_FIELD = "Combo10GroupID";

		public const string COMBO10LABEL_FIELD = "Combo10Label";

		public const string COMBO10TYPE_FIELD = "Combo10Type";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable CustomFieldSetupTable => base.Tables["[Custom Field Setups]"];

		public CustomFieldSetupData()
		{
			BuildDataTables();
		}

		public CustomFieldSetupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Custom Field Setups]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CustomFieldSetupID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TableName", typeof(string));
			columns.Add("CustomType", typeof(byte));
			columns.Add("IsInactiveString1", typeof(bool)).DefaultValue = true;
			columns.Add("String1GroupID", typeof(int));
			columns.Add("String1Label", typeof(string));
			columns.Add("IsInactiveString2", typeof(bool)).DefaultValue = true;
			columns.Add("String2GroupID", typeof(int));
			columns.Add("String2Label", typeof(string));
			columns.Add("IsInactiveString3", typeof(bool)).DefaultValue = true;
			columns.Add("String3GroupID", typeof(int));
			columns.Add("String3Label", typeof(string));
			columns.Add("IsInactiveString4", typeof(bool)).DefaultValue = true;
			columns.Add("String4GroupID", typeof(int));
			columns.Add("String4Label", typeof(string));
			columns.Add("IsInactiveString5", typeof(bool)).DefaultValue = true;
			columns.Add("String5GroupID", typeof(int));
			columns.Add("String5Label", typeof(string));
			columns.Add("IsInactiveString6", typeof(bool)).DefaultValue = true;
			columns.Add("String6GroupID", typeof(int));
			columns.Add("String6Label", typeof(string));
			columns.Add("IsInactiveString7", typeof(bool)).DefaultValue = true;
			columns.Add("String7GroupID", typeof(int));
			columns.Add("String7Label", typeof(string));
			columns.Add("IsInactiveString8", typeof(bool)).DefaultValue = true;
			columns.Add("String8GroupID", typeof(int));
			columns.Add("String8Label", typeof(string));
			columns.Add("IsInactiveString9", typeof(bool)).DefaultValue = true;
			columns.Add("String9GroupID", typeof(int));
			columns.Add("String9Label", typeof(string));
			columns.Add("IsInactiveString10", typeof(bool)).DefaultValue = true;
			columns.Add("String10GroupID", typeof(int));
			columns.Add("String10Label", typeof(string));
			columns.Add("IsInactiveMemo1", typeof(bool)).DefaultValue = true;
			columns.Add("Memo1GroupID", typeof(int));
			columns.Add("Memo1Label", typeof(string));
			columns.Add("IsInactiveMemo2", typeof(bool)).DefaultValue = true;
			columns.Add("Memo2GroupID", typeof(int));
			columns.Add("Memo2Label", typeof(string));
			columns.Add("IsInactiveMemo3", typeof(bool)).DefaultValue = true;
			columns.Add("Memo3GroupID", typeof(int));
			columns.Add("Memo3Label", typeof(string));
			columns.Add("IsInactiveMemo4", typeof(bool)).DefaultValue = true;
			columns.Add("Memo4GroupID", typeof(int));
			columns.Add("Memo4Label", typeof(string));
			columns.Add("IsInactiveDecimal1", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal1GroupID", typeof(int));
			columns.Add("Decimal1Label", typeof(string));
			columns.Add("IsInactiveDecimal2", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal2GroupID", typeof(int));
			columns.Add("Decimal2Label", typeof(string));
			columns.Add("IsInactiveDecimal3", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal3GroupID", typeof(int));
			columns.Add("Decimal3Label", typeof(string));
			columns.Add("IsInactiveDecimal4", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal4GroupID", typeof(int));
			columns.Add("Decimal4Label", typeof(string));
			columns.Add("IsInactiveDecimal5", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal5GroupID", typeof(int));
			columns.Add("Decimal5Label", typeof(string));
			columns.Add("IsInactiveDecimal6", typeof(bool)).DefaultValue = true;
			columns.Add("Decimal6GroupID", typeof(int));
			columns.Add("Decimal6Label", typeof(string));
			columns.Add("IsInactiveBool1", typeof(bool)).DefaultValue = true;
			columns.Add("Bool1GroupID", typeof(int));
			columns.Add("Bool1Label", typeof(string));
			columns.Add("IsInactiveBool2", typeof(bool)).DefaultValue = true;
			columns.Add("Bool2GroupID", typeof(int));
			columns.Add("Bool2Label", typeof(string));
			columns.Add("IsInactiveBool3", typeof(bool)).DefaultValue = true;
			columns.Add("Bool3GroupID", typeof(int));
			columns.Add("Bool3Label", typeof(string));
			columns.Add("IsInactiveBool4", typeof(bool)).DefaultValue = true;
			columns.Add("Bool4GroupID", typeof(int));
			columns.Add("Bool4Label", typeof(string));
			columns.Add("IsInactiveBool5", typeof(bool)).DefaultValue = true;
			columns.Add("Bool5GroupID", typeof(int));
			columns.Add("Bool5Label", typeof(string));
			columns.Add("IsInactiveBool6", typeof(bool)).DefaultValue = true;
			columns.Add("Bool6GroupID", typeof(int));
			columns.Add("Bool6Label", typeof(string));
			columns.Add("IsInactiveDateTime1", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime1GroupID", typeof(int));
			columns.Add("DateTime1Label", typeof(string));
			columns.Add("IsInactiveDateTime2", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime2GroupID", typeof(int));
			columns.Add("DateTime2Label", typeof(string));
			columns.Add("IsInactiveDateTime3", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime3GroupID", typeof(int));
			columns.Add("DateTime3Label", typeof(string));
			columns.Add("IsInactiveDateTime4", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime4GroupID", typeof(int));
			columns.Add("DateTime4Label", typeof(string));
			columns.Add("IsInactiveDateTime5", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime5GroupID", typeof(int));
			columns.Add("DateTime5Label", typeof(string));
			columns.Add("IsInactiveDateTime6", typeof(bool)).DefaultValue = true;
			columns.Add("DateTime6GroupID", typeof(int));
			columns.Add("DateTime6Label", typeof(string));
			columns.Add("IsInactiveCombo1", typeof(bool)).DefaultValue = true;
			columns.Add("Combo1GroupID", typeof(int));
			columns.Add("Combo1Label", typeof(string));
			columns.Add("Combo1Type", typeof(byte));
			columns.Add("IsInactiveCombo2", typeof(bool)).DefaultValue = true;
			columns.Add("Combo2GroupID", typeof(int));
			columns.Add("Combo2Label", typeof(string));
			columns.Add("Combo2Type", typeof(byte));
			columns.Add("IsInactiveCombo3", typeof(bool)).DefaultValue = true;
			columns.Add("Combo3GroupID", typeof(int));
			columns.Add("Combo3Label", typeof(string));
			columns.Add("Combo3Type", typeof(byte));
			columns.Add("IsInactiveCombo4", typeof(bool)).DefaultValue = true;
			columns.Add("Combo4GroupID", typeof(int));
			columns.Add("Combo4Label", typeof(string));
			columns.Add("Combo4Type", typeof(byte));
			columns.Add("IsInactiveCombo5", typeof(bool)).DefaultValue = true;
			columns.Add("Combo5GroupID", typeof(int));
			columns.Add("Combo5Label", typeof(string));
			columns.Add("Combo5Type", typeof(byte));
			columns.Add("IsInactiveCombo6", typeof(bool)).DefaultValue = true;
			columns.Add("Combo6GroupID", typeof(int));
			columns.Add("Combo6Label", typeof(string));
			columns.Add("Combo6Type", typeof(byte));
			columns.Add("IsInactiveCombo7", typeof(bool)).DefaultValue = true;
			columns.Add("Combo7GroupID", typeof(int));
			columns.Add("Combo7Label", typeof(string));
			columns.Add("Combo7Type", typeof(byte));
			columns.Add("IsInactiveCombo8", typeof(bool)).DefaultValue = true;
			columns.Add("Combo8GroupID", typeof(int));
			columns.Add("Combo8Label", typeof(string));
			columns.Add("Combo8Type", typeof(byte));
			columns.Add("IsInactiveCombo9", typeof(bool)).DefaultValue = true;
			columns.Add("Combo9GroupID", typeof(int));
			columns.Add("Combo9Label", typeof(string));
			columns.Add("Combo9Type", typeof(byte));
			columns.Add("IsInactiveCombo10", typeof(bool)).DefaultValue = true;
			columns.Add("Combo10GroupID", typeof(int));
			columns.Add("Combo10Label", typeof(string));
			columns.Add("Combo10Type", typeof(byte));
			columns.Add("DateTimeStamp", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
