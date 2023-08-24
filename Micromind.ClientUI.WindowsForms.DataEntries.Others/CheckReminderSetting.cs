using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CheckReminderSetting : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		public bool isPDC = true;

		private Label label6;

		private NumericUpDown numericUpDownCheckToDeposit;

		private Label label7;

		private Line line1;

		private XPButton buttonCancel;

		private XPButton buttonOK;

		private Container components;

		public CheckReminderSetting()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.CheckReminderSetting));
			label6 = new System.Windows.Forms.Label();
			numericUpDownCheckToDeposit = new System.Windows.Forms.NumericUpDown();
			label7 = new System.Windows.Forms.Label();
			line1 = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonOK = new Micromind.UISupport.XPButton();
			((System.ComponentModel.ISupportInitialize)numericUpDownCheckToDeposit).BeginInit();
			SuspendLayout();
			label6.AutoSize = true;
			label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label6.Location = new System.Drawing.Point(128, 30);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(110, 13);
			label6.TabIndex = 254;
			label6.Text = "days before due date.";
			numericUpDownCheckToDeposit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			numericUpDownCheckToDeposit.Location = new System.Drawing.Point(72, 30);
			numericUpDownCheckToDeposit.Name = "numericUpDownCheckToDeposit";
			numericUpDownCheckToDeposit.Size = new System.Drawing.Size(48, 20);
			numericUpDownCheckToDeposit.TabIndex = 252;
			label7.AutoSize = true;
			label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label7.Location = new System.Drawing.Point(8, 30);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(61, 13);
			label7.TabIndex = 253;
			label7.Text = "Remind Me";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Silver;
			line1.Location = new System.Drawing.Point(-24, 74);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(360, 1);
			line1.TabIndex = 257;
			line1.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.Silver;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(256, 82);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(65, 20);
			buttonCancel.TabIndex = 256;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.Silver;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(184, 82);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(65, 20);
			buttonOK.TabIndex = 255;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(338, 120);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			base.Controls.Add(label6);
			base.Controls.Add(numericUpDownCheckToDeposit);
			base.Controls.Add(label7);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CheckReminderSetting";
			Text = "Check Reminder Setting";
			base.Load += new System.EventHandler(CheckReminderSetting_Load);
			((System.ComponentModel.ISupportInitialize)numericUpDownCheckToDeposit).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (isPDC)
				{
					DBSettings.SaveSetting(DBSettings.ChecksToDepositReminder, numericUpDownCheckToDeposit.Value.ToString());
				}
				else
				{
					DBSettings.SaveSetting(DBSettings.PaidChecksDueReminder, numericUpDownCheckToDeposit.Value.ToString());
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			try
			{
				if (isPDC)
				{
					numericUpDownCheckToDeposit.Value = decimal.Parse(DBSettings.GetSetting(DBSettings.ChecksToDepositReminder).ToString());
				}
				else
				{
					numericUpDownCheckToDeposit.Value = decimal.Parse(DBSettings.GetSetting(DBSettings.PaidChecksDueReminder).ToString());
				}
			}
			catch
			{
			}
		}

		private void CheckReminderSetting_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				LoadData();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
