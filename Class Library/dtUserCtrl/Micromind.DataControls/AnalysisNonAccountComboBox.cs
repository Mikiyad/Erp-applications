using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class AnalysisNonAccountComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private bool showAccountReceivables = true;

		private bool showAccountPayables = true;

		private int[] selectedIDs = new int[0];

		private bool isKeyPressed;

		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string filteredAccountID = "";

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

		[Description("Show/Dont Show account receivables.")]
		[Category("Filter")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Browsable(true)]
		public bool ShowAccountReceivables
		{
			get
			{
				return showAccountReceivables;
			}
			set
			{
				showAccountReceivables = value;
			}
		}

		[Description("Show/Dont Show account payables.")]
		[Category("Filter")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Browsable(true)]
		public bool ShowAccountPayables
		{
			get
			{
				return showAccountPayables;
			}
			set
			{
				showAccountPayables = value;
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

		public AnalysisNonAccountComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Analysis;
			TABLENAME_FIELD = "Analysis";
			ID_FIELD = "AnalysisID";
			NAME_FIELD = "AnalysisName";
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

		private CompanyAccountTypes GetAccountType()
		{
			return (base.Parent as IComboBoxAccount)?.GetAccountType() ?? CompanyAccountTypes.Bank;
		}

		private void QuickAdd()
		{
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetAnalysisNonAccountList(isReferesh);
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

		public void FilterByAccount(string accountID)
		{
			filteredAccountID = accountID;
			LoadData(isReferesh: false);
		}

		public bool IsValidAccountAnalysis(string accountID, string analysisID)
		{
			if (analysisID == "")
			{
				return true;
			}
			if (accountID == "" || base.DataSource == null)
			{
				return false;
			}
			DataTable dataTable = base.DataSource as DataTable;
			if (dataTable != null)
			{
				if (dataTable.Select("AccountID='" + accountID + "' AND Code='" + analysisID + "'").Length == 0)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public override void FillData(DataSet data)
		{
			if (!Factory.IsDBConnected)
			{
				return;
			}
			_ = data.Tables[0];
			if (filteredAccountID != "")
			{
				DataRow[] array = data.Tables[0].Select("AccountID = '" + filteredAccountID + "'");
				DataSet dataSet = new DataSet();
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
			else
			{
				data = data;
			}
			base.FillData(data);
		}
	}
}
