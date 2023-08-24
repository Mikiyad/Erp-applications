using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataCaches;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class TransactionTypeComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private SysDocTypes filteredTypeID = SysDocTypes.None;

		public bool HasCustom
		{
			get
			{
				return hasCustom;
			}
			set
			{
				hasCustom = value;
			}
		}

		public bool HasAllAccount
		{
			get
			{
				return hasAll;
			}
			set
			{
				hasAll = value;
			}
		}

		public new bool ShowQuickAdd
		{
			get
			{
				return showQuickAdd;
			}
			set
			{
				showQuickAdd = value;
			}
		}

		public bool ShowInactiveItems
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

		public SysDocTypes SelectedType
		{
			get
			{
				if (SelectedID == "")
				{
					return SysDocTypes.None;
				}
				return (SysDocTypes)int.Parse(GetSelectedCellValue("Code").ToString());
			}
		}

		public override string SelectedID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells["Code"].Value.ToString();
			}
			set
			{
				Enum.TryParse(value, out SysDocTypes result);
				int num = (int)result;
				base.SelectedID = num.ToString();
			}
		}

		public TransactionTypeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.SysDoc;
			allowSort = false;
			base.DropDownStyle = UltraComboStyle.DropDownList;
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
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		private void QuickAdd()
		{
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetTransactionComboList(isReferesh);
		}

		public void SetDefaultID(string locationID)
		{
			if (!(locationID == ""))
			{
				if (!isDataLoaded)
				{
					LoadData();
				}
				foreach (UltraGridRow item in base.Items)
				{
					if (item.Cells["LocationID"].Value.ToString() == locationID)
					{
						item.Selected = true;
						break;
					}
				}
			}
		}

		public void FilterByType(SysDocTypes typeID)
		{
			try
			{
				if (base.DisplayLayout.Bands.Count == 0 && base.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					filteredTypeID = typeID;
				}
				else if (filteredTypeID != typeID)
				{
					filteredTypeID = typeID;
					LoadData(isReferesh: false);
				}
			}
			catch
			{
			}
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add("Types");
				dataSet.Tables[0].Columns.Add("Code");
				dataSet.Tables[0].Columns.Add("Name");
				foreach (object value in Enum.GetValues(typeof(SysDocTypes)))
				{
					int num = (int)(SysDocTypes)Enum.Parse(typeof(SysDocTypes), value.ToString());
					if (!(value.ToString().ToLower() == "none") && (value.ToString().ToLower() == "purchasereceipt" || value.ToString().ToLower() == "purchaseorder" || value.ToString().ToLower() == "jobmaterialrequisition" || value.ToString().ToLower() == "salesorder" || value.ToString().ToLower() == "purchaseinvoice" || value.ToString().ToLower() == "salesinvoice" || value.ToString().ToLower() == "salesreceipt" || value.ToString().ToLower() == "deliverynote" || value.ToString().ToLower() == "goodsreceivednote" || value.ToString().ToLower() == "inventoryadjustment" || value.ToString().ToLower() == "directinventorytransfer" || value.ToString().ToLower() == "inventoryrepacking" || value.ToString().ToLower() == "transittransferout" || value.ToString().ToLower() == "inventorynonesale" || value.ToString().ToLower() == "returntransittransfer" || value.ToString().ToLower() == "consignoutsettlement" || value.ToString().ToLower() == "inventoryrepacking" || value.ToString().ToLower() == "consignout"))
					{
						dataSet.Tables[0].Rows.Add(num, value.ToString());
					}
				}
				dataSet.Tables[0].DefaultView.Sort = "Name ASC";
				DataSet dataSet2 = new DataSet();
				dataSet2.Tables.Add(dataSet.Tables[0].DefaultView.ToTable());
				FillData(dataSet2);
				foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
				{
					if (column.Key != "Name")
					{
						column.Hidden = true;
					}
				}
				base.ValueMember = "Code";
				base.DisplayMember = "Name";
				base.CharacterCasing = CharacterCasing.Normal;
				base.IsDataLoaded = true;
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
}
