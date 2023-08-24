using System;

namespace Micromind.DataControls
{
	[Serializable]
	public class DashboardLayout : MarshalByRefObject
	{
		private bool isThreeColumn;

		private bool isCrossColumnRow;

		private NavBarLayout leftNavBarLayout = new NavBarLayout();

		private NavBarLayout rightNavBarLayout = new NavBarLayout();

		private NavBarLayout lastNavBarLayout = new NavBarLayout();

		private NavBarLayout crossNavBarLayout = new NavBarLayout();

		public bool IsCrossColumnRow
		{
			get
			{
				return isCrossColumnRow;
			}
			set
			{
				isCrossColumnRow = value;
			}
		}

		public bool IsThreeColumn
		{
			get
			{
				return isThreeColumn;
			}
			set
			{
				isThreeColumn = value;
			}
		}

		public NavBarLayout LeftBarLayout => leftNavBarLayout;

		public NavBarLayout LastBarLayout
		{
			get
			{
				if (lastNavBarLayout == null)
				{
					lastNavBarLayout = new NavBarLayout();
				}
				return lastNavBarLayout;
			}
		}

		public NavBarLayout CrossBarLayout
		{
			get
			{
				if (crossNavBarLayout == null)
				{
					crossNavBarLayout = new NavBarLayout();
				}
				return crossNavBarLayout;
			}
		}

		public NavBarLayout RightBarLayout => rightNavBarLayout;
	}
}
