using System;
using System.Drawing;

namespace Micromind.UISupport.Win32
{
	public struct PAINTSTRUCT
	{
		public IntPtr hdc;

		public int fErase;

		public Rectangle rcPaint;

		public int fRestore;

		public int fIncUpdate;

		public int Reserved1;

		public int Reserved2;

		public int Reserved3;

		public int Reserved4;

		public int Reserved5;

		public int Reserved6;

		public int Reserved7;

		public int Reserved8;
	}
}
