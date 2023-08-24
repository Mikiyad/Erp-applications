using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
	public class PayrollItemDetailsForm : Form, IForm
	{
		private PayrollItemData currentData;

		private const string TABLENAME_CONST = "PayrollItem";

		private const string IDFIELD_CONST = "PayrollItemID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool isDeduction;

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

		private CheckBox checkBoxInLeaveSalary;

		private CheckBox checkBoxInDeduction;

		private AllAccountsComboBox comboBoxAccount;

		private MMTextBox textBoxNote;

		private Label label1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ComboBox comboBoxPayCode;

		private MMLabel labelType;

		private MMTextBox textBoxAccountName;

		private ComboBox comboBoxDeductionCode;

		private Panel panelPaymentChoice;

		private CheckBox checkBoxInEOSB;

		private CheckBox checkBoxInOvertime;

		private CheckBox checkBoxInFixed;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private CheckBox checkBoxSalaryDeduction;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5023;

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

		public bool IsDeduction
		{
			get
			{
				return isDeduction;
			}
			set
			{
				if (value)
				{
					Text = "Deduction Item";
					labelCode.Text = "Deduction Code:";
				}
				else
				{
					Text = "Payment Item";
					labelCode.Text = "Payment Code:";
				}
				comboBoxDeductionCode.Visible = false;
				MMLabel mMLabel = labelType;
				bool visible = comboBoxPayCode.Visible = !value;
				mMLabel.Visible = visible;
				panelPaymentChoice.Visible = !value;
				isDeduction = value;
			}
		}

		public PayrollItemDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += PayrollItemDetailsForm_Load;
			comboBoxAccount.SelectedIndexChanged += comboBoxAccount_SelectedIndexChanged;
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAccountName.Text = comboBoxAccount.SelectedName;
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new PayrollItemData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.PayrollItemTable.Rows[0] : currentData.PayrollItemTable.NewRow();
					dataRow.BeginEdit();
					dataRow["PayrollItemID"] = textBoxCode.Text.Trim();
					dataRow["PayrollItemName"] = textBoxName.Text.Trim();
					if (IsDeduction)
					{
						dataRow["PayrollItemType"] = 2;
					}
					else
					{
						dataRow["PayrollItemType"] = 1;
					}
					if (IsDeduction)
					{
						dataRow["PayCodeType"] = comboBoxDeductionCode.SelectedIndex + 1;
					}
					else
					{
						dataRow["PayCodeType"] = comboBoxPayCode.SelectedIndex + 1;
					}
					dataRow["AccountID"] = comboBoxAccount.SelectedID;
					dataRow["Note"] = textBoxNote.Text;
					dataRow["Inactive"] = checkBoxInactive.Checked;
					dataRow["InLeaveSalary"] = checkBoxInLeaveSalary.Checked;
					dataRow["InDeduction"] = checkBoxInDeduction.Checked;
					dataRow["InServiceBenefit"] = checkBoxInEOSB.Checked;
					dataRow["InOvertime"] = checkBoxInOvertime.Checked;
					dataRow["InFixed"] = checkBoxInFixed.Checked;
					dataRow["InSalaryDeduction"] = checkBoxSalaryDeduction.Checked;
					dataRow.EndEdit();
					if (isNewRecord)
					{
						currentData.PayrollItemTable.Rows.Add(dataRow);
					}
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
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
					currentData = Factory.PayrollItemSystem.GetPayrollItemByID(id.Trim());
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
			checked
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["PayrollItemID"].ToString();
					textBoxName.Text = dataRow["PayrollItemName"].ToString();
					if (IsDeduction)
					{
						comboBoxDeductionCode.SelectedIndex = int.Parse(dataRow["PayCodeType"].ToString()) - 1;
					}
					else
					{
						comboBoxPayCode.SelectedIndex = int.Parse(dataRow["PayCodeType"].ToString()) - 1;
					}
					textBoxNote.Text = dataRow["Note"].ToString();
					checkBoxInLeaveSalary.Checked = bool.Parse(dataRow["InLeaveSalary"].ToString());
					checkBoxInDeduction.Checked = bool.Parse(dataRow["InDeduction"].ToString());
					checkBoxInEOSB.Checked = bool.Parse(dataRow["InServiceBenefit"].ToString());
					checkBoxInOvertime.Checked = (!string.IsNullOrEmpty(dataRow["InOvertime"].ToString()) && bool.Parse(dataRow["InOvertime"].ToString()));
					checkBoxInFixed.Checked = (!string.IsNullOrEmpty(dataRow["InFixed"].ToString()) && bool.Parse(dataRow["InFixed"].ToString()));
					comboBoxAccount.SelectedID = dataRow["AccountID"].ToString();
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
					if (dataRow["InSalaryDeduction"] != DBNull.Value)
					{
						checkBoxSalaryDeduction.Checked = bool.Parse(dataRow["InSalaryDeduction"].ToString());
					}
					else
					{
						checkBoxSalaryDeduction.Checked = false;
					}
				}
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
					flag = Factory.PayrollItemSystem.CreatePayrollItem(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.PayrollItem, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PayrollItemSystem.UpdatePayrollItem(currentData);
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
			if (comboBoxAccount.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				comboBoxAccount.Focus();
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("PayrollItem", "PayrollItemID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			if (!IsDeduction && comboBoxPayCode.SelectedIndex < 0)
			{
				ErrorHelper.InformationMessage("Please select a payment type.");
				comboBoxPayCode.Focus();
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
			textBoxNote.Clear();
			comboBoxPayCode.SelectedIndex = 0;
			comboBoxDeductionCode.SelectedIndex = 0;
			comboBoxAccount.Clear();
			textBoxAccountName.Clear();
			checkBoxInactive.Checked = false;
			checkBoxInDeduction.Checked = true;
			checkBoxInLeaveSalary.Checked = true;
			checkBoxInEOSB.Checked = true;
			checkBoxInOvertime.Checked = false;
			checkBoxInFixed.Checked = false;
			checkBoxSalaryDeduction.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void PayrollItemGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void PayrollItemGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.PayrollItemSystem.DeletePayrollItem(textBoxCode.Text);
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
			byte b = 1;
			if (IsDeduction)
			{
				b = 2;
			}
			LoadData(DatabaseHelper.GetNextID("PayrollItem", "PayrollItemID", textBoxCode.Text, "PayrollItemType", b.ToString()));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			byte b = 1;
			if (IsDeduction)
			{
				b = 2;
			}
			LoadData(DatabaseHelper.GetPreviousID("PayrollItem", "PayrollItemID", textBoxCode.Text, "PayrollItemType", b.ToString()));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			byte b = 1;
			if (IsDeduction)
			{
				b = 2;
			}
			LoadData(DatabaseHelper.GetLastID("PayrollItem", "PayrollItemID", "PayrollItemType", b.ToString()));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			byte b = 1;
			if (IsDeduction)
			{
				b = 2;
			}
			LoadData(DatabaseHelper.GetFirstID("PayrollItem", "PayrollItemID", "PayrollItemType", b.ToString()));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					byte b = 1;
					if (IsDeduction)
					{
						b = 2;
					}
					if (Factory.DatabaseSystem.ExistFieldValue("PayrollItem", "PayrollItemID", toolStripTextBoxFind.Text.Trim(), "PayrollItemType", b.ToString()))
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

		private void PayrollItemDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDeductionCode.Items.Clear();
				comboBoxDeductionCode.Items.Add("Due");
				comboBoxDeductionCode.Items.Add("Expense");
				comboBoxDeductionCode.Items.Add("Insurance");
				comboBoxDeductionCode.Items.Add("Purchase");
				comboBoxDeductionCode.Items.Add("Tax");
				comboBoxDeductionCode.SelectedIndex = 0;
				comboBoxPayCode.Items.Clear();
				comboBoxPayCode.Items.Add("Basic Salary");
				comboBoxPayCode.Items.Add("Allowance");
				comboBoxPayCode.SelectedIndex = 0;
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
			FormHelper formHelper = new FormHelper();
			if (!isDeduction)
			{
				formHelper.ShowList(DataComboType.PayrollItem);
			}
			else
			{
				formHelper.ShowList(DataComboType.Deduction);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAccount.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.PayrollItemDetailsForm));
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
			checkBoxInLeaveSalary = new System.Windows.Forms.CheckBox();
			checkBoxInDeduction = new System.Windows.Forms.CheckBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			label1 = new System.Windows.Forms.Label();
			comboBoxPayCode = new System.Windows.Forms.ComboBox();
			labelType = new Micromind.UISupport.MMLabel();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxDeductionCode = new System.Windows.Forms.ComboBox();
			panelPaymentChoice = new System.Windows.Forms.Panel();
			checkBoxSalaryDeduction = new System.Windows.Forms.CheckBox();
			checkBoxInFixed = new System.Windows.Forms.CheckBox();
			checkBoxInOvertime = new System.Windows.Forms.CheckBox();
			checkBoxInEOSB = new System.Windows.Forms.CheckBox();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			panelPaymentChoice.SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(559, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 260);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(559, 40);
			panelButtons.TabIndex = 8;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(559, 1);
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
			xpButton1.Location = new System.Drawing.Point(449, 8);
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
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(92, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Payment Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(129, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(162, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Description:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(129, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(347, 20);
			textBoxName.TabIndex = 2;
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
			checkBoxInactive.Location = new System.Drawing.Point(297, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			checkBoxInLeaveSalary.AutoSize = true;
			checkBoxInLeaveSalary.Checked = true;
			checkBoxInLeaveSalary.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInLeaveSalary.Location = new System.Drawing.Point(15, 11);
			checkBoxInLeaveSalary.Name = "checkBoxInLeaveSalary";
			checkBoxInLeaveSalary.Size = new System.Drawing.Size(185, 17);
			checkBoxInLeaveSalary.TabIndex = 0;
			checkBoxInLeaveSalary.Text = "Include in leave salary calculation";
			checkBoxInLeaveSalary.UseVisualStyleBackColor = true;
			checkBoxInDeduction.AutoSize = true;
			checkBoxInDeduction.Checked = true;
			checkBoxInDeduction.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInDeduction.Location = new System.Drawing.Point(15, 34);
			checkBoxInDeduction.Name = "checkBoxInDeduction";
			checkBoxInDeduction.Size = new System.Drawing.Size(221, 17);
			checkBoxInDeduction.TabIndex = 1;
			checkBoxInDeduction.Text = "Include in unpaid leave salary deductions";
			checkBoxInDeduction.UseVisualStyleBackColor = true;
			comboBoxAccount.Assigned = false;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccount.Editable = true;
			comboBoxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccount.FilterString = "";
			comboBoxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccount.FilterSysDocID = "";
			comboBoxAccount.HasAllAccount = false;
			comboBoxAccount.HasCustom = false;
			comboBoxAccount.IsDataLoaded = false;
			comboBoxAccount.Location = new System.Drawing.Point(129, 103);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(162, 20);
			comboBoxAccount.TabIndex = 4;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(129, 147);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(347, 20);
			textBoxNote.TabIndex = 6;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 149);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 17;
			label1.Text = "Note:";
			comboBoxPayCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxPayCode.FormattingEnabled = true;
			comboBoxPayCode.Location = new System.Drawing.Point(129, 80);
			comboBoxPayCode.Name = "comboBoxPayCode";
			comboBoxPayCode.Size = new System.Drawing.Size(162, 21);
			comboBoxPayCode.TabIndex = 4;
			labelType.AutoSize = true;
			labelType.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelType.IsFieldHeader = false;
			labelType.IsRequired = true;
			labelType.Location = new System.Drawing.Point(12, 82);
			labelType.Name = "labelType";
			labelType.PenWidth = 1f;
			labelType.ShowBorder = false;
			labelType.Size = new System.Drawing.Size(39, 13);
			labelType.TabIndex = 3;
			labelType.Text = "Type:";
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.IsModified = false;
			textBoxAccountName.Location = new System.Drawing.Point(129, 125);
			textBoxAccountName.MaxLength = 255;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(347, 20);
			textBoxAccountName.TabIndex = 5;
			textBoxAccountName.TabStop = false;
			comboBoxDeductionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDeductionCode.FormattingEnabled = true;
			comboBoxDeductionCode.Location = new System.Drawing.Point(129, 80);
			comboBoxDeductionCode.Name = "comboBoxDeductionCode";
			comboBoxDeductionCode.Size = new System.Drawing.Size(162, 21);
			comboBoxDeductionCode.TabIndex = 3;
			panelPaymentChoice.Controls.Add(checkBoxSalaryDeduction);
			panelPaymentChoice.Controls.Add(checkBoxInFixed);
			panelPaymentChoice.Controls.Add(checkBoxInOvertime);
			panelPaymentChoice.Controls.Add(checkBoxInEOSB);
			panelPaymentChoice.Controls.Add(checkBoxInDeduction);
			panelPaymentChoice.Controls.Add(checkBoxInLeaveSalary);
			panelPaymentChoice.Location = new System.Drawing.Point(0, 179);
			panelPaymentChoice.Name = "panelPaymentChoice";
			panelPaymentChoice.Size = new System.Drawing.Size(529, 75);
			panelPaymentChoice.TabIndex = 7;
			checkBoxSalaryDeduction.AutoSize = true;
			checkBoxSalaryDeduction.Location = new System.Drawing.Point(265, 54);
			checkBoxSalaryDeduction.Name = "checkBoxSalaryDeduction";
			checkBoxSalaryDeduction.Size = new System.Drawing.Size(156, 17);
			checkBoxSalaryDeduction.TabIndex = 5;
			checkBoxSalaryDeduction.Text = "Include in Salary Deduction";
			checkBoxSalaryDeduction.UseVisualStyleBackColor = true;
			checkBoxInFixed.AutoSize = true;
			checkBoxInFixed.Location = new System.Drawing.Point(15, 55);
			checkBoxInFixed.Name = "checkBoxInFixed";
			checkBoxInFixed.Size = new System.Drawing.Size(62, 17);
			checkBoxInFixed.TabIndex = 3;
			checkBoxInFixed.Text = "Is Fixed";
			checkBoxInFixed.UseVisualStyleBackColor = true;
			checkBoxInOvertime.AutoSize = true;
			checkBoxInOvertime.Location = new System.Drawing.Point(265, 34);
			checkBoxInOvertime.Name = "checkBoxInOvertime";
			checkBoxInOvertime.Size = new System.Drawing.Size(171, 17);
			checkBoxInOvertime.TabIndex = 4;
			checkBoxInOvertime.Text = "Include in Overtime calculation";
			checkBoxInOvertime.UseVisualStyleBackColor = true;
			checkBoxInEOSB.AutoSize = true;
			checkBoxInEOSB.Checked = true;
			checkBoxInEOSB.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInEOSB.Location = new System.Drawing.Point(265, 11);
			checkBoxInEOSB.Name = "checkBoxInEOSB";
			checkBoxInEOSB.Size = new System.Drawing.Size(234, 17);
			checkBoxInEOSB.TabIndex = 4;
			checkBoxInEOSB.Text = "Include in End of Service benefit calculation";
			checkBoxInEOSB.UseVisualStyleBackColor = true;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance13;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(15, 106);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(53, 15);
			linkLabelVoucherNumber.TabIndex = 135;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Account:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance14;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(559, 300);
			base.Controls.Add(linkLabelVoucherNumber);
			base.Controls.Add(panelPaymentChoice);
			base.Controls.Add(comboBoxDeductionCode);
			base.Controls.Add(comboBoxPayCode);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxAccount);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxAccountName);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(labelType);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PayrollItemDetailsForm";
			Text = "Payment Items";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(PayrollItemDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			panelPaymentChoice.ResumeLayout(false);
			panelPaymentChoice.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
