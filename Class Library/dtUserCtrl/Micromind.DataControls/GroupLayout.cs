using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;

namespace Micromind.DataControls
{
	[Serializable]
	public class GroupLayout
	{
		public string ValueColumn = "";

		public string ArgumentColumn = "";

		public string Code = "";

		public bool IsCustom;

		public GadgetStyles GadgetStyle = GadgetStyles.List;

		public GadgetTypes GadgetType;

		public GadgetFilterOptions FilterOption;

		public int Height = 200;

		public int Index = -1;

		public bool Expanded = true;

		public string Title = "";

		public int ChartType;

		public int ChartPallet;

		public string Description = "";

		public GadgetCategories Category;

		public bool ColorEach;

		public string colorPaletteName = "Office";

		public LinksCollection Links = new LinksCollection();

		public List<ChartSerie> chartSeries = new List<ChartSerie>();

		public List<ChartSerie> ChartSeries
		{
			get
			{
				if (chartSeries == null)
				{
					chartSeries = new List<ChartSerie>();
				}
				return chartSeries;
			}
			set
			{
				chartSeries = value;
			}
		}

		public GroupLayout(GadgetTypes gadgetType, string code, GadgetCategories category, string title, int height, int index, bool expanded, GadgetStyles gadgetStyle, ViewType chartType, GadgetFilterOptions filterOption, bool isCustom)
		{
			Category = category;
			GadgetStyle = gadgetStyle;
			GadgetType = gadgetType;
			if (height > 0)
			{
				Height = height;
			}
			else
			{
				Height = 200;
			}
			Index = index;
			Expanded = expanded;
			Title = title;
			IsCustom = isCustom;
			Code = code;
			ChartType = (int)chartType;
			FilterOption = filterOption;
			ChartSeries = new List<ChartSerie>();
		}

		public GroupLayout(GadgetTypes gadgetType, string title, int height, int index, bool expanded)
		{
			GadgetType = gadgetType;
			Height = height;
			Index = index;
			Expanded = expanded;
			Title = title;
			ChartSeries = new List<ChartSerie>();
		}

		public GroupLayout(GadgetTypes gadgetType, string title, int height)
		{
			GadgetType = gadgetType;
			Title = title;
			Height = height;
			ChartSeries = new List<ChartSerie>();
		}

		public GroupLayout(GadgetTypes gadgetType, string title, GadgetCategories category, GadgetStyles style)
		{
			Category = category;
			GadgetType = gadgetType;
			Title = title;
			GadgetStyle = style;
			ChartSeries = new List<ChartSerie>();
		}

		public GroupLayout()
		{
			ChartSeries = new List<ChartSerie>();
		}
	}
}
