using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataCaches;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class MultiColumnComboBox : UltraCombo, ICustomReportControl
	{
		private string baseFilter = "";

		internal bool allowQuickAdd = true;

		internal bool allowSort = true;

		internal string TABLENAME_FIELD = "";

		internal string ID_FIELD = "";

		internal string ID2_FIELD = "";

		internal string ID2VALUE_FIELD = "";

		internal string NAME_FIELD = "";

		private TextBox descriptionTextBox;

		private string oldValue = "";

		private bool ignoreSpace;

		private DataComboType comboType;

		internal bool isDataLoaded;

		private bool LoadItemFeatures;

		private bool ActivatePartsDetails;

		private bool tradingBaseonB2C;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private string comboKeyUpText = "";

		private bool keyDownUpPressed;

		private bool isUpdating;

		private bool ignoreDropDown;

		public bool IsLoadingData;

		private bool ValAssigned;

		private string setval = "";

		internal string lastSelectedID = "";

		private DataView dataView = new DataView();

		private IContainer components;

		private UltraToolTipManager ultraToolTipManager1;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public override bool AutoSize
		{
			get
			{
				return true;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string OldValue
		{
			get
			{
				return oldValue;
			}
			set
			{
				oldValue = value;
			}
		}

		internal DataComboType ComboType
		{
			get
			{
				return comboType;
			}
			set
			{
				comboType = value;
			}
		}

		public string FilterString
		{
			get
			{
				return baseFilter;
			}
			set
			{
				baseFilter = value;
			}
		}

		public TextBox DescriptionTextBox
		{
			get
			{
				return descriptionTextBox;
			}
			set
			{
				descriptionTextBox = value;
			}
		}

		public bool Assigned
		{
			get
			{
				return ValAssigned;
			}
			set
			{
				ValAssigned = value;
			}
		}

		internal new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
				ApplyBandSettings();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedName
		{
			get
			{
				if (base.SelectedRow != null && SelectedID != "")
				{
					return base.SelectedRow.Cells["Name"].Text.ToString();
				}
				return "";
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedID
		{
			get
			{
				if (base.Rows.Count == 0)
				{
					return "";
				}
				if (base.SelectedRow == null || base.SelectedRow.Cells[0].Value == null)
				{
					if (Text == "")
					{
						return "";
					}
					Focus();
					throw new CompanyException("Entered text is not a valid item in the list. Please select correct item.");
				}
				return base.SelectedRow.Cells["Code"].Value.ToString();
			}
			set
			{
				if (value != "" && !IsDataLoaded)
				{
					if (TABLENAME_FIELD != "" && ID_FIELD != "" && NAME_FIELD != "")
					{
						GetComboRowByID(value);
					}
					else
					{
						LoadData();
					}
				}
				if (base.DisplayMember == "Code" || base.DisplayMember == "")
				{
					base.Value = value;
					Text = value;
				}
				else
				{
					base.Value = value;
				}
				if (Text != "" && base.SelectedRow == null && TABLENAME_FIELD != "" && ID_FIELD != "" && NAME_FIELD != "")
				{
					try
					{
						DataSet dataSet = (!(ID2_FIELD != "")) ? Factory.DatabaseSystem.GetComboRowByID(TABLENAME_FIELD, ID_FIELD, value, NAME_FIELD) : Factory.DatabaseSystem.GetComboRowByID(TABLENAME_FIELD, ID_FIELD, value, ID2_FIELD, ID2VALUE_FIELD, NAME_FIELD);
						if (!dataSet.Tables[0].Columns.Contains("SearchColumn"))
						{
							dataSet.Tables[0].Columns.Add("SearchColumn");
						}
						if (!dataSet.Tables[0].Columns.Contains("UPC"))
						{
							dataSet.Tables[0].Columns.Add("UPC");
						}
						if (!dataSet.Tables[0].Columns.Contains("SearchUPC"))
						{
							dataSet.Tables[0].Columns.Add("SearchUPC");
						}
						if (DataSource != null)
						{
							if (DataSource.GetType() == typeof(DataView))
							{
								((DataView)DataSource).Table.Merge(dataSet.Tables[0]);
							}
							else
							{
								((DataSet)DataSource).Tables[0].Merge(dataSet.Tables[0]);
							}
						}
						else
						{
							DataSource = dataSet.Tables[0];
						}
						ComboDataHelper.SetRefreshStatus(comboType, needRefresh: true);
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
				}
			}
		}

		public RowsCollection Items => base.Rows;

		public bool Editable
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				return -1;
			}
			set
			{
				try
				{
					base.Rows[value].Selected = true;
				}
				catch
				{
				}
			}
		}

		public bool IsDataLoaded
		{
			get
			{
				return isDataLoaded;
			}
			set
			{
				isDataLoaded = value;
			}
		}

		public event EventHandler SelectedIndexChanged;

		public event CancelEventHandler BeforeSelectedIndexChanged;

		public MultiColumnComboBox()
		{
			InitializeComponent();
			base.ValueChanged += MultiColumnComboBox_ValueChanged;
			base.BeforeDropDown += MultiColumnComboBox_BeforeDropDown;
			base.KeyDown += MultiColumnComboBox_KeyDown;
			base.DropDownSearchMethod = DropDownSearchMethod.Linear;
			base.RowSelected += MultiColumnComboBox_RowSelected;
			base.Leave += MultiColumnComboBox_Leave;
			base.GotFocus += MultiColumnComboBox_GotFocus;
			base.AfterCloseUp += MultiColumnComboBox_AfterCloseUp;
			base.Validating += MultiColumnComboBox_Validating;
			base.CharacterCasing = CharacterCasing.Upper;
			base.ItemNotInList += MultiColumnComboBox_ItemNotInList;
			base.UseFlatMode = DefaultableBoolean.True;
			base.AfterDropDown += MultiColumnComboBox_AfterDropDown;
			SelectedIndexChanged += MultiColumnComboBox_SelectedIndexChanged;
			base.MaxDropDownItems = 12;
			base.AlwaysInEditMode = false;
			ignoreSpace = UserPreferences.IgnoreSpaceInComboSearch;
			if (Factory.IsConnected)
			{
				LoadItemFeatures = CompanyPreferences.LoadItemFeatures;
				ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;
				tradingBaseonB2C = CompanyPreferences.TradingBaseonB2C;
			}
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (SelectedID != "")
				{
					return " '" + SelectedID + "'";
				}
				return " ANY (SELECT " + ID_FIELD + " FROM " + TABLENAME_FIELD + ")";
			}
			if (crFieldName == "" || SelectedID == "")
			{
				return "''=''";
			}
			return crFieldName + " = '" + SelectedID + "'";
		}

		public void FireBeforeDropDownEvent(object sender)
		{
			MultiColumnComboBox_BeforeDropDown(sender, null);
		}

		public void FireOnKeyUpEvent(KeyEventArgs e, string comboText)
		{
			comboKeyUpText = comboText;
			OnKeyUp(e);
		}

		public void FireOnKeyDownEvent(object sender, KeyEventArgs e)
		{
			MultiColumnComboBox_KeyDown(sender, e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			SuspendLayout();
			if (base.DropDownStyle == UltraComboStyle.DropDown && Editable && base.Rows.Count < 200000 && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
			{
				string text = "";
				if (comboKeyUpText == "")
				{
					try
					{
						text = ((base.SelectionStart <= 0) ? Text : Text.Substring(0, base.SelectionStart));
					}
					catch
					{
						text = Text;
					}
				}
				else
				{
					text = comboKeyUpText;
				}
				string text2 = "Code LIKE '%" + text + "%' OR Name LIKE '%" + text + "%' ";
				if (ignoreSpace)
				{
					text2 = text2 + " OR SearchColumn LIKE '%" + text.Replace(" ", "") + "%'";
				}
				if (CompanyPreferences.ShowItemUPCInCombo && GetType() == typeof(ProductComboBox))
				{
					text2 = text2 + " OR UPC LIKE '%" + text + "%'";
					if (ignoreSpace)
					{
						text2 = text2 + " OR SearchUPC LIKE '%" + text + "%'";
					}
				}
				if (tradingBaseonB2C && (GetType() == typeof(customersFlatComboBox) || GetType() == typeof(CustomerLeadsComboBox)))
				{
					text2 = text2 + " OR Mobile LIKE '%" + text + "%'";
				}
				dataView.RowFilter = text2;
				if (baseFilter != "")
				{
					DataView obj2 = dataView;
					obj2.RowFilter = obj2.RowFilter + " AND (" + baseFilter + ")";
				}
			}
			ResumeLayout();
			base.OnKeyUp(e);
		}

		protected override void OnAfterCloseUp(EventArgs e)
		{
			if (base.DropDownStyle == UltraComboStyle.DropDown && Editable)
			{
				SuspendLayout();
				foreach (UltraGridRow row in base.Rows)
				{
					row.Hidden = false;
				}
				ResumeLayout();
			}
			base.OnAfterCloseUp(e);
		}

		private void MultiColumnComboBox_GotFocus(object sender, EventArgs e)
		{
		}

		private void MultiColumnComboBox_AfterDropDown(object sender, EventArgs e)
		{
		}

		private void MultiColumnComboBox_ItemNotInList(object sender, ValidationErrorEventArgs e)
		{
			if (!base.ReadOnly && base.Enabled && allowQuickAdd)
			{
				QuickAddItem(e);
			}
		}

		private void MultiColumnComboBox_Validating(object sender, CancelEventArgs e)
		{
		}

		public void RaiseQuickAddEvent()
		{
			ComboEvents.OnQuickAddRequested(this, Text, comboType);
		}

		public virtual void QuickAddItem()
		{
			if (!CompanyPreferences.ShowItemUPCInCombo || !(GetType() == typeof(ProductComboBox)))
			{
				QuickAddItem(null);
			}
		}

		public virtual void QuickAddItem(ValidationErrorEventArgs e)
		{
			if (Text != null && !(Text.Trim() == ""))
			{
				if (ShowQuickAdd())
				{
					RaiseQuickAddEvent();
				}
				if (e != null)
				{
					e.RetainFocus = true;
					Text = "";
				}
			}
		}

		private void MultiColumnComboBox_AfterCloseUp(object sender, EventArgs e)
		{
			RaiseSelectedIndexChange();
		}

		private void MultiColumnComboBox_Leave(object sender, EventArgs e)
		{
			RaiseSelectedIndexChange();
		}

		private void MultiColumnComboBox_RowSelected(object sender, RowSelectedEventArgs e)
		{
			if (descriptionTextBox != null)
			{
				if (e.Row == null || e.Row.Cells.Count < 2 || e.Row.Cells[1].Value == null)
				{
					DescriptionTextBox.Clear();
				}
				else
				{
					descriptionTextBox.Text = e.Row.Cells[1].Value.ToString();
				}
			}
		}

		private void MultiColumnComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape && base.IsDroppedDown && e.KeyCode == Keys.Shift)
			{
				BeginUpdate();
				SelectedID = lastSelectedID;
				EndUpdate();
			}
			if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && !e.Alt && !e.Shift && !e.Control && !base.IsDroppedDown)
			{
				try
				{
					PerformAction(UltraComboAction.Dropdown);
				}
				catch
				{
				}
			}
			if (e.KeyCode == Keys.F2)
			{
				if (DataSource == null || IsReadOnly)
				{
					return;
				}
				ComboSearchDialog comboSearchDialog = new ComboSearchDialog();
				comboSearchDialog.IsMultiSelect = false;
				comboSearchDialog.SelectedItem = SelectedID;
				DataSet dataSet = new DataSet();
				DataView dataView = (DataView)DataSource;
				dataView.RowFilter = baseFilter;
				dataSet.Merge(dataView.ToTable());
				if (dataSet.Tables.Contains("Product"))
				{
					dataSet = Factory.ProductSystem.GetProductsForCombo();
				}
				comboSearchDialog.DataSource = dataSet;
				if (comboSearchDialog.ShowDialog() == DialogResult.OK)
				{
					string text = SelectedID = comboSearchDialog.GetSelectedCode("Code");
				}
			}
			if (LoadItemFeatures && e.KeyCode == Keys.F3 && ActivatePartsDetails)
			{
				ProductsPartsDialog obj2 = new ProductsPartsDialog
				{
					IsMultiSelect = false,
					SelectedItem = SelectedID
				};
				DataSet dataSet2 = new DataSet();
				DataView dataView2 = (DataView)DataSource;
				dataView2.RowFilter = baseFilter;
				dataSet2.Merge(dataView2.ToTable());
				if (dataSet2.Tables.Contains("Product"))
				{
					dataSet2 = Factory.ProductSystem.GetProductsForCombo();
				}
				obj2.DataSource = dataSet2;
				obj2.BringToFront();
				obj2.Activate();
				obj2.Focus();
				obj2.Visible = true;
				obj2.TopMost = true;
			}
		}

		private void MultiColumnComboBox_BeforeDropDown(object sender, CancelEventArgs e)
		{
			try
			{
				base.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				base.AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				if (!isDataLoaded || ComboDataHelper.NeedRefresh(ComboType))
				{
					if (base.DisplayMember == "Code")
					{
						lastSelectedID = Text;
					}
					else if (base.Value != null)
					{
						lastSelectedID = base.Value.ToString();
					}
					isDataLoaded = true;
					LoadData();
					SelectedID = lastSelectedID;
				}
				dataView.RowFilter = baseFilter;
				if (Text == "" && SelectedID != "")
				{
					SelectedID = "";
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		internal void ForceLoadData()
		{
			try
			{
				LoadData();
				isDataLoaded = true;
			}
			catch
			{
				throw;
			}
		}

		private void MultiColumnComboBox_ValueChanged(object sender, EventArgs e)
		{
			if (IsLoadingData)
			{
				return;
			}
			base.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			if (this.SelectedIndexChanged != null)
			{
				if (keyDownUpPressed)
				{
					keyDownUpPressed = false;
				}
				if (!base.IsDroppedDown)
				{
					RaiseSelectedIndexChange();
				}
			}
		}

		private void MultiColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (GetType() == typeof(ProductComboBox))
			{
				DataSet dataSet = new DataSet();
				if (Text != "" && Text != setval)
				{
					dataSet = Factory.ProductSystem.GetProductComboRowByID(Text);
				}
				if (dataSet.Tables.Contains("productCombo") && Text != setval && dataSet.Tables[0].Rows.Count > 0)
				{
					setval = dataSet.Tables[0].Rows[0]["ProductID"].ToString();
					ValAssigned = false;
				}
				if (this.SelectedIndexChanged != null && !ValAssigned && CompanyPreferences.ShowItemUPCInCombo && GetType() == typeof(ProductComboBox) && Text != setval && setval != "")
				{
					ValAssigned = true;
					Text = setval;
				}
			}
		}

		internal void RaiseSelectedIndexChange()
		{
			try
			{
				ultraToolTipManager1.Enabled = true;
				UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
				ultraToolTipInfo.ToolTipText = "";
				string text = Text;
				text = ((base.DisplayMember == "Code") ? Text : ((base.Value == null) ? "" : base.Value.ToString()));
				if (base.SelectedRow == null && base.Textbox != null && text != lastSelectedID)
				{
					ultraToolTipManager1.SetUltraToolTip(this, ultraToolTipInfo);
					lastSelectedID = "";
					if (this.SelectedIndexChanged != null)
					{
						this.SelectedIndexChanged(this, null);
					}
				}
				else if (base.SelectedRow == null || base.SelectedRow.Cells[0].Text == lastSelectedID)
				{
					ultraToolTipManager1.SetUltraToolTip(this, ultraToolTipInfo);
				}
				else
				{
					OldValue = lastSelectedID;
					if (base.SelectedRow != null)
					{
						lastSelectedID = base.SelectedRow.Cells[0].Value.ToString();
					}
					else
					{
						lastSelectedID = "";
					}
					if (this.SelectedIndexChanged != null)
					{
						this.SelectedIndexChanged(this, null);
					}
					if (base.SelectedRow != null)
					{
						string text3 = ultraToolTipInfo.ToolTipText = base.SelectedRow.Cells[1].Value.ToString();
					}
					ultraToolTipManager1.SetUltraToolTip(this, ultraToolTipInfo);
					if (Text == "" && DescriptionTextBox != null)
					{
						DescriptionTextBox.Clear();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void ApplyBandSettings()
		{
			try
			{
				if (base.DisplayLayout != null && base.DisplayLayout.Bands.Count != 0 && base.DisplayLayout.Bands[0].Columns.Count != 0)
				{
					if (allowSort)
					{
						base.DisplayLayout.Bands[0].SortedColumns.Add(base.DisplayLayout.Bands[0].Columns[0], descending: false);
					}
					base.ValueMember = base.DisplayLayout.Bands[0].Columns[0].Key;
					base.DisplayLayout.Bands[0].ColHeadersVisible = false;
					base.DisplayLayout.Bands[0].Override.BorderStyleCell = UIElementBorderStyle.None;
					base.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
					base.DisplayLayout.Bands[0].Override.DefaultColWidth = 200;
					base.DisplayLayout.Bands[0].Override.DefaultRowHeight = 16;
					base.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
					base.DisplayLayout.Appearance.BorderColor = SystemColors.InactiveCaption;
					base.DisplayLayout.Bands[0].Override.HotTrackRowAppearance.BackColor = Color.LightSteelBlue;
				}
			}
			catch
			{
				throw;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && e.KeyCode != Keys.Shift)
				{
					if (base.DisplayLayout.Bands.Count == 0)
					{
						return;
					}
					if (!base.IsDroppedDown && Focused)
					{
						PerformAction(UltraComboAction.Dropdown);
						if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
						{
							e.SuppressKeyPress = true;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			base.OnKeyDown(e);
		}

		public virtual void GetComboRowByID(string id)
		{
			try
			{
				DataSet dataSet = (!(ID2_FIELD != "")) ? Factory.DatabaseSystem.GetComboRowByID(TABLENAME_FIELD, ID_FIELD, id, NAME_FIELD) : Factory.DatabaseSystem.GetComboRowByID(TABLENAME_FIELD, ID_FIELD, id, ID2_FIELD, ID2VALUE_FIELD, NAME_FIELD);
				dataSet.Tables[0].Columns.Add("SearchColumn");
				dataSet.Tables[0].Columns.Add("UPC");
				dataSet.Tables[0].Columns.Add("SearchUPC");
				DataSource = dataSet;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public object GetSelectedCellValue(string key)
		{
			try
			{
				if (base.DesignMode)
				{
					return "";
				}
				if (SelectedID == "")
				{
					return "";
				}
				if (SelectedID == "" && !isDataLoaded)
				{
					LoadData();
					SelectedID = SelectedID;
				}
				if (base.SelectedRow == null || !base.SelectedRow.Cells.Exists(key))
				{
					return "";
				}
				return base.SelectedRow.Cells[key].Value;
			}
			catch
			{
				throw;
			}
		}

		public void DropDown()
		{
			PerformAction(UltraComboAction.Dropdown);
		}

		public bool SelectItemByText(string text)
		{
			return false;
		}

		public void Clear()
		{
			lastSelectedID = "";
			Text = "";
			SelectedID = "";
			base.SelectedRow = null;
		}

		public virtual void LoadData()
		{
			MessageBox.Show("LoadData called from MultiColumnCombo Virtual method.");
		}

		public virtual string GetSelectedItemName()
		{
			if (base.SelectedRow == null || base.SelectedRow.Cells.Count < 2)
			{
				return "";
			}
			return base.SelectedRow.Cells[1].Value.ToString();
		}

		public bool ContainsID(string id)
		{
			DataTable dataTable = DataSource as DataTable;
			if (dataTable == null)
			{
				return false;
			}
			if (dataTable.Select("Code='" + id + "'").Length == 0)
			{
				return false;
			}
			return true;
		}

		public bool ShowQuickAdd()
		{
			if (ErrorHelper.QuestionMessageYesNo("'" + Text + "' is not available in the list.", "Do you want to add?") == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}

		public virtual void FillData(DataView data)
		{
			FillData(data.ToTable());
		}

		public virtual void FillData(DataSet data)
		{
			if (data.Tables.Count > 0)
			{
				FillData(data.Tables[0]);
			}
			if (base.DisplayLayout.Bands[0].Columns.Exists("ParentType"))
			{
				base.DisplayLayout.Bands[0].Columns["ParentType"].Hidden = true;
			}
		}

		public virtual void FillData(DataTable table)
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					base.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
					BeginUpdate();
					if (table == null)
					{
						DataSource = new DataTable();
					}
					else
					{
						dataView = new DataView(table);
						DataSource = dataView;
					}
					if (GetType() != typeof(SysDocTypeComboBox) && GetType() != typeof(PivotGroupComboBox))
					{
						base.DisplayMember = "Code";
						base.ValueMember = "Code";
					}
					else
					{
						base.DisplayMember = "Name";
						base.ValueMember = "Code";
					}
					foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
					{
						if (GetType() == typeof(customersFlatComboBox) || (GetType() == typeof(CustomerLeadsComboBox) && tradingBaseonB2C))
						{
							if (column.Key != "Code" && column.Key != "Name" && column.Key != "Mobile")
							{
								column.Hidden = true;
							}
						}
						else if (column.Key != "Code" && column.Key != "Name")
						{
							column.Hidden = true;
						}
					}
					EndUpdate();
					if (ignoreSpace)
					{
						if (!table.Columns.Contains("SearchColumn"))
						{
							table.Columns.Add("SearchColumn");
						}
						base.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
					}
					if (CompanyPreferences.ShowItemUPCInCombo)
					{
						if (!table.Columns.Contains("UPC"))
						{
							table.Columns.Add("UPC");
						}
						base.DisplayLayout.Bands[0].Columns["UPC"].Hidden = true;
						if (!table.Columns.Contains("SearchUPC"))
						{
							table.Columns.Add("SearchUPC");
						}
						base.DisplayLayout.Bands[0].Columns["SearchUPC"].Hidden = true;
					}
					base.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
					if (table != null)
					{
						base.DisplayLayout.Bands[0].Columns["Code"].Width = 200;
						base.DisplayLayout.Bands[0].Columns["Name"].Width = 350;
					}
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
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
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			ultraToolTipManager1.ContainingControl = this;
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
