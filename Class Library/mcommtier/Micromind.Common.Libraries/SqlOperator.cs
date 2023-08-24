using System;

namespace Micromind.Common.Libraries
{
	[Serializable]
	public class SqlOperator
	{
		internal CompareValueOperator compareValueOP;

		internal LogicalFieldOperator logicalFieldOp;

		internal LogicalValueOperator logicalValueOp;

		public object CompareValueOp
		{
			get
			{
				switch (compareValueOP)
				{
				case CompareValueOperator.Equal:
					return "=";
				case CompareValueOperator.Greater:
					return ">";
				case CompareValueOperator.GreaterEqual:
					return ">=";
				case CompareValueOperator.Less:
					return "<";
				case CompareValueOperator.LessEqual:
					return "<=";
				case CompareValueOperator.Like:
					return "LIKE";
				case CompareValueOperator.NotEqual:
					return "<>";
				case CompareValueOperator.IS:
					return "IS";
				case CompareValueOperator.ISNOT:
					return "IS NOT";
				case CompareValueOperator.Null:
					return "";
				default:
					return "=";
				}
			}
			set
			{
				compareValueOP = (CompareValueOperator)value;
			}
		}

		public object LogicalFieldOp
		{
			get
			{
				switch (logicalFieldOp)
				{
				case LogicalFieldOperator.AND:
					return "AND";
				case LogicalFieldOperator.OR:
					return "OR";
				case LogicalFieldOperator.Null:
					return "AND";
				default:
					return "AND";
				}
			}
			set
			{
				logicalFieldOp = (LogicalFieldOperator)value;
			}
		}

		public object LogicalValueOp
		{
			get
			{
				switch (logicalValueOp)
				{
				case LogicalValueOperator.ALL:
					return "ALL";
				case LogicalValueOperator.AND:
					return "AND";
				case LogicalValueOperator.ANY:
					return "ANY";
				case LogicalValueOperator.BETWEEN:
					return "BETWEEN";
				case LogicalValueOperator.OR:
					return "OR";
				case LogicalValueOperator.IN:
					return "IN";
				case LogicalValueOperator.NOT_IN:
					return "NOT IN";
				case LogicalValueOperator.Null:
					return "";
				default:
					return "=";
				}
			}
			set
			{
				logicalValueOp = (LogicalValueOperator)value;
			}
		}

		public LogicalFieldOperator GetLogicalFieldOperator()
		{
			return logicalFieldOp;
		}

		public string GetValueOperator()
		{
			if (compareValueOP == CompareValueOperator.None)
			{
				return string.Empty;
			}
			if (logicalValueOp != 0 && compareValueOP != 0)
			{
				return "=";
			}
			if (logicalValueOp == LogicalValueOperator.Null && compareValueOP == CompareValueOperator.Null)
			{
				return "=";
			}
			return LogicalValueOp.ToString() + CompareValueOp.ToString();
		}
	}
}
