using Micromind.ClientLibraries;
using Micromind.DataCaches.Inventory;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class ProductBrandsComboBox : MultiColumnComboBox
	{
		private bool isKeyPressed;

		private bool showQuickAdd = true;

		private bool showAll;

		private Container components;

		private bool hasCustom;

		public bool ShowInactiveItems
		{
			get
			{
				return true;
			}
			set
			{
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

		public ProductBrandsComboBox()
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
			base.Name = "ProductBrandsComboBox";
			base.Size = new System.Drawing.Size(184, 0);
			ResumeLayout(false);
		}

		protected DataSet GetData(bool isReferesh)
		{
			return Brands.GetBrands(isReferesh);
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
				if (dataSet != null)
				{
					FillData(dataSet);
					base.IsDataLoaded = true;
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
