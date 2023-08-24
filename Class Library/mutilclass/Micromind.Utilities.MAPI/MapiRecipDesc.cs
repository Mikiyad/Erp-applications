using System;
using System.Runtime.InteropServices;

namespace Micromind.Utilities.MAPI
{
	[StructLayout(LayoutKind.Sequential)]
	public class MapiRecipDesc
	{
		public int reserved;

		public int recipClass;

		public string name;

		public string address;

		public int eIDSize;

		public IntPtr entryID;
	}
}
