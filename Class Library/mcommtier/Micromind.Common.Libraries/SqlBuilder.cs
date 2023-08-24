using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

using System.Text;

namespace Micromind.Common.Libraries
{
	[Serializable]
	
	public class SqlBuilder : IDisposable
	{
		[Serializable]
		private class OrderBy
		{
			public string column;

			public bool isAscending;

			public OrderBy(string column, bool isAscending)
			{
				this.column = column;
				this.isAscending = isAscending;
			}
		}

		[Serializable]
		private class TableJointer
		{
			public string sourceField;

			public string targetTable;

			public string targetTableAlias;

			public string targetField;

			public string sourceTable;

			public JoinType joinType;

			public TableJointer(string sourceTable, string sourceField, string targetTable, string targetField, JoinType joinType)
			{
				this.targetTable = targetTable;
				this.targetField = targetField;
				this.sourceField = sourceField;
				this.sourceTable = sourceTable;
				this.joinType = joinType;
			}

			public TableJointer(string sourceTable, string sourceField, string targetTable, string targetTableAlias, string targetField, JoinType joinType)
			{
				this.targetTable = targetTable;
				this.targetTableAlias = targetTableAlias;
				this.targetField = targetField;
				this.sourceField = sourceField;
				this.sourceTable = sourceTable;
				this.joinType = joinType;
			}
		}

		private StringCollection tablesCols = new StringCollection();

		private ArrayList sqlHelpers = new ArrayList();

		private ArrayList jointerList = new ArrayList();

		private StringCollection selectList = new StringCollection();

		private StringCollection columnList = new StringCollection();

		private bool isComparing = true;

		private JoinType joinType;

		private string sourceTable;

		private SqlBuilder subqueryBuilder;

		private bool hasAggregateFunction;

		private ArrayList nameValue = new ArrayList();

		private string insertTableName;

		private ArrayList orederByColumns = new ArrayList();

		private bool useDistict = true;

		private int topN = -1;

		public string JointerSourceTable
		{
			set
			{
				sourceTable = value;
			}
		}

		public ArrayList FieldValues => nameValue;

		public int TopN
		{
			get
			{
				return topN;
			}
			set
			{
				topN = value;
			}
		}

		public SqlBuilder SubqueryBuilder
		{
			get
			{
				return subqueryBuilder;
			}
			set
			{
				subqueryBuilder = value;
			}
		}

		public bool IsComparing
		{
			get
			{
				return isComparing;
			}
			set
			{
				isComparing = value;
			}
		}

		public bool UseDistinct
		{
			set
			{
				useDistict = value;
			}
		}

		public void Dispose()
		{
			sqlHelpers = null;
			tablesCols = null;
			jointerList = null;
			selectList = null;
			columnList = null;
			subqueryBuilder = null;
			nameValue = null;
			orederByColumns = null;
		}

		public void AddJointer(string sourceTable, string sourceField, string targetTable, string targetField)
		{
			jointerList.Add(new TableJointer(sourceTable, sourceField, targetTable, targetField, JoinType.LeftOuter));
			joinType = JoinType.LeftOuter;
			isComparing = true;
			if (this.sourceTable == null || this.sourceTable.Length == 0)
			{
				this.sourceTable = sourceTable;
			}
		}

		public void AddJointer(string sourceTable, string sourceField, string targetTable, string targetTableAlias, string targetField)
		{
			jointerList.Add(new TableJointer(sourceTable, sourceField, targetTable, targetTableAlias, targetField, JoinType.LeftOuter));
			joinType = JoinType.LeftOuter;
			isComparing = true;
			if (this.sourceTable == null || this.sourceTable.Length == 0)
			{
				this.sourceTable = sourceTable;
			}
		}

		public void AddJointer(string sourceTable, string sourceField, string targetTable, string targetField, JoinType joinType)
		{
			jointerList.Add(new TableJointer(sourceTable, sourceField, targetTable, targetField, joinType));
			this.joinType = joinType;
			isComparing = true;
			if (this.sourceTable == null || this.sourceTable.Length == 0)
			{
				this.sourceTable = sourceTable;
			}
		}

