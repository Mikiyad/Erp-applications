using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Micromind.UISupport.Controls
{
	public class TabControlDesigner : ParentControlDesigner
	{
		private ISelectionService _selectionService;

		public override ICollection AssociatedComponents
		{
			get
			{
				if (base.Control is TabControl)
				{
					return ((TabControl)base.Control).TabPages;
				}
				return base.AssociatedComponents;
			}
		}

		public ISelectionService SelectionService
		{
			get
			{
				if (_selectionService == null)
				{
					_selectionService = (ISelectionService)GetService(typeof(ISelectionService));
				}
				return _selectionService;
			}
		}

		protected override bool DrawGrid => false;

		protected override void WndProc(ref Message msg)
		{
			if (msg.Msg == 513)
			{
				TabControl tabControl = SelectionService.PrimarySelection as TabControl;
				if (tabControl != null)
				{
					int x = (short)((int)msg.LParam & 0xFFFF);
					int y = (short)((uint)((int)msg.LParam & -65536) >> 16);
					tabControl.ExternalMouseTest(msg.HWnd, new Point(x, y));
				}
			}
			else if (msg.Msg == 515)
			{
				TabControl tabControl2 = SelectionService.PrimarySelection as TabControl;
				if (tabControl2 != null)
				{
					int x2 = (short)((int)msg.LParam & 0xFFFF);
					int y2 = (short)((uint)((int)msg.LParam & -65536) >> 16);
					if (tabControl2.WantDoubleClick(msg.HWnd, new Point(x2, y2)))
					{
						return;
					}
				}
			}
			base.WndProc(ref msg);
		}
	}
}
