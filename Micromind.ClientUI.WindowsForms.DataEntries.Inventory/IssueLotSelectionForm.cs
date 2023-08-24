using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class IssueLotSelectionForm : Form
	{
		private DataTable productLotTable;

		private DataSet currentData;

		private string vendorID = "";

		private bool activatebin = CompanyPreferences.ActivateBin;

		private bool isDefaultLoad;

		private bool materialReservationOnSO = CompanyPreferences.MaterialReservationONSO;

		private string binID = "";

		private float rowQuantity;

		private bool ispicklist;

		private SysDocTypes sysdocType = SysDocTypes.None;

		private DataSet companyInfo;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private Label label3;

		private DataEntryGrid dataGridItems;

		private Label label1;

		private TextBox textBoxLocation;

		private TextBox textBoxProductID;

		private TextBox textBoxProductName;

		private Label label2;

		private QuantityTextBox textBoxQuantity;

		private XPButton buttonAllocate;

		private CheckBox checkBoxShowAvailableLots;

		private BinComboBox comboBoxGridBin;

		private RackComboBox comboBoxGridRack;

		public float RowQuantity
		{
			get
			{
				return rowQuantity;
			}
			set
			{
				rowQuantity = value;
				textBoxQuantity.Text = value.ToString();
			}
		}

		public string CustomerID
		{
			get;
			set;
		}

		public DataTable ProductLotTable
		{
			get
			{
				return productLotTable;
			}
			set
			{
				productLotTable = value;
			}
		}

		public string SysDocID
		{
			get;
			set;
		}

		public string VoucherID
		{
			get;
			set;
		}

		public bool Ispicklist
		{
			get
			{
				return ispicklist;
			}
			set
			{
				ispicklist = value;
			}
		}

		public bool IsDefaultLoad
		{
			get
			{
				return isDefaultLoad;
			}
			set
			{
				isDefaultLoad = value;
			}
		}

		public string ProductID
		{
			get
			{
				return textBoxProductID.Text;
			}
			set
			{
				textBoxProductID.Text = value;
			}
		}

		public string ProductDescription
		{
			get
			{
				return textBoxProductName.Text;
			}
			set
			{
				textBoxProductName.Text = value;
			}
		}

		public string LocationID
		{
			get
			{
				return textBoxLocation.Text;
			}
			set
			{
				textBoxLocation.Text = value;
			}
		}

		public SysDocTypes sysDocType
		{
			get
			{
				return sysdocType;
			}
			set
			{
				sysdocType = value;
			}
		}

		public string VendorID
		{
			get
			{
				return vendorID;
			}
			set
			{
				vendorID = value;
			}
		}

		public string BinID
		{
			get
			{
				return binID;
			}
			set
			{
				binID = value;
			}
		}

		public decimal TotalQuantity
		{
			get
			{
				decimal result = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					result += decimal.Parse(row.Cells["Quantity"].Value.ToString());
				}
				return result;
			}
		}

		public IssueLotSelectionForm()
		{
			InitializeComponent();
			base.Activated += IssueLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += IssueLotSelectionForm_Load;
			base.FormClosing += IssueLotSelectionForm_FormClosing;
			dataGridItems.CellDataError += dataGridItems_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGridItems_BeforeCellUpdate;
		}

		private void dataGridItems_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Quantity" && e.Cell.IsActiveCell && e.NewValue != null && !(e.NewValue.ToString() == ""))
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["LotQty"].Value.ToString(), out result);
				decimal.TryParse(e.NewValue.ToString(), out result2);
				if (result2 > result || result2 < 0m)
				{
					ErrorHelper.WarningMessage("Please enter a positive quantity equal or less than available lot quantity.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a non-negative numeric value.");
			}
		}

		private void IssueLotSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridItems.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void IssueLotSelectionForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
				LoadData();
				FillData();
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			try
			{
				if (sysdocType != SysDocTypes.SalesOrder)
				{
					currentData = Factory.ProductSystem.GetProductAvailableLotsAndBins(ProductID, LocationID, SysDocID, VoucherID, vendorID);
				}
				else
				{
					currentData = Factory.ProductSystem.GetSOProductAvailableLotsAndBins(ProductID, LocationID, SysDocID, VoucherID, vendorID);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			DataRow dataRow = null;
			checked
			{
				if ((currentData != null) & (currentData.Tables.Count > 0))
				{
					DataTable dataTable2 = currentData.Tables["Product_Lot"];
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						dataRow = dataTable2.Rows[i];
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["LotNumber"] = dataRow["LotNumber"];
						dataRow2["Reference"] = dataRow["Reference"];
						dataRow2["SourceLotNumber"] = dataRow["SourceLotNumber"];
						dataRow2["Consign#"] = dataRow["Consign#"];
						dataRow2["ExpiryDate"] = dataRow["ExpiryDate"];
						dataRow2["Cost"] = dataRow["Cost"];
						dataRow2["Productiondate"] = dataRow["ProductionDate"];
						dataRow2["LotQty"] = dataRow["AvailableQty"];
						dataRow2["Reference2"] = dataRow["Reference2"];
						dataRow2["BinID"] = dataRow["BinID"];
						dataRow2["RackID"] = dataRow["RackID"];
						int.Parse(dataRow["ItemType"].ToString());
						if (currentData.Tables["Product_Lot"].Columns.Contains("QtyRs"))
						{
							if (materialReservationOnSO && !string.IsNullOrEmpty(dataRow["QtyRs"].ToString()))
							{
								dataRow2["RsrvdQty"] = decimal.Parse(dataRow["QtyRs"].ToString());
								dataGridItems.DisplayLayout.Bands[0].Columns["RsrvdQty"].Hidden = false;
							}
							else
							{
								dataGridItems.DisplayLayout.Bands[0].Columns["RsrvdQty"].Hidden = true;
							}
						}
						if (string.IsNullOrEmpty(BinID) || !(dataRow["BinID"].ToString() != BinID))
						{
							dataTable.Rows.Add(dataRow2);
						}
					}
				}
				if (ProductLotTable == null || productLotTable.Rows.Count <= 0)
				{
					return;
				}
				for (int j = 0; j < ProductLotTable.Rows.Count; j++)
				{
					int num = int.Parse(productLotTable.Rows[j]["LotNumber"].ToString());
					dataRow = ProductLotTable.Rows[j];
					bool flag = false;
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (int.Parse(row.Cells["LotNumber"].Value.ToString()) == num)
						{
							row.Cells["Quantity"].Value = dataRow["SoldQty"].ToString();
							if (IsDefaultLoad)
							{
								row.Cells["Quantity"].Value = row.Cells["LotQty"].Value;
							}
							flag = true;
							break;
						}
					}
					if (!string.IsNullOrEmpty(SysDocID) && !string.IsNullOrEmpty(VoucherID) && !flag && num > 0)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["LotNumber"] = num;
						dataRow3["Reference"] = dataRow["Reference"];
						if (dataRow.Table.Columns.Contains("Consign#"))
						{
							dataRow3["Consign#"] = dataRow["Consign#"];
						}
						dataRow3["ExpiryDate"] = dataRow["ExpiryDate"];
						dataRow3["Cost"] = dataRow["Cost"];
						dataRow3["Productiondate"] = dataRow["ProductionDate"];
						dataRow3["LotQty"] = dataRow["SoldQty"];
						dataRow3["Quantity"] = dataRow["SoldQty"];
						if (dataRow.Table.Columns.Contains("BinID"))
						{
							dataRow3["BinID"] = dataRow["BinID"];
							dataRow3["RackID"] = dataRow["RackID"];
						}
						if (dataRow.Table.Columns.Contains("Reference2"))
						{
							dataRow3["Reference2"] = dataRow["Reference2"];
						}
						dataTable.Rows.Add(dataRow3);
					}
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.SetupUI();
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("LotNumber", typeof(int));
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Reference2");
				dataTable.Columns.Add("SourceLotNumber");
				dataTable.Columns.Add("Consign#");
				dataTable.Columns.Add("BinID");
				dataTable.Columns.Add("RackID");
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("ProductionDate", typeof(DateTime));
				dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
				dataTable.Columns.Add("LotQty", typeof(float));
				dataTable.Columns.Add("Quantity", typeof(float));
				dataTable.Columns.Add("RsrvdQty", typeof(float));
				dataGridItems.DataSource = dataTable;
				companyInfo = Factory.CompanyInformationSystem.GetCompanyInformation();
				string text = companyInfo.Tables[0].Rows[0]["LotNoIdentity"].ToString();
				string text2 = companyInfo.Tables[0].Rows[0]["Reference2"].ToString();
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Header.Caption = "Lot Number";
				dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Header.Caption = "Lot ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Header.Caption = "Bin";
				dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Header.Caption = "Rack";
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Header.Caption = "Prod.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Header.Caption = "Exp.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].Header.Caption = "Lot Qty";
				if (text != "")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Header.Caption = text;
				}
				if (text2 != "")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Header.Caption = text2;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
				}
				if (!activatebin)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Hidden = false;
				}
				if (!materialReservationOnSO)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RsrvdQty"].Hidden = true;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["BinID"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["RackID"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Reference"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Consign#"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn9.CellActivation = activation2;
				Activation activation6 = ultraGridColumn8.CellActivation = activation4;
				Activation activation8 = ultraGridColumn7.CellActivation = activation6;
				Activation activation10 = ultraGridColumn6.CellActivation = activation8;
				Activation activation12 = ultraGridColumn5.CellActivation = activation10;
				Activation activation14 = ultraGridColumn4.CellActivation = activation12;
				Activation activation16 = ultraGridColumn3.CellActivation = activation14;
				Activation activation19 = ultraGridColumn.CellActivation = (ultraGridColumn2.CellActivation = activation16);
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].CellAppearance;
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].CellAppearance;
				AppearanceBase cellAppearance8 = dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGridItems.DisplayLayout.Bands[0].Columns["Consign#"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = cellAppearance9.BackColor = color;
				Color color5 = cellAppearance8.BackColor = color3;
				Color color7 = cellAppearance7.BackColor = color5;
				Color color9 = cellAppearance6.BackColor = color7;
				Color color11 = cellAppearance5.BackColor = color9;
				Color color13 = cellAppearance4.BackColor = color11;
				Color color15 = cellAppearance3.BackColor = color13;
				Color color18 = cellAppearance.BackColor = (cellAppearance2.BackColor = color15);
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Qty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("LotQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["LotQty"], SummaryPosition.UseSummaryPositionColumn);
				if (ispicklist)
				{
					dataGridItems.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
					buttonAllocate.Enabled = false;
				}
				else
				{
					dataGridItems.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
					buttonAllocate.Enabled = true;
				}
				dataGridItems.AllowAddNew = false;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void IssueLotSelectionForm_Activated(object sender, EventArgs e)
		{
			dataGridItems.Focus();
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private bool ValidateData()
		{
			decimal d = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal num = default(decimal);
				if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
				{
					num = decimal.Parse(row.Cells["Quantity"].Value.ToString());
				}
				d += num;
			}
			if (sysDocType != SysDocTypes.SalesOrder && d != Convert.ToDecimal(rowQuantity))
			{
				ErrorHelper.WarningMessage("Total allocated lot quantities should be equal to the total issued quantity.");
				return false;
			}
			if (materialReservationOnSO)
			{
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal d2 = default(decimal);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					num2 = ((row2.Cells["Quantity"].Value == null || !(row2.Cells["Quantity"].Value.ToString() != "")) ? default(decimal) : decimal.Parse(row2.Cells["Quantity"].Value.ToString()));
					d += num2;
					num3 = ((row2.Cells["RsrvdQty"].Value == null || !(row2.Cells["RsrvdQty"].Value.ToString() != "")) ? default(decimal) : decimal.Parse(row2.Cells["RsrvdQty"].Value.ToString()));
					if (row2.Cells["LotQty"].Value != null && row2.Cells["LotQty"].Value.ToString() != "")
					{
						d2 = decimal.Parse(row2.Cells["LotQty"].Value.ToString());
					}
					if (num2 > d2 - num3)
					{
						ErrorHelper.WarningMessage("Total issued quantity must be less than the reserved quantity.");
						return false;
					}
				}
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
				if (!ValidateData())
				{
					base.DialogResult = DialogResult.None;
				}
				else if (SaveRows())
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					base.DialogResult = DialogResult.None;
				}
			}
			catch (Exception e2)
			{
				base.DialogResult = DialogResult.None;
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool SaveRows()
		{
			try
			{
				DataSet data = new DataSet();
				data = ((sysDocType == SysDocTypes.SalesOrder) ? SalesOrderData.AddSalesOrderLotDetailTable(data) : InventoryTransactionData.AddProductLotIssueDetailTable(data));
				DataTable dataTable = data.Tables[0];
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Quantity"].Value != null && !(row.Cells["Quantity"].Value.ToString() == "") && !(decimal.Parse(row.Cells["Quantity"].Value.ToString()) == 0m))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["LotNumber"] = row.Cells["LotNumber"].Value.ToString();
						if (row.Cells["SourceLotNumber"].Value != null && !string.IsNullOrWhiteSpace(row.Cells["SourceLotNumber"].Value.ToString()))
						{
							dataRow["SourceLotNumber"] = row.Cells["SourceLotNumber"].Value.ToString();
						}
						else
						{
							dataRow["SourceLotNumber"] = DBNull.Value;
						}
						dataRow["Reference"] = row.Cells["Reference"].Value.ToString();
						dataRow["ProductID"] = textBoxProductID.Text;
						dataRow["LocationID"] = textBoxLocation.Text;
						dataRow["Cost"] = row.Cells["Cost"].Value.ToString();
						dataRow["SoldQty"] = row.Cells["Quantity"].Value.ToString();
						dataRow["BinID"] = row.Cells["BinID"].Value.ToString();
						dataRow["RackID"] = row.Cells["RackID"].Value.ToString();
						dataRow["Reference2"] = row.Cells["Reference2"].Value.ToString();
						dataRow.EndEdit();
						dataTable.Rows.Add(dataRow);
					}
				}
				productLotTable = dataTable.Copy();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void checkBoxUnitPriceOverWrite_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonAllocate_Click(object sender, EventArgs e)
		{
			try
			{
				decimal d = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
					{
						d += decimal.Parse(row.Cells["Quantity"].Value.ToString());
					}
				}
				decimal num = (decimal)rowQuantity - d;
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (num == 0m)
					{
						break;
					}
					decimal num2 = decimal.Parse(row2.Cells["LotQty"].Value.ToString());
					if (row2.Cells["Quantity"].Value == null || !(row2.Cells["Quantity"].Value.ToString() != ""))
					{
						if (!(num > num2))
						{
							row2.Cells["Quantity"].Value = num;
							break;
						}
						row2.Cells["Quantity"].Value = num2;
						num -= num2;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxShowAvailableLots_CheckedChanged(object sender, EventArgs e)
		{
			LoadData();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.IssueLotSelectionForm));
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxShowAvailableLots = new System.Windows.Forms.CheckBox();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label3 = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			label1 = new System.Windows.Forms.Label();
			textBoxLocation = new System.Windows.Forms.TextBox();
			textBoxProductID = new System.Windows.Forms.TextBox();
			textBoxProductName = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxQuantity = new Micromind.UISupport.QuantityTextBox();
			buttonAllocate = new Micromind.UISupport.XPButton();
			comboBoxGridBin = new Micromind.DataControls.BinComboBox();
			comboBoxGridRack = new Micromind.DataControls.RackComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(checkBoxShowAvailableLots);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 331);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(608, 40);
			panelButtons.TabIndex = 3;
			checkBoxShowAvailableLots.AutoSize = true;
			checkBoxShowAvailableLots.Location = new System.Drawing.Point(10, 8);
			checkBoxShowAvailableLots.Name = "checkBoxShowAvailableLots";
			checkBoxShowAvailableLots.Size = new System.Drawing.Size(223, 17);
			checkBoxShowAvailableLots.TabIndex = 15;
			checkBoxShowAvailableLots.Text = "Show available lots which are not sold yet";
			checkBoxShowAvailableLots.UseVisualStyleBackColor = true;
			checkBoxShowAvailableLots.Visible = false;
			checkBoxShowAvailableLots.CheckedChanged += new System.EventHandler(checkBoxShowAvailableLots_CheckedChanged);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(396, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(608, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(498, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 10);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(38, 13);
			label3.TabIndex = 10;
			label3.Text = "Item:";
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(10, 59);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(585, 256);
			dataGridItems.TabIndex = 12;
			dataGridItems.Text = "dataEntryGrid1";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 35);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 13);
			label1.TabIndex = 13;
			label1.Text = "Location:";
			textBoxLocation.Location = new System.Drawing.Point(76, 32);
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(138, 20);
			textBoxLocation.TabIndex = 14;
			textBoxProductID.Location = new System.Drawing.Point(76, 8);
			textBoxProductID.Name = "textBoxProductID";
			textBoxProductID.ReadOnly = true;
			textBoxProductID.Size = new System.Drawing.Size(138, 20);
			textBoxProductID.TabIndex = 15;
			textBoxProductName.Location = new System.Drawing.Point(217, 8);
			textBoxProductName.Name = "textBoxProductName";
			textBoxProductName.ReadOnly = true;
			textBoxProductName.Size = new System.Drawing.Size(356, 20);
			textBoxProductName.TabIndex = 15;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(227, 35);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(59, 13);
			label2.TabIndex = 13;
			label2.Text = "Quantity:";
			textBoxQuantity.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxQuantity.CustomReportFieldName = "";
			textBoxQuantity.CustomReportKey = "";
			textBoxQuantity.CustomReportValueType = 1;
			textBoxQuantity.IsComboTextBox = false;
			textBoxQuantity.IsModified = false;
			textBoxQuantity.Location = new System.Drawing.Point(292, 32);
			textBoxQuantity.Name = "textBoxQuantity";
			textBoxQuantity.ReadOnly = true;
			textBoxQuantity.Size = new System.Drawing.Size(123, 20);
			textBoxQuantity.TabIndex = 16;
			textBoxQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			buttonAllocate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAllocate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonAllocate.BackColor = System.Drawing.Color.DarkGray;
			buttonAllocate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAllocate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAllocate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAllocate.Location = new System.Drawing.Point(500, 34);
			buttonAllocate.Name = "buttonAllocate";
			buttonAllocate.Size = new System.Drawing.Size(96, 24);
			buttonAllocate.TabIndex = 17;
			buttonAllocate.Text = "Auto Allocate";
			buttonAllocate.UseVisualStyleBackColor = false;
			buttonAllocate.Click += new System.EventHandler(buttonAllocate_Click);
			comboBoxGridBin.Assigned = false;
			comboBoxGridBin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridBin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridBin.CustomReportFieldName = "";
			comboBoxGridBin.CustomReportKey = "";
			comboBoxGridBin.CustomReportValueType = 1;
			comboBoxGridBin.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBin.DisplayLayout.Appearance = appearance13;
			comboBoxGridBin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridBin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridBin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBin.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBin.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBin.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridBin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBin.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridBin.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridBin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBin.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridBin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBin.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridBin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridBin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridBin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridBin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridBin.Editable = true;
			comboBoxGridBin.FilterString = "";
			comboBoxGridBin.HasAllAccount = false;
			comboBoxGridBin.HasCustom = false;
			comboBoxGridBin.IsDataLoaded = false;
			comboBoxGridBin.Location = new System.Drawing.Point(254, 175);
			comboBoxGridBin.MaxDropDownItems = 12;
			comboBoxGridBin.Name = "comboBoxGridBin";
			comboBoxGridBin.ShowInactiveItems = false;
			comboBoxGridBin.ShowQuickAdd = true;
			comboBoxGridBin.Size = new System.Drawing.Size(100, 20);
			comboBoxGridBin.TabIndex = 18;
			comboBoxGridBin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBin.Visible = false;
			comboBoxGridRack.Assigned = false;
			comboBoxGridRack.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridRack.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridRack.CustomReportFieldName = "";
			comboBoxGridRack.CustomReportKey = "";
			comboBoxGridRack.CustomReportValueType = 1;
			comboBoxGridRack.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridRack.DisplayLayout.Appearance = appearance25;
			comboBoxGridRack.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridRack.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridRack.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridRack.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridRack.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridRack.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridRack.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridRack.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridRack.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridRack.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridRack.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridRack.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridRack.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridRack.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridRack.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridRack.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridRack.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridRack.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridRack.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridRack.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridRack.Editable = true;
			comboBoxGridRack.FilterBinID = "";
			comboBoxGridRack.FilterString = "";
			comboBoxGridRack.HasAllAccount = false;
			comboBoxGridRack.HasCustom = false;
			comboBoxGridRack.IsDataLoaded = false;
			comboBoxGridRack.Location = new System.Drawing.Point(312, 216);
			comboBoxGridRack.MaxDropDownItems = 12;
			comboBoxGridRack.Name = "comboBoxGridRack";
			comboBoxGridRack.ShowInactiveItems = false;
			comboBoxGridRack.ShowQuickAdd = true;
			comboBoxGridRack.Size = new System.Drawing.Size(100, 20);
			comboBoxGridRack.TabIndex = 19;
			comboBoxGridRack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridRack.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(608, 371);
			base.Controls.Add(comboBoxGridRack);
			base.Controls.Add(comboBoxGridBin);
			base.Controls.Add(buttonAllocate);
			base.Controls.Add(textBoxQuantity);
			base.Controls.Add(textBoxProductName);
			base.Controls.Add(textBoxProductID);
			base.Controls.Add(textBoxLocation);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "IssueLotSelectionForm";
			Text = "Lot Selection";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
