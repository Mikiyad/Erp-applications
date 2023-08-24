using DevExpress.XtraCharts;

namespace Micromind.DataControls
{
	public interface ICustomGadget
	{
		GroupLayout GroupLayout
		{
			get;
			set;
		}

		GadgetFilterOptions FilterOption
		{
			get;
			set;
		}

		ViewType ChartType
		{
			get;
			set;
		}

		void Init();
	}
}
