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
	public class AccountGroupComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private bool showAccountReceivables = true;

		private bool showAccountPayables = true;

		private int[] selectedIDs = new int[0];

		private bool isKeyPressed;

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

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedTypeName
		{
			get
			{
				if (base.SelectedRow != null && SelectedID != "")
				{
					if (base.SelectedRow.Cells.Exists("Type"))
					{
						return base.SelectedRow.Cells["Type"].Text.ToString();
					}
					return "";
				}
				return "";
			}
		}

		public byte SelectedType
		{
			get
			{
				if (base.SelectedRow == null || !base.SelectedRow.Cells.Exists("TypeID"))
				{
					return 0;
				}
				return byte.Parse(base.SelectedRow.Cells["TypeID"].Value.ToString());
			}
		}

		public AccountGroupComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.AccountGroup;
			TABLENAME_FIELD = "Account_Group";
			ID_FIELD = "GroupID";
			NAME_FIELD = "GroupName";
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
			return CombosData.GetAccountGroupList(isReferesh);
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
			if (Factory.IsDBConnected)
			{
				try
				{
					base.FillData(data);
					base.DisplayMember = "Code";
					base.ValueMember = "Code";
					foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
					{
						if (column.Key != "Code" && column.Key != "Name" && column.Key != "Type")
						{
							column.Hidden = true;
						}
					}
					base.DisplayLayout.Bands[0].Columns["Code"].Width = 100;
					base.DisplayLayout.Bands[0].Columns["Type"].Width = 70;
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
}
