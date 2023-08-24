using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Libraries;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class AxolonUpdateDialog : Form
	{
		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label labelServerVersion;

		private Label labelClientVersion;

		private UltraGroupBox ultraGroupBox4;

		private RadioButton radioButtonInternet;

		private RadioButton radioButtonLocal;

		private Button buttonUpdate;

		private Button buttonClose;

		private TextBox textBoxClientUpdatePath;

		public AxolonUpdateDialog()
		{
			InitializeComponent();
			ReadVersions();
		}

		private void ReadVersions()
		{
			ApplicationUpdateConfig currentAxolonServerVersion = Factory.CompanyInformationSystem.GetCurrentAxolonServerVersion();
			ApplicationUpdateConfig currentClientVersion = UIGlobal.GetCurrentClientVersion();
			labelClientVersion.Text = currentClientVersion.GetVersionString();
			labelServerVersion.Text = currentAxolonServerVersion.GetVersionString();
			textBoxClientUpdatePath.Text = currentAxolonServerVersion.ClientUpdatePath;
		}

		private void AxolonUpdateDialog_Load(object sender, EventArgs e)
		{
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidateUpdatePath())
				{
					string arguments = labelClientVersion.Text + " " + labelServerVersion.Text + " \"" + textBoxClientUpdatePath.Text + "\"";
					Process.Start("Axolon Updater.exe", arguments);
					Application.Exit();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool ValidateUpdatePath()
		{
			if (radioButtonLocal.Checked)
			{
				if (File.Exists(textBoxClientUpdatePath.Text + "\\UpdateInfo.axo"))
				{
					return true;
				}
				ErrorHelper.ErrorMessage("The path specified does not contain update files.\nPlease provide a valid path or select to update from the internet.");
				return false;
			}
			return true;
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
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			labelServerVersion = new System.Windows.Forms.Label();
			labelClientVersion = new System.Windows.Forms.Label();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxClientUpdatePath = new System.Windows.Forms.TextBox();
			radioButtonInternet = new System.Windows.Forms.RadioButton();
			radioButtonLocal = new System.Windows.Forms.RadioButton();
			buttonUpdate = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 18);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(117, 13);
			label1.TabIndex = 0;
			label1.Text = "New Updates Found";
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 42);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(449, 38);
			label2.TabIndex = 1;
			label2.Text = "The Axolon Server that you are connecting is using a newer version than your the client. You must update your client to continue.";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 90);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(117, 13);
			label3.TabIndex = 2;
			label3.Text = "Axolon Server Version:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(12, 114);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(76, 13);
			label4.TabIndex = 3;
			label4.Text = "Client Version:";
			labelServerVersion.AutoSize = true;
			labelServerVersion.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelServerVersion.Location = new System.Drawing.Point(135, 90);
			labelServerVersion.Name = "labelServerVersion";
			labelServerVersion.Size = new System.Drawing.Size(33, 13);
			labelServerVersion.TabIndex = 2;
			labelServerVersion.Text = "3.0.0";
			labelClientVersion.AutoSize = true;
			labelClientVersion.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelClientVersion.Location = new System.Drawing.Point(135, 114);
			labelClientVersion.Name = "labelClientVersion";
			labelClientVersion.Size = new System.Drawing.Size(33, 13);
			labelClientVersion.TabIndex = 2;
			labelClientVersion.Text = "3.0.0";
			ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox4.Controls.Add(textBoxClientUpdatePath);
			ultraGroupBox4.Controls.Add(radioButtonInternet);
			ultraGroupBox4.Controls.Add(radioButtonLocal);
			ultraGroupBox4.Location = new System.Drawing.Point(0, 143);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(541, 85);
			ultraGroupBox4.TabIndex = 15;
			ultraGroupBox4.Text = "Update";
			textBoxClientUpdatePath.Location = new System.Drawing.Point(167, 25);
			textBoxClientUpdatePath.Name = "textBoxClientUpdatePath";
			textBoxClientUpdatePath.Size = new System.Drawing.Size(362, 20);
			textBoxClientUpdatePath.TabIndex = 2;
			radioButtonInternet.AutoSize = true;
			radioButtonInternet.Location = new System.Drawing.Point(15, 52);
			radioButtonInternet.Name = "radioButtonInternet";
			radioButtonInternet.Size = new System.Drawing.Size(122, 17);
			radioButtonInternet.TabIndex = 1;
			radioButtonInternet.Text = "Update from Internet";
			radioButtonInternet.UseVisualStyleBackColor = true;
			radioButtonLocal.AutoSize = true;
			radioButtonLocal.Checked = true;
			radioButtonLocal.Location = new System.Drawing.Point(15, 24);
			radioButtonLocal.Name = "radioButtonLocal";
			radioButtonLocal.Size = new System.Drawing.Size(152, 17);
			radioButtonLocal.TabIndex = 0;
			radioButtonLocal.TabStop = true;
			radioButtonLocal.Text = "Update from local network:";
			radioButtonLocal.UseVisualStyleBackColor = true;
			buttonUpdate.Location = new System.Drawing.Point(295, 290);
			buttonUpdate.Name = "buttonUpdate";
			buttonUpdate.Size = new System.Drawing.Size(114, 32);
			buttonUpdate.TabIndex = 16;
			buttonUpdate.Text = "Update";
			buttonUpdate.UseVisualStyleBackColor = true;
			buttonUpdate.Click += new System.EventHandler(buttonUpdate_Click);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(415, 290);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(114, 32);
			buttonClose.TabIndex = 17;
			buttonClose.Text = "Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			base.AcceptButton = buttonUpdate;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(543, 334);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonUpdate);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(label4);
			base.Controls.Add(labelClientVersion);
			base.Controls.Add(labelServerVersion);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AxolonUpdateDialog";
			Text = "Update";
			base.Load += new System.EventHandler(AxolonUpdateDialog_Load);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
