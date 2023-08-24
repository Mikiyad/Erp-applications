using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class PayeesComboBox : MultiColumnComboBox
	{
		private bool showInactiveItems;

		private bool showQuickAdd = true;

		private Container components;

		private bool hasAll;

		public PayeeComboData SelectedItem
		{
			get
			{
				UltraGridRow selectedRow = base.SelectedRow;
				if (selectedRow == null)
				{
					return null;
				}
				return new PayeeComboData
				{
					ID = int.Parse(selectedRow.Cells[0].Value.ToString()),
					Name = selectedRow.Cells[1].Value.ToString()
				};
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

		public bool HasAll
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

		public PayeesComboBox()
		{
			InitializeComponent();
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
			base.Name = "PayeesComboBox";
			base.Size = new System.Drawing.Size(288, 0);
			ResumeLayout(false);
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		public new void LoadData()
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

		public DataSet GetData(bool isReferesh)
		{
			return null;
		}

		private new void FillData(DataSet data)
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					if (data == null)
					{
						base.DataSource = new DataSet();
					}
					else
					{
						base.DataSource = data;
					}
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

		private void QuickAdd()
		{
		}
	}
}