		private StringBuilder[] GetJointerExp()
		{
			if (joinType == JoinType.Null)
			{
				return null;
			}
			StringBuilder[] array = new StringBuilder[jointerList.Count];
			for (int i = 0; i < jointerList.Count; i = checked(i + 1))
			{
				array[i] = new StringBuilder();
				TableJointer tableJointer = (TableJointer)jointerList[i];
				array[i].Append(" ").Append(GetJoinTypeExp(tableJointer)).Append(" ");
				string value = (tableJointer.targetTableAlias != null && !(tableJointer.targetTableAlias == string.Empty)) ? tableJointer.targetTableAlias : GetTableAlias(tableJointer.targetTable);
				array[i].Append(tableJointer.targetTable).Append(" ").Append(value)
					.Append(" ");
				array[i].Append("ON").Append(" ");
				array[i].Append(GetTableAlias(tableJointer.sourceTable)).Append(".").Append(tableJointer.sourceField);
				array[i].Append(" = ");
				array[i].Append(value).Append(".").Append(tableJointer.targetField);
			}
			return array;
		}

		public void AddInsertUpdateParameters(string tableName, params FieldValue[] nameVal)
		{
			insertTableName = tableName;
			foreach (FieldValue value in nameVal)
			{
				nameValue.Add(value);
			}
		}

		public string GetInsertExpression()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT ").Append(insertTableName).Append("(");
			foreach (FieldValue item in nameValue)
			{
				stringBuilder.Append(item.Name).Append(",");
			}
			checked
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.Append(") VALUES(");
				foreach (FieldValue item2 in nameValue)
				{
					stringBuilder.Append(item2.Value).Append(",");
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		public string GetUpdateExpression()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			stringBuilder.Append("UPDATE ").Append(insertTableName).Append(" SET ");
			foreach (FieldValue item in nameValue)
			{
				if (!item.IsUpdateConditionField)
				{
					stringBuilder.Append(item.Name).Append(" = ").Append(item.Value)
						.Append(",");
				}
				else
				{
					flag = true;
				}
			}
			checked
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				if (flag)
				{
					stringBuilder.Append(" WHERE ");
					foreach (FieldValue item2 in nameValue)
					{
						if (item2.IsUpdateConditionField)
						{
							if (!item2.CheckForNullValue)
							{
								stringBuilder.Append(item2.Name).Append(" = ").Append(item2.Value)
									.Append(" AND ");
							}
							else
							{
								stringBuilder.Append("(").Append(item2.Name).Append(" = ")
									.Append(item2.Value)
									.Append(" ");
								stringBuilder.Append("OR ").Append(item2.Name).Append(" IS NULL")
									.Append(") AND ");
							}
						}
					}
					stringBuilder.Remove(stringBuilder.Length - 4, 4);
				}
				return stringBuilder.ToString();
			}
		}

		public static string GetTableAlias(string tableName, int inc)
		{
			string str = "";
			checked
			{
				if (tableName[0] == '[')
				{
					for (int i = 1; i < tableName.Length - 1; i++)
					{
						if (tableName[i] != ' ')
						{
							str += tableName[i].ToString();
						}
					}
				}
				else
				{
					str = tableName;
				}
				return str + inc.ToString();
			}
		}

		public static string GetTableAlias(string tableName)
		{
			string text = "";
			if (tableName == null)
			{
				return "";
			}
			checked
			{
				if (tableName[0] == '[')
				{
					for (int i = 1; i < tableName.Length - 1; i++)
					{
						if (tableName[i] != ' ')
						{
							text += tableName[i].ToString();
						}
					}
				}
				else
				{
					text = tableName;
				}
				return text;
			}
		}

		private string GetJoinTypeExp(TableJointer jointer)
		{
			switch (jointer.joinType)
			{
			case JoinType.LeftOuter:
				return "LEFT OUTER JOIN";
			case JoinType.RightOuterJoin:
				return "RIGHT OUTER JOIN";
			default:
				return "LEFT OUTER JOIN";
			}
		}

		public void AddTable(string strTable)
		{
			if (strTable.Trim().Length > 0 && !TableExists(strTable))
			{
				tablesCols.Add(strTable);
			}
		}

