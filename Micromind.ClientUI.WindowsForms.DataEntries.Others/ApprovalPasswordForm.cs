using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ApprovalPasswordForm : Form
	{
		public string CorrectPassword = "";

		private IContainer components;

		private Label label1;

		private TextBox textBoxPassword;

		private Button buttonOK;

		private Button buttonCancel;

		public ApprovalPasswordForm()
		{
			InitializeComponent();
		}

		private void ApprovalPasswordForm_Activated(object sender, EventArgs e)
		{
			textBoxPassword.Focus();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxPassword.Text == CorrectPassword)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				ErrorHelper.ErrorMessage("Invalid Password.");
				textBoxPassword.Clear();
				textBoxPassword.Focus();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void ApprovalPasswordForm_Load(object sender, EventArgs e)
		{
			try
			{
				base.DialogResult = DialogResult.None;
			}
			catch
			{
			}
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
			label1 = new System.Windows.Forms.Label();
			textBoxPassword = new System.Windows.Forms.TextBox();
			buttonOK = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 20);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(56, 13);
			label1.TabIndex = 0;
			label1.Text = "Password:";
			textBoxPassword.Location = new System.Drawing.Point(74, 18);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(253, 20);
			textBoxPassword.TabIndex = 1;
			textBoxPassword.UseSystemPasswordChar = true;
			buttonOK.Location = new System.Drawing.Point(131, 76);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(95, 25);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(232, 76);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(95, 25);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(334, 109);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ApprovalPasswordForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Password";
			base.Load += new System.EventHandler(ApprovalPasswordForm_Load);
			base.Activated += new System.EventHandler(ApprovalPasswordForm_Activated);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
