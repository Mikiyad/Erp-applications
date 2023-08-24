using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DialogBoxBaseForm : Form, IBaseForm
	{
		private Container components;

		public DialogBoxBaseForm()
		{
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
            this.SuspendLayout();
            // 
            // DialogBoxBaseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(314, 169);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DialogBoxBaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dialog Box";
            this.ResumeLayout(false);

		}

		public void ApplyPreferences()
		{
		}

		protected void SetAcceptButton(XPButton button)
		{
			if (!ApplicationSettings.Preferences.General.PressingEnterMovesBetweenFields)
			{
				base.AcceptButton = button;
			}
			else
			{
				base.AcceptButton = null;
			}
		}

		public void InitDialog()
		{
			Translate();
			Font = new Font("Tahoma", Font.Size, Font.Style);
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			base.KeyPreview = true;
			if (ApplicationSettings.Preferences.General.PressingEnterMovesBetweenFields)
			{
				base.AcceptButton = null;
			}
			foreach (Control control3 in base.Controls)
			{
				if (control3 is Panel || control3 is BAPanel)
				{
					foreach (Control control4 in control3.Controls)
					{
						if (!(control4 is Label) && !(control4 is MMLabel) && !(control4 is RichTextBox) && !(control4 is LinkLabel) && !(control4 is BALinkLabel) && (control3 is Button || control3 is XPButton))
						{
							control3.Height = 24;
						}
					}
				}
				else if (!(control3 is Label) && !(control3 is MMLabel) && !(control3 is RichTextBox) && !(control3 is LinkLabel) && !(control3 is BALinkLabel))
				{
					BaseHelper.InitControl(control3);
				}
			}
		}

		public void Translate()
		{
			Translator.Translators.Translate(this);
		}
	}
}
