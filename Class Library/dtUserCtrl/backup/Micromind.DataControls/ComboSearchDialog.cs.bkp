using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ComboSearchDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private bool loadItemDetails = CompanyPreferences.LoadItemFeatures;

		private string selectedItem = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private TextBox textBoxSearch;

		private Label label1;

		private MMSListGrid dataGridItems;

		private Panel panelItemdetails;

		private Panel panel1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label labelStock;

		private Label labelCountry;

		private Label labelManufacturer;

		private Label labelBrand;

		private Label labelCategory;

		private Label labelUnit;

		private ProductPhotoViewer productPhotoViewer;

		private RadioButton radioButton1;

		private GridControl gridControl1;

		private GridView itemGridView;

		private TextBox textBoxFilter;

		private System.Windows.Forms.ComboBox comboBoxFilterConditions;

		private TextBox textBoxCondition;

		private System.Windows.Forms.ComboBox comboBoxColumn;

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
					gridControl1.DataSource = value.Tables[0];
					HideOtherColumns();
					AddColumnsForCondition();
					GetSavedRegistrySettings();
					dataGridItems.DataSource = value;
					dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
					dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
					foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (column2.Key.ToLower() != "name" && column2.Key.ToLower() != "code" && column2.Key.ToLower() != "description" && column2.Key.ToLower() != "alias" && column2.Key.ToLower() != "class" && column2.Key.ToLower() != "category" && column2.Key.ToLower() != "styleid" && column2.Key.ToLower() != "brand" && column2.Key.ToLower() != "total orderedqty" && column2.Key.ToLower() != "exp.deliverydate")
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

		public ComboSearchDialog()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			itemGridView.DoubleClick += gridView_DoubleClick;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			_ = loadItemDetails;
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
			textBoxFilter.Focus();
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

		private void GridControlLayout()
		{
			try
			{
				string xmlFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\ComboSearchDialogGridLayout.xml";
				gridControl1.MainView.RestoreLayoutFromXml(xmlFile);
			}
			catch
			{
			}
		}

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			dataGridItems.ApplyUIDesign();
			dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			GridControlLayout();
			if (DataSource.Tables.Contains("Products"))
			{
				dataGridItems.Width = 757;
				base.Width = 1047;
			}
			textBoxSearch.Text = SelectedItem.Trim();
			textBoxFilter.Focus();
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (itemGridView.SelectedRowsCount > 0)
			{
				return itemGridView.GetFocusedRowCellValue("Code").ToString();
			}
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			if (DataSource != null && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridItems.BeginUpdate();
				if (dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				string text = textBoxSearch.Text;
				string[] separator = new string[1]
				{
					"%"
				};
				string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				foreach (string compareValue in array)
				{
					dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = compareValue;
				}
				dataGridItems.EndUpdate();
				if (dataGridItems.Rows.VisibleRowCount > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(0);
				}
			}
		}

		private void HideOtherColumns()
		{
			foreach (GridColumn column in itemGridView.Columns)
			{
				if (column.FieldName.ToLower() == "name" || column.FieldName.ToLower() == "code" || column.FieldName.ToLower() == "description" || column.FieldName.ToLower() == "alias" || column.FieldName.ToLower() == "class" || column.FieldName.ToLower() == "category" || column.FieldName.ToLower() == "styleid" || column.FieldName.ToLower() == "brand" || column.FieldName.ToLower() == "total orderedqty" || column.FieldName.ToLower() == "exp.deliverydate")
				{
					column.Visible = true;
				}
				else
				{
					column.Visible = false;
				}
			}
		}

		private void AddColumnsForCondition()
		{
			foreach (GridColumn column in itemGridView.Columns)
			{
				if (column.Visible)
				{
					comboBoxColumn.Items.Add(column.FieldName);
				}
			}
			if (itemGridView.Columns.Contains(itemGridView.Columns["Name"]))
			{
				comboBoxColumn.SelectedItem = "Name";
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

		private void ClearForm()
		{
			labelUnit.Text = "";
			labelCategory.Text = "";
			labelBrand.Text = "";
			labelManufacturer.Text = "";
			labelCountry.Text = "";
			labelStock.Text = "";
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
		}

		public string GetSelectedItem(string codeColumnName)
		{
			if (itemGridView.SelectedRowsCount > 0)
			{
				return itemGridView.GetFocusedRowCellValue("Code").ToString();
			}
			return "";
		}

		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void textBoxFilter_TextChanged(object sender, EventArgs e)
		{
			Filter();
		}

		private void Filter()
		{
			string text = textBoxFilter.Text;
			string[] separator = new string[1]
			{
				" "
			};
			itemGridView.ActiveFilterString = "";
			string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			string text2 = "";
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				text2 = ((array.Length != 1 && !(text3 == array[array.Length - 1])) ? (text2 + "Contains([SearchColumn], '" + text3 + "') And ") : (text2 + "Contains([SearchColumn], '" + text3 + "')"));
			}
			GridView gridView = itemGridView;
			ViewColumnFilterInfo columnInfo = new ViewColumnFilterInfo(gridView.Columns["SearchColumn"], new ColumnFilterInfo(text2, ""));
			gridView.ActiveFilter.Add(columnInfo);
			if (text == "")
			{
				itemGridView.ActiveFilterString = "";
			}
		}

		private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			GridView gridView = (GridView)sender;
			if (!gridView.OptionsView.ShowAutoFilterRow || !gridView.IsDataRow(e.RowHandle))
			{
				return;
			}
			string text = "BEEF";
			if (!string.IsNullOrEmpty(text))
			{
				int num = e.DisplayText.IndexOf(text, StringComparison.CurrentCultureIgnoreCase);
				if (num != -1)
				{
					e.Appearance.FillRectangle(e.Cache, e.Bounds);
					e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, text, e.Appearance, Color.Black, Color.Gold, invert: false, num);
					e.Handled = true;
				}
			}
		}

		public static void CustomDrawCell(GridControl gridControl, GridView gridView)
		{
			gridView.CustomDrawCell += delegate(object s, RowCellCustomDrawEventArgs e)
			{
				if (e.Column.VisibleIndex == 2)
				{
					e.Appearance.FillRectangle(e.Cache, e.Bounds);
					e.Cache.Paint.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, "BEEF", e.Appearance, Color.Black, Color.Gold, invert: false);
					e.Cache.FillRectangle(Color.Salmon, e.Bounds);
					e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
					e.Handled = true;
				}
			};
		}

		private void textBoxCondition_TextChanged(object sender, EventArgs e)
		{
			FilterByCondition();
		}

		private void ComboSearchDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (comboBoxFilterConditions.SelectedIndex >= 0)
			{
				Global.SaveRegistryOptionValue("FilterCondition", comboBoxFilterConditions.SelectedItem.ToString(), isEncrypt: true);
			}
			if (comboBoxColumn.SelectedIndex >= 0)
			{
				Global.SaveRegistryOptionValue("FilterColumn", comboBoxColumn.SelectedItem.ToString(), isEncrypt: true);
			}
			try
			{
				string xmlFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\Axolon Local Settings\\ComboSearchDialogGridLayout.xml";
				gridControl1.MainView.SaveLayoutToXml(xmlFile);
			}
			catch
			{
			}
		}

		private void GetSavedRegistrySettings()
		{
			string registryOptionValue = Global.GetRegistryOptionValue("FilterCondition", isEncrypt: true);
			comboBoxFilterConditions.SelectedItem = registryOptionValue;
			string registryOptionValue2 = Global.GetRegistryOptionValue("FilterColumn", isEncrypt: true);
			if (registryOptionValue2 != "" && registryOptionValue2 != null && itemGridView.Columns.Contains(itemGridView.Columns[registryOptionValue2]))
			{
				comboBoxColumn.SelectedItem = registryOptionValue2;
			}
		}

		private void FilterByCondition()
		{
			string text = textBoxCondition.Text;
			string text2 = "";
			string text3 = comboBoxColumn.SelectedItem.ToString();
			itemGridView.ActiveFilterString = "";
			if (comboBoxFilterConditions.SelectedIndex >= 0 && text != "")
			{
				string a = comboBoxFilterConditions.SelectedItem.ToString();
				if (a == "Begin with")
				{
					text2 = "StartsWith([" + text3 + "], '" + text + "')";
				}
				else if (a == "Contains")
				{
					text2 = "Contains([" + text3 + "], '" + text + "')";
				}
				else if (a == "Ends with")
				{
					text2 = "EndsWith([" + text3 + "], '" + text + "')";
				}
				else if (a == "Is like")
				{
					text2 = "[" + text3 + "] Like '" + text + "'";
				}
				else if (a == "Is any of")
				{
					text2 = "[" + text3 + "] In ('" + text + "')";
				}
			}
			GridView gridView = itemGridView;
			ViewColumnFilterInfo columnInfo = new ViewColumnFilterInfo(gridView.Columns[text3], new ColumnFilterInfo(text2, ""));
			gridView.ActiveFilter.Add(columnInfo);
			if (text2 == "")
			{
				itemGridView.ActiveFilterString = "";
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			FilterByCondition();
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
			panelButtons = new System.Windows.Forms.Panel();
			comboBoxColumn = new System.Windows.Forms.ComboBox();
			textBoxCondition = new System.Windows.Forms.TextBox();
			comboBoxFilterConditions = new System.Windows.Forms.ComboBox();
			textBoxFilter = new System.Windows.Forms.TextBox();
			gridControl1 = new DevExpress.XtraGrid.GridControl();
			itemGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
			radioButton1 = new System.Windows.Forms.RadioButton();
			panelItemdetails = new System.Windows.Forms.Panel();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			labelStock = new System.Windows.Forms.Label();
			labelCountry = new System.Windows.Forms.Label();
			labelManufacturer = new System.Windows.Forms.Label();
			labelBrand = new System.Windows.Forms.Label();
			labelCategory = new System.Windows.Forms.Label();
			labelUnit = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			textBoxSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)itemGridView).BeginInit();
			panelItemdetails.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(comboBoxColumn);
			panelButtons.Controls.Add(textBoxCondition);
			panelButtons.Controls.Add(comboBoxFilterConditions);
			panelButtons.Controls.Add(textBoxFilter);
			panelButtons.Controls.Add(gridControl1);
			panelButtons.Controls.Add(radioButton1);
			panelButtons.Controls.Add(panelItemdetails);
			panelButtons.Controls.Add(dataGridItems);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 2);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(776, 544);
			panelButtons.TabIndex = 3;
			comboBoxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxColumn.FormattingEnabled = true;
			comboBoxColumn.Location = new System.Drawing.Point(139, 40);
			comboBoxColumn.Name = "comboBoxColumn";
			comboBoxColumn.Size = new System.Drawing.Size(127, 21);
			comboBoxColumn.TabIndex = 21;
			textBoxCondition.Location = new System.Drawing.Point(268, 41);
			textBoxCondition.Name = "textBoxCondition";
			textBoxCondition.Size = new System.Drawing.Size(331, 20);
			textBoxCondition.TabIndex = 20;
			textBoxCondition.TextChanged += new System.EventHandler(textBoxCondition_TextChanged);
			comboBoxFilterConditions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFilterConditions.FormattingEnabled = true;
			comboBoxFilterConditions.Items.AddRange(new object[5]
			{
				"Contains",
				"Begin with",
				"Ends with",
				"Is like",
				"Is any of"
			});
			comboBoxFilterConditions.Location = new System.Drawing.Point(10, 40);
			comboBoxFilterConditions.Name = "comboBoxFilterConditions";
			comboBoxFilterConditions.Size = new System.Drawing.Size(127, 21);
			comboBoxFilterConditions.TabIndex = 19;
			textBoxFilter.Location = new System.Drawing.Point(10, 18);
			textBoxFilter.Name = "textBoxFilter";
			textBoxFilter.Size = new System.Drawing.Size(589, 20);
			textBoxFilter.TabIndex = 18;
			textBoxFilter.TextChanged += new System.EventHandler(textBoxFilter_TextChanged);
			gridControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			gridControl1.Location = new System.Drawing.Point(10, 63);
			gridControl1.MainView = itemGridView;
			gridControl1.Name = "gridControl1";
			gridControl1.Size = new System.Drawing.Size(762, 477);
			gridControl1.TabIndex = 16;
			gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1]
			{
				itemGridView
			});
			itemGridView.GridControl = gridControl1;
			itemGridView.Name = "itemGridView";
			itemGridView.OptionsBehavior.Editable = false;
			itemGridView.OptionsCustomization.AllowGroup = false;
			itemGridView.OptionsFilter.DefaultFilterEditorView = DevExpress.XtraEditors.FilterEditorViewMode.TextAndVisual;
			itemGridView.OptionsFind.ClearFindOnClose = false;
			itemGridView.OptionsFind.ShowCloseButton = false;
			itemGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			itemGridView.OptionsView.ShowGroupPanel = false;
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new System.Drawing.Point(576, 120);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(74, 17);
			radioButton1.TabIndex = 15;
			radioButton1.TabStop = true;
			radioButton1.Text = "Advanced";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton1.Visible = false;
			panelItemdetails.Controls.Add(panel1);
			panelItemdetails.Location = new System.Drawing.Point(775, 46);
			panelItemdetails.Name = "panelItemdetails";
			panelItemdetails.Size = new System.Drawing.Size(244, 427);
			panelItemdetails.TabIndex = 7;
			panelItemdetails.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(productPhotoViewer);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(labelStock);
			panel1.Controls.Add(labelCountry);
			panel1.Controls.Add(labelManufacturer);
			panel1.Controls.Add(labelBrand);
			panel1.Controls.Add(labelCategory);
			panel1.Controls.Add(labelUnit);
			panel1.Location = new System.Drawing.Point(7, 2);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(221, 179);
			panel1.TabIndex = 122;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(6, 159);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 15;
			label2.Text = "Cost :";
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(9, 192);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 250);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 121;
			productPhotoViewer.Visible = false;
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
			labelStock.AutoSize = true;
			labelStock.Location = new System.Drawing.Point(99, 136);
			labelStock.Name = "labelStock";
			labelStock.Size = new System.Drawing.Size(0, 13);
			labelStock.TabIndex = 6;
			labelCountry.AutoSize = true;
			labelCountry.Location = new System.Drawing.Point(72, 111);
			labelCountry.Name = "labelCountry";
			labelCountry.Size = new System.Drawing.Size(0, 13);
			labelCountry.TabIndex = 5;
			labelManufacturer.AutoSize = true;
			labelManufacturer.Location = new System.Drawing.Point(97, 86);
			labelManufacturer.Name = "labelManufacturer";
			labelManufacturer.Size = new System.Drawing.Size(0, 13);
			labelManufacturer.TabIndex = 4;
			labelBrand.AutoSize = true;
			labelBrand.Location = new System.Drawing.Point(72, 59);
			labelBrand.Name = "labelBrand";
			labelBrand.Size = new System.Drawing.Size(0, 13);
			labelBrand.TabIndex = 3;
			labelCategory.AutoSize = true;
			labelCategory.Location = new System.Drawing.Point(72, 35);
			labelCategory.Name = "labelCategory";
			labelCategory.Size = new System.Drawing.Size(0, 13);
			labelCategory.TabIndex = 2;
			labelUnit.AutoSize = true;
			labelUnit.Location = new System.Drawing.Point(72, 8);
			labelUnit.Name = "labelUnit";
			labelUnit.Size = new System.Drawing.Size(0, 13);
			labelUnit.TabIndex = 1;
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
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(10, 104);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(762, 192);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.Visible = false;
			dataGridItems.AfterRowActivate += new System.EventHandler(dataGridItems_AfterRowActivate);
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(1121, 512);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
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
			linePanelDown.Size = new System.Drawing.Size(776, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(1226, 512);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
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
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(776, 546);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ComboSearchDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Item";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ComboSearchDialog_FormClosing);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)itemGridView).EndInit();
			panelItemdetails.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
