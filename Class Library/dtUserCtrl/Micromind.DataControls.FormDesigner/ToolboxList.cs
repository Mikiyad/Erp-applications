using System;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.FormDesigner
{
	internal class ToolboxList : ListBox
	{
		private int itemUnderMouse = -1;

		private int curSelected = -1;

		private Color umColor = SystemColors.InactiveCaptionText;

		private Color umSelColor = SystemColors.InactiveCaption;

		private Color selColor = Color.AliceBlue;

		private Color groupColor = SystemColors.ControlDark;

		private Color frameColor = Color.Black;

		private readonly int DragDistance = 3;

		private Point mouseClickOrigin;

		private int groupControlHeightDiff = 5;

		private ImageList imageList;

		public Color ItemUnderMouseColor
		{
			get
			{
				return umColor;
			}
			set
			{
				umColor = value;
			}
		}

		public Color SelectedItemUnderMouseColor
		{
			get
			{
				return umSelColor;
			}
			set
			{
				umSelColor = value;
			}
		}

		public Color SelectedItemColor
		{
			get
			{
				return selColor;
			}
			set
			{
				selColor = value;
			}
		}

		public Color FrameColor
		{
			get
			{
				return frameColor;
			}
			set
			{
				frameColor = value;
			}
		}

		public Color GroupColor
		{
			get
			{
				return groupColor;
			}
			set
			{
				groupColor = value;
			}
		}

		public ToolboxItem CurrentSelected
		{
			get
			{
				if (curSelected < 0)
				{
					return null;
				}
				return base.Items[curSelected] as ToolboxItem;
			}
			set
			{
				int num = 0;
				foreach (ToolboxItem item in base.Items)
				{
					if (item == value)
					{
						ChangeSelection(num);
						break;
					}
					num++;
				}
			}
		}

		public int CurrentSelectedIndex
		{
			get
			{
				return curSelected;
			}
			set
			{
				ChangeSelection(value);
			}
		}

		public ImageList Images
		{
			get
			{
				return imageList;
			}
			set
			{
				imageList = value;
			}
		}

		public event ItemDragEventHandler ItemDrag;

		private int GetItemAt(Point pt)
		{
			int i = base.TopIndex;
			for (int count = base.Items.Count; i < count; i++)
			{
				if (GetItemRectangle(i).Contains(pt))
				{
					return i;
				}
			}
			return -1;
		}

		private int Distance(Point p1, Point p2)
		{
			int num = p1.X - p2.X;
			int num2 = p1.Y - p2.Y;
			return (int)Math.Sqrt(num * num + num2 * num2);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0)
			{
				if (Distance(mouseClickOrigin, Control.MousePosition) > DragDistance && curSelected >= 0 && this.ItemDrag != null)
				{
					ToolboxItem item = base.Items[curSelected] as ToolboxItem;
					this.ItemDrag(this, new ItemDragEventArgs(item));
				}
				return;
			}
			Point pt = PointToClient(Control.MousePosition);
			int itemAt = GetItemAt(pt);
			if (itemAt != -1 && itemUnderMouse != itemAt)
			{
				_ = base.Items[itemAt];
				if (itemUnderMouse != -1)
				{
					PaintItem(itemUnderMouse, underMouse: false, null);
				}
				itemUnderMouse = itemAt;
				PaintItem(itemUnderMouse, underMouse: true, null);
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (itemUnderMouse != -1)
			{
				PaintItem(itemUnderMouse, underMouse: false, null);
				itemUnderMouse = -1;
			}
			base.OnMouseLeave(e);
		}

		protected void PaintItem(int item, bool underMouse, Graphics graphics)
		{
			Graphics graphics2 = (graphics == null) ? Graphics.FromHwnd(base.Handle) : graphics;
			Rectangle itemRectangle = GetItemRectangle(item);
			bool flag = item == curSelected;
			ToolboxItem toolboxItem = base.Items[item] as ToolboxItem;
			string text = toolboxItem.text;
			underMouse = (underMouse && !toolboxItem.groupControl);
			Color color = BackColor;
			if (toolboxItem.groupControl && flag)
			{
				color = SelectedItemColor;
			}
			else if (toolboxItem.groupControl)
			{
				color = GroupColor;
			}
			else if (underMouse && flag)
			{
				color = SelectedItemUnderMouseColor;
			}
			else if (underMouse)
			{
				color = ItemUnderMouseColor;
			}
			else if (flag)
			{
				color = SelectedItemColor;
			}
			Brush brush = new SolidBrush(color);
			Brush brush2 = new SolidBrush(ForeColor);
			graphics2.FillRectangle(brush, itemRectangle);
			if (underMouse | flag)
			{
				Pen pen = new Pen(FrameColor, 1f);
				itemRectangle.Size = new Size(itemRectangle.Size.Width - 1, itemRectangle.Size.Height - 1);
				graphics2.DrawRectangle(pen, itemRectangle);
				pen.Dispose();
			}
			itemRectangle = GetItemRectangle(item);
			int num = 0;
			int num2 = 0;
			if (imageList != null)
			{
				num = imageList.ImageSize.Height;
				num2 = imageList.ImageSize.Width;
				int num3 = (!toolboxItem.groupControl) ? 2 : 0;
				int x = itemRectangle.Left + num3;
				if (RightToLeft == RightToLeft.Yes)
				{
					x = itemRectangle.Right - num3 - num2;
				}
				if (toolboxItem.image >= 0)
				{
					imageList.Draw(graphics2, x, itemRectangle.Top + num3, num2, num, toolboxItem.image);
				}
			}
			SizeF sizeF = graphics2.MeasureString(text, Font);
			int num4 = (itemRectangle.Height - (int)sizeF.Height) / 2;
			int num5 = 1;
			Font font = Font;
			if (toolboxItem.groupControl)
			{
				font = new Font(font.FontFamily, font.Size, FontStyle.Bold);
			}
			StringFormat stringFormat = new StringFormat();
			if (RightToLeft == RightToLeft.Yes)
			{
				stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				itemRectangle.Size = new Size(itemRectangle.Width - num2 - 2, itemRectangle.Height);
			}
			else
			{
				itemRectangle.Location = new Point(itemRectangle.Left + num2 + 2, itemRectangle.Top);
			}
			itemRectangle.Inflate(-num5, -num4);
			graphics2.DrawString(text, font, brush2, itemRectangle, stringFormat);
			brush2.Dispose();
			brush.Dispose();
			if (toolboxItem.groupControl)
			{
				font.Dispose();
			}
			if (graphics == null)
			{
				graphics2.Dispose();
			}
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index >= 0 && e.Index < base.Items.Count)
			{
				Point pt = PointToClient(Control.MousePosition);
				bool underMouse = GetItemRectangle(e.Index).Contains(pt) & (Control.MouseButtons == MouseButtons.None);
				PaintItem(e.Index, underMouse, e.Graphics);
			}
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (e.Index >= 0 && e.Index < base.Items.Count)
			{
				ToolboxItem toolboxItem = base.Items[e.Index] as ToolboxItem;
				e.ItemWidth = base.ClientSize.Width;
				e.ItemHeight = (toolboxItem.groupControl ? (ItemHeight - groupControlHeightDiff) : ItemHeight);
			}
		}

		private void ChangeSelection(int newSelected)
		{
			if (newSelected != curSelected)
			{
				if (curSelected >= 0)
				{
					int item = curSelected;
					curSelected = -1;
					PaintItem(item, underMouse: false, null);
				}
				if (newSelected >= 0)
				{
					curSelected = newSelected;
					PaintItem(curSelected, underMouse: false, null);
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			mouseClickOrigin = Control.MousePosition;
			int itemAt = GetItemAt(PointToClient(Control.MousePosition));
			if (itemAt >= 0)
			{
				ChangeSelection(itemAt);
			}
			base.OnMouseDown(e);
		}
	}
}
