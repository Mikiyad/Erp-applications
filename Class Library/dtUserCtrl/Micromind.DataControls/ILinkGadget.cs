using DevExpress.XtraNavBar;

namespace Micromind.DataControls
{
	public interface ILinkGadget
	{
		LinksCollection Links
		{
			get;
			set;
		}

		NavBarGroup ParentNavBarGroup
		{
			get;
			set;
		}
	}
}
