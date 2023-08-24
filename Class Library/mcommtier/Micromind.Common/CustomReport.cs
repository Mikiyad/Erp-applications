using System;
using System.Collections.Generic;

namespace Micromind.Common
{
	[Serializable]
	public class CustomReport
	{
		public List<CustomReportTable> Tables = new List<CustomReportTable>();

		public List<ReportRelation> Relations = new List<ReportRelation>();

		public List<ReportParameter> Parameters = new List<ReportParameter>();

		public List<CustomReportControl> Controls = new List<CustomReportControl>();
	}
}
