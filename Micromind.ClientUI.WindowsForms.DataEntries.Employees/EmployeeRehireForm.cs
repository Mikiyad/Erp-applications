using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeRehireForm : Form, IForm
	{
		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Rehire";

		private const string IDFIELD_CONST = "ActivityID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private MMTextBox textBoxFromLocation;

		private MMTextBox textBoxFromDivision;

		private MMLabel mmLabel4;

		private MMTextBox textBoxFromDepartment;

		private FormManager formManager;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxRequestedBy;

		private MMLabel mmLabel9;

		private MMTextBox textBoxReference;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxCode;

		private MMLabel label1TransactionID;

		private MMLabel mmLabel12;

		private MMSDateTimePicker dateTimePickerDate;

		private MMTextBox textBoxDesignation;

		private MMLabel mmLabel7;

		private MMLabel labelTerminated;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5037;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.Clear();
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					labelTerminated.Visible = false;
				}
				comboBoxEmployee.Enabled = isNewRecord;
				MMLabel mMLabel = label1TransactionID;
				bool visible = textBoxCode.Visible = !isNewRecord;
				mMLabel.Visible = visible;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public EmployeeRehireForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeRehireForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Rehire);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)3;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Reason"] = textBoxReason.Text;
				dataRow["Reference"] = textBoxReference.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeRehireTable.Rows[0] : currentData.EmployeeRehireTable.NewRow());
				dataRow["RequestedBy"] = textBoxRequestedBy.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeRehireTable.Rows.Add(dataRow);
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
			SaveData();
			textBoxCode.Focus();
		}

		public void LoadData(int id)
		{
			try
			{
				if (CanClose())
				{
					currentData = Factory.EmployeeActivitySystem.GetEmployeeActivityByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id.ToString();
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
				DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
				textBoxCode.Text = dataRow["ActivityID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReason.Text = dataRow["Reason"].ToString();
				textBoxReference.Text = dataRow["Reference"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				dataRow = currentData.Tables["Employee_Rehire"].Rows[0];
				textBoxRequestedBy.Text = dataRow["RequestedBy"].ToString();
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Rehire, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Rehire, isUpdate: false);
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
			catch (CompanyException ex)
			{
				ErrorHelper.ErrorMessage(ex.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value < DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (comboBoxEmployee.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (!labelTerminated.Visible)
			{
				ErrorHelper.InformationMessage("This employee is already active.");
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
			comboBoxEmployee.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
			textBoxDesignation.Clear();
			textBoxNote.Clear();
			textBoxReason.Clear();
			dateTimePickerDate.Value = DateTime.Today;
			textBoxReference.Clear();
			textBoxRequestedBy.Clear();
			labelTerminated.Visible = false;
			formManager.ResetDirty();
			comboBoxEmployee.Focus();
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private bool Delete()
		{
			try
			{
				return false;
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
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Rehire", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Rehire", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Rehire", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Rehire", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
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

		private void EmployeeRehireForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
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
			}
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				if (comboBoxEmployee.SelectedID == "")
				{
					textBoxFromLocation.Text = "";
					textBoxFromDivision.Text = "";
					textBoxFromDepartment.Text = "";
					textBoxDesignation.Text = "";
					labelTerminated.Visible = false;
				}
				else
				{
					DataSet employeeBriefInfo = Factory.EmployeeSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
					if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
						textBoxFromLocation.Text = dataRow["WorkLocationName"].ToString();
						textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
						textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
						textBoxDesignation.Text = dataRow["PositionName"].ToString();
						bool result = false;
						bool.TryParse(dataRow["IsTerminated"].ToString(), out result);
						if (result && IsNewRecord)
						{
							labelTerminated.Visible = true;
						}
						else
						{
							labelTerminated.Visible = false;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeRehireForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxRequestedBy = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			label1TransactionID = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			labelTerminated = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(595, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 314);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(595, 40);
			panelButtons.TabIndex = 7;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(595, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 4;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(485, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.Location = new System.Drawing.Point(329, 77);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(141, 20);
			textBoxFromLocation.TabIndex = 1;
			textBoxFromLocation.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.Location = new System.Drawing.Point(112, 99);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(140, 20);
			textBoxFromDivision.TabIndex = 4;
			textBoxFromDivision.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(258, 78);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Location:";
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(329, 99);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(141, 20);
			textBoxFromDepartment.TabIndex = 5;
			textBoxFromDepartment.TabStop = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(112, 33);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(177, 20);
			comboBoxEmployee.TabIndex = 0;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(112, 55);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(358, 20);
			textBoxEmployeeName.TabIndex = 1;
			textBoxEmployeeName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(12, 102);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 9;
			mmLabel3.Text = "Division:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(258, 102);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 9;
			mmLabel5.Text = "Department:";
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.Location = new System.Drawing.Point(112, 174);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(358, 20);
			textBoxReason.TabIndex = 3;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(11, 177);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(47, 13);
			mmLabel1.TabIndex = 9;
			mmLabel1.Text = "Reason:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(11, 199);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(77, 13);
			mmLabel8.TabIndex = 9;
			mmLabel8.Text = "Requested By:";
			textBoxRequestedBy.BackColor = System.Drawing.Color.White;
			textBoxRequestedBy.CustomReportFieldName = "";
			textBoxRequestedBy.CustomReportKey = "";
			textBoxRequestedBy.CustomReportValueType = 1;
			textBoxRequestedBy.IsComboTextBox = false;
			textBoxRequestedBy.Location = new System.Drawing.Point(112, 196);
			textBoxRequestedBy.MaxLength = 30;
			textBoxRequestedBy.Name = "textBoxRequestedBy";
			textBoxRequestedBy.Size = new System.Drawing.Size(140, 20);
			textBoxRequestedBy.TabIndex = 4;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(258, 199);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(60, 13);
			mmLabel9.TabIndex = 9;
			mmLabel9.Text = "Reference:";
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.Location = new System.Drawing.Point(329, 196);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(141, 20);
			textBoxReference.TabIndex = 5;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(12, 233);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(30, 13);
			mmLabel10.TabIndex = 9;
			mmLabel10.Text = "Note";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(112, 233);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(469, 55);
			textBoxNote.TabIndex = 6;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(383, 33);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 1;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			label1TransactionID.AutoSize = true;
			label1TransactionID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1TransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			label1TransactionID.IsFieldHeader = false;
			label1TransactionID.IsRequired = false;
			label1TransactionID.Location = new System.Drawing.Point(297, 35);
			label1TransactionID.Name = "label1TransactionID";
			label1TransactionID.PenWidth = 1f;
			label1TransactionID.ShowBorder = false;
			label1TransactionID.Size = new System.Drawing.Size(80, 13);
			label1TransactionID.TabIndex = 9;
			label1TransactionID.Text = "Transaction ID:";
			label1TransactionID.Visible = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = true;
			mmLabel12.Location = new System.Drawing.Point(12, 156);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(79, 13);
			mmLabel12.TabIndex = 0;
			mmLabel12.Text = "Rehire Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(112, 152);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2015, 3, 28, 11, 8, 46, 380);
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.Location = new System.Drawing.Point(112, 77);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(140, 20);
			textBoxDesignation.TabIndex = 1;
			textBoxDesignation.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(12, 80);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(47, 13);
			mmLabel7.TabIndex = 9;
			mmLabel7.Text = "Position:";
			labelTerminated.AutoSize = true;
			labelTerminated.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelTerminated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelTerminated.ForeColor = System.Drawing.Color.Red;
			labelTerminated.IsFieldHeader = false;
			labelTerminated.IsRequired = false;
			labelTerminated.Location = new System.Drawing.Point(297, 36);
			labelTerminated.Name = "labelTerminated";
			labelTerminated.PenWidth = 1f;
			labelTerminated.ShowBorder = false;
			labelTerminated.Size = new System.Drawing.Size(115, 13);
			labelTerminated.TabIndex = 9;
			labelTerminated.Text = "Employee is terminated";
			labelTerminated.Visible = false;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance13;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(12, 32);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel6.TabIndex = 130;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Employee:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(595, 354);
			base.Controls.Add(ultraFormattedLinkLabel6);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxReference);
			base.Controls.Add(textBoxRequestedBy);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxReason);
			base.Controls.Add(textBoxFromDepartment);
			base.Controls.Add(textBoxFromDivision);
			base.Controls.Add(labelTerminated);
			base.Controls.Add(label1TransactionID);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(textBoxDesignation);
			base.Controls.Add(textBoxFromLocation);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel12);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(603, 381);
			base.Name = "EmployeeRehireForm";
			Text = "Employee Rehire Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
