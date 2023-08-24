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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class BOMDetailsForm : Form, IForm
	{
		private bool canAccessCost = true;

		private BOMData currentData;

		private const string TABLENAME_CONST = "BOM";

		private const string IDFIELD_CONST = "BOMID";

		private bool isNewRecord = true;

		private bool isLoading;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private OpenFileDialog openFileDialog1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel4;

		private MMLabel mmLabel13;

		private DataEntryGrid dataEntryGridBOM;

		private ProductUnitComboBox comboBoxGridBOMUnit;

		private ProductComboBox comboBoxGridBOMItem;

		private ToolStripButton toolStripButtonInformation;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4010;

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

		public BOMDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			try
			{
				SetupBOMGrid();
				dataEntryGridBOM.SetupUI();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void AddEvents()
		{
			base.Load += BOMDetailsForm_Load;
			dataEntryGridBOM.AfterCellUpdate += dataEntryGridBOM_AfterCellUpdate;
			dataEntryGridBOM.CellDataError += dataEntryGridBOM_CellDataError;
			dataEntryGridBOM.BeforeRowDeactivate += dataEntryGridBOM_BeforeRowDeactivate;
			dataEntryGridBOM.HeaderClicked += dataGridItems_HeaderClicked;
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataEntryGridBOM.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataEntryGridBOM, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void dataEntryGridBOM_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (dataEntryGridBOM.ActiveRow != null && (dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value == null || dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString() == ""))
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				dataEntryGridBOM.EnterEditMode(dataEntryGridBOM.ActiveRow.Cells["Item Code"]);
			}
		}

		private void dataEntryGridBOM_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataEntryGridBOM.ActiveCell.Column.Key == "Item Code")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Item does not exist. Please select a correct item.");
			}
		}

		private void dataEntryGridBOM_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (!(e.Cell.Column.Key == "Item Code"))
			{
				return;
			}
			if (comboBoxGridBOMItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
			{
				comboBoxGridBOMItem.SelectedID = e.Cell.Value.ToString();
			}
			else if (comboBoxGridBOMItem.SelectedRow == null)
			{
				return;
			}
			ItemTypes itemTypes = ItemTypes.None;
			if (comboBoxGridBOMItem.SelectedRow != null && !(comboBoxGridBOMItem.SelectedID == "") && comboBoxGridBOMItem.SelectedRow.Cells["ItemType"].Value != null)
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridBOMItem.SelectedRow.Cells["ItemType"].Value.ToString()));
			}
			switch (itemTypes)
			{
			case ItemTypes.Matrix:
			{
				MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
				matrixSelectionForm.LoadMatrixData(comboBoxGridBOMItem.SelectedID, comboBoxGridBOMItem.SelectedName);
				dataEntryGridBOM.ActiveRow.Delete(displayPrompt: false);
				if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
				{
					foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
					{
						UltraGridRow ultraGridRow = dataEntryGridBOM.DisplayLayout.Bands[0].AddNew();
						ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
						ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
						ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
						ultraGridRow.Update();
					}
				}
				if (dataEntryGridBOM.ActiveRow != null)
				{
					dataEntryGridBOM.EnterEditMode(dataEntryGridBOM.ActiveRow.Cells["Item Code"]);
				}
				break;
			}
			case ItemTypes.Service:
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Activation = Activation.AllowEdit;
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Appearance.BackColor = Color.White;
				break;
			default:
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Activation = Activation.Disabled;
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Appearance.BackColor = Color.WhiteSmoke;
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Appearance.BackColorDisabled = Color.WhiteSmoke;
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Appearance.ForeColorDisabled = Color.Black;
				break;
			}
			if (dataEntryGridBOM.ActiveRow != null)
			{
				dataEntryGridBOM.ActiveRow.Cells["Description"].Value = comboBoxGridBOMItem.SelectedName;
				dataEntryGridBOM.ActiveRow.Cells["UnitID"].Value = comboBoxGridBOMItem.SelectedUnitID;
				dataEntryGridBOM.ActiveRow.Cells["Cost"].Value = comboBoxGridBOMItem.SelectedAverageCost;
				dataEntryGridBOM.ActiveRow.Cells["ItemType"].Value = (int)comboBoxGridBOMItem.SelectedItemType;
				if (dataEntryGridBOM.ActiveRow.Cells["Quantity"].Value == null || dataEntryGridBOM.ActiveRow.Cells["Quantity"].Value.ToString() == "")
				{
					dataEntryGridBOM.ActiveRow.Cells["Quantity"].Value = 1;
				}
			}
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BOMData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BOMTable.Rows[0] : currentData.BOMTable.NewRow();
				dataRow.BeginEdit();
				if (isNewRecord)
				{
					dataRow["BOMID"] = textBoxCode.Text.Trim();
				}
				else
				{
					dataRow["BOMID"] = textBoxCode.Text;
				}
				dataRow["BOMName"] = textBoxName.Text;
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.BOMTable.Rows.Add(dataRow);
				}
				currentData.BOMDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataEntryGridBOM.Rows)
				{
					DataRow dataRow2 = currentData.BOMDetailTable.NewRow();
					dataRow2["BOMID"] = textBoxCode.Text;
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					dataRow2["UnitID"] = row.Cells["UnitID"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					currentData.BOMDetailTable.Rows.Add(dataRow2);
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
				textBoxCode.Focus();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.BOMSystem.GetBOMByID(id);
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
				isLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["BOMID"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					textBoxName.Text = dataRow["BOMName"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					DataTable dataTable = dataEntryGridBOM.DataSource as DataTable;
					dataTable?.Rows.Clear();
					dataTable.BeginLoadData();
					foreach (DataRow row in currentData.BOMDetailTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Item Code"] = row["ProductID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["UnitID"] = row["UnitID"];
						dataRow3["ItemType"] = row["ItemType"];
						dataRow3["Cost"] = row["Cost"];
						dataRow3["Quantity"] = row["Quantity"];
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.EndLoadData();
					foreach (UltraGridRow row2 in dataEntryGridBOM.Rows)
					{
						if (row2.Cells["ItemType"].Value.ToString() == 1.ToString())
						{
							row2.Cells["Cost"].Activation = Activation.Disabled;
							row2.Cells["Cost"].Appearance.ForeColorDisabled = Color.Black;
							row2.Cells["Cost"].Appearance.BackColorDisabled = Color.WhiteSmoke;
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isLoading = false;
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
				bool flag = (!isNewRecord) ? Factory.BOMSystem.UpdateBOM(currentData) : Factory.BOMSystem.CreateBOM(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.BOM, needRefresh: true);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("BOM", "BOMID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			foreach (UltraGridRow row in dataEntryGridBOM.Rows)
			{
				if (row.Cells["Item Code"].Value == null || row.Cells["Item Code"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item for all the rows.");
					dataEntryGridBOM.Focus();
					row.Activate();
					return false;
				}
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("BOM", "BOMID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("BOM", "BOMID");
			textBoxName.Clear();
			textBoxNote.Clear();
			checkBoxInactive.Checked = false;
			dataEntryGridBOM.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void ProductGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.BOMSystem.DeleteBOM(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.BOM, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("BOM", "BOMID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("BOM", "BOMID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("BOM", "BOMID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("BOM", "BOMID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("BOM", "BOMID", toolStripTextBoxFind.Text.Trim()))
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

		private void BOMDetailsForm_Load(object sender, EventArgs e)
		{
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void SetupBOMGrid()
		{
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Item Code");
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("ItemType", typeof(int));
			dataTable.Columns.Add("Cost", typeof(decimal));
			dataTable.Columns.Add("UnitID", typeof(string));
			dataTable.Columns.Add("Quantity", typeof(decimal));
			dataEntryGridBOM.DataSource = dataTable;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridBOMItem;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["UnitID"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["UnitID"].MaxLength = 15;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["UnitID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["UnitID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["UnitID"].ValueList = comboBoxGridBOMUnit;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Cost"].CellActivation = Activation.AllowEdit;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Cost"].Format = Format.GridAmountFormat;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["ItemType"].CellActivation = Activation.Disabled;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["ItemType"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["ItemType"].CellAppearance.ForeColorDisabled = Color.Black;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 100000;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,##0.####";
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Description"].Width = Convert.ToInt32((double)dataEntryGridBOM.Width / 2.5);
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(0, "None");
			valueList.ValueListItems.Add(1, "Inventory");
			valueList.ValueListItems.Add(2, "Non-Inventory");
			valueList.ValueListItems.Add(3, "Service");
			valueList.ValueListItems.Add(4, "Discount");
			valueList.ValueListItems.Add(5, "Consignment");
			valueList.ValueListItems.Add(6, "Matrix");
			valueList.ValueListItems.Add(7, "Assembly");
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridBOM.DisplayLayout.Bands[0].Columns["ItemType"].ValueList = valueList;
			if (!dataEntryGridBOM.DisplayLayout.Bands[0].Summaries.Exists("QtyTotal"))
			{
				dataEntryGridBOM.DisplayLayout.Bands[0].Summaries.Add("QtyTotal", SummaryType.Sum, dataEntryGridBOM.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void buttonRemoveImage_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataEntryGridBOM.ActiveRow != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataEntryGridBOM.ActiveRow != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataEntryGridBOM.ActiveRow != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value != null && dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataEntryGridBOM.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataEntryGridBOM.Left + 20, dataEntryGridBOM.Top + 20);
				}
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataEntryGridBOM.ActiveRow != null)
			{
				dataEntryGridBOM.ActiveRow.Delete(displayPrompt: false);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.BOMDetailsForm));
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			labelCode = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxGridBOMUnit = new Micromind.DataControls.ProductUnitComboBox();
			comboBoxGridBOMItem = new Micromind.DataControls.ProductComboBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			dataEntryGridBOM = new Micromind.DataControls.DataEntryGrid();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBOMUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBOMItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridBOM).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(319, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactvie";
			checkBoxInactive.UseVisualStyleBackColor = true;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 39);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(71, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "BOM Code:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(74, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "BOM Name:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(89, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(224, 20);
			textBoxCode.TabIndex = 0;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(89, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(322, 20);
			textBoxName.TabIndex = 1;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(89, 82);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(521, 20);
			textBoxNote.TabIndex = 54;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(11, 85);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 53;
			mmLabel4.Text = "Note:";
			comboBoxGridBOMUnit.AlwaysInEditMode = true;
			comboBoxGridBOMUnit.Assigned = false;
			comboBoxGridBOMUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridBOMUnit.CustomReportFieldName = "";
			comboBoxGridBOMUnit.CustomReportKey = "";
			comboBoxGridBOMUnit.CustomReportValueType = 1;
			comboBoxGridBOMUnit.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBOMUnit.DisplayLayout.Appearance = appearance;
			comboBoxGridBOMUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBOMUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMUnit.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBOMUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridBOMUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBOMUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridBOMUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBOMUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBOMUnit.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBOMUnit.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridBOMUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBOMUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMUnit.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBOMUnit.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridBOMUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBOMUnit.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMUnit.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridBOMUnit.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridBOMUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBOMUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBOMUnit.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridBOMUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBOMUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGridBOMUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridBOMUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridBOMUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridBOMUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridBOMUnit.Editable = true;
			comboBoxGridBOMUnit.FilterString = "";
			comboBoxGridBOMUnit.IsDataLoaded = false;
			comboBoxGridBOMUnit.Location = new System.Drawing.Point(86, 332);
			comboBoxGridBOMUnit.MaxDropDownItems = 12;
			comboBoxGridBOMUnit.Name = "comboBoxGridBOMUnit";
			comboBoxGridBOMUnit.Size = new System.Drawing.Size(101, 20);
			comboBoxGridBOMUnit.TabIndex = 122;
			comboBoxGridBOMUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBOMUnit.Visible = false;
			comboBoxGridBOMItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridBOMItem.AlwaysInEditMode = true;
			comboBoxGridBOMItem.Assigned = false;
			comboBoxGridBOMItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridBOMItem.CustomReportFieldName = "";
			comboBoxGridBOMItem.CustomReportKey = "";
			comboBoxGridBOMItem.CustomReportValueType = 1;
			comboBoxGridBOMItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBOMItem.DisplayLayout.Appearance = appearance13;
			comboBoxGridBOMItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBOMItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBOMItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridBOMItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBOMItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridBOMItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBOMItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBOMItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBOMItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridBOMItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBOMItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBOMItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridBOMItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBOMItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBOMItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridBOMItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridBOMItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBOMItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBOMItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridBOMItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBOMItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridBOMItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridBOMItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridBOMItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridBOMItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridBOMItem.Editable = true;
			comboBoxGridBOMItem.FilterString = "";
			comboBoxGridBOMItem.FilterSysDocID = "";
			comboBoxGridBOMItem.HasAllAccount = false;
			comboBoxGridBOMItem.HasCustom = false;
			comboBoxGridBOMItem.IsDataLoaded = false;
			comboBoxGridBOMItem.Location = new System.Drawing.Point(286, 359);
			comboBoxGridBOMItem.MaxDropDownItems = 12;
			comboBoxGridBOMItem.Name = "comboBoxGridBOMItem";
			comboBoxGridBOMItem.Show3PLItems = true;
			comboBoxGridBOMItem.ShowInactiveItems = false;
			comboBoxGridBOMItem.ShowQuickAdd = true;
			comboBoxGridBOMItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridBOMItem.TabIndex = 121;
			comboBoxGridBOMItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBOMItem.Visible = false;
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(12, 120);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(128, 13);
			mmLabel13.TabIndex = 58;
			mmLabel13.Text = "Enter bill of material items:";
			dataEntryGridBOM.AllowAddNew = false;
			dataEntryGridBOM.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridBOM.DisplayLayout.Appearance = appearance25;
			dataEntryGridBOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridBOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridBOM.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridBOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataEntryGridBOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridBOM.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataEntryGridBOM.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridBOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridBOM.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridBOM.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataEntryGridBOM.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridBOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridBOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridBOM.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridBOM.DisplayLayout.Override.CellAppearance = appearance32;
			dataEntryGridBOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridBOM.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridBOM.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataEntryGridBOM.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataEntryGridBOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridBOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridBOM.DisplayLayout.Override.RowAppearance = appearance35;
			dataEntryGridBOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridBOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataEntryGridBOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridBOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridBOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridBOM.Location = new System.Drawing.Point(12, 136);
			dataEntryGridBOM.Name = "dataEntryGridBOM";
			dataEntryGridBOM.ShowDeleteMenu = true;
			dataEntryGridBOM.ShowInsertMenu = true;
			dataEntryGridBOM.ShowMoveRowsMenu = true;
			dataEntryGridBOM.Size = new System.Drawing.Size(673, 292);
			dataEntryGridBOM.TabIndex = 57;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(697, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(23, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(50, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(23, 22);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 444);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(697, 40);
			panelButtons.TabIndex = 0;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(697, 1);
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
			xpButton1.Location = new System.Drawing.Point(587, 8);
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
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator4,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 142);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(38, 178);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 124;
			productPhotoViewer.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(697, 484);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(dataEntryGridBOM);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(comboBoxGridBOMUnit);
			base.Controls.Add(comboBoxGridBOMItem);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "BOMDetailsForm";
			Text = "BOM Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)comboBoxGridBOMUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBOMItem).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridBOM).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
