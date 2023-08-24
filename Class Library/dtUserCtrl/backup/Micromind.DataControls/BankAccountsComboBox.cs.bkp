using Infragistics.Win.UltraWinGrid;
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
	public class BankAccountsComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private bool showAccountReceivables = true;

		private bool showAccountPayables = true;

		private int[] selectedIDs = new int[0];

		private bool isKeyPressed;

		private int filteredTypeID = -1;

		private new bool isDataLoaded;

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

		public BankAccountsComboBox()
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

		private DataView GetData(bool isReferesh)
		{
			return new DataView(CombosData.GetAllAccountsList(refresh: false).Tables[0])
			{
				RowFilter = "SubType IN (3)"
			};
		}

		public override void FillData(DataView data)
		{
			if (Factory.IsDBConnected)
			{
				DataTable dataTable = data.ToTable();
				base.FillData(dataTable);
				try
				{
					BeginUpdate();
					if (dataTable == null)
					{
						base.DataSource = new DataTable();
					}
					else
					{
						base.DataSource = dataTable;
					}
					foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
					{
						if (column.Key == "SubTypeName")
						{
							column.Hidden = false;
						}
					}
					if (dataTable != null)
					{
						base.DisplayLayout.Bands[0].Columns["SubTypeName"].Width = 150;
					}
					EndUpdate();
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

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			DataView dataView = null;
			try
			{
				dataView = GetData(isReferesh);
				FillData(dataView);
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
