using System;

namespace Micromind.DataControls
{
	[Serializable]
	public class Link
	{
		public string DisplayName = "";

		public string TargetScreen = "";

		public string Group = "";

		public string SubGroup = "";

		public int Index;

		public Link(string targetScreen, string displayName, string group, string subGroup, int index)
		{
			DisplayName = displayName;
			TargetScreen = targetScreen;
			Group = group;
			SubGroup = subGroup;
			Index = index;
		}

		public Link(string targetScreen, string displayName, string group, string subGroup)
		{
			DisplayName = displayName;
			TargetScreen = targetScreen;
			Group = group;
			SubGroup = subGroup;
		}

		public Link()
		{
		}

		public override string ToString()
		{
			if (DisplayName == "")
			{
				return "Link";
			}
			return DisplayName;
		}
	}
}
