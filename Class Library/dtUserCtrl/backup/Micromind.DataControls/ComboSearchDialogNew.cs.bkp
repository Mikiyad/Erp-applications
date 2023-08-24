using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ComboSearchDialogNew : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private bool loadItemDetails = CompanyPreferences.LoadItemFeatures;

		private bool canAccessCost = true;

		private string selectedItem = "";

		private string selectedProvider = "";

		private string selectedVendor = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private TextBox textBoxSearch;

		private Label label1;

		private MMSListGrid dataGridItems;

		private Panel panelItemdetails;

		private ProductPhotoViewer productPhotoViewer;

		private Panel panel1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label labelCost;

		private Label labelStock;

		private Label labelCountry;

		private Label labelManufacturer;

		private Label labelBrand;

		private Label labelCategory;

		private Label labelUnit;

		private DataEntryGrid dataGridPurchase;

		private DataEntryGrid dataGridSales;

		private GroupBox groupBox2;

		private GroupBox groupBoxPurchase;

		private TextBox textBoxVendorName;

		private TextBox textBoxCustomerName;

		private customersFlatComboBox comboBoxCustomers;

		private vendorsFlatComboBox comboBoxVendors;

		private Label labelvendor;

		private Label labelcustomer;

		private GroupBox groupBoxAll;

		private Label label37;

		private DataEntryGrid dataGridMultiLocation;

		private Label labelUnitPrice;

		private NumberTextBox textBoxCost;

		private Label labelLastCost;

		private NumberTextBox textBoxLastCost;

		private MMLabel labelMinPrice;

		private MMLabel labelSpecialPrice;

		private MMLabel labelWholesalePrice;

		private MMLabel labelStandardPrice;

		private UnitPriceTextBox textBoxMinimumPrice;

		private UnitPriceTextBox textBoxSpecialPrice;

		private UnitPriceTextBox textBoxWholesalePrice;

		private UnitPriceTextBox textBoxStandardPrice;

		private Label labelFactor;

		private TextBox textBoxUnitFactor;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
				if (value && dataGridItems.DataSource != null)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				}
			}
		}

		public string SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				selectedItem = value;
			}
		}

		public string SelectedProvider
		{
			get
			{
				return selectedProvider;
			}
			set
			{
				selectedProvider = value;
			}
		}

		public string SelectedVendor
		{
			get
			{
				return selectedVendor;
			}
			set
			{
				selectedVendor = value;
			}
		}

		public UltraGridRow SelectedRow
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.ActiveRow != null)
				{
					return dataGridItems.ActiveRow;
				}
				return null;
			}
		}

		public List<UltraGridRow> SelectedRows
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				List<UltraGridRow> list = new List<UltraGridRow>();
				if (IsMultiSelect)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["C"].Value.ToString() == "True")
						{
							list.Add(row);
						}
					}
				}
				if (list.Count == 0 && dataGridItems.ActiveRow != null)
				{
					list.Add(dataGridItems.ActiveRow);
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public DataSet DataSource
		{
			get
			{
				if (dataGridItems.DataSource == null || dataGridItems.DataSource.ToString() == "")
				{
					DataSet dataSet = new DataSet();
					return DataSource = Factory.ProductSystem.GetProducts();
				}
				return (DataSet)dataGridItems.DataSource;
			}
			set
			{
				if (value != null)
				{
					if (value != null && value.Tables.Count > 0 && !value.Tables[0].Columns.Contains("SearchColumn"))
					{
						DataTable dataTable = value.Tables[0];
						dataTable.Columns.Add("SearchColumn");
						string text = "";
						foreach (DataRow row in dataTable.Rows)
						{
							text = "";
							foreach (DataColumn column in dataTable.Columns)
							{
								if (!(column.ColumnName == "SearchColumn"))
								{
									text = text + row[column].ToString() + " ";
								}
							}
							row["SearchColumn"] = text;
						}
					}
					dataGridItems.DataSource = value;
					dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
					dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
					foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (column2.Key.ToLower() != "name" && column2.Key.ToLower() != "code" && column2.Key.ToLower() != "description")
						{
							column2.Hidden = true;
						}
					}
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("ItemType"))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = false;
						ValueList valueList = new ValueList();
						valueList.ValueListItems.Add(0, "None");
						valueList.ValueListItems.Add(1, "Inventory");
						valueList.ValueListItems.Add(2, "Non-Inventory");
						valueList.ValueListItems.Add(3, "Service");
						valueList.ValueListItems.Add(4, "Discount");
						valueList.ValueListItems.Add(5, "Consignment");
						valueList.ValueListItems.Add(6, "Matrix");
						valueList.ValueListItems.Add(7, "Assembly");
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Header.Caption = "Item Type";
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ValueList = valueList;
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Width = 100;
					}
					if (IsMultiSelect && !dataGridItems.DisplayLayout.Bands[0].Columns.Exists("C"))
					{
						UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
						ultraGridColumn.DataType = typeof(bool);
						ultraGridColumn.CellActivation = Activation.AllowEdit;
						ultraGridColumn.CellClickAction = CellClickAction.Edit;
						ultraGridColumn.CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
						ultraGridColumn.Width = 18;
						ultraGridColumn.MinWidth = 18;
						ultraGridColumn.MaxWidth = 18;
						ultraGridColumn.LockedWidth = true;
						ultraGridColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
					}
				}
			}
		}

		public event EventHandler ValidateSelection;

		public ComboSearchDialogNew()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			if (!loadItemDetails)
			{
				groupBoxAll.Visible = false;
				panelItemdetails.Visible = false;
				labelvendor.Visible = false;
				labelcustomer.Visible = false;
				textBoxCustomerName.Visible = false;
				textBoxVendorName.Visible = false;
				comboBoxCustomers.Visible = false;
				comboBoxVendors.Visible = false;
				base.Size = new Size(525, 584);
			}
			else
			{
				comboBoxCustomers.SelectedID = selectedProvider;
			}
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				if (dataGridItems.ActiveRow != null)
				{
					int visibleIndex = dataGridItems.ActiveRow.VisibleIndex;
					if (visibleIndex < dataGridItems.Rows.VisibleRowCount)
					{
						dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex + 1);
					}
					e.SuppressKeyPress = true;
				}
			}
			else if (e.KeyCode == Keys.Up && dataGridItems.ActiveRow != null)
			{
				int visibleIndex2 = dataGridItems.ActiveRow.VisibleIndex;
				if (visibleIndex2 > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex2 - 1);
				}
				e.SuppressKeyPress = true;
			}
		}

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			dataGridItems.ApplyUIDesign();
			dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			labelStandardPrice.Text = CompanyPreferences.UnitPrice1Title + ":";
			labelWholesalePrice.Text = CompanyPreferences.UnitPrice2Title + ":";
			labelSpecialPrice.Text = CompanyPreferences.UnitPrice3Title + ":";
			dataGridMultiLocation.SetupUI();
			SetupMultilocationGrid();
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			dataGridSales.SetupUI();
			dataGridPurchase.SetupUI();
			SetupSalesGrid();
			SetupPurchaseGrid();
			if (SelectedItem != "")
			{
				textBoxSearch.Text = SelectedItem.Trim();
			}
			textBoxSearch.Focus();
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (SelectedRow != null)
			{
				return SelectedRow.Cells[codeColumnName].Value.ToString();
			}
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			if (DataSource != null && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridItems.DataSource = DataSource;
				dataGridItems.BeginUpdate();
				if (dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = textBoxSearch.Text;
				dataGridItems.EndUpdate();
				if (dataGridItems.Rows.VisibleRowCount > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(0);
				}
				if (textBoxSearch.Text == "")
				{
					textBoxCost.Clear();
					textBoxStandardPrice.Clear();
					textBoxWholesalePrice.Clear();
					textBoxSpecialPrice.Clear();
					textBoxMinimumPrice.Clear();
					textBoxLastCost.Clear();
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!isMultiSelect || dataGridItems.Rows.Count <= 1)
			{
				SelectItem();
			}
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
			{
				ErrorHelper.InformationMessage("Please select an item first.");
				return;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridItems.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
		}

		private void SetupGrid()
		{
		}

		private void ClearForm()
		{
			labelUnit.Text = "";
			labelCategory.Text = "";
			labelBrand.Text = "";
			labelManufacturer.Text = "";
			labelCountry.Text = "";
			labelStock.Text = "";
			labelCost.Text = "";
			textBoxUnitFactor.Text = "";
			TextBox textBox = textBoxUnitFactor;
			bool visible = labelFactor.Visible = false;
			textBox.Visible = visible;
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			if (selectedItem == null || selectedItem == "")
			{
				if (textBoxSearch.Text == "")
				{
					dataGridItems.ActiveRow = null;
					dataGridItems.DataSource = "";
					dataGridItems.DisplayLayout.Bands[0].Columns[0].Header.Caption = "";
				}
				else
				{
					if (dataGridItems.ActiveRow == null)
					{
						return;
					}
					ClearForm();
					if (dataGridItems.ActiveRow.Cells["Code"].Value == null || !(dataGridItems.ActiveRow.Cells["Code"].Value.ToString() != "") || !loadItemDetails)
					{
						return;
					}
					string text = dataGridItems.ActiveRow.Cells["Code"].Value.ToString();
					productPhotoViewer.ShowImage(text, dataGridItems.Left + 220, dataGridItems.Top - 30);
					DataSet dataSet = new DataSet();
					dataSet = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(text));
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = dataSet.Tables[0].Rows[0];
						labelUnit.Text = dataRow["UnitName"].ToString();
						labelCategory.Text = dataRow["CategoryName"].ToString();
						labelBrand.Text = dataRow["BrandName"].ToString();
						labelManufacturer.Text = dataRow["ManufacturerName"].ToString();
						labelCountry.Text = dataRow["CountryName"].ToString();
						labelStock.Text = dataRow["Quantity"].ToString();
						textBoxCost.Text = decimal.Parse(dataRow["StandardCost"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxStandardPrice.Text = decimal.Parse(dataRow["UnitPrice1"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxWholesalePrice.Text = decimal.Parse(dataRow["UnitPrice2"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxSpecialPrice.Text = decimal.Parse(dataRow["UnitPrice3"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxMinimumPrice.Text = decimal.Parse(dataRow["MinPrice"].ToString()).ToString(Format.TotalAmountFormat);
						if (!string.IsNullOrEmpty(dataRow["LAST COST"].ToString()))
						{
							textBoxLastCost.Text = decimal.Parse(dataRow["LAST COST"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxLastCost.Text = 0.0.ToString();
						}
						labelCost.Text = dataRow["AverageCost"].ToString();
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
						{
							UnitPriceTextBox unitPriceTextBox = textBoxStandardPrice;
							bool visible = labelStandardPrice.Visible = false;
							unitPriceTextBox.Visible = visible;
						}
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
						{
							UnitPriceTextBox unitPriceTextBox2 = textBoxWholesalePrice;
							bool visible = labelWholesalePrice.Visible = false;
							unitPriceTextBox2.Visible = visible;
						}
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
						{
							UnitPriceTextBox unitPriceTextBox3 = textBoxSpecialPrice;
							bool visible = labelSpecialPrice.Visible = false;
							unitPriceTextBox3.Visible = visible;
						}
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
						{
							UnitPriceTextBox unitPriceTextBox4 = textBoxMinimumPrice;
							bool visible = labelMinPrice.Visible = false;
							unitPriceTextBox4.Visible = visible;
						}
						if (!string.IsNullOrEmpty(labelUnit.Text))
						{
							DataSet productUnits = Factory.ProductSystem.GetProductUnits(text);
							string text2 = "";
							string text3 = "";
							string text4 = "";
							if (productUnits != null)
							{
								foreach (DataRow row in productUnits.Tables[0].Rows)
								{
									if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
									{
										TextBox textBox = textBoxUnitFactor;
										textBox.Text = textBox.Text + ";" + Environment.NewLine;
									}
									text2 = row["Factor"].ToString();
									text3 = row["FactorType"].ToString();
									text4 = row["UnitID"].ToString();
									if (text3 == "M")
									{
										TextBox textBox2 = textBoxUnitFactor;
										textBox2.Text = textBox2.Text + "1 " + labelUnit.Text + " = " + text2 + " " + text4;
									}
									else
									{
										TextBox textBox2 = textBoxUnitFactor;
										textBox2.Text = textBox2.Text + "1 " + text4 + " = " + text2 + " " + labelUnit.Text;
									}
									if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
									{
										labelFactor.Visible = true;
										textBoxUnitFactor.Visible = true;
									}
								}
							}
						}
						if (!canAccessCost)
						{
							Label label = labelCost;
							bool visible = this.label2.Visible = false;
							label.Visible = visible;
							Label label2 = labelUnitPrice;
							NumberTextBox numberTextBox = textBoxCost;
							Label label3 = labelLastCost;
							bool flag7 = textBoxLastCost.Visible = false;
							bool flag9 = label3.Visible = flag7;
							visible = (numberTextBox.Visible = flag9);
							label2.Visible = visible;
							groupBoxPurchase.Visible = false;
						}
					}
					(dataGridSales.DataSource as DataTable).Rows.Clear();
					(dataGridPurchase.DataSource as DataTable).Rows.Clear();
					DisplayPurchaseDetails(text);
					DisplaySalesDetails(text);
					DisplayProductUnderLocation(text);
				}
			}
			else
			{
				if (dataGridItems.ActiveRow == null)
				{
					return;
				}
				ClearForm();
				if (dataGridItems.ActiveRow.Cells["Code"].Value == null || !(dataGridItems.ActiveRow.Cells["Code"].Value.ToString() != "") || !loadItemDetails)
				{
					return;
				}
				string text5 = dataGridItems.ActiveRow.Cells["Code"].Value.ToString();
				productPhotoViewer.ShowImage(text5, dataGridItems.Left + 220, dataGridItems.Top - 30);
				DataSet dataSet2 = new DataSet();
				dataSet2 = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(text5));
				if (dataSet2.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow2 = dataSet2.Tables[0].Rows[0];
					labelUnit.Text = dataRow2["UnitName"].ToString();
					labelCategory.Text = dataRow2["CategoryName"].ToString();
					labelBrand.Text = dataRow2["BrandName"].ToString();
					labelManufacturer.Text = dataRow2["ManufacturerName"].ToString();
					labelCountry.Text = dataRow2["CountryName"].ToString();
					labelStock.Text = dataRow2["Quantity"].ToString();
					labelCost.Text = dataRow2["AverageCost"].ToString();
					if (!string.IsNullOrEmpty(dataRow2["StandardCost"].ToString()))
					{
						textBoxCost.Text = decimal.Parse(dataRow2["StandardCost"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow2["UnitPrice1"].ToString()))
					{
						textBoxStandardPrice.Text = decimal.Parse(dataRow2["UnitPrice1"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow2["UnitPrice2"].ToString()))
					{
						textBoxWholesalePrice.Text = decimal.Parse(dataRow2["UnitPrice2"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow2["UnitPrice3"].ToString()))
					{
						textBoxSpecialPrice.Text = decimal.Parse(dataRow2["UnitPrice3"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow2["MinPrice"].ToString()))
					{
						textBoxMinimumPrice.Text = decimal.Parse(dataRow2["MinPrice"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow2["LAST COST"].ToString()))
					{
						textBoxLastCost.Text = decimal.Parse(dataRow2["LAST COST"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxLastCost.Text = 0.0.ToString();
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
					{
						UnitPriceTextBox unitPriceTextBox5 = textBoxStandardPrice;
						bool visible = labelStandardPrice.Visible = false;
						unitPriceTextBox5.Visible = visible;
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
					{
						UnitPriceTextBox unitPriceTextBox6 = textBoxWholesalePrice;
						bool visible = labelWholesalePrice.Visible = false;
						unitPriceTextBox6.Visible = visible;
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
					{
						UnitPriceTextBox unitPriceTextBox7 = textBoxSpecialPrice;
						bool visible = labelSpecialPrice.Visible = false;
						unitPriceTextBox7.Visible = visible;
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
					{
						UnitPriceTextBox unitPriceTextBox8 = textBoxMinimumPrice;
						bool visible = labelMinPrice.Visible = false;
						unitPriceTextBox8.Visible = visible;
					}
					if (!string.IsNullOrEmpty(labelUnit.Text))
					{
						DataSet productUnits2 = Factory.ProductSystem.GetProductUnits(text5);
						string text6 = "";
						string text7 = "";
						string text8 = "";
						if (productUnits2 != null)
						{
							foreach (DataRow row2 in productUnits2.Tables[0].Rows)
							{
								if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
								{
									TextBox textBox3 = textBoxUnitFactor;
									textBox3.Text = textBox3.Text + ";" + Environment.NewLine;
								}
								text6 = row2["Factor"].ToString();
								text7 = row2["FactorType"].ToString();
								text8 = row2["UnitID"].ToString();
								if (text7 == "M")
								{
									TextBox textBox2 = textBoxUnitFactor;
									textBox2.Text = textBox2.Text + "1 " + labelUnit.Text + " = " + text6 + " " + text8;
								}
								else
								{
									TextBox textBox2 = textBoxUnitFactor;
									textBox2.Text = textBox2.Text + "1 " + text8 + " = " + text6 + " " + labelUnit.Text;
								}
								if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
								{
									labelFactor.Visible = true;
									textBoxUnitFactor.Visible = true;
								}
							}
						}
					}
					if (!canAccessCost)
					{
						Label label4 = labelCost;
						bool visible = this.label2.Visible = false;
						label4.Visible = visible;
						Label label5 = labelUnitPrice;
						NumberTextBox numberTextBox2 = textBoxCost;
						Label label6 = labelLastCost;
						bool flag7 = textBoxLastCost.Visible = false;
						bool flag9 = label6.Visible = flag7;
						visible = (numberTextBox2.Visible = flag9);
						label5.Visible = visible;
						groupBoxPurchase.Visible = false;
					}
				}
				(dataGridSales.DataSource as DataTable).Rows.Clear();
				(dataGridPurchase.DataSource as DataTable).Rows.Clear();
				DisplayPurchaseDetails(text5);
				DisplaySalesDetails(text5);
				DisplayProductUnderLocation(text5);
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
		}

		private void SetupSalesGrid()
		{
			dataGridSales.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Date");
			dataTable.Columns.Add("Customer");
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			dataGridSales.DataSource = dataTable;
			dataGridSales.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[3].Width = 50 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[4].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[5].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[6].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[7].Width = 20 * dataGridSales.Width / 100;
		}

		private void SetupPurchaseGrid()
		{
			dataGridPurchase.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Date");
			dataTable.Columns.Add("Supplier");
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			dataGridPurchase.DataSource = dataTable;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[3].Width = 50 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[4].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[5].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[6].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[7].Width = 20 * dataGridPurchase.Width / 100;
		}

		private void SetupMultilocationGrid()
		{
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Station Name");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataGridMultiLocation.DataSource = dataTable;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[0].Width = 50 * dataGridMultiLocation.Width / 100;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[1].Width = 50 * dataGridMultiLocation.Width / 100;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = -99999999m;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
		}

		private void DisplaySalesDetails(string SelectedID)
		{
			DataSet dataSet = null;
			string defaultLocationID = Global.DefaultLocationID;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail(comboBoxCustomers.SelectedID, SelectedID, defaultLocationID);
			new DataSet();
			DataTable dataTable = dataGridSales.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Customer"] = row["CustomerName"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplaySalesDetailsCustomer(string CustomerID, string productID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail(comboBoxCustomers.SelectedID, productID);
			new DataSet();
			DataTable dataTable = dataGridSales.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Customer"] = row["CustomerName"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayPurchaseDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail(comboBoxVendors.SelectedID, SelectedID);
			new DataSet();
			DataTable dataTable = dataGridPurchase.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Supplier"] = row["VendorName"];
					dataRow2["Quantity"] = row["Quantity"];
					if (canAccessCost)
					{
						dataRow2["Price"] = row["UnitPrice"];
						dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					}
					else
					{
						UltraGridColumn ultraGridColumn = dataGridPurchase.DisplayLayout.Bands[0].Columns["Amount"];
						bool hidden = dataGridPurchase.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
						ultraGridColumn.Hidden = hidden;
					}
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayPurchaseDetails(string vendorID, string productID)
		{
			DataSet dataSet = null;
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail(vendorID, productID);
			new DataSet();
			DataTable dataTable = dataGridPurchase.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Supplier"] = row["VendorName"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayProductUnderLocation(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.ProductSystem.GetProductAvailability(SelectedID, "");
			new DataSet();
			DataTable dataTable = dataGridMultiLocation.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Station Name"] = row["Location Name"];
					dataRow2["Quantity"] = row["Onhand"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void comboBoxCustomers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedItem != "")
			{
				DisplaySalesDetails(dataGridItems.ActiveRow.Cells["Code"].Value.ToString());
			}
		}

		private void comboBoxVendors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedItem != "")
			{
				DisplayPurchaseDetails(dataGridItems.ActiveRow.Cells["Code"].Value.ToString());
			}
		}

		private void ComboSearchDialogNew_Activated(object sender, EventArgs e)
		{
			if (SelectedProvider != "" && SelectedProvider != null)
			{
				comboBoxCustomers.SelectedID = SelectedProvider;
			}
			else if (SelectedVendor != "" && SelectedVendor != null)
			{
				comboBoxVendors.SelectedID = SelectedVendor;
			}
		}

		private void ComboSearchDialogNew_Load(object sender, EventArgs e)
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
			panelButtons = new System.Windows.Forms.Panel();
			labelLastCost = new System.Windows.Forms.Label();
			textBoxLastCost = new Micromind.UISupport.NumberTextBox();
			labelMinPrice = new Micromind.UISupport.MMLabel();
			labelSpecialPrice = new Micromind.UISupport.MMLabel();
			labelWholesalePrice = new Micromind.UISupport.MMLabel();
			labelStandardPrice = new Micromind.UISupport.MMLabel();
			textBoxMinimumPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxSpecialPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxWholesalePrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxStandardPrice = new Micromind.UISupport.UnitPriceTextBox();
			labelUnitPrice = new System.Windows.Forms.Label();
			textBoxCost = new Micromind.UISupport.NumberTextBox();
			label37 = new System.Windows.Forms.Label();
			groupBox2 = new System.Windows.Forms.GroupBox();
			labelvendor = new System.Windows.Forms.Label();
			labelcustomer = new System.Windows.Forms.Label();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			groupBoxPurchase = new System.Windows.Forms.GroupBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			groupBoxAll = new System.Windows.Forms.GroupBox();
			textBoxSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			panelItemdetails = new System.Windows.Forms.Panel();
			panel1 = new System.Windows.Forms.Panel();
			labelFactor = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			labelCost = new System.Windows.Forms.Label();
			labelStock = new System.Windows.Forms.Label();
			labelCountry = new System.Windows.Forms.Label();
			labelManufacturer = new System.Windows.Forms.Label();
			labelBrand = new System.Windows.Forms.Label();
			labelCategory = new System.Windows.Forms.Label();
			labelUnit = new System.Windows.Forms.Label();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			dataGridMultiLocation = new Micromind.DataControls.DataEntryGrid();
			dataGridSales = new Micromind.DataControls.DataEntryGrid();
			comboBoxVendors = new Micromind.DataControls.vendorsFlatComboBox();
			dataGridPurchase = new Micromind.DataControls.DataEntryGrid();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			comboBoxCustomers = new Micromind.DataControls.customersFlatComboBox();
			textBoxUnitFactor = new System.Windows.Forms.TextBox();
			panelButtons.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBoxPurchase.SuspendLayout();
			panelItemdetails.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridSales).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendors).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomers).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(labelLastCost);
			panelButtons.Controls.Add(textBoxLastCost);
			panelButtons.Controls.Add(labelMinPrice);
			panelButtons.Controls.Add(labelSpecialPrice);
			panelButtons.Controls.Add(labelWholesalePrice);
			panelButtons.Controls.Add(labelStandardPrice);
			panelButtons.Controls.Add(textBoxMinimumPrice);
			panelButtons.Controls.Add(textBoxSpecialPrice);
			panelButtons.Controls.Add(textBoxWholesalePrice);
			panelButtons.Controls.Add(textBoxStandardPrice);
			panelButtons.Controls.Add(labelUnitPrice);
			panelButtons.Controls.Add(textBoxCost);
			panelButtons.Controls.Add(label37);
			panelButtons.Controls.Add(dataGridMultiLocation);
			panelButtons.Controls.Add(groupBox2);
			panelButtons.Controls.Add(labelvendor);
			panelButtons.Controls.Add(labelcustomer);
			panelButtons.Controls.Add(comboBoxVendors);
			panelButtons.Controls.Add(groupBoxPurchase);
			panelButtons.Controls.Add(textBoxVendorName);
			panelButtons.Controls.Add(textBoxCustomerName);
			panelButtons.Controls.Add(dataGridItems);
			panelButtons.Controls.Add(comboBoxCustomers);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(groupBoxAll);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 50);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(927, 544);
			panelButtons.TabIndex = 3;
			labelLastCost.AutoSize = true;
			labelLastCost.Location = new System.Drawing.Point(237, 270);
			labelLastCost.Name = "labelLastCost";
			labelLastCost.Size = new System.Drawing.Size(102, 13);
			labelLastCost.TabIndex = 222;
			labelLastCost.Text = "Last Purchase Cost:";
			textBoxLastCost.AllowDecimal = true;
			textBoxLastCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastCost.CustomReportFieldName = "";
			textBoxLastCost.CustomReportKey = "";
			textBoxLastCost.CustomReportValueType = 1;
			textBoxLastCost.IsComboTextBox = false;
			textBoxLastCost.IsModified = false;
			textBoxLastCost.Location = new System.Drawing.Point(344, 267);
			textBoxLastCost.MaxLength = 15;
			textBoxLastCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLastCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLastCost.Name = "textBoxLastCost";
			textBoxLastCost.NullText = "0";
			textBoxLastCost.ReadOnly = true;
			textBoxLastCost.Size = new System.Drawing.Size(120, 20);
			textBoxLastCost.TabIndex = 221;
			textBoxLastCost.Text = "0.00";
			textBoxLastCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelMinPrice.AutoSize = true;
			labelMinPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelMinPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelMinPrice.IsFieldHeader = false;
			labelMinPrice.IsRequired = false;
			labelMinPrice.Location = new System.Drawing.Point(23, 359);
			labelMinPrice.Name = "labelMinPrice";
			labelMinPrice.PenWidth = 1f;
			labelMinPrice.ShowBorder = false;
			labelMinPrice.Size = new System.Drawing.Size(78, 13);
			labelMinPrice.TabIndex = 213;
			labelMinPrice.Text = "Minimum Price:";
			labelSpecialPrice.AutoSize = true;
			labelSpecialPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSpecialPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelSpecialPrice.IsFieldHeader = false;
			labelSpecialPrice.IsRequired = false;
			labelSpecialPrice.Location = new System.Drawing.Point(23, 337);
			labelSpecialPrice.Name = "labelSpecialPrice";
			labelSpecialPrice.PenWidth = 1f;
			labelSpecialPrice.ShowBorder = false;
			labelSpecialPrice.Size = new System.Drawing.Size(65, 13);
			labelSpecialPrice.TabIndex = 214;
			labelSpecialPrice.Text = "Unit Price 3:";
			labelWholesalePrice.AutoSize = true;
			labelWholesalePrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelWholesalePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelWholesalePrice.IsFieldHeader = false;
			labelWholesalePrice.IsRequired = false;
			labelWholesalePrice.Location = new System.Drawing.Point(23, 315);
			labelWholesalePrice.Name = "labelWholesalePrice";
			labelWholesalePrice.PenWidth = 1f;
			labelWholesalePrice.ShowBorder = false;
			labelWholesalePrice.Size = new System.Drawing.Size(65, 13);
			labelWholesalePrice.TabIndex = 215;
			labelWholesalePrice.Text = "Unit Price 2:";
			labelStandardPrice.AutoSize = true;
			labelStandardPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelStandardPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelStandardPrice.IsFieldHeader = false;
			labelStandardPrice.IsRequired = false;
			labelStandardPrice.Location = new System.Drawing.Point(23, 293);
			labelStandardPrice.Name = "labelStandardPrice";
			labelStandardPrice.PenWidth = 1f;
			labelStandardPrice.ShowBorder = false;
			labelStandardPrice.Size = new System.Drawing.Size(65, 13);
			labelStandardPrice.TabIndex = 216;
			labelStandardPrice.Text = "Unit Price 1:";
			textBoxMinimumPrice.CustomReportFieldName = "";
			textBoxMinimumPrice.CustomReportKey = "";
			textBoxMinimumPrice.CustomReportValueType = 1;
			textBoxMinimumPrice.IsComboTextBox = false;
			textBoxMinimumPrice.IsModified = false;
			textBoxMinimumPrice.Location = new System.Drawing.Point(115, 356);
			textBoxMinimumPrice.MaxLength = 10;
			textBoxMinimumPrice.Name = "textBoxMinimumPrice";
			textBoxMinimumPrice.Size = new System.Drawing.Size(120, 20);
			textBoxMinimumPrice.TabIndex = 220;
			textBoxMinimumPrice.Text = "0.00";
			textBoxMinimumPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSpecialPrice.CustomReportFieldName = "";
			textBoxSpecialPrice.CustomReportKey = "";
			textBoxSpecialPrice.CustomReportValueType = 1;
			textBoxSpecialPrice.IsComboTextBox = false;
			textBoxSpecialPrice.IsModified = false;
			textBoxSpecialPrice.Location = new System.Drawing.Point(115, 334);
			textBoxSpecialPrice.MaxLength = 10;
			textBoxSpecialPrice.Name = "textBoxSpecialPrice";
			textBoxSpecialPrice.Size = new System.Drawing.Size(120, 20);
			textBoxSpecialPrice.TabIndex = 219;
			textBoxSpecialPrice.Text = "0.00";
			textBoxSpecialPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxWholesalePrice.CustomReportFieldName = "";
			textBoxWholesalePrice.CustomReportKey = "";
			textBoxWholesalePrice.CustomReportValueType = 1;
			textBoxWholesalePrice.IsComboTextBox = false;
			textBoxWholesalePrice.IsModified = false;
			textBoxWholesalePrice.Location = new System.Drawing.Point(115, 312);
			textBoxWholesalePrice.MaxLength = 10;
			textBoxWholesalePrice.Name = "textBoxWholesalePrice";
			textBoxWholesalePrice.Size = new System.Drawing.Size(120, 20);
			textBoxWholesalePrice.TabIndex = 218;
			textBoxWholesalePrice.Text = "0.00";
			textBoxWholesalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxStandardPrice.CustomReportFieldName = "";
			textBoxStandardPrice.CustomReportKey = "";
			textBoxStandardPrice.CustomReportValueType = 1;
			textBoxStandardPrice.IsComboTextBox = false;
			textBoxStandardPrice.IsModified = false;
			textBoxStandardPrice.Location = new System.Drawing.Point(115, 290);
			textBoxStandardPrice.MaxLength = 10;
			textBoxStandardPrice.Name = "textBoxStandardPrice";
			textBoxStandardPrice.Size = new System.Drawing.Size(120, 20);
			textBoxStandardPrice.TabIndex = 217;
			textBoxStandardPrice.Text = "0.00";
			textBoxStandardPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelUnitPrice.AutoSize = true;
			labelUnitPrice.Location = new System.Drawing.Point(23, 270);
			labelUnitPrice.Name = "labelUnitPrice";
			labelUnitPrice.Size = new System.Drawing.Size(31, 13);
			labelUnitPrice.TabIndex = 212;
			labelUnitPrice.Text = "Cost:";
			textBoxCost.AllowDecimal = true;
			textBoxCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCost.CustomReportFieldName = "";
			textBoxCost.CustomReportKey = "";
			textBoxCost.CustomReportValueType = 1;
			textBoxCost.IsComboTextBox = false;
			textBoxCost.IsModified = false;
			textBoxCost.Location = new System.Drawing.Point(115, 267);
			textBoxCost.MaxLength = 15;
			textBoxCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxCost.Name = "textBoxCost";
			textBoxCost.NullText = "0";
			textBoxCost.ReadOnly = true;
			textBoxCost.Size = new System.Drawing.Size(120, 20);
			textBoxCost.TabIndex = 211;
			textBoxCost.Text = "0.00";
			textBoxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label37.Location = new System.Drawing.Point(699, 280);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(105, 13);
			label37.TabIndex = 210;
			label37.Text = "MULTI LOCATION";
			groupBox2.Controls.Add(dataGridSales);
			groupBox2.Location = new System.Drawing.Point(19, 403);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(445, 128);
			groupBox2.TabIndex = 124;
			groupBox2.TabStop = false;
			groupBox2.Text = "Sale Details";
			labelvendor.AutoSize = true;
			labelvendor.Location = new System.Drawing.Point(513, 43);
			labelvendor.Name = "labelvendor";
			labelvendor.Size = new System.Drawing.Size(41, 13);
			labelvendor.TabIndex = 20;
			labelvendor.Text = "Vendor";
			labelcustomer.AutoSize = true;
			labelcustomer.Location = new System.Drawing.Point(509, 20);
			labelcustomer.Name = "labelcustomer";
			labelcustomer.Size = new System.Drawing.Size(51, 13);
			labelcustomer.TabIndex = 19;
			labelcustomer.Text = "Customer";
			textBoxVendorName.Location = new System.Drawing.Point(735, 39);
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(178, 20);
			textBoxVendorName.TabIndex = 17;
			groupBoxPurchase.Controls.Add(dataGridPurchase);
			groupBoxPurchase.Location = new System.Drawing.Point(482, 403);
			groupBoxPurchase.Name = "groupBoxPurchase";
			groupBoxPurchase.Size = new System.Drawing.Size(430, 132);
			groupBoxPurchase.TabIndex = 123;
			groupBoxPurchase.TabStop = false;
			groupBoxPurchase.Text = "Purchase Details";
			textBoxCustomerName.Location = new System.Drawing.Point(735, 17);
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(177, 20);
			textBoxCustomerName.TabIndex = 16;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(712, 512);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Visible = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(927, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(817, 512);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Visible = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			groupBoxAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			groupBoxAll.Location = new System.Drawing.Point(502, 1);
			groupBoxAll.Name = "groupBoxAll";
			groupBoxAll.Size = new System.Drawing.Size(418, 68);
			groupBoxAll.TabIndex = 21;
			groupBoxAll.TabStop = false;
			textBoxSearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxSearch.Location = new System.Drawing.Point(48, 13);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(394, 20);
			textBoxSearch.TabIndex = 4;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 5;
			label1.Text = "Find:";
			panelItemdetails.Controls.Add(panel1);
			panelItemdetails.Controls.Add(productPhotoViewer);
			panelItemdetails.Location = new System.Drawing.Point(502, 120);
			panelItemdetails.Name = "panelItemdetails";
			panelItemdetails.Size = new System.Drawing.Size(422, 207);
			panelItemdetails.TabIndex = 7;
			panel1.Controls.Add(textBoxUnitFactor);
			panel1.Controls.Add(labelFactor);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(labelCost);
			panel1.Controls.Add(labelStock);
			panel1.Controls.Add(labelCountry);
			panel1.Controls.Add(labelManufacturer);
			panel1.Controls.Add(labelBrand);
			panel1.Controls.Add(labelCategory);
			panel1.Controls.Add(labelUnit);
			panel1.Location = new System.Drawing.Point(7, 4);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(221, 200);
			panel1.TabIndex = 122;
			labelFactor.AutoSize = true;
			labelFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelFactor.Location = new System.Drawing.Point(7, 159);
			labelFactor.Name = "labelFactor";
			labelFactor.Size = new System.Drawing.Size(78, 13);
			labelFactor.TabIndex = 17;
			labelFactor.Text = "Unit Factor :";
			labelFactor.Visible = false;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(6, 181);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 15;
			label2.Text = "Cost :";
			label2.Visible = false;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(6, 136);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(93, 13);
			label3.TabIndex = 14;
			label3.Text = "Stock In Hand:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(6, 111);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(58, 13);
			label4.TabIndex = 13;
			label4.Text = "Country :";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(6, 85);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(90, 13);
			label5.TabIndex = 12;
			label5.Text = "Manufacturer :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(6, 59);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(48, 13);
			label6.TabIndex = 11;
			label6.Text = "Brand :";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(6, 35);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(65, 13);
			label7.TabIndex = 10;
			label7.Text = "Category :";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(6, 8);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(69, 13);
			label8.TabIndex = 9;
			label8.Text = "Main Unit :";
			labelCost.AutoSize = true;
			labelCost.Location = new System.Drawing.Point(72, 181);
			labelCost.Name = "labelCost";
			labelCost.Size = new System.Drawing.Size(10, 13);
			labelCost.TabIndex = 7;
			labelCost.Text = "t";
			labelCost.Visible = false;
			labelStock.AutoSize = true;
			labelStock.Location = new System.Drawing.Point(99, 136);
			labelStock.Name = "labelStock";
			labelStock.Size = new System.Drawing.Size(10, 13);
			labelStock.TabIndex = 6;
			labelStock.Text = "t";
			labelCountry.AutoSize = true;
			labelCountry.Location = new System.Drawing.Point(72, 111);
			labelCountry.Name = "labelCountry";
			labelCountry.Size = new System.Drawing.Size(10, 13);
			labelCountry.TabIndex = 5;
			labelCountry.Text = "t";
			labelManufacturer.AutoSize = true;
			labelManufacturer.Location = new System.Drawing.Point(97, 86);
			labelManufacturer.Name = "labelManufacturer";
			labelManufacturer.Size = new System.Drawing.Size(10, 13);
			labelManufacturer.TabIndex = 4;
			labelManufacturer.Text = "t";
			labelBrand.AutoSize = true;
			labelBrand.Location = new System.Drawing.Point(72, 59);
			labelBrand.Name = "labelBrand";
			labelBrand.Size = new System.Drawing.Size(10, 13);
			labelBrand.TabIndex = 3;
			labelBrand.Text = "t";
			labelCategory.AutoSize = true;
			labelCategory.Location = new System.Drawing.Point(72, 35);
			labelCategory.Name = "labelCategory";
			labelCategory.Size = new System.Drawing.Size(10, 13);
			labelCategory.TabIndex = 2;
			labelCategory.Text = "t";
			labelUnit.AutoSize = true;
			labelUnit.Location = new System.Drawing.Point(72, 8);
			labelUnit.Name = "labelUnit";
			labelUnit.Size = new System.Drawing.Size(10, 13);
			labelUnit.TabIndex = 1;
			labelUnit.Text = "t";
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(233, 3);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 250);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 121;
			productPhotoViewer.Visible = false;
			dataGridMultiLocation.AllowAddNew = false;
			dataGridMultiLocation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridMultiLocation.DisplayLayout.Appearance = appearance;
			dataGridMultiLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridMultiLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridMultiLocation.DisplayLayout.MaxColScrollRegions = 1;
			dataGridMultiLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridMultiLocation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridMultiLocation.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridMultiLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridMultiLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridMultiLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridMultiLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridMultiLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridMultiLocation.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridMultiLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridMultiLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridMultiLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridMultiLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridMultiLocation.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridMultiLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridMultiLocation.ExitEditModeOnLeave = false;
			dataGridMultiLocation.IncludeLotItems = false;
			dataGridMultiLocation.LoadLayoutFailed = false;
			dataGridMultiLocation.Location = new System.Drawing.Point(584, 296);
			dataGridMultiLocation.Name = "dataGridMultiLocation";
			dataGridMultiLocation.ShowClearMenu = true;
			dataGridMultiLocation.ShowDeleteMenu = true;
			dataGridMultiLocation.ShowInsertMenu = true;
			dataGridMultiLocation.ShowMoveRowsMenu = true;
			dataGridMultiLocation.Size = new System.Drawing.Size(328, 107);
			dataGridMultiLocation.TabIndex = 209;
			dataGridMultiLocation.Text = "dataEntryGrid3";
			dataGridSales.AllowAddNew = false;
			dataGridSales.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSales.DisplayLayout.Appearance = appearance13;
			dataGridSales.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSales.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridSales.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridSales.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSales.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSales.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSales.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridSales.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSales.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSales.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSales.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridSales.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSales.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridSales.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridSales.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSales.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridSales.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridSales.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSales.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridSales.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSales.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSales.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridSales.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSales.ExitEditModeOnLeave = false;
			dataGridSales.IncludeLotItems = false;
			dataGridSales.LoadLayoutFailed = false;
			dataGridSales.Location = new System.Drawing.Point(4, 13);
			dataGridSales.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridSales.Name = "dataGridSales";
			dataGridSales.ShowClearMenu = true;
			dataGridSales.ShowDeleteMenu = true;
			dataGridSales.ShowInsertMenu = true;
			dataGridSales.ShowMoveRowsMenu = true;
			dataGridSales.Size = new System.Drawing.Size(435, 104);
			dataGridSales.TabIndex = 123;
			dataGridSales.Text = "dataEntryGrid1";
			comboBoxVendors.Assigned = false;
			comboBoxVendors.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVendors.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendors.CustomReportFieldName = "";
			comboBoxVendors.CustomReportKey = "";
			comboBoxVendors.CustomReportValueType = 1;
			comboBoxVendors.DescriptionTextBox = textBoxVendorName;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendors.DisplayLayout.Appearance = appearance25;
			comboBoxVendors.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendors.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendors.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendors.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxVendors.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendors.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxVendors.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendors.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendors.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendors.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxVendors.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendors.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendors.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendors.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxVendors.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendors.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendors.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxVendors.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxVendors.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendors.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendors.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxVendors.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendors.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxVendors.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendors.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendors.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendors.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendors.Editable = true;
			comboBoxVendors.FilterString = "";
			comboBoxVendors.FilterSysDocID = "";
			comboBoxVendors.HasAll = false;
			comboBoxVendors.HasCustom = false;
			comboBoxVendors.IsDataLoaded = false;
			comboBoxVendors.Location = new System.Drawing.Point(564, 39);
			comboBoxVendors.MaxDropDownItems = 12;
			comboBoxVendors.Name = "comboBoxVendors";
			comboBoxVendors.ShowConsignmentOnly = false;
			comboBoxVendors.ShowQuickAdd = true;
			comboBoxVendors.Size = new System.Drawing.Size(166, 20);
			comboBoxVendors.TabIndex = 18;
			comboBoxVendors.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendors.SelectedIndexChanged += new System.EventHandler(comboBoxVendors_SelectedIndexChanged);
			dataGridPurchase.AllowAddNew = false;
			dataGridPurchase.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPurchase.DisplayLayout.Appearance = appearance37;
			dataGridPurchase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPurchase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridPurchase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridPurchase.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPurchase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPurchase.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPurchase.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridPurchase.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPurchase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPurchase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPurchase.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridPurchase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPurchase.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridPurchase.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridPurchase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPurchase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridPurchase.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridPurchase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPurchase.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridPurchase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPurchase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPurchase.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridPurchase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPurchase.ExitEditModeOnLeave = false;
			dataGridPurchase.IncludeLotItems = false;
			dataGridPurchase.LoadLayoutFailed = false;
			dataGridPurchase.Location = new System.Drawing.Point(4, 14);
			dataGridPurchase.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridPurchase.Name = "dataGridPurchase";
			dataGridPurchase.ShowClearMenu = true;
			dataGridPurchase.ShowDeleteMenu = true;
			dataGridPurchase.ShowInsertMenu = true;
			dataGridPurchase.ShowMoveRowsMenu = true;
			dataGridPurchase.Size = new System.Drawing.Size(420, 106);
			dataGridPurchase.TabIndex = 3;
			dataGridPurchase.Text = "dataEntryGrid1";
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance49;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(10, 3);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(486, 252);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.AfterRowActivate += new System.EventHandler(dataGridItems_AfterRowActivate);
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			comboBoxCustomers.Assigned = false;
			comboBoxCustomers.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomers.CustomReportFieldName = "";
			comboBoxCustomers.CustomReportKey = "";
			comboBoxCustomers.CustomReportValueType = 1;
			comboBoxCustomers.DescriptionTextBox = textBoxCustomerName;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomers.DisplayLayout.Appearance = appearance61;
			comboBoxCustomers.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomers.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomers.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxCustomers.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomers.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxCustomers.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomers.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomers.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomers.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxCustomers.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomers.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomers.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxCustomers.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomers.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxCustomers.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxCustomers.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomers.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomers.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxCustomers.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomers.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxCustomers.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomers.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomers.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomers.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomers.Editable = true;
			comboBoxCustomers.FilterString = "";
			comboBoxCustomers.FilterSysDocID = "";
			comboBoxCustomers.HasAll = false;
			comboBoxCustomers.HasCustom = false;
			comboBoxCustomers.IsDataLoaded = false;
			comboBoxCustomers.Location = new System.Drawing.Point(564, 16);
			comboBoxCustomers.MaxDropDownItems = 12;
			comboBoxCustomers.Name = "comboBoxCustomers";
			comboBoxCustomers.ShowConsignmentOnly = false;
			comboBoxCustomers.ShowInactive = false;
			comboBoxCustomers.ShowLPOCustomersOnly = false;
			comboBoxCustomers.ShowPROCustomersOnly = false;
			comboBoxCustomers.ShowQuickAdd = true;
			comboBoxCustomers.Size = new System.Drawing.Size(166, 20);
			comboBoxCustomers.TabIndex = 15;
			comboBoxCustomers.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCustomers.SelectedIndexChanged += new System.EventHandler(comboBoxCustomers_SelectedIndexChanged);
			textBoxUnitFactor.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxUnitFactor.Location = new System.Drawing.Point(89, 158);
			textBoxUnitFactor.Multiline = true;
			textBoxUnitFactor.Name = "textBoxUnitFactor";
			textBoxUnitFactor.ReadOnly = true;
			textBoxUnitFactor.Size = new System.Drawing.Size(129, 38);
			textBoxUnitFactor.TabIndex = 18;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(927, 594);
			base.Controls.Add(panelItemdetails);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ComboSearchDialogNew";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Item";
			base.Activated += new System.EventHandler(ComboSearchDialogNew_Activated);
			base.Load += new System.EventHandler(ComboSearchDialogNew_Load);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBoxPurchase.ResumeLayout(false);
			panelItemdetails.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridSales).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendors).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomers).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
