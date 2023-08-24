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
	public class EmployeeFilterComboBox : MultiColumnComboBox
	{
		private bool showQuickAdd = true;

		private bool showTerminated = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string filteredReportToID = "asdr323f@$@";

		private ScreenAccessRight screenRight;

		private bool selfRequest;

		private bool allintermediatesub;

		private bool allsubordiantes;

		private bool accessall;

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

		public EmployeeFilterComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.EmployeeFilter;
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
			return CombosData.GetEmployeeFilterList(isReferesh);
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

		public void FilterByReport(string reportID)
		{
			filteredReportToID = reportID;
			LoadData(isReferesh: false);
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessAllSub))
			{
				allsubordiantes = false;
			}
			else
			{
				allsubordiantes = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessSelfRequest))
			{
				selfRequest = false;
			}
			else
			{
				selfRequest = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessIntermediate))
			{
				allintermediatesub = false;
			}
			else
			{
				allintermediatesub = true;
			}
			if (!allsubordiantes && !selfRequest && !allintermediatesub)
			{
				accessall = true;
			}
			if (Global.IsUserAdmin)
			{
				accessall = true;
			}
		}

		public override void FillData(DataSet data)
		{
			SetSecurity();
			if (!Factory.IsDBConnected)
			{
				return;
			}
			_ = data.Tables[0];
			filteredReportToID = "ABC";
			if (filteredReportToID != "")
			{
				string text = Global.CurrentUserEmployeeID.ToString();
				text = Factory.DatabaseSystem.GetFieldValue("Users", "EmployeeID", "UserID", Global.CurrentUser).ToString();
				DataRow[] array = null;
				if (!accessall)
				{
					if (allintermediatesub)
					{
						array = data.Tables[0].Select("ReportToID = '" + text + "' OR Code = '" + text + "'");
					}
					else if (selfRequest)
					{
						array = data.Tables[0].Select("Code = '" + text + "'");
					}
					else if (allsubordiantes)
					{
						array = data.Tables[0].Select("ReportToID = '" + text + "' OR Code = '" + text + "' OR SubReportToID = '" + text + "'");
					}
				}
				else if (accessall)
				{
					array = data.Tables[0].Select("Code<>''");
				}
				DataSet dataSet = new DataSet();
				if (array != null)
				{
					if (array.Length != 0)
					{
						dataSet.Merge(array);
					}
					else
					{
						dataSet = data.Clone();
					}
					data = dataSet;
				}
			}
			else
			{
				data = data.Clone();
			}
			if (allsubordiantes || allintermediatesub || selfRequest || accessall)
			{
				base.FillData(data);
			}
		}
	}
}
