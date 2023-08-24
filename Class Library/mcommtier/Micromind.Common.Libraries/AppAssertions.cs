using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Micromind.Common.Libraries
{
	public sealed class AppAssertions
	{
		[Conditional("DEBUG")]
		public static void AssertParamColHasAllParams(SqlBuilder sqlBuilder, SqlParameterCollection paramCols)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FieldValue fieldValue in sqlBuilder.FieldValues)
			{
				if (!paramCols.Contains(fieldValue.Value))
				{
					stringBuilder.Append(fieldValue.Value + " does not exits.\n");
				}
			}
			if (stringBuilder.Length != 0)
			{
				throw new ApplicationException(stringBuilder.ToString());
			}
		}

		[Conditional("DEBUG")]
		public static void CheckNullCondition(object obj)
		{
			if (obj == null)
			{
				throw new NullReferenceException();
			}
		}

		[Conditional("DEBUG")]
		public static void CheckNullCondition(object obj, string objName)
		{
			if (obj == null)
			{
				throw new NullReferenceException(objName + " is null.");
			}
		}
	}
}
