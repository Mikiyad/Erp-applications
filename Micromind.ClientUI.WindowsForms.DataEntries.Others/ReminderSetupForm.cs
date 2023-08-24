using Infragistics.Win.UltraWinEditors;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ReminderSetupForm : Form, IForm
	{
		private ReminderData currentData;

		private string TABLENAME_CONST = "";

		private string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private MMLabel mmLabel9;

		private CheckBox checkBoxOverdueInvoice;

		private CheckBox checkBoxOverdueBill;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private CheckBox checkBoxPDCOnhand;

		private MMLabel mmLabel3;

		private CheckBox checkBoxPDCIssued;

		private MMLabel mmLabel4;

		private MMTextBox textBoxUserID;

		private UltraNumericEditor textBoxOverdueInvoiceDays;

		private UltraNumericEditor textBoxOverdueBillDays;

		private UltraNumericEditor textBoxPDCOnhandDays;

		private UltraNumericEditor textBoxPDCIssuedDays;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public ReminderSetupForm()
		{
			InitializeComponent();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ReminderData();
				}
				DataRow dataRow = currentData.ReminderSettingTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = Global.CurrentUser;
				dataRow["ReminderID"] = checkBoxOverdueInvoice.Tag.ToString();
				dataRow["Days"] = textBoxOverdueInvoiceDays.Text;
				dataRow["IsSelected"] = checkBoxOverdueInvoice.Checked;
				dataRow.EndEdit();
				currentData.ReminderSettingTable.Rows.Add(dataRow);
				dataRow = currentData.ReminderSettingTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = Global.CurrentUser;
				dataRow["ReminderID"] = checkBoxOverdueBill.Tag.ToString();
				dataRow["Days"] = textBoxOverdueBillDays.Text;
				dataRow["IsSelected"] = checkBoxOverdueBill.Checked;
				dataRow.EndEdit();
				currentData.ReminderSettingTable.Rows.Add(dataRow);
				dataRow = currentData.ReminderSettingTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = Global.CurrentUser;
				dataRow["ReminderID"] = textBoxPDCOnhandDays.Tag.ToString();
				dataRow["Days"] = textBoxPDCOnhandDays.Text;
				dataRow["IsSelected"] = checkBoxPDCOnhand.Checked;
				dataRow.EndEdit();
				currentData.ReminderSettingTable.Rows.Add(dataRow);
				dataRow = currentData.ReminderSettingTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = Global.CurrentUser;
				dataRow["ReminderID"] = textBoxPDCIssuedDays.Tag.ToString();
				dataRow["Days"] = textBoxPDCIssuedDays.Text;
				dataRow["IsSelected"] = checkBoxPDCIssued.Checked;
				dataRow.EndEdit();
				currentData.ReminderSettingTable.Rows.Add(dataRow);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData()
		{
			try
			{
				if (!(Global.CurrentUser == "") && CanClose())
				{
					currentData = Factory.ReminderSystem.GetReminderSettingByUser(Global.CurrentUser);
					if (currentData != null)
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				checkBoxOverdueInvoice.Checked = IsReminderSelected(checkBoxOverdueInvoice.Tag.ToString());
				checkBoxOverdueBill.Checked = IsReminderSelected(checkBoxOverdueBill.Tag.ToString());
				checkBoxPDCOnhand.Checked = IsReminderSelected(checkBoxPDCOnhand.Tag.ToString());
				checkBoxPDCIssued.Checked = IsReminderSelected(checkBoxPDCIssued.Tag.ToString());
				textBoxOverdueInvoiceDays.Text = GetReminderDays(textBoxOverdueInvoiceDays.Tag.ToString());
				textBoxOverdueBillDays.Text = GetReminderDays(textBoxOverdueBillDays.Tag.ToString());
				textBoxPDCIssuedDays.Text = GetReminderDays(textBoxPDCIssuedDays.Tag.ToString());
				textBoxPDCOnhandDays.Text = GetReminderDays(textBoxPDCOnhandDays.Tag.ToString());
			}
		}

		private bool IsReminderSelected(string reminderID)
		{
			DataRow[] array = currentData.Tables["Reminder_Setting"].Select("ReminderID = " + reminderID);
			if (array.Length == 0)
			{
				return false;
			}
			if (array[0]["IsSelected"] != DBNull.Value)
			{
				return bool.Parse(array[0]["IsSelected"].ToString());
			}
			return false;
		}

		private string GetReminderDays(string reminderID)
		{
			DataRow[] array = currentData.Tables["Reminder_Setting"].Select("ReminderID = " + reminderID);
			if (array.Length == 0)
			{
				return "0";
			}
			return array[0]["Days"].ToString();
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = Factory.ReminderSystem.UpdateReminderSetting(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					ClearForm();
					Close();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.Edit)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					textBoxUserID.Text = Global.CurrentUser;
					IsNewRecord = true;
					ClearForm();
					LoadData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				Dispose();
			}
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ReminderSetupForm));
			mmLabel9 = new Micromind.UISupport.MMLabel();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxOverdueInvoice = new System.Windows.Forms.CheckBox();
			checkBoxOverdueBill = new System.Windows.Forms.CheckBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			checkBoxPDCOnhand = new System.Windows.Forms.CheckBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			checkBoxPDCIssued = new System.Windows.Forms.CheckBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxUserID = new Micromind.UISupport.MMTextBox();
			textBoxOverdueInvoiceDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			textBoxOverdueBillDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			textBoxPDCOnhandDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			textBoxPDCIssuedDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)textBoxOverdueInvoiceDays).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxOverdueBillDays).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxPDCOnhandDays).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxPDCIssuedDays).BeginInit();
			SuspendLayout();
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(234, 46);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(122, 13);
			mmLabel9.TabIndex = 27;
			mmLabel9.Text = "days after the due date";
			mmLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 180);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(465, 40);
			panelButtons.TabIndex = 9;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(465, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(355, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(255, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			checkBoxOverdueInvoice.AutoSize = true;
			checkBoxOverdueInvoice.Location = new System.Drawing.Point(12, 46);
			checkBoxOverdueInvoice.Name = "checkBoxOverdueInvoice";
			checkBoxOverdueInvoice.Size = new System.Drawing.Size(146, 17);
			checkBoxOverdueInvoice.TabIndex = 1;
			checkBoxOverdueInvoice.Tag = "1";
			checkBoxOverdueInvoice.Text = "Remind overdue invoices";
			checkBoxOverdueInvoice.UseVisualStyleBackColor = true;
			checkBoxOverdueBill.AutoSize = true;
			checkBoxOverdueBill.Location = new System.Drawing.Point(11, 74);
			checkBoxOverdueBill.Name = "checkBoxOverdueBill";
			checkBoxOverdueBill.Size = new System.Drawing.Size(124, 17);
			checkBoxOverdueBill.TabIndex = 3;
			checkBoxOverdueBill.Tag = "2";
			checkBoxOverdueBill.Text = "Remind overdue bills";
			checkBoxOverdueBill.UseVisualStyleBackColor = true;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(233, 74);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(130, 13);
			mmLabel1.TabIndex = 30;
			mmLabel1.Text = "days before the due date";
			mmLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(233, 101);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(101, 13);
			mmLabel2.TabIndex = 30;
			mmLabel2.Text = "days after the date";
			mmLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxPDCOnhand.AutoSize = true;
			checkBoxPDCOnhand.Location = new System.Drawing.Point(11, 101);
			checkBoxPDCOnhand.Name = "checkBoxPDCOnhand";
			checkBoxPDCOnhand.Size = new System.Drawing.Size(126, 17);
			checkBoxPDCOnhand.TabIndex = 5;
			checkBoxPDCOnhand.Tag = "3";
			checkBoxPDCOnhand.Text = "Remind PDC onhand";
			checkBoxPDCOnhand.UseVisualStyleBackColor = true;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(233, 129);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(109, 13);
			mmLabel3.TabIndex = 30;
			mmLabel3.Text = "days before the date";
			mmLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxPDCIssued.AutoSize = true;
			checkBoxPDCIssued.Location = new System.Drawing.Point(11, 129);
			checkBoxPDCIssued.Name = "checkBoxPDCIssued";
			checkBoxPDCIssued.Size = new System.Drawing.Size(120, 17);
			checkBoxPDCIssued.TabIndex = 7;
			checkBoxPDCIssued.Tag = "4";
			checkBoxPDCIssued.Text = "Remind PDC issued";
			checkBoxPDCIssued.UseVisualStyleBackColor = true;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(12, 9);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(47, 13);
			mmLabel4.TabIndex = 27;
			mmLabel4.Text = "User ID:";
			mmLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxUserID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUserID.IsComboTextBox = false;
			textBoxUserID.IsRequired = true;
			textBoxUserID.Location = new System.Drawing.Point(65, 6);
			textBoxUserID.MaxLength = 15;
			textBoxUserID.Name = "textBoxUserID";
			textBoxUserID.ReadOnly = true;
			textBoxUserID.Size = new System.Drawing.Size(184, 20);
			textBoxUserID.TabIndex = 0;
			textBoxUserID.TabStop = false;
			textBoxUserID.Tag = "";
			textBoxOverdueInvoiceDays.Location = new System.Drawing.Point(157, 45);
			textBoxOverdueInvoiceDays.MaxValue = 99;
			textBoxOverdueInvoiceDays.MinValue = 0;
			textBoxOverdueInvoiceDays.Name = "textBoxOverdueInvoiceDays";
			textBoxOverdueInvoiceDays.PromptChar = ' ';
			textBoxOverdueInvoiceDays.Size = new System.Drawing.Size(62, 21);
			textBoxOverdueInvoiceDays.TabIndex = 2;
			textBoxOverdueInvoiceDays.Tag = "1";
			textBoxOverdueBillDays.Location = new System.Drawing.Point(157, 72);
			textBoxOverdueBillDays.MaxValue = 99;
			textBoxOverdueBillDays.MinValue = 0;
			textBoxOverdueBillDays.Name = "textBoxOverdueBillDays";
			textBoxOverdueBillDays.PromptChar = ' ';
			textBoxOverdueBillDays.Size = new System.Drawing.Size(62, 21);
			textBoxOverdueBillDays.TabIndex = 4;
			textBoxOverdueBillDays.Tag = "2";
			textBoxPDCOnhandDays.Location = new System.Drawing.Point(157, 99);
			textBoxPDCOnhandDays.MaxValue = 99;
			textBoxPDCOnhandDays.MinValue = 0;
			textBoxPDCOnhandDays.Name = "textBoxPDCOnhandDays";
			textBoxPDCOnhandDays.PromptChar = ' ';
			textBoxPDCOnhandDays.Size = new System.Drawing.Size(62, 21);
			textBoxPDCOnhandDays.TabIndex = 6;
			textBoxPDCOnhandDays.Tag = "3";
			textBoxPDCIssuedDays.Location = new System.Drawing.Point(157, 125);
			textBoxPDCIssuedDays.MaxValue = 99;
			textBoxPDCIssuedDays.MinValue = 0;
			textBoxPDCIssuedDays.Name = "textBoxPDCIssuedDays";
			textBoxPDCIssuedDays.PromptChar = ' ';
			textBoxPDCIssuedDays.Size = new System.Drawing.Size(62, 21);
			textBoxPDCIssuedDays.TabIndex = 8;
			textBoxPDCIssuedDays.Tag = "4";
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(465, 220);
			base.Controls.Add(textBoxPDCIssuedDays);
			base.Controls.Add(textBoxPDCOnhandDays);
			base.Controls.Add(textBoxOverdueBillDays);
			base.Controls.Add(textBoxOverdueInvoiceDays);
			base.Controls.Add(checkBoxPDCIssued);
			base.Controls.Add(checkBoxPDCOnhand);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(checkBoxOverdueBill);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(checkBoxOverdueInvoice);
			base.Controls.Add(panelButtons);
			base.Controls.Add(textBoxUserID);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(formManager);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ReminderSetupForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Reminders Setup";
			base.Load += new System.EventHandler(AccountGroupDetailsForm_Load);
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)textBoxOverdueInvoiceDays).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxOverdueBillDays).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxPDCOnhandDays).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxPDCIssuedDays).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
