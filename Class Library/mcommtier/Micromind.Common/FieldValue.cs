using System;
using System.Data;

namespace Micromind.Common
{
	[Serializable]
	public class FieldValue
	{
		private string name;

		private string val;

		private SqlDbType fieldType = SqlDbType.Int;

		private bool isUpdateConditionField;

		private bool checkForNull;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string Value
		{
			get
			{
				return val;
			}
			set
			{
				val = value;
			}
		}

		public SqlDbType FieldType
		{
			get
			{
				return fieldType;
			}
			set
			{
				fieldType = value;
			}
		}

		public bool IsUpdateConditionField
		{
			get
			{
				return isUpdateConditionField;
			}
			set
			{
				isUpdateConditionField = value;
			}
		}

		public bool CheckForNullValue => checkForNull;

		public FieldValue(string name, string val)
		{
			Name = name;
			Value = val;
		}

		public FieldValue(string name, string val, SqlDbType fieldType)
		{
			Name = name;
			Value = val;
			this.fieldType = fieldType;
		}

		public FieldValue(string name, string val, bool isUpdateConditionField)
		{
			Name = name;
			Value = val;
			this.isUpdateConditionField = isUpdateConditionField;
		}

		public FieldValue(string name, string val, bool isUpdateConditionField, bool checkForNullValue)
		{
			Name = name;
			Value = val;
			this.isUpdateConditionField = isUpdateConditionField;
			checkForNull = checkForNullValue;
		}
	}
}
