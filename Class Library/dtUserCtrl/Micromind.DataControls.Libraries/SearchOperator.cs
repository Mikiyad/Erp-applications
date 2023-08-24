using Micromind.Common.Libraries;

namespace Micromind.DataControls.Libraries
{
	public class SearchOperator
	{
		private SearchOperators op = SearchOperators.Equal;

		public string OperatorText => GetOperatorText(op);

		public SearchOperators Operator
		{
			get
			{
				return op;
			}
			set
			{
				op = value;
			}
		}

		public SearchOperator()
		{
		}

		public SearchOperator(SearchOperators op)
		{
			this.op = op;
		}

		public static string GetOperatorText(SearchOperators op)
		{
			switch (op)
			{
			default:
				return "Equal";
			case SearchOperators.NotEqual:
				return "Not Equal";
			case SearchOperators.LessThan:
				return "Less Than";
			case SearchOperators.LessThanEqual:
				return "Less Than Equal";
			case SearchOperators.GreaterThan:
				return "Greater Than";
			case SearchOperators.GreatherThanEqual:
				return "Greater Than Equal";
			case SearchOperators.Contains:
				return "Contains";
			case SearchOperators.BeginsWith:
				return "Begins With";
			case SearchOperators.EndsWith:
				return "Ends With";
			}
		}

		public override string ToString()
		{
			return OperatorText;
		}
	}
}
