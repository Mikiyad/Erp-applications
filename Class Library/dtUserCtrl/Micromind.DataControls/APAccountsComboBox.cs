using Micromind.ClientLibraries;
using Micromind.DataCaches.Accounts;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class APAccountsComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private Container components;

		public APAccountsComboBox()
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
			base.Name = "APAccountsComboBox";
			base.Size = new System.Drawing.Size(448, 20);
		}

		protected DataSet GetData(bool isReferesh)
		{
			try
			{
				return AllAccounts.GetAPAccounts(isReferesh);
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return null;
		}

		public new void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			FillData(GetData(isReferesh));
			base.IsDataLoaded = true;
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
	}
}
