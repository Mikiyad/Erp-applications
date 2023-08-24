using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataCaches;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SysDocComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private bool excludeFromSecurity;

		private bool ispriceIncludeTax;

		private string divisionID = string.Empty;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private bool showAll;

		private SysDocTypes filteredTypeID = SysDocTypes.None;

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

		public bool ShowAll
		{
			get
			{
				return showAll;
			}
			set
			{
				showAll = value;
			}
		}

		public bool ExcludeFromSecurity
		{
			get
			{
				return excludeFromSecurity;
			}
			set
			{
				excludeFromSecurity = value;
			}
		}

		public string LocationID => GetSelectedCellValue("LocationID").ToString();

		public bool IsPriceIncludeTax
		{
			get
			{
				bool.TryParse(GetSelectedCellValue("PriceIncludeTax").ToString(), out ispriceIncludeTax);
				return ispriceIncludeTax;
			}
		}

		public string DivisionID
		{
			get
			{
				return GetSelectedCellValue("DivisionID").ToString();
			}
			set
			{
				divisionID = value;
			}
		}

		public SysDocComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.SysDoc;
			TABLENAME_FIELD = "System_Document";
			ID_FIELD = "SysDocID";
			NAME_FIELD = "DocName";
			base.DropDownStyle = UltraComboStyle.DropDownList;
			if (Global.ConStatus == ConnectionStatus.Connected)
			{
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeSysDoc) && !ExcludeFromSecurity)
				{
					base.ReadOnly = true;
				}
				else
				{
					base.ReadOnly = false;
				}
			}
			else
			{
				base.ReadOnly = false;
			}
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

		private DataView GetData(bool isReferesh)
		{
			DataView dataView = new DataView(CombosData.GetSysDocList(isReferesh).Tables[0]);
			if (!showAll)
			{
				int num = (int)filteredTypeID;
				dataView.RowFilter = "SysDocType = " + num.ToString();
			}
			return dataView;
		}

		public void SetDefaultID(string locationID)
		{
			if (!(locationID == ""))
			{
				if (!isDataLoaded)
				{
					LoadData();
				}
				if (base.Items != null)
				{
					foreach (UltraGridRow item in base.Items)
					{
						if (item.Cells["LocationID"].Value != null && item.Cells["LocationID"].Value.ToString() == locationID && !item.Hidden)
						{
							item.Selected = true;
							break;
						}
					}
				}
			}
		}

		public void SetDefaultLocation()
		{
			if (base.Items != null)
			{
				foreach (UltraGridRow item in base.Items)
				{
					if (!item.Cells.Exists("LocationID"))
					{
						break;
					}
					if (item.Cells["LocationID"].Value != null && item.Cells["LocationID"].Value.ToString() == Global.DefaultLocationID)
					{
						item.Selected = true;
						break;
					}
				}
			}
		}

		public void FilterByType(SysDocTypes typeID)
		{
			try
			{
				if (base.DisplayLayout.Bands.Count == 0 && base.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					filteredTypeID = typeID;
				}
				else if (filteredTypeID != typeID)
				{
					filteredTypeID = typeID;
					LoadData(isReferesh: false);
				}
			}
			catch
			{
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
				SetDefaultLocation();
				FilterByType(filteredTypeID);
				if ((filteredTypeID == SysDocTypes.GJournal || filteredTypeID == SysDocTypes.CRMActivity || IsFilter(filteredTypeID)) && !Global.IsUserAdmin)
				{
					foreach (UltraGridRow row in base.Rows)
					{
						bool flag = false;
						string sysDocID = row.Cells["Code"].Value.ToString();
						DataSet entityLinks = Factory.SystemDocumentSystem.GetEntityLinks(sysDocID, SysDocEntityTypes.User);
						if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
						{
							foreach (DataRow row2 in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
							{
								string a = row2["EntityID"].ToString();
								string a2 = row2["EntityID"].ToString().ToUpper();
								if (a == Global.CurrentUser || a == Global.CurrentUser.ToUpper() || a2 == Global.CurrentUser.ToUpper())
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								row.Hidden = true;
							}
						}
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

		private bool IsFilter(SysDocTypes Doc)
		{
			if (new ArrayList
			{
				SysDocTypes.GJournal,
				SysDocTypes.CRMActivity,
				SysDocTypes.PurchaseClaim,
				SysDocTypes.PurchaseQuote,
				SysDocTypes.PurchaseOrder,
				SysDocTypes.GoodsReceivedNote,
				SysDocTypes.PurchaseInvoice,
				SysDocTypes.CashPurchase,
				SysDocTypes.ImportGoodsReceivedNote,
				SysDocTypes.ImportPurchaseInvoice,
				SysDocTypes.ImportPurchaseOrder,
				SysDocTypes.ProformaInvoice,
				SysDocTypes.PackingList,
				SysDocTypes.BOLList,
				SysDocTypes.GRNReturn,
				SysDocTypes.PurchaseOrderNI,
				SysDocTypes.PurchaseInvoiceNI,
				SysDocTypes.CashPurchaseReturn,
				SysDocTypes.CreditPurchaseReturn,
				SysDocTypes.PurchaseCostEntry,
				SysDocTypes.SalesEnquiry,
				SysDocTypes.SalesOrder,
				SysDocTypes.SalesQuote,
				SysDocTypes.SalesProforma,
				SysDocTypes.DeliveryNote,
				SysDocTypes.DeliveryReturn,
				SysDocTypes.SalesReceipt,
				SysDocTypes.SalesInvoice,
				SysDocTypes.CashSalesReturn,
				SysDocTypes.CreditSalesReturn,
				SysDocTypes.ExportSalesOrder,
				SysDocTypes.ExportPickList,
				SysDocTypes.ExportPackingList,
				SysDocTypes.ExportDeliveryNote,
				SysDocTypes.ExportSalesInvoice,
				SysDocTypes.CreditLimitReview,
				SysDocTypes.InventoryAdjustment,
				SysDocTypes.InventoryNoneSale,
				SysDocTypes.DirectInventoryTransfer,
				SysDocTypes.InventoryRepacking,
				SysDocTypes.TransitTransferIn,
				SysDocTypes.TransitTransferOut,
				SysDocTypes.ReturnTransitTransfer
			}.Contains(Doc))
			{
				return true;
			}
			return false;
		}

		public string GetPrintTemplateName()
		{
			string empty = string.Empty;
			foreach (UltraGridRow item in base.Items)
			{
				if (item.Cells["Code"].Value.ToString() == SelectedID)
				{
					return item.Cells["PrintTemplateName"].Value.ToString();
				}
			}
			return empty;
		}
	}
}
