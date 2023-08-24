using Infragistics.Win.UltraWinEditors;
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
	public class ReceivedChequeComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		public byte[] filteredStatus;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string filteredBankID = "";

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

		public ReceivedChequeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.ReceivedCheque;
			TABLENAME_FIELD = "Cheque_Received";
			ID_FIELD = "ChequeNumber";
			NAME_FIELD = "PayeeName";
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
			return CombosData.GetReceivedChequeList(isReferesh);
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
				if (filteredStatus != null)
				{
					string text = "";
					for (int i = 0; i < filteredStatus.Length; i++)
					{
						text += filteredStatus[i].ToString();
						if (i < filteredStatus.Length)
						{
							text += ",";
						}
					}
					DataRow[] rows = dataSet.Tables[0].Select("Status IN (" + text.ToString() + ")");
					dataSet = new DataSet();
					dataSet.Merge(rows);
				}
				FillData(dataSet);
				if (filteredBankID != "")
				{
					if (base.DisplayLayout.Bands[0].ColumnFilters.Exists("BankID"))
					{
						base.DisplayLayout.Bands[0].ColumnFilters["BankID"].FilterConditions.Clear();
					}
					if (dataSet != null && dataSet.Tables.Count > 0)
					{
						base.DisplayLayout.Bands[0].ColumnFilters["BankID"].FilterConditions.Add(FilterComparisionOperator.Equals, filteredBankID);
					}
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

		public void FilterByBank(string bankID)
		{
			try
			{
				if (base.DisplayLayout.Bands.Count == 0 || base.DisplayLayout.Bands[0].Columns.Count == 0)
				{
					filteredBankID = bankID;
				}
				else if (!(filteredBankID == bankID) || base.DisplayLayout.Bands[0].ColumnFilters["BankID"].FilterConditions.Count == 0)
				{
					filteredBankID = bankID;
					base.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
					base.DisplayLayout.Bands[0].ColumnFilters["BankID"].FilterConditions.Add(FilterComparisionOperator.Equals, bankID);
				}
			}
			catch
			{
			}
		}

		public override void QuickAddItem(ValidationErrorEventArgs e)
		{
			if (Text.Trim() != "")
			{
				ErrorHelper.InformationMessage("This cheque number is not in the list");
				e.RetainFocus = true;
			}
		}
	}
}
