using Micromind.UISupport;
using System.Data;

namespace Micromind.DataControls
{
	internal class JVRowTypeCombo : MMComboBox
	{
		public void LoadData()
		{
			SetupControl();
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			dataSet.Tables[0].Columns.Add("ID");
			dataSet.Tables[0].Columns.Add("Desc");
			dataSet.Tables[0].Rows.Add("1", "Account");
			dataSet.Tables[0].Rows.Add("2", "Customer");
			dataSet.Tables[0].Rows.Add("3", "Vendor");
			dataSet.Tables[0].Rows.Add("4", "Employee");
			base.DataSource = dataSet;
			base.ValueMember = "ID";
			base.DisplayMember = "Desc";
		}
	}
}
