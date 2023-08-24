using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class OpenPOComboBox : MultiColumnComboBox
	{
		private string filterID = "";

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string selectedSysDocID = "";

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

		public string FilterID
		{
			get
			{
				return filterID;
			}
			set
			{
				filterID = value;
			}
		}

		public string SelectedSysDocID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				if (!base.SelectedRow.Cells.Exists("SysDocID"))
				{
					return selectedSysDocID;
				}
				return base.SelectedRow.Cells["SysDocID"].Value.ToString();
			}
			set
			{
				selectedSysDocID = value;
			}
		}

		public OpenPOComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.EmployeeLoan;
			TABLENAME_FIELD = "Purchase_Order";
			ID_FIELD = "VoucherID";
			NAME_FIELD = "Note";
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

		public void ApplyFilter()
		{
			LoadData();
		}

		private void QuickAdd()
		{
		}

		private DataSet GetData(string vendorID)
		{
			return Factory.PurchaseOrderSystem.GetOpenPOComboData(vendorID);
		}

		public override void LoadData()
		{
		}

		public void LoadData(string vendorID)
		{
			try
			{
				DataView data = new DataView(GetData(vendorID).Tables[0]);
				FillData(data);
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
