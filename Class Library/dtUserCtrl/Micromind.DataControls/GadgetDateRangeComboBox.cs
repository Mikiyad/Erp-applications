using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;

namespace Micromind.DataControls
{
	public class GadgetDateRangeComboBox : ComboBoxEdit
	{
		public new bool IsLoading;

		private IContainer components;

		public DateTime FromDate => GetRange().From;

		public DateTime ToDate => GetRange().To;

		public GadgetDateRangeComboBox()
		{
			InitializeComponent();
			base.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
		}

		public GadgetDateRangeComboBox(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public void LoadData()
		{
			IsLoading = true;
			base.Properties.Items.Clear();
			base.Properties.Items.AddRange(HelperLib.GetDateRangeComboList(includeCustom: false));
			SelectedIndex = 4;
			IsLoading = false;
		}

		public DateRangeStruct GetRange()
		{
			DateRangeStruct dateRangeStruct = default(DateRangeStruct);
			if (SelectedItem == null)
			{
				return HelperLib.GetDateRange(DatePeriods.ThisMonthToDate);
			}
			dateRangeStruct = default(DateRangeStruct);
			ComboData comboData = (ComboData)SelectedItem;
			if (comboData != null)
			{
				return HelperLib.GetDateRange((DatePeriods)int.Parse(comboData.ID));
			}
			return dateRangeStruct;
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
			components = new System.ComponentModel.Container();
		}
	}
}
