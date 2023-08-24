using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomFieldSetups : StoreObject
	{
		private const string CUSTOMFIELDSETUPID_PARM = "@CustomFieldSetupID";

		private const string TABLENAME_PARM = "@TableName";

		private const string CUSTOMTYPE_PARM = "@CustomType";

		private const string ISINACTIVESTRING1_PARM = "@IsInactiveString1";

		private const string STRING1GROUPID_PARM = "@String1GroupID";

		private const string STRING1LABEL_PARM = "@String1Label";

		private const string ISINACTIVESTRING2_PARM = "@IsInactiveString2";

		private const string STRING2GROUPID_PARM = "@String2GroupID";

		private const string STRING2LABEL_PARM = "@String2Label";

		private const string ISINACTIVESTRING3_PARM = "@IsInactiveString3";

		private const string STRING3GROUPID_PARM = "@String3GroupID";

		private const string STRING3LABEL_PARM = "@String3Label";

		private const string ISINACTIVESTRING4_PARM = "@IsInactiveString4";

		private const string STRING4GROUPID_PARM = "@String4GroupID";

		private const string STRING4LABEL_PARM = "@String4Label";

		private const string ISINACTIVESTRING5_PARM = "@IsInactiveString5";

		private const string STRING5GROUPID_PARM = "@String5GroupID";

		private const string STRING5LABEL_PARM = "@String5Label";

		private const string ISINACTIVESTRING6_PARM = "@IsInactiveString6";

		private const string STRING6GROUPID_PARM = "@String6GroupID";

		private const string STRING6LABEL_PARM = "@String6Label";

		private const string ISINACTIVESTRING7_PARM = "@IsInactiveString7";

		private const string STRING7GROUPID_PARM = "@String7GroupID";

		private const string STRING7LABEL_PARM = "@String7Label";

		private const string ISINACTIVESTRING8_PARM = "@IsInactiveString8";

		private const string STRING8GROUPID_PARM = "@String8GroupID";

		private const string STRING8LABEL_PARM = "@String8Label";

		private const string ISINACTIVESTRING9_PARM = "@IsInactiveString9";

		private const string STRING9GROUPID_PARM = "@String9GroupID";

		private const string STRING9LABEL_PARM = "@String9Label";

		private const string ISINACTIVESTRING10_PARM = "@IsInactiveString10";

		private const string STRING10GROUPID_PARM = "@String10GroupID";

		private const string STRING10LABEL_PARM = "@String10Label";

		private const string ISINACTIVEMEMO1_PARM = "@IsInactiveMemo1";

		private const string MEMO1GROUPID_PARM = "@Memo1GroupID";

		private const string MEMO1LABEL_PARM = "@Memo1Label";

		private const string ISINACTIVEMEMO2_PARM = "@IsInactiveMemo2";

		private const string MEMO2GROUPID_PARM = "@Memo2GroupID";

		private const string MEMO2LABEL_PARM = "@Memo2Label";

		private const string ISINACTIVEMEMO3_PARM = "@IsInactiveMemo3";

		private const string MEMO3GROUPID_PARM = "@Memo3GroupID";

		private const string MEMO3LABEL_PARM = "@Memo3Label";

		private const string ISINACTIVEMEMO4_PARM = "@IsInactiveMemo4";

		private const string MEMO4GROUPID_PARM = "@Memo4GroupID";

		private const string MEMO4LABEL_PARM = "@Memo4Label";

		private const string ISINACTIVEDECIMAL1_PARM = "@IsInactiveDecimal1";

		private const string DECIMAL1GROUPID_PARM = "@Decimal1GroupID";

		private const string DECIMAL1LABEL_PARM = "@Decimal1Label";

		private const string ISINACTIVEDECIMAL2_PARM = "@IsInactiveDecimal2";

		private const string DECIMAL2GROUPID_PARM = "@Decimal2GroupID";

		private const string DECIMAL2LABEL_PARM = "@Decimal1Labe2";

		private const string ISINACTIVEDECIMAL3_PARM = "@IsInactiveDecimal3";

		private const string DECIMAL3GROUPID_PARM = "@Decimal3GroupID";

		private const string DECIMAL3LABEL_PARM = "@Decimal3Label";

		private const string ISINACTIVEDECIMAL4_PARM = "@IsInactiveDecimal4";

		private const string DECIMAL4GROUPID_PARM = "@Decimal4GroupID";

		private const string DECIMAL4LABEL_PARM = "@Decimal4Label";

		private const string ISINACTIVEDECIMAL5_PARM = "@IsInactiveDecimal5";

		private const string DECIMAL5GROUPID_PARM = "@Decimal5GroupID";

		private const string DECIMAL5LABEL_PARM = "@Decimal5Label";

		private const string ISINACTIVEDECIMAL6_PARM = "@IsInactiveDecimal6";

		private const string DECIMAL6GROUPID_PARM = "@Decimal6GroupID";

		private const string DECIMAL6LABEL_PARM = "@Decimal6Label";

		private const string ISINACTIVEBOOL1_PARM = "@IsInactiveBool1";

		private const string BOOL1GROUPID_PARM = "@Bool1GroupID";

		private const string BOOL1LABEL_PARM = "@Bool1Label";

		private const string ISINACTIVEBOOL2_PARM = "@IsInactiveBool2";

		private const string BOOL2GROUPID_PARM = "@Bool2GroupID";

		private const string BOOL2LABEL_PARM = "@Bool2Label";

		private const string ISINACTIVEBOOL3_PARM = "@IsInactiveBool3";

		private const string BOOL3GROUPID_PARM = "@Bool3GroupID";

		private const string BOOL3LABEL_PARM = "@Bool3Label";

		private const string ISINACTIVEBOOL4_PARM = "@IsInactiveBool4";

		private const string BOOL4GROUPID_PARM = "@Bool4GroupID";

		private const string BOOL4LABEL_PARM = "@Bool4Label";

		private const string ISINACTIVEBOOL5_PARM = "@IsInactiveBool5";

		private const string BOOL5GROUPID_PARM = "@Bool5GroupID";

		private const string BOOL5LABEL_PARM = "@Bool5Label";

		private const string ISINACTIVEBOOL6_PARM = "@IsInactiveBool6";

		private const string BOOL6GROUPID_PARM = "@Bool6GroupID";

		private const string BOOL6LABEL_PARM = "@Bool6Label";

		private const string ISINACTIVEDATETIME1_PARM = "@IsInactiveDateTime1";

		private const string DATETIME1GROUPID_PARM = "@DateTime1GroupID";

		private const string DATETIME1LABEL_PARM = "@DateTime1Label";

		private const string ISINACTIVEDATETIME2_PARM = "@IsInactiveDateTime2";

		private const string DATETIME2GROUPID_PARM = "@DateTime2GroupID";

		private const string DATETIME2LABEL_PARM = "@DateTime2Label";

		private const string ISINACTIVEDATETIME3_PARM = "@IsInactiveDateTime3";

		private const string DATETIME3GROUPID_PARM = "@DateTime3GroupID";

		private const string DATETIME3LABEL_PARM = "@DateTime3Label";

		private const string ISINACTIVEDATETIME4_PARM = "@IsInactiveDateTime4";

		private const string DATETIME4GROUPID_PARM = "@DateTime4GroupID";

		private const string DATETIME4LABEL_PARM = "@DateTime4Label";

		private const string ISINACTIVEDATETIME5_PARM = "@IsInactiveDateTime5";

		private const string DATETIME5GROUPID_PARM = "@DateTime5GroupID";

		private const string DATETIME5LABEL_PARM = "@DateTime5Label";

		private const string ISINACTIVEDATETIME6_PARM = "@IsInactiveDateTime6";

		private const string DATETIME6GROUPID_PARM = "@DateTime6GroupID";

		private const string DATETIME6LABEL_PARM = "@DateTime6Label";

		private const string ISINACTIVECOMBO1_PARM = "@IsInactiveCombo1";

		private const string COMBO1GROUPID_PARM = "@Combo1GroupID";

		private const string COMBO1LABEL_PARM = "@Combo1Label";

		private const string COMBO1TYPE_PARM = "@Combo1Type";

		private const string ISINACTIVECOMBO2_PARM = "@IsInactiveCombo2";

		private const string COMBO2GROUPID_PARM = "@Combo2GroupID";

		private const string COMBO2LABEL_PARM = "@Combo2Label";

		private const string COMBO2TYPE_PARM = "@Combo2Type";

		private const string ISINACTIVECOMBO3_PARM = "@IsInactiveCombo3";

		private const string COMBO3GROUPID_PARM = "@Combo3GroupID";

		private const string COMBO3LABEL_PARM = "@Combo3Label";

		private const string COMBO3TYPE_PARM = "@Combo3Type";

		private const string ISINACTIVECOMBO4_PARM = "@IsInactiveCombo4";

		private const string COMBO4GROUPID_PARM = "@Combo4GroupID";

		private const string COMBO4LABEL_PARM = "@Combo4Label";

		private const string COMBO4TYPE_PARM = "@Combo4Type";

		private const string ISINACTIVECOMBO5_PARM = "@IsInactiveCombo5";

		private const string COMBO5GROUPID_PARM = "@Combo5GroupID";

		private const string COMBO5LABEL_PARM = "@Combo5Label";

		private const string COMBO5TYPE_PARM = "@Combo5Type";

		private const string ISINACTIVECOMBO6_PARM = "@IsInactiveCombo6";

		private const string COMBO6GROUPID_PARM = "@Combo6GroupID";

		private const string COMBO6LABEL_PARM = "@Combo6Label";

		private const string COMBO6TYPE_PARM = "@Combo6Type";

		private const string ISINACTIVECOMBO7_PARM = "@IsInactiveCombo7";

		private const string COMBO7GROUPID_PARM = "@Combo7GroupID";

		private const string COMBO7LABEL_PARM = "@Combo7Label";

		private const string COMBO7TYPE_PARM = "@Combo7Type";

		private const string ISINACTIVECOMBO8_PARM = "@IsInactiveCombo8";

		private const string COMBO8GROUPID_PARM = "@Combo8GroupID";

		private const string COMBO8LABEL_PARM = "@Combo8Label";

		private const string COMBO8TYPE_PARM = "@Combo8Type";

		private const string ISINACTIVECOMBO9_PARM = "@IsInactiveCombo9";

		private const string COMBO9GROUPID_PARM = "@Combo9GroupID";

		private const string COMBO9LABEL_PARM = "@Combo9Label";

		private const string COMBO9TYPE_PARM = "@Combo9Type";

		private const string ISINACTIVECOMBO10_PARM = "@IsInactiveCombo10";

		private const string COMBO10GROUPID_PARM = "@Combo10GroupID";

		private const string COMBO10LABEL_PARM = "@Combo10Label";

		private const string COMBO10TYPE_PARM = "@Combo10Type";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		public bool CheckConcurrency = true;

		public CustomFieldSetups(Config config)
			: base(config)
		{
		}

		private string GetInsertText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Field Setups]", new FieldValue("TableName", "@TableName"), new FieldValue("CustomType", "@CustomType"), new FieldValue("IsInactiveString1", "@IsInactiveString1"), new FieldValue("String1GroupID", "@String1GroupID"), new FieldValue("String1Label", "@String1Label"), new FieldValue("IsInactiveString2", "@IsInactiveString2"), new FieldValue("String2GroupID", "@String2GroupID"), new FieldValue("String2Label", "@String2Label"), new FieldValue("IsInactiveString3", "@IsInactiveString3"), new FieldValue("String3GroupID", "@String3GroupID"), new FieldValue("String3Label", "@String3Label"), new FieldValue("IsInactiveString4", "@IsInactiveString4"), new FieldValue("String4GroupID", "@String4GroupID"), new FieldValue("String4Label", "@String4Label"), new FieldValue("IsInactiveString5", "@IsInactiveString5"), new FieldValue("String5GroupID", "@String5GroupID"), new FieldValue("String5Label", "@String5Label"), new FieldValue("IsInactiveString6", "@IsInactiveString6"), new FieldValue("String6GroupID", "@String6GroupID"), new FieldValue("String6Label", "@String6Label"), new FieldValue("IsInactiveString7", "@IsInactiveString7"), new FieldValue("String7GroupID", "@String7GroupID"), new FieldValue("String7Label", "@String7Label"), new FieldValue("IsInactiveString8", "@IsInactiveString8"), new FieldValue("String8GroupID", "@String8GroupID"), new FieldValue("String8Label", "@String8Label"), new FieldValue("IsInactiveString9", "@IsInactiveString9"), new FieldValue("String9GroupID", "@String9GroupID"), new FieldValue("String9Label", "@String9Label"), new FieldValue("IsInactiveString10", "@IsInactiveString10"), new FieldValue("String10GroupID", "@String10GroupID"), new FieldValue("String10Label", "@String10Label"), new FieldValue("IsInactiveMemo1", "@IsInactiveMemo1"), new FieldValue("Memo1GroupID", "@Memo1GroupID"), new FieldValue("Memo1Label", "@Memo1Label"), new FieldValue("IsInactiveMemo2", "@IsInactiveMemo2"), new FieldValue("Memo2GroupID", "@Memo2GroupID"), new FieldValue("Memo2Label", "@Memo2Label"), new FieldValue("IsInactiveMemo3", "@IsInactiveMemo3"), new FieldValue("Memo3GroupID", "@Memo3GroupID"), new FieldValue("Memo3Label", "@Memo3Label"), new FieldValue("IsInactiveMemo4", "@IsInactiveMemo4"), new FieldValue("Memo4GroupID", "@Memo4GroupID"), new FieldValue("Memo4Label", "@Memo4Label"), new FieldValue("IsInactiveDecimal1", "@IsInactiveDecimal1"), new FieldValue("Decimal1GroupID", "@Decimal1GroupID"), new FieldValue("Decimal1Label", "@Decimal1Label"), new FieldValue("IsInactiveDecimal2", "@IsInactiveDecimal2"), new FieldValue("Decimal2GroupID", "@Decimal2GroupID"), new FieldValue("Decimal2Label", "@Decimal1Labe2"), new FieldValue("IsInactiveDecimal3", "@IsInactiveDecimal3"), new FieldValue("Decimal3GroupID", "@Decimal3GroupID"), new FieldValue("Decimal3Label", "@Decimal3Label"), new FieldValue("IsInactiveDecimal4", "@IsInactiveDecimal4"), new FieldValue("Decimal4GroupID", "@Decimal4GroupID"), new FieldValue("Decimal4Label", "@Decimal4Label"), new FieldValue("IsInactiveDecimal5", "@IsInactiveDecimal5"), new FieldValue("Decimal5GroupID", "@Decimal5GroupID"), new FieldValue("Decimal5Label", "@Decimal5Label"), new FieldValue("IsInactiveDecimal6", "@IsInactiveDecimal6"), new FieldValue("Decimal6GroupID", "@Decimal6GroupID"), new FieldValue("Decimal6Label", "@Decimal6Label"), new FieldValue("IsInactiveBool1", "@IsInactiveBool1"), new FieldValue("Bool1GroupID", "@Bool1GroupID"), new FieldValue("Bool1Label", "@Bool1Label"), new FieldValue("IsInactiveBool2", "@IsInactiveBool2"), new FieldValue("Bool2GroupID", "@Bool2GroupID"), new FieldValue("Bool2Label", "@Bool2Label"), new FieldValue("IsInactiveBool3", "@IsInactiveBool3"), new FieldValue("Bool3GroupID", "@Bool3GroupID"), new FieldValue("Bool3Label", "@Bool3Label"), new FieldValue("IsInactiveBool4", "@IsInactiveBool4"), new FieldValue("Bool4GroupID", "@Bool4GroupID"), new FieldValue("Bool4Label", "@Bool4Label"), new FieldValue("IsInactiveBool5", "@IsInactiveBool5"), new FieldValue("Bool5GroupID", "@Bool5GroupID"), new FieldValue("Bool5Label", "@Bool5Label"), new FieldValue("IsInactiveBool6", "@IsInactiveBool6"), new FieldValue("Bool6GroupID", "@Bool6GroupID"), new FieldValue("Bool6Label", "@Bool6Label"), new FieldValue("IsInactiveDateTime1", "@IsInactiveDateTime1"), new FieldValue("DateTime1GroupID", "@DateTime1GroupID"), new FieldValue("DateTime1Label", "@DateTime1Label"), new FieldValue("IsInactiveDateTime2", "@IsInactiveDateTime2"), new FieldValue("DateTime2GroupID", "@DateTime2GroupID"), new FieldValue("DateTime2Label", "@DateTime2Label"), new FieldValue("IsInactiveDateTime3", "@IsInactiveDateTime3"), new FieldValue("DateTime3GroupID", "@DateTime3GroupID"), new FieldValue("DateTime3Label", "@DateTime3Label"), new FieldValue("IsInactiveDateTime4", "@IsInactiveDateTime4"), new FieldValue("DateTime4GroupID", "@DateTime4GroupID"), new FieldValue("DateTime4Label", "@DateTime4Label"), new FieldValue("IsInactiveDateTime5", "@IsInactiveDateTime5"), new FieldValue("DateTime5GroupID", "@DateTime5GroupID"), new FieldValue("DateTime5Label", "@DateTime5Label"), new FieldValue("IsInactiveDateTime6", "@IsInactiveDateTime6"), new FieldValue("DateTime6GroupID", "@DateTime6GroupID"), new FieldValue("DateTime6Label", "@DateTime6Label"), new FieldValue("IsInactiveCombo1", "@IsInactiveCombo1"), new FieldValue("Combo1GroupID", "@Combo1GroupID"), new FieldValue("Combo1Label", "@Combo1Label"), new FieldValue("Combo1Type", "@Combo1Type"), new FieldValue("IsInactiveCombo2", "@IsInactiveCombo2"), new FieldValue("Combo2GroupID", "@Combo2GroupID"), new FieldValue("Combo2Label", "@Combo2Label"), new FieldValue("Combo2Type", "@Combo2Type"), new FieldValue("IsInactiveCombo3", "@IsInactiveCombo3"), new FieldValue("Combo3GroupID", "@Combo3GroupID"), new FieldValue("Combo3Label", "@Combo3Label"), new FieldValue("Combo3Type", "@Combo3Type"), new FieldValue("IsInactiveCombo4", "@IsInactiveCombo4"), new FieldValue("Combo4GroupID", "@Combo4GroupID"), new FieldValue("Combo4Label", "@Combo4Label"), new FieldValue("Combo4Type", "@Combo4Type"), new FieldValue("IsInactiveCombo5", "@IsInactiveCombo5"), new FieldValue("Combo5GroupID", "@Combo5GroupID"), new FieldValue("Combo5Label", "@Combo5Label"), new FieldValue("Combo5Type", "@Combo5Type"), new FieldValue("IsInactiveCombo6", "@IsInactiveCombo6"), new FieldValue("Combo6GroupID", "@Combo6GroupID"), new FieldValue("Combo6Label", "@Combo6Label"), new FieldValue("Combo6Type", "@Combo6Type"), new FieldValue("IsInactiveCombo7", "@IsInactiveCombo7"), new FieldValue("Combo7GroupID", "@Combo7GroupID"), new FieldValue("Combo7Label", "@Combo7Label"), new FieldValue("Combo7Type", "@Combo7Type"), new FieldValue("IsInactiveCombo8", "@IsInactiveCombo8"), new FieldValue("Combo8GroupID", "@Combo8GroupID"), new FieldValue("Combo8Label", "@Combo8Label"), new FieldValue("Combo8Type", "@Combo8Type"), new FieldValue("IsInactiveCombo9", "@IsInactiveCombo9"), new FieldValue("Combo9GroupID", "@Combo9GroupID"), new FieldValue("Combo9Label", "@Combo9Label"), new FieldValue("Combo9Type", "@Combo9Type"), new FieldValue("IsInactiveCombo10", "@IsInactiveCombo10"), new FieldValue("Combo10GroupID", "@Combo10GroupID"), new FieldValue("Combo10Label", "@Combo10Label"), new FieldValue("Combo10Type", "@Combo10Type"));
			return sqlBuilder.GetInsertExpression();
		}

		private string GetUpdateText()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("[Custom Field Setups]", new FieldValue("CustomFieldSetupID", "@CustomFieldSetupID", isUpdateConditionField: true), new FieldValue("IsInactiveString1", "@IsInactiveString1"), new FieldValue("String1GroupID", "@String1GroupID"), new FieldValue("String1Label", "@String1Label"), new FieldValue("IsInactiveString2", "@IsInactiveString2"), new FieldValue("String2GroupID", "@String2GroupID"), new FieldValue("String2Label", "@String2Label"), new FieldValue("IsInactiveString3", "@IsInactiveString3"), new FieldValue("String3GroupID", "@String3GroupID"), new FieldValue("String3Label", "@String3Label"), new FieldValue("IsInactiveString4", "@IsInactiveString4"), new FieldValue("String4GroupID", "@String4GroupID"), new FieldValue("String4Label", "@String4Label"), new FieldValue("IsInactiveString5", "@IsInactiveString5"), new FieldValue("String5GroupID", "@String5GroupID"), new FieldValue("String5Label", "@String5Label"), new FieldValue("IsInactiveString6", "@IsInactiveString6"), new FieldValue("String6GroupID", "@String6GroupID"), new FieldValue("String6Label", "@String6Label"), new FieldValue("IsInactiveString7", "@IsInactiveString7"), new FieldValue("String7GroupID", "@String7GroupID"), new FieldValue("String7Label", "@String7Label"), new FieldValue("IsInactiveString8", "@IsInactiveString8"), new FieldValue("String8GroupID", "@String8GroupID"), new FieldValue("String8Label", "@String8Label"), new FieldValue("IsInactiveString9", "@IsInactiveString9"), new FieldValue("String9GroupID", "@String9GroupID"), new FieldValue("String9Label", "@String9Label"), new FieldValue("IsInactiveString10", "@IsInactiveString10"), new FieldValue("String10GroupID", "@String10GroupID"), new FieldValue("String10Label", "@String10Label"), new FieldValue("IsInactiveMemo1", "@IsInactiveMemo1"), new FieldValue("Memo1GroupID", "@Memo1GroupID"), new FieldValue("Memo1Label", "@Memo1Label"), new FieldValue("IsInactiveMemo2", "@IsInactiveMemo2"), new FieldValue("Memo2GroupID", "@Memo2GroupID"), new FieldValue("Memo2Label", "@Memo2Label"), new FieldValue("IsInactiveMemo3", "@IsInactiveMemo3"), new FieldValue("Memo3GroupID", "@Memo3GroupID"), new FieldValue("Memo3Label", "@Memo3Label"), new FieldValue("IsInactiveMemo4", "@IsInactiveMemo4"), new FieldValue("Memo4GroupID", "@Memo4GroupID"), new FieldValue("Memo4Label", "@Memo4Label"), new FieldValue("IsInactiveDecimal1", "@IsInactiveDecimal1"), new FieldValue("Decimal1GroupID", "@Decimal1GroupID"), new FieldValue("Decimal1Label", "@Decimal1Label"), new FieldValue("IsInactiveDecimal2", "@IsInactiveDecimal2"), new FieldValue("Decimal2GroupID", "@Decimal2GroupID"), new FieldValue("Decimal2Label", "@Decimal1Labe2"), new FieldValue("IsInactiveDecimal3", "@IsInactiveDecimal3"), new FieldValue("Decimal3GroupID", "@Decimal3GroupID"), new FieldValue("Decimal3Label", "@Decimal3Label"), new FieldValue("IsInactiveDecimal4", "@IsInactiveDecimal4"), new FieldValue("Decimal4GroupID", "@Decimal4GroupID"), new FieldValue("Decimal4Label", "@Decimal4Label"), new FieldValue("IsInactiveDecimal5", "@IsInactiveDecimal5"), new FieldValue("Decimal5GroupID", "@Decimal5GroupID"), new FieldValue("Decimal5Label", "@Decimal5Label"), new FieldValue("IsInactiveDecimal6", "@IsInactiveDecimal6"), new FieldValue("Decimal6GroupID", "@Decimal6GroupID"), new FieldValue("Decimal6Label", "@Decimal6Label"), new FieldValue("IsInactiveBool1", "@IsInactiveBool1"), new FieldValue("Bool1GroupID", "@Bool1GroupID"), new FieldValue("Bool1Label", "@Bool1Label"), new FieldValue("IsInactiveBool2", "@IsInactiveBool2"), new FieldValue("Bool2GroupID", "@Bool2GroupID"), new FieldValue("Bool2Label", "@Bool2Label"), new FieldValue("IsInactiveBool3", "@IsInactiveBool3"), new FieldValue("Bool3GroupID", "@Bool3GroupID"), new FieldValue("Bool3Label", "@Bool3Label"), new FieldValue("IsInactiveBool4", "@IsInactiveBool4"), new FieldValue("Bool4GroupID", "@Bool4GroupID"), new FieldValue("Bool4Label", "@Bool4Label"), new FieldValue("IsInactiveBool5", "@IsInactiveBool5"), new FieldValue("Bool5GroupID", "@Bool5GroupID"), new FieldValue("Bool5Label", "@Bool5Label"), new FieldValue("IsInactiveBool6", "@IsInactiveBool6"), new FieldValue("Bool6GroupID", "@Bool6GroupID"), new FieldValue("Bool6Label", "@Bool6Label"), new FieldValue("IsInactiveDateTime1", "@IsInactiveDateTime1"), new FieldValue("DateTime1GroupID", "@DateTime1GroupID"), new FieldValue("DateTime1Label", "@DateTime1Label"), new FieldValue("IsInactiveDateTime2", "@IsInactiveDateTime2"), new FieldValue("DateTime2GroupID", "@DateTime2GroupID"), new FieldValue("DateTime2Label", "@DateTime2Label"), new FieldValue("IsInactiveDateTime3", "@IsInactiveDateTime3"), new FieldValue("DateTime3GroupID", "@DateTime3GroupID"), new FieldValue("DateTime3Label", "@DateTime3Label"), new FieldValue("IsInactiveDateTime4", "@IsInactiveDateTime4"), new FieldValue("DateTime4GroupID", "@DateTime4GroupID"), new FieldValue("DateTime4Label", "@DateTime4Label"), new FieldValue("IsInactiveDateTime5", "@IsInactiveDateTime5"), new FieldValue("DateTime5GroupID", "@DateTime5GroupID"), new FieldValue("DateTime5Label", "@DateTime5Label"), new FieldValue("IsInactiveDateTime6", "@IsInactiveDateTime6"), new FieldValue("DateTime6GroupID", "@DateTime6GroupID"), new FieldValue("DateTime6Label", "@DateTime6Label"), new FieldValue("IsInactiveCombo1", "@IsInactiveCombo1"), new FieldValue("Combo1GroupID", "@Combo1GroupID"), new FieldValue("Combo1Label", "@Combo1Label"), new FieldValue("Combo1Type", "@Combo1Type"), new FieldValue("IsInactiveCombo2", "@IsInactiveCombo2"), new FieldValue("Combo2GroupID", "@Combo2GroupID"), new FieldValue("Combo2Label", "@Combo2Label"), new FieldValue("Combo2Type", "@Combo2Type"), new FieldValue("IsInactiveCombo3", "@IsInactiveCombo3"), new FieldValue("Combo3GroupID", "@Combo3GroupID"), new FieldValue("Combo3Label", "@Combo3Label"), new FieldValue("Combo3Type", "@Combo3Type"), new FieldValue("IsInactiveCombo4", "@IsInactiveCombo4"), new FieldValue("Combo4GroupID", "@Combo4GroupID"), new FieldValue("Combo4Label", "@Combo4Label"), new FieldValue("Combo4Type", "@Combo4Type"), new FieldValue("IsInactiveCombo5", "@IsInactiveCombo5"), new FieldValue("Combo5GroupID", "@Combo5GroupID"), new FieldValue("Combo5Label", "@Combo5Label"), new FieldValue("Combo5Type", "@Combo5Type"), new FieldValue("IsInactiveCombo6", "@IsInactiveCombo6"), new FieldValue("Combo6GroupID", "@Combo6GroupID"), new FieldValue("Combo6Label", "@Combo6Label"), new FieldValue("Combo6Type", "@Combo6Type"), new FieldValue("IsInactiveCombo7", "@IsInactiveCombo7"), new FieldValue("Combo7GroupID", "@Combo7GroupID"), new FieldValue("Combo7Label", "@Combo7Label"), new FieldValue("Combo7Type", "@Combo7Type"), new FieldValue("IsInactiveCombo8", "@IsInactiveCombo8"), new FieldValue("Combo8GroupID", "@Combo8GroupID"), new FieldValue("Combo8Label", "@Combo8Label"), new FieldValue("Combo8Type", "@Combo8Type"), new FieldValue("IsInactiveCombo9", "@IsInactiveCombo9"), new FieldValue("Combo9GroupID", "@Combo9GroupID"), new FieldValue("Combo9Label", "@Combo9Label"), new FieldValue("Combo9Type", "@Combo9Type"), new FieldValue("IsInactiveCombo10", "@IsInactiveCombo10"), new FieldValue("Combo10GroupID", "@Combo10GroupID"), new FieldValue("Combo10Label", "@Combo10Label"), new FieldValue("Combo10Type", "@Combo10Type"));
			if (CheckConcurrency)
			{
				sqlBuilder.AddInsertUpdateParameters("[Custom Field Setups]", new FieldValue("DateTimeStamp", "@DateTimeStamp", isUpdateConditionField: true, checkForNullValue: true));
			}
			return sqlBuilder.GetUpdateExpression();
		}

		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetInsertText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@TableName", SqlDbType.NVarChar);
				parameters.Add("@CustomType", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveString1", SqlDbType.Bit);
				parameters.Add("@String1GroupID", SqlDbType.Int);
				parameters.Add("@String1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString2", SqlDbType.Bit);
				parameters.Add("@String2GroupID", SqlDbType.Int);
				parameters.Add("@String2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString3", SqlDbType.Bit);
				parameters.Add("@String3GroupID", SqlDbType.Int);
				parameters.Add("@String3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString4", SqlDbType.Bit);
				parameters.Add("@String4GroupID", SqlDbType.Int);
				parameters.Add("@String4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString5", SqlDbType.Bit);
				parameters.Add("@String5GroupID", SqlDbType.Int);
				parameters.Add("@String5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString6", SqlDbType.Bit);
				parameters.Add("@String6GroupID", SqlDbType.Int);
				parameters.Add("@String6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString7", SqlDbType.Bit);
				parameters.Add("@String7GroupID", SqlDbType.Int);
				parameters.Add("@String7Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString8", SqlDbType.Bit);
				parameters.Add("@String8GroupID", SqlDbType.Int);
				parameters.Add("@String8Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString9", SqlDbType.Bit);
				parameters.Add("@String9GroupID", SqlDbType.Int);
				parameters.Add("@String9Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString10", SqlDbType.Bit);
				parameters.Add("@String10GroupID", SqlDbType.Int);
				parameters.Add("@String10Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo1", SqlDbType.Bit);
				parameters.Add("@Memo1GroupID", SqlDbType.Int);
				parameters.Add("@Memo1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo2", SqlDbType.Bit);
				parameters.Add("@Memo2GroupID", SqlDbType.Int);
				parameters.Add("@Memo2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo3", SqlDbType.Bit);
				parameters.Add("@Memo3GroupID", SqlDbType.Int);
				parameters.Add("@Memo3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo4", SqlDbType.Bit);
				parameters.Add("@Memo4GroupID", SqlDbType.Int);
				parameters.Add("@Memo4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal1", SqlDbType.Bit);
				parameters.Add("@Decimal1GroupID", SqlDbType.Int);
				parameters.Add("@Decimal1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal2", SqlDbType.Bit);
				parameters.Add("@Decimal2GroupID", SqlDbType.Int);
				parameters.Add("@Decimal1Labe2", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal3", SqlDbType.Bit);
				parameters.Add("@Decimal3GroupID", SqlDbType.Int);
				parameters.Add("@Decimal3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal4", SqlDbType.Bit);
				parameters.Add("@Decimal4GroupID", SqlDbType.Int);
				parameters.Add("@Decimal4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal5", SqlDbType.Bit);
				parameters.Add("@Decimal5GroupID", SqlDbType.Int);
				parameters.Add("@Decimal5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal6", SqlDbType.Bit);
				parameters.Add("@Decimal6GroupID", SqlDbType.Int);
				parameters.Add("@Decimal6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool1", SqlDbType.Bit);
				parameters.Add("@Bool1GroupID", SqlDbType.Int);
				parameters.Add("@Bool1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool2", SqlDbType.Bit);
				parameters.Add("@Bool2GroupID", SqlDbType.Int);
				parameters.Add("@Bool2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool3", SqlDbType.Bit);
				parameters.Add("@Bool3GroupID", SqlDbType.Int);
				parameters.Add("@Bool3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool4", SqlDbType.Bit);
				parameters.Add("@Bool4GroupID", SqlDbType.Int);
				parameters.Add("@Bool4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool5", SqlDbType.Bit);
				parameters.Add("@Bool5GroupID", SqlDbType.Int);
				parameters.Add("@Bool5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool6", SqlDbType.Bit);
				parameters.Add("@Bool6GroupID", SqlDbType.Int);
				parameters.Add("@Bool6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime1", SqlDbType.Bit);
				parameters.Add("@DateTime1GroupID", SqlDbType.Int);
				parameters.Add("@DateTime1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime2", SqlDbType.Bit);
				parameters.Add("@DateTime2GroupID", SqlDbType.Int);
				parameters.Add("@DateTime2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime3", SqlDbType.Bit);
				parameters.Add("@DateTime3GroupID", SqlDbType.Int);
				parameters.Add("@DateTime3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime4", SqlDbType.Bit);
				parameters.Add("@DateTime4GroupID", SqlDbType.Int);
				parameters.Add("@DateTime4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime5", SqlDbType.Bit);
				parameters.Add("@DateTime5GroupID", SqlDbType.Int);
				parameters.Add("@DateTime5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime6", SqlDbType.Bit);
				parameters.Add("@DateTime6GroupID", SqlDbType.Int);
				parameters.Add("@DateTime6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveCombo1", SqlDbType.Bit);
				parameters.Add("@Combo1GroupID", SqlDbType.Int);
				parameters.Add("@Combo1Label", SqlDbType.NVarChar);
				parameters.Add("@Combo1Type", SqlDbType.Bit);
				parameters.Add("@IsInactiveCombo2", SqlDbType.Bit);
				parameters.Add("@Combo2GroupID", SqlDbType.Int);
				parameters.Add("@Combo2Label", SqlDbType.NVarChar);
				parameters.Add("@Combo2Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo3", SqlDbType.Bit);
				parameters.Add("@Combo3GroupID", SqlDbType.Int);
				parameters.Add("@Combo3Label", SqlDbType.NVarChar);
				parameters.Add("@Combo3Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo4", SqlDbType.Bit);
				parameters.Add("@Combo4GroupID", SqlDbType.Int);
				parameters.Add("@Combo4Label", SqlDbType.NVarChar);
				parameters.Add("@Combo4Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo5", SqlDbType.Bit);
				parameters.Add("@Combo5GroupID", SqlDbType.Int);
				parameters.Add("@Combo5Label", SqlDbType.NVarChar);
				parameters.Add("@Combo5Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo6", SqlDbType.Bit);
				parameters.Add("@Combo6GroupID", SqlDbType.Int);
				parameters.Add("@Combo6Label", SqlDbType.NVarChar);
				parameters.Add("@Combo6Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo7", SqlDbType.Bit);
				parameters.Add("@Combo7GroupID", SqlDbType.Int);
				parameters.Add("@Combo7Label", SqlDbType.NVarChar);
				parameters.Add("@Combo7Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo8", SqlDbType.Bit);
				parameters.Add("@Combo8GroupID", SqlDbType.Int);
				parameters.Add("@Combo8Label", SqlDbType.NVarChar);
				parameters.Add("@Combo8Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo9", SqlDbType.Bit);
				parameters.Add("@Combo9GroupID", SqlDbType.Int);
				parameters.Add("@Combo9Label", SqlDbType.NVarChar);
				parameters.Add("@Combo9Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo10", SqlDbType.Bit);
				parameters.Add("@Combo10GroupID", SqlDbType.Int);
				parameters.Add("@Combo10Label", SqlDbType.NVarChar);
				parameters.Add("@Combo10Type", SqlDbType.TinyInt);
				parameters["@TableName"].SourceColumn = "TableName";
				parameters["@CustomType"].SourceColumn = "CustomType";
				parameters["@IsInactiveString1"].SourceColumn = "IsInactiveString1";
				parameters["@String1GroupID"].SourceColumn = "String1GroupID";
				parameters["@String1Label"].SourceColumn = "String1Label";
				parameters["@IsInactiveString2"].SourceColumn = "IsInactiveString2";
				parameters["@String2GroupID"].SourceColumn = "String2GroupID";
				parameters["@String2Label"].SourceColumn = "String2Label";
				parameters["@IsInactiveString3"].SourceColumn = "IsInactiveString3";
				parameters["@String3GroupID"].SourceColumn = "String3GroupID";
				parameters["@String3Label"].SourceColumn = "String3Label";
				parameters["@IsInactiveString4"].SourceColumn = "IsInactiveString4";
				parameters["@String4GroupID"].SourceColumn = "String4GroupID";
				parameters["@String4Label"].SourceColumn = "String4Label";
				parameters["@IsInactiveString5"].SourceColumn = "IsInactiveString5";
				parameters["@String5GroupID"].SourceColumn = "String5GroupID";
				parameters["@String5Label"].SourceColumn = "String5Label";
				parameters["@IsInactiveString6"].SourceColumn = "IsInactiveString6";
				parameters["@String6GroupID"].SourceColumn = "String6GroupID";
				parameters["@String6Label"].SourceColumn = "String6Label";
				parameters["@IsInactiveString7"].SourceColumn = "IsInactiveString7";
				parameters["@String7GroupID"].SourceColumn = "String7GroupID";
				parameters["@String7Label"].SourceColumn = "String7Label";
				parameters["@IsInactiveString8"].SourceColumn = "IsInactiveString8";
				parameters["@String8GroupID"].SourceColumn = "String8GroupID";
				parameters["@String8Label"].SourceColumn = "String8Label";
				parameters["@IsInactiveString9"].SourceColumn = "IsInactiveString9";
				parameters["@String9GroupID"].SourceColumn = "String9GroupID";
				parameters["@String9Label"].SourceColumn = "String9Label";
				parameters["@IsInactiveString10"].SourceColumn = "IsInactiveString10";
				parameters["@String10GroupID"].SourceColumn = "String10GroupID";
				parameters["@String10Label"].SourceColumn = "String10Label";
				parameters["@IsInactiveMemo1"].SourceColumn = "IsInactiveMemo1";
				parameters["@Memo1GroupID"].SourceColumn = "Memo1GroupID";
				parameters["@Memo1Label"].SourceColumn = "Memo1Label";
				parameters["@IsInactiveMemo2"].SourceColumn = "IsInactiveMemo2";
				parameters["@Memo2GroupID"].SourceColumn = "Memo2GroupID";
				parameters["@Memo2Label"].SourceColumn = "Memo2Label";
				parameters["@IsInactiveMemo3"].SourceColumn = "IsInactiveMemo3";
				parameters["@Memo3GroupID"].SourceColumn = "Memo3GroupID";
				parameters["@Memo3Label"].SourceColumn = "Memo3Label";
				parameters["@IsInactiveMemo4"].SourceColumn = "IsInactiveMemo4";
				parameters["@Memo4GroupID"].SourceColumn = "Memo4GroupID";
				parameters["@Memo4Label"].SourceColumn = "Memo4Label";
				parameters["@IsInactiveDecimal1"].SourceColumn = "IsInactiveDecimal1";
				parameters["@Decimal1GroupID"].SourceColumn = "Decimal1GroupID";
				parameters["@Decimal1Label"].SourceColumn = "Decimal1Label";
				parameters["@IsInactiveDecimal2"].SourceColumn = "IsInactiveDecimal2";
				parameters["@Decimal2GroupID"].SourceColumn = "Decimal2GroupID";
				parameters["@Decimal1Labe2"].SourceColumn = "Decimal2Label";
				parameters["@IsInactiveDecimal3"].SourceColumn = "IsInactiveDecimal3";
				parameters["@Decimal3GroupID"].SourceColumn = "Decimal3GroupID";
				parameters["@Decimal3Label"].SourceColumn = "Decimal3Label";
				parameters["@IsInactiveDecimal4"].SourceColumn = "IsInactiveDecimal4";
				parameters["@Decimal4GroupID"].SourceColumn = "Decimal4GroupID";
				parameters["@Decimal4Label"].SourceColumn = "Decimal4Label";
				parameters["@IsInactiveDecimal5"].SourceColumn = "IsInactiveDecimal5";
				parameters["@Decimal5GroupID"].SourceColumn = "Decimal5GroupID";
				parameters["@Decimal5Label"].SourceColumn = "Decimal5Label";
				parameters["@IsInactiveDecimal6"].SourceColumn = "IsInactiveDecimal6";
				parameters["@Decimal6GroupID"].SourceColumn = "Decimal6GroupID";
				parameters["@Decimal6Label"].SourceColumn = "Decimal6Label";
				parameters["@IsInactiveBool1"].SourceColumn = "IsInactiveBool1";
				parameters["@Bool1GroupID"].SourceColumn = "Bool1GroupID";
				parameters["@Bool1Label"].SourceColumn = "Bool1Label";
				parameters["@IsInactiveBool2"].SourceColumn = "IsInactiveBool2";
				parameters["@Bool2GroupID"].SourceColumn = "Bool2GroupID";
				parameters["@Bool2Label"].SourceColumn = "Bool2Label";
				parameters["@IsInactiveBool3"].SourceColumn = "IsInactiveBool3";
				parameters["@Bool3GroupID"].SourceColumn = "Bool3GroupID";
				parameters["@Bool3Label"].SourceColumn = "Bool3Label";
				parameters["@IsInactiveBool4"].SourceColumn = "IsInactiveBool4";
				parameters["@Bool4GroupID"].SourceColumn = "Bool4GroupID";
				parameters["@Bool4Label"].SourceColumn = "Bool4Label";
				parameters["@IsInactiveBool5"].SourceColumn = "IsInactiveBool5";
				parameters["@Bool5GroupID"].SourceColumn = "Bool5GroupID";
				parameters["@Bool5Label"].SourceColumn = "Bool5Label";
				parameters["@IsInactiveBool6"].SourceColumn = "IsInactiveBool6";
				parameters["@Bool6GroupID"].SourceColumn = "Bool6GroupID";
				parameters["@Bool6Label"].SourceColumn = "Bool6Label";
				parameters["@IsInactiveDateTime1"].SourceColumn = "IsInactiveDateTime1";
				parameters["@DateTime1GroupID"].SourceColumn = "DateTime1GroupID";
				parameters["@DateTime1Label"].SourceColumn = "DateTime1Label";
				parameters["@IsInactiveDateTime2"].SourceColumn = "IsInactiveDateTime2";
				parameters["@DateTime2GroupID"].SourceColumn = "DateTime2GroupID";
				parameters["@DateTime2Label"].SourceColumn = "DateTime2Label";
				parameters["@IsInactiveDateTime3"].SourceColumn = "IsInactiveDateTime3";
				parameters["@DateTime3GroupID"].SourceColumn = "DateTime3GroupID";
				parameters["@DateTime3Label"].SourceColumn = "DateTime3Label";
				parameters["@IsInactiveDateTime4"].SourceColumn = "IsInactiveDateTime4";
				parameters["@DateTime4GroupID"].SourceColumn = "DateTime4GroupID";
				parameters["@DateTime4Label"].SourceColumn = "DateTime4Label";
				parameters["@IsInactiveDateTime5"].SourceColumn = "IsInactiveDateTime5";
				parameters["@DateTime5GroupID"].SourceColumn = "DateTime5GroupID";
				parameters["@DateTime5Label"].SourceColumn = "DateTime5Label";
				parameters["@IsInactiveDateTime6"].SourceColumn = "IsInactiveDateTime6";
				parameters["@DateTime6GroupID"].SourceColumn = "DateTime6GroupID";
				parameters["@DateTime6Label"].SourceColumn = "DateTime6Label";
				parameters["@IsInactiveCombo1"].SourceColumn = "IsInactiveCombo1";
				parameters["@Combo1GroupID"].SourceColumn = "Combo1GroupID";
				parameters["@Combo1Label"].SourceColumn = "Combo1Label";
				parameters["@Combo1Type"].SourceColumn = "Combo1Type";
				parameters["@IsInactiveCombo2"].SourceColumn = "IsInactiveCombo2";
				parameters["@Combo2GroupID"].SourceColumn = "Combo2GroupID";
				parameters["@Combo2Label"].SourceColumn = "Combo2Label";
				parameters["@Combo2Type"].SourceColumn = "Combo2Type";
				parameters["@IsInactiveCombo3"].SourceColumn = "IsInactiveCombo3";
				parameters["@Combo3GroupID"].SourceColumn = "Combo3GroupID";
				parameters["@Combo3Label"].SourceColumn = "Combo3Label";
				parameters["@Combo3Type"].SourceColumn = "Combo3Type";
				parameters["@IsInactiveCombo4"].SourceColumn = "IsInactiveCombo4";
				parameters["@Combo4GroupID"].SourceColumn = "Combo4GroupID";
				parameters["@Combo4Label"].SourceColumn = "Combo4Label";
				parameters["@Combo4Type"].SourceColumn = "Combo4Type";
				parameters["@IsInactiveCombo5"].SourceColumn = "IsInactiveCombo5";
				parameters["@Combo5GroupID"].SourceColumn = "Combo5GroupID";
				parameters["@Combo5Label"].SourceColumn = "Combo5Label";
				parameters["@Combo5Type"].SourceColumn = "Combo5Type";
				parameters["@IsInactiveCombo6"].SourceColumn = "IsInactiveCombo6";
				parameters["@Combo6GroupID"].SourceColumn = "Combo6GroupID";
				parameters["@Combo6Label"].SourceColumn = "Combo6Label";
				parameters["@Combo6Type"].SourceColumn = "Combo6Type";
				parameters["@IsInactiveCombo7"].SourceColumn = "IsInactiveCombo7";
				parameters["@Combo7GroupID"].SourceColumn = "Combo7GroupID";
				parameters["@Combo7Label"].SourceColumn = "Combo7Label";
				parameters["@Combo7Type"].SourceColumn = "Combo7Type";
				parameters["@IsInactiveCombo8"].SourceColumn = "IsInactiveCombo8";
				parameters["@Combo8GroupID"].SourceColumn = "Combo8GroupID";
				parameters["@Combo8Label"].SourceColumn = "Combo8Label";
				parameters["@Combo8Type"].SourceColumn = "Combo8Type";
				parameters["@IsInactiveCombo9"].SourceColumn = "IsInactiveCombo9";
				parameters["@Combo9GroupID"].SourceColumn = "Combo9GroupID";
				parameters["@Combo9Label"].SourceColumn = "Combo9Label";
				parameters["@Combo9Type"].SourceColumn = "Combo9Type";
				parameters["@IsInactiveCombo10"].SourceColumn = "IsInactiveCombo10";
				parameters["@Combo10GroupID"].SourceColumn = "Combo10GroupID";
				parameters["@Combo10Label"].SourceColumn = "Combo10Label";
				parameters["@Combo10Type"].SourceColumn = "Combo10Type";
			}
			return insertCommand;
		}

		private SqlCommand GetUpdateCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand(GetUpdateText(), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = insertCommand.Parameters;
				parameters.Add("@CustomFieldSetupID", SqlDbType.Int);
				parameters.Add("@IsInactiveString1", SqlDbType.Bit);
				parameters.Add("@String1GroupID", SqlDbType.Int);
				parameters.Add("@String1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString2", SqlDbType.Bit);
				parameters.Add("@String2GroupID", SqlDbType.Int);
				parameters.Add("@String2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString3", SqlDbType.Bit);
				parameters.Add("@String3GroupID", SqlDbType.Int);
				parameters.Add("@String3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString4", SqlDbType.Bit);
				parameters.Add("@String4GroupID", SqlDbType.Int);
				parameters.Add("@String4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString5", SqlDbType.Bit);
				parameters.Add("@String5GroupID", SqlDbType.Int);
				parameters.Add("@String5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString6", SqlDbType.Bit);
				parameters.Add("@String6GroupID", SqlDbType.Int);
				parameters.Add("@String6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString7", SqlDbType.Bit);
				parameters.Add("@String7GroupID", SqlDbType.Int);
				parameters.Add("@String7Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString8", SqlDbType.Bit);
				parameters.Add("@String8GroupID", SqlDbType.Int);
				parameters.Add("@String8Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString9", SqlDbType.Bit);
				parameters.Add("@String9GroupID", SqlDbType.Int);
				parameters.Add("@String9Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveString10", SqlDbType.Bit);
				parameters.Add("@String10GroupID", SqlDbType.Int);
				parameters.Add("@String10Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo1", SqlDbType.Bit);
				parameters.Add("@Memo1GroupID", SqlDbType.Int);
				parameters.Add("@Memo1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo2", SqlDbType.Bit);
				parameters.Add("@Memo2GroupID", SqlDbType.Int);
				parameters.Add("@Memo2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo3", SqlDbType.Bit);
				parameters.Add("@Memo3GroupID", SqlDbType.Int);
				parameters.Add("@Memo3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveMemo4", SqlDbType.Bit);
				parameters.Add("@Memo4GroupID", SqlDbType.Int);
				parameters.Add("@Memo4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal1", SqlDbType.Bit);
				parameters.Add("@Decimal1GroupID", SqlDbType.Int);
				parameters.Add("@Decimal1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal2", SqlDbType.Bit);
				parameters.Add("@Decimal2GroupID", SqlDbType.Int);
				parameters.Add("@Decimal1Labe2", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal3", SqlDbType.Bit);
				parameters.Add("@Decimal3GroupID", SqlDbType.Int);
				parameters.Add("@Decimal3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal4", SqlDbType.Bit);
				parameters.Add("@Decimal4GroupID", SqlDbType.Int);
				parameters.Add("@Decimal4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal5", SqlDbType.Bit);
				parameters.Add("@Decimal5GroupID", SqlDbType.Int);
				parameters.Add("@Decimal5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDecimal6", SqlDbType.Bit);
				parameters.Add("@Decimal6GroupID", SqlDbType.Int);
				parameters.Add("@Decimal6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool1", SqlDbType.Bit);
				parameters.Add("@Bool1GroupID", SqlDbType.Int);
				parameters.Add("@Bool1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool2", SqlDbType.Bit);
				parameters.Add("@Bool2GroupID", SqlDbType.Int);
				parameters.Add("@Bool2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool3", SqlDbType.Bit);
				parameters.Add("@Bool3GroupID", SqlDbType.Int);
				parameters.Add("@Bool3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool4", SqlDbType.Bit);
				parameters.Add("@Bool4GroupID", SqlDbType.Int);
				parameters.Add("@Bool4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool5", SqlDbType.Bit);
				parameters.Add("@Bool5GroupID", SqlDbType.Int);
				parameters.Add("@Bool5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveBool6", SqlDbType.Bit);
				parameters.Add("@Bool6GroupID", SqlDbType.Int);
				parameters.Add("@Bool6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime1", SqlDbType.Bit);
				parameters.Add("@DateTime1GroupID", SqlDbType.Int);
				parameters.Add("@DateTime1Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime2", SqlDbType.Bit);
				parameters.Add("@DateTime2GroupID", SqlDbType.Int);
				parameters.Add("@DateTime2Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime3", SqlDbType.Bit);
				parameters.Add("@DateTime3GroupID", SqlDbType.Int);
				parameters.Add("@DateTime3Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime4", SqlDbType.Bit);
				parameters.Add("@DateTime4GroupID", SqlDbType.Int);
				parameters.Add("@DateTime4Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime5", SqlDbType.Bit);
				parameters.Add("@DateTime5GroupID", SqlDbType.Int);
				parameters.Add("@DateTime5Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveDateTime6", SqlDbType.Bit);
				parameters.Add("@DateTime6GroupID", SqlDbType.Int);
				parameters.Add("@DateTime6Label", SqlDbType.NVarChar);
				parameters.Add("@IsInactiveCombo1", SqlDbType.Bit);
				parameters.Add("@Combo1GroupID", SqlDbType.Int);
				parameters.Add("@Combo1Label", SqlDbType.NVarChar);
				parameters.Add("@Combo1Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo2", SqlDbType.Bit);
				parameters.Add("@Combo2GroupID", SqlDbType.Int);
				parameters.Add("@Combo2Label", SqlDbType.NVarChar);
				parameters.Add("@Combo2Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo3", SqlDbType.Bit);
				parameters.Add("@Combo3GroupID", SqlDbType.Int);
				parameters.Add("@Combo3Label", SqlDbType.NVarChar);
				parameters.Add("@Combo3Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo4", SqlDbType.Bit);
				parameters.Add("@Combo4GroupID", SqlDbType.Int);
				parameters.Add("@Combo4Label", SqlDbType.NVarChar);
				parameters.Add("@Combo4Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo5", SqlDbType.Bit);
				parameters.Add("@Combo5GroupID", SqlDbType.Int);
				parameters.Add("@Combo5Label", SqlDbType.NVarChar);
				parameters.Add("@Combo5Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo6", SqlDbType.Bit);
				parameters.Add("@Combo6GroupID", SqlDbType.Int);
				parameters.Add("@Combo6Label", SqlDbType.NVarChar);
				parameters.Add("@Combo6Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo7", SqlDbType.Bit);
				parameters.Add("@Combo7GroupID", SqlDbType.Int);
				parameters.Add("@Combo7Label", SqlDbType.NVarChar);
				parameters.Add("@Combo7Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo8", SqlDbType.Bit);
				parameters.Add("@Combo8GroupID", SqlDbType.Int);
				parameters.Add("@Combo8Label", SqlDbType.NVarChar);
				parameters.Add("@Combo8Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo9", SqlDbType.Bit);
				parameters.Add("@Combo9GroupID", SqlDbType.Int);
				parameters.Add("@Combo9Label", SqlDbType.NVarChar);
				parameters.Add("@Combo9Type", SqlDbType.TinyInt);
				parameters.Add("@IsInactiveCombo10", SqlDbType.Bit);
				parameters.Add("@Combo10GroupID", SqlDbType.Int);
				parameters.Add("@Combo10Label", SqlDbType.NVarChar);
				parameters.Add("@Combo10Type", SqlDbType.TinyInt);
				parameters.Add("@DateTimeStamp", SqlDbType.DateTime);
				parameters["@CustomFieldSetupID"].SourceColumn = "CustomFieldSetupID";
				parameters["@IsInactiveString1"].SourceColumn = "IsInactiveString1";
				parameters["@String1GroupID"].SourceColumn = "String1GroupID";
				parameters["@String1Label"].SourceColumn = "String1Label";
				parameters["@IsInactiveString2"].SourceColumn = "IsInactiveString2";
				parameters["@String2GroupID"].SourceColumn = "String2GroupID";
				parameters["@String2Label"].SourceColumn = "String2Label";
				parameters["@IsInactiveString3"].SourceColumn = "IsInactiveString3";
				parameters["@String3GroupID"].SourceColumn = "String3GroupID";
				parameters["@String3Label"].SourceColumn = "String3Label";
				parameters["@IsInactiveString4"].SourceColumn = "IsInactiveString4";
				parameters["@String4GroupID"].SourceColumn = "String4GroupID";
				parameters["@String4Label"].SourceColumn = "String4Label";
				parameters["@IsInactiveString5"].SourceColumn = "IsInactiveString5";
				parameters["@String5GroupID"].SourceColumn = "String5GroupID";
				parameters["@String5Label"].SourceColumn = "String5Label";
				parameters["@IsInactiveString6"].SourceColumn = "IsInactiveString6";
				parameters["@String6GroupID"].SourceColumn = "String6GroupID";
				parameters["@String6Label"].SourceColumn = "String6Label";
				parameters["@IsInactiveString7"].SourceColumn = "IsInactiveString7";
				parameters["@String7GroupID"].SourceColumn = "String7GroupID";
				parameters["@String7Label"].SourceColumn = "String7Label";
				parameters["@IsInactiveString8"].SourceColumn = "IsInactiveString8";
				parameters["@String8GroupID"].SourceColumn = "String8GroupID";
				parameters["@String8Label"].SourceColumn = "String8Label";
				parameters["@IsInactiveString9"].SourceColumn = "IsInactiveString9";
				parameters["@String9GroupID"].SourceColumn = "String9GroupID";
				parameters["@String9Label"].SourceColumn = "String9Label";
				parameters["@IsInactiveString10"].SourceColumn = "IsInactiveString10";
				parameters["@String10GroupID"].SourceColumn = "String10GroupID";
				parameters["@String10Label"].SourceColumn = "String10Label";
				parameters["@IsInactiveMemo1"].SourceColumn = "IsInactiveMemo1";
				parameters["@Memo1GroupID"].SourceColumn = "Memo1GroupID";
				parameters["@Memo1Label"].SourceColumn = "Memo1Label";
				parameters["@IsInactiveMemo2"].SourceColumn = "IsInactiveMemo2";
				parameters["@Memo2GroupID"].SourceColumn = "Memo2GroupID";
				parameters["@Memo2Label"].SourceColumn = "Memo2Label";
				parameters["@IsInactiveMemo3"].SourceColumn = "IsInactiveMemo3";
				parameters["@Memo3GroupID"].SourceColumn = "Memo3GroupID";
				parameters["@Memo3Label"].SourceColumn = "Memo3Label";
				parameters["@IsInactiveMemo4"].SourceColumn = "IsInactiveMemo4";
				parameters["@Memo4GroupID"].SourceColumn = "Memo4GroupID";
				parameters["@Memo4Label"].SourceColumn = "Memo4Label";
				parameters["@IsInactiveDecimal1"].SourceColumn = "IsInactiveDecimal1";
				parameters["@Decimal1GroupID"].SourceColumn = "Decimal1GroupID";
				parameters["@Decimal1Label"].SourceColumn = "Decimal1Label";
				parameters["@IsInactiveDecimal2"].SourceColumn = "IsInactiveDecimal2";
				parameters["@Decimal2GroupID"].SourceColumn = "Decimal2GroupID";
				parameters["@Decimal1Labe2"].SourceColumn = "Decimal2Label";
				parameters["@IsInactiveDecimal3"].SourceColumn = "IsInactiveDecimal3";
				parameters["@Decimal3GroupID"].SourceColumn = "Decimal3GroupID";
				parameters["@Decimal3Label"].SourceColumn = "Decimal3Label";
				parameters["@IsInactiveDecimal4"].SourceColumn = "IsInactiveDecimal4";
				parameters["@Decimal4GroupID"].SourceColumn = "Decimal4GroupID";
				parameters["@Decimal4Label"].SourceColumn = "Decimal4Label";
				parameters["@IsInactiveDecimal5"].SourceColumn = "IsInactiveDecimal5";
				parameters["@Decimal5GroupID"].SourceColumn = "Decimal5GroupID";
				parameters["@Decimal5Label"].SourceColumn = "Decimal5Label";
				parameters["@IsInactiveDecimal6"].SourceColumn = "IsInactiveDecimal6";
				parameters["@Decimal6GroupID"].SourceColumn = "Decimal6GroupID";
				parameters["@Decimal6Label"].SourceColumn = "Decimal6Label";
				parameters["@IsInactiveBool1"].SourceColumn = "IsInactiveBool1";
				parameters["@Bool1GroupID"].SourceColumn = "Bool1GroupID";
				parameters["@Bool1Label"].SourceColumn = "Bool1Label";
				parameters["@IsInactiveBool2"].SourceColumn = "IsInactiveBool2";
				parameters["@Bool2GroupID"].SourceColumn = "Bool2GroupID";
				parameters["@Bool2Label"].SourceColumn = "Bool2Label";
				parameters["@IsInactiveBool3"].SourceColumn = "IsInactiveBool3";
				parameters["@Bool3GroupID"].SourceColumn = "Bool3GroupID";
				parameters["@Bool3Label"].SourceColumn = "Bool3Label";
				parameters["@IsInactiveBool4"].SourceColumn = "IsInactiveBool4";
				parameters["@Bool4GroupID"].SourceColumn = "Bool4GroupID";
				parameters["@Bool4Label"].SourceColumn = "Bool4Label";
				parameters["@IsInactiveBool5"].SourceColumn = "IsInactiveBool5";
				parameters["@Bool5GroupID"].SourceColumn = "Bool5GroupID";
				parameters["@Bool5Label"].SourceColumn = "Bool5Label";
				parameters["@IsInactiveBool6"].SourceColumn = "IsInactiveBool6";
				parameters["@Bool6GroupID"].SourceColumn = "Bool6GroupID";
				parameters["@Bool6Label"].SourceColumn = "Bool6Label";
				parameters["@IsInactiveDateTime1"].SourceColumn = "IsInactiveDateTime1";
				parameters["@DateTime1GroupID"].SourceColumn = "DateTime1GroupID";
				parameters["@DateTime1Label"].SourceColumn = "DateTime1Label";
				parameters["@IsInactiveDateTime2"].SourceColumn = "IsInactiveDateTime2";
				parameters["@DateTime2GroupID"].SourceColumn = "DateTime2GroupID";
				parameters["@DateTime2Label"].SourceColumn = "DateTime2Label";
				parameters["@IsInactiveDateTime3"].SourceColumn = "IsInactiveDateTime3";
				parameters["@DateTime3GroupID"].SourceColumn = "DateTime3GroupID";
				parameters["@DateTime3Label"].SourceColumn = "DateTime3Label";
				parameters["@IsInactiveDateTime4"].SourceColumn = "IsInactiveDateTime4";
				parameters["@DateTime4GroupID"].SourceColumn = "DateTime4GroupID";
				parameters["@DateTime4Label"].SourceColumn = "DateTime4Label";
				parameters["@IsInactiveDateTime5"].SourceColumn = "IsInactiveDateTime5";
				parameters["@DateTime5GroupID"].SourceColumn = "DateTime5GroupID";
				parameters["@DateTime5Label"].SourceColumn = "DateTime5Label";
				parameters["@IsInactiveDateTime6"].SourceColumn = "IsInactiveDateTime6";
				parameters["@DateTime6GroupID"].SourceColumn = "DateTime6GroupID";
				parameters["@DateTime6Label"].SourceColumn = "DateTime6Label";
				parameters["@IsInactiveCombo1"].SourceColumn = "IsInactiveCombo1";
				parameters["@Combo1GroupID"].SourceColumn = "Combo1GroupID";
				parameters["@Combo1Label"].SourceColumn = "Combo1Label";
				parameters["@Combo1Type"].SourceColumn = "Combo1Type";
				parameters["@IsInactiveCombo2"].SourceColumn = "IsInactiveCombo2";
				parameters["@Combo2GroupID"].SourceColumn = "Combo2GroupID";
				parameters["@Combo2Label"].SourceColumn = "Combo2Label";
				parameters["@Combo2Type"].SourceColumn = "Combo2Type";
				parameters["@IsInactiveCombo3"].SourceColumn = "IsInactiveCombo3";
				parameters["@Combo3GroupID"].SourceColumn = "Combo3GroupID";
				parameters["@Combo3Label"].SourceColumn = "Combo3Label";
				parameters["@Combo3Type"].SourceColumn = "Combo3Type";
				parameters["@IsInactiveCombo4"].SourceColumn = "IsInactiveCombo4";
				parameters["@Combo4GroupID"].SourceColumn = "Combo4GroupID";
				parameters["@Combo4Label"].SourceColumn = "Combo4Label";
				parameters["@Combo4Type"].SourceColumn = "Combo4Type";
				parameters["@IsInactiveCombo5"].SourceColumn = "IsInactiveCombo5";
				parameters["@Combo5GroupID"].SourceColumn = "Combo5GroupID";
				parameters["@Combo5Label"].SourceColumn = "Combo5Label";
				parameters["@Combo5Type"].SourceColumn = "Combo5Type";
				parameters["@IsInactiveCombo6"].SourceColumn = "IsInactiveCombo6";
				parameters["@Combo6GroupID"].SourceColumn = "Combo6GroupID";
				parameters["@Combo6Label"].SourceColumn = "Combo6Label";
				parameters["@Combo6Type"].SourceColumn = "Combo6Type";
				parameters["@IsInactiveCombo7"].SourceColumn = "IsInactiveCombo7";
				parameters["@Combo7GroupID"].SourceColumn = "Combo7GroupID";
				parameters["@Combo7Label"].SourceColumn = "Combo7Label";
				parameters["@Combo7Type"].SourceColumn = "Combo7Type";
				parameters["@IsInactiveCombo8"].SourceColumn = "IsInactiveCombo8";
				parameters["@Combo8GroupID"].SourceColumn = "Combo8GroupID";
				parameters["@Combo8Label"].SourceColumn = "Combo8Label";
				parameters["@Combo8Type"].SourceColumn = "Combo8Type";
				parameters["@IsInactiveCombo9"].SourceColumn = "IsInactiveCombo9";
				parameters["@Combo9GroupID"].SourceColumn = "Combo9GroupID";
				parameters["@Combo9Label"].SourceColumn = "Combo9Label";
				parameters["@Combo9Type"].SourceColumn = "Combo9Type";
				parameters["@IsInactiveCombo10"].SourceColumn = "IsInactiveCombo10";
				parameters["@Combo10GroupID"].SourceColumn = "Combo10GroupID";
				parameters["@Combo10Label"].SourceColumn = "Combo10Label";
				parameters["@Combo10Type"].SourceColumn = "Combo10Type";
				parameters["@DateTimeStamp"].SourceColumn = "DateTimeStamp";
			}
			return insertCommand;
		}

		private bool InsertCustomFieldSetup(CustomFieldSetupData customFieldSetupData, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			bool flag2 = true;
			SqlCommand insertCommand = GetInsertCommand();
			if (sqlTransaction == null)
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				flag2 = false;
			}
			try
			{
				insertCommand.Transaction = sqlTransaction;
				flag &= Insert(customFieldSetupData, "[Custom Field Setups]", insertCommand);
				if (!flag)
				{
					return flag;
				}
				object insertedRowIdentity = GetInsertedRowIdentity("[Custom Field Setups]", insertCommand);
				customFieldSetupData.CustomFieldSetupTable.Rows[0]["CustomFieldSetupID"] = insertedRowIdentity;
				UpdateTableRowByID("[Custom Field Setups]", "CustomFieldSetupID", "DateTimeStamp", insertedRowIdentity, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Custom Field Setups]", "CustomFieldSetupID", insertedRowIdentity, sqlTransaction, isInsert: true);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				if (!flag2)
				{
					base.DBConfig.EndTransaction(flag);
				}
			}
		}

		private bool InsertCustomFieldSetup(CustomFieldSetupData customFieldSetupData)
		{
			return InsertCustomFieldSetup(customFieldSetupData, null);
		}

		private bool UpdateCustomFieldSetup(CustomFieldSetupData customFieldSetupData)
		{
			bool flag = true;
			SqlCommand updateCommand = GetUpdateCommand();
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (updateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(customFieldSetupData, "[Custom Field Setups]", updateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = customFieldSetupData.CustomFieldSetupTable.Rows[0]["CustomFieldSetupID"];
				UpdateTableRowByID("[Custom Field Setups]", "CustomFieldSetupID", "DateTimeStamp", obj, DateTime.Now, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("[Custom Field Setups]", "CustomFieldSetupID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool InsertUpdateCustomFieldSetup(CustomFieldSetupData customFieldSetupData, string tableName)
		{
			return InsertUpdateCustomFieldSetup(customFieldSetupData, tableName, CustomTypes.NONE);
		}

		public bool InsertUpdateCustomFieldSetup(CustomFieldSetupData customFieldSetupData, string tableName, CustomTypes cutomType)
		{
			if (customFieldSetupData == null)
			{
				throw new NullReferenceException("CustomField is null.");
			}
			if (customFieldSetupData.CustomFieldSetupTable.Rows.Count != 1)
			{
				throw new ApplicationException("CustomField table must have one row.");
			}
			int setupIDByTableName = GetSetupIDByTableName(tableName, cutomType);
			DataRow dataRow = customFieldSetupData.CustomFieldSetupTable.Rows[0];
			if (cutomType == CustomTypes.NONE)
			{
				dataRow["CustomType"] = DBNull.Value;
			}
			else
			{
				dataRow["CustomType"] = cutomType;
			}
			dataRow["TableName"] = tableName;
			if (setupIDByTableName != -1)
			{
				customFieldSetupData.CustomFieldSetupTable.AcceptChanges();
				dataRow["CustomFieldSetupID"] = setupIDByTableName;
				return UpdateCustomFieldSetup(customFieldSetupData);
			}
			return InsertCustomFieldSetup(customFieldSetupData);
		}

		internal int GetSetupIDByTableName(string tableName)
		{
			return GetSetupIDByTableName(tableName, CustomTypes.NONE);
		}

		internal int GetSetupIDByTableName(string tableName, CustomTypes type)
		{
			object obj;
			if (type != CustomTypes.NONE)
			{
				obj = ExecuteSelectScalar("[Custom Field Setups]", "TableName", "CustomType", tableName, (byte)type, "CustomFieldSetupID", base.DBConfig.SqlTransaction);
			}
			else
			{
				string exp = "SELECT CustomFieldSetupID FROM [Custom Field Setups] WHERE TableName='" + tableName + "' AND CustomType IS NULL";
				obj = ExecuteScalar(exp);
			}
			if (obj != null && obj != DBNull.Value)
			{
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public CustomFieldSetupData GetSetupByTableName(string tableName)
		{
			return GetSetupByTableName(tableName, CustomTypes.NONE);
		}

		public CustomFieldSetupData GetSetupByTableName(string tableName, CustomTypes type)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TableName";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = tableName;
			commandHelper.TableName = "[Custom Field Setups]";
			sqlBuilder.AddCommandHelper(commandHelper);
			if (type != CustomTypes.NONE)
			{
				commandHelper = new CommandHelper();
				commandHelper.FieldName = "CustomType";
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.FieldValue = (byte)type;
				commandHelper.TableName = "[Custom Field Setups]";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			sqlBuilder.IsComparing = true;
			CustomFieldSetupData customFieldSetupData = new CustomFieldSetupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customFieldSetupData, "[Custom Field Setups]", sqlBuilder);
			return customFieldSetupData;
		}

		public bool ExistSetupByTable(string tableName)
		{
			return ExistSetupByTable(tableName, CustomTypes.NONE);
		}

		public bool ExistSetupByTable(string tableName, CustomTypes type)
		{
			try
			{
				if (type != CustomTypes.NONE)
				{
					return IsTableFieldValueExist("[Custom Field Setups]", "TableName", "CustomType", tableName, (byte)type);
				}
				string exp = "SELECT CustomFieldSetupID FROM [Custom Field Setups] WHERE TableName='" + tableName + "' AND CustomType IS NULL";
				return ExecuteScalar(exp) != null;
			}
			catch
			{
				throw;
			}
		}
	}
}
