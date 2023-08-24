using System.Windows.Forms;

namespace Micromind.UISupport.Common
{
	public class ControlHelper
	{
		public static void RemoveAll(Control control)
		{
			if (control != null && control.Controls.Count > 0)
			{
				Button button = null;
				Form form = control.FindForm();
				if (form != null)
				{
					button = new Button();
					button.Visible = false;
					form.Controls.Add(button);
					form.ActiveControl = button;
				}
				control.Controls.Clear();
				if (form != null)
				{
					button.Dispose();
					form.Controls.Remove(button);
				}
			}
		}

		public static void Remove(Control.ControlCollection coll, Control item)
		{
			if (coll != null && item != null)
			{
				Button button = null;
				Form form = item.FindForm();
				if (form != null)
				{
					button = new Button();
					button.Visible = false;
					form.Controls.Add(button);
					form.ActiveControl = button;
				}
				coll.Remove(item);
				if (form != null)
				{
					button.Dispose();
					form.Controls.Remove(button);
				}
			}
		}

		public static void RemoveAt(Control.ControlCollection coll, int index)
		{
			if (coll != null && index >= 0 && index < coll.Count)
			{
				Remove(coll, coll[index]);
			}
		}

		public static void RemoveForm(Control source, Form form)
		{
			ContainerControl containerControl = source.FindForm();
			if (containerControl == null)
			{
				containerControl = (source as ContainerControl);
			}
			Button button = new Button();
			if (containerControl != null)
			{
				button.Visible = false;
				containerControl.Controls.Add(button);
				containerControl.ActiveControl = button;
			}
			form.Parent = null;
			if (containerControl != null)
			{
				button.Dispose();
				containerControl.Controls.Remove(button);
			}
		}
	}
}
