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
	public class POSExpenseComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

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
		public virtual string SelectedAccountID
		{
			get
			{
				if (base.SelectedRow != null && SelectedID != "")
				{
					if (base.SelectedRow.Cells.Exists("AccountID"))
					{
						return base.SelectedRow.Cells["AccountID"].Text.ToString();
					}
					return "";
				}
				return "";
			}
		}

		public string CashRegisterID
		{
			get;
			set;
		}

		public POSExpenseComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.AccountGroup;
			TABLENAME_FIELD = "Account_Group";
			ID_FIELD = "GroupID";
			NAME_FIELD = "GroupName";
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
			return CombosData.GetPOSExpenseAccountList(isReferesh, CashRegisterID);
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
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Contains("DisplayName"))
				{
					dataSet.Tables[0].Columns["DisplayName"].ColumnName = "Name";
				}
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Contains("AccountID"))
				{
					dataSet.Tables[0].Columns["AccountID"].ColumnName = "Code";
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

		public override void FillData(DataSet data)
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					base.FillData(data);
					base.DisplayMember = "Name";
					base.ValueMember = "Code";
					foreach (UltraGridColumn column in base.DisplayLayout.Bands[0].Columns)
					{
						if (column.Key != "Name")
						{
							column.Hidden = true;
						}
					}
					base.DisplayLayout.Bands[0].Columns["Code"].Width = 100;
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
