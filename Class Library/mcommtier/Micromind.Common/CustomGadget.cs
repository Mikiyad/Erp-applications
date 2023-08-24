using System;
using System.Collections.Generic;

namespace Micromind.Common
{
	[Serializable]
	public class CustomGadget
	{
		public List<CustomGadgetTable> Tables = new List<CustomGadgetTable>();

		public List<GadgetRelation> Relations = new List<GadgetRelation>();

		public List<GadgetParameter> Parameters = new List<GadgetParameter>();

		public List<CustomGadgetControl> Controls = new List<CustomGadgetControl>();
	}
}
