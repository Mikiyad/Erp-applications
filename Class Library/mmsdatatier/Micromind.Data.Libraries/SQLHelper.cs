namespace Micromind.Data.Libraries
{
	public static class SQLHelper
	{
		public static bool ValidateQuerySecurity(string query)
		{
			if (query.ToLower().Contains("delete ") || query.ToLower().Contains("insert ") || query.ToLower().Contains("update ") || query.ToLower().Contains("insert ") || query.ToLower().Contains("drop ") || query.ToLower().Contains("alter ") || query.ToLower().Contains("truncate ") || query.ToLower().Contains("exec "))
			{
				return false;
			}
			return true;
		}
	}
}
