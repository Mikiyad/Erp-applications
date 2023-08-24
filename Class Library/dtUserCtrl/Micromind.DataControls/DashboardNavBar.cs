using DevExpress.Utils;
using DevExpress.XtraNavBar;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class DashboardNavBar : NavBarControl
	{
		private NavBarHitInfo downHitInfo;

		private bool isResizing;

		private bool isMoving;

		private Point mouseDownPoint;

		private NavBarGroup resizingNavGroup;

		private IContainer components;

		public DashboardNavBar()
		{
			InitializeComponent();
			BackColor = Color.White;
		}

		public DashboardNavBar(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		private void DashboardNavBar_MouseLeave(object sender, EventArgs e)
		{
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Cursor = Cursors.Arrow;
			isResizing = false;
			resizingNavGroup = null;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (isResizing && resizingNavGroup != null)
			{
				Rectangle rectangle = resizingNavGroup.ControlContainer.RectangleToScreen(resizingNavGroup.ControlContainer.Bounds);
				Point point = PointToScreen(e.Location);
				if (rectangle.Top < point.Y)
				{
					resizingNavGroup.GroupClientHeight = point.Y - rectangle.Top;
				}
				isResizing = false;
				resizingNavGroup = null;
			}
			isMoving = false;
		}

		private void RecalculateSize()
		{
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			NavBarHitInfo navBarHitInfo = CalcHitInfo(new Point(e.X, e.Y));
			if (e.Button == MouseButtons.Left && navBarHitInfo.InGroupCaption)
			{
				downHitInfo = navBarHitInfo;
			}
			if (resizingNavGroup != null)
			{
				mouseDownPoint = e.Location;
				isResizing = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			NavBarHitInfo navBarHitInfo = CalcHitInfo(e.Location);
			_ = navBarHitInfo.Group;
			if (e.Button == MouseButtons.Left && downHitInfo != null)
			{
				Size dragSize = SystemInformation.DragSize;
				Rectangle rectangle = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2, downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
				isMoving = true;
				if (!rectangle.Contains(new Point(e.X, e.Y)))
				{
					DoDragDrop(downHitInfo.Group, DragDropEffects.Move);
					downHitInfo = null;
					DXMouseEventArgs.GetMouseArgs(e).Handled = true;
				}
			}
			if (isResizing || isMoving)
			{
				return;
			}
			if (navBarHitInfo.Group != null && navBarHitInfo.Group.GroupStyle == NavBarGroupStyle.ControlContainer && navBarHitInfo.HitTest == NavBarHitTest.GroupClient)
			{
				int bottom = GetViewInfo().GetGroupInfo(navBarHitInfo.Group).ClientInfoBounds.Bottom;
				if (bottom < e.Y + 8 && bottom > e.Y - 8)
				{
					resizingNavGroup = navBarHitInfo.Group;
					Cursor = Cursors.SizeNS;
					return;
				}
			}
			resizingNavGroup = null;
			Cursor = Cursors.Arrow;
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);
			Point p = PointToClient(new Point(e.X, e.Y));
			NavBarHitInfo navBarHitInfo = CalcHitInfo(p);
			navBarHitInfo.HitTest.ToString();
			if (navBarHitInfo.InGroup && e.Data.GetDataPresent(typeof(NavBarGroup)))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			NavBarGroup navBarGroup = (NavBarGroup)e.Data.GetData(typeof(NavBarGroup));
			if (navBarGroup != null)
			{
				Point p = PointToClient(new Point(e.X, e.Y));
				NavBarGroup group = CalcHitInfo(p).Group;
				if (group != null)
				{
					Groups.Move(Groups.IndexOf(navBarGroup), Groups.IndexOf(group));
				}
			}
		}

		public IGadget GetGroupGadget(NavBarGroup group)
		{
			if (group.ControlContainer != null)
			{
				foreach (Control control in group.ControlContainer.Controls)
				{
					if (control != null)
					{
						return (IGadget)control;
					}
				}
				return null;
			}
			if (group.Tag != null)
			{
				ILinkGadget linkGadget = (ILinkGadget)group.Tag;
				linkGadget.Links.Clear();
				foreach (NavBarItemLink itemLink in group.ItemLinks)
				{
					if (itemLink.Item.Tag != null)
					{
						linkGadget.Links.Add((Link)itemLink.Item.Tag);
					}
				}
				return (IGadget)group.Tag;
			}
			return null;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			Appearance.Background.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
			Appearance.Background.Options.UseBackColor = true;
			Appearance.GroupHeader.BackColor = System.Drawing.Color.White;
			Appearance.GroupHeader.Options.UseBackColor = true;
			BackColor = System.Drawing.Color.White;
			DragDropFlags = (DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop);
			base.OptionsNavPane.ExpandedWidth = 140;
			base.StoreDefaultPaintStyleName = true;
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
