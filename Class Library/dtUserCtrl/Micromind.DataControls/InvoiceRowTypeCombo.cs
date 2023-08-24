using Infragistics.Win.UltraWinGrid;
using System.Data;

namespace Micromind.DataControls
{
	internal class InvoiceRowTypeCombo : UltraCombo
	{
		public void LoadData()
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			dataSet.Tables[0].Columns.Add("ID");
			dataSet.Tables[0].Columns.Add("Desc");
			dataSet.Tables[0].Rows.Add("1", "Item");
			dataSet.Tables[0].Rows.Add("2", "Account");
			dataSet.Tables[0].Rows.Add("3", "Remarks");
			dataSet.Tables[0].Rows.Add("4", "Subtotal");
			base.DataSource = dataSet;
			base.ValueMember = "ID";
			base.DisplayMember = "Desc";
		}
	}
}
