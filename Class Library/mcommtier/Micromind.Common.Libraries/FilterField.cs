using System;
using System.Data;

namespace Micromind.Common.Libraries
{
	[Serializable]
	public class FilterField
	{
		private FilterValueTypes filterValueType = FilterValueTypes.Strings;

		public string Name = "";

		public object Value;

		public object Value2;

		private CompareValueOperator compareOperator = CompareValueOperator.Equal;

		public LogicalValueOperator logicalValueOperator = LogicalValueOperator.AND;

		public SqlDbType sqlDbType = SqlDbType.NVarChar;

		private bool addWildcardBeforeValue;

		private bool addWildcardAfterValue;

		public FilterValueTypes FilterValueType
		{
			get
			{
				return filterValueType;
			}
			set
			{
				switch (value)
				{
				case FilterValueTypes.Strings:
					sqlDbType = SqlDbType.NVarChar;
					break;
				case FilterValueTypes.Numbers:
					sqlDbType = SqlDbType.Int;
					break;
				case FilterValueTypes.Dates:
					sqlDbType = SqlDbType.DateTime;
					break;
				}
				filterValueType = value;
			}
		}

		public CompareValueOperator CompareOperator => compareOperator;

		public bool AddWildcardBeforeValue => addWildcardBeforeValue;

		public bool AddWildcardAfterValue => addWildcardAfterValue;

		public SearchOperators SearchOperator
		{
			set
			{
				switch (value)
				{
				case SearchOperators.BeginsWith:
					compareOperator = CompareValueOperator.Like;
					addWildcardAfterValue = true;
					addWildcardBeforeValue = false;
					break;
				case SearchOperators.Contains:
					compareOperator = CompareValueOperator.Like;
					addWildcardAfterValue = true;
					addWildcardBeforeValue = true;
					break;
				case SearchOperators.EndsWith:
					compareOperator = CompareValueOperator.Like;
					addWildcardAfterValue = false;
					addWildcardBeforeValue = true;
					break;
				default:
					compareOperator = CompareValueOperator.Equal;
					break;
				case SearchOperators.GreaterThan:
					compareOperator = CompareValueOperator.Greater;
					break;
				case SearchOperators.GreatherThanEqual:
					compareOperator = CompareValueOperator.GreaterEqual;
					break;
				case SearchOperators.LessThan:
					compareOperator = CompareValueOperator.Less;
					break;
				case SearchOperators.LessThanEqual:
					compareOperator = CompareValueOperator.LessEqual;
					break;
				case SearchOperators.NotEqual:
					compareOperator = CompareValueOperator.NotEqual;
					break;
				}
			}
		}

		public FilterField(string name, object value, SearchOperators searchOperator, FilterValueTypes filterValueType)
		{
			if (name == null || name.Trim() == string.Empty)
			{
				throw new ApplicationException("Name cannot be null.");
			}
			Name = name;
			Value = value;
			SearchOperator = searchOperator;
			FilterValueType = filterValueType;
		}

		public FilterField(string name, object value, SearchOperators searchOperator)
		{
			if (name == null || name.Trim() == string.Empty)
			{
				throw new ApplicationException("Name cannot be null.");
			}
			Name = name;
			Value = value;
			SearchOperator = searchOperator;
		}

		public FilterField(string name, object value, SearchOperators searchOperator, LogicalValueOperator logicalValueOperator, FilterValueTypes filterValueType)
		{
			if (name == null || name.Trim() == string.Empty)
			{
				throw new ApplicationException("Name cannot be null.");
			}
			Name = name;
			Value = value;
			SearchOperator = searchOperator;
			this.logicalValueOperator = logicalValueOperator;
			FilterValueType = filterValueType;
		}

		public FilterField(string name, object value)
		{
			if (name == null || name.Trim() == string.Empty)
			{
				throw new ApplicationException("Name cannot be null.");
			}
			Name = name;
			Value = value;
		}
	}
}
