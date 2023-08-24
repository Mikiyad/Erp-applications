namespace Micromind.ClientLibraries
{
	public interface ICustomReportControlPlus : ICustomReportControl
	{
		string CustomReportKey1
		{
			get;
			set;
		}

		string GetParameterValue1();
	}
}
