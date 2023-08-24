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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class PaymentTermDetailsForm : Form, IForm
	{
		private PaymentTermData currentData;

		private const string TABLENAME_CONST = "Payment_Term";

		private const string IDFIELD_CONST = "PaymentTermID";

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

		private CheckBox checkBoxInactive;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private NumberTextBox textBoxTermNetDays;

		private MMLabel mmLabel2;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel4;

		private PaymentTermTypesComboBox comboBoxTermType;

		private AmountOrPercentComboBox comboBoxDiscountType;

		private NumberTextBox textBoxDiscountDays;

		private MMLabel mmLabel5;

		private AmountTextBox textBoxDiscountAmount;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel mmLabel6;

		private MMLabel mmLabel7;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private RadioButton radioButtonSingleInstallment;

		private RadioButton radioButtonMultipleInstallment;

		private DataEntryGrid dataGridItems;

		private Panel panel1;

		private PaymentTermTypesComboBox comboBoxGridTerm;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6010;

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

		public PaymentTermDetailsForm()
		{
			InitializeComponent();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PaymentTermData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TermTable.Rows[0] : currentData.TermTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PaymentTermID"] = textBoxCode.Text.Trim();
				dataRow["TermName"] = textBoxName.Text.Trim();
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["TermType"] = comboBoxTermType.SelectedID;
				dataRow["IsInstallments"] = radioButtonMultipleInstallment.Checked;
				dataRow["DiscountType"] = comboBoxDiscountType.SelectedID;
				dataRow["DiscountAmount"] = textBoxDiscountAmount.Text;
				dataRow["NetDays"] = textBoxTermNetDays.Text;
				dataRow["DiscountDays"] = textBoxDiscountDays.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.TermTable.Rows.Add(dataRow);
				}
				currentData.InstallmentsTable.Rows.Clear();
				if (radioButtonMultipleInstallment.Checked)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.InstallmentsTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["PaymentTermID"] = textBoxCode.Text.Trim();
						dataRow2["RowIndex"] = row.Index;
						dataRow2["Percentage"] = row.Cells["Percentage"].Value.ToString();
						dataRow2["Days"] = row.Cells["Days"].Value.ToString();
						dataRow2["TermType"] = row.Cells["TermType"].Value.ToString();
						currentData.InstallmentsTable.Rows.Add(dataRow2);
					}
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
					currentData = Factory.TermSystem.GetTermByID(id.Trim());
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
				textBoxCode.Text = dataRow["PaymentTermID"].ToString();
				textBoxName.Text = dataRow["TermName"].ToString();
				comboBoxTermType.SelectedID = dataRow["TermType"].ToString();
				comboBoxDiscountType.SelectedID = dataRow["DiscountType"].ToString();
				textBoxDiscountAmount.Text = dataRow["DiscountAmount"].ToString();
				textBoxTermNetDays.Text = dataRow["NetDays"].ToString();
				textBoxDiscountDays.Text = dataRow["DiscountDays"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				bool flag = false;
				if (!dataRow["IsInstallments"].IsDBNullOrEmpty())
				{
					flag = bool.Parse(dataRow["IsInstallments"].ToString());
				}
				if (flag)
				{
					radioButtonMultipleInstallment.Checked = true;
				}
				else
				{
					radioButtonSingleInstallment.Checked = true;
				}
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("Payment_Term_Installment") && currentData.InstallmentsTable.Rows.Count != 0)
				{
					foreach (DataRow row in currentData.Tables["Payment_Term_Installment"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Percentage"] = row["Percentage"];
						dataRow3["Days"] = row["Days"];
						dataRow3["TermType"] = row["TermType"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
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
					flag = Factory.TermSystem.CreateTerm(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.PaymentTerm, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.TermSystem.UpdateTerm(currentData);
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
			if (comboBoxTermType.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				comboBoxTermType.Focus();
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Payment_Term", "PaymentTermID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			if (radioButtonMultipleInstallment.Checked)
			{
				int num = 0;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.Cells["Percentage"].Value.IsNullOrEmpty())
					{
						num = checked(num + int.Parse(row.Cells["Percentage"].Value.ToString()));
					}
				}
				if (num != 100)
				{
					ErrorHelper.WarningMessage("Sum of installments percentage must be 100.");
					return false;
				}
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Payment_Term", "PaymentTermID");
			(dataGridItems.DataSource as DataTable).Rows.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			comboBoxTermType.SelectedID = "1";
			comboBoxDiscountType.SelectedID = "1";
			checkBoxInactive.Checked = false;
			textBoxDiscountAmount.Text = "0";
			textBoxDiscountDays.Text = "0";
			textBoxTermNetDays.Text = "0";
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void PaymentTermGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void PaymentTermGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.TermSystem.DeleteTerm(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.PaymentTerm, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Payment_Term", "PaymentTermID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Payment_Term", "PaymentTermID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Payment_Term", "PaymentTermID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Payment_Term", "PaymentTermID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Payment_Term", "PaymentTermID", toolStripTextBoxFind.Text.Trim()))
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

		private void PaymentTermDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDiscountType.LoadData();
				comboBoxTermType.LoadData();
				comboBoxTermType.SelectedID = "1";
				comboBoxDiscountType.SelectedID = "1";
				SetSecurity();
				dataGridItems.SetupUI();
				SetupGrid();
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

		private void comboBoxTermType_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxDiscountType_ValueChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.PaymentTerm);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Acccounts;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void radioButtonMultipleInstallment_CheckedChanged(object sender, EventArgs e)
		{
			dataGridItems.Enabled = radioButtonMultipleInstallment.Checked;
			textBoxTermNetDays.Enabled = radioButtonSingleInstallment.Checked;
			comboBoxTermType.Enabled = radioButtonSingleInstallment.Checked;
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Percentage");
				dataTable.Columns.Add("Days");
				dataTable.Columns.Add("TermType");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percentage"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percentage"].MaxValue = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percentage"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percentage"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Days"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.IntegerNonNegative;
				dataGridItems.DisplayLayout.Bands[0].Columns["Days"].MaxValue = 400;
				dataGridItems.DisplayLayout.Bands[0].Columns["Days"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Days"].CellAppearance.TextHAlign = HAlign.Right;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "From Invoice Date");
				valueList.ValueListItems.Add(2, "From End of Month");
				valueList.ValueListItems.Add(3, "After PO Date");
				valueList.ValueListItems.Add(4, "After ATD");
				valueList.ValueListItems.Add(5, "After Packing List");
				valueList.ValueListItems.Add(6, "Before ETA");
				valueList.ValueListItems.Add(7, "After ETA");
				valueList.ValueListItems.Add(8, "After BL");
				valueList.ValueListItems.Add(9, "After GRN");
				dataGridItems.DisplayLayout.Bands[0].Columns["Termtype"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["TermType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["TermType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["TermType"].Header.Caption = "Due Calculation";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Percent", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Percentage"], SummaryPosition.UseSummaryPositionColumn);
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.PaymentTermDetailsForm));
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
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
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
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxTermNetDays = new Micromind.UISupport.NumberTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxTermType = new Micromind.DataControls.PaymentTermTypesComboBox();
			comboBoxDiscountType = new Micromind.DataControls.AmountOrPercentComboBox();
			textBoxDiscountDays = new Micromind.UISupport.NumberTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			radioButtonSingleInstallment = new System.Windows.Forms.RadioButton();
			radioButtonMultipleInstallment = new System.Windows.Forms.RadioButton();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panel1 = new System.Windows.Forms.Panel();
			comboBoxGridTerm = new Micromind.DataControls.PaymentTermTypesComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTermType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountType).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTerm).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
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
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(665, 31);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 411);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(665, 40);
			panelButtons.TabIndex = 15;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(665, 1);
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
			xpButton1.Location = new System.Drawing.Point(555, 8);
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
			labelCode.Location = new System.Drawing.Point(9, 37);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(72, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Term Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(108, 35);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 1;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Term Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(108, 57);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(384, 20);
			textBoxName.TabIndex = 4;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(295, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
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
			textBoxTermNetDays.AllowDecimal = false;
			textBoxTermNetDays.CustomReportFieldName = "";
			textBoxTermNetDays.CustomReportKey = "";
			textBoxTermNetDays.CustomReportValueType = 1;
			textBoxTermNetDays.IsComboTextBox = false;
			textBoxTermNetDays.IsModified = false;
			textBoxTermNetDays.Location = new System.Drawing.Point(108, 109);
			textBoxTermNetDays.MaxLength = 3;
			textBoxTermNetDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTermNetDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTermNetDays.Name = "textBoxTermNetDays";
			textBoxTermNetDays.NullText = "0";
			textBoxTermNetDays.Size = new System.Drawing.Size(78, 20);
			textBoxTermNetDays.TabIndex = 7;
			textBoxTermNetDays.Text = "0";
			textBoxTermNetDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(6, 13);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(79, 13);
			mmLabel2.TabIndex = 8;
			mmLabel2.Text = "Discount Days:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(105, 58);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(384, 20);
			textBoxNote.TabIndex = 14;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(9, 60);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 13;
			mmLabel4.Text = "Note:";
			comboBoxTermType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxTermType.Location = new System.Drawing.Point(239, 109);
			comboBoxTermType.Name = "comboBoxTermType";
			comboBoxTermType.SelectedID = "";
			comboBoxTermType.Size = new System.Drawing.Size(253, 21);
			comboBoxTermType.TabIndex = 6;
			comboBoxTermType.ValueChanged += new System.EventHandler(comboBoxTermType_ValueChanged);
			comboBoxDiscountType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxDiscountType.Location = new System.Drawing.Point(186, 33);
			comboBoxDiscountType.Name = "comboBoxDiscountType";
			comboBoxDiscountType.SelectedID = "";
			comboBoxDiscountType.Size = new System.Drawing.Size(123, 21);
			comboBoxDiscountType.TabIndex = 11;
			comboBoxDiscountType.ValueChanged += new System.EventHandler(comboBoxDiscountType_ValueChanged);
			textBoxDiscountDays.AllowDecimal = false;
			textBoxDiscountDays.CustomReportFieldName = "";
			textBoxDiscountDays.CustomReportKey = "";
			textBoxDiscountDays.CustomReportValueType = 1;
			textBoxDiscountDays.IsComboTextBox = false;
			textBoxDiscountDays.IsModified = false;
			textBoxDiscountDays.Location = new System.Drawing.Point(105, 11);
			textBoxDiscountDays.MaxLength = 3;
			textBoxDiscountDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDiscountDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDiscountDays.Name = "textBoxDiscountDays";
			textBoxDiscountDays.NullText = "0";
			textBoxDiscountDays.Size = new System.Drawing.Size(78, 20);
			textBoxDiscountDays.TabIndex = 9;
			textBoxDiscountDays.Text = "0";
			textBoxDiscountDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(6, 37);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(91, 13);
			mmLabel5.TabIndex = 10;
			mmLabel5.Text = "Discount Amount:";
			textBoxDiscountAmount.AllowDecimal = true;
			textBoxDiscountAmount.CustomReportFieldName = "";
			textBoxDiscountAmount.CustomReportKey = "";
			textBoxDiscountAmount.CustomReportValueType = 1;
			textBoxDiscountAmount.IsComboTextBox = false;
			textBoxDiscountAmount.IsModified = false;
			textBoxDiscountAmount.Location = new System.Drawing.Point(105, 34);
			textBoxDiscountAmount.MaxLength = 15;
			textBoxDiscountAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDiscountAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDiscountAmount.Name = "textBoxDiscountAmount";
			textBoxDiscountAmount.NullText = "0";
			textBoxDiscountAmount.Size = new System.Drawing.Size(78, 20);
			textBoxDiscountAmount.TabIndex = 12;
			textBoxDiscountAmount.Text = "0.00";
			textBoxDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscountAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(192, 112);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(29, 13);
			mmLabel6.TabIndex = 8;
			mmLabel6.Text = "days";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(75, 112);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(27, 13);
			mmLabel7.TabIndex = 8;
			mmLabel7.Text = "Due";
			radioButtonSingleInstallment.AutoSize = true;
			radioButtonSingleInstallment.Checked = true;
			radioButtonSingleInstallment.Location = new System.Drawing.Point(12, 83);
			radioButtonSingleInstallment.Name = "radioButtonSingleInstallment";
			radioButtonSingleInstallment.Size = new System.Drawing.Size(110, 17);
			radioButtonSingleInstallment.TabIndex = 17;
			radioButtonSingleInstallment.TabStop = true;
			radioButtonSingleInstallment.Text = "Single Installment:";
			radioButtonSingleInstallment.UseVisualStyleBackColor = true;
			radioButtonMultipleInstallment.AutoSize = true;
			radioButtonMultipleInstallment.Location = new System.Drawing.Point(12, 149);
			radioButtonMultipleInstallment.Name = "radioButtonMultipleInstallment";
			radioButtonMultipleInstallment.Size = new System.Drawing.Size(122, 17);
			radioButtonMultipleInstallment.TabIndex = 18;
			radioButtonMultipleInstallment.Text = "Multiple Installments:";
			radioButtonMultipleInstallment.UseVisualStyleBackColor = true;
			radioButtonMultipleInstallment.CheckedChanged += new System.EventHandler(radioButtonMultipleInstallment_CheckedChanged);
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.Enabled = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(13, 172);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(638, 130);
			dataGridItems.TabIndex = 19;
			dataGridItems.Text = "dataEntryGrid1";
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel1.Controls.Add(textBoxDiscountAmount);
			panel1.Controls.Add(mmLabel5);
			panel1.Controls.Add(textBoxDiscountDays);
			panel1.Controls.Add(comboBoxDiscountType);
			panel1.Controls.Add(mmLabel2);
			panel1.Controls.Add(textBoxNote);
			panel1.Controls.Add(mmLabel4);
			panel1.Location = new System.Drawing.Point(3, 308);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(613, 97);
			panel1.TabIndex = 20;
			comboBoxGridTerm.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxGridTerm.Location = new System.Drawing.Point(206, 215);
			comboBoxGridTerm.Name = "comboBoxGridTerm";
			comboBoxGridTerm.SelectedID = "";
			comboBoxGridTerm.Size = new System.Drawing.Size(253, 21);
			comboBoxGridTerm.TabIndex = 21;
			comboBoxGridTerm.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(665, 451);
			base.Controls.Add(panel1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(radioButtonMultipleInstallment);
			base.Controls.Add(radioButtonSingleInstallment);
			base.Controls.Add(textBoxTermNetDays);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(comboBoxTermType);
			base.Controls.Add(formManager);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxGridTerm);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(673, 478);
			base.Name = "PaymentTermDetailsForm";
			Text = "Payment Term";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(PaymentTermDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxTermType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountType).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTerm).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
