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
	public class ReceiveLotSelectionForm : Form
	{
		private DataTable productLotTable;

		private bool isReturn;

		private bool activatebin = CompanyPreferences.ActivateBin;

		private string binID = "";

		private bool isDefaultLoad;

		private bool allowAddNewRow = true;

		private bool allowEdit = true;

		private float rowQuantity;

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

		private BinComboBox comboBoxGridBin;

		private RackComboBox comboBoxGridRack;

		public bool AllowAddNewRow
		{
			get
			{
				return allowAddNewRow;
			}
			set
			{
				allowAddNewRow = value;
			}
		}

		public bool AllowEdit
		{
			get
			{
				return allowEdit;
			}
			set
			{
				allowEdit = value;
				if (!value)
				{
					dataGridItems.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				}
			}
		}

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

		public string ReturnSourceSysDocID
		{
			get;
			set;
		}

		public string ReturnSourceVoucherID
		{
			get;
			set;
		}

		public bool IsReturn
		{
			get
			{
				return isReturn;
			}
			set
			{
				isReturn = value;
			}
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

		public string CustomerID
		{
			get;
			set;
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

		public string LotNumber
		{
			get;
			set;
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

		public ReceiveLotSelectionForm()
		{
			InitializeComponent();
			base.Activated += ReceiveLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += ReceiveLotSelectionForm_Load;
			base.FormClosing += ReceiveLotSelectionForm_FormClosing;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.CellDataError += dataGridItems_CellDataError;
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (e.Cell.Column.Key == "LotNumber")
				{
					LotNumber = e.Cell.Value.ToString();
				}
				if (e.Cell.Column.Key == "BinID" && !string.IsNullOrEmpty(e.Cell.Value.ToString()))
				{
					binID = e.Cell.Value.ToString();
					comboBoxGridRack.FilterBinID = binID;
					comboBoxGridRack.LoadData(isReferesh: true);
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

		private void ReceiveLotSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
			dataGridItems.SaveLayout();
		}

		private void ReceiveLotSelectionForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
				FillData();
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadReturnableLots()
		{
			try
			{
				if (isReturn)
				{
					DataSet productReturnableLotsAndBins = Factory.ProductSystem.GetProductReturnableLotsAndBins(ProductID, LocationID, SysDocID, VoucherID, CustomerID, ReturnSourceSysDocID, ReturnSourceVoucherID);
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					for (int i = 0; i < productReturnableLotsAndBins.Tables[0].Rows.Count; i = checked(i + 1))
					{
						DataRow dataRow = productReturnableLotsAndBins.Tables[0].Rows[i];
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["LotID"] = dataRow["LotNumber"];
						dataRow2["SourceLotNumber"] = dataRow["LotNumber"];
						dataRow2["LotNumber"] = dataRow["Reference"];
						dataRow2["ReceiptDate"] = dataRow["ReceiptDate"];
						dataRow2["ReceiptNumber"] = dataRow["SourceReceiptNumber"];
						dataRow2["ExpiryDate"] = dataRow["ExpiryDate"];
						dataRow2["Productiondate"] = dataRow["ProductionDate"];
						dataRow2["BinID"] = dataRow["BinID"];
						dataRow2["RackID"] = dataRow["RackID"];
						dataRow2["Reference2"] = dataRow["Reference2"];
						if (IsDefaultLoad)
						{
							dataRow2["Quantity"] = dataRow["RowSoldQty"];
						}
						dataTable.Rows.Add(dataRow2);
					}
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						row.Cells["LotNumber"].Activation = Activation.NoEdit;
						row.Cells["ReceiptDate"].Activation = Activation.NoEdit;
						row.Cells["ReceiptDate"].Activation = Activation.NoEdit;
						row.Cells["ExpiryDate"].Activation = Activation.NoEdit;
						row.Cells["Productiondate"].Activation = Activation.NoEdit;
						row.Cells["BinID"].Activation = Activation.NoEdit;
						row.Cells["RackID"].Activation = Activation.NoEdit;
						row.Cells["Reference2"].Activation = Activation.NoEdit;
						Infragistics.Win.Appearance appearance = row.Cells["LotNumber"].Appearance;
						Infragistics.Win.Appearance appearance2 = row.Cells["ReceiptDate"].Appearance;
						Infragistics.Win.Appearance appearance3 = row.Cells["ReceiptDate"].Appearance;
						Infragistics.Win.Appearance appearance4 = row.Cells["ExpiryDate"].Appearance;
						Infragistics.Win.Appearance appearance5 = row.Cells["Productiondate"].Appearance;
						Infragistics.Win.Appearance appearance6 = row.Cells["BinID"].Appearance;
						Infragistics.Win.Appearance appearance7 = row.Cells["RackID"].Appearance;
						Color color = row.Cells["Reference2"].Appearance.BackColor = Color.WhiteSmoke;
						Color color3 = appearance7.BackColor = color;
						Color color5 = appearance6.BackColor = color3;
						Color color7 = appearance5.BackColor = color5;
						Color color9 = appearance4.BackColor = color7;
						Color color11 = appearance3.BackColor = color9;
						Color color14 = appearance.BackColor = (appearance2.BackColor = color11);
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
			if (isReturn)
			{
				LoadReturnableLots();
			}
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if ((isReturn && dataTable.Rows.Count == 0) || (!isReturn && (ProductLotTable == null || productLotTable.Rows.Count == 0)))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Quantity"] = rowQuantity;
				dataRow["LotNumber"] = LotNumber;
				dataTable.Rows.Add(dataRow);
				dataGridItems.Rows[0].Cells["LotNumber"].Activate();
				dataGridItems.EnterEditMode();
			}
			else
			{
				if (ProductLotTable == null || productLotTable.Rows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < ProductLotTable.Rows.Count; i = checked(i + 1))
				{
					DataRow dataRow2 = ProductLotTable.Rows[i];
					if (isReturn && dataRow2["SourceLotNumber"].ToString() != "")
					{
						int num = Convert.ToInt32(dataRow2["SourceLotNumber"].ToString());
						foreach (UltraGridRow row in dataGridItems.Rows)
						{
							if (row.Cells["LotID"].Value != null && row.Cells["LotID"].Value.ToString() == num.ToString())
							{
								row.Cells["Quantity"].Value = dataRow2["LotQty"].ToString();
								break;
							}
						}
						continue;
					}
					if (IsDefaultLoad)
					{
						dataTable.Clear();
						IsDefaultLoad = false;
					}
					DataRow dataRow3 = dataTable.NewRow();
					if (dataRow2["Reference"].IsNullOrEmpty())
					{
						dataRow3["LotNumber"] = dataRow2["LotNumber"];
					}
					else
					{
						dataRow3["LotNumber"] = dataRow2["Reference"];
					}
					dataRow3["LotID"] = dataRow2["LotNumber"];
					if (dataRow2["SourceLotNumber"] != DBNull.Value && dataRow2["SourceLotNumber"].ToString() != "")
					{
						dataRow3["SourceLotNumber"] = dataRow2["SourceLotNumber"];
					}
					else
					{
						dataRow3["SourceLotNumber"] = DBNull.Value;
					}
					dataRow3["ReceiptDate"] = dataRow2["ReceiptDate"];
					dataRow3["ReceiptNumber"] = dataRow2["VoucherID"];
					dataRow3["ExpiryDate"] = dataRow2["ExpiryDate"];
					dataRow3["BinID"] = dataRow2["BinID"];
					if (!string.IsNullOrEmpty(dataRow2["BinID"].ToString()))
					{
						comboBoxGridRack.FilterBinID = dataRow2["BinID"].ToString().Trim();
					}
					dataRow3["RackID"] = dataRow2["RackID"];
					dataRow3["Productiondate"] = dataRow2["ProductionDate"];
					dataRow3["Quantity"] = dataRow2["LotQty"];
					dataRow3["Reference2"] = dataRow2["Reference2"];
					dataTable.Rows.Add(dataRow3);
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("LotID");
				dataTable.Columns.Add("LotNumber");
				dataTable.Columns.Add("Reference2");
				dataTable.Columns.Add("BinID");
				dataTable.Columns.Add("RackID");
				dataTable.Columns.Add("SourceLotNumber", typeof(int));
				dataTable.Columns.Add("ReceiptNumber");
				dataTable.Columns.Add("ReceiptDate", typeof(DateTime));
				dataTable.Columns.Add("ProductionDate", typeof(DateTime));
				dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
				dataTable.Columns.Add("Quantity", typeof(float));
				dataGridItems.DataSource = dataTable;
				companyInfo = Factory.CompanyInformationSystem.GetCompanyInformation();
				string text = companyInfo.Tables[0].Rows[0]["LotNoIdentity"].ToString();
				string text2 = companyInfo.Tables[0].Rows[0]["Reference2"].ToString();
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				if (!activatebin)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].CharacterCasing = CharacterCasing.Upper;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].MaxLength = 64;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].ValueList = comboBoxGridBin;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].CharacterCasing = CharacterCasing.Upper;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].MaxLength = 64;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					dataGridItems.DisplayLayout.Bands[0].Columns["RackID"].ValueList = comboBoxGridRack;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["LotID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
				if (!IsReturn && allowAddNewRow)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].Hidden = true;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Header.Caption = "Lot Number";
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Header.Caption = "Bin";
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Header.Caption = "Prod.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Header.Caption = "Exp.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptNumber"].Header.Caption = "Receipt Num";
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].Header.Caption = "Receipt Date";
				if (text != "")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Header.Caption = text;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Qty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.SetupUI();
				if (!AllowAddNewRow)
				{
					dataGridItems.AllowAddNew = false;
					dataGridItems.ShowDeleteMenu = false;
					dataGridItems.ShowInsertMenu = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].CellActivation = Activation.NoEdit;
					AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].CellAppearance;
					AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].CellAppearance;
					AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].CellAppearance;
					Color color = dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].CellAppearance.BackColor = Color.WhiteSmoke;
					Color color3 = cellAppearance3.BackColor = color;
					Color color6 = cellAppearance.BackColor = (cellAppearance2.BackColor = color3);
				}
				else
				{
					dataGridItems.AllowAddNew = true;
					dataGridItems.ShowDeleteMenu = true;
					dataGridItems.ShowInsertMenu = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].CellActivation = Activation.AllowEdit;
					AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].CellAppearance;
					AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["ReceiptDate"].CellAppearance;
					AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].CellAppearance;
					Color color = dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].CellAppearance.BackColor = Color.WhiteSmoke;
					Color color3 = cellAppearance6.BackColor = color;
					Color color6 = cellAppearance4.BackColor = (cellAppearance5.BackColor = color3);
				}
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void ReceiveLotSelectionForm_Activated(object sender, EventArgs e)
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
				if (row.Cells["LotNumber"].Value == null || row.Cells["LotNumber"].Value.ToString() == "")
				{
					ErrorHelper.WarningMessage("Please enter a lot number.");
					return false;
				}
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result);
				if ((row.Cells["LotID"].Value == null || row.Cells["LotID"].Value.ToString() == "") && (row.Cells["Quantity"].Value == null || string.IsNullOrEmpty(row.Cells["Quantity"].Value.ToString())))
				{
					ErrorHelper.WarningMessage("Please enter quantity for all the new lots.");
					return false;
				}
				d += result;
			}
			if (d != Convert.ToDecimal(rowQuantity))
			{
				ErrorHelper.WarningMessage("Total allocated lot quantities should be equal to the total receiving quantity.");
				return false;
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
				DataTable dataTable = InventoryTransactionData.AddProductLotReceivingDetailTable(new DataSet()).Tables[0];
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Quantity"].Value != null && !string.IsNullOrEmpty(row.Cells["Quantity"].Value.ToString()) && !(decimal.Parse(row.Cells["Quantity"].Value.ToString()) == 0m))
					{
						DataRow dataRow = dataTable.NewRow();
						if (!row.Cells["LotID"].Value.IsNullOrEmpty())
						{
							dataRow["LotNumber"] = row.Cells["LotID"].Value.ToString();
						}
						else
						{
							dataRow["LotNumber"] = row.Cells["LotNumber"].Value.ToString();
						}
						dataRow["Reference"] = row.Cells["LotNumber"].Value.ToString();
						dataRow["ProductID"] = textBoxProductID.Text;
						dataRow["LocationID"] = textBoxLocation.Text;
						if (row.Cells["ProductionDate"].Value != null && row.Cells["ProductionDate"].Value.ToString() != "")
						{
							dataRow["ProductionDate"] = row.Cells["ProductionDate"].Value.ToString();
						}
						else
						{
							dataRow["ProductionDate"] = DBNull.Value;
						}
						if (row.Cells["ExpiryDate"].Value != null && row.Cells["ExpiryDate"].Value.ToString() != "")
						{
							dataRow["ExpiryDate"] = row.Cells["ExpiryDate"].Value.ToString();
						}
						else
						{
							dataRow["ExpiryDate"] = DBNull.Value;
						}
						dataRow["LotQty"] = row.Cells["Quantity"].Value.ToString();
						dataRow["BinID"] = row.Cells["BinID"].Value.ToString();
						dataRow["RackID"] = row.Cells["RackID"].Value.ToString();
						dataRow["Reference2"] = row.Cells["Reference2"].Value.ToString();
						if (row.Cells["ReceiptDate"].Value != DBNull.Value)
						{
							dataRow["ReceiptDate"] = row.Cells["ReceiptDate"].Value.ToString();
						}
						if (row.Cells["SourceLotNumber"].Value != DBNull.Value)
						{
							dataRow["SourceLotNumber"] = row.Cells["SourceLotNumber"].Value.ToString();
						}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ReceiveLotSelectionForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxLocation = new System.Windows.Forms.TextBox();
			textBoxProductID = new System.Windows.Forms.TextBox();
			textBoxProductName = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxQuantity = new Micromind.UISupport.QuantityTextBox();
			comboBoxGridRack = new Micromind.DataControls.RackComboBox();
			comboBoxGridBin = new Micromind.DataControls.BinComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 331);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(587, 40);
			panelButtons.TabIndex = 3;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(375, 8);
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
			linePanelDown.Size = new System.Drawing.Size(587, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(477, 8);
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
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 34);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 13);
			label1.TabIndex = 13;
			label1.Text = "Location:";
			textBoxLocation.Location = new System.Drawing.Point(76, 31);
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
			label2.Location = new System.Drawing.Point(385, 38);
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
			textBoxQuantity.Location = new System.Drawing.Point(450, 35);
			textBoxQuantity.Name = "textBoxQuantity";
			textBoxQuantity.ReadOnly = true;
			textBoxQuantity.Size = new System.Drawing.Size(123, 20);
			textBoxQuantity.TabIndex = 16;
			textBoxQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxGridRack.Assigned = false;
			comboBoxGridRack.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridRack.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridRack.CustomReportFieldName = "";
			comboBoxGridRack.CustomReportKey = "";
			comboBoxGridRack.CustomReportValueType = 1;
			comboBoxGridRack.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridRack.DisplayLayout.Appearance = appearance;
			comboBoxGridRack.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridRack.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridRack.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridRack.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridRack.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridRack.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridRack.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridRack.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridRack.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridRack.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridRack.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridRack.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridRack.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridRack.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridRack.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridRack.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxGridRack.Location = new System.Drawing.Point(392, 178);
			comboBoxGridRack.MaxDropDownItems = 12;
			comboBoxGridRack.Name = "comboBoxGridRack";
			comboBoxGridRack.ShowInactiveItems = false;
			comboBoxGridRack.ShowQuickAdd = true;
			comboBoxGridRack.Size = new System.Drawing.Size(100, 20);
			comboBoxGridRack.TabIndex = 18;
			comboBoxGridRack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridRack.Visible = false;
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
			comboBoxGridBin.Location = new System.Drawing.Point(388, 116);
			comboBoxGridBin.MaxDropDownItems = 12;
			comboBoxGridBin.Name = "comboBoxGridBin";
			comboBoxGridBin.ShowInactiveItems = false;
			comboBoxGridBin.ShowQuickAdd = true;
			comboBoxGridBin.Size = new System.Drawing.Size(100, 20);
			comboBoxGridBin.TabIndex = 17;
			comboBoxGridBin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBin.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance25;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
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
			dataGridItems.Size = new System.Drawing.Size(564, 256);
			dataGridItems.TabIndex = 12;
			dataGridItems.Text = "dataEntryGrid1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(587, 371);
			base.Controls.Add(comboBoxGridRack);
			base.Controls.Add(comboBoxGridBin);
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
			base.Name = "ReceiveLotSelectionForm";
			Text = "Lot Selection";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
