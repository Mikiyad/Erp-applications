using DevExpress.XtraPivotGrid;

namespace Micromind.ClientUI.Reports
{
	public class PivotFieldDesign
	{
		private PivotGroupInterval groupInterval = PivotGroupInterval.Date;

		public PivotGroupInterval GroupInterval
		{
			get
			{
				return groupInterval;
			}
			set
			{
				groupInterval = value;
			}
		}
	}
}
