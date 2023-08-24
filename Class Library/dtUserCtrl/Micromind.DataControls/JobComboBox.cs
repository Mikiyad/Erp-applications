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
	public class JobComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private string filterID = "";

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

		public JobComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Job;
			TABLENAME_FIELD = "Job";
			ID_FIELD = "JobID";
			NAME_FIELD = "JobName";
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

		public void Filter(string customerID)
		{
			try
			{
				if (customerID != "")
				{
					filterID = customerID;
					LoadData(isReferesh: false);
				}
				else
				{
					filterID = "";
					LoadData(isReferesh: false);
				}
			}
			catch
			{
			}
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
			return CombosData.GetJobList(isReferesh);
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
				if (filterID != "")
				{
					DataRow[] rows = dataSet.Tables[0].Select("CustomerID='" + filterID + "'");
					DataSet dataSet2 = dataSet.Clone();
					dataSet2.Tables[0].Rows.Clear();
					dataSet2.Merge(rows);
					FillData(dataSet2);
					base.DisplayLayout.Bands[0].Columns["CustomerName"].Hidden = false;
				}
				else
				{
					FillData(dataSet);
					base.DisplayLayout.Bands[0].Columns["CustomerName"].Hidden = false;
					base.IsDataLoaded = true;
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
}
