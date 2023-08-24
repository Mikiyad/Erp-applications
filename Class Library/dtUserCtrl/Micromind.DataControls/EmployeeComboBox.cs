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
	public class EmployeeComboBox : MultiColumnComboBox
	{
		private bool showQuickAdd = true;

		private bool showTerminated = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private string filterID = "";

		private string sponsorID = "";

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

		public bool ShowTerminatedEmployees
		{
			get
			{
				return showTerminated;
			}
			set
			{
				showTerminated = value;
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

		public EmployeeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Employee;
			TABLENAME_FIELD = "Employee";
			ID_FIELD = "EmployeeID";
			NAME_FIELD = "FirstName";
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
			return CombosData.GetEmployeeList(isReferesh);
		}

		public void Filter(string positionID)
		{
			try
			{
				if (positionID != "")
				{
					filterID = positionID;
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

		public void FilterBySponsorId(string positionID)
		{
			try
			{
				if (positionID != "")
				{
					sponsorID = positionID;
					LoadData(isReferesh: false);
				}
				else
				{
					sponsorID = "";
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
			DataSet dataSet = null;
			try
			{
				dataSet = GetData(isReferesh);
				if (!showTerminated)
				{
					DataRow[] rows = dataSet.Tables["Employee"].Select("IsTerminated='False'");
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(rows);
					dataSet = dataSet2;
				}
				if (filterID != "")
				{
					DataRow[] rows2 = dataSet.Tables[0].Select("PositionID='" + filterID + "'");
					DataSet dataSet3 = dataSet.Clone();
					dataSet3.Tables[0].Rows.Clear();
					dataSet3.Merge(rows2);
					FillData(dataSet3);
				}
				else if (sponsorID != "")
				{
					DataRow[] rows3 = dataSet.Tables[0].Select("SponsorId='" + sponsorID + "'");
					DataSet dataSet4 = dataSet.Clone();
					dataSet4.Tables[0].Rows.Clear();
					dataSet4.Merge(rows3);
					FillData(dataSet4);
				}
				else
				{
					FillData(dataSet);
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
