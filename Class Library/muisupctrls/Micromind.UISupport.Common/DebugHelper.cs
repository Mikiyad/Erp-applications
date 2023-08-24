using System;
using System.Windows.Forms;

namespace Micromind.UISupport.Common
{
	public class DebugHelper
	{
		public static void ListControls(Control.ControlCollection controls)
		{
			ListControls("Control Collection", controls, fullName: false);
		}

		public static void ListControls(string title, Control.ControlCollection controls)
		{
			ListControls(title, controls, fullName: false);
		}

		public static void ListControls(string title, Control.ControlCollection controls, bool fullName)
		{
			Console.WriteLine("\n{0}", title);
			int count = controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = controls[i];
				string text = (!fullName) ? control.GetType().Name : control.GetType().FullName;
				Console.WriteLine("{0} V:{1} T:{2} N:{3}", i, control.Visible, text, control.Name);
			}
		}
	}
}
