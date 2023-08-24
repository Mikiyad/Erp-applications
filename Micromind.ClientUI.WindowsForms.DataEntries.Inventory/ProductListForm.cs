using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using Micromind.UISupport.GUIControls.Others;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductListForm : Form, IForm
	{
		private DataSet flagData;

		private bool isFirstTimeLoading = true;

		private bool canAccessCost = true;

		private bool isEditMode;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet productData;

		private bool showInactiveItems;

		private XPButton buttonDone;

		private XPButton buttonNew;

		private XPButton buttonDelete;

		private Panel panelButtons;

		private Line linePanelDown;

		private IContainer components;

		private XPButton buttonOpen;

		private DataGridList dataGridList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonShowInactive;

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private ThumbnailViewer thumbnailViewer1;

		private LocationComboBox comboBoxLocation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripMenuItemLocationWiseOnhand;

		private RadioButton radioButtonAllLocations;

		private RadioButton radioButtonOneLocation;

		private CheckBox checkBoxZero;

		private ContextMenuStrip contextMenuItems;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem selectedRowsToolStripMenuItem;

		private ToolStripMenuItem totalOnhandToolStripMenuItem;

		private System.Windows.Forms.ToolTip toolTip1;

		private UltraToolTipManager ultraToolTipManager1;

		private ContextMenuStrip contextMenuStripDropDown;

		private ToolStripMenuItem matrixQuantityToolStripMenuItem;

		private ToolStripButton toolStripButtonEditMode;

		private ToolStripButton toolStripButton1;

		private ToolStripMenuItem inventoryLedgerToolStripMenuItem;

		private ToolStripButton toolStripButtonMerge;

		private ToolStripMenuItem availableLotsToolStripMenuItem;

		private ToolStripButton toolStripButtonFitText;

		private ToolStripButton toolStripButtonClearFilter;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem PurchaseStatisticsToolStripMenuItem;

		private ImageList imageList1;

		private ToolStripMenuItem flagsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem toolStripItemClearFlag;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

		private bool allowflagitems;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4011;

		public ScreenTypes ScreenType => ScreenTypes.List;

		private bool ShowInactiveItems
		{
			get
			{
				return showInactiveItems;
			}
			set
			{
				showInactiveItems = value;
			}
		}

		private bool IsEditMode
		{
			get
			{
				return toolStripButtonEditMode.Checked;
			}
			set
			{
				if (value)
				{
					foreach (UltraGridColumn column in dataGridList.DisplayLayout.Bands[0].Columns)
					{
						column.CellActivation = Activation.NoEdit;
					}
					dataGridList.DisplayLayout.Bands[0].Override.MultiCellSelectionMode = MultiCellSelectionMode.Standard;
					dataGridList.DisplayLayout.Bands[0].Override.AllowMultiCellOperations = AllowMultiCellOperation.All;
					dataGridList.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
					Color lightYellow = Color.LightYellow;
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice1"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Description"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("VendorRef"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Weight"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellClickAction = CellClickAction.EditAndSelectText;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UPC"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellAppearance.BackColor = lightYellow;
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellActivation = Activation.AllowEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellClickAction = CellClickAction.EditAndSelectText;
					}
				}
				else
				{
					dataGridList.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
					Color backColor = dataGridList.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColor;
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice1"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Description"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("VendorRef"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Weight"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["Weight"].CellClickAction = CellClickAction.RowSelect;
					}
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UPC"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellAppearance.BackColor = backColor;
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellActivation = Activation.NoEdit;
						dataGridList.DisplayLayout.Bands[0].Columns["UPC"].CellClickAction = CellClickAction.RowSelect;
					}
				}
			}
		}

		public ProductListForm()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridList.DoubleClickCell += dataGridList_DoubleClickCell;
			dataGridList.DropDownMenu.Items.Add(new ToolStripSeparator());
			dataGridList.DropDownMenu.Items.Add(toolStripMenuItemLocationWiseOnhand);
			toolStripMenuItemLocationWiseOnhand.Click += toolStripMenuItemLocationWiseOnhand_Click;
			dataGridList.DropDownMenu.Opening += DropDownMenu_Opening;
			dataGridList.DropDownMenu.Opening += contextMenuStripDropDown_Opening;
			comboBoxLocation.ShowAll = true;
			checked
			{
				int num;
				for (num = 0; num < contextMenuStripDropDown.Items.Count; num++)
				{
					dataGridList.DropDownMenu.Items.Add(contextMenuStripDropDown.Items[num]);
					num--;
				}
				comboBoxLocation.ShowWarehouseOnly = true;
				dataGridList.KeyDown += dataGridList_KeyDown;
				dataGridList.AfterSelectChange += dataGridList_AfterSelectChange;
				dataGridList.AfterCellUpdate += dataGridList_AfterCellUpdate;
				dataGridList.AllowUnfittedView = true;
				base.FormClosing += ProductListForm_FormClosing;
				dataGridList.BeforeRowFilterDropDownPopulate += DataGridList_BeforeRowFilterDropDownPopulate;
				dataGridList.InitializeRow += DataGridList_InitializeRow;
			}
		}

		private void DataGridList_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
		{
			try
			{
				if (e.Column.Key == "F")
				{
					e.Handled = true;
					foreach (DataRow row in flagData.Tables[0].Rows)
					{
						int argb = int.Parse(row["Color"].ToString());
						ValueListItem valueListItem = new ValueListItem(row["Name"].ToString());
						valueListItem.Appearance.Image = ImageHelper.CreateImage(Color.FromArgb(argb), 12, 16);
						e.ValueList.ValueListItems.Add(valueListItem);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FlagItem_Click(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
				int num = int.Parse(((NameValue)toolStripMenuItem.Tag).Name.ToString());
				if (Factory.EntityFlagSystem.SetFlag(25, GetSelectedID(), num, !toolStripMenuItem.Checked))
				{
					if (toolStripMenuItem.Checked)
					{
						productData.Tables["Entity_Flag_Detail"].Rows.Add(GetSelectedID(), num, 25, toolStripMenuItem.Text, int.Parse(((NameValue)toolStripMenuItem.Tag).ID.ToString()));
					}
					else
					{
						DataRow[] array = productData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND FlagID = " + num);
						productData.Tables["Entity_Flag_Detail"].Rows.Remove(array[0]);
					}
					SetRowFlags(dataGridList.ActiveRow);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void DataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			try
			{
				SetRowFlags(e.Row);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetRowFlags(UltraGridRow row)
		{
			try
			{
				string str = row.Cells["Item Code"].Value.ToString();
				DataRow[] array = productData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + str + "'");
				if (array.Length != 0)
				{
					string text = "";
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						int argb = int.Parse(array[i]["Color"].ToString());
						if (text != "")
						{
							text += " ";
						}
						text += array[i]["FlagName"].ToString();
						Bitmap value = ImageHelper.CreateImage(Color.FromArgb(argb), 12, 16);
						arrayList.Add(value);
					}
					Bitmap imageBackground = ImageHelper.CombineBitmaps((Bitmap[])arrayList.ToArray(typeof(Bitmap)));
					row.Cells["F"].Appearance.ImageBackground = imageBackground;
					row.Cells["F"].Value = text;
					row.Cells["F"].Appearance.ForeColor = Color.Transparent;
					row.Cells["F"].ActiveAppearance.ForeColor = Color.Transparent;
					row.Cells["F"].SelectedAppearance.ForeColor = Color.Transparent;
				}
				else
				{
					row.Cells["F"].Appearance.ImageBackground = null;
					row.Cells["F"].Value = "";
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridList_KeyDown(object sender, KeyEventArgs e)
		{
			if (!IsEditMode)
			{
				return;
			}
			checked
			{
				if (e.KeyCode == Keys.Down)
				{
					if (dataGridList.ActiveCell != null && dataGridList.ActiveCell.IsInEditMode && dataGridList.ActiveRow.Index < dataGridList.Rows.Count - 1)
					{
						dataGridList.Rows[dataGridList.ActiveRow.Index + 1].Cells[dataGridList.ActiveCell.Column.Index].Activate();
						dataGridList.PerformAction(UltraGridAction.EnterEditMode);
						dataGridList.ActiveCell.SelectAll();
					}
				}
				else if (e.KeyCode == Keys.Up && dataGridList.ActiveCell != null && dataGridList.ActiveCell.IsInEditMode && dataGridList.ActiveRow.Index != 0)
				{
					dataGridList.Rows[dataGridList.ActiveRow.Index - 1].Cells[dataGridList.ActiveCell.Column.Index].Activate();
					dataGridList.PerformAction(UltraGridAction.EnterEditMode);
					dataGridList.ActiveCell.SelectAll();
				}
			}
		}

		private void dataGridList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (IsEditMode)
				{
					string text = "";
					if (!(e.Cell.Row.GetType() != typeof(UltraGridRow)))
					{
						UltraGridRow row = e.Cell.Row;
						text = e.Cell.Row.Cells["Item Code"].Text.ToString();
						if (!(text == ""))
						{
							byte num = checked((byte)int.Parse(row.Cells["Type"].Value.ToString()));
							string tableName = "Product";
							string idFieldName = "ProductID";
							if (num == 6)
							{
								tableName = "Product_Parent";
								idFieldName = "ProductParentID";
							}
							if (Factory.DatabaseSystem.UpdateFieldValue(tableName, e.Cell.Column.Key, e.Cell.Value.ToString(), e.Cell.Column.DataType, idFieldName, text) == 0)
							{
								ErrorHelper.WarningMessage("Could not update this value. The item you are trying to update may not be found.");
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ProductListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (IsEditMode)
				{
					IsEditMode = false;
				}
				Global.GlobalSettings.SaveFormProperties(this);
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
				dataGridList.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
		{
			if (dataGridList.Selected.Rows.Count > 1)
			{
				toolTip1.ToolTipTitle = "Selected Items Summary";
				toolTip1.ShowAlways = true;
				toolTip1.AutomaticDelay = 500000;
				toolTip1.AutoPopDelay = 500000;
				toolTip1.ReshowDelay = 500;
				string str = "";
				decimal num = default(decimal);
				decimal result = default(decimal);
				decimal d = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				foreach (UltraGridRow row in dataGridList.Selected.Rows)
				{
					num = default(decimal);
					if (row.Cells["onhand"].Value != null)
					{
						decimal.TryParse(row.Cells["onhand"].Value.ToString(), out num);
					}
					num2 += num;
					if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Value"))
					{
						result = default(decimal);
						if (row.Cells["Value"].Value != null)
						{
							decimal.TryParse(row.Cells["Value"].Value.ToString(), out result);
						}
						d += result;
						num3 += num * result;
					}
				}
				str = str + "Selected Rows:  " + dataGridList.Selected.Rows.Count.ToString();
				str = str + "\nTotal QTY Onhand:  " + num2.ToString(Format.QuantityFormat);
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Avg Cost") && !dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].Hidden)
				{
					if (num2 > 0m)
					{
						result = d / num2;
					}
					str = str + "\nAverage Cost:  " + result.ToString(Format.TotalAmountFormat);
					str = str + "\nValue:  " + Global.CurrencySymbol + " " + num3.ToString(Format.TotalAmountFormat);
				}
				toolTip1.SetToolTip(dataGridList, str);
			}
			else
			{
				toolTip1.SetToolTip(dataGridList, "");
			}
		}

		private void DropDownMenu_Opening(object sender, CancelEventArgs e)
		{
			UltraGridRow selectedItem = GetSelectedItem();
			if (selectedItem == null || !selectedItem.IsDataRow)
			{
				toolStripMenuItemLocationWiseOnhand.Enabled = false;
				matrixQuantityToolStripMenuItem.Enabled = false;
			}
			else if (checked((byte)int.Parse(selectedItem.Cells["Type"].Value.ToString())) == 6)
			{
				matrixQuantityToolStripMenuItem.Visible = true;
				matrixQuantityToolStripMenuItem.Enabled = true;
				toolStripMenuItemLocationWiseOnhand.Visible = false;
			}
			else
			{
				matrixQuantityToolStripMenuItem.Visible = false;
				toolStripMenuItemLocationWiseOnhand.Enabled = true;
				toolStripMenuItemLocationWiseOnhand.Visible = true;
			}
		}

		private void toolStripMenuItemLocationWiseOnhand_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow != null && dataGridList.ActiveRow.Cells["Item Code"].Value != null && dataGridList.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridList.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void dataGridList_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			Point position = Cursor.Position;
			checked
			{
				if (e.Cell.Column.Key == "P" && e.Cell.Value.ToString() == "1")
				{
					string productID = e.Cell.Row.Cells["Item Code"].Value.ToString();
					Image image = ((byte)int.Parse(e.Cell.Row.Cells["Type"].Value.ToString()) != 6) ? PublicFunctions.GetProductThumbnailImage(productID, isProductParentID: false) : PublicFunctions.GetProductThumbnailImage(productID, isProductParentID: true);
					thumbnailViewer1.Image = image;
					thumbnailViewer1.Show(position.X - base.Left, position.Y - base.Top);
				}
				else
				{
					OpenForEdit();
				}
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
			Delete();
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
			New();
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
		}

		private void ProductListForm_Activated(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && productData != null)
			{
				productData.Dispose();
				productData = null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductListForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxZero = new System.Windows.Forms.CheckBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			radioButtonOneLocation = new System.Windows.Forms.RadioButton();
			radioButtonAllLocations = new System.Windows.Forms.RadioButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonDone = new Micromind.UISupport.XPButton();
			buttonOpen = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonShowInactive = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonClearFilter = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonEditMode = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			thumbnailViewer1 = new Micromind.UISupport.GUIControls.Others.ThumbnailViewer();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuItems = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			selectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			totalOnhandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			contextMenuStripDropDown = new System.Windows.Forms.ContextMenuStrip(components);
			matrixQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			inventoryLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			PurchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			availableLotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			flagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripItemClearFlag = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			imageList1 = new System.Windows.Forms.ImageList(components);
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			contextMenuItems.SuspendLayout();
			contextMenuStripDropDown.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(checkBoxZero);
			panelButtons.Controls.Add(comboBoxLocation);
			panelButtons.Controls.Add(radioButtonOneLocation);
			panelButtons.Controls.Add(radioButtonAllLocations);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 434);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(865, 56);
			panelButtons.TabIndex = 1;
			checkBoxZero.AutoSize = true;
			checkBoxZero.Location = new System.Drawing.Point(363, 32);
			checkBoxZero.Name = "checkBoxZero";
			checkBoxZero.Size = new System.Drawing.Size(166, 17);
			checkBoxZero.TabIndex = 18;
			checkBoxZero.Text = "Show items with zero balance";
			checkBoxZero.UseVisualStyleBackColor = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxLocation.Editable = true;
			comboBoxLocation.Enabled = false;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(564, 7);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(117, 20);
			comboBoxLocation.TabIndex = 15;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonOneLocation.AutoSize = true;
			radioButtonOneLocation.Location = new System.Drawing.Point(465, 7);
			radioButtonOneLocation.Name = "radioButtonOneLocation";
			radioButtonOneLocation.Size = new System.Drawing.Size(98, 17);
			radioButtonOneLocation.TabIndex = 17;
			radioButtonOneLocation.Text = "Single Location";
			radioButtonOneLocation.UseVisualStyleBackColor = true;
			radioButtonOneLocation.CheckedChanged += new System.EventHandler(radioButtonOneLocation_CheckedChanged);
			radioButtonAllLocations.AutoSize = true;
			radioButtonAllLocations.Checked = true;
			radioButtonAllLocations.Location = new System.Drawing.Point(363, 7);
			radioButtonAllLocations.Name = "radioButtonAllLocations";
			radioButtonAllLocations.Size = new System.Drawing.Size(85, 17);
			radioButtonAllLocations.TabIndex = 17;
			radioButtonAllLocations.TabStop = true;
			radioButtonAllLocations.Text = "All Locations";
			radioButtonAllLocations.UseVisualStyleBackColor = true;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(865, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 20);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(757, 20);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			buttonOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpen.BackColor = System.Drawing.Color.DarkGray;
			buttonOpen.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpen.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpen.Location = new System.Drawing.Point(12, 20);
			buttonOpen.Name = "buttonOpen";
			buttonOpen.Size = new System.Drawing.Size(96, 24);
			buttonOpen.TabIndex = 1;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 20);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonShowInactive,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripButtonColumnChooser,
				toolStripButtonClearFilter,
				toolStripButtonMerge,
				toolStripSeparator3,
				toolStripButtonEditMode
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(865, 25);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(65, 22);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(49, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonShowInactive.CheckOnClick = true;
			toolStripButtonShowInactive.Image = Micromind.ClientUI.Properties.Resources.ShowInactive;
			toolStripButtonShowInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowInactive.Name = "toolStripButtonShowInactive";
			toolStripButtonShowInactive.Size = new System.Drawing.Size(95, 22);
			toolStripButtonShowInactive.Text = "Show Inactive";
			toolStripButtonShowInactive.Click += new System.EventHandler(toolStripButtonShowInactive_Click);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(98, 22);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(88, 22);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(77, 22);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(67, 22);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonClearFilter.Image = Micromind.ClientUI.Properties.Resources.clearfilter;
			toolStripButtonClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonClearFilter.Name = "toolStripButtonClearFilter";
			toolStripButtonClearFilter.Size = new System.Drawing.Size(23, 22);
			toolStripButtonClearFilter.Text = "Clear All Filters";
			toolStripButtonClearFilter.Click += new System.EventHandler(toolStripButtonClearFilter_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(82, 22);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButtonMerge_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripButtonEditMode.CheckOnClick = true;
			toolStripButtonEditMode.Image = Micromind.ClientUI.Properties.Resources.edit1;
			toolStripButtonEditMode.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonEditMode.Name = "toolStripButtonEditMode";
			toolStripButtonEditMode.Size = new System.Drawing.Size(74, 22);
			toolStripButtonEditMode.Text = "Edit Mode";
			toolStripButtonEditMode.Click += new System.EventHandler(toolStripButtonEditMode_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance13;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.Location = new System.Drawing.Point(12, 37);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(841, 391);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			thumbnailViewer1.BackColor = System.Drawing.Color.White;
			thumbnailViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			thumbnailViewer1.Image = null;
			thumbnailViewer1.Location = new System.Drawing.Point(92, 179);
			thumbnailViewer1.Name = "thumbnailViewer1";
			thumbnailViewer1.Size = new System.Drawing.Size(128, 128);
			thumbnailViewer1.TabIndex = 291;
			thumbnailViewer1.Visible = false;
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			contextMenuItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				toolStripSeparator5,
				selectedRowsToolStripMenuItem,
				totalOnhandToolStripMenuItem
			});
			contextMenuItems.Name = "contextMenuItems";
			contextMenuItems.Size = new System.Drawing.Size(149, 54);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(145, 6);
			selectedRowsToolStripMenuItem.Name = "selectedRowsToolStripMenuItem";
			selectedRowsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			selectedRowsToolStripMenuItem.Text = "Selected Rows:";
			totalOnhandToolStripMenuItem.Name = "totalOnhandToolStripMenuItem";
			totalOnhandToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			totalOnhandToolStripMenuItem.Text = "Total Onhand:";
			toolTip1.AutomaticDelay = 20000;
			toolTip1.AutoPopDelay = 200000;
			toolTip1.InitialDelay = 500;
			toolTip1.ReshowDelay = 4000;
			ultraToolTipManager1.AutoPopDelay = 50000;
			ultraToolTipManager1.ContainingControl = this;
			contextMenuStripDropDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				matrixQuantityToolStripMenuItem,
				inventoryLedgerToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				PurchaseStatisticsToolStripMenuItem,
				availableLotsToolStripMenuItem,
				toolStripSeparator6,
				flagsToolStripMenuItem
			});
			contextMenuStripDropDown.Name = "contextMenuItems";
			contextMenuStripDropDown.Size = new System.Drawing.Size(177, 142);
			contextMenuStripDropDown.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStripDropDown_Opening);
			matrixQuantityToolStripMenuItem.Name = "matrixQuantityToolStripMenuItem";
			matrixQuantityToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			matrixQuantityToolStripMenuItem.Text = "Matrix Quantity...";
			matrixQuantityToolStripMenuItem.Click += new System.EventHandler(matrixQuantityToolStripMenuItem_Click);
			inventoryLedgerToolStripMenuItem.Name = "inventoryLedgerToolStripMenuItem";
			inventoryLedgerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			inventoryLedgerToolStripMenuItem.Text = "Inventory Ledger...";
			inventoryLedgerToolStripMenuItem.Click += new System.EventHandler(inventoryLedgerToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Chart_48;
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			salesStatisticsToolStripMenuItem.Click += new System.EventHandler(salesStatisticsToolStripMenuItem_Click);
			PurchaseStatisticsToolStripMenuItem.Name = "PurchaseStatisticsToolStripMenuItem";
			PurchaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			PurchaseStatisticsToolStripMenuItem.Text = "Purchase Statistics...";
			PurchaseStatisticsToolStripMenuItem.Visible = false;
			PurchaseStatisticsToolStripMenuItem.Click += new System.EventHandler(PurchaseStatisticsToolStripMenuItem_Click);
			availableLotsToolStripMenuItem.Name = "availableLotsToolStripMenuItem";
			availableLotsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			availableLotsToolStripMenuItem.Text = "Available Lots...";
			availableLotsToolStripMenuItem.Click += new System.EventHandler(availableLotsToolStripMenuItem_Click);
			flagsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				toolStripSeparator7,
				toolStripItemClearFlag
			});
			flagsToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.colorcategory;
			flagsToolStripMenuItem.Name = "flagsToolStripMenuItem";
			flagsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			flagsToolStripMenuItem.Text = "Flags";
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(149, 6);
			toolStripItemClearFlag.Name = "toolStripItemClearFlag";
			toolStripItemClearFlag.Size = new System.Drawing.Size(152, 22);
			toolStripItemClearFlag.Tag = "0";
			toolStripItemClearFlag.Text = "Clear Flag";
			toolStripItemClearFlag.Click += new System.EventHandler(toolStripItemFlag_Click);
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(23, 23);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "followup.gif");
			imageList1.Images.SetKeyName(1, "colorcategory.png");
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(173, 6);
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(865, 490);
			base.Controls.Add(thumbnailViewer1);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ProductListForm";
			Text = "Item List";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			contextMenuItems.ResumeLayout(false);
			contextMenuStripDropDown.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			Global.GlobalSettings.LoadFormProperties(this);
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					string locationID = "";
					if (radioButtonOneLocation.Checked)
					{
						locationID = comboBoxLocation.SelectedID;
					}
					productData = Factory.ProductSystem.GetProductList(toolStripButtonShowInactive.Checked, checkBoxZero.Checked, locationID);
					dataGridList.DataSource = productData;
					if (!canAccessCost && productData.Tables[0].Columns.Contains("Avg Cost"))
					{
						productData.Tables[0].Columns.Remove("Avg Cost");
						if (productData.Tables[0].Columns.Contains("Value"))
						{
							productData.Tables[0].Columns.Remove("Value");
						}
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1) && productData.Tables[0].Columns.Contains("UnitPrice1"))
					{
						productData.Tables[0].Columns.Remove("UnitPrice1");
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2) && productData.Tables[0].Columns.Contains("UnitPrice2"))
					{
						productData.Tables[0].Columns.Remove("UnitPrice2");
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3) && productData.Tables[0].Columns.Contains("UnitPrice3"))
					{
						productData.Tables[0].Columns.Remove("UnitPrice3");
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) && productData.Tables[0].Columns.Contains("MinPrice"))
					{
						productData.Tables[0].Columns.Remove("MinPrice");
					}
					foreach (UltraGridRow row in dataGridList.Rows)
					{
						if (row.IsGroupByRow)
						{
							foreach (UltraGridChildBand item in (IEnumerable)row.ChildBands)
							{
								foreach (UltraGridRow row2 in item.Rows)
								{
									AdjustGridRow(row2);
								}
							}
						}
						else
						{
							AdjustGridRow(row);
						}
					}
					if (isFirstTimeLoading)
					{
						SetupGrid();
					}
					isFirstTimeLoading = false;
				}
				catch (SqlException ex)
				{
					dataGridList.LoadLayoutFailed = true;
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					dataGridList.LoadLayoutFailed = true;
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void AdjustGridRow(UltraGridRow row)
		{
			try
			{
				int result = 1;
				int.TryParse(row.Cells["Type"].Value.ToString(), out result);
				if (result == 6)
				{
					row.Appearance.FontData.Bold = DefaultableBoolean.True;
				}
			}
			catch
			{
				throw;
			}
		}

		private void HideShowColumns()
		{
			if ((dataGridList.DisplayLayout == null || dataGridList.DisplayLayout.Bands.Count >= 1) && dataGridList.DisplayLayout.Bands[0].Columns.Count != 0)
			{
				_ = dataGridList.DisplayLayout.Bands[0].Columns.Count;
				dataGridList.DisplayLayout.Bands[0].Columns["UPC"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Manufacturer"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Brand"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute1"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute2"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute3"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["QuantityPerUnit"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Weight"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["ReorderLevel"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Age"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Class"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["TotalWeight"].Hidden = true;
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].Hidden = true;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].Hidden = true;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].Hidden = true;
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Description"].Width = 200;
			}
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				int result = 1;
				int.TryParse(dataGridList.ActiveRow.Cells["Type"].Value.ToString(), out result);
				if (checked((byte)result) == 6)
				{
					FormActivator.BringFormToFront(FormActivator.MatrixProductDetailsFormObj);
					FormActivator.MatrixProductDetailsFormObj.LoadData(selectedID);
				}
				else
				{
					FormActivator.ProductDetailsFormObj.LoadData(selectedID);
					FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
				}
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			New();
		}

		private void New()
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			if (!IsEditMode)
			{
				OpenForEdit();
			}
		}

		private void ProductListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					if (Factory.IsDBConnected)
					{
						dataGridList.ApplyUIDesign();
						LoadData();
					}
				}
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupGrid()
		{
			try
			{
				if (Factory.IsDBConnected)
				{
					LoadFlags();
					HideShowColumns();
					dataGridList.LoadLayout();
					AdjustGridColumnSettings();
				}
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadFlags()
		{
			try
			{
				flagData = Factory.EntityFlagSystem.GetEntityFlagList(EntityTypesEnum.Items);
				int num = 0;
				foreach (DataRow row in flagData.Tables[0].Rows)
				{
					Bitmap image = new Bitmap(12, 16);
					using (Graphics graphics = Graphics.FromImage(image))
					{
						graphics.Clear(Color.FromArgb(int.Parse(row["Color"].ToString())));
					}
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(row["Name"].ToString(), image);
					toolStripMenuItem.CheckOnClick = true;
					toolStripMenuItem.Click += FlagItem_Click;
					flagsToolStripMenuItem.DropDownItems.Insert(num, toolStripMenuItem);
					num = checked(num + 1);
					toolStripMenuItem.Tag = new NameValue(row["Code"].ToString(), row["Color"].ToString());
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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
			if (!Security.GetScreenAccessRight("ProductDetailsForm").Edit)
			{
				toolStripButtonEditMode.Enabled = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.Allowflagitems))
			{
				allowflagitems = false;
			}
			else
			{
				allowflagitems = true;
			}
		}

		public void OnActivated()
		{
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Products;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private void AdjustGridColumnSettings()
		{
			if ((dataGridList.DisplayLayout == null || dataGridList.DisplayLayout.Bands.Count >= 1) && dataGridList.DisplayLayout.Bands[0].Columns.Count != 0)
			{
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("ReorderLevel"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["ReorderLevel"].Header.Caption = "Reorder Level";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("VendorRef"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].Header.Caption = "VendorRef";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("QuantityPerUnit"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["QuantityPerUnit"].Header.Caption = "Qty Per Unit";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice1"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].Header.Caption = CompanyPreferences.UnitPrice1Title;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].Header.Caption = CompanyPreferences.UnitPrice2Title;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].Header.Caption = CompanyPreferences.UnitPrice3Title;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].Header.Caption = "Min Price";
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridList.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Header.Caption = "Last Sale";
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["F"];
				ultraGridColumn.Header.Caption = "";
				ultraGridColumn.Header.Appearance.Image = imageList1.Images[1];
				ultraGridColumn.Header.Appearance.ImageVAlign = VAlign.Middle;
				ultraGridColumn.Hidden = false;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].CellDisplayStyle = CellDisplayStyle.FormattedText;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].Width = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].MaxWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].MinWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].LockedWidth = true;
				dataGridList.DisplayLayout.Bands[0].Columns["I"].AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.SetInactiveColumn("I");
				dataGridList.DisplayLayout.Bands[0].Columns["P"].CellDisplayStyle = CellDisplayStyle.FormattedText;
				dataGridList.DisplayLayout.Bands[0].Columns["P"].MaxWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["P"].MinWidth = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["P"].LockedWidth = true;
				dataGridList.DisplayLayout.Bands[0].Columns["P"].AllowRowSummaries = AllowRowSummaries.False;
				dataGridList.DisplayLayout.Bands[0].Columns["P"].AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.SetPhotoColumn("P");
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(0, "None");
				valueList.ValueListItems.Add(1, "Inventory");
				valueList.ValueListItems.Add(2, "Non-Inventory");
				valueList.ValueListItems.Add(3, "Service");
				valueList.ValueListItems.Add(4, "Discount");
				valueList.ValueListItems.Add(5, "Consignment");
				valueList.ValueListItems.Add(6, "Matrix");
				valueList.ValueListItems.Add(7, "Assembly");
				valueList.ValueListItems.Add(8, "Project Fee");
				valueList.ValueListItems.Add(9, "Inventory 3PL");
				dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList;
				dataGridList.DisplayLayout.Bands[0].Columns["Type"].AllowRowSummaries = AllowRowSummaries.False;
				dataGridList.DisplayLayout.Bands[0].Columns["Onhand"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].Format = "#,##0 Days";
				dataGridList.DisplayLayout.Bands[0].Columns["LastSale"].NullText = "None";
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice1"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].CellAppearance.TextHAlign = HAlign.Right;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].CellAppearance.TextHAlign = HAlign.Right;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].CellAppearance.TextHAlign = HAlign.Right;
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].CellAppearance.TextHAlign = HAlign.Right;
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridList.DisplayLayout.Bands[0].Columns["UPC"].MaxLength = 20;
				dataGridList.DisplayLayout.Bands[0].Columns["VendorRef"].MaxLength = 15;
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Avg Cost"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].Format = "n";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("Value"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Value"].CellAppearance.TextHAlign = HAlign.Right;
					dataGridList.DisplayLayout.Bands[0].Columns["Value"].Format = "n";
					dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Value"], addSummary: true);
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Onhand"].Format = "n";
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice1"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice1"].Format = "#,##0.00##";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice2"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice2"].Format = "#,##0.00##";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("UnitPrice3"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["UnitPrice3"].Format = "#,##0.00##";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("MinPrice"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["MinPrice"].Format = "#,##0.00##";
				}
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("TotalWeight"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["TotalWeight"].Format = "#,##0.00";
				}
				dataGridList.ApplyQuantityColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Onhand"], addSummary: true);
				dataGridList.AddColumnRowCount(dataGridList.DisplayLayout.Bands[0].Columns["Item Code"]);
				dataGridList.DisplayLayout.Bands[0].Columns["F"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].Width = 18;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].MaxWidth = 80;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].MinWidth = 80;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].LockedWidth = false;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.DisplayLayout.Bands[0].Override.FixedHeaderIndicator = FixedHeaderIndicator.Button;
				dataGridList.DisplayLayout.UseFixedHeaders = true;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].AllowRowFiltering = DefaultableBoolean.True;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].FilterComparisonStyle = FilterComparisonStyle.ValueOrDisplayText;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].FilterOperatorLocation = FilterOperatorLocation.Hidden;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].FilterClearButtonVisible = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Bands[0].Columns["F"].AllowRowSummaries = AllowRowSummaries.False;
				if (!allowflagitems)
				{
					flagsToolStripMenuItem.Visible = false;
				}
				else
				{
					flagsToolStripMenuItem.Visible = true;
				}
			}
		}

		private string GetSelectedID()
		{
			string result = "";
			if (dataGridList.ActiveRow == null)
			{
				return "";
			}
			dataGridList.ActiveRow.GetType();
			if (dataGridList.ActiveRow != null)
			{
				if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
				{
					return "";
				}
				result = dataGridList.ActiveRow.Cells["Item Code"].Text.ToString();
			}
			return result;
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridList.ActiveRow != null)
			{
				return dataGridList.ActiveRow;
			}
			return null;
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
			if (!isReadOnly)
			{
				string selectedID = GetSelectedID();
				if (selectedID != "")
				{
					try
					{
						if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.Yes)
						{
							try
							{
								if (Factory.ProductSystem.GetProductTransactionsExists(selectedID))
								{
									throw new CompanyException("Some transactions already done with this item. You can't delete ");
								}
								PublicFunctions.StartWaiting(this);
								if (Factory.ProductSystem.DeleteProduct(selectedID))
								{
									ComboDataHelper.SetRefreshStatus(DataComboType.Product, needRefresh: true);
									try
									{
										GetSelectedItem().Delete(displayPrompt: false);
									}
									catch
									{
									}
								}
							}
							catch (SqlException ex)
							{
								ErrorHelper.ProcessError(ex);
							}
						}
					}
					catch (SqlException ex2)
					{
						if (ex2.Number == 547)
						{
							ErrorHelper.ErrorMessage("Cannot delete this item because it is in use or referenced by other records.");
						}
						else
						{
							ErrorHelper.ProcessError(ex2);
						}
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
					finally
					{
						PublicFunctions.EndWaiting(this);
					}
				}
			}
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Item List";
		}

		private void Print()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public bool SaveReport(ExternalReportTypes reportType)
		{
			return true;
		}

		public void ClearView()
		{
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new DataExportHelper().ExportToExcel(dataGridList);
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridList.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridList.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridList.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
		}

		private void radioButtonOneLocation_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxLocation.Enabled = radioButtonOneLocation.Checked;
		}

		private void matrixQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			MatrixQuantityForm matrixQuantityForm = new MatrixQuantityForm();
			matrixQuantityForm.LoadData(selectedID, GetSelectedItem().Cells["Description"].Value.ToString());
			matrixQuantityForm.ShowDialog();
		}

		private void toolStripButtonEditMode_Click(object sender, EventArgs e)
		{
			IsEditMode = toolStripButtonEditMode.Checked;
		}

		private void inventoryLedgerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				InventoryLedgerForm inventoryLedgerForm = new InventoryLedgerForm();
				inventoryLedgerForm.SelectedID = selectedID;
				inventoryLedgerForm.Show();
				inventoryLedgerForm.BringToFront();
			}
		}

		private void toolStripButtonMerge_Click(object sender, EventArgs e)
		{
			if (toolStripButtonMerge.Checked)
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.OnlyWhenSorted;
			}
			else
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Never;
			}
		}

		private void availableLotsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			ProductLotDetailsDialog productLotDetailsDialog = new ProductLotDetailsDialog();
			productLotDetailsDialog.ProductID = selectedID;
			productLotDetailsDialog.Description = GetSelectedItem().Cells["Description"].Value.ToString();
			productLotDetailsDialog.LoadData(selectedID);
			productLotDetailsDialog.ShowDialog(this);
		}

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
			if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
			{
				dataGridList.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
			}
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
				inventorySalesStatisticForm.SelectedID = selectedID;
				inventorySalesStatisticForm.Show();
				inventorySalesStatisticForm.BringToFront();
			}
		}

		private void PurchaseStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				InventoryPurchasesStatisticForm inventoryPurchasesStatisticForm = new InventoryPurchasesStatisticForm();
				inventoryPurchasesStatisticForm.SelectedID = selectedID;
				inventoryPurchasesStatisticForm.Show();
				inventoryPurchasesStatisticForm.BringToFront();
			}
		}

		private void toolStripItemFlag_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Remove all flags?") == DialogResult.Yes && Factory.EntityFlagSystem.SetFlag(25, GetSelectedID(), -1, removeFlag: true))
				{
					DataRow[] array = productData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND EntityType = 25");
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						productData.Tables["Entity_Flag_Detail"].Rows.Remove(array[i]);
					}
					SetRowFlags(dataGridList.ActiveRow);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void contextMenuStripDropDown_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				if (GetSelectedItem() != null)
				{
					foreach (object dropDownItem in flagsToolStripMenuItem.DropDownItems)
					{
						if (!(dropDownItem.GetType() == typeof(ToolStripSeparator)))
						{
							ToolStripMenuItem toolStripMenuItem = dropDownItem as ToolStripMenuItem;
							int num = int.Parse(toolStripMenuItem.Tag.ToString());
							if (productData.Tables["Entity_Flag_Detail"].Select("EntityID = '" + GetSelectedID() + "' AND FlagID = " + num).Length == 0)
							{
								toolStripMenuItem.Checked = false;
							}
							else
							{
								toolStripMenuItem.Checked = true;
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
