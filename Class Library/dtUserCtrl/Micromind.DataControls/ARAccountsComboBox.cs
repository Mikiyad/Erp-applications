using Micromind.ClientLibraries;
using Micromind.DataCaches.Accounts;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class ARAccountsComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private Container components;

		public ARAccountsComboBox()
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
			SuspendLayout();
			base.Name = "ARAccountsComboBox";
			base.Size = new System.Drawing.Size(216, 20);
			ResumeLayout(false);
		}

		protected DataSet GetData(bool isReferesh)
		{
			try
			{
				return AllAccounts.GetARAccounts(isReferesh);
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
