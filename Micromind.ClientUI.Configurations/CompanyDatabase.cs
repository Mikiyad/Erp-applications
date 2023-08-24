namespace Micromind.ClientUI.Configurations
{
	public class CompanyDatabase
	{
		private string companyName = "";

		private string databaseName = "";

		public bool OperationSucceeded;

		public bool ConnectDatabase = true;

		public string userName = "";

		public string password = "";

		public string server = "";

		public string port = "";

		public string CompanyName
		{
			get
			{
				return companyName;
			}
			set
			{
				companyName = value;
			}
		}

		public string DatabaseName
		{
			get
			{
				return databaseName;
			}
			set
			{
				databaseName = value;
			}
		}
	}
}
