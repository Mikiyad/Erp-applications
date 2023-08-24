using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class LockPeriodDialog : Form
	{
		private DataSet lastClosingData;

		private IContainer components;

		private Button buttonCancel;

		private TextBox textBoxRemarks;

		private Label label1;

		private Button buttonOK;

		private Label label2;

		private Line linePanelDown;

		private flatDatePicker dateTimePickerDate;

		private TextBox textBoxLastDate;

		private Label label3;

		private Label label4;

		private Button buttonUnlock;

		private TextBox textBoxLastRemark;

		private flatDatePicker dateTimePickerInvDate;

		private TextBox textBoxLastInvDate;

		private Label label5;

		private CheckBox checkBoxFinDate;

		private CheckBox checkBoxInvDate;

		private Label label6;

		private MMSDateTimePicker dateTimePickerchkDate;

		private MMSDateTimePicker dateTimePickerchkInvDate;

		public string EnteredName
		{
			get
			{
				return textBoxRemarks.Text;
			}
			set
			{
				textBoxRemarks.Text = value;
			}
		}

		public LockPeriodDialog()
		{
			InitializeComponent();
			base.Load += ClosePeriodDialog_Load;
			base.StartPosition = FormStartPosition.CenterParent;
			dateTimePickerchkDate.Value = DateTime.Today;
			dateTimePickerchkInvDate.Value = DateTime.Today;
		}

		private void ClosePeriodDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadLastClosing();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadLastClosing()
		{
			try
			{
				lastClosingData = Factory.CompanyInformationSystem.GetLastClosingPeriod();
				if (lastClosingData != null && lastClosingData.Tables.Count > 0 && lastClosingData.Tables[0].Rows.Count > 0)
				{
					if (!string.IsNullOrEmpty(lastClosingData.Tables["Period"].Rows[0]["CloseDate"].ToString()))
					{
						textBoxLastDate.Text = DateTime.Parse(lastClosingData.Tables["Period"].Rows[0]["CloseDate"].ToString()).ToShortDateString();
					}
					if (!string.IsNullOrEmpty(lastClosingData.Tables["Period"].Rows[0]["InventoryCloseDate"].ToString()))
					{
						textBoxLastInvDate.Text = DateTime.Parse(lastClosingData.Tables["Period"].Rows[0]["InventoryCloseDate"].ToString()).ToShortDateString();
					}
					textBoxLastRemark.Text = lastClosingData.Tables["Period"].Rows[0]["Remarks"].ToString();
				}
				else
				{
					buttonUnlock.Enabled = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				DateTime dateTime = dateTimePickerchkDate.Value;
				DateTime dateTime2 = dateTimePickerchkInvDate.Value;
				DateTime initialFiscalYearDate = Factory.CompanyInformationSystem.GetInitialFiscalYearDate();
				if (!dateTimePickerchkDate.Checked && textBoxLastDate.Text == "")
				{
					dateTime = initialFiscalYearDate;
				}
				if (!dateTimePickerchkDate.Checked && textBoxLastDate.Text != "")
				{
					dateTime = DateTime.Parse(textBoxLastDate.Text);
				}
				if (!dateTimePickerchkInvDate.Checked && textBoxLastInvDate.Text == "")
				{
					dateTime2 = initialFiscalYearDate;
				}
				if (!dateTimePickerchkInvDate.Checked && textBoxLastInvDate.Text != "")
				{
					dateTime2 = DateTime.Parse(textBoxLastInvDate.Text);
				}
				if (!dateTimePickerchkDate.Checked && !dateTimePickerchkInvDate.Checked)
				{
					ErrorHelper.WarningMessage("Set Lock Date.");
				}
				else if (dateTime > dateTime2)
				{
					ErrorHelper.WarningMessage("Financial Lock should not be higher.");
				}
				else if (Factory.CompanyInformationSystem.ClosePeriod(dateTime, dateTime2, textBoxRemarks.Text))
				{
					ErrorHelper.InformationMessage("Successfully closed the requested period.");
					Close();
				}
			}
			catch (CompanyException e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}

		private void ClosePeriodDialog_Activated(object sender, EventArgs e)
		{
			textBoxRemarks.Focus();
		}

		private void buttonUnlock_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Unlocking period will allow users to post transactions in the period.\nAre you sure you want to unlock the selected period lock?") != DialogResult.No)
				{
					int num = -1;
					if (lastClosingData != null)
					{
						num = int.Parse(lastClosingData.Tables[0].Rows[0]["PeriodID"].ToString());
						if (Factory.CompanyInformationSystem.UnlockPeriod(num))
						{
							LoadLastClosing();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxFinDate_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxInvDate_CheckedChanged(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.LockPeriodDialog));
			buttonCancel = new System.Windows.Forms.Button();
			textBoxRemarks = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			textBoxLastDate = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			buttonUnlock = new System.Windows.Forms.Button();
			textBoxLastRemark = new System.Windows.Forms.TextBox();
			textBoxLastInvDate = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			checkBoxFinDate = new System.Windows.Forms.CheckBox();
			checkBoxInvDate = new System.Windows.Forms.CheckBox();
			label6 = new System.Windows.Forms.Label();
			dateTimePickerchkInvDate = new Micromind.UISupport.MMSDateTimePicker();
			dateTimePickerchkDate = new Micromind.UISupport.MMSDateTimePicker();
			dateTimePickerInvDate = new Micromind.UISupport.flatDatePicker();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			linePanelDown = new Micromind.UISupport.Line();
			SuspendLayout();
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(377, 228);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 5;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			textBoxRemarks.Location = new System.Drawing.Point(132, 180);
			textBoxRemarks.MaxLength = 15;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(354, 20);
			textBoxRemarks.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(128, 13);
			label1.TabIndex = 2;
			label1.Text = "Last Financial Lock Date:";
			buttonOK.Location = new System.Drawing.Point(284, 228);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			label2.Location = new System.Drawing.Point(12, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(453, 36);
			label2.TabIndex = 3;
			label2.Text = "Enter the lock date to lock the period from accepting enteries. No journal transaction will be recorded for this closing.";
			textBoxLastDate.Location = new System.Drawing.Point(133, 48);
			textBoxLastDate.Name = "textBoxLastDate";
			textBoxLastDate.ReadOnly = true;
			textBoxLastDate.Size = new System.Drawing.Size(152, 20);
			textBoxLastDate.TabIndex = 0;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 136);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 13);
			label3.TabIndex = 18;
			label3.Text = "Financial Lock Date:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(3, 184);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(52, 13);
			label4.TabIndex = 19;
			label4.Text = "Remarks:";
			buttonUnlock.Location = new System.Drawing.Point(291, 47);
			buttonUnlock.Name = "buttonUnlock";
			buttonUnlock.Size = new System.Drawing.Size(102, 22);
			buttonUnlock.TabIndex = 1;
			buttonUnlock.Text = "&Unlock Period";
			buttonUnlock.UseVisualStyleBackColor = true;
			buttonUnlock.Click += new System.EventHandler(buttonUnlock_Click);
			textBoxLastRemark.Location = new System.Drawing.Point(133, 96);
			textBoxLastRemark.MaxLength = 15;
			textBoxLastRemark.Name = "textBoxLastRemark";
			textBoxLastRemark.ReadOnly = true;
			textBoxLastRemark.Size = new System.Drawing.Size(354, 20);
			textBoxLastRemark.TabIndex = 20;
			textBoxLastInvDate.Location = new System.Drawing.Point(133, 72);
			textBoxLastInvDate.Name = "textBoxLastInvDate";
			textBoxLastInvDate.ReadOnly = true;
			textBoxLastInvDate.Size = new System.Drawing.Size(152, 20);
			textBoxLastInvDate.TabIndex = 22;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 161);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(107, 13);
			label5.TabIndex = 23;
			label5.Text = "Inventory Lock Date:";
			checkBoxFinDate.AutoSize = true;
			checkBoxFinDate.Checked = true;
			checkBoxFinDate.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxFinDate.Location = new System.Drawing.Point(50, 234);
			checkBoxFinDate.Name = "checkBoxFinDate";
			checkBoxFinDate.Size = new System.Drawing.Size(15, 14);
			checkBoxFinDate.TabIndex = 24;
			checkBoxFinDate.UseVisualStyleBackColor = true;
			checkBoxFinDate.Visible = false;
			checkBoxFinDate.CheckedChanged += new System.EventHandler(checkBoxFinDate_CheckedChanged);
			checkBoxInvDate.AutoSize = true;
			checkBoxInvDate.Checked = true;
			checkBoxInvDate.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInvDate.Location = new System.Drawing.Point(50, 234);
			checkBoxInvDate.Name = "checkBoxInvDate";
			checkBoxInvDate.Size = new System.Drawing.Size(15, 14);
			checkBoxInvDate.TabIndex = 25;
			checkBoxInvDate.UseVisualStyleBackColor = true;
			checkBoxInvDate.Visible = false;
			checkBoxInvDate.CheckedChanged += new System.EventHandler(checkBoxInvDate_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(3, 76);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(130, 13);
			label6.TabIndex = 26;
			label6.Text = "Last Inventory Lock Date:";
			dateTimePickerchkInvDate.Checked = false;
			dateTimePickerchkInvDate.CustomFormat = " ";
			dateTimePickerchkInvDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerchkInvDate.Location = new System.Drawing.Point(132, 155);
			dateTimePickerchkInvDate.Name = "dateTimePickerchkInvDate";
			dateTimePickerchkInvDate.ShowCheckBox = true;
			dateTimePickerchkInvDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerchkInvDate.TabIndex = 28;
			dateTimePickerchkInvDate.Value = new System.DateTime(0L);
			dateTimePickerchkDate.Checked = false;
			dateTimePickerchkDate.CustomFormat = " ";
			dateTimePickerchkDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerchkDate.Location = new System.Drawing.Point(132, 130);
			dateTimePickerchkDate.Name = "dateTimePickerchkDate";
			dateTimePickerchkDate.ShowCheckBox = true;
			dateTimePickerchkDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerchkDate.TabIndex = 27;
			dateTimePickerchkDate.Value = new System.DateTime(0L);
			dateTimePickerInvDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerInvDate.Location = new System.Drawing.Point(82, 228);
			dateTimePickerInvDate.Name = "dateTimePickerInvDate";
			dateTimePickerInvDate.Size = new System.Drawing.Size(152, 20);
			dateTimePickerInvDate.TabIndex = 21;
			dateTimePickerInvDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 9, 732);
			dateTimePickerInvDate.Visible = false;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(82, 228);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(152, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 9, 732);
			dateTimePickerDate.Visible = false;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-49, 218);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(527, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(490, 264);
			base.Controls.Add(dateTimePickerchkInvDate);
			base.Controls.Add(dateTimePickerchkDate);
			base.Controls.Add(label6);
			base.Controls.Add(checkBoxInvDate);
			base.Controls.Add(checkBoxFinDate);
			base.Controls.Add(label5);
			base.Controls.Add(textBoxLastInvDate);
			base.Controls.Add(dateTimePickerInvDate);
			base.Controls.Add(textBoxLastRemark);
			base.Controls.Add(buttonUnlock);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxLastDate);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LockPeriodDialog";
			Text = "Lock Period";
			base.Activated += new System.EventHandler(ClosePeriodDialog_Activated);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
