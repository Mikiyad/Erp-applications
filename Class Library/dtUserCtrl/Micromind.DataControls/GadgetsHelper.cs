using DevExpress.XtraCharts;
using Micromind.Common;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Micromind.DataControls
{
	public class GadgetsHelper
	{
		public static Dictionary<GadgetTypes, GroupLayout> GadgetsCollection => new Dictionary<GadgetTypes, GroupLayout>
		{
			{
				GadgetTypes.FavoriteBankAccountsList,
				new GroupLayout(GadgetTypes.FavoriteBankAccountsList, "Bank Accounts Summary", GadgetCategories.Accounts, GadgetStyles.List)
			},
			{
				GadgetTypes.MonthlySalesChart,
				new GroupLayout(GadgetTypes.MonthlySalesChart, "Monthly Sales", GadgetCategories.Sales, GadgetStyles.Chart)
			},
			{
				GadgetTypes.PDCIssuedList,
				new GroupLayout(GadgetTypes.PDCIssuedList, "PDCs Issued", GadgetCategories.Accounts, GadgetStyles.List)
			},
			{
				GadgetTypes.PDCOnhandList,
				new GroupLayout(GadgetTypes.PDCOnhandList, "PDCs Onhand", GadgetCategories.Accounts, GadgetStyles.List)
			},
			{
				GadgetTypes.ItemsReorderList,
				new GroupLayout(GadgetTypes.ItemsReorderList, "Items to Reorder", GadgetCategories.Inventory, GadgetStyles.List)
			},
			{
				GadgetTypes.TopCustomersChart,
				new GroupLayout(GadgetTypes.TopCustomersChart, "Top Customers", GadgetCategories.Sales, GadgetStyles.Chart)
			},
			{
				GadgetTypes.TopInvoicesChart,
				new GroupLayout(GadgetTypes.TopInvoicesChart, "Top Invoices", GadgetCategories.Sales, GadgetStyles.Chart)
			},
			{
				GadgetTypes.TopProductsChart,
				new GroupLayout(GadgetTypes.TopProductsChart, "Top Products", GadgetCategories.Sales, GadgetStyles.Chart)
			},
			{
				GadgetTypes.TopSalespersonChart,
				new GroupLayout(GadgetTypes.TopSalespersonChart, "Top Salespersons", GadgetCategories.Sales, GadgetStyles.Chart)
			},
			{
				GadgetTypes.DailySalesChart,
				new GroupLayout(GadgetTypes.DailySalesChart, "Daily Sales", GadgetCategories.Sales, GadgetStyles.Chart)
			}
		};

		public static GroupLayout GetGadgetLayout(GadgetTypes gadgetType)
		{
			GroupLayout value = null;
			GadgetsCollection.TryGetValue(gadgetType, out value);
			return value;
		}

		public static string GetGadgetTitle(GadgetTypes gadgetType)
		{
			GroupLayout value = null;
			GadgetsCollection.TryGetValue(gadgetType, out value);
			if (value != null)
			{
				return value.Title;
			}
			return "";
		}

		public static IGadget CreateGadget(GroupLayout layout)
		{
			GadgetTypes gadgetType = layout.GadgetType;
			IGadget gadget = null;
			switch (gadgetType)
			{
			case GadgetTypes.FavoriteBankAccountsList:
				gadget = new FavoriteBankAccountsGadget();
				break;
			case GadgetTypes.MonthlySalesChart:
				gadget = new GadgetMonthlySalesChart();
				break;
			case GadgetTypes.PDCIssuedList:
				gadget = new GadgetPDCIssuedList();
				break;
			case GadgetTypes.PDCOnhandList:
				gadget = new GadgetPDCOnhandList();
				break;
			case GadgetTypes.ItemsReorderList:
				gadget = new GadgetReorderItems();
				break;
			case GadgetTypes.TopCustomersChart:
				gadget = new GadgetTopCustomersChart();
				break;
			case GadgetTypes.TopInvoicesChart:
				gadget = new GadgetTopInvoicesChart();
				break;
			case GadgetTypes.TopProductsChart:
				gadget = new GadgetTopProductsChart();
				break;
			case GadgetTypes.TopSalespersonChart:
				gadget = new GadgetTopSalespersonChart();
				break;
			case GadgetTypes.TransactionShortcuts:
				gadget = new GadgetTransactionShortcuts();
				break;
			case GadgetTypes.CardShortcuts:
				gadget = new GadgetCardShortcuts();
				break;
			case GadgetTypes.ReportShortcuts:
				gadget = new GadgetReportShortcuts();
				break;
			case GadgetTypes.DailySalesChart:
				gadget = new GadgetDailySalesChart();
				break;
			case GadgetTypes.Custom:
			{
				if (layout.GadgetStyle == GadgetStyles.Chart)
				{
					gadget = new CustomGadgetChart();
					gadget.GadgetID = layout.Code;
					gadget.GadgetTitle = layout.Title;
					gadget.GadgetStyle = layout.GadgetStyle;
				}
				else if (layout.GadgetStyle == GadgetStyles.Gauge)
				{
					gadget = new CustomGadgetGauge();
					gadget.GadgetID = layout.Code;
					gadget.GadgetTitle = layout.Title;
					gadget.GadgetStyle = layout.GadgetStyle;
				}
				else if (layout.GadgetStyle == GadgetStyles.Number)
				{
					gadget = new CustomGadgetNumeric();
					gadget.GadgetID = layout.Code;
					gadget.GadgetTitle = layout.Title;
					gadget.GadgetStyle = layout.GadgetStyle;
				}
				else
				{
					gadget = new CustomGadgetList();
					gadget.GadgetID = layout.Code;
					gadget.GadgetTitle = layout.Title;
					gadget.GadgetStyle = layout.GadgetStyle;
				}
				ICustomGadget obj = gadget as ICustomGadget;
				obj.GroupLayout = layout;
				obj.ChartType = (ViewType)layout.ChartType;
				obj.FilterOption = layout.FilterOption;
				obj.Init();
				ICustomGadgetChart customGadgetChart = gadget as ICustomGadgetChart;
				if (customGadgetChart != null)
				{
					customGadgetChart.ColorEach = layout.ColorEach;
					if (layout.colorPaletteName != null)
					{
						customGadgetChart.ColorPaletteName = layout.colorPaletteName;
					}
				}
				break;
			}
			}
			if (gadget == null)
			{
				throw new CompanyException("GadgetType is not implemented in method CreateGadget()");
			}
			return gadget;
		}

		public static MemoryStream SerializeToStream(object data)
		{
			MemoryStream memoryStream = new MemoryStream();
			((IFormatter)new BinaryFormatter()).Serialize((Stream)memoryStream, data);
			return memoryStream;
		}

		public static object DeserializeFromStream(byte[] streamBytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(streamBytes, 0, streamBytes.Length);
			return DeserializeFromStream(memoryStream);
		}

		public static object DeserializeFromStream(MemoryStream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			stream.Seek(0L, SeekOrigin.Begin);
			return ((IFormatter)binaryFormatter).Deserialize((Stream)stream);
		}
	}
}
