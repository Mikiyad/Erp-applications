using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class PayeeTypeComboBox : MultiColumnComboBox
	{
		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

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

		public PayeeTypeComboBox()
		{
			InitializeComponent();
			base.BeforeDropDown += PayeeTypeComboBox_BeforeDropDown;
			allowSort = false;
		}

		private void PayeeTypeComboBox_BeforeDropDown(object sender, CancelEventArgs e)
		{
			base.Width = 100;
			base.DropDownWidth = 100;
			base.DisplayLayout.Bands[0].Columns["Code"].Width = 20;
			base.DisplayLayout.Bands[0].Columns["Name"].Width = 75;
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

		private DataSet GetData(bool isReferesh)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			dataSet.Tables[0].Columns.Add("Code");
			dataSet.Tables[0].Columns.Add("Name");
			dataSet.Tables[0].Rows.Add("A", "Account");
			dataSet.Tables[0].Rows.Add("C", "Customer");
			dataSet.Tables[0].Rows.Add("V", "Vendor");
			dataSet.Tables[0].Rows.Add("E", "Employee");
			return dataSet;
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			DataSet dataSet = null;
			try
			{
				dataSet = GetData(isReferesh);
				FillData(dataSet);
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