		public void ClearColumns()
		{
		}

		public void AddColumn(string tableName, string columnName)
		{
			if (!selectList.Contains(GetTableAlias(tableName) + "." + columnName))
			{
				selectList.Add(GetTableAlias(tableName) + "." + columnName);
				AddTable(tableName);
				AddColumn(GetTableAlias(tableName) + "." + columnName);
			}
		}

		public void AddColumn(string tableName, string columnName, string alias)
		{
			if (!selectList.Contains(GetTableAlias(tableName) + "." + columnName + " AS " + alias))
			{
				selectList.Add(GetTableAlias(tableName) + "." + columnName + " AS " + alias);
				AddTable(tableName);
				AddColumn(GetTableAlias(tableName) + "." + columnName);
			}
		}

		public void AddColumn(string tableName, string tableAliasName, string columnName, string alias)
		{
			if (!selectList.Contains(tableAliasName + "." + columnName + " AS " + alias))
			{
				selectList.Add(tableAliasName + "." + columnName + " AS " + alias);
				AddTable(tableName);
				AddColumn(tableAliasName + "." + columnName);
			}
		}

		public void RemoveAllColumns()
		{
			selectList.Clear();
		}

		public void AddNegativeColumn(string tableName, string columnName)
		{
			selectList.Add("-" + GetTableAlias(tableName) + "." + columnName + " AS " + columnName);
			AddTable(tableName);
			AddColumn("-" + GetTableAlias(tableName) + "." + columnName);
		}

		public void AddMultColumn(string tableName1, string columnName1, string tableName2, string columnName2, string alias)
		{
			selectList.Add("(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ") AS " + alias);
			AddTable(tableName1);
			AddTable(tableName2);
			AddColumn("(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ")");
		}

		public void AddNegativeMultColumn(string tableName1, string columnName1, string tableName2, string columnName2, string alias)
		{
			selectList.Add("-(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ") AS " + alias);
			AddTable(tableName1);
			AddTable(tableName2);
			AddColumn("-(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ")");
		}

		private void AddColumn(string name)
		{
			if (!columnList.Contains(name))
			{
				columnList.Add(name);
			}
		}

		public void AddMultColumn(string tableName1, string columnName1, string tableName2, string columnName2, string alias, AggregateFunctions aggregateFunction)
		{
			hasAggregateFunction = true;
			selectList.Add(Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ") AS " + alias);
			AddTable(tableName1);
			AddTable(tableName2);
		}

