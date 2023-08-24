using System;
using System.Collections.Generic;
using System.IO;

namespace Micromind.DataControls.FlatDashboard
{
	[Serializable]
	public class DashboardLayout : MarshalByRefObject
	{
		public List<GroupLayout> GadgetsList = new List<GroupLayout>();

		public MemoryStream Layout;
	}
}
