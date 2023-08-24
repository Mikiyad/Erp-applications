using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others.HelpSupports
{
	public class WarningMessageDialog : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private ErrorHelperDialogResult result = new ErrorHelperDialogResult();

		private CheckBox checkBox1;

		private PictureBox pictureBox1;

		private Label labelMessage;

		private Line line1;

		private XPButton buttonYes;

		private XPButton buttonNo;

		private Container components;

		public WarningMessageDialog()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Others.HelpSupports.WarningMessageDialog));
			checkBox1 = new System.Windows.Forms.CheckBox();
			labelMessage = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			line1 = new Micromind.UISupport.Line();
			buttonYes = new Micromind.UISupport.XPButton();
			buttonNo = new Micromind.UISupport.XPButton();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBox1.Location = new System.Drawing.Point(28, 97);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(324, 25);
			checkBox1.TabIndex = 0;
			checkBox1.Text = "In the future, do not show this warning";
			labelMessage.Location = new System.Drawing.Point(48, 9);
			labelMessage.Name = "labelMessage";
			labelMessage.Size = new System.Drawing.Size(312, 85);
			labelMessage.TabIndex = 1;
			labelMessage.Text = "labelMessage";
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(8, 14);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(34, 34);
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.CornflowerBlue;
			line1.Location = new System.Drawing.Point(8, 128);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(360, 1);
			line1.TabIndex = 20;
			line1.TabStop = false;
			buttonYes.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonYes.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonYes.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonYes.Location = new System.Drawing.Point(288, 134);
			buttonYes.Name = "buttonYes";
			buttonYes.Size = new System.Drawing.Size(75, 19);
			buttonYes.TabIndex = 1;
			buttonYes.Text = "&No";
			buttonYes.Click += new System.EventHandler(buttonYes_Click);
			buttonNo.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNo.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNo.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNo.Location = new System.Drawing.Point(207, 134);
			buttonNo.Name = "buttonNo";
			buttonNo.Size = new System.Drawing.Size(75, 19);
			buttonNo.TabIndex = 0;
			buttonNo.Text = "&Yes";
			buttonNo.Click += new System.EventHandler(buttonNo_Click);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.LightGray;
			base.ClientSize = new System.Drawing.Size(376, 169);
			base.Controls.Add(buttonNo);
			base.Controls.Add(buttonYes);
			base.Controls.Add(line1);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(labelMessage);
			base.Controls.Add(checkBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WarningMessageDialog";
			base.Load += new System.EventHandler(WarningMessageDialog_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		public ErrorHelperDialogResult ShowDialog(Form owner, string message, MessageBoxButtons buttons)
		{
			labelMessage.Text = message;
			Text = Application.ProductName;
			checked
			{
				if (buttons == MessageBoxButtons.OK)
				{
					buttonYes.Text = "&OK";
					buttonYes.Left = unchecked(base.Width / 2) - unchecked(buttonYes.Width / 2);
					buttonNo.Visible = false;
				}
				ShowDialog(owner);
				return result;
			}
		}

		private void WarningMessageDialog_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonYes_Click(object sender, EventArgs e)
		{
			result.DontShowAgain = checkBox1.Checked;
			result.Answer = DialogResult.Yes;
			Close();
		}

		private void buttonNo_Click(object sender, EventArgs e)
		{
			result.DontShowAgain = checkBox1.Checked;
			result.Answer = DialogResult.No;
			Close();
		}

		private void buttonNo_Click_1(object sender, EventArgs e)
		{
		}
	}
}
