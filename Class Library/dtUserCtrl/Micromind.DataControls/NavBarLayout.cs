using System;
using System.Collections.Generic;

namespace Micromind.DataControls
{
	[Serializable]
	public class NavBarLayout
	{
		public int Width = -1;

		public List<GroupLayout> GroupLayoutList = new List<GroupLayout>();
	}
}
