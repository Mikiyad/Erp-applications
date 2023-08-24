using Micromind.ClientUI.Libraries;
using Micromind.UISupport;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public sealed class BaseHelper
	{
		internal static void InitLeftPanel(Panel panel)
		{
			checked
			{
				if (panel != null && panel.Name == "panelLeft")
				{
					UILib.AddPanelLine(panel);
					foreach (Control control in panel.Controls)
					{
						if (control.GetType() == typeof(LinkLabel))
						{
							LinkLabel linkLabel = control as LinkLabel;
							if (!linkLabel.AutoSize && linkLabel.Size.Height == 24)
							{
								linkLabel.Height = 26;
							}
							LinkArea linkArea = linkLabel.LinkArea;
							linkArea.Start = 0;
							linkArea.Length = linkLabel.Text.Length + 50;
							linkLabel.LinkArea = linkArea;
						}
						else
						{
							_ = (control.GetType() == typeof(Line));
						}
						if (control.Name == "labelLastModified")
						{
							control.Dock = DockStyle.None;
							control.Left = 0;
							control.Height = 50;
							control.Top = panel.Height - control.Height;
							control.Width = panel.Width - 2;
							control.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
							control.Font = new Font("Tahoma", 8.25f);
						}
					}
				}
			}
		}

		internal static void InitControl(Control c)
		{
			if (c.GetType() == typeof(Label) || c.GetType() == typeof(MMLabel))
			{
				return;
			}
			if (c.GetType() == typeof(XPButton) || c.GetType() == typeof(Button))
			{
				c.Height = 24;
			}
			else if (c.GetType() == typeof(MMTextBox))
			{
				MMTextBox mMTextBox = c as MMTextBox;
				if (mMTextBox.Multiline)
				{
					mMTextBox.AcceptsReturn = true;
				}
				else
				{
					mMTextBox.AcceptsReturn = false;
				}
			}
		}

		internal static void InitControls(Control control)
		{
			if (control.GetType() == typeof(Panel) || control.GetType() == typeof(BAPanel))
			{
				foreach (Control control2 in control.Controls)
				{
					InitControls(control2);
				}
			}
			InitControl(control);
		}
	}
}
