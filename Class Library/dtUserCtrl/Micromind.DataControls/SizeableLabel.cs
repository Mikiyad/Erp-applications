using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SizeableLabel : Label
	{
		private bool isResizing;

		private IContainer components;

		public SizeableLabel()
		{
			InitializeComponent();
			base.SizeChanged += SizeableLabel_SizeChanged;
			base.TextChanged += SizeableLabel_TextChanged;
			base.AutoEllipsis = true;
		}

		private void SizeableLabel_TextChanged(object sender, EventArgs e)
		{
			DoResize();
		}

		private void SizeableLabel_SizeChanged(object sender, EventArgs e)
		{
			DoResize();
		}

		private void DoResize()
		{
			try
			{
				int num;
				if (!isResizing)
				{
					isResizing = true;
					string text = Text;
					if (text.Length > 0)
					{
						num = 100;
						int num2 = DisplayRectangle.Width - 10;
						int num3 = DisplayRectangle.Height - 3;
						using (Graphics graphics = CreateGraphics())
						{
							for (int i = 1; i <= 100; i++)
							{
								using (Font font = new Font(Font.FontFamily, i))
								{
									SizeF sizeF = graphics.MeasureString(text, font);
									if (sizeF.Width > (float)num2 || sizeF.Height > (float)num3)
									{
										num = i - 1;
										goto IL_00c2;
									}
								}
							}
						}
						goto IL_00c2;
					}
				}
				goto end_IL_0000;
				IL_00c2:
				if (num < 4)
				{
					num = 4;
				}
				if (num > 200)
				{
					num = 200;
				}
				Font = new Font(Font.FontFamily, num);
				end_IL_0000:;
			}
			catch
			{
			}
			finally
			{
				isResizing = false;
			}
		}

		public SizeableLabel(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
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
