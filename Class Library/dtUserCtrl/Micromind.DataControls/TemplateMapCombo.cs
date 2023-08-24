using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class TemplateMapCombo : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private DataTable templateData = new DataTable();

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private string filterScreenID = "";

		private string filterFormID = "";

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

		public string FilterScreenID
		{
			get
			{
				return filterScreenID;
			}
			set
			{
				filterScreenID = value;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
		}

		public string FilterFormID
		{
			get
			{
				return filterFormID;
			}
			set
			{
				filterFormID = value;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
		}

		public string SelectedFileName
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells["Title"].Value.ToString();
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

		public TemplateMapCombo()
		{
			InitializeComponent();
			base.ComboType = DataComboType.PrintTemplateMap;
			TABLENAME_FIELD = "Print_Template_Map";
			ID_FIELD = "MapID";
			NAME_FIELD = "TemplateName";
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
			return PrintTemplateMap.PrintTemplateMapData;
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
				if (FilterScreenID != "")
				{
					DataRow[] rows = dataSet.Tables[0].Select("ID IN ('" + FilterScreenID + "')");
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(rows);
					dataSet = dataSet2;
				}
				if (FilterFormID != "")
				{
					DataRow[] rows2 = dataSet.Tables[0].Select("FormID IN ('" + FilterFormID + "')");
					DataSet dataSet3 = new DataSet();
					dataSet3.Merge(rows2);
					dataSet = dataSet3;
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
	}
}
