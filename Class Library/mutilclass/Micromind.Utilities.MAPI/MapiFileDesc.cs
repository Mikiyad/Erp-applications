using System;
using System.Runtime.InteropServices;

namespace Micromind.Utilities.MAPI
{
	[StructLayout(LayoutKind.Sequential)]
	public class MapiFileDesc
	{
		public int reserved;

		public int flags;

		public int position;

		public string path;

		public string name;

		public IntPtr type;
	}
}
