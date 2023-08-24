using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class DatabaseComboBox : MultiColumnComboBox
	{
		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

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

		public bool HasAllAccount
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

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedVersion
		{
			get
			{
				if (base.SelectedRow != null && SelectedID != "")
				{
					return base.SelectedRow.Cells["DBVersion"].Text.ToString();
				}
				return "";
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedDataVersion
		{
			get
			{
				if (base.SelectedRow != null && SelectedID != "")
				{
					if (base.SelectedRow.Cells.Contains("DBDataVersion"))
					{
						return base.SelectedRow.Cells["DBDataVersion"].Text.ToString();
					}
					return "1.8.11.140";
				}
				return "";
			}
		}

		public DatabaseComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Database;
			TABLENAME_FIELD = "";
			ID_FIELD = "";
			NAME_FIELD = "";
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
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		private void QuickAdd()
		{
		}

		private DataSet GetData(bool isReferesh)
		{
			return null;
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			try
			{
				Application.DoEvents();
				string currentInstanceName = Global.CurrentInstanceName;
				if (!GlobalRules.IsCorrectServerName(currentInstanceName.Trim()))
				{
					ErrorHelper.WarningMessage("Incorrect server name.", "Cannot connect to server:", currentInstanceName);
				}
				else
				{
					DataSet databases = Factory.GetDatabases(Global.CurrentInstanceName);
					if (databases == null)
					{
						ErrorHelper.ErrorMessage("Cannot connect to database server.", "Please try again.");
					}
					else
					{
						databases.Tables[0].Columns["Database_name"].ColumnName = "Code";
						databases.Tables[0].Columns["CompanyName"].ColumnName = "Name";
						for (int i = 0; i < databases.Tables[0].Columns.Count; i++)
						{
							if (databases.Tables[0].Columns[i].ColumnName != "Code" && databases.Tables[0].Columns[i].ColumnName != "Name" && databases.Tables[0].Columns[i].ColumnName != "DBDataVersion" && databases.Tables[0].Columns[i].ColumnName != "DBVersion")
							{
								databases.Tables[0].Columns.RemoveAt(i);
								i--;
							}
						}
						for (int j = 0; j < databases.Tables[0].Rows.Count; j++)
						{
							if (databases.Tables[0].Rows[j]["Name"].ToString() == "")
							{
								databases.Tables[0].Rows.RemoveAt(j);
								j--;
							}
						}
						base.DataSource = databases;
						base.DisplayLayout.Bands[0].Columns["DBVersion"].Hidden = true;
						base.DisplayLayout.Bands[0].Columns["DBDataVersion"].Hidden = true;
						base.IsDataLoaded = true;
					}
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