		public void AddSubtractColumn(string tableName1, string columnName1, string tableName2, string columnName2, string alias, AggregateFunctions aggregateFunction)
		{
			hasAggregateFunction = true;
			selectList.Add(Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName1) + "." + columnName1 + ") - " + Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName2) + "." + columnName2 + ") AS " + alias);
			AddTable(tableName1);
			AddTable(tableName2);
		}

		public void AddColumn(string tableName, string columnName, string alias, AggregateFunctions aggregateFunction)
		{
			hasAggregateFunction = true;
			selectList.Add(Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName) + "." + columnName + ") AS " + alias);
			AddTable(tableName);
		}

		public void AddColumn(string tableName, string columnName, AggregateFunctions aggregateFunction)
		{
			hasAggregateFunction = true;
			selectList.Add(Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName) + "." + columnName + ") AS " + columnName);
			AddTable(tableName);
		}

		public void AddNegativeColumn(string tableName, string columnName, AggregateFunctions aggregateFunction)
		{
			if (!selectList.Contains(tableName + "." + columnName))
			{
				hasAggregateFunction = true;
				selectList.Add("-" + Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName) + "." + columnName + ") AS " + columnName);
				AddTable(tableName);
			}
		}

		public void AddNegativeMultColumn(string tableName1, string columnName1, string tableName2, string columnName2, string alias, AggregateFunctions aggregateFunction)
		{
			hasAggregateFunction = true;
			selectList.Add("-" + Enum.GetName(typeof(AggregateFunctions), aggregateFunction) + "(" + GetTableAlias(tableName1) + "." + columnName1 + "*" + GetTableAlias(tableName2) + "." + columnName2 + ") AS " + alias);
			AddTable(tableName1);
			AddTable(tableName2);
		}

		public void AddOrderByColumn(string tableName, string columnName)
		{
			AddTable(tableName);
			orederByColumns.Add(new OrderBy(GetTableAlias(tableName) + "." + columnName, isAscending: true));
		}

		public void AddOrderByColumn(string tableName, string columnName, bool isAscending)
		{
			AddTable(tableName);
			orederByColumns.Add(new OrderBy(GetTableAlias(tableName) + "." + columnName, isAscending));
		}

		public void AddCommandHelper(CommandHelper cmdHelper)
		{
			AddTable(cmdHelper.TableName);
			foreach (CommandHelper sqlHelper in sqlHelpers)
			{
				sqlHelper.IsLastField = false;
			}
			sqlHelpers.Add(cmdHelper);
			isComparing = true;
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue, SqlDbType fieldType, bool isLastField)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = fieldType;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.IsLastField = isLastField;
			if (!isLastField)
			{
				commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.AND;
			}
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, DateTime date1, DateTime date2, bool isLastField)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = SqlDbType.DateTime;
			commandHelper.FieldValue = date1.ToShortDateString();
			commandHelper.FieldValue2 = date2.ToShortDateString();
			commandHelper.TableName = tableName;
			commandHelper.SqlOp.LogicalValueOp = LogicalValueOperator.BETWEEN;
			commandHelper.IsLastField = isLastField;
			if (!isLastField)
			{
				commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.AND;
			}
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue, bool isLastField)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.IsLastField = isLastField;
			if (!isLastField)
			{
				commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.AND;
			}
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue, SqlDbType fieldType)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = fieldType;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.IsLastField = true;
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = SqlDbType.Int;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.IsLastField = true;
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue, SqlDbType fieldType, LogicalFieldOperator logicalFieldOperator)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = fieldType;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.SqlOp.LogicalFieldOp = logicalFieldOperator;
			if (logicalFieldOperator != 0)
			{
				commandHelper.IsLastField = false;
			}
			AddCommandHelper(commandHelper);
		}

		public void AddSearchCondition(string tableName, string fieldName, object fieldValue, SqlDbType fieldType, LogicalValueOperator logicalValueOperator)
		{
			CommandHelper commandHelper = new CommandHelper();
			commandHelper.FieldName = fieldName;
			commandHelper.SqlFieldType = fieldType;
			commandHelper.FieldValue = fieldValue;
			commandHelper.TableName = tableName;
			commandHelper.SqlOp.LogicalValueOp = logicalValueOperator;
			AddCommandHelper(commandHelper);
		}

		private void AddWhereExpression(StringBuilder exp)
		{
			exp.Append(" WHERE ");
		}

		private void AddAllTables(StringBuilder exp)
		{
			if (joinType != 0)
			{
				exp.Append(sourceTable).Append(" ").Append(GetTableAlias(sourceTable));
				return;
			}
			StringEnumerator enumerator = tablesCols.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					exp.Append(current).Append(" ").Append(GetTableAlias(current))
						.Append(",");
				}
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
			exp.Remove(checked(exp.Length - 1), 1);
		}

		private void FixCommandHelper()
		{
			bool flag = false;
			int num = 0;
			CommandHelper commandHelper = null;
			checked
			{
				for (int i = 0; i < sqlHelpers.Count; i++)
				{
					commandHelper = (CommandHelper)sqlHelpers[i];
					if (commandHelper.SqlOp.GetLogicalFieldOperator() == LogicalFieldOperator.OR)
					{
						if (!flag)
						{
							commandHelper.HasOpenParanteses = true;
							flag = true;
							num = 1;
						}
					}
					else if (flag)
					{
						if (num > 1)
						{
							commandHelper.HasCloseParanteses = true;
							commandHelper.HasOpenParanteses = false;
						}
						else
						{
							commandHelper.HasCloseParanteses = false;
							commandHelper.HasOpenParanteses = false;
						}
						flag = false;
					}
					num++;
				}
				if (commandHelper != null && flag)
				{
					if (num > 2)
					{
						commandHelper.HasCloseParanteses = true;
						commandHelper.HasOpenParanteses = false;
					}
					else
					{
						commandHelper.HasCloseParanteses = false;
						commandHelper.HasOpenParanteses = false;
					}
				}
			}
		}

		public string GetWhereExpression()
		{
			StringBuilder stringBuilder = new StringBuilder();
			AddAllFields(stringBuilder);
			return stringBuilder.ToString();
		}

		private bool AddAllFields(StringBuilder exp)
		{
			bool result = false;
			FixCommandHelper();
			checked
			{
				for (int i = 0; i < sqlHelpers.Count; i++)
				{
					CommandHelper commandHelper = (CommandHelper)sqlHelpers[i];
					if (i == sqlHelpers.Count - 1)
					{
						commandHelper.SqlOp.LogicalFieldOp = LogicalFieldOperator.Null;
						commandHelper.IsLastField = true;
					}
					string expression = commandHelper.GetExpression();
					if (expression.Trim().Length > 0)
					{
						exp.Append(" ");
						exp.Append(expression);
						result = true;
					}
				}
				return result;
			}
		}

		public string GetSelectExpression()
		{
			StringBuilder stringBuilder = new StringBuilder();
			checked
			{
				if (selectList.Count > 0)
				{
					if (useDistict && topN <= 0)
					{
						stringBuilder.Append("SELECT DISTINCT ");
					}
					else if (topN > 0)
					{
						stringBuilder.Append("SELECT TOP ").Append(topN.ToString()).Append(" ");
					}
					else
					{
						stringBuilder.Append("SELECT ");
					}
					StringEnumerator enumerator = selectList.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							string current = enumerator.Current;
							stringBuilder.Append(current);
							stringBuilder.Append(',');
						}
					}
					finally
					{
						(enumerator as IDisposable)?.Dispose();
					}
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					stringBuilder.Append(" FROM ");
				}
				else if (topN > 0)
				{
					stringBuilder.Append("SELECT TOP ").Append(topN.ToString()).Append(" * FROM ");
				}
				else
				{
					stringBuilder.Append("SELECT * FROM ");
				}
				AddAllTables(stringBuilder);
				StringBuilder[] jointerExp = GetJointerExp();
				if (jointerExp != null)
				{
					StringBuilder[] array = jointerExp;
					foreach (StringBuilder stringBuilder2 in array)
					{
						stringBuilder.Append(stringBuilder2.ToString()).Append(" ");
					}
				}
				AddWhereExpression(stringBuilder);
				if (!AddAllFields(stringBuilder))
				{
					stringBuilder.Remove(stringBuilder.Length - 6, 6);
				}
				if (subqueryBuilder != null)
				{
					stringBuilder.Append(" ").Append("(").Append(subqueryBuilder.GetSelectExpression())
						.Append(")");
				}
				if (hasAggregateFunction)
				{
					AddGroupBy(stringBuilder);
				}
				AddOrderBy(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		private void AddOrderBy(StringBuilder exp)
		{
			if (orederByColumns.Count > 0)
			{
				exp.Append(" ORDER BY ");
			}
			foreach (OrderBy orederByColumn in orederByColumns)
			{
				string column = orederByColumn.column;
				column = ((!orederByColumn.isAscending) ? (column + " DESC") : (column + " ASC"));
				exp.Append(column).Append(",");
			}
			if (orederByColumns.Count > 0)
			{
				exp.Remove(checked(exp.Length - 1), 1);
			}
		}

		private void AddGroupBy(StringBuilder exp)
		{
			if (exp == null)
			{
				throw new ApplicationException("Expression is null.");
			}
			if (columnList.Count > 0)
			{
				exp.Append(" GROUP BY ");
				StringEnumerator enumerator = columnList.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						exp.Append(current).Append(",");
					}
				}
				finally
				{
					(enumerator as IDisposable)?.Dispose();
				}
				exp.Remove(checked(exp.Length - 1), 1);
			}
		}

		public bool TableExists(string srcTable)
		{
			StringEnumerator enumerator = tablesCols.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					if (srcTable.ToUpper() == current.ToUpper())
					{
						return true;
					}
				}
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
			return false;
		}
	}
}
