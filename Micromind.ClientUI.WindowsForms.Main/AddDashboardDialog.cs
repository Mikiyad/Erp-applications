using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class AddDashboardDialog : Form
	{
		private DashboardData currentData;

		private const string TABLENAME_CONST = "Dashboard";

		private const string IDFIELD_CONST = "DashboardID";

		private bool isNewRecord = true;

		private string templateName = "";

		private string userID = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private MMTextBox textBoxCode;

		private Label label1;

		private Label label2;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private Label labelLayoutTemplate;

		private CheckBox checkBoxChangeLayout;

		private XPButton buttonChangeLayout;

		private ComboBox comboBoxTemplate;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					textBoxCode.ReadOnly = false;
				}
				else
				{
					textBoxCode.ReadOnly = true;
				}
			}
		}

		public AddDashboardDialog()
		{
			InitializeComponent();
			AddEvents();
			ClearForm();
		}

		private void AddEvents()
		{
			base.Load += DashboardDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new DashboardData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.DashboardTable.Rows[0] : currentData.DashboardTable.NewRow();
				dataRow.BeginEdit();
				dataRow["DashboardID"] = textBoxCode.Text.Trim();
				dataRow["Name"] = textBoxName.Text.Trim();
				dataRow["UserID"] = Global.CurrentUser;
				dataRow["RowIndex"] = 0;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.DashboardTable.Rows.Add(dataRow);
				}
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
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.DashboardSystem.GetDashboardByID(id, Global.CurrentUser);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
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
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["DashboardID"].ToString();
				textBoxName.Text = dataRow["Name"].ToString();
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
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
				bool flag = (!isNewRecord) ? Factory.DashboardSystem.UpdateDashboard(currentData) : Factory.DashboardSystem.CreateDashboard(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
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
			if (textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter a name.");
				return false;
			}
			if (isNewRecord && textBoxCode.Text == "")
			{
				textBoxCode.Text = textBoxName.Text.Replace(" ", "");
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Dashboard", "DashboardID", textBoxCode.Text.Trim(), "UserID", Global.CurrentUser))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
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
			textBoxCode.Clear();
			textBoxName.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void DashboardGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void DashboardGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.DashboardSystem.DeleteDashboard(textBoxCode.Text, Global.CurrentUser);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Dashboard", "DashboardID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Dashboard", "DashboardID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Dashboard", "DashboardID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Dashboard", "DashboardID"));
		}

		public void OnActivated()
		{
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

		private void DashboardDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxName_Validated(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxCode.Text = textBoxName.Text.Replace(" ", "");
			}
		}

		private void checkBoxChangeLayout_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxChangeLayout.Checked)
			{
				comboBoxTemplate.Visible = true;
				labelLayoutTemplate.Visible = true;
				buttonChangeLayout.Visible = true;
			}
			else
			{
				comboBoxTemplate.Visible = false;
				labelLayoutTemplate.Visible = false;
				buttonChangeLayout.Visible = false;
			}
		}

		private void buttonChangeLayout_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to update this dashboard?") == DialogResult.Yes && ErrorHelper.WarningMessageOkCancel("Upading DashBoard cannot be reverted.", "Do you still want to continue?") != DialogResult.Cancel && (0 & (Factory.DashboardSystem.UpdateDashboardWithTemplate(textBoxCode.Text, templateName, Global.CurrentUser) ? 1 : 0)) != 0)
			{
				Close();
			}
		}

		private void AddDashboardDialog_Load(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.DashboardSystem.GetAvailableDashboardTemplates();
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
			{
				dataSet.Tables[1].Rows.Add("None");
				comboBoxTemplate.DataSource = dataSet.Tables[1];
				comboBoxTemplate.DisplayMember = dataSet.Tables[1].Columns[0].ToString();
			}
		}

		private void comboBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			templateName = comboBoxTemplate.Text;
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxName = new Micromind.UISupport.MMTextBox();
			labelLayoutTemplate = new System.Windows.Forms.Label();
			checkBoxChangeLayout = new System.Windows.Forms.CheckBox();
			buttonChangeLayout = new Micromind.UISupport.XPButton();
			comboBoxTemplate = new System.Windows.Forms.ComboBox();
			formManager = new Micromind.DataControls.FormManager();
			panelButtons.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 138);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(369, 40);
			panelButtons.TabIndex = 2;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(184, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(85, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonSave_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(369, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(275, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(83, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(xpButton1_Click);
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(86, 110);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(38, 20);
			textBoxCode.TabIndex = 4;
			textBoxCode.Visible = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 113);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(35, 13);
			label1.TabIndex = 11;
			label1.Text = "Code:";
			label1.Visible = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 24);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 13;
			label2.Text = "Name:";
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(86, 21);
			textBoxName.MaxLength = 15;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(250, 20);
			textBoxName.TabIndex = 1;
			textBoxName.Validated += new System.EventHandler(textBoxName_Validated);
			labelLayoutTemplate.AutoSize = true;
			labelLayoutTemplate.Location = new System.Drawing.Point(12, 77);
			labelLayoutTemplate.Name = "labelLayoutTemplate";
			labelLayoutTemplate.Size = new System.Drawing.Size(57, 13);
			labelLayoutTemplate.TabIndex = 18;
			labelLayoutTemplate.Text = "Template :";
			labelLayoutTemplate.Visible = false;
			checkBoxChangeLayout.AutoSize = true;
			checkBoxChangeLayout.Location = new System.Drawing.Point(86, 51);
			checkBoxChangeLayout.Name = "checkBoxChangeLayout";
			checkBoxChangeLayout.Size = new System.Drawing.Size(98, 17);
			checkBoxChangeLayout.TabIndex = 2;
			checkBoxChangeLayout.Text = "Change Layout";
			checkBoxChangeLayout.UseVisualStyleBackColor = true;
			checkBoxChangeLayout.CheckedChanged += new System.EventHandler(checkBoxChangeLayout_CheckedChanged);
			buttonChangeLayout.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonChangeLayout.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonChangeLayout.BackColor = System.Drawing.Color.DarkGray;
			buttonChangeLayout.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonChangeLayout.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonChangeLayout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonChangeLayout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonChangeLayout.Location = new System.Drawing.Point(251, 96);
			buttonChangeLayout.Name = "buttonChangeLayout";
			buttonChangeLayout.Size = new System.Drawing.Size(85, 24);
			buttonChangeLayout.TabIndex = 21;
			buttonChangeLayout.Text = "Change";
			buttonChangeLayout.UseVisualStyleBackColor = false;
			buttonChangeLayout.Visible = false;
			buttonChangeLayout.Click += new System.EventHandler(buttonChangeLayout_Click);
			comboBoxTemplate.FormattingEnabled = true;
			comboBoxTemplate.Location = new System.Drawing.Point(86, 72);
			comboBoxTemplate.Name = "comboBoxTemplate";
			comboBoxTemplate.Size = new System.Drawing.Size(250, 21);
			comboBoxTemplate.TabIndex = 22;
			comboBoxTemplate.Visible = false;
			comboBoxTemplate.SelectedIndexChanged += new System.EventHandler(comboBoxTemplate_SelectedIndexChanged);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 17;
			formManager.Text = "formManager1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(369, 178);
			base.Controls.Add(comboBoxTemplate);
			base.Controls.Add(buttonChangeLayout);
			base.Controls.Add(checkBoxChangeLayout);
			base.Controls.Add(labelLayoutTemplate);
			base.Controls.Add(formManager);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxName);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddDashboardDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			Text = "Dashboard";
			base.Load += new System.EventHandler(AddDashboardDialog_Load);
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
