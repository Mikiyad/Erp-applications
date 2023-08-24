using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomizePanelItem : SimpleButton
	{
		private bool _isDragging;

		private int _DDradius = 40;

		private int _mX;

		private int _mY;

		private IContainer components;

		public bool AllowDrag
		{
			get;
			set;
		}

		public GadgetTypes GadgetType => ((GroupLayout)base.Tag).GadgetType;

		public new GroupLayout Layout
		{
			get
			{
				return (GroupLayout)base.Tag;
			}
			set
			{
				base.Tag = value;
				Text = value.Title;
			}
		}

		public CustomizePanelItem()
		{
			AllowDrag = true;
			base.Size = new Size(235, 37);
			MinimumSize = new Size(100, 37);
			MaximumSize = new Size(500, 37);
			Appearance.TextOptions.HAlignment = HorzAlignment.Near;
			base.ParentChanged += CustomizePanelItem_ParentChanged;
			Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
		}

		private void CustomizePanelItem_ParentChanged(object sender, EventArgs e)
		{
			if (base.Parent != null)
			{
				base.Width = base.Parent.Width - 24;
				base.Parent.Resize += Parent_Resize;
			}
		}

		private void Parent_Resize(object sender, EventArgs e)
		{
			if (base.Parent != null)
			{
				base.Width = base.Parent.Width - 24;
				base.Parent.PerformLayout();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
			_mX = e.X;
			_mY = e.Y;
			_isDragging = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!_isDragging && e.Button == MouseButtons.Left && _DDradius > 0 && AllowDrag)
			{
				int num = _mX - e.X;
				int num2 = _mY - e.Y;
				if (num * num + num2 * num2 > _DDradius)
				{
					DoDragDrop(this, DragDropEffects.All);
					_isDragging = true;
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_isDragging = false;
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
			components = new System.ComponentModel.Container();
		}
	}
}
