using Micromind.DataControls.FlatDashboard;
using System.Drawing;

namespace Micromind.DataControls
{
	public interface IGadget
	{
		Micromind.DataControls.FlatDashboard.FlatDashboard ParentDashboard
		{
			get;
			set;
		}

		bool IsBusy
		{
			get;
		}

		string GadgetTitle
		{
			get;
			set;
		}

		Image Icon
		{
			get;
		}

		GadgetStyles GadgetStyle
		{
			get;
			set;
		}

		GadgetCategories Category
		{
			get;
			set;
		}

		string Description
		{
			get;
			set;
		}

		GadgetTypes GadgetType
		{
			get;
		}

		string GadgetID
		{
			get;
			set;
		}

		void LoadData(bool isRefresh);
	}
}
