using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class MatrixProductDetailsForm : Form, IForm
	{
		private DataSet productData = new DataSet();

		private ProductParentData currentData;

		private const string TABLENAME_CONST = "Product_Parent";

		private const string IDFIELD_CONST = "ProductParentID";

		private bool isNewRecord = true;

		private int componentDescGeneration = 2;

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

		private MMLabel mmLabel6;

		private ItemCostingComboBox comboBoxCostMethod;

		private MMLabel mmLabel8;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UnitPriceTextBox textBoxStandardPrice;

		private MMLabel labelStandardPrice;

		private ProductCategoryComboBox comboBoxCategory;

		private QuantityTextBox textBoxQuantityPerUnit;

		private UnitComboBox comboBoxUOM;

		private Panel panel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private OpenFileDialog openFileDialog1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private MMLabel labelCustomerNameHeader;

		private CountryComboBox comboBoxOrigin;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private ProductBrandComboBox comboBoxBrand;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ProductManufacturerComboBox comboBoxManufacturer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private vendorsFlatComboBox comboBoxPrefVendor;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel4;

		private XPButton buttonComponents;

		private DataEntryGrid dataGridAttributes;

		private Label label3;

		private Label label2;

		private Label label1;

		private DimensionComboBox comboBoxDimension3;

		private DimensionComboBox comboBoxDimension2;

		private DimensionComboBox comboBoxDimension1;

		private DataEntryGrid dataGridDimension3;

		private DataEntryGrid dataGridDimension2;

		private DataEntryGrid dataGridDimension1;

		private Panel panelDimension1;

		private Panel panelDimension2;

		private Panel panelDimension3;

		private ContextMenuStrip contextMenuStripComponents;

		private Label label4;

		private MatrixTemplateComboBox matrixTemplateComboBox;

		private ContextMenuStrip contextMenuStripDimension;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem deleteToolStripMenuItem1;

		private ToolStripButton toolStripButtonQuantity;

		private MMLabel labelMinPrice;

		private MMLabel labelSpecialPrice;

		private MMLabel labelWholesalePrice;

		private UnitPriceTextBox textBoxMinimumPrice;

		private UnitPriceTextBox textBoxSpecialPrice;

		private UnitPriceTextBox textBoxWholesalePrice;

		private MMTextBox textBoxPrefVendor;

		private MMTextBox textBoxOrigin;

		private MMTextBox textBoxBrand;

		private MMTextBox textBoxManufact;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private PictureBox pictureBoxNoImage;

		private ToolStripButton toolStripButtonShowPicture;

		private CheckBox checkBoxExcludeFromCatalogue;

		private ToolStripMenuItem toolStripMenuItemRemoveComponent;

		private XPButton buttonAddItem;

		private ToolStripButton toolStripButtonInformation;

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
				toolStripButtonQuantity.Enabled = !isNewRecord;
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

		public MatrixProductDetailsForm()
		{
			InitializeComponent();
			base.FormClosing += MatrixProductDetailsForm_FormClosing;
			labelStandardPrice.Text = CompanyPreferences.UnitPrice1Title + ":";
			labelWholesalePrice.Text = CompanyPreferences.UnitPrice2Title + ":";
			labelSpecialPrice.Text = CompanyPreferences.UnitPrice3Title + ":";
			AddEvents();
			dataGridAttributes.RowUpdateCancelAction = RowUpdateCancelAction.RetainDataAndActivation;
			dataGridDimension1.AfterRowUpdate += dataGridDimension1_AfterRowUpdate;
			dataGridDimension2.AfterRowUpdate += dataGridDimension2_AfterRowUpdate;
			dataGridDimension3.AfterRowUpdate += dataGridDimension3_AfterRowUpdate;
			dataGridDimension1.BeforeRowUpdate += dataGridDimension1_BeforeRowUpdate;
			dataGridDimension2.BeforeRowUpdate += dataGridDimension2_BeforeRowUpdate;
			dataGridDimension3.BeforeRowUpdate += dataGridDimension3_BeforeRowUpdate;
			dataGridAttributes.ShowDeleteMenu = false;
			dataGridAttributes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
			dataGridAttributes.KeyDown += dataGridAttributes_KeyDown;
			dataGridAttributes.BeforeRowInsert += dataGridAttributes_BeforeRowInsert;
			dataGridAttributes.BeforeRowUpdate += dataGridAttributes_BeforeRowUpdate;
			dataGridAttributes.BeforeCellUpdate += dataGridAttributes_BeforeCellUpdate;
			dataGridAttributes.BeforeExitEditMode += dataGridAttributes_BeforeExitEditMode;
			dataGridAttributes.BeforeRowDeactivate += dataGridAttributes_BeforeRowDeactivate;
			dataGridAttributes.AfterEnterEditMode += dataGridAttributes_AfterEnterEditMode;
			dataGridAttributes.GotFocus += dataGridAttributes_GotFocus;
			dataGridAttributes.Validated += dataGridAttributes_LostFocus;
			dataGridAttributes.AfterCellUpdate += dataGridAttributes_AfterCellUpdate;
			componentDescGeneration = CompanyPreferences.MatrixDescriptionGenerationMethod;
		}

		private void dataGridDimension3_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[0].Value != null && e.Row.Cells[1].Value != null)
			{
				foreach (UltraGridRow row in dataGridDimension3.Rows)
				{
					if (!row.IsActiveRow && row.Cells[0].Value.ToString().Trim() == e.Row.Cells[0].Value.ToString() && row.Cells[1].Value.ToString().Trim() == e.Row.Cells[1].Value.ToString())
					{
						ErrorHelper.InformationMessage("Item is already exist in the list!");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void dataGridDimension2_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[0].Value != null && e.Row.Cells[1].Value != null)
			{
				foreach (UltraGridRow row in dataGridDimension2.Rows)
				{
					if (!row.IsActiveRow && row.Cells[0].Value.ToString().Trim() == e.Row.Cells[0].Value.ToString() && row.Cells[1].Value.ToString().Trim() == e.Row.Cells[1].Value.ToString())
					{
						ErrorHelper.InformationMessage("Item is already exist in the list!");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void dataGridDimension1_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[0].Value != null && e.Row.Cells[1].Value != null)
			{
				foreach (UltraGridRow row in dataGridDimension1.Rows)
				{
					if (!row.IsActiveRow && row.Cells[0].Value.ToString().Trim() == e.Row.Cells[0].Value.ToString() && row.Cells[1].Value.ToString().Trim() == e.Row.Cells[1].Value.ToString())
					{
						ErrorHelper.InformationMessage("Item is already exist in the list!");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void MatrixProductDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridAttributes.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridAttributes_CellChange(object sender, CellEventArgs e)
		{
		}

		private void dataGridAttributes_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Value.ToString() != e.Cell.Value.ToString().Trim())
			{
				e.Cell.Value = e.Cell.Value.ToString().Trim();
			}
		}

		private void dataGridDimension1_AfterRowUpdate(object sender, RowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			SetValueList(1);
		}

		private void dataGridDimension2_AfterRowUpdate(object sender, RowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			SetValueList(2);
		}

		private void dataGridDimension3_AfterRowUpdate(object sender, RowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells[0].Value != null)
			{
				e.Row.Cells[0].Value = e.Row.Cells[0].Value.ToString().Trim();
			}
			if (e.Row != null && e.Row.Cells[1].Value != null)
			{
				e.Row.Cells[1].Value = e.Row.Cells[1].Value.ToString().Trim();
			}
			SetValueList(3);
		}

		private void SetValueList(int dimID)
		{
			switch (dimID)
			{
			case 1:
			{
				ValueList valueList2 = new ValueList();
				foreach (UltraGridRow row in dataGridDimension1.Rows)
				{
					valueList2.ValueListItems.Add(row.Cells["AttributeID"].Value.ToString());
				}
				if (valueList2.ValueListItems.Count > 0)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].ValueList = valueList2;
				}
				else
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].ValueList = null;
				}
				break;
			}
			case 2:
			{
				ValueList valueList3 = new ValueList();
				foreach (UltraGridRow row2 in dataGridDimension2.Rows)
				{
					valueList3.ValueListItems.Add(row2.Cells["AttributeID"].Value.ToString());
				}
				if (valueList3.ValueListItems.Count > 0)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].ValueList = valueList3;
				}
				else
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].ValueList = null;
				}
				break;
			}
			case 3:
			{
				ValueList valueList = new ValueList();
				foreach (UltraGridRow row3 in dataGridDimension3.Rows)
				{
					valueList.ValueListItems.Add(row3.Cells["AttributeID"].Value.ToString());
				}
				if (valueList.ValueListItems.Count > 0)
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].ValueList = valueList;
				}
				else
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].ValueList = null;
				}
				break;
			}
			}
		}

		private void dataGridAttributes_AfterEnterEditMode(object sender, EventArgs e)
		{
			if (!(dataGridAttributes.ActiveCell.Column.Key == "ProductID") || !(dataGridAttributes.ActiveCell.Value.ToString() == ""))
			{
				return;
			}
			string text = textBoxCode.Text + "-" + dataGridAttributes.ActiveRow.Cells["Attribute1"].Value.ToString();
			string text2 = textBoxName.Text;
			if (componentDescGeneration == 2)
			{
				text2 = text2 + " " + dataGridAttributes.ActiveRow.Cells["Attribute1"].Value.ToString();
			}
			if (dataGridAttributes.ActiveRow.Cells["Attribute2"].Value != null && dataGridAttributes.ActiveRow.Cells["Attribute2"].Value.ToString() != "")
			{
				text = text + "-" + dataGridAttributes.ActiveRow.Cells["Attribute2"].Value.ToString();
				if (componentDescGeneration == 2)
				{
					text2 = text2 + " " + dataGridAttributes.ActiveRow.Cells["Attribute2"].Value.ToString();
				}
			}
			if (dataGridAttributes.ActiveRow.Cells["Attribute3"].Value != null && dataGridAttributes.ActiveRow.Cells["Attribute3"].Value.ToString() != "")
			{
				text = text + "-" + dataGridAttributes.ActiveRow.Cells["Attribute3"].Value.ToString();
				if (componentDescGeneration == 2)
				{
					text2 = text2 + " " + dataGridAttributes.ActiveRow.Cells["Attribute3"].Value.ToString();
				}
			}
			if (dataGridAttributes.ExistCellValue("ProductID", text) < 0)
			{
				dataGridAttributes.ActiveCell.Value = text;
			}
			dataGridAttributes.ActiveRow.Cells["Description"].Value = text2;
			dataGridAttributes.ActiveRow.Cells["UnitPrice1"].Value = textBoxStandardPrice.Text;
			dataGridAttributes.ActiveRow.Cells["UnitPrice2"].Value = textBoxWholesalePrice.Text;
			dataGridAttributes.ActiveRow.Cells["UnitPrice3"].Value = textBoxSpecialPrice.Text;
			dataGridAttributes.ActiveRow.Cells["MinPrice"].Value = textBoxMinimumPrice.Text;
			dataGridAttributes.ActiveCell.SelectAll();
		}

		private void dataGridAttributes_LostFocus(object sender, EventArgs e)
		{
			MergedCellStyle mergedCellStyle3 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].MergedCellStyle = (dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].MergedCellStyle = MergedCellStyle.Always);
		}

		private void dataGridAttributes_GotFocus(object sender, EventArgs e)
		{
			MergedCellStyle mergedCellStyle3 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].MergedCellStyle = (dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].MergedCellStyle = MergedCellStyle.Never);
		}

		private void dataGridAttributes_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGridAttributes_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
		}

		private void dataGridAttributes_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "Attribute1")
			{
				if (e.Cell.Text.ToString() != "" && comboBoxDimension1.SelectedID == "")
				{
					ErrorHelper.InformationMessage("You have not selected the dimension 1 for this item. Please select dimension 1 for the matrix.");
					e.Cancel = true;
				}
				else if (dataGridDimension1.ExistCellValue("AttributeID", e.NewValue.ToString().Trim()) < 0)
				{
					UltraGridRow ultraGridRow = dataGridDimension1.DisplayLayout.Bands[0].AddNew();
					object obj2 = ultraGridRow.Cells["AttributeID"].Value = (ultraGridRow.Cells["AttributeName"].Value = e.NewValue.ToString().Trim());
				}
			}
			else if (e.Cell.Column.Key == "Attribute2")
			{
				if (e.Cell.Text.ToString() != "" && comboBoxDimension2.SelectedID == "")
				{
					ErrorHelper.InformationMessage("You have not selected the dimension 2 for this item. Please select dimension 2 for the matrix.");
					e.Cancel = true;
				}
				else if (dataGridDimension2.ExistCellValue("AttributeID", e.NewValue.ToString().Trim()) < 0)
				{
					UltraGridRow ultraGridRow2 = dataGridDimension2.DisplayLayout.Bands[0].AddNew();
					object obj2 = ultraGridRow2.Cells["AttributeID"].Value = (ultraGridRow2.Cells["AttributeName"].Value = e.NewValue.ToString().Trim());
				}
			}
			else if (e.Cell.Column.Key == "Attribute3")
			{
				if (e.Cell.Text.ToString() != "" && comboBoxDimension3.SelectedID == "")
				{
					ErrorHelper.InformationMessage("You have not selected the dimension 3 for this item. Please select dimension 3 for the matrix.");
					e.Cancel = true;
				}
				else if (dataGridDimension3.ExistCellValue("AttributeID", e.NewValue.ToString().Trim()) < 0)
				{
					UltraGridRow ultraGridRow3 = dataGridDimension3.DisplayLayout.Bands[0].AddNew();
					object obj2 = ultraGridRow3.Cells["AttributeID"].Value = (ultraGridRow3.Cells["AttributeName"].Value = e.NewValue.ToString().Trim());
				}
			}
		}

		private void dataGridAttributes_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			if (dataGridAttributes.HasRowAnyValue(e.Row))
			{
				if (e.Row.Cells["Attribute1"].Value == null || e.Row.Cells["Attribute1"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter the Attribute 1.");
					e.Cancel = true;
					return;
				}
				foreach (UltraGridRow row in dataGridAttributes.Rows)
				{
					if (dataGridAttributes.ActiveRow == null)
					{
						return;
					}
					if (row != dataGridAttributes.ActiveRow)
					{
						if (row.Cells["Attribute1"].Value.ToString() == dataGridAttributes.ActiveRow.Cells["Attribute1"].Text.ToString() && row.Cells["Attribute2"].Value.ToString() == dataGridAttributes.ActiveRow.Cells["Attribute2"].Text.ToString() && row.Cells["Attribute3"].Value.ToString() == dataGridAttributes.ActiveRow.Cells["Attribute3"].Text.ToString())
						{
							ErrorHelper.InformationMessage("There is already a row with the same attributes. Please enter different attributes.");
							e.Cancel = true;
							return;
						}
						if (row.Cells["ProductID"].Value.ToString() != "" && row.Cells["ProductID"].Value.ToString().ToLower() == dataGridAttributes.ActiveRow.Cells["ProductID"].Text.ToLower().ToString())
						{
							ErrorHelper.InformationMessage("There is already a row with the same Item Code. Please enter different product code.");
							e.Cancel = true;
							return;
						}
					}
				}
				if (e.Row.Cells["UnitPrice1"].Value == null || e.Row.Cells["UnitPrice1"].Value.ToString() == "")
				{
					e.Row.Cells["UnitPrice1"].Value = 0.ToString(Format.UnitPriceFormat);
				}
			}
			else
			{
				e.Row.Delete();
			}
		}

		private void dataGridAttributes_BeforeRowInsert(object sender, BeforeRowInsertEventArgs e)
		{
		}

		private void dataGridAttributes_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && dataGridAttributes.Selected.Rows.Count > 0)
			{
				DeleteComponents();
			}
		}

		private void AddEvents()
		{
			base.Load += MatrixProductDetailsForm_Load;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			textBoxName.TextChanged += textBoxName_TextChanged;
			comboBoxDimension1.SelectedIndexChanged += comboBoxDimension1_SelectedIndexChanged;
			comboBoxDimension2.SelectedIndexChanged += comboBoxDimension2_SelectedIndexChanged;
			comboBoxDimension3.SelectedIndexChanged += comboBoxDimension3_SelectedIndexChanged;
			matrixTemplateComboBox.SelectedIndexChanged += matrixTemplateComboBox_SelectedIndexChanged;
			contextMenuStripComponents.Opening += contextMenuStripComponents_Opening;
		}

		private void contextMenuStripComponents_Opening(object sender, CancelEventArgs e)
		{
			if (dataGridAttributes.ActiveRow == null)
			{
				contextMenuStripComponents.Enabled = false;
			}
			else
			{
				contextMenuStripComponents.Enabled = true;
			}
		}

		private void matrixTemplateComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadFromTemplate();
		}

		private void LoadFromTemplate()
		{
			if (matrixTemplateComboBox.SelectedID == "")
			{
				return;
			}
			DataSet matrixTemplateByID = Factory.MatrixTemplateSystem.GetMatrixTemplateByID(matrixTemplateComboBox.SelectedID);
			if (!matrixTemplateByID.Tables.Contains("Matrix_Template_Detail") || matrixTemplateByID.Tables["Matrix_Template_Detail"].Rows.Count <= 0)
			{
				return;
			}
			DataTable dataTable = matrixTemplateByID.Tables["Matrix_Template_Detail"];
			DataRow[] array = dataTable.Select("Dimension=1");
			checked
			{
				if (array.Length != 0)
				{
					comboBoxDimension1.SelectedID = array[0]["DimensionID"].ToString();
					DataTable dataTable2 = new DataTable();
					dataTable2.Columns.Add("AttributeID");
					dataTable2.Columns.Add("AttributeName");
					for (int i = 0; i < array.Length; i++)
					{
						dataTable2.Rows.Add(array[i]["AttributeID"].ToString(), array[i]["AttributeName"].ToString());
					}
				}
				array = dataTable.Select("Dimension=2");
				if (array.Length != 0)
				{
					comboBoxDimension2.SelectedID = array[0]["DimensionID"].ToString();
					DataTable dataTable3 = new DataTable();
					dataTable3.Columns.Add("AttributeID");
					dataTable3.Columns.Add("AttributeName");
					for (int j = 0; j < array.Length; j++)
					{
						dataTable3.Rows.Add(array[j]["AttributeID"].ToString(), array[j]["AttributeName"].ToString());
					}
				}
				array = dataTable.Select("Dimension=3");
				if (array.Length != 0)
				{
					comboBoxDimension3.SelectedID = array[0]["DimensionID"].ToString();
					DataTable dataTable4 = new DataTable();
					dataTable4.Columns.Add("AttributeID");
					dataTable4.Columns.Add("AttributeName");
					for (int k = 0; k < array.Length; k++)
					{
						dataTable4.Rows.Add(array[k]["AttributeID"].ToString(), array[k]["AttributeName"].ToString());
					}
				}
			}
		}

		private void comboBoxDimension3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension3.SelectedID == "")
			{
				dataGridDimension3.DataSource = null;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute3"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = "Attribute3";
				}
			}
			else
			{
				DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension3.SelectedID);
				dataGridDimension3.DataSource = dimensionAttributes;
				dataGridDimension3.DisplayLayout.Bands[0].ColHeadersVisible = false;
				dataGridDimension3.DisplayLayout.Bands[0].Columns["AttributeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridDimension3.DisplayLayout.Bands[0].Columns["AttributeID"].MaxLength = 15;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute3"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = comboBoxDimension3.Text;
				}
			}
			SetValueList(3);
		}

		private void comboBoxDimension2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension2.SelectedID == "")
			{
				dataGridDimension2.DataSource = null;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute2"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = "Attribute2";
				}
			}
			else
			{
				DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension2.SelectedID);
				dataGridDimension2.DataSource = dimensionAttributes;
				dataGridDimension2.DisplayLayout.Bands[0].ColHeadersVisible = false;
				dataGridDimension2.DisplayLayout.Bands[0].Columns["AttributeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridDimension2.DisplayLayout.Bands[0].Columns["AttributeID"].MaxLength = 15;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute2"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = comboBoxDimension2.Text;
				}
			}
			panelDimension3.Enabled = (comboBoxDimension2.SelectedID != "");
			SetValueList(2);
		}

		private void comboBoxDimension1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension1.SelectedID == "")
			{
				dataGridDimension1.DataSource = null;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute1"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = "Attribute1";
				}
			}
			else
			{
				DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension1.SelectedID);
				dataGridDimension1.DataSource = dimensionAttributes;
				dataGridDimension1.DisplayLayout.Bands[0].ColHeadersVisible = false;
				dataGridDimension1.DisplayLayout.Bands[0].Columns["AttributeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridDimension1.DisplayLayout.Bands[0].Columns["AttributeID"].MaxLength = 15;
				if (dataGridAttributes.DisplayLayout.Bands[0].Columns.Exists("Attribute1"))
				{
					dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = comboBoxDimension1.Text;
				}
			}
			SetValueList(1);
			panelDimension2.Enabled = (comboBoxDimension1.SelectedID != "");
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			if (textBoxCode.Text.Trim() != "")
			{
				Text = "Item Details - " + textBoxCode.Text.Trim();
			}
			else
			{
				Text = "Item Details";
			}
			SetHeaderName();
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ProductParentData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ProductParentTable.Rows[0] : currentData.ProductParentTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ProductParentID"] = textBoxCode.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow["Description"] = textBoxName.Text;
				dataRow["ExcludeFromCatalogue"] = checkBoxExcludeFromCatalogue.Checked;
				dataRow["ParentType"] = ItemParentTypes.Matrix;
				dataRow["CostMethod"] = comboBoxCostMethod.SelectedID;
				dataRow["UnitID"] = comboBoxUOM.SelectedID;
				dataRow["CategoryID"] = comboBoxCategory.SelectedID;
				if (textBoxStandardPrice.Text != "")
				{
					dataRow["UnitPrice1"] = textBoxStandardPrice.Text;
				}
				else
				{
					dataRow["UnitPrice1"] = 0;
				}
				if (textBoxWholesalePrice.Text != "")
				{
					dataRow["UnitPrice2"] = textBoxWholesalePrice.Text;
				}
				else
				{
					dataRow["UnitPrice2"] = 0;
				}
				if (textBoxSpecialPrice.Text != "")
				{
					dataRow["UnitPrice3"] = textBoxSpecialPrice.Text;
				}
				else
				{
					dataRow["UnitPrice3"] = 0;
				}
				if (textBoxMinimumPrice.Text != "")
				{
					dataRow["MinPrice"] = textBoxMinimumPrice.Text;
				}
				else
				{
					dataRow["MinPrice"] = 0;
				}
				dataRow["QuantityPerUnit"] = textBoxQuantityPerUnit.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["ManufacturerID"] = comboBoxManufacturer.SelectedID;
				dataRow["BrandID"] = comboBoxBrand.SelectedID;
				dataRow["Origin"] = comboBoxOrigin.SelectedID;
				dataRow["PreferredVendor"] = comboBoxPrefVendor.SelectedID;
				dataRow["ItemType"] = (byte)6;
				byte b = 1;
				if (comboBoxDimension3.SelectedID != "")
				{
					b = 3;
				}
				else if (comboBoxDimension2.SelectedID != "")
				{
					b = 2;
				}
				dataRow["Dimensions"] = b;
				dataRow["Dim1"] = comboBoxDimension1.SelectedID;
				if (comboBoxDimension2.SelectedID != "")
				{
					dataRow["Dim2"] = comboBoxDimension2.SelectedID;
				}
				else
				{
					dataRow["Dim2"] = DBNull.Value;
				}
				if (comboBoxDimension3.SelectedID != "")
				{
					dataRow["Dim3"] = comboBoxDimension3.Text;
				}
				else
				{
					dataRow["Dim3"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ProductParentTable.Rows.Add(dataRow);
				}
				DataTable matrixAttributeDimensionTable = currentData.MatrixAttributeDimensionTable;
				matrixAttributeDimensionTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridDimension1.Rows)
				{
					dataRow = matrixAttributeDimensionTable.NewRow();
					dataRow["ProductParentID"] = textBoxCode.Text;
					dataRow["Dimension"] = 1;
					dataRow["DimensionID"] = comboBoxDimension1.SelectedID;
					dataRow["AttributeID"] = row.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					matrixAttributeDimensionTable.Rows.Add(dataRow);
				}
				foreach (UltraGridRow row2 in dataGridDimension2.Rows)
				{
					dataRow = matrixAttributeDimensionTable.NewRow();
					dataRow["ProductParentID"] = textBoxCode.Text;
					dataRow["Dimension"] = 2;
					dataRow["DimensionID"] = comboBoxDimension2.SelectedID;
					dataRow["AttributeID"] = row2.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row2.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row2.Index;
					dataRow.EndEdit();
					matrixAttributeDimensionTable.Rows.Add(dataRow);
				}
				foreach (UltraGridRow row3 in dataGridDimension3.Rows)
				{
					dataRow = matrixAttributeDimensionTable.NewRow();
					dataRow["ProductParentID"] = textBoxCode.Text;
					dataRow["Dimension"] = 3;
					dataRow["DimensionID"] = comboBoxDimension3.SelectedID;
					dataRow["AttributeID"] = row3.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row3.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row3.Index;
					dataRow.EndEdit();
					matrixAttributeDimensionTable.Rows.Add(dataRow);
				}
				matrixAttributeDimensionTable = currentData.ProdutParentComponentsTable;
				matrixAttributeDimensionTable.Rows.Clear();
				foreach (UltraGridRow row4 in dataGridAttributes.Rows)
				{
					dataRow = matrixAttributeDimensionTable.NewRow();
					dataRow["ProductParentID"] = textBoxCode.Text;
					dataRow["ProductID"] = row4.Cells["ProductID"].Value.ToString();
					dataRow["Description"] = row4.Cells["Description"].Value.ToString();
					dataRow["Attribute1"] = row4.Cells["Attribute1"].Value.ToString();
					dataRow["Attribute2"] = row4.Cells["Attribute2"].Value.ToString();
					dataRow["Attribute3"] = row4.Cells["Attribute3"].Value.ToString();
					dataRow["UPC"] = row4.Cells["UPC"].Value.ToString();
					if (row4.Cells["UnitPrice1"].Value != null && row4.Cells["UnitPrice1"].Value.ToString() != "")
					{
						dataRow["UnitPrice1"] = row4.Cells["UnitPrice1"].Value.ToString();
					}
					else
					{
						dataRow["UnitPrice1"] = 0;
					}
					if (row4.Cells["UnitPrice2"].Value != null && row4.Cells["UnitPrice2"].Value.ToString() != "")
					{
						dataRow["UnitPrice2"] = row4.Cells["UnitPrice2"].Value.ToString();
					}
					if (row4.Cells["UnitPrice3"].Value != null && row4.Cells["UnitPrice3"].Value.ToString() != "")
					{
						dataRow["UnitPrice3"] = row4.Cells["UnitPrice3"].Value.ToString();
					}
					if (row4.Cells["MinPrice"].Value != null && row4.Cells["MinPrice"].Value.ToString() != "")
					{
						dataRow["MinPrice"] = row4.Cells["MinPrice"].Value.ToString();
					}
					dataRow["MatrixParentID"] = textBoxCode.Text;
					dataRow["ItemType"] = ItemTypes.Inventory;
					dataRow["CostMethod"] = comboBoxCostMethod.SelectedID;
					dataRow["UnitID"] = comboBoxUOM.SelectedID;
					dataRow["CategoryID"] = comboBoxCategory.SelectedID;
					dataRow["QuantityPerUnit"] = textBoxQuantityPerUnit.Text;
					dataRow["ManufacturerID"] = comboBoxManufacturer.SelectedID;
					dataRow["BrandID"] = comboBoxBrand.SelectedID;
					dataRow["Origin"] = comboBoxOrigin.SelectedID;
					dataRow["PreferredVendor"] = comboBoxPrefVendor.SelectedID;
					if (isNewRecord)
					{
						dataRow["IsNewRow"] = true;
					}
					else if (row4.Cells["ProductID"].Activation == Activation.AllowEdit)
					{
						dataRow["IsNewRow"] = true;
					}
					else
					{
						dataRow["IsNewRow"] = false;
					}
					dataRow.EndEdit();
					matrixAttributeDimensionTable.Rows.Add(dataRow);
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
					currentData = Factory.ProductParentSystem.GetProductParentByID(id.Trim());
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
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
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
			checked
			{
				try
				{
					isLoading = true;
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = currentData.Tables[0].Rows[0];
						textBoxCode.Text = dataRow["ProductParentID"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						if (dataRow["IsInactive"] != DBNull.Value)
						{
							checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
						}
						else
						{
							checkBoxInactive.Checked = false;
						}
						textBoxName.Text = dataRow["Description"].ToString();
						comboBoxCostMethod.SelectedID = int.Parse(dataRow["CostMethod"].ToString());
						comboBoxCategory.SelectedID = dataRow["CategoryID"].ToString();
						if (dataRow["ExcludeFromCatalogue"] != DBNull.Value)
						{
							checkBoxExcludeFromCatalogue.Checked = bool.Parse(dataRow["ExcludeFromCatalogue"].ToString());
						}
						else
						{
							checkBoxExcludeFromCatalogue.Checked = false;
						}
						if (dataRow["UnitPrice1"] != DBNull.Value)
						{
							textBoxStandardPrice.Text = decimal.Parse(dataRow["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
						}
						else
						{
							textBoxStandardPrice.Text = 0.ToString(Format.UnitPriceFormat);
						}
						if (dataRow["UnitPrice2"] != DBNull.Value)
						{
							textBoxWholesalePrice.Text = decimal.Parse(dataRow["UnitPrice2"].ToString()).ToString(Format.UnitPriceFormat);
						}
						else
						{
							textBoxWholesalePrice.Text = 0.ToString(Format.UnitPriceFormat);
						}
						if (dataRow["UnitPrice3"] != DBNull.Value)
						{
							textBoxSpecialPrice.Text = decimal.Parse(dataRow["UnitPrice3"].ToString()).ToString(Format.UnitPriceFormat);
						}
						else
						{
							textBoxSpecialPrice.Text = 0.ToString(Format.UnitPriceFormat);
						}
						if (dataRow["MinPrice"] != DBNull.Value)
						{
							textBoxMinimumPrice.Text = decimal.Parse(dataRow["MinPrice"].ToString()).ToString(Format.UnitPriceFormat);
						}
						else
						{
							textBoxMinimumPrice.Text = 0.ToString(Format.UnitPriceFormat);
						}
						textBoxQuantityPerUnit.Text = dataRow["QuantityPerUnit"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						comboBoxManufacturer.SelectedID = dataRow["ManufacturerID"].ToString();
						comboBoxBrand.SelectedID = dataRow["BrandID"].ToString();
						comboBoxOrigin.SelectedID = dataRow["Origin"].ToString();
						comboBoxPrefVendor.SelectedID = dataRow["PreferredVendor"].ToString();
						if (dataRow["HasPhoto"] != DBNull.Value)
						{
							bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
							linkLoadImage.Visible = flag;
							linkRemovePicture.Enabled = flag;
							if (flag)
							{
								pictureBoxPhoto.Image = null;
							}
							else
							{
								pictureBoxPhoto.Image = pictureBoxNoImage.Image;
							}
						}
						else
						{
							linkLoadImage.Visible = false;
							pictureBoxPhoto.Image = pictureBoxNoImage.Image;
							linkRemovePicture.Enabled = false;
						}
						if (currentData.Tables.Contains("Matrix_Attribute_Dimension") && currentData.Tables["Matrix_Attribute_Dimension"].Rows.Count > 0)
						{
							DataTable dataTable = currentData.Tables["Matrix_Attribute_Dimension"];
							DataRow[] array = dataTable.Select("Dimension=1");
							if (array.Length != 0)
							{
								comboBoxDimension1.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable2 = new DataTable();
								dataTable2.Columns.Add("AttributeID");
								dataTable2.Columns.Add("AttributeName");
								for (int i = 0; i < array.Length; i++)
								{
									dataTable2.Rows.Add(array[i]["AttributeID"].ToString(), array[i]["AttributeName"].ToString());
								}
								dataGridDimension1.DataSource = dataTable2;
							}
							array = dataTable.Select("Dimension=2");
							if (array.Length != 0)
							{
								comboBoxDimension2.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable3 = new DataTable();
								dataTable3.Columns.Add("AttributeID");
								dataTable3.Columns.Add("AttributeName");
								for (int j = 0; j < array.Length; j++)
								{
									dataTable3.Rows.Add(array[j]["AttributeID"].ToString(), array[j]["AttributeName"].ToString());
								}
								dataGridDimension2.DataSource = dataTable3;
							}
							array = dataTable.Select("Dimension=3");
							if (array.Length != 0)
							{
								comboBoxDimension3.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable4 = new DataTable();
								dataTable4.Columns.Add("AttributeID");
								dataTable4.Columns.Add("AttributeName");
								for (int k = 0; k < array.Length; k++)
								{
									dataTable4.Rows.Add(array[k]["AttributeID"].ToString(), array[k]["AttributeName"].ToString());
								}
								dataGridDimension3.DataSource = dataTable4;
							}
						}
						productData.Tables[0].Rows.Clear();
						foreach (DataRow row in currentData.Tables["Product_Parent_Components"].Rows)
						{
							dataRow = productData.Tables[0].NewRow();
							foreach (DataColumn column in productData.Tables[0].Columns)
							{
								dataRow[column.ColumnName] = row[column.ColumnName];
							}
							dataRow.EndEdit();
							productData.Tables[0].Rows.Add(dataRow);
						}
						foreach (UltraGridRow row2 in dataGridAttributes.Rows)
						{
							row2.Cells["ProductID"].Activation = Activation.NoEdit;
							row2.Cells["ProductID"].Appearance.BackColor = Color.WhiteSmoke;
							row2.Cells["Attribute1"].Activation = Activation.NoEdit;
							row2.Cells["Attribute2"].Activation = Activation.NoEdit;
							row2.Cells["Attribute3"].Activation = Activation.NoEdit;
							row2.Cells["Attribute1"].Appearance.BackColor = Color.WhiteSmoke;
							row2.Cells["Attribute2"].Appearance.BackColor = Color.WhiteSmoke;
							row2.Cells["Attribute3"].Appearance.BackColor = Color.WhiteSmoke;
						}
						dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice1"].Format = Format.UnitPriceFormat;
						SetHeaderName();
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
				bool flag = (!isNewRecord) ? Factory.ProductParentSystem.UpdateProductParent(currentData) : Factory.ProductParentSystem.CreateProductParent(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Product, needRefresh: true);
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
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					string str = "One of the product codes that you have entered is already exist. Please make sure that all product codes are unique and do not exist.\nThe duplicate product code is: ";
					str += checked(ex.Message.Substring(ex.Message.IndexOf('(') + 1, ex.Message.IndexOf(')') - ex.Message.IndexOf('(') - 1));
					ErrorHelper.ErrorMessage(str);
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
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
			if (dataGridAttributes.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("You must enter at least one component for this matrix.");
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[1];
				dataGridAttributes.Focus();
				return false;
			}
			if (dataGridAttributes.Rows.Count > 0 && !dataGridAttributes.PerformAction(UltraGridAction.FirstRowInBand))
			{
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[1];
				dataGridAttributes.Focus();
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Product_Parent", "ProductParentID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
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
			if (comboBoxCostMethod.Enabled && comboBoxCostMethod.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select the costing method.");
				return false;
			}
			int num = 1;
			if (comboBoxDimension3.SelectedID != "")
			{
				num = 3;
			}
			else if (comboBoxDimension2.SelectedID != "")
			{
				num = 2;
			}
			foreach (UltraGridRow row in dataGridAttributes.Rows)
			{
				if (num == 3)
				{
					if (row.Cells["Attribute3"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("One of components does not have a value for Attribute 3.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
					if (dataGridDimension3.ExistCellValue("AttributeID", row.Cells["Attribute3"].Value.ToString()) < 0)
					{
						ErrorHelper.InformationMessage("Attribute " + row.Cells["Attribute3"].Value.ToString() + " does not exist in dimension 3. Please add the attribute.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
				}
				if (num >= 2)
				{
					if (row.Cells["Attribute2"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("One of components does not have a value for Attribute 2.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
					if (dataGridDimension2.ExistCellValue("AttributeID", row.Cells["Attribute2"].Value.ToString()) < 0)
					{
						ErrorHelper.InformationMessage("Attribute " + row.Cells["Attribute2"].Value.ToString() + " does not exist in dimension 2. Please add the attribute.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
				}
				if (num >= 1)
				{
					if (row.Cells["Attribute1"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("One of components does not have a value for Attribute 1.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
					if (dataGridDimension1.ExistCellValue("AttributeID", row.Cells["Attribute1"].Value.ToString()) < 0)
					{
						ErrorHelper.InformationMessage("Attribute " + row.Cells["Attribute1"].Value.ToString() + " does not exist in dimension 1. Please add the attribute.");
						ultraTabControl1.SelectedTab = tabPageDetails.Tab;
						dataGridAttributes.Focus();
						return false;
					}
				}
				if (num == 2 && row.Cells["Attribute3"].Value != null && row.Cells["Attribute3"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("You have entered attribute 3 for some items but Dimension3 is not selected. Please select Dimension 3 or remove the attributes 3.");
					return false;
				}
				if (num == 1 && row.Cells["Attribute2"].Value != null && row.Cells["Attribute2"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("You have entered attribute 2 for some items but Dimension 2 is not selected. Please select Dimension 2 or remove the attributes 2.");
					return false;
				}
				if (row.Cells["ProductID"].Value == null || row.Cells["ProductID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter Item Code for all component rows.");
					return false;
				}
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Product_Parent", "ProductParentID", textBoxCode.Text.Trim()))
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
			textBoxNote.Clear();
			textBoxCode.Enabled = true;
			comboBoxUOM.Clear();
			comboBoxCategory.Clear();
			textBoxStandardPrice.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxQuantityPerUnit.Text = "1";
			textBoxNote.Clear();
			comboBoxManufacturer.Clear();
			comboBoxBrand.Clear();
			comboBoxOrigin.Clear();
			matrixTemplateComboBox.Clear();
			checkBoxExcludeFromCatalogue.Checked = false;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].ValueList = null;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].ValueList = null;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].ValueList = null;
			if (dataGridAttributes.DisplayLayout.Bands[0] != null)
			{
				dataGridAttributes.DisplayLayout.Bands[0].Columns["ProductID"].CellActivation = Activation.AllowEdit;
			}
			Panel panel = panelDimension3;
			bool enabled = panelDimension2.Enabled = false;
			panel.Enabled = enabled;
			productData.Tables[0].Rows.Clear();
			if (dataGridAttributes.DisplayLayout.Bands.Count > 0)
			{
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = "Attribute1";
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = "Attribute2";
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = "Attribute3";
			}
			comboBoxDimension1.Clear();
			comboBoxDimension2.Clear();
			comboBoxDimension3.Clear();
			comboBoxPrefVendor.Clear();
			dataGridDimension1.DataSource = null;
			dataGridDimension2.DataSource = null;
			dataGridDimension3.DataSource = null;
			checkBoxInactive.Checked = false;
			pictureBoxPhoto.Image = null;
			ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
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
				DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				bool flag = true;
				switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to delete all the components for this matrix also?\nTo delete all the components click 'Yes'.\nTo keep the components click 'No'."))
				{
				case DialogResult.Yes:
					flag = true;
					break;
				case DialogResult.No:
					flag = false;
					break;
				default:
					return false;
				}
				return Factory.ProductParentSystem.DeleteProductParent(textBoxCode.Text, flag);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1029)
				{
					ErrorHelper.ErrorMessage("Cannot delete this matrix. " + ex.Message);
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
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
			LoadData(DatabaseHelper.GetNextID("Product_Parent", "ProductParentID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Product_Parent", "ProductParentID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Product_Parent", "ProductParentID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Product_Parent", "ProductParentID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Product_Parent", "ProductParentID", toolStripTextBoxFind.Text.Trim()))
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

		private void MatrixProductDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				Global.GlobalSettings.LoadFormProperties(this);
				comboBoxCostMethod.LoadData();
				comboBoxCostMethod.SelectedIndex = 0;
				SetupGrids();
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void SetupGrids()
		{
			dataGridDimension1.SetupUI();
			dataGridDimension2.SetupUI();
			dataGridDimension3.SetupUI();
			dataGridDimension1.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension2.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension3.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension1.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
			dataGridDimension2.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
			dataGridDimension3.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
			productData = new DataSet();
			dataGridAttributes.Clear();
			productData.Tables.Add("Product");
			DataColumnCollection columns = productData.Tables[0].Columns;
			columns.Add("Attribute1");
			columns.Add("Attribute2");
			columns.Add("Attribute3");
			columns.Add("ProductID").MaxLength = 64;
			columns.Add("Description").MaxLength = 255;
			columns.Add("UPC").MaxLength = 30;
			columns.Add("UnitPrice1", typeof(decimal));
			columns.Add("UnitPrice2", typeof(decimal));
			columns.Add("UnitPrice3", typeof(decimal));
			columns.Add("MinPrice", typeof(decimal));
			dataGridAttributes.DataSource = productData;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].MergedCellStyle = MergedCellStyle.Always;
			if (dataGridDimension2.Rows.Count > 0)
			{
				dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].MergedCellStyle = MergedCellStyle.Always;
			}
			dataGridAttributes.DisplayLayout.Override.SelectTypeRow = SelectType.Extended;
			UltraGridColumn ultraGridColumn = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice2"];
			UltraGridColumn ultraGridColumn2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice3"];
			bool flag2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["MinPrice"].Hidden = true;
			bool hidden = ultraGridColumn2.Hidden = flag2;
			ultraGridColumn.Hidden = hidden;
			dataGridAttributes.LoadLayout();
			dataGridAttributes.SetupUI(2);
			AppearanceBase cellAppearance = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellAppearance;
			AppearanceBase cellAppearance2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellAppearance;
			AppearanceBase cellAppearance3 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellAppearance;
			HAlign hAlign2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["MinPrice"].CellAppearance.TextHAlign = HAlign.Right;
			HAlign hAlign4 = cellAppearance3.TextHAlign = hAlign2;
			HAlign hAlign7 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign4);
			UltraGridColumn ultraGridColumn3 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice1"];
			UltraGridColumn ultraGridColumn4 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice2"];
			UltraGridColumn ultraGridColumn5 = dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice3"];
			string text = dataGridAttributes.DisplayLayout.Bands[0].Columns["MinPrice"].Format = Format.UnitPriceFormat;
			string text3 = ultraGridColumn5.Format = text;
			string text6 = ultraGridColumn3.Format = (ultraGridColumn4.Format = text3);
			dataGridAttributes.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;
			dataGridAttributes.DisplayLayout.Bands[0].Override.RowSelectorStyle = HeaderStyle.WindowsXPCommand;
			dataGridAttributes.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.Fixed;
			dataGridAttributes.AllowAddNew = true;
			dataGridAttributes.DisplayLayout.Bands[0].Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.SeparateElement;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].CharacterCasing = CharacterCasing.Upper;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].CharacterCasing = CharacterCasing.Upper;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].CharacterCasing = CharacterCasing.Upper;
			UltraGridColumn ultraGridColumn6 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"];
			UltraGridColumn ultraGridColumn7 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"];
			int num2 = dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].MaxLength = 15;
			int num5 = ultraGridColumn6.MaxLength = (ultraGridColumn7.MaxLength = num2);
			dataGridAttributes.DisplayLayout.Bands[0].Columns["ProductID"].MaxLength = 64;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["UPC"].MaxLength = 30;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["ProductID"].Header.Caption = "Item Code";
			dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice1"].Header.Caption = CompanyPreferences.UnitPrice1Title;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice2"].Header.Caption = CompanyPreferences.UnitPrice2Title;
			dataGridAttributes.DisplayLayout.Bands[0].Columns["UnitPrice3"].Header.Caption = CompanyPreferences.UnitPrice3Title;
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductCategory(comboBoxCategory.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditUOM(comboBoxUOM.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductManufacturer(comboBoxManufacturer.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProductBrand(comboBoxBrand.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxOrigin.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxPrefVendor.SelectedID);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void buttonRemoveImage_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductListFormObj);
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (textBoxCode.Text != "" && !isNewRecord)
			{
				string text = textBoxCode.Text;
				FormActivator.ProductQuantityFormObj.LoadData(text);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void tabPageDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void buttonComponents_Click(object sender, EventArgs e)
		{
			if (textBoxCode.Text.Trim() == "" || textBoxName.Text == "")
			{
				ErrorHelper.WarningMessage("Please enter matrix item code and description first.");
				return;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Product_Parent", "ProductParentID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Matrix item code already exist.");
				textBoxCode.Focus();
				return;
			}
			CreateMatrixComponent createMatrixComponent = new CreateMatrixComponent();
			createMatrixComponent.DimensionGrid1 = dataGridDimension1;
			createMatrixComponent.DimensionGrid2 = dataGridDimension2;
			createMatrixComponent.DimensionGrid3 = dataGridDimension3;
			createMatrixComponent.IsNewRecord = isNewRecord;
			createMatrixComponent.Dimension1Name = comboBoxDimension1.Text;
			createMatrixComponent.Dimension2Name = comboBoxDimension2.Text;
			createMatrixComponent.Dimension3Name = comboBoxDimension3.Text;
			createMatrixComponent.SelectedData = productData;
			if (createMatrixComponent.ShowDialog(this) != DialogResult.Cancel)
			{
				textBoxCode.Enabled = false;
				if (isNewRecord)
				{
					(dataGridAttributes.DataSource as DataSet).Tables[0].Rows.Clear();
				}
				if (createMatrixComponent.CreateAll)
				{
					foreach (UltraGridRow row in dataGridDimension1.Rows)
					{
						if (dataGridDimension2.Rows.Count > 0)
						{
							foreach (UltraGridRow row2 in dataGridDimension2.Rows)
							{
								if (dataGridDimension3.Rows.Count > 0)
								{
									foreach (UltraGridRow row3 in dataGridDimension3.Rows)
									{
										AddComponentRow(row, row2, row3);
									}
								}
								else
								{
									AddComponentRow(row, row2, null);
								}
							}
						}
						else
						{
							AddComponentRow(row, null, null);
						}
					}
				}
				else
				{
					foreach (UltraGridRow row4 in createMatrixComponent.ComponentsGrid.Rows)
					{
						if (bool.Parse(row4.Cells["Check"].Value.ToString()))
						{
							string atr1Code = row4.Cells["Attribute1"].Value.ToString();
							string atr2Code = row4.Cells["Attribute2"].Value.ToString();
							string atr3Code = row4.Cells["Attribute3"].Value.ToString();
							string atr1Name = row4.Cells["Attribute1Name"].Value.ToString();
							string atr2Name = row4.Cells["Attribute2Name"].Value.ToString();
							string atr3Name = row4.Cells["Attribute2Name"].Value.ToString();
							AddComponentRow(atr1Code, atr1Name, atr2Code, atr2Name, atr3Code, atr3Name);
						}
					}
				}
				if (!isNewRecord)
				{
					foreach (UltraGridRow row5 in dataGridAttributes.Rows)
					{
						if (row5.Cells["ProductID"].Activation == Activation.AllowEdit)
						{
							row5.Appearance.FontData.Bold = DefaultableBoolean.True;
						}
					}
				}
			}
		}

		private void AddComponentRow(UltraGridRow atr1, UltraGridRow atr2, UltraGridRow atr3)
		{
			string atr1Code = "";
			string atr1Name = "";
			string atr2Code = "";
			string atr2Name = "";
			string atr3Code = "";
			string atr3Name = "";
			if (atr1 != null)
			{
				atr1Code = atr1.Cells["AttributeID"].Value.ToString();
				atr1Name = atr1.Cells["AttributeName"].Value.ToString();
			}
			if (atr2 != null)
			{
				atr2Code = atr2.Cells["AttributeID"].Value.ToString();
				atr2Name = atr2.Cells["AttributeName"].Value.ToString();
			}
			if (atr3 != null)
			{
				atr3Code = atr3.Cells["AttributeID"].Value.ToString();
				atr3Name = atr3.Cells["AttributeName"].Value.ToString();
			}
			AddComponentRow(atr1Code, atr1Name, atr2Code, atr2Name, atr3Code, atr3Name);
		}

		private void AddComponentRow(string atr1Code, string atr1Name, string atr2Code, string atr2Name, string atr3Code, string atr3Name)
		{
			DataRow dataRow = productData.Tables[0].NewRow();
			string text = textBoxCode.Text + "-" + atr1Code;
			string text2 = textBoxName.Text;
			if (componentDescGeneration == 2)
			{
				text2 = text2 + " " + atr1Name;
			}
			dataRow["Attribute1"] = atr1Code;
			if (atr2Code != string.Empty)
			{
				text = text + "-" + atr2Code;
				if (componentDescGeneration == 2)
				{
					text2 = text2 + " " + atr2Name;
				}
				dataRow["Attribute2"] = atr2Code;
			}
			if (atr3Code != string.Empty)
			{
				text = text + "-" + atr3Code;
				if (componentDescGeneration == 2)
				{
					text2 = text2 + " " + atr3Name;
				}
				dataRow["Attribute3"] = atr3Code;
			}
			if ((dataGridAttributes.DataSource as DataSet).Tables[0].Select("ProductID='" + text + "'").Length == 0)
			{
				dataRow["ProductID"] = text;
				dataRow["Description"] = text2;
				dataRow["UnitPrice1"] = textBoxStandardPrice.Text;
				dataRow["UnitPrice2"] = textBoxWholesalePrice.Text;
				dataRow["UnitPrice3"] = textBoxSpecialPrice.Text;
				dataRow["MinPrice"] = textBoxMinimumPrice.Text;
				dataRow.EndEdit();
				productData.Tables[0].Rows.Add(dataRow);
			}
		}

		private bool DeleteComponents()
		{
			try
			{
				bool flag = true;
				if (dataGridAttributes.Selected.Rows.Count == 0 && dataGridAttributes.ActiveRow != null)
				{
					dataGridAttributes.ActiveRow.Selected = true;
				}
				if (dataGridAttributes.Selected.Rows.Count == 0)
				{
					return true;
				}
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete selected components?") == DialogResult.No)
				{
					return true;
				}
				if (IsNewRecord)
				{
					dataGridAttributes.DeleteSelectedRows(displayPrompt: false);
				}
				else
				{
					string[] selectedComponentsID = GetSelectedComponentsID();
					flag &= Factory.ProductParentSystem.DeleteComponents(selectedComponentsID);
					if (flag)
					{
						dataGridAttributes.DeleteSelectedRows(displayPrompt: false);
					}
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1029)
				{
					ErrorHelper.ErrorMessage("Cannot delete selected components. " + ex.Message);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private string[] GetSelectedComponentsID()
		{
			List<string> list = new List<string>();
			foreach (UltraGridRow row in dataGridAttributes.Selected.Rows)
			{
				string item = row.Cells["ProductID"].Value.ToString();
				list.Add(item);
			}
			return list.ToArray();
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (UltraGridRow row in dataGridAttributes.Rows)
			{
				row.Selected = true;
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteComponents();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (base.ActiveControl == null || base.ActiveControl.Parent == null)
			{
				return;
			}
			checked
			{
				if (base.ActiveControl.Parent.Name == dataGridDimension1.Name)
				{
					if (dataGridDimension1.ActiveRow != null && dataGridDimension1.ActiveRow.Index != 0)
					{
						dataGridDimension1.Rows.Move(dataGridDimension1.ActiveRow, dataGridDimension1.ActiveRow.Index - 1);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension2.Name)
				{
					if (dataGridDimension2.ActiveRow != null && dataGridDimension2.ActiveRow.Index != 0)
					{
						dataGridDimension2.Rows.Move(dataGridDimension2.ActiveRow, dataGridDimension2.ActiveRow.Index - 1);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension3.Name && dataGridDimension3.ActiveRow != null && dataGridDimension3.ActiveRow.Index != 0)
				{
					dataGridDimension3.Rows.Move(dataGridDimension3.ActiveRow, dataGridDimension3.ActiveRow.Index - 1);
				}
			}
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (base.ActiveControl == null || base.ActiveControl.Parent == null)
			{
				return;
			}
			checked
			{
				if (base.ActiveControl.Parent.Name == dataGridDimension1.Name)
				{
					if (dataGridDimension1.ActiveRow != null && dataGridDimension1.ActiveRow.Index != dataGridDimension1.Rows.Count - 1)
					{
						dataGridDimension1.Rows.Move(dataGridDimension1.ActiveRow, dataGridDimension1.ActiveRow.Index + 1);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension2.Name)
				{
					if (dataGridDimension2.ActiveRow != null && dataGridDimension2.ActiveRow.Index != dataGridDimension2.Rows.Count + 1)
					{
						dataGridDimension2.Rows.Move(dataGridDimension2.ActiveRow, dataGridDimension2.ActiveRow.Index + 1);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension3.Name && dataGridDimension3.ActiveRow != null && dataGridDimension3.ActiveRow.Index != dataGridDimension3.Rows.Count + 1)
				{
					dataGridDimension3.Rows.Move(dataGridDimension3.ActiveRow, dataGridDimension3.ActiveRow.Index + 1);
				}
			}
		}

		private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (base.ActiveControl == null || base.ActiveControl.Parent == null)
			{
				return;
			}
			checked
			{
				if (base.ActiveControl.Parent.Name == dataGridDimension1.Name)
				{
					if (dataGridDimension1.ActiveRow != null)
					{
						dataGridDimension1.ActiveRow.Delete(displayPrompt: false);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension2.Name)
				{
					if (dataGridDimension2.ActiveRow != null && dataGridDimension2.ActiveRow.Index != dataGridDimension2.Rows.Count - 1)
					{
						dataGridDimension2.ActiveRow.Delete(displayPrompt: false);
					}
				}
				else if (base.ActiveControl.Parent.Name == dataGridDimension3.Name && dataGridDimension3.ActiveRow != null && dataGridDimension3.ActiveRow.Index != dataGridDimension3.Rows.Count - 1)
				{
					dataGridDimension3.ActiveRow.Delete(displayPrompt: false);
				}
			}
		}

		private void toolStripButtonQuantity_Click(object sender, EventArgs e)
		{
			MatrixQuantityForm matrixQuantityForm = new MatrixQuantityForm();
			matrixQuantityForm.LoadData(textBoxCode.Text, textBoxName.Text);
			matrixQuantityForm.ShowDialog(this);
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddProductPhoto(textBoxCode.Text, image, isMatrixParent: true))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.ProductParentSystem.RemoveProductPhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove the image.");
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetProductThumbnailImage(textBoxCode.Text, isProductParentID: true);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			try
			{
				if (dataGridAttributes.ActiveRow != null)
				{
					if (isNewRecord || dataGridAttributes.ActiveRow.Cells["Attribute1"].Activation == Activation.AllowEdit)
					{
						dataGridAttributes.ActiveRow.Delete(displayPrompt: false);
					}
					else if (ErrorHelper.QuestionMessageYesNo("Removing item from matrix does not delete the item from your item list.\nDo you want to remove the item?") == DialogResult.Yes)
					{
						string productID = dataGridAttributes.ActiveRow.Cells["ProductID"].Value.ToString();
						if (Factory.ProductParentSystem.RemoveProductFromMatrix(textBoxCode.Text, productID))
						{
							dataGridAttributes.ActiveRow.Delete(displayPrompt: false);
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonAddItem_Click(object sender, EventArgs e)
		{
			try
			{
				AddItemToMatrix addItemToMatrix = new AddItemToMatrix();
				addItemToMatrix.DataGrid = dataGridAttributes;
				if (comboBoxDimension3.SelectedID != "")
				{
					addItemToMatrix.DimentionCount = 3;
				}
				else if (comboBoxDimension2.SelectedID != "")
				{
					addItemToMatrix.DimentionCount = 2;
				}
				else
				{
					addItemToMatrix.DimentionCount = 1;
				}
				addItemToMatrix.Attribute1List = (ValueList)dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute1"].ValueList;
				addItemToMatrix.Attribute2List = (ValueList)dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute2"].ValueList;
				addItemToMatrix.Attribute3List = (ValueList)dataGridAttributes.DisplayLayout.Bands[0].Columns["Attribute3"].ValueList;
				if (addItemToMatrix.ShowDialog() == DialogResult.OK && Factory.ProductParentSystem.AddProductToMatrix(textBoxCode.Text, addItemToMatrix.ProductID, addItemToMatrix.Attribute1, addItemToMatrix.Attribute2, addItemToMatrix.Attribute3))
				{
					DataTable dataTable = (dataGridAttributes.DataSource as DataSet).Tables[0];
					DataRow dataRow = dataTable.NewRow();
					dataRow["Attribute1"] = addItemToMatrix.Attribute1;
					dataRow["Attribute2"] = addItemToMatrix.Attribute2;
					dataRow["Attribute3"] = addItemToMatrix.Attribute3;
					dataRow["ProductID"] = addItemToMatrix.ProductID;
					DataRow dataRow2 = Factory.ProductSystem.GetProductByID(addItemToMatrix.ProductID).ProductTable.Rows[0];
					dataRow["Description"] = dataRow2["Description"];
					dataRow["UPC"] = dataRow2["UPC"];
					dataRow["UnitPrice1"] = dataRow2["UnitPrice1"];
					dataRow["Unitprice2"] = dataRow2["UnitPrice2"];
					dataRow["UnitPrice3"] = dataRow2["UnitPrice3"];
					dataRow["MinPrice"] = dataRow2["MinPrice"];
					dataTable.Rows.Add(dataRow);
					UltraGridRow ultraGridRow = dataGridAttributes.Rows[checked(dataGridAttributes.Rows.Count - 1)];
					ultraGridRow.Cells["ProductID"].Activation = Activation.NoEdit;
					ultraGridRow.Cells["ProductID"].Appearance.BackColor = Color.WhiteSmoke;
					ultraGridRow.Cells["Attribute1"].Activation = Activation.NoEdit;
					ultraGridRow.Cells["Attribute2"].Activation = Activation.NoEdit;
					ultraGridRow.Cells["Attribute3"].Activation = Activation.NoEdit;
					ultraGridRow.Cells["Attribute1"].Appearance.BackColor = Color.WhiteSmoke;
					ultraGridRow.Cells["Attribute2"].Appearance.BackColor = Color.WhiteSmoke;
					ultraGridRow.Cells["Attribute3"].Appearance.BackColor = Color.WhiteSmoke;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.MatrixProductDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxExcludeFromCatalogue = new System.Windows.Forms.CheckBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			textBoxPrefVendor = new Micromind.UISupport.MMTextBox();
			textBoxOrigin = new Micromind.UISupport.MMTextBox();
			textBoxBrand = new Micromind.UISupport.MMTextBox();
			textBoxManufact = new Micromind.UISupport.MMTextBox();
			labelMinPrice = new Micromind.UISupport.MMLabel();
			labelSpecialPrice = new Micromind.UISupport.MMLabel();
			labelWholesalePrice = new Micromind.UISupport.MMLabel();
			textBoxMinimumPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxSpecialPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxWholesalePrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPrefVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxOrigin = new Micromind.DataControls.CountryComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxBrand = new Micromind.DataControls.ProductBrandComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxManufacturer = new Micromind.DataControls.ProductManufacturerComboBox();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxUOM = new Micromind.DataControls.UnitComboBox();
			textBoxQuantityPerUnit = new Micromind.UISupport.QuantityTextBox();
			comboBoxCategory = new Micromind.DataControls.ProductCategoryComboBox();
			labelStandardPrice = new Micromind.UISupport.MMLabel();
			textBoxStandardPrice = new Micromind.UISupport.UnitPriceTextBox();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxCostMethod = new Micromind.DataControls.ItemCostingComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonAddItem = new Micromind.UISupport.XPButton();
			panelDimension1 = new System.Windows.Forms.Panel();
			dataGridDimension1 = new Micromind.DataControls.DataEntryGrid();
			contextMenuStripDimension = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			label1 = new System.Windows.Forms.Label();
			comboBoxDimension1 = new Micromind.DataControls.DimensionComboBox();
			panelDimension2 = new System.Windows.Forms.Panel();
			dataGridDimension2 = new Micromind.DataControls.DataEntryGrid();
			label2 = new System.Windows.Forms.Label();
			comboBoxDimension2 = new Micromind.DataControls.DimensionComboBox();
			panelDimension3 = new System.Windows.Forms.Panel();
			dataGridDimension3 = new Micromind.DataControls.DataEntryGrid();
			label3 = new System.Windows.Forms.Label();
			comboBoxDimension3 = new Micromind.DataControls.DimensionComboBox();
			label4 = new System.Windows.Forms.Label();
			matrixTemplateComboBox = new Micromind.DataControls.MatrixTemplateComboBox();
			buttonComponents = new Micromind.UISupport.XPButton();
			dataGridAttributes = new Micromind.DataControls.DataEntryGrid();
			contextMenuStripComponents = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItemRemoveComponent = new System.Windows.Forms.ToolStripMenuItem();
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
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			toolStripButtonQuantity = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrefVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManufacturer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			tabPageDetails.SuspendLayout();
			panelDimension1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension1).BeginInit();
			contextMenuStripDimension.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension1).BeginInit();
			panelDimension2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension2).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension2).BeginInit();
			panelDimension3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension3).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension3).BeginInit();
			((System.ComponentModel.ISupportInitialize)matrixTemplateComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).BeginInit();
			contextMenuStripComponents.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(checkBoxExcludeFromCatalogue);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(textBoxPrefVendor);
			tabPageGeneral.Controls.Add(textBoxOrigin);
			tabPageGeneral.Controls.Add(textBoxBrand);
			tabPageGeneral.Controls.Add(textBoxManufact);
			tabPageGeneral.Controls.Add(labelMinPrice);
			tabPageGeneral.Controls.Add(labelSpecialPrice);
			tabPageGeneral.Controls.Add(labelWholesalePrice);
			tabPageGeneral.Controls.Add(textBoxMinimumPrice);
			tabPageGeneral.Controls.Add(textBoxSpecialPrice);
			tabPageGeneral.Controls.Add(textBoxWholesalePrice);
			tabPageGeneral.Controls.Add(textBoxNote);
			tabPageGeneral.Controls.Add(mmLabel4);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel7);
			tabPageGeneral.Controls.Add(comboBoxPrefVendor);
			tabPageGeneral.Controls.Add(comboBoxOrigin);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel6);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel4);
			tabPageGeneral.Controls.Add(comboBoxBrand);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(comboBoxManufacturer);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel9);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel8);
			tabPageGeneral.Controls.Add(comboBoxUOM);
			tabPageGeneral.Controls.Add(textBoxQuantityPerUnit);
			tabPageGeneral.Controls.Add(comboBoxCategory);
			tabPageGeneral.Controls.Add(labelStandardPrice);
			tabPageGeneral.Controls.Add(textBoxStandardPrice);
			tabPageGeneral.Controls.Add(checkBoxInactive);
			tabPageGeneral.Controls.Add(mmLabel8);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Controls.Add(comboBoxCostMethod);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(725, 431);
			checkBoxExcludeFromCatalogue.AutoSize = true;
			checkBoxExcludeFromCatalogue.Location = new System.Drawing.Point(124, 258);
			checkBoxExcludeFromCatalogue.Name = "checkBoxExcludeFromCatalogue";
			checkBoxExcludeFromCatalogue.Size = new System.Drawing.Size(178, 17);
			checkBoxExcludeFromCatalogue.TabIndex = 21;
			checkBoxExcludeFromCatalogue.Text = "Exclude from Product Catalogue";
			checkBoxExcludeFromCatalogue.UseVisualStyleBackColor = true;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(666, 281);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 86;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(580, 143);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 12;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(541, 143);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 11;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance2;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(570, 60);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 83;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance3;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(541, 12);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 82;
			pictureBoxPhoto.TabStop = false;
			textBoxPrefVendor.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPrefVendor.CustomReportFieldName = "";
			textBoxPrefVendor.CustomReportKey = "";
			textBoxPrefVendor.CustomReportValueType = 1;
			textBoxPrefVendor.IsComboTextBox = false;
			textBoxPrefVendor.IsModified = false;
			textBoxPrefVendor.Location = new System.Drawing.Point(263, 233);
			textBoxPrefVendor.MaxLength = 255;
			textBoxPrefVendor.Name = "textBoxPrefVendor";
			textBoxPrefVendor.ReadOnly = true;
			textBoxPrefVendor.Size = new System.Drawing.Size(266, 20);
			textBoxPrefVendor.TabIndex = 20;
			textBoxPrefVendor.TabStop = false;
			textBoxOrigin.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxOrigin.CustomReportFieldName = "";
			textBoxOrigin.CustomReportKey = "";
			textBoxOrigin.CustomReportValueType = 1;
			textBoxOrigin.IsComboTextBox = false;
			textBoxOrigin.IsModified = false;
			textBoxOrigin.Location = new System.Drawing.Point(263, 211);
			textBoxOrigin.MaxLength = 255;
			textBoxOrigin.Name = "textBoxOrigin";
			textBoxOrigin.ReadOnly = true;
			textBoxOrigin.Size = new System.Drawing.Size(266, 20);
			textBoxOrigin.TabIndex = 18;
			textBoxOrigin.TabStop = false;
			textBoxBrand.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBrand.CustomReportFieldName = "";
			textBoxBrand.CustomReportKey = "";
			textBoxBrand.CustomReportValueType = 1;
			textBoxBrand.IsComboTextBox = false;
			textBoxBrand.IsModified = false;
			textBoxBrand.Location = new System.Drawing.Point(263, 189);
			textBoxBrand.MaxLength = 255;
			textBoxBrand.Name = "textBoxBrand";
			textBoxBrand.ReadOnly = true;
			textBoxBrand.Size = new System.Drawing.Size(266, 20);
			textBoxBrand.TabIndex = 16;
			textBoxBrand.TabStop = false;
			textBoxManufact.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxManufact.CustomReportFieldName = "";
			textBoxManufact.CustomReportKey = "";
			textBoxManufact.CustomReportValueType = 1;
			textBoxManufact.IsComboTextBox = false;
			textBoxManufact.IsModified = false;
			textBoxManufact.Location = new System.Drawing.Point(263, 167);
			textBoxManufact.MaxLength = 255;
			textBoxManufact.Name = "textBoxManufact";
			textBoxManufact.ReadOnly = true;
			textBoxManufact.Size = new System.Drawing.Size(266, 20);
			textBoxManufact.TabIndex = 14;
			textBoxManufact.TabStop = false;
			labelMinPrice.AutoSize = true;
			labelMinPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelMinPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelMinPrice.IsFieldHeader = false;
			labelMinPrice.IsRequired = false;
			labelMinPrice.Location = new System.Drawing.Point(264, 133);
			labelMinPrice.Name = "labelMinPrice";
			labelMinPrice.PenWidth = 1f;
			labelMinPrice.ShowBorder = false;
			labelMinPrice.Size = new System.Drawing.Size(78, 13);
			labelMinPrice.TabIndex = 79;
			labelMinPrice.Text = "Minimum Price:";
			labelSpecialPrice.AutoSize = true;
			labelSpecialPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSpecialPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelSpecialPrice.IsFieldHeader = false;
			labelSpecialPrice.IsRequired = false;
			labelSpecialPrice.Location = new System.Drawing.Point(8, 133);
			labelSpecialPrice.Name = "labelSpecialPrice";
			labelSpecialPrice.PenWidth = 1f;
			labelSpecialPrice.ShowBorder = false;
			labelSpecialPrice.Size = new System.Drawing.Size(65, 13);
			labelSpecialPrice.TabIndex = 80;
			labelSpecialPrice.Text = "Unit Price 3:";
			labelWholesalePrice.AutoSize = true;
			labelWholesalePrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelWholesalePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelWholesalePrice.IsFieldHeader = false;
			labelWholesalePrice.IsRequired = false;
			labelWholesalePrice.Location = new System.Drawing.Point(264, 112);
			labelWholesalePrice.Name = "labelWholesalePrice";
			labelWholesalePrice.PenWidth = 1f;
			labelWholesalePrice.ShowBorder = false;
			labelWholesalePrice.Size = new System.Drawing.Size(65, 13);
			labelWholesalePrice.TabIndex = 81;
			labelWholesalePrice.Text = "Unit Price 2:";
			textBoxMinimumPrice.CustomReportFieldName = "";
			textBoxMinimumPrice.CustomReportKey = "";
			textBoxMinimumPrice.CustomReportValueType = 1;
			textBoxMinimumPrice.IsComboTextBox = false;
			textBoxMinimumPrice.IsModified = false;
			textBoxMinimumPrice.Location = new System.Drawing.Point(394, 130);
			textBoxMinimumPrice.MaxLength = 10;
			textBoxMinimumPrice.Name = "textBoxMinimumPrice";
			textBoxMinimumPrice.Size = new System.Drawing.Size(135, 20);
			textBoxMinimumPrice.TabIndex = 10;
			textBoxMinimumPrice.Text = "0.00";
			textBoxMinimumPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSpecialPrice.CustomReportFieldName = "";
			textBoxSpecialPrice.CustomReportKey = "";
			textBoxSpecialPrice.CustomReportValueType = 1;
			textBoxSpecialPrice.IsComboTextBox = false;
			textBoxSpecialPrice.IsModified = false;
			textBoxSpecialPrice.Location = new System.Drawing.Point(123, 130);
			textBoxSpecialPrice.MaxLength = 10;
			textBoxSpecialPrice.Name = "textBoxSpecialPrice";
			textBoxSpecialPrice.Size = new System.Drawing.Size(135, 20);
			textBoxSpecialPrice.TabIndex = 9;
			textBoxSpecialPrice.Text = "0.00";
			textBoxSpecialPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxWholesalePrice.CustomReportFieldName = "";
			textBoxWholesalePrice.CustomReportKey = "";
			textBoxWholesalePrice.CustomReportValueType = 1;
			textBoxWholesalePrice.IsComboTextBox = false;
			textBoxWholesalePrice.IsModified = false;
			textBoxWholesalePrice.Location = new System.Drawing.Point(394, 109);
			textBoxWholesalePrice.MaxLength = 10;
			textBoxWholesalePrice.Name = "textBoxWholesalePrice";
			textBoxWholesalePrice.Size = new System.Drawing.Size(135, 20);
			textBoxWholesalePrice.TabIndex = 8;
			textBoxWholesalePrice.Text = "0.00";
			textBoxWholesalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(123, 281);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(540, 136);
			textBoxNote.TabIndex = 22;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 281);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 77;
			mmLabel4.Text = "Note:";
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(8, 237);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(91, 14);
			ultraFormattedLinkLabel7.TabIndex = 76;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Preferred Vendor:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance4;
			comboBoxPrefVendor.AlwaysInEditMode = true;
			comboBoxPrefVendor.Assigned = false;
			comboBoxPrefVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPrefVendor.CustomReportFieldName = "";
			comboBoxPrefVendor.CustomReportKey = "";
			comboBoxPrefVendor.CustomReportValueType = 1;
			comboBoxPrefVendor.DescriptionTextBox = textBoxPrefVendor;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPrefVendor.DisplayLayout.Appearance = appearance5;
			comboBoxPrefVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPrefVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrefVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxPrefVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPrefVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPrefVendor.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPrefVendor.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxPrefVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPrefVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPrefVendor.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxPrefVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPrefVendor.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrefVendor.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxPrefVendor.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxPrefVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPrefVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxPrefVendor.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxPrefVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPrefVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxPrefVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPrefVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPrefVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPrefVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPrefVendor.Editable = true;
			comboBoxPrefVendor.FilterString = "";
			comboBoxPrefVendor.FilterSysDocID = "";
			comboBoxPrefVendor.HasAll = false;
			comboBoxPrefVendor.HasCustom = false;
			comboBoxPrefVendor.IsDataLoaded = false;
			comboBoxPrefVendor.Location = new System.Drawing.Point(124, 233);
			comboBoxPrefVendor.MaxDropDownItems = 12;
			comboBoxPrefVendor.Name = "comboBoxPrefVendor";
			comboBoxPrefVendor.ShowConsignmentOnly = false;
			comboBoxPrefVendor.ShowQuickAdd = true;
			comboBoxPrefVendor.Size = new System.Drawing.Size(135, 20);
			comboBoxPrefVendor.TabIndex = 19;
			comboBoxPrefVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOrigin.AlwaysInEditMode = true;
			comboBoxOrigin.Assigned = false;
			comboBoxOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOrigin.CustomReportFieldName = "";
			comboBoxOrigin.CustomReportKey = "";
			comboBoxOrigin.CustomReportValueType = 1;
			comboBoxOrigin.DescriptionTextBox = textBoxOrigin;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOrigin.DisplayLayout.Appearance = appearance17;
			comboBoxOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxOrigin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOrigin.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxOrigin.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxOrigin.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOrigin.Editable = true;
			comboBoxOrigin.FilterString = "";
			comboBoxOrigin.HasAllAccount = false;
			comboBoxOrigin.HasCustom = false;
			comboBoxOrigin.IsDataLoaded = false;
			comboBoxOrigin.Location = new System.Drawing.Point(124, 211);
			comboBoxOrigin.MaxDropDownItems = 12;
			comboBoxOrigin.Name = "comboBoxOrigin";
			comboBoxOrigin.ShowInactiveItems = false;
			comboBoxOrigin.ShowQuickAdd = true;
			comboBoxOrigin.Size = new System.Drawing.Size(135, 20);
			comboBoxOrigin.TabIndex = 17;
			comboBoxOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(8, 215);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(37, 14);
			ultraFormattedLinkLabel6.TabIndex = 73;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Origin:";
			appearance29.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance29;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(8, 193);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(36, 14);
			ultraFormattedLinkLabel4.TabIndex = 74;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Brand:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance30;
			comboBoxBrand.AlwaysInEditMode = true;
			comboBoxBrand.Assigned = false;
			comboBoxBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBrand.CustomReportFieldName = "";
			comboBoxBrand.CustomReportKey = "";
			comboBoxBrand.CustomReportValueType = 1;
			comboBoxBrand.DescriptionTextBox = textBoxBrand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBrand.DisplayLayout.Appearance = appearance31;
			comboBoxBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBrand.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBrand.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBrand.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBrand.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxBrand.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxBrand.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBrand.Editable = true;
			comboBoxBrand.FilterString = "";
			comboBoxBrand.HasAllAccount = false;
			comboBoxBrand.HasCustom = false;
			comboBoxBrand.IsDataLoaded = false;
			comboBoxBrand.Location = new System.Drawing.Point(124, 189);
			comboBoxBrand.MaxDropDownItems = 12;
			comboBoxBrand.Name = "comboBoxBrand";
			comboBoxBrand.ShowInactiveItems = false;
			comboBoxBrand.Size = new System.Drawing.Size(135, 20);
			comboBoxBrand.TabIndex = 15;
			comboBoxBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(8, 169);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(72, 14);
			ultraFormattedLinkLabel3.TabIndex = 66;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Manufacturer:";
			appearance43.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance43;
			comboBoxManufacturer.AlwaysInEditMode = true;
			comboBoxManufacturer.Assigned = false;
			comboBoxManufacturer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManufacturer.CustomReportFieldName = "";
			comboBoxManufacturer.CustomReportKey = "";
			comboBoxManufacturer.CustomReportValueType = 1;
			comboBoxManufacturer.DescriptionTextBox = textBoxManufact;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManufacturer.DisplayLayout.Appearance = appearance44;
			comboBoxManufacturer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManufacturer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.GroupByBox.Appearance = appearance45;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManufacturer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance46;
			comboBoxManufacturer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance47.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance47.BackColor2 = System.Drawing.SystemColors.Control;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManufacturer.DisplayLayout.GroupByBox.PromptAppearance = appearance47;
			comboBoxManufacturer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManufacturer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManufacturer.DisplayLayout.Override.ActiveCellAppearance = appearance48;
			appearance49.BackColor = System.Drawing.SystemColors.Highlight;
			appearance49.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManufacturer.DisplayLayout.Override.ActiveRowAppearance = appearance49;
			comboBoxManufacturer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManufacturer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.Override.CardAreaAppearance = appearance50;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			appearance51.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManufacturer.DisplayLayout.Override.CellAppearance = appearance51;
			comboBoxManufacturer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManufacturer.DisplayLayout.Override.CellPadding = 0;
			appearance52.BackColor = System.Drawing.SystemColors.Control;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManufacturer.DisplayLayout.Override.GroupByRowAppearance = appearance52;
			appearance53.TextHAlignAsString = "Left";
			comboBoxManufacturer.DisplayLayout.Override.HeaderAppearance = appearance53;
			comboBoxManufacturer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManufacturer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			comboBoxManufacturer.DisplayLayout.Override.RowAppearance = appearance54;
			comboBoxManufacturer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManufacturer.DisplayLayout.Override.TemplateAddRowAppearance = appearance55;
			comboBoxManufacturer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManufacturer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManufacturer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManufacturer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManufacturer.Editable = true;
			comboBoxManufacturer.FilterString = "";
			comboBoxManufacturer.HasAllAccount = false;
			comboBoxManufacturer.HasCustom = false;
			comboBoxManufacturer.IsDataLoaded = false;
			comboBoxManufacturer.Location = new System.Drawing.Point(124, 167);
			comboBoxManufacturer.MaxDropDownItems = 12;
			comboBoxManufacturer.Name = "comboBoxManufacturer";
			comboBoxManufacturer.ShowInactiveItems = false;
			comboBoxManufacturer.ShowQuickAdd = true;
			comboBoxManufacturer.Size = new System.Drawing.Size(135, 20);
			comboBoxManufacturer.TabIndex = 13;
			comboBoxManufacturer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(264, 63);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel9.TabIndex = 54;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Category:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(8, 84);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(60, 14);
			ultraFormattedLinkLabel8.TabIndex = 53;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Main UOM:";
			appearance57.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance57;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxUOM.AlwaysInEditMode = true;
			comboBoxUOM.Assigned = false;
			comboBoxUOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUOM.CustomReportFieldName = "";
			comboBoxUOM.CustomReportKey = "";
			comboBoxUOM.CustomReportValueType = 1;
			comboBoxUOM.DescriptionTextBox = null;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUOM.DisplayLayout.Appearance = appearance58;
			comboBoxUOM.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUOM.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.GroupByBox.Appearance = appearance59;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.BandLabelAppearance = appearance60;
			comboBoxUOM.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance61.BackColor2 = System.Drawing.SystemColors.Control;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUOM.DisplayLayout.GroupByBox.PromptAppearance = appearance61;
			comboBoxUOM.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUOM.DisplayLayout.MaxRowScrollRegions = 1;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUOM.DisplayLayout.Override.ActiveCellAppearance = appearance62;
			appearance63.BackColor = System.Drawing.SystemColors.Highlight;
			appearance63.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUOM.DisplayLayout.Override.ActiveRowAppearance = appearance63;
			comboBoxUOM.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUOM.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.CardAreaAppearance = appearance64;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUOM.DisplayLayout.Override.CellAppearance = appearance65;
			comboBoxUOM.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUOM.DisplayLayout.Override.CellPadding = 0;
			appearance66.BackColor = System.Drawing.SystemColors.Control;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUOM.DisplayLayout.Override.GroupByRowAppearance = appearance66;
			appearance67.TextHAlignAsString = "Left";
			comboBoxUOM.DisplayLayout.Override.HeaderAppearance = appearance67;
			comboBoxUOM.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUOM.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			comboBoxUOM.DisplayLayout.Override.RowAppearance = appearance68;
			comboBoxUOM.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUOM.DisplayLayout.Override.TemplateAddRowAppearance = appearance69;
			comboBoxUOM.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUOM.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUOM.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUOM.Editable = true;
			comboBoxUOM.FilterString = "";
			comboBoxUOM.HasAllAccount = false;
			comboBoxUOM.HasCustom = false;
			comboBoxUOM.IsDataLoaded = false;
			comboBoxUOM.Location = new System.Drawing.Point(123, 83);
			comboBoxUOM.MaxDropDownItems = 12;
			comboBoxUOM.Name = "comboBoxUOM";
			comboBoxUOM.ShowInactiveItems = false;
			comboBoxUOM.ShowQuickAdd = true;
			comboBoxUOM.Size = new System.Drawing.Size(136, 20);
			comboBoxUOM.TabIndex = 5;
			comboBoxUOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxQuantityPerUnit.CustomReportFieldName = "";
			textBoxQuantityPerUnit.CustomReportKey = "";
			textBoxQuantityPerUnit.CustomReportValueType = 1;
			textBoxQuantityPerUnit.IsComboTextBox = false;
			textBoxQuantityPerUnit.IsModified = false;
			textBoxQuantityPerUnit.Location = new System.Drawing.Point(394, 82);
			textBoxQuantityPerUnit.MaxLength = 10;
			textBoxQuantityPerUnit.Name = "textBoxQuantityPerUnit";
			textBoxQuantityPerUnit.Size = new System.Drawing.Size(135, 20);
			textBoxQuantityPerUnit.TabIndex = 6;
			textBoxQuantityPerUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxCategory.AlwaysInEditMode = true;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance70;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance71.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance71;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance72;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance73.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance73.BackColor2 = System.Drawing.SystemColors.Control;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance73;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance74;
			appearance75.BackColor = System.Drawing.SystemColors.Highlight;
			appearance75.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance75;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance76;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			appearance77.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance77;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance78.BackColor = System.Drawing.SystemColors.Control;
			appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance78.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance78;
			appearance79.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance79;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance80;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance81.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance81;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(394, 60);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.Size = new System.Drawing.Size(135, 20);
			comboBoxCategory.TabIndex = 4;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelStandardPrice.AutoSize = true;
			labelStandardPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelStandardPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelStandardPrice.IsFieldHeader = false;
			labelStandardPrice.IsRequired = false;
			labelStandardPrice.Location = new System.Drawing.Point(8, 111);
			labelStandardPrice.Name = "labelStandardPrice";
			labelStandardPrice.PenWidth = 1f;
			labelStandardPrice.ShowBorder = false;
			labelStandardPrice.Size = new System.Drawing.Size(65, 13);
			labelStandardPrice.TabIndex = 15;
			labelStandardPrice.Text = "Unit Price 1:";
			textBoxStandardPrice.CustomReportFieldName = "";
			textBoxStandardPrice.CustomReportKey = "";
			textBoxStandardPrice.CustomReportValueType = 1;
			textBoxStandardPrice.IsComboTextBox = false;
			textBoxStandardPrice.IsModified = false;
			textBoxStandardPrice.Location = new System.Drawing.Point(122, 109);
			textBoxStandardPrice.MaxLength = 10;
			textBoxStandardPrice.Name = "textBoxStandardPrice";
			textBoxStandardPrice.Size = new System.Drawing.Size(136, 20);
			textBoxStandardPrice.TabIndex = 7;
			textBoxStandardPrice.Text = "0.00";
			textBoxStandardPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(316, 11);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(8, 63);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(84, 13);
			mmLabel8.TabIndex = 26;
			mmLabel8.Text = "Costing Method:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 12);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(68, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Item Code:";
			comboBoxCostMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCostMethod.FormattingEnabled = true;
			comboBoxCostMethod.Items.AddRange(new object[2]
			{
				"Average",
				"FIFO"
			});
			comboBoxCostMethod.Location = new System.Drawing.Point(123, 60);
			comboBoxCostMethod.Name = "comboBoxCostMethod";
			comboBoxCostMethod.SelectedID = 0;
			comboBoxCostMethod.Size = new System.Drawing.Size(136, 21);
			comboBoxCostMethod.TabIndex = 3;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 32);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(103, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Item Description:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(123, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(187, 20);
			textBoxCode.TabIndex = 0;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(123, 31);
			textBoxName.MaxLength = 255;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(371, 20);
			textBoxName.TabIndex = 2;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(264, 84);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(67, 13);
			mmLabel6.TabIndex = 19;
			mmLabel6.Text = "Qty Per Unit:";
			tabPageDetails.Controls.Add(buttonAddItem);
			tabPageDetails.Controls.Add(panelDimension1);
			tabPageDetails.Controls.Add(panelDimension2);
			tabPageDetails.Controls.Add(panelDimension3);
			tabPageDetails.Controls.Add(label4);
			tabPageDetails.Controls.Add(matrixTemplateComboBox);
			tabPageDetails.Controls.Add(buttonComponents);
			tabPageDetails.Controls.Add(dataGridAttributes);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(725, 437);
			tabPageDetails.Paint += new System.Windows.Forms.PaintEventHandler(tabPageDetails_Paint);
			buttonAddItem.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAddItem.BackColor = System.Drawing.Color.Silver;
			buttonAddItem.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAddItem.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAddItem.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAddItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAddItem.Location = new System.Drawing.Point(538, 171);
			buttonAddItem.Name = "buttonAddItem";
			buttonAddItem.Size = new System.Drawing.Size(128, 24);
			buttonAddItem.TabIndex = 5;
			buttonAddItem.Text = "Add Existing Item...";
			buttonAddItem.UseVisualStyleBackColor = false;
			buttonAddItem.Click += new System.EventHandler(buttonAddItem_Click);
			panelDimension1.Controls.Add(dataGridDimension1);
			panelDimension1.Controls.Add(label1);
			panelDimension1.Controls.Add(comboBoxDimension1);
			panelDimension1.Location = new System.Drawing.Point(0, 32);
			panelDimension1.Name = "panelDimension1";
			panelDimension1.Size = new System.Drawing.Size(177, 163);
			panelDimension1.TabIndex = 1;
			dataGridDimension1.AllowAddNew = false;
			dataGridDimension1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridDimension1.ContextMenuStrip = contextMenuStripDimension;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension1.DisplayLayout.Appearance = appearance82;
			dataGridDimension1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.GroupByBox.Appearance = appearance83;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance84;
			dataGridDimension1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance85.BackColor2 = System.Drawing.SystemColors.Control;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension1.DisplayLayout.GroupByBox.PromptAppearance = appearance85;
			dataGridDimension1.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension1.DisplayLayout.Override.ActiveCellAppearance = appearance86;
			appearance87.BackColor = System.Drawing.SystemColors.Highlight;
			appearance87.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension1.DisplayLayout.Override.ActiveRowAppearance = appearance87;
			dataGridDimension1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.Override.CardAreaAppearance = appearance88;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			appearance89.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension1.DisplayLayout.Override.CellAppearance = appearance89;
			dataGridDimension1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension1.DisplayLayout.Override.CellPadding = 0;
			appearance90.BackColor = System.Drawing.SystemColors.Control;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.Override.GroupByRowAppearance = appearance90;
			appearance91.TextHAlignAsString = "Left";
			dataGridDimension1.DisplayLayout.Override.HeaderAppearance = appearance91;
			dataGridDimension1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension1.DisplayLayout.Override.RowAppearance = appearance92;
			dataGridDimension1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension1.DisplayLayout.Override.TemplateAddRowAppearance = appearance93;
			dataGridDimension1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension1.ExitEditModeOnLeave = false;
			dataGridDimension1.IncludeLotItems = false;
			dataGridDimension1.LoadLayoutFailed = false;
			dataGridDimension1.Location = new System.Drawing.Point(11, 46);
			dataGridDimension1.Name = "dataGridDimension1";
			dataGridDimension1.ShowClearMenu = true;
			dataGridDimension1.ShowDeleteMenu = true;
			dataGridDimension1.ShowInsertMenu = true;
			dataGridDimension1.ShowMoveRowsMenu = true;
			dataGridDimension1.Size = new System.Drawing.Size(160, 105);
			dataGridDimension1.TabIndex = 1;
			dataGridDimension1.Text = "dataEntryGrid1";
			contextMenuStripDimension.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				toolStripMenuItem1,
				toolStripMenuItem2,
				toolStripSeparator4,
				deleteToolStripMenuItem1
			});
			contextMenuStripDimension.Name = "contextMenuStripComponents";
			contextMenuStripDimension.Size = new System.Drawing.Size(139, 76);
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItem1.Text = "Move Up";
			toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new System.Drawing.Size(138, 22);
			toolStripMenuItem2.Text = "Move Down";
			toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(135, 6);
			deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
			deleteToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
			deleteToolStripMenuItem1.Text = "Delete";
			deleteToolStripMenuItem1.Click += new System.EventHandler(deleteToolStripMenuItem1_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(10, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(68, 13);
			label1.TabIndex = 21;
			label1.Text = "Dimension 1:";
			comboBoxDimension1.AlwaysInEditMode = true;
			comboBoxDimension1.Assigned = false;
			comboBoxDimension1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension1.CustomReportFieldName = "";
			comboBoxDimension1.CustomReportKey = "";
			comboBoxDimension1.CustomReportValueType = 1;
			comboBoxDimension1.DescriptionTextBox = null;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension1.DisplayLayout.Appearance = appearance94;
			comboBoxDimension1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance95.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.GroupByBox.Appearance = appearance95;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance96;
			comboBoxDimension1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance97.BackColor2 = System.Drawing.SystemColors.Control;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension1.DisplayLayout.GroupByBox.PromptAppearance = appearance97;
			comboBoxDimension1.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension1.DisplayLayout.Override.ActiveCellAppearance = appearance98;
			appearance99.BackColor = System.Drawing.SystemColors.Highlight;
			appearance99.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension1.DisplayLayout.Override.ActiveRowAppearance = appearance99;
			comboBoxDimension1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.Override.CardAreaAppearance = appearance100;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			appearance101.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension1.DisplayLayout.Override.CellAppearance = appearance101;
			comboBoxDimension1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension1.DisplayLayout.Override.CellPadding = 0;
			appearance102.BackColor = System.Drawing.SystemColors.Control;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.Override.GroupByRowAppearance = appearance102;
			appearance103.TextHAlignAsString = "Left";
			comboBoxDimension1.DisplayLayout.Override.HeaderAppearance = appearance103;
			comboBoxDimension1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension1.DisplayLayout.Override.RowAppearance = appearance104;
			comboBoxDimension1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension1.DisplayLayout.Override.TemplateAddRowAppearance = appearance105;
			comboBoxDimension1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension1.Editable = true;
			comboBoxDimension1.FilterString = "";
			comboBoxDimension1.HasAllAccount = false;
			comboBoxDimension1.HasCustom = false;
			comboBoxDimension1.IsDataLoaded = false;
			comboBoxDimension1.Location = new System.Drawing.Point(13, 20);
			comboBoxDimension1.MaxDropDownItems = 12;
			comboBoxDimension1.Name = "comboBoxDimension1";
			comboBoxDimension1.ShowInactiveItems = false;
			comboBoxDimension1.ShowQuickAdd = true;
			comboBoxDimension1.Size = new System.Drawing.Size(158, 20);
			comboBoxDimension1.TabIndex = 0;
			comboBoxDimension1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelDimension2.Controls.Add(dataGridDimension2);
			panelDimension2.Controls.Add(label2);
			panelDimension2.Controls.Add(comboBoxDimension2);
			panelDimension2.Enabled = false;
			panelDimension2.Location = new System.Drawing.Point(183, 33);
			panelDimension2.Name = "panelDimension2";
			panelDimension2.Size = new System.Drawing.Size(174, 162);
			panelDimension2.TabIndex = 2;
			dataGridDimension2.AllowAddNew = false;
			dataGridDimension2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridDimension2.ContextMenuStrip = contextMenuStripDimension;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension2.DisplayLayout.Appearance = appearance106;
			dataGridDimension2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance107.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.GroupByBox.Appearance = appearance107;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance108;
			dataGridDimension2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance109.BackColor2 = System.Drawing.SystemColors.Control;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension2.DisplayLayout.GroupByBox.PromptAppearance = appearance109;
			dataGridDimension2.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension2.DisplayLayout.Override.ActiveCellAppearance = appearance110;
			appearance111.BackColor = System.Drawing.SystemColors.Highlight;
			appearance111.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension2.DisplayLayout.Override.ActiveRowAppearance = appearance111;
			dataGridDimension2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.Override.CardAreaAppearance = appearance112;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			appearance113.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension2.DisplayLayout.Override.CellAppearance = appearance113;
			dataGridDimension2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension2.DisplayLayout.Override.CellPadding = 0;
			appearance114.BackColor = System.Drawing.SystemColors.Control;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.Override.GroupByRowAppearance = appearance114;
			appearance115.TextHAlignAsString = "Left";
			dataGridDimension2.DisplayLayout.Override.HeaderAppearance = appearance115;
			dataGridDimension2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension2.DisplayLayout.Override.RowAppearance = appearance116;
			dataGridDimension2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension2.DisplayLayout.Override.TemplateAddRowAppearance = appearance117;
			dataGridDimension2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension2.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension2.ExitEditModeOnLeave = false;
			dataGridDimension2.IncludeLotItems = false;
			dataGridDimension2.LoadLayoutFailed = false;
			dataGridDimension2.Location = new System.Drawing.Point(9, 45);
			dataGridDimension2.Name = "dataGridDimension2";
			dataGridDimension2.ShowClearMenu = true;
			dataGridDimension2.ShowDeleteMenu = true;
			dataGridDimension2.ShowInsertMenu = true;
			dataGridDimension2.ShowMoveRowsMenu = true;
			dataGridDimension2.Size = new System.Drawing.Size(160, 105);
			dataGridDimension2.TabIndex = 1;
			dataGridDimension2.Text = "dataEntryGrid1";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 5);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(68, 13);
			label2.TabIndex = 21;
			label2.Text = "Dimension 2:";
			comboBoxDimension2.AlwaysInEditMode = true;
			comboBoxDimension2.Assigned = false;
			comboBoxDimension2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension2.CustomReportFieldName = "";
			comboBoxDimension2.CustomReportKey = "";
			comboBoxDimension2.CustomReportValueType = 1;
			comboBoxDimension2.DescriptionTextBox = null;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension2.DisplayLayout.Appearance = appearance118;
			comboBoxDimension2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance119.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.GroupByBox.Appearance = appearance119;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance120;
			comboBoxDimension2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance121.BackColor2 = System.Drawing.SystemColors.Control;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension2.DisplayLayout.GroupByBox.PromptAppearance = appearance121;
			comboBoxDimension2.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension2.DisplayLayout.Override.ActiveCellAppearance = appearance122;
			appearance123.BackColor = System.Drawing.SystemColors.Highlight;
			appearance123.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension2.DisplayLayout.Override.ActiveRowAppearance = appearance123;
			comboBoxDimension2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.Override.CardAreaAppearance = appearance124;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			appearance125.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension2.DisplayLayout.Override.CellAppearance = appearance125;
			comboBoxDimension2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension2.DisplayLayout.Override.CellPadding = 0;
			appearance126.BackColor = System.Drawing.SystemColors.Control;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.Override.GroupByRowAppearance = appearance126;
			appearance127.TextHAlignAsString = "Left";
			comboBoxDimension2.DisplayLayout.Override.HeaderAppearance = appearance127;
			comboBoxDimension2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension2.DisplayLayout.Override.RowAppearance = appearance128;
			comboBoxDimension2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension2.DisplayLayout.Override.TemplateAddRowAppearance = appearance129;
			comboBoxDimension2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension2.Editable = true;
			comboBoxDimension2.FilterString = "";
			comboBoxDimension2.HasAllAccount = false;
			comboBoxDimension2.HasCustom = false;
			comboBoxDimension2.IsDataLoaded = false;
			comboBoxDimension2.Location = new System.Drawing.Point(9, 20);
			comboBoxDimension2.MaxDropDownItems = 12;
			comboBoxDimension2.Name = "comboBoxDimension2";
			comboBoxDimension2.ShowInactiveItems = false;
			comboBoxDimension2.ShowQuickAdd = true;
			comboBoxDimension2.Size = new System.Drawing.Size(160, 20);
			comboBoxDimension2.TabIndex = 0;
			comboBoxDimension2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelDimension3.Controls.Add(dataGridDimension3);
			panelDimension3.Controls.Add(label3);
			panelDimension3.Controls.Add(comboBoxDimension3);
			panelDimension3.Enabled = false;
			panelDimension3.Location = new System.Drawing.Point(359, 33);
			panelDimension3.Name = "panelDimension3";
			panelDimension3.Size = new System.Drawing.Size(173, 164);
			panelDimension3.TabIndex = 3;
			dataGridDimension3.AllowAddNew = false;
			dataGridDimension3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridDimension3.ContextMenuStrip = contextMenuStripDimension;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension3.DisplayLayout.Appearance = appearance130;
			dataGridDimension3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			dataGridDimension3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension3.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			dataGridDimension3.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension3.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension3.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			dataGridDimension3.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension3.DisplayLayout.Override.CellAppearance = appearance137;
			dataGridDimension3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension3.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			dataGridDimension3.DisplayLayout.Override.HeaderAppearance = appearance139;
			dataGridDimension3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension3.DisplayLayout.Override.RowAppearance = appearance140;
			dataGridDimension3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension3.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			dataGridDimension3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension3.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension3.ExitEditModeOnLeave = false;
			dataGridDimension3.IncludeLotItems = false;
			dataGridDimension3.LoadLayoutFailed = false;
			dataGridDimension3.Location = new System.Drawing.Point(6, 45);
			dataGridDimension3.Name = "dataGridDimension3";
			dataGridDimension3.ShowClearMenu = true;
			dataGridDimension3.ShowDeleteMenu = true;
			dataGridDimension3.ShowInsertMenu = true;
			dataGridDimension3.ShowMoveRowsMenu = true;
			dataGridDimension3.Size = new System.Drawing.Size(158, 104);
			dataGridDimension3.TabIndex = 1;
			dataGridDimension3.Text = "dataEntryGrid1";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 6);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(68, 13);
			label3.TabIndex = 21;
			label3.Text = "Dimension 3:";
			comboBoxDimension3.AlwaysInEditMode = true;
			comboBoxDimension3.Assigned = false;
			comboBoxDimension3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension3.CustomReportFieldName = "";
			comboBoxDimension3.CustomReportKey = "";
			comboBoxDimension3.CustomReportValueType = 1;
			comboBoxDimension3.DescriptionTextBox = null;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension3.DisplayLayout.Appearance = appearance142;
			comboBoxDimension3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance143.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.GroupByBox.Appearance = appearance143;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance144;
			comboBoxDimension3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance145.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance145.BackColor2 = System.Drawing.SystemColors.Control;
			appearance145.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension3.DisplayLayout.GroupByBox.PromptAppearance = appearance145;
			comboBoxDimension3.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			appearance146.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension3.DisplayLayout.Override.ActiveCellAppearance = appearance146;
			appearance147.BackColor = System.Drawing.SystemColors.Highlight;
			appearance147.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension3.DisplayLayout.Override.ActiveRowAppearance = appearance147;
			comboBoxDimension3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.Override.CardAreaAppearance = appearance148;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			appearance149.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension3.DisplayLayout.Override.CellAppearance = appearance149;
			comboBoxDimension3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension3.DisplayLayout.Override.CellPadding = 0;
			appearance150.BackColor = System.Drawing.SystemColors.Control;
			appearance150.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance150.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance150.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.Override.GroupByRowAppearance = appearance150;
			appearance151.TextHAlignAsString = "Left";
			comboBoxDimension3.DisplayLayout.Override.HeaderAppearance = appearance151;
			comboBoxDimension3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension3.DisplayLayout.Override.RowAppearance = appearance152;
			comboBoxDimension3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance153.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension3.DisplayLayout.Override.TemplateAddRowAppearance = appearance153;
			comboBoxDimension3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension3.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension3.Editable = true;
			comboBoxDimension3.FilterString = "";
			comboBoxDimension3.HasAllAccount = false;
			comboBoxDimension3.HasCustom = false;
			comboBoxDimension3.IsDataLoaded = false;
			comboBoxDimension3.Location = new System.Drawing.Point(6, 21);
			comboBoxDimension3.MaxDropDownItems = 12;
			comboBoxDimension3.Name = "comboBoxDimension3";
			comboBoxDimension3.ShowInactiveItems = false;
			comboBoxDimension3.ShowQuickAdd = true;
			comboBoxDimension3.Size = new System.Drawing.Size(158, 20);
			comboBoxDimension3.TabIndex = 0;
			comboBoxDimension3.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(10, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(54, 13);
			label4.TabIndex = 23;
			label4.Text = "Template:";
			matrixTemplateComboBox.AlwaysInEditMode = true;
			matrixTemplateComboBox.Assigned = false;
			matrixTemplateComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			matrixTemplateComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			matrixTemplateComboBox.CustomReportFieldName = "";
			matrixTemplateComboBox.CustomReportKey = "";
			matrixTemplateComboBox.CustomReportValueType = 1;
			matrixTemplateComboBox.DescriptionTextBox = null;
			appearance154.BackColor = System.Drawing.SystemColors.Window;
			appearance154.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			matrixTemplateComboBox.DisplayLayout.Appearance = appearance154;
			matrixTemplateComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			matrixTemplateComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance155.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance155.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance155.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance155.BorderColor = System.Drawing.SystemColors.Window;
			matrixTemplateComboBox.DisplayLayout.GroupByBox.Appearance = appearance155;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			matrixTemplateComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance156;
			matrixTemplateComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance157.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance157.BackColor2 = System.Drawing.SystemColors.Control;
			appearance157.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance157.ForeColor = System.Drawing.SystemColors.GrayText;
			matrixTemplateComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance157;
			matrixTemplateComboBox.DisplayLayout.MaxColScrollRegions = 1;
			matrixTemplateComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance158.BackColor = System.Drawing.SystemColors.Window;
			appearance158.ForeColor = System.Drawing.SystemColors.ControlText;
			matrixTemplateComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance158;
			appearance159.BackColor = System.Drawing.SystemColors.Highlight;
			appearance159.ForeColor = System.Drawing.SystemColors.HighlightText;
			matrixTemplateComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance159;
			matrixTemplateComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			matrixTemplateComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance160.BackColor = System.Drawing.SystemColors.Window;
			matrixTemplateComboBox.DisplayLayout.Override.CardAreaAppearance = appearance160;
			appearance161.BorderColor = System.Drawing.Color.Silver;
			appearance161.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			matrixTemplateComboBox.DisplayLayout.Override.CellAppearance = appearance161;
			matrixTemplateComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			matrixTemplateComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance162.BackColor = System.Drawing.SystemColors.Control;
			appearance162.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance162.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance162.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance162.BorderColor = System.Drawing.SystemColors.Window;
			matrixTemplateComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance162;
			appearance163.TextHAlignAsString = "Left";
			matrixTemplateComboBox.DisplayLayout.Override.HeaderAppearance = appearance163;
			matrixTemplateComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			matrixTemplateComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance164.BackColor = System.Drawing.SystemColors.Window;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			matrixTemplateComboBox.DisplayLayout.Override.RowAppearance = appearance164;
			matrixTemplateComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance165.BackColor = System.Drawing.SystemColors.ControlLight;
			matrixTemplateComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance165;
			matrixTemplateComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			matrixTemplateComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			matrixTemplateComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			matrixTemplateComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			matrixTemplateComboBox.Editable = true;
			matrixTemplateComboBox.FilterString = "";
			matrixTemplateComboBox.HasAllAccount = false;
			matrixTemplateComboBox.HasCustom = false;
			matrixTemplateComboBox.IsDataLoaded = false;
			matrixTemplateComboBox.Location = new System.Drawing.Point(68, 10);
			matrixTemplateComboBox.MaxDropDownItems = 12;
			matrixTemplateComboBox.Name = "matrixTemplateComboBox";
			matrixTemplateComboBox.ShowInactiveItems = false;
			matrixTemplateComboBox.ShowQuickAdd = true;
			matrixTemplateComboBox.Size = new System.Drawing.Size(161, 20);
			matrixTemplateComboBox.TabIndex = 0;
			matrixTemplateComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonComponents.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonComponents.BackColor = System.Drawing.Color.Silver;
			buttonComponents.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonComponents.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonComponents.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonComponents.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonComponents.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonComponents.Location = new System.Drawing.Point(538, 142);
			buttonComponents.Name = "buttonComponents";
			buttonComponents.Size = new System.Drawing.Size(128, 24);
			buttonComponents.TabIndex = 4;
			buttonComponents.Text = "Create Components";
			buttonComponents.UseVisualStyleBackColor = false;
			buttonComponents.Click += new System.EventHandler(buttonComponents_Click);
			dataGridAttributes.AllowAddNew = false;
			dataGridAttributes.AllowCustomizeHeaders = true;
			dataGridAttributes.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridAttributes.ContextMenuStrip = contextMenuStripComponents;
			appearance166.BackColor = System.Drawing.SystemColors.Window;
			appearance166.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridAttributes.DisplayLayout.Appearance = appearance166;
			dataGridAttributes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridAttributes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance167.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance167.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance167.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance167.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.GroupByBox.Appearance = appearance167;
			appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance168;
			dataGridAttributes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance169.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance169.BackColor2 = System.Drawing.SystemColors.Control;
			appearance169.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance169.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAttributes.DisplayLayout.GroupByBox.PromptAppearance = appearance169;
			dataGridAttributes.DisplayLayout.MaxColScrollRegions = 1;
			dataGridAttributes.DisplayLayout.MaxRowScrollRegions = 1;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			appearance170.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridAttributes.DisplayLayout.Override.ActiveCellAppearance = appearance170;
			appearance171.BackColor = System.Drawing.SystemColors.Highlight;
			appearance171.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridAttributes.DisplayLayout.Override.ActiveRowAppearance = appearance171;
			dataGridAttributes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridAttributes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridAttributes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.CardAreaAppearance = appearance172;
			appearance173.BorderColor = System.Drawing.Color.Silver;
			appearance173.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridAttributes.DisplayLayout.Override.CellAppearance = appearance173;
			dataGridAttributes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridAttributes.DisplayLayout.Override.CellPadding = 0;
			appearance174.BackColor = System.Drawing.SystemColors.Control;
			appearance174.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance174.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance174.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance174.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAttributes.DisplayLayout.Override.GroupByRowAppearance = appearance174;
			appearance175.TextHAlignAsString = "Left";
			dataGridAttributes.DisplayLayout.Override.HeaderAppearance = appearance175;
			dataGridAttributes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridAttributes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			dataGridAttributes.DisplayLayout.Override.RowAppearance = appearance176;
			dataGridAttributes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance177.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridAttributes.DisplayLayout.Override.TemplateAddRowAppearance = appearance177;
			dataGridAttributes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridAttributes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridAttributes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridAttributes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridAttributes.ExitEditModeOnLeave = false;
			dataGridAttributes.IncludeLotItems = false;
			dataGridAttributes.LoadLayoutFailed = false;
			dataGridAttributes.Location = new System.Drawing.Point(10, 201);
			dataGridAttributes.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridAttributes.Name = "dataGridAttributes";
			dataGridAttributes.ShowClearMenu = true;
			dataGridAttributes.ShowDeleteMenu = true;
			dataGridAttributes.ShowInsertMenu = true;
			dataGridAttributes.ShowMoveRowsMenu = true;
			dataGridAttributes.Size = new System.Drawing.Size(705, 232);
			dataGridAttributes.TabIndex = 6;
			dataGridAttributes.Text = "dataEntryGrid1";
			contextMenuStripComponents.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				toolStripMenuItemRemoveComponent
			});
			contextMenuStripComponents.Name = "contextMenuStripComponents";
			contextMenuStripComponents.Size = new System.Drawing.Size(145, 26);
			toolStripMenuItemRemoveComponent.Name = "toolStripMenuItemRemoveComponent";
			toolStripMenuItemRemoveComponent.Size = new System.Drawing.Size(144, 22);
			toolStripMenuItemRemoveComponent.Text = "Remove Item";
			toolStripMenuItemRemoveComponent.Click += new System.EventHandler(toolStripMenuItem3_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
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
				toolStripButtonShowPicture,
				toolStripButtonQuantity,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(729, 31);
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
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			toolStripButtonQuantity.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonQuantity.Image");
			toolStripButtonQuantity.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonQuantity.Name = "toolStripButtonQuantity";
			toolStripButtonQuantity.Size = new System.Drawing.Size(127, 28);
			toolStripButtonQuantity.Text = "Quantity Onhand";
			toolStripButtonQuantity.Click += new System.EventHandler(toolStripButtonQuantity_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 514);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(729, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(729, 1);
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
			xpButton1.Location = new System.Drawing.Point(619, 8);
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 60);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(729, 454);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance178.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance178;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Matrix";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(725, 431);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MaximumSize = new System.Drawing.Size(0, 29);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(729, 29);
			panel1.TabIndex = 315;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(32, 8);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 3;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(729, 554);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(panel1);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MatrixProductDetailsForm";
			Text = "Matrix Item Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrefVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManufacturer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUOM).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			panelDimension1.ResumeLayout(false);
			panelDimension1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension1).EndInit();
			contextMenuStripDimension.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxDimension1).EndInit();
			panelDimension2.ResumeLayout(false);
			panelDimension2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension2).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension2).EndInit();
			panelDimension3.ResumeLayout(false);
			panelDimension3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension3).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension3).EndInit();
			((System.ComponentModel.ISupportInitialize)matrixTemplateComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridAttributes).EndInit();
			contextMenuStripComponents.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
