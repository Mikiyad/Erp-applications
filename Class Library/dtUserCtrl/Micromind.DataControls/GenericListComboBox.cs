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
	public class GenericListComboBox : MultiColumnComboBox
	{
		private bool isSingleColumn;

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private GenericListTypes genericListType;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		public bool IsSingleColumn
		{
			get
			{
				return isSingleColumn;
			}
			set
			{
				isSingleColumn = value;
				if (value)
				{
					base.DisplayMember = "Name";
				}
			}
		}

		public GenericListTypes GenericListType
		{
			get
			{
				return genericListType;
			}
			set
			{
				genericListType = value;
				ID2VALUE_FIELD = ((byte)value).ToString();
			}
		}

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

		public GenericListComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.GenericList;
			TABLENAME_FIELD = "Generic_List";
			ID_FIELD = "GenericListID";
			NAME_FIELD = "GenericListName";
			ID2_FIELD = "GenericListType";
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
			return CombosData.GetGenericListList(isReferesh, GenericListType);
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			try
			{
				DataView data = new DataView(GetData(isReferesh).Tables[0], "GenericListType = " + (byte)GenericListType, "Code", DataViewRowState.CurrentRows);
				FillData(data);
				if (isSingleColumn)
				{
					base.DisplayMember = "Name";
					base.DisplayLayout.Bands[0].Columns["Code"].Hidden = true;
				}
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
