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
	public class ClientAssetComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string filteredJobID = "asdr323f@$@";

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

		public string SerialNo => GetSelectedCellValue("SerialNo").ToString();

		public ClientAssetComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.ClientAsset;
			TABLENAME_FIELD = "ClientAsset";
			ID_FIELD = "ClientAssetID";
			NAME_FIELD = "ClientAssetName";
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
			return CombosData.GetCientAssetList(isReferesh);
		}

		public void FilterByProject(string jobID)
		{
			filteredJobID = jobID;
			LoadData(isReferesh: true);
		}

		public override void FillData(DataSet data)
		{
			if (!Factory.IsDBConnected)
			{
				return;
			}
			_ = data.Tables[0];
			if (filteredJobID != "")
			{
				DataRow[] array = data.Tables[0].Select("JobID = '" + filteredJobID + "'");
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
				data = data.Clone();
			}
			base.FillData(data);
			base.DisplayLayout.Bands[0].Columns["SerialNo"].Hidden = false;
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
	}
}
