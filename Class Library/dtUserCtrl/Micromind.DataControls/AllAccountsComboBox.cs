using Infragistics.Win;
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
	public class AllAccountsComboBox : MultiColumnComboBox
	{
		private string filterSysDocID = "";

		private bool useAccountNumbers;

		private bool showAccountReceivables = true;

		private bool showAccountPayables = true;

		private int[] selectedIDs = new int[0];

		private bool isKeyPressed;

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

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

		public string FilterSysDocID
		{
			get
			{
				return filterSysDocID;
			}
			set
			{
				filterSysDocID = value;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
		}

		public AccountSubTypes FilterSubType
		{
			get;
			set;
		}

		public AccountTypes FilterAccountType
		{
			get;
			set;
		}

		public AllAccountsComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Accounts;
			TABLENAME_FIELD = "Account";
			ID_FIELD = "AccountID";
			NAME_FIELD = "AccountName";
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

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData != Keys.F3 && e.KeyData == Keys.F5)
			{
				LoadData(isReferesh: true);
			}
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		private void flatComboBox_Load(object sender, EventArgs e)
		{
			if (HasAllAccount && SelectedID != "")
			{
				Text = "All Accounts";
			}
		}

		private CompanyAccountTypes GetAccountType()
		{
			return (base.Parent as IComboBoxAccount)?.GetAccountType() ?? CompanyAccountTypes.Bank;
		}

		private void QuickAdd()
		{
			if (!Security.HasComboAccessRight(ScreenAreas.Accounts, suppressMessage: true))
			{
				showQuickAdd = false;
				return;
			}
			string a = Text = Text.Trim();
			_ = (a == "");
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetAllAccountsList(refresh: false);
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

		public override void FillData(DataSet data)
		{
			if (!Factory.IsDBConnected)
			{
				return;
			}
			_ = data.Tables[0];
			if (FilterAccountType != 0)
			{
				DataRow[] rows = data.Tables[0].Select("TypeID = " + ((int)FilterAccountType).ToString());
				DataSet dataSet = new DataSet();
				dataSet.Merge(rows);
				data = dataSet;
			}
			if (FilterSubType != 0)
			{
				DataRow[] rows2 = data.Tables[0].Select("SubType = " + ((int)FilterSubType).ToString());
				DataSet dataSet2 = new DataSet();
				dataSet2.Merge(rows2);
				data = dataSet2;
			}
			if (filterSysDocID != "")
			{
				string text = "";
				DataSet entityLinks = Factory.SystemDocumentSystem.GetEntityLinks(filterSysDocID, SysDocEntityTypes.AccountGroup);
				if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
				{
					foreach (DataRow row in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
					{
						if (text != "")
						{
							text += ",";
						}
						text = text + "'" + row["EntityID"].ToString() + "'";
					}
				}
				if (text != "")
				{
					DataRow[] rows3 = data.Tables[0].Select("GroupID IN (" + text + ")");
					DataSet dataSet3 = new DataSet();
					dataSet3.Merge(rows3);
					data = dataSet3;
				}
			}
			base.FillData(data);
			isDataLoaded = true;
			try
			{
				if (base.DisplayLayout.Bands[0].Columns.Exists("SubTypeName"))
				{
					base.DisplayLayout.Bands[0].Columns["SubTypeName"].Hidden = false;
					base.DisplayLayout.Bands[0].Columns["SubTypeName"].Width = 150;
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

		public ValueList GetAnalysisValueList(string accountID)
		{
			if (accountID == "")
			{
				return new ValueList();
			}
			ValueList valueList = new ValueList();
			DataRow[] array = GetData(isReferesh: false).Tables["Account_Analysis_Detail"].Select("AccountID = '" + accountID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				valueList.ValueListItems.Add(array[i]["Code"].ToString(), array[i]["Name"].ToString());
			}
			return valueList;
		}

		public DataSet GetAnalysisDataList(string accountID)
		{
			if (accountID == "")
			{
				return new DataSet();
			}
			new ValueList();
			DataRow[] rows = GetData(isReferesh: false).Tables["Account_Analysis_Detail"].Select("AccountID = '" + accountID + "'");
			DataSet dataSet = new DataSet();
			dataSet.Merge(rows);
			return dataSet;
		}
	}
}
