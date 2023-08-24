using System;

namespace Micromind.UISupport.Win32
{
	public struct TRACKMOUSEEVENTS
	{
		public uint cbSize;

		public uint dwFlags;

		public IntPtr hWnd;

		public uint dwHoverTime;
	}
}
