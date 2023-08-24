using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Utilities
{
	public class ReminderSettingsDialog : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private XPButton buttonCancel;

		private XPButton xpButtonOK;

		private ToolTip toolTip1;

		private Line line1;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private CheckBox checkBoxOverdueInvoices;

		private CheckBox checkBoxOverdueBills;

		private CheckBox checkBoxReceivedChecks;

		private CheckBox checkBoxPaidChecks;

		private CheckBox checkBoxInventoryReorder;

		private CheckBox checkBoxEmployeestoPay;

		private CheckBox checkBoxEmployeeDocuments;

		private CheckBox checkBoxDrivingLicense;

		private CheckBox checkBoxVisa;

		private CheckBox checkBoxPassport;

		private MMTextBox textBoxOverdueInvoices;

		private MMTextBox textBoxOverdueBills;

		private MMTextBox textBoxReceivedChecks;

		private MMTextBox textBoxPaidChecks;

		private MMTextBox textBoxEmployeestoPay;

		private MMTextBox textBoxEmployeeDocs;

		private IContainer components;

		public ReminderSettingsDialog()
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
			components = new System.ComponentModel.Container();
			buttonCancel = new Micromind.UISupport.XPButton();
			xpButtonOK = new Micromind.UISupport.XPButton();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			checkBoxOverdueInvoices = new System.Windows.Forms.CheckBox();
			checkBoxOverdueBills = new System.Windows.Forms.CheckBox();
			checkBoxReceivedChecks = new System.Windows.Forms.CheckBox();
			checkBoxPaidChecks = new System.Windows.Forms.CheckBox();
			checkBoxInventoryReorder = new System.Windows.Forms.CheckBox();
			checkBoxEmployeestoPay = new System.Windows.Forms.CheckBox();
			checkBoxEmployeeDocuments = new System.Windows.Forms.CheckBox();
			checkBoxDrivingLicense = new System.Windows.Forms.CheckBox();
			checkBoxVisa = new System.Windows.Forms.CheckBox();
			checkBoxPassport = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			textBoxOverdueInvoices = new Micromind.UISupport.MMTextBox();
			textBoxOverdueBills = new Micromind.UISupport.MMTextBox();
			textBoxReceivedChecks = new Micromind.UISupport.MMTextBox();
			textBoxPaidChecks = new Micromind.UISupport.MMTextBox();
			textBoxEmployeestoPay = new Micromind.UISupport.MMTextBox();
			textBoxEmployeeDocs = new Micromind.UISupport.MMTextBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			SuspendLayout();
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(354, 240);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(56, 20);
			buttonCancel.TabIndex = 18;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			xpButtonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButtonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonOK.Location = new System.Drawing.Point(290, 240);
			xpButtonOK.Name = "xpButtonOK";
			xpButtonOK.Size = new System.Drawing.Size(56, 20);
			xpButtonOK.TabIndex = 17;
			xpButtonOK.Text = "&OK";
			xpButtonOK.Click += new System.EventHandler(xpButtonOK_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 234);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(402, 1);
			line1.TabIndex = 16;
			line1.TabStop = false;
			checkBoxOverdueInvoices.BackColor = System.Drawing.Color.Transparent;
			checkBoxOverdueInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxOverdueInvoices.Location = new System.Drawing.Point(16, 34);
			checkBoxOverdueInvoices.Name = "checkBoxOverdueInvoices";
			checkBoxOverdueInvoices.Size = new System.Drawing.Size(192, 21);
			checkBoxOverdueInvoices.TabIndex = 0;
			checkBoxOverdueInvoices.Text = "Overdue Invoices";
			checkBoxOverdueInvoices.UseVisualStyleBackColor = false;
			checkBoxOverdueBills.BackColor = System.Drawing.Color.Transparent;
			checkBoxOverdueBills.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxOverdueBills.Location = new System.Drawing.Point(16, 55);
			checkBoxOverdueBills.Name = "checkBoxOverdueBills";
			checkBoxOverdueBills.Size = new System.Drawing.Size(192, 21);
			checkBoxOverdueBills.TabIndex = 2;
			checkBoxOverdueBills.Text = "Overdue Bills";
			checkBoxOverdueBills.UseVisualStyleBackColor = false;
			checkBoxReceivedChecks.BackColor = System.Drawing.Color.Transparent;
			checkBoxReceivedChecks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxReceivedChecks.Location = new System.Drawing.Point(16, 76);
			checkBoxReceivedChecks.Name = "checkBoxReceivedChecks";
			checkBoxReceivedChecks.Size = new System.Drawing.Size(192, 21);
			checkBoxReceivedChecks.TabIndex = 4;
			checkBoxReceivedChecks.Text = "Received Cheques";
			checkBoxReceivedChecks.UseVisualStyleBackColor = false;
			checkBoxPaidChecks.BackColor = System.Drawing.Color.Transparent;
			checkBoxPaidChecks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxPaidChecks.Location = new System.Drawing.Point(16, 97);
			checkBoxPaidChecks.Name = "checkBoxPaidChecks";
			checkBoxPaidChecks.Size = new System.Drawing.Size(192, 20);
			checkBoxPaidChecks.TabIndex = 6;
			checkBoxPaidChecks.Text = "Paid Cheques";
			checkBoxPaidChecks.UseVisualStyleBackColor = false;
			checkBoxInventoryReorder.BackColor = System.Drawing.Color.Transparent;
			checkBoxInventoryReorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxInventoryReorder.Location = new System.Drawing.Point(16, 117);
			checkBoxInventoryReorder.Name = "checkBoxInventoryReorder";
			checkBoxInventoryReorder.Size = new System.Drawing.Size(192, 21);
			checkBoxInventoryReorder.TabIndex = 8;
			checkBoxInventoryReorder.Text = "Inventory to Reorder";
			checkBoxInventoryReorder.UseVisualStyleBackColor = false;
			checkBoxEmployeestoPay.BackColor = System.Drawing.Color.Transparent;
			checkBoxEmployeestoPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxEmployeestoPay.Location = new System.Drawing.Point(16, 138);
			checkBoxEmployeestoPay.Name = "checkBoxEmployeestoPay";
			checkBoxEmployeestoPay.Size = new System.Drawing.Size(192, 21);
			checkBoxEmployeestoPay.TabIndex = 10;
			checkBoxEmployeestoPay.Text = "Employees to Pay";
			checkBoxEmployeestoPay.UseVisualStyleBackColor = false;
			checkBoxEmployeeDocuments.BackColor = System.Drawing.Color.Transparent;
			checkBoxEmployeeDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxEmployeeDocuments.Location = new System.Drawing.Point(16, 159);
			checkBoxEmployeeDocuments.Name = "checkBoxEmployeeDocuments";
			checkBoxEmployeeDocuments.Size = new System.Drawing.Size(192, 20);
			checkBoxEmployeeDocuments.TabIndex = 12;
			checkBoxEmployeeDocuments.Text = "Employee Documents Expiration";
			checkBoxEmployeeDocuments.UseVisualStyleBackColor = false;
			checkBoxDrivingLicense.BackColor = System.Drawing.Color.Transparent;
			checkBoxDrivingLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxDrivingLicense.Location = new System.Drawing.Point(184, 179);
			checkBoxDrivingLicense.Name = "checkBoxDrivingLicense";
			checkBoxDrivingLicense.Size = new System.Drawing.Size(112, 21);
			checkBoxDrivingLicense.TabIndex = 16;
			checkBoxDrivingLicense.Text = "Driving License";
			checkBoxDrivingLicense.UseVisualStyleBackColor = false;
			checkBoxVisa.BackColor = System.Drawing.Color.Transparent;
			checkBoxVisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxVisa.Location = new System.Drawing.Point(120, 179);
			checkBoxVisa.Name = "checkBoxVisa";
			checkBoxVisa.Size = new System.Drawing.Size(64, 21);
			checkBoxVisa.TabIndex = 15;
			checkBoxVisa.Text = "Visa";
			checkBoxVisa.UseVisualStyleBackColor = false;
			checkBoxPassport.BackColor = System.Drawing.Color.Transparent;
			checkBoxPassport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxPassport.Location = new System.Drawing.Point(48, 179);
			checkBoxPassport.Name = "checkBoxPassport";
			checkBoxPassport.Size = new System.Drawing.Size(72, 21);
			checkBoxPassport.TabIndex = 14;
			checkBoxPassport.Text = "Passport";
			checkBoxPassport.UseVisualStyleBackColor = false;
			label1.Location = new System.Drawing.Point(224, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(72, 14);
			label1.TabIndex = 27;
			label1.Text = "Remind Me";
			textBoxOverdueInvoices.BackColor = System.Drawing.Color.White;
			textBoxOverdueInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxOverdueInvoices.IsComboTextBox = false;
			textBoxOverdueInvoices.Location = new System.Drawing.Point(240, 34);
			textBoxOverdueInvoices.MaxLength = 2;
			textBoxOverdueInvoices.Name = "textBoxOverdueInvoices";
			textBoxOverdueInvoices.Size = new System.Drawing.Size(32, 13);
			textBoxOverdueInvoices.TabIndex = 1;
			textBoxOverdueInvoices.Text = "3";
			textBoxOverdueInvoices.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			textBoxOverdueBills.BackColor = System.Drawing.Color.White;
			textBoxOverdueBills.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxOverdueBills.IsComboTextBox = false;
			textBoxOverdueBills.Location = new System.Drawing.Point(240, 55);
			textBoxOverdueBills.MaxLength = 2;
			textBoxOverdueBills.Name = "textBoxOverdueBills";
			textBoxOverdueBills.Size = new System.Drawing.Size(32, 13);
			textBoxOverdueBills.TabIndex = 3;
			textBoxOverdueBills.Text = "3";
			textBoxOverdueBills.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			textBoxReceivedChecks.BackColor = System.Drawing.Color.White;
			textBoxReceivedChecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxReceivedChecks.IsComboTextBox = false;
			textBoxReceivedChecks.Location = new System.Drawing.Point(240, 76);
			textBoxReceivedChecks.MaxLength = 2;
			textBoxReceivedChecks.Name = "textBoxReceivedChecks";
			textBoxReceivedChecks.Size = new System.Drawing.Size(32, 13);
			textBoxReceivedChecks.TabIndex = 5;
			textBoxReceivedChecks.Text = "3";
			textBoxReceivedChecks.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			textBoxPaidChecks.BackColor = System.Drawing.Color.White;
			textBoxPaidChecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxPaidChecks.IsComboTextBox = false;
			textBoxPaidChecks.Location = new System.Drawing.Point(240, 97);
			textBoxPaidChecks.MaxLength = 2;
			textBoxPaidChecks.Name = "textBoxPaidChecks";
			textBoxPaidChecks.Size = new System.Drawing.Size(32, 13);
			textBoxPaidChecks.TabIndex = 7;
			textBoxPaidChecks.Text = "3";
			textBoxPaidChecks.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			textBoxEmployeestoPay.BackColor = System.Drawing.Color.White;
			textBoxEmployeestoPay.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxEmployeestoPay.IsComboTextBox = false;
			textBoxEmployeestoPay.Location = new System.Drawing.Point(240, 138);
			textBoxEmployeestoPay.MaxLength = 2;
			textBoxEmployeestoPay.Name = "textBoxEmployeestoPay";
			textBoxEmployeestoPay.Size = new System.Drawing.Size(32, 13);
			textBoxEmployeestoPay.TabIndex = 11;
			textBoxEmployeestoPay.Text = "3";
			textBoxEmployeestoPay.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			textBoxEmployeeDocs.BackColor = System.Drawing.Color.White;
			textBoxEmployeeDocs.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxEmployeeDocs.IsComboTextBox = false;
			textBoxEmployeeDocs.Location = new System.Drawing.Point(240, 159);
			textBoxEmployeeDocs.MaxLength = 2;
			textBoxEmployeeDocs.Name = "textBoxEmployeeDocs";
			textBoxEmployeeDocs.Size = new System.Drawing.Size(32, 13);
			textBoxEmployeeDocs.TabIndex = 13;
			textBoxEmployeeDocs.Text = "3";
			textBoxEmployeeDocs.Validating += new System.ComponentModel.CancelEventHandler(textBoxOverdueInvoices_Validating);
			label2.Location = new System.Drawing.Point(280, 34);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(128, 14);
			label2.TabIndex = 35;
			label2.Text = "days after due date.";
			label3.Location = new System.Drawing.Point(280, 55);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(128, 14);
			label3.TabIndex = 36;
			label3.Text = "days before due date.";
			label4.Location = new System.Drawing.Point(280, 76);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(128, 14);
			label4.TabIndex = 37;
			label4.Text = "days before due date.";
			label5.Location = new System.Drawing.Point(280, 97);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(128, 13);
			label5.TabIndex = 38;
			label5.Text = "days before due date.";
			label6.Location = new System.Drawing.Point(280, 138);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(128, 13);
			label6.TabIndex = 39;
			label6.Text = "days before due date.";
			label7.Location = new System.Drawing.Point(280, 159);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(128, 14);
			label7.TabIndex = 40;
			label7.Text = "days before expire date.";
			base.AcceptButton = xpButtonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(418, 267);
			base.ControlBox = false;
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxEmployeeDocs);
			base.Controls.Add(textBoxEmployeestoPay);
			base.Controls.Add(textBoxPaidChecks);
			base.Controls.Add(textBoxReceivedChecks);
			base.Controls.Add(textBoxOverdueBills);
			base.Controls.Add(textBoxOverdueInvoices);
			base.Controls.Add(label1);
			base.Controls.Add(checkBoxPassport);
			base.Controls.Add(checkBoxVisa);
			base.Controls.Add(checkBoxDrivingLicense);
			base.Controls.Add(checkBoxEmployeeDocuments);
			base.Controls.Add(checkBoxEmployeestoPay);
			base.Controls.Add(checkBoxInventoryReorder);
			base.Controls.Add(checkBoxPaidChecks);
			base.Controls.Add(checkBoxReceivedChecks);
			base.Controls.Add(checkBoxOverdueBills);
			base.Controls.Add(checkBoxOverdueInvoices);
			base.Controls.Add(line1);
			base.Controls.Add(xpButtonOK);
			base.Controls.Add(buttonCancel);
			base.Name = "ReminderSettingsDialog";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			Text = "Reminder Settings";
			base.Load += new System.EventHandler(ReminderSettingsDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}

		private void LoadSettings()
		{
			try
			{
				Global.CompanySettings.LoadFormProperties(this);
			}
			catch
			{
			}
			try
			{
				checkBoxDrivingLicense.Checked = UIGlobal.RemindEmployeeDrivingLicense;
				checkBoxEmployeeDocuments.Checked = UIGlobal.RemindEmployeeDocuments;
				checkBoxEmployeestoPay.Checked = UIGlobal.RemindEmployeesToPay;
				checkBoxInventoryReorder.Checked = UIGlobal.RemindReorderItems;
				checkBoxOverdueBills.Checked = UIGlobal.RemindOverdueBills;
				checkBoxOverdueInvoices.Checked = UIGlobal.RemindOverdueInvoices;
				checkBoxPaidChecks.Checked = UIGlobal.RemindPaidChecks;
				checkBoxPassport.Checked = UIGlobal.RemindEmployeePassport;
				checkBoxReceivedChecks.Checked = UIGlobal.RemindRecievedChecks;
				checkBoxVisa.Checked = UIGlobal.RemindEmployeeVisa;
			}
			catch
			{
			}
			try
			{
				textBoxEmployeeDocs.Text = UIGlobal.EmployeeDocumentsDays.ToString();
				textBoxEmployeestoPay.Text = UIGlobal.EmployeesToPayDays.ToString();
				textBoxOverdueBills.Text = UIGlobal.OverdueBillsDays.ToString();
				textBoxOverdueInvoices.Text = UIGlobal.OverdueInvoicesDays.ToString();
				textBoxPaidChecks.Text = UIGlobal.PaidChecksDays.ToString();
				textBoxReceivedChecks.Text = UIGlobal.ReceivedChecksDays.ToString();
			}
			catch
			{
			}
		}

		private void SaveSettings()
		{
			Global.CompanySettings.LoadFormProperties(this);
			try
			{
				UIGlobal.RemindEmployeeDrivingLicense = checkBoxDrivingLicense.Checked;
				UIGlobal.RemindEmployeeDocuments = checkBoxEmployeeDocuments.Checked;
				UIGlobal.RemindEmployeesToPay = checkBoxEmployeestoPay.Checked;
				UIGlobal.RemindReorderItems = checkBoxInventoryReorder.Checked;
				UIGlobal.RemindOverdueBills = checkBoxOverdueBills.Checked;
				UIGlobal.RemindOverdueInvoices = checkBoxOverdueInvoices.Checked;
				UIGlobal.RemindPaidChecks = checkBoxPaidChecks.Checked;
				UIGlobal.RemindEmployeePassport = checkBoxPassport.Checked;
				UIGlobal.RemindRecievedChecks = checkBoxReceivedChecks.Checked;
				UIGlobal.RemindEmployeeVisa = checkBoxVisa.Checked;
			}
			catch
			{
			}
			try
			{
				UIGlobal.EmployeeDocumentsDays = byte.Parse(textBoxEmployeeDocs.Text);
				UIGlobal.EmployeesToPayDays = byte.Parse(textBoxEmployeestoPay.Text);
				UIGlobal.OverdueBillsDays = byte.Parse(textBoxOverdueBills.Text);
				UIGlobal.OverdueInvoicesDays = byte.Parse(textBoxOverdueInvoices.Text);
				UIGlobal.PaidChecksDays = byte.Parse(textBoxPaidChecks.Text);
				UIGlobal.ReceivedChecksDays = byte.Parse(textBoxReceivedChecks.Text);
			}
			catch
			{
			}
		}

		private void ReminderSettingsDialog_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				LoadSettings();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxOverdueInvoices_Validating(object sender, CancelEventArgs e)
		{
			MMTextBox mMTextBox = sender as MMTextBox;
			if (mMTextBox != null)
			{
				mMTextBox.Text = mMTextBox.Text.Trim();
				if (mMTextBox.Text.Trim() == "")
				{
					mMTextBox.Text = "0";
				}
				else
				{
					try
					{
						if (byte.Parse(mMTextBox.Text) < 0)
						{
							mMTextBox.Text = "0";
						}
					}
					catch
					{
						ErrorHelper.WarningMessage("Please enter a numeric value.");
						e.Cancel = true;
					}
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void xpButtonOK_Click(object sender, EventArgs e)
		{
			SaveSettings();
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
