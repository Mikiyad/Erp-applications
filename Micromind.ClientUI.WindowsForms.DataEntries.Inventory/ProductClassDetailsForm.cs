using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductClassDetailsForm : Form, IForm
	{
		private ProductClassData currentData;

		private const string TABLENAME_CONST = "Product_Class";

		private const string IDFIELD_CONST = "ClassID";

		private bool isNewRecord = true;

		private DataSet taxData;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

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

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxIsInactive;

		private UnitComboBox comboBoxUOM;

		private MMLabel mmLabel8;

		private ItemCostingComboBox comboBoxCostMethod;

		private MMLabel mmLabel7;

		private ItemTypesComboBox comboBoxItemType;

		private ProductCategoryComboBox comboBoxCategory;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private UltraGroupBox ultraGroupBox2;

		private MMTextBox textBoxCOGSAccount;

		private MMTextBox textBoxAssetAccount;

		private MMTextBox textBoxIncomeAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxAssetAccount;

		private AllAccountsComboBox comboBoxCOGS;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private AllAccountsComboBox comboBoxIncomeAccount;

		private ToolStripButton toolStripButtonInformation;

		private DataEntryGrid dataEntryGridTax;

		private UltraGroupBox ultraGroupBox7;

		private MMTextBox textBoxGroupName;

		private MMLabel mmLabel9;

		private ComboBox comboBoxTaxOption;

		private TaxGroupComboBox comboBoxTaxGroup;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelCompanyDivision;

		private System.Windows.Forms.ToolTip toolTip1;

		private CompanyDivisionComboBox comboBoxCompanyDivision;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4009;

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

		public ProductClassDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ProductClassDetailsForm_Load;
			comboBoxIncomeAccount.SelectedIndexChanged += comboBoxIncomeAccount_SelectedIndexChanged;
			comboBoxAssetAccount.SelectedIndexChanged += comboBoxAssetAccount_SelectedIndexChanged;
			comboBoxCOGS.SelectedIndexChanged += comboBoxCOGS_SelectedIndexChanged;
			dataEntryGridTax.HeaderClicked += dataGridItems_HeaderClicked;
			dataEntryGridTax.InitializeRow += dataEntryGridTax_AfterCellUpdate;
		}

		private void SetupTaxGrid()
		{
			dataEntryGridTax.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Tax");
			dataTable.Columns.Add("Tax Name");
			dataTable.Columns.Add("Percent", typeof(decimal));
			dataEntryGridTax.DataSource = dataTable;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].MaxLength = 64;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax Name"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax Name"].MaxLength = 64;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax Name"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax Name"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].MinValue = 0;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].MaxLength = 100000;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].Format = "#,##0.####";
			dataEntryGridTax.SetupUI();
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataEntryGridTax.ActiveRow != null)
			{
				FormHelper formHelper = new FormHelper();
				string text = "";
				if (ultraGridColumn != null && ultraGridColumn.Key == "Tax" && !string.IsNullOrEmpty(dataEntryGridTax.ActiveRow.Cells["Tax"].Value.ToString()))
				{
					text = dataEntryGridTax.ActiveRow.Cells["Tax"].Value.ToString();
					formHelper.EditTax(text);
				}
			}
		}

		private void dataEntryGridTax_AfterCellUpdate(object sender, InitializeRowEventArgs e)
		{
			string text = "";
			foreach (UltraGridRow row in dataEntryGridTax.Rows)
			{
				if (row.Cells["Tax"].Text != "")
				{
					text = row.Cells["Tax"].Text;
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("Tax", "TaxName", "TaxID", text);
					row.Cells["Tax Name"].Value = fieldValue.ToString();
				}
			}
		}

		private void comboBoxCOGS_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCOGSAccount.Text = comboBoxCOGS.SelectedName;
		}

		private void comboBoxAssetAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAssetAccount.Text = comboBoxAssetAccount.SelectedName;
		}

		private void comboBoxIncomeAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxIncomeAccount.Text = comboBoxIncomeAccount.SelectedName;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ProductClassData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ProductClassTable.Rows[0] : currentData.ProductClassTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ClassID"] = textBoxCode.Text.Trim();
				dataRow["ClassName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["ItemType"] = comboBoxItemType.SelectedID;
				dataRow["CostMethod"] = comboBoxCostMethod.SelectedID;
				dataRow["UnitID"] = comboBoxUOM.SelectedID;
				dataRow["CategoryID"] = comboBoxCategory.SelectedID;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IncomeAccount"] = comboBoxIncomeAccount.SelectedID;
				dataRow["AssetAccount"] = comboBoxAssetAccount.SelectedID;
				dataRow["COGSAccount"] = comboBoxCOGS.SelectedID;
				dataRow["DivisionID"] = comboBoxCompanyDivision.SelectedID;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ProductClassTable.Rows.Add(dataRow);
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
					currentData = Factory.ProductClassSystem.GetProductClassByID(id.Trim());
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["ClassID"].ToString();
					textBoxName.Text = dataRow["ClassName"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					comboBoxItemType.SelectedID = int.Parse(dataRow["ItemType"].ToString());
					comboBoxCostMethod.SelectedID = int.Parse(dataRow["CostMethod"].ToString());
					comboBoxUOM.SelectedID = dataRow["UnitID"].ToString();
					comboBoxCategory.SelectedID = dataRow["CategoryID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxIncomeAccount.SelectedID = dataRow["IncomeAccount"].ToString();
					comboBoxAssetAccount.SelectedID = dataRow["AssetAccount"].ToString();
					comboBoxCOGS.SelectedID = dataRow["COGSAccount"].ToString();
					comboBoxCompanyDivision.SelectedID = dataRow["DivisionID"].ToString();
					if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
					{
						comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
					}
					else
					{
						comboBoxTaxOption.SelectedIndex = 0;
					}
					if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
					{
						comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
					}
					else
					{
						comboBoxTaxGroup.Clear();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		public void LoadTaxData()
		{
			if (isNewRecord)
			{
				taxData = Factory.TaxSystem.GetTaxList();
			}
			else
			{
				taxData = Factory.TaxSystem.GetTaxClassList(textBoxCode.Text);
			}
			if (taxData != null && taxData.Tables.Count != 0 && taxData.Tables[0].Rows.Count != 0)
			{
				FillTaxData();
			}
		}

		private void FillTaxData()
		{
			if (taxData != null && taxData.Tables.Count != 0 && taxData.Tables[0].Rows.Count != 0)
			{
				_ = dataEntryGridTax.Rows.Count;
				DataTable dataTable = dataEntryGridTax.DataSource as DataTable;
				dataTable?.Rows.Clear();
				foreach (DataRow row in taxData.Tables["Tax"].Rows)
				{
					if (dataTable != null)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["Tax"] = row["Tax Code"];
						if (taxData.Tables["Tax"].Columns.Contains("Tax Percent") && row["Tax Percent"].ToString() != "")
						{
							dataRow2["Percent"] = row["Tax Percent"];
						}
						dataTable.Rows.Add(dataRow2);
						dataTable.AcceptChanges();
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
					flag = Factory.ProductClassSystem.CreateProductClass(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.ProductClass, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ProductClassSystem.UpdateProductClass(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Product_Class", "ClassID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Product_Class", "ClassID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Product_Class", "ClassID");
			textBoxName.Clear();
			textBoxNote.Clear();
			checkBoxIsInactive.Checked = false;
			comboBoxUOM.Clear();
			comboBoxCategory.Clear();
			textBoxNote.Clear();
			comboBoxIncomeAccount.Clear();
			comboBoxAssetAccount.Clear();
			comboBoxCOGS.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxCostMethod.SelectedID = 1;
			comboBoxItemType.SelectedID = 0;
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			comboBoxCompanyDivision.Clear();
			dataEntryGridTax.Clear();
		}

		private void ProductClassGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductClassGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.ProductClassSystem.DeleteProductClass(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.ProductClass, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Product_Class", "ClassID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Product_Class", "ClassID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Product_Class", "ClassID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Product_Class", "ClassID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Product_Class", "ClassID", toolStripTextBoxFind.Text.Trim()))
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

		private void ProductClassDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxCostMethod.LoadData();
				comboBoxItemType.LoadData();
				comboBoxCostMethod.SelectedID = 1;
				SetSecurity();
				SetupTaxGrid();
				dataEntryGridTax.SetupUI();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataEntryGridTax.LoadLayoutFailed = true;
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductCategory(comboBoxCategory.Text);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditUOM(comboBoxUOM.Text);
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxIncomeAccount.Text);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAssetAccount.Text);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGS.Text);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductClass);
		}

		private void linkLabelARAccount_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxIncomeAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGS.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAssetAccount.SelectedID);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 0 || comboBoxTaxOption.SelectedIndex == 2)
			{
				comboBoxTaxGroup.ReadOnly = true;
				comboBoxTaxGroup.Clear();
			}
			else
			{
				comboBoxTaxGroup.ReadOnly = false;
			}
		}

		private void ultraFormattedLinkLabel10_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void ultraGroupBox1_Click(object sender, EventArgs e)
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
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductClassDetailsForm));
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
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelARAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			ultraFormattedLinkLabelCompanyDivision = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			textBoxGroupName = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxCOGSAccount = new Micromind.UISupport.MMTextBox();
			textBoxAssetAccount = new Micromind.UISupport.MMTextBox();
			textBoxIncomeAccount = new Micromind.UISupport.MMTextBox();
			comboBoxAssetAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCOGS = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCompanyDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			comboBoxCategory = new Micromind.DataControls.ProductCategoryComboBox();
			comboBoxUOM = new Micromind.DataControls.UnitComboBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			comboBoxCostMethod = new Micromind.DataControls.ItemCostingComboBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			comboBoxItemType = new Micromind.DataControls.ItemTypesComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dataEntryGridTax = new Micromind.DataControls.DataEntryGrid();
			labelCode = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGS).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTax).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(548, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 393);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(548, 40);
			panelButtons.TabIndex = 6;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabelCompanyDivision);
			ultraGroupBox1.Controls.Add(ultraGroupBox2);
			ultraGroupBox1.Controls.Add(comboBoxCompanyDivision);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel7);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel9);
			ultraGroupBox1.Controls.Add(comboBoxCategory);
			ultraGroupBox1.Controls.Add(comboBoxUOM);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(comboBoxCostMethod);
			ultraGroupBox1.Controls.Add(mmLabel7);
			ultraGroupBox1.Controls.Add(comboBoxItemType);
			ultraGroupBox1.Location = new System.Drawing.Point(6, 106);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(516, 189);
			ultraGroupBox1.TabIndex = 4;
			ultraGroupBox1.Text = "Details";
			ultraGroupBox1.Click += new System.EventHandler(ultraGroupBox1_Click);
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(textBoxCOGSAccount);
			ultraGroupBox2.Controls.Add(textBoxAssetAccount);
			ultraGroupBox2.Controls.Add(textBoxIncomeAccount);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox2.Controls.Add(comboBoxAssetAccount);
			ultraGroupBox2.Controls.Add(comboBoxCOGS);
			ultraGroupBox2.Controls.Add(linkLabelARAccount);
			ultraGroupBox2.Controls.Add(comboBoxIncomeAccount);
			ultraGroupBox2.Location = new System.Drawing.Point(0, 95);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(513, 94);
			ultraGroupBox2.TabIndex = 5;
			ultraGroupBox2.Text = "Accounts";
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(10, 44);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(83, 14);
			ultraFormattedLinkLabel2.TabIndex = 4;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "COGS Account:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked_1);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(10, 64);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(78, 14);
			ultraFormattedLinkLabel1.TabIndex = 2;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Asset Account:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			linkLabelARAccount.AutoSize = true;
			linkLabelARAccount.Location = new System.Drawing.Point(10, 19);
			linkLabelARAccount.Name = "linkLabelARAccount";
			linkLabelARAccount.Size = new System.Drawing.Size(86, 14);
			linkLabelARAccount.TabIndex = 0;
			linkLabelARAccount.TabStop = true;
			linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelARAccount.Value = "Income Account:";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkLabelARAccount.VisitedLinkAppearance = appearance3;
			linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelARAccount_LinkClicked_1);
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(273, 27);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel7.TabIndex = 56;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Category:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(273, 50);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(33, 14);
			ultraFormattedLinkLabel9.TabIndex = 54;
			ultraFormattedLinkLabel9.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabel9, "Unit of measurement");
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "UOM:";
			appearance5.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance5;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			checkBoxIsInactive.AutoSize = true;
			checkBoxIsInactive.Location = new System.Drawing.Point(419, 37);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = true;
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel10);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(textBoxGroupName);
			ultraGroupBox7.Controls.Add(mmLabel9);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Location = new System.Drawing.Point(12, 301);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(494, 83);
			ultraGroupBox7.TabIndex = 5;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel10.AutoSize = true;
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.LinkAppearance = appearance6;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(4, 48);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel10.TabIndex = 73;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Tax Group:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance7;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked_1);
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Customer",
				"Taxable",
				"Non Taxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(98, 22);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(112, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			ultraFormattedLinkLabelCompanyDivision.AutoSize = true;
			ultraFormattedLinkLabelCompanyDivision.Location = new System.Drawing.Point(7, 75);
			ultraFormattedLinkLabelCompanyDivision.Name = "ultraFormattedLinkLabelCompanyDivision";
			ultraFormattedLinkLabelCompanyDivision.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabelCompanyDivision.TabIndex = 133;
			ultraFormattedLinkLabelCompanyDivision.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabelCompanyDivision, "Company Division");
			ultraFormattedLinkLabelCompanyDivision.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelCompanyDivision.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelCompanyDivision.Value = "Comp.Division:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelCompanyDivision.VisitedLinkAppearance = appearance8;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = textBoxGroupName;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance9;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance10.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance10;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance11;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance12.BackColor2 = System.Drawing.SystemColors.Control;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance12;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance13;
			appearance14.BackColor = System.Drawing.SystemColors.Highlight;
			appearance14.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance14;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance15;
			appearance16.BorderColor = System.Drawing.Color.Silver;
			appearance16.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance16;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance17.BackColor = System.Drawing.SystemColors.Control;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance17;
			appearance18.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance18;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance19;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance20;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(98, 46);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(157, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGroupName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGroupName.CustomReportFieldName = "";
			textBoxGroupName.CustomReportKey = "";
			textBoxGroupName.CustomReportValueType = 1;
			textBoxGroupName.IsComboTextBox = false;
			textBoxGroupName.IsModified = false;
			textBoxGroupName.Location = new System.Drawing.Point(256, 46);
			textBoxGroupName.MaxLength = 30;
			textBoxGroupName.Name = "textBoxGroupName";
			textBoxGroupName.ReadOnly = true;
			textBoxGroupName.Size = new System.Drawing.Size(233, 20);
			textBoxGroupName.TabIndex = 2;
			textBoxGroupName.TabStop = false;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(1, 25);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(64, 13);
			mmLabel9.TabIndex = 70;
			mmLabel9.Text = "Tax Option:";
			textBoxCOGSAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCOGSAccount.CustomReportFieldName = "";
			textBoxCOGSAccount.CustomReportKey = "";
			textBoxCOGSAccount.CustomReportValueType = 1;
			textBoxCOGSAccount.IsComboTextBox = false;
			textBoxCOGSAccount.IsModified = false;
			textBoxCOGSAccount.Location = new System.Drawing.Point(245, 42);
			textBoxCOGSAccount.MaxLength = 255;
			textBoxCOGSAccount.Name = "textBoxCOGSAccount";
			textBoxCOGSAccount.ReadOnly = true;
			textBoxCOGSAccount.Size = new System.Drawing.Size(255, 20);
			textBoxCOGSAccount.TabIndex = 3;
			textBoxCOGSAccount.TabStop = false;
			textBoxAssetAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetAccount.CustomReportFieldName = "";
			textBoxAssetAccount.CustomReportKey = "";
			textBoxAssetAccount.CustomReportValueType = 1;
			textBoxAssetAccount.IsComboTextBox = false;
			textBoxAssetAccount.IsModified = false;
			textBoxAssetAccount.Location = new System.Drawing.Point(245, 65);
			textBoxAssetAccount.MaxLength = 255;
			textBoxAssetAccount.Name = "textBoxAssetAccount";
			textBoxAssetAccount.ReadOnly = true;
			textBoxAssetAccount.Size = new System.Drawing.Size(255, 20);
			textBoxAssetAccount.TabIndex = 5;
			textBoxAssetAccount.TabStop = false;
			textBoxIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxIncomeAccount.CustomReportFieldName = "";
			textBoxIncomeAccount.CustomReportKey = "";
			textBoxIncomeAccount.CustomReportValueType = 1;
			textBoxIncomeAccount.IsComboTextBox = false;
			textBoxIncomeAccount.IsModified = false;
			textBoxIncomeAccount.Location = new System.Drawing.Point(245, 20);
			textBoxIncomeAccount.MaxLength = 255;
			textBoxIncomeAccount.Name = "textBoxIncomeAccount";
			textBoxIncomeAccount.ReadOnly = true;
			textBoxIncomeAccount.Size = new System.Drawing.Size(255, 20);
			textBoxIncomeAccount.TabIndex = 1;
			textBoxIncomeAccount.TabStop = false;
			comboBoxAssetAccount.Assigned = false;
			comboBoxAssetAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAssetAccount.CustomReportFieldName = "";
			comboBoxAssetAccount.CustomReportKey = "";
			comboBoxAssetAccount.CustomReportValueType = 1;
			comboBoxAssetAccount.DescriptionTextBox = null;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAssetAccount.DisplayLayout.Appearance = appearance21;
			comboBoxAssetAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAssetAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.Appearance = appearance22;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance24.BackColor2 = System.Drawing.SystemColors.Control;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
			comboBoxAssetAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAssetAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveCellAppearance = appearance25;
			appearance26.BackColor = System.Drawing.SystemColors.Highlight;
			appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveRowAppearance = appearance26;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.CardAreaAppearance = appearance27;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAssetAccount.DisplayLayout.Override.CellAppearance = appearance28;
			comboBoxAssetAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAssetAccount.DisplayLayout.Override.CellPadding = 0;
			appearance29.BackColor = System.Drawing.SystemColors.Control;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.GroupByRowAppearance = appearance29;
			appearance30.TextHAlignAsString = "Left";
			comboBoxAssetAccount.DisplayLayout.Override.HeaderAppearance = appearance30;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			comboBoxAssetAccount.DisplayLayout.Override.RowAppearance = appearance31;
			comboBoxAssetAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAssetAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
			comboBoxAssetAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAssetAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAssetAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAssetAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAssetAccount.Editable = true;
			comboBoxAssetAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAssetAccount.FilterString = "";
			comboBoxAssetAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAssetAccount.FilterSysDocID = "";
			comboBoxAssetAccount.HasAllAccount = false;
			comboBoxAssetAccount.HasCustom = false;
			comboBoxAssetAccount.IsDataLoaded = false;
			comboBoxAssetAccount.Location = new System.Drawing.Point(105, 65);
			comboBoxAssetAccount.MaxDropDownItems = 12;
			comboBoxAssetAccount.MaxLength = 64;
			comboBoxAssetAccount.Name = "comboBoxAssetAccount";
			comboBoxAssetAccount.ShowInactiveItems = false;
			comboBoxAssetAccount.ShowQuickAdd = true;
			comboBoxAssetAccount.Size = new System.Drawing.Size(136, 20);
			comboBoxAssetAccount.TabIndex = 4;
			comboBoxAssetAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCOGS.Assigned = false;
			comboBoxCOGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGS.CustomReportFieldName = "";
			comboBoxCOGS.CustomReportKey = "";
			comboBoxCOGS.CustomReportValueType = 1;
			comboBoxCOGS.DescriptionTextBox = null;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGS.DisplayLayout.Appearance = appearance33;
			comboBoxCOGS.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGS.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.GroupByBox.Appearance = appearance34;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGS.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
			comboBoxCOGS.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance36.BackColor2 = System.Drawing.SystemColors.Control;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGS.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
			comboBoxCOGS.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGS.DisplayLayout.MaxRowScrollRegions = 1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGS.DisplayLayout.Override.ActiveCellAppearance = appearance37;
			appearance38.BackColor = System.Drawing.SystemColors.Highlight;
			appearance38.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGS.DisplayLayout.Override.ActiveRowAppearance = appearance38;
			comboBoxCOGS.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGS.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.Override.CardAreaAppearance = appearance39;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			appearance40.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGS.DisplayLayout.Override.CellAppearance = appearance40;
			comboBoxCOGS.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGS.DisplayLayout.Override.CellPadding = 0;
			appearance41.BackColor = System.Drawing.SystemColors.Control;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.Override.GroupByRowAppearance = appearance41;
			appearance42.TextHAlignAsString = "Left";
			comboBoxCOGS.DisplayLayout.Override.HeaderAppearance = appearance42;
			comboBoxCOGS.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGS.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGS.DisplayLayout.Override.RowAppearance = appearance43;
			comboBoxCOGS.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGS.DisplayLayout.Override.TemplateAddRowAppearance = appearance44;
			comboBoxCOGS.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCOGS.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCOGS.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCOGS.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCOGS.Editable = true;
			comboBoxCOGS.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCOGS.FilterString = "";
			comboBoxCOGS.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCOGS.FilterSysDocID = "";
			comboBoxCOGS.HasAllAccount = false;
			comboBoxCOGS.HasCustom = false;
			comboBoxCOGS.IsDataLoaded = false;
			comboBoxCOGS.Location = new System.Drawing.Point(105, 42);
			comboBoxCOGS.MaxDropDownItems = 12;
			comboBoxCOGS.MaxLength = 64;
			comboBoxCOGS.Name = "comboBoxCOGS";
			comboBoxCOGS.ShowInactiveItems = false;
			comboBoxCOGS.ShowQuickAdd = true;
			comboBoxCOGS.Size = new System.Drawing.Size(136, 20);
			comboBoxCOGS.TabIndex = 2;
			comboBoxCOGS.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxIncomeAccount.Assigned = false;
			comboBoxIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxIncomeAccount.CustomReportFieldName = "";
			comboBoxIncomeAccount.CustomReportKey = "";
			comboBoxIncomeAccount.CustomReportValueType = 1;
			comboBoxIncomeAccount.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxIncomeAccount.DisplayLayout.Appearance = appearance45;
			comboBoxIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxIncomeAccount.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxIncomeAccount.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxIncomeAccount.Editable = true;
			comboBoxIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxIncomeAccount.FilterString = "";
			comboBoxIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxIncomeAccount.FilterSysDocID = "";
			comboBoxIncomeAccount.HasAllAccount = false;
			comboBoxIncomeAccount.HasCustom = false;
			comboBoxIncomeAccount.IsDataLoaded = false;
			comboBoxIncomeAccount.Location = new System.Drawing.Point(105, 20);
			comboBoxIncomeAccount.MaxDropDownItems = 12;
			comboBoxIncomeAccount.MaxLength = 64;
			comboBoxIncomeAccount.Name = "comboBoxIncomeAccount";
			comboBoxIncomeAccount.ShowInactiveItems = false;
			comboBoxIncomeAccount.ShowQuickAdd = true;
			comboBoxIncomeAccount.Size = new System.Drawing.Size(136, 20);
			comboBoxIncomeAccount.TabIndex = 0;
			comboBoxIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCompanyDivision.Assigned = false;
			comboBoxCompanyDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCompanyDivision.CustomReportFieldName = "";
			comboBoxCompanyDivision.CustomReportKey = "";
			comboBoxCompanyDivision.CustomReportValueType = 1;
			comboBoxCompanyDivision.DescriptionTextBox = null;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCompanyDivision.DisplayLayout.Appearance = appearance57;
			comboBoxCompanyDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCompanyDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			comboBoxCompanyDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCompanyDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCompanyDivision.DisplayLayout.Override.CellAppearance = appearance64;
			comboBoxCompanyDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCompanyDivision.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderAppearance = appearance66;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			comboBoxCompanyDivision.DisplayLayout.Override.RowAppearance = appearance67;
			comboBoxCompanyDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCompanyDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			comboBoxCompanyDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCompanyDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCompanyDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCompanyDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCompanyDivision.Editable = true;
			comboBoxCompanyDivision.FilterString = "";
			comboBoxCompanyDivision.HasAllAccount = false;
			comboBoxCompanyDivision.HasCustom = false;
			comboBoxCompanyDivision.IsDataLoaded = false;
			comboBoxCompanyDivision.Location = new System.Drawing.Point(107, 72);
			comboBoxCompanyDivision.MaxDropDownItems = 12;
			comboBoxCompanyDivision.Name = "comboBoxCompanyDivision";
			comboBoxCompanyDivision.ShowInactiveItems = false;
			comboBoxCompanyDivision.ShowQuickAdd = true;
			comboBoxCompanyDivision.Size = new System.Drawing.Size(160, 20);
			comboBoxCompanyDivision.TabIndex = 4;
			comboBoxCompanyDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance69;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(336, 25);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.Size = new System.Drawing.Size(166, 20);
			comboBoxCategory.TabIndex = 2;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUOM.Assigned = false;
			comboBoxUOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUOM.CustomReportFieldName = "";
			comboBoxUOM.CustomReportKey = "";
			comboBoxUOM.CustomReportValueType = 1;
			comboBoxUOM.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUOM.DisplayLayout.Appearance = appearance81;
			comboBoxUOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxUOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxUOM.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUOM.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUOM.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxUOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUOM.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxUOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUOM.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxUOM.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxUOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxUOM.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxUOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxUOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUOM.Editable = true;
			comboBoxUOM.FilterString = "";
			comboBoxUOM.HasAllAccount = false;
			comboBoxUOM.HasCustom = false;
			comboBoxUOM.IsDataLoaded = false;
			comboBoxUOM.Location = new System.Drawing.Point(336, 48);
			comboBoxUOM.MaxDropDownItems = 12;
			comboBoxUOM.Name = "comboBoxUOM";
			comboBoxUOM.ShowInactiveItems = false;
			comboBoxUOM.ShowQuickAdd = true;
			comboBoxUOM.Size = new System.Drawing.Size(166, 20);
			comboBoxUOM.TabIndex = 3;
			comboBoxUOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(7, 51);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(84, 13);
			mmLabel8.TabIndex = 48;
			mmLabel8.Text = "Costing Method:";
			comboBoxCostMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCostMethod.Enabled = false;
			comboBoxCostMethod.FormattingEnabled = true;
			comboBoxCostMethod.Items.AddRange(new object[2]
			{
				"Average",
				"FIFO"
			});
			comboBoxCostMethod.Location = new System.Drawing.Point(107, 48);
			comboBoxCostMethod.Name = "comboBoxCostMethod";
			comboBoxCostMethod.SelectedID = 0;
			comboBoxCostMethod.Size = new System.Drawing.Size(160, 21);
			comboBoxCostMethod.TabIndex = 1;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(7, 28);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(57, 13);
			mmLabel7.TabIndex = 0;
			mmLabel7.Text = "Item Type:";
			comboBoxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxItemType.FormattingEnabled = true;
			comboBoxItemType.Location = new System.Drawing.Point(107, 25);
			comboBoxItemType.Name = "comboBoxItemType";
			comboBoxItemType.SelectedID = 0;
			comboBoxItemType.SelectedType = Micromind.Common.Data.ItemTypes.Inventory;
			comboBoxItemType.Size = new System.Drawing.Size(160, 21);
			comboBoxItemType.TabIndex = 0;
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
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(114, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(397, 20);
			textBoxNote.TabIndex = 3;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(114, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(397, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(114, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(299, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(13, 82);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 7;
			mmLabel4.Text = "Note:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(13, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(77, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Class Name:";
			dataEntryGridTax.AllowAddNew = false;
			dataEntryGridTax.Anchor = System.Windows.Forms.AnchorStyles.None;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridTax.DisplayLayout.Appearance = appearance93;
			dataEntryGridTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			dataEntryGridTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTax.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			dataEntryGridTax.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridTax.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridTax.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			dataEntryGridTax.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridTax.DisplayLayout.Override.CellAppearance = appearance100;
			dataEntryGridTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridTax.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			dataEntryGridTax.DisplayLayout.Override.HeaderAppearance = appearance102;
			dataEntryGridTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridTax.DisplayLayout.Override.RowAppearance = appearance103;
			dataEntryGridTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			dataEntryGridTax.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridTax.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridTax.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridTax.IncludeLotItems = false;
			dataEntryGridTax.LoadLayoutFailed = false;
			dataEntryGridTax.Location = new System.Drawing.Point(486, 135);
			dataEntryGridTax.Name = "dataEntryGridTax";
			dataEntryGridTax.ShowClearMenu = true;
			dataEntryGridTax.ShowDeleteMenu = true;
			dataEntryGridTax.ShowInsertMenu = true;
			dataEntryGridTax.ShowMoveRowsMenu = true;
			dataEntryGridTax.Size = new System.Drawing.Size(10, 50);
			dataEntryGridTax.TabIndex = 24;
			dataEntryGridTax.Visible = false;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(13, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(74, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Class Code:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(548, 1);
			linePanelDown.TabIndex = 7;
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
			xpButton1.Location = new System.Drawing.Point(438, 8);
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(548, 433);
			base.Controls.Add(ultraGroupBox7);
			base.Controls.Add(checkBoxIsInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(dataEntryGridTax);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ProductClassDetailsForm";
			Text = "Item Class";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGS).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTax).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
