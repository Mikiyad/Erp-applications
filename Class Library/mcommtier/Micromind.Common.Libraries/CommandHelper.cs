using System;
using System.Data;

using System.Text;

namespace Micromind.Common.Libraries
{
	[Serializable]
	
	public class CommandHelper
	{
		private bool allowNull;

		private SqlDbType sqlFieldType;

		private string fieldName = "";

		private string tableName = "";

		private object fieldValue1 = "";

		private object fieldValue2 = "";

		private SqlOperator sqlOp = new SqlOperator();

		private bool isLastField = true;

		private bool isFirstField = true;

		private bool addWildcardAfterValue;

		private bool addWildcardBeforeValue;

		private bool hasCloseParanteses;

		private bool hasOpenParanteses;

		public bool AllowNull
		{
			get
			{
				return allowNull;
			}
			set
			{
				allowNull = value;
			}
		}

		public SqlDbType SqlFieldType
		{
			get
			{
				return sqlFieldType;
			}
			set
			{
				sqlFieldType = value;
			}
		}

		public string FieldName
		{
			get
			{
				return fieldName;
			}
			set
			{
				fieldName = value;
			}
		}

		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

		public object FieldValue
		{
			set
			{
				fieldValue1 = value;
			}
		}

		public object FieldValue2
		{
			set
			{
				fieldValue2 = value;
			}
		}

		public SqlOperator SqlOp
		{
			get
			{
				return sqlOp;
			}
			set
			{
				sqlOp = value;
			}
		}

		public bool HasCloseParanteses
		{
			get
			{
				return hasCloseParanteses;
			}
			set
			{
				hasCloseParanteses = value;
			}
		}

		public bool HasOpenParanteses
		{
			get
			{
				return hasOpenParanteses;
			}
			set
			{
				hasOpenParanteses = value;
			}
		}

		public bool IsLastField
		{
			get
			{
				return isLastField;
			}
			set
			{
				isLastField = value;
			}
		}

		public bool IsFirstField
		{
			get
			{
				return isFirstField;
			}
			set
			{
				isFirstField = value;
			}
		}

		public bool AddWildcardAfterValue
		{
			get
			{
				return addWildcardAfterValue;
			}
			set
			{
				addWildcardAfterValue = value;
			}
		}

		public bool AddWildcardBeforeValue
		{
			get
			{
				return addWildcardBeforeValue;
			}
			set
			{
				addWildcardBeforeValue = value;
			}
		}

		private string AddSingleQuote(string str)
		{
			if (str == null || str.Length == 0)
			{
				return str;
			}
			int num;
			for (num = 0; num != str.Length; num = checked(num + 2))
			{
				num = str.IndexOf('\'', num);
				if (num < 0)
				{
					break;
				}
				str = str.Insert(num, "'");
			}
			return str;
		}

		private bool IsValueArrayType()
		{
			if (fieldValue1 == null)
			{
				return false;
			}
			return fieldValue1.GetType().BaseType == typeof(Array);
		}

		private object GetFieldValue()
		{
			if (sqlOp.logicalValueOp == LogicalValueOperator.BETWEEN)
			{
				return GetFieldValue1() + " AND " + GetFieldValue2();
			}
			if (IsValueArrayType())
			{
				return GetFieldArrayValue();
			}
			return GetFieldValue1();
		}

