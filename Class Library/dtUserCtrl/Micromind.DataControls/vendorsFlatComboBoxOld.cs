using Micromind.ClientLibraries;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class vendorsFlatComboBoxOld : MultiColumnComboBox
	{
		private PartnerComboData vendor;

		private bool isKeyPressed;

		private bool showQuickAdd = true;

		private Container components;

		private bool hasAll;

		private bool hasCustom;

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

		public PartnerComboData SelectedItem
		{
			get
			{
				try
				{
					return new PartnerComboData();
				}
				catch
				{
					return null;
				}
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

		public vendorsFlatComboBoxOld()
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
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			base.Size = new System.Drawing.Size(288, 22);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}

		private void QuickAdd()
		{
		}

		protected DataSet GetData(bool isReferesh)
		{
			return null;
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
