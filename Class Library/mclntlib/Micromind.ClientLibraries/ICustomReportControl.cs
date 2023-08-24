namespace Micromind.ClientLibraries
{
	public interface ICustomReportControl
	{
		string CustomReportFieldName
		{
			get;
			set;
		}

		string CustomReportKey
		{
			get;
			set;
		}

		byte CustomReportValueType
		{
			get;
			set;
		}

		string GetParameterValue();
	}
}