		private object GetFieldArrayValue()
		{
			string text = "";
			SqlDbType sqlDbType = SqlFieldType;
			checked
			{
				if (sqlDbType <= SqlDbType.NVarChar)
				{
					switch (sqlDbType)
					{
					case SqlDbType.BigInt:
					case SqlDbType.Float:
					case SqlDbType.Int:
					case SqlDbType.Money:
						goto IL_014e;
					}
				}
				else
				{
					if (sqlDbType == SqlDbType.SmallInt)
					{
						short[] array = (short[])fieldValue1;
						if (array == null || array.Length == 0)
						{
							return text = "IS NOT NULL";
						}
						text = "IN(";
						short[] array2 = array;
						foreach (short num in array2)
						{
							text = text + num + ",";
						}
						text = text.Remove(text.Length - 1, 1);
						return text + ")";
					}
					if (sqlDbType == SqlDbType.TinyInt)
					{
						byte[] array3 = (byte[])fieldValue1;
						if (array3 == null || array3.Length == 0)
						{
							return text = "IS NOT NULL";
						}
						text = "IN(";
						byte[] array4 = array3;
						foreach (byte b in array4)
						{
							text = text + b + ",";
						}
						text = text.Remove(text.Length - 1, 1);
						return text + ")";
					}
					_ = 22;
				}
				if (!(fieldValue1.GetType() == typeof(int[])))
				{
					string[] array5 = (string[])fieldValue1;
					if (array5 == null || array5.Length == 0)
					{
						return text = "IS NOT NULL";
					}
					string[] array6;
					if (sqlOp.CompareValueOp.ToString().CompareTo("LIKE") != 0)
					{
						text = "IN(";
						array6 = array5;
						foreach (string str in array6)
						{
							text = text + "'" + str + "',";
						}
						text = text.Remove(text.Length - 1, 1);
						return text + ")";
					}
					array6 = array5;
					foreach (string val in array6)
					{
						text = text + GetFieldValue(val) + " OR ";
					}
					return text.Remove(text.Length - 3, 3);
				}
				goto IL_014e;
			}
			IL_014e:
			int[] array7 = (int[])fieldValue1;
			if (array7 == null || array7.Length == 0)
			{
				return text = "IS NOT NULL";
			}
			text = "IN(";
			int[] array8 = array7;
			foreach (int num2 in array8)
			{
				text = text + num2 + ",";
			}
			text = text.Remove(checked(text.Length - 1), 1);
			return text + ")";
		}

		private object GetFieldValue(object val)
		{
			SqlDbType sqlDbType = SqlFieldType;
			if (sqlDbType == SqlDbType.DateTime || sqlDbType == SqlDbType.NVarChar)
			{
				if (sqlOp.CompareValueOp.ToString().CompareTo("LIKE") == 0)
				{
					if (AddWildcardBeforeValue)
					{
						val = "%" + val;
					}
					if (AddWildcardAfterValue)
					{
						val = val.ToString() + "%";
					}
				}
				val = AddSingleQuote(val.ToString());
				val = ((val.ToString().Length <= 0) ? "''" : ("'" + val.ToString() + "'"));
			}
			return val;
		}

		private object GetFieldValue1()
		{
			object val = fieldValue1.ToString();
			return GetFieldValue(val);
		}

		private object GetFieldValue2()
		{
			object val = fieldValue2.ToString();
			return GetFieldValue(val);
		}

		public string GetExpression()
		{
			if ((fieldValue1 == null || fieldValue1.ToString() == string.Empty) && (fieldName == null || fieldName == string.Empty))
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (HasOpenParanteses || allowNull)
			{
				stringBuilder.Append("(");
			}
			string text = "";
			text = ((TableName.Length > 0) ? (SqlBuilder.GetTableAlias(TableName) + "." + FieldName) : FieldName);
			stringBuilder.Append(text);
			if (!IsValueArrayType())
			{
				stringBuilder.Append(" ").Append(SqlOp.GetValueOperator()).Append(" ")
					.Append(GetFieldValue().ToString());
			}
			else
			{
				stringBuilder.Append(" ").Append(GetFieldValue().ToString());
			}
			if (allowNull)
			{
				stringBuilder.Append(" OR ").Append(text).Append(" IS NULL");
			}
			if (HasCloseParanteses || allowNull)
			{
				stringBuilder.Append(")");
			}
			if (!IsLastField || sqlOp.logicalFieldOp != 0)
			{
				stringBuilder.Append(" ").Append(sqlOp.LogicalFieldOp);
			}
			return stringBuilder.ToString();
		}
	}
}
