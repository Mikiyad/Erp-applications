using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class ProvisionTypeDetailsForm : Form, IForm
	{
		private EmployeeProvisionTypeData currentData;

		private const string TABLENAME_CONST = "Employee_Provision_Type";

		private const string IDFIELD_CONST = "ProvisionTypeID";

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

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private AllAccountsComboBox provisionAccountsComboBox;

		private AllAccountsComboBox expenseAccountsComboBox;

		private MMTextBox mmTextBoxProvisionName;

		private MMTextBox mmTextBoxExpenseName;

		private Label label1;

		private RadioButton radioButtonEOS;

		private RadioButton radioButtonLeave;

		private RadioButton radioButtonTicket;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5027;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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
					textBoxCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
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

		public ProvisionTypeDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ProvisionTypeDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeProvisionTypeData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeProvisionTypeTable.Rows[0] : currentData.EmployeeProvisionTypeTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ProvisionTypeID"] = textBoxCode.Text.Trim();
				dataRow["ProvisionTypeName"] = textBoxName.Text.Trim();
				dataRow["ExpenseAccountID"] = expenseAccountsComboBox.SelectedID;
				dataRow["ProvisionAccountID"] = provisionAccountsComboBox.SelectedID;
				if (radioButtonEOS.Checked)
				{
					dataRow["ProvisionFor"] = 1;
				}
				else if (radioButtonLeave.Checked)
				{
					dataRow["ProvisionFor"] = 2;
				}
				else if (radioButtonTicket.Checked)
				{
					dataRow["ProvisionFor"] = 3;
				}
				else
				{
					dataRow["ProvisionFor"] = DBNull.Value;
				}
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeProvisionTypeTable.Rows.Add(dataRow);
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

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.ProvisionTypeSystem.GetProvisionTypeByID(id.Trim());
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
				textBoxCode.Text = dataRow["ProvisionTypeID"].ToString();
				textBoxName.Text = dataRow["ProvisionTypeName"].ToString();
				expenseAccountsComboBox.SelectedID = dataRow["ExpenseAccountID"].ToString();
				provisionAccountsComboBox.SelectedID = dataRow["ProvisionAccountID"].ToString();
				ProvisionForEnum provisionForEnum = ProvisionForEnum.EOS;
				switch ((dataRow["ProvisionFor"] == DBNull.Value) ? 1 : int.Parse(dataRow["ProvisionFor"].ToString()))
				{
				case 1:
					radioButtonEOS.Checked = true;
					break;
				case 2:
					radioButtonLeave.Checked = true;
					break;
				default:
					radioButtonTicket.Checked = true;
					break;
				}
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.ProvisionTypeSystem.CreateProvisionType(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeProvisionType, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ProvisionTypeSystem.UpdateProvisionType(currentData);
				}
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Employee_Provision_Type", "ProvisionTypeID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Employee_Provision_Type", "ProvisionTypeID");
			textBoxName.Clear();
			expenseAccountsComboBox.Clear();
			provisionAccountsComboBox.Clear();
			mmTextBoxExpenseName.Clear();
			mmTextBoxProvisionName.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
			radioButtonEOS.Checked = true;
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.ProvisionTypeSystem.DeleteProvisionType(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Employee_Provision_Type", "ProvisionTypeID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Employee_Provision_Type", "ProvisionTypeID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Employee_Provision_Type", "ProvisionTypeID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Employee_Provision_Type", "ProvisionTypeID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee_Provision_Type", "ProvisionTypeID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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

		private void ProvisionTypeDetailsForm_Load(object sender, EventArgs e)
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EmployeeProvisionType);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
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
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.ProvisionTypeDetailsForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			provisionAccountsComboBox = new Micromind.DataControls.AllAccountsComboBox();
			mmTextBoxProvisionName = new Micromind.UISupport.MMTextBox();
			expenseAccountsComboBox = new Micromind.DataControls.AllAccountsComboBox();
			mmTextBoxExpenseName = new Micromind.UISupport.MMTextBox();
			label1 = new System.Windows.Forms.Label();
			radioButtonEOS = new System.Windows.Forms.RadioButton();
			radioButtonLeave = new System.Windows.Forms.RadioButton();
			radioButtonTicket = new System.Windows.Forms.RadioButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)provisionAccountsComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)expenseAccountsComboBox).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(470, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
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
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 160);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(470, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(470, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(360, 8);
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
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 39);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(72, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Type Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(108, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 1;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 61);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Type Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(108, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(324, 20);
			textBoxName.TabIndex = 4;
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
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(293, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(8, 83);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(89, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "Provision A/C:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(8, 106);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(58, 13);
			mmLabel3.TabIndex = 17;
			mmLabel3.Text = "Exp A/C:";
			provisionAccountsComboBox.Assigned = false;
			provisionAccountsComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			provisionAccountsComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			provisionAccountsComboBox.CustomReportFieldName = "";
			provisionAccountsComboBox.CustomReportKey = "";
			provisionAccountsComboBox.CustomReportValueType = 1;
			provisionAccountsComboBox.DescriptionTextBox = mmTextBoxProvisionName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			provisionAccountsComboBox.DisplayLayout.Appearance = appearance;
			provisionAccountsComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			provisionAccountsComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			provisionAccountsComboBox.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			provisionAccountsComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			provisionAccountsComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			provisionAccountsComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			provisionAccountsComboBox.DisplayLayout.MaxColScrollRegions = 1;
			provisionAccountsComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			provisionAccountsComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			provisionAccountsComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			provisionAccountsComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			provisionAccountsComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			provisionAccountsComboBox.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			provisionAccountsComboBox.DisplayLayout.Override.CellAppearance = appearance8;
			provisionAccountsComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			provisionAccountsComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			provisionAccountsComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			provisionAccountsComboBox.DisplayLayout.Override.HeaderAppearance = appearance10;
			provisionAccountsComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			provisionAccountsComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			provisionAccountsComboBox.DisplayLayout.Override.RowAppearance = appearance11;
			provisionAccountsComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			provisionAccountsComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			provisionAccountsComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			provisionAccountsComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			provisionAccountsComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			provisionAccountsComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			provisionAccountsComboBox.Editable = true;
			provisionAccountsComboBox.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			provisionAccountsComboBox.FilterString = "";
			provisionAccountsComboBox.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			provisionAccountsComboBox.FilterSysDocID = "";
			provisionAccountsComboBox.HasAllAccount = false;
			provisionAccountsComboBox.HasCustom = false;
			provisionAccountsComboBox.IsDataLoaded = false;
			provisionAccountsComboBox.Location = new System.Drawing.Point(108, 80);
			provisionAccountsComboBox.MaxDropDownItems = 12;
			provisionAccountsComboBox.Name = "provisionAccountsComboBox";
			provisionAccountsComboBox.ShowInactiveItems = false;
			provisionAccountsComboBox.ShowQuickAdd = true;
			provisionAccountsComboBox.Size = new System.Drawing.Size(100, 20);
			provisionAccountsComboBox.TabIndex = 19;
			provisionAccountsComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBoxProvisionName.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBoxProvisionName.CustomReportFieldName = "";
			mmTextBoxProvisionName.CustomReportKey = "";
			mmTextBoxProvisionName.CustomReportValueType = 1;
			mmTextBoxProvisionName.IsComboTextBox = false;
			mmTextBoxProvisionName.IsModified = false;
			mmTextBoxProvisionName.Location = new System.Drawing.Point(209, 80);
			mmTextBoxProvisionName.MaxLength = 64;
			mmTextBoxProvisionName.Name = "mmTextBoxProvisionName";
			mmTextBoxProvisionName.ReadOnly = true;
			mmTextBoxProvisionName.Size = new System.Drawing.Size(223, 20);
			mmTextBoxProvisionName.TabIndex = 21;
			expenseAccountsComboBox.Assigned = false;
			expenseAccountsComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			expenseAccountsComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			expenseAccountsComboBox.CustomReportFieldName = "";
			expenseAccountsComboBox.CustomReportKey = "";
			expenseAccountsComboBox.CustomReportValueType = 1;
			expenseAccountsComboBox.DescriptionTextBox = mmTextBoxExpenseName;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			expenseAccountsComboBox.DisplayLayout.Appearance = appearance13;
			expenseAccountsComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			expenseAccountsComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			expenseAccountsComboBox.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			expenseAccountsComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			expenseAccountsComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			expenseAccountsComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			expenseAccountsComboBox.DisplayLayout.MaxColScrollRegions = 1;
			expenseAccountsComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			expenseAccountsComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			expenseAccountsComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			expenseAccountsComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			expenseAccountsComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			expenseAccountsComboBox.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			expenseAccountsComboBox.DisplayLayout.Override.CellAppearance = appearance20;
			expenseAccountsComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			expenseAccountsComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			expenseAccountsComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			expenseAccountsComboBox.DisplayLayout.Override.HeaderAppearance = appearance22;
			expenseAccountsComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			expenseAccountsComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			expenseAccountsComboBox.DisplayLayout.Override.RowAppearance = appearance23;
			expenseAccountsComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			expenseAccountsComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			expenseAccountsComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			expenseAccountsComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			expenseAccountsComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			expenseAccountsComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			expenseAccountsComboBox.Editable = true;
			expenseAccountsComboBox.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			expenseAccountsComboBox.FilterString = "";
			expenseAccountsComboBox.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			expenseAccountsComboBox.FilterSysDocID = "";
			expenseAccountsComboBox.HasAllAccount = false;
			expenseAccountsComboBox.HasCustom = false;
			expenseAccountsComboBox.IsDataLoaded = false;
			expenseAccountsComboBox.Location = new System.Drawing.Point(108, 102);
			expenseAccountsComboBox.MaxDropDownItems = 12;
			expenseAccountsComboBox.Name = "expenseAccountsComboBox";
			expenseAccountsComboBox.ShowInactiveItems = false;
			expenseAccountsComboBox.ShowQuickAdd = true;
			expenseAccountsComboBox.Size = new System.Drawing.Size(100, 20);
			expenseAccountsComboBox.TabIndex = 20;
			expenseAccountsComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBoxExpenseName.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBoxExpenseName.CustomReportFieldName = "";
			mmTextBoxExpenseName.CustomReportKey = "";
			mmTextBoxExpenseName.CustomReportValueType = 1;
			mmTextBoxExpenseName.IsComboTextBox = false;
			mmTextBoxExpenseName.IsModified = false;
			mmTextBoxExpenseName.Location = new System.Drawing.Point(209, 102);
			mmTextBoxExpenseName.MaxLength = 64;
			mmTextBoxExpenseName.Name = "mmTextBoxExpenseName";
			mmTextBoxExpenseName.ReadOnly = true;
			mmTextBoxExpenseName.Size = new System.Drawing.Size(223, 20);
			mmTextBoxExpenseName.TabIndex = 22;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 128);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(71, 13);
			label1.TabIndex = 23;
			label1.Text = "Provision For:";
			radioButtonEOS.AutoSize = true;
			radioButtonEOS.Location = new System.Drawing.Point(108, 127);
			radioButtonEOS.Name = "radioButtonEOS";
			radioButtonEOS.Size = new System.Drawing.Size(47, 17);
			radioButtonEOS.TabIndex = 24;
			radioButtonEOS.TabStop = true;
			radioButtonEOS.Text = "EOS";
			radioButtonEOS.UseVisualStyleBackColor = true;
			radioButtonLeave.AutoSize = true;
			radioButtonLeave.Location = new System.Drawing.Point(163, 127);
			radioButtonLeave.Name = "radioButtonLeave";
			radioButtonLeave.Size = new System.Drawing.Size(55, 17);
			radioButtonLeave.TabIndex = 25;
			radioButtonLeave.TabStop = true;
			radioButtonLeave.Text = "Leave";
			radioButtonLeave.UseVisualStyleBackColor = true;
			radioButtonTicket.AutoSize = true;
			radioButtonTicket.Location = new System.Drawing.Point(224, 127);
			radioButtonTicket.Name = "radioButtonTicket";
			radioButtonTicket.Size = new System.Drawing.Size(55, 17);
			radioButtonTicket.TabIndex = 26;
			radioButtonTicket.TabStop = true;
			radioButtonTicket.Text = "Ticket";
			radioButtonTicket.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(470, 200);
			base.Controls.Add(radioButtonTicket);
			base.Controls.Add(radioButtonLeave);
			base.Controls.Add(radioButtonEOS);
			base.Controls.Add(label1);
			base.Controls.Add(mmTextBoxExpenseName);
			base.Controls.Add(mmTextBoxProvisionName);
			base.Controls.Add(expenseAccountsComboBox);
			base.Controls.Add(provisionAccountsComboBox);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ProvisionTypeDetailsForm";
			Text = "Provision Type";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)provisionAccountsComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)expenseAccountsComboBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
