using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Libraries;
using Micromind.DataControls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomGadgetList : UserControl, IGadget, ICustomGadget
	{
		private GadgetFilterOptions filterOption;

		private bool autoRefresh;

		private int refreshInterval = 300;

		private string gadgetTitle = "Custom Gadget";

		private string gadgetID = "";

		private bool hasAccess = true;

		private bool isDataLoaded;

		private bool isRefresh;

		private bool isInit;

		private string customGadgetID = "";

		private DataSet gadgetData;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private bool isLoaded;

		private GadgetStyles gadgetStyle = GadgetStyles.List;

		private IContainer components;

		private GridControl gridControl;

		private GridView gridView3;

		private GridView gridView4;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private Panel panel1;

		private ImageList imageList1;

		private Label labelPeriod;

		private GadgetDateRangeComboBox comboBoxPeriod;

		private Timer timerMain;

		public Micromind.DataControls.FlatDashboard.FlatDashboard ParentDashboard
		{
			get
			{
				return parentDashboard;
			}
			set
			{
				parentDashboard = value;
			}
		}

		public bool IsBusy => backgroundWorker.IsBusy;

		public ViewType ChartType
		{
			get
			{
				return ViewType.Bar;
			}
			set
			{
			}
		}

		public Image Icon
		{
			get
			{
				if (imageList1.Images.Count > 0)
				{
					return imageList1.Images[0];
				}
				return null;
			}
		}

		public GadgetFilterOptions FilterOption
		{
			get
			{
				return filterOption;
			}
			set
			{
				filterOption = value;
				if (value == GadgetFilterOptions.DatePeriod)
				{
					Label label = labelPeriod;
					bool visible = comboBoxPeriod.Visible = true;
					label.Visible = visible;
				}
				else
				{
					Label label2 = labelPeriod;
					bool visible = comboBoxPeriod.Visible = false;
					label2.Visible = visible;
				}
			}
		}

		public string GadgetTitle
		{
			get
			{
				return gadgetTitle;
			}
			set
			{
				gadgetTitle = value;
			}
		}

		public GadgetStyles GadgetStyle
		{
			get
			{
				return GadgetStyles.List;
			}
			set
			{
				gadgetStyle = value;
			}
		}

		public GadgetTypes GadgetType => GadgetTypes.Custom;

		public string GadgetID
		{
			get
			{
				return gadgetID;
			}
			set
			{
				gadgetID = value;
			}
		}

		public GroupLayout GroupLayout
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public GadgetCategories Category
		{
			get;
			set;
		}

		public event EventHandler DataLoadCompleted;

		public CustomGadgetList()
		{
			InitializeComponent();
			try
			{
				isInit = true;
				gridView3.Appearance.FocusedRow.BackColor = Color.LightGray;
				gridView3.Appearance.FocusedRow.ForeColor = Color.Black;
				gridView3.Appearance.FocusedCell.ForeColor = Color.Black;
				gridView3.Appearance.FocusedCell.BackColor = Color.LightGray;
				gridView3.Appearance.HideSelectionRow.BackColor = Color.White;
				gridView3.Appearance.HideSelectionRow.ForeColor = Color.Black;
				gridControl.LookAndFeel.SkinName = "Office 2010 Silver";
				gridView3.FocusRectStyle = DrawFocusRectStyle.None;
				base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
				base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
				backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
				gridView3.DoubleClick += gridView3_DoubleClick;
				comboBoxPeriod.SelectedIndexChanged += comboBoxPeriod_SelectedIndexChanged;
				comboBoxPeriod.LoadData();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isInit = false;
			}
		}

		private void gridView3_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				GridView gridView = (GridView)sender;
				Point pt = gridView.GridControl.PointToClient(Control.MousePosition);
				DoRowDoubleClick(gridView, pt);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void DoRowDoubleClick(GridView view, Point pt)
		{
			try
			{
				if (gadgetData != null)
				{
					SmartListDrillDownActions smartListDrillDownActions = SmartListDrillDownActions.None;
					DataRow dataRow = gadgetData.Tables[0].Rows[0];
					if (dataRow["DrillAction"] != DBNull.Value)
					{
						smartListDrillDownActions = (SmartListDrillDownActions)int.Parse(dataRow["DrillAction"].ToString());
					}
					switch (smartListDrillDownActions)
					{
					case SmartListDrillDownActions.OpenCard:
					{
						DataComboType cardType = (DataComboType)int.Parse(dataRow["DrillCardTypeID"].ToString());
						string text3 = dataRow["DrillCardIDField"].ToString();
						if (view.Columns.ColumnByFieldName(text3) == null)
						{
							ErrorHelper.WarningMessage("Column name: " + text3 + " does not exist.");
						}
						else
						{
							string docNumber2 = view.GetRowCellValue(view.FocusedRowHandle, text3).ToString();
							ParentDashboard.OnGadgetDoubleClick(smartListDrillDownActions, cardType, "", docNumber2, isPreview: false);
						}
						break;
					}
					case SmartListDrillDownActions.OpenTransaction:
					{
						string text = dataRow["DrillTransactionVoucherIDField"].ToString();
						string text2 = dataRow["DrillTransactionSysDocIDField"].ToString();
						bool isPreview = false;
						if (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["IsPreview"]))
						{
							isPreview = bool.Parse(dataRow["IsPreview"].ToString());
						}
						if (view.Columns.ColumnByFieldName(text) == null)
						{
							ErrorHelper.WarningMessage("Column name: " + text + " does not exist.");
						}
						else if (view.Columns.ColumnByFieldName(text2) == null)
						{
							ErrorHelper.WarningMessage("Column name: " + text2 + " does not exist.");
						}
						else
						{
							string docNumber = view.GetRowCellValue(view.FocusedRowHandle, text).ToString();
							string sysDocID = view.GetRowCellValue(view.FocusedRowHandle, text2).ToString();
							ParentDashboard.OnGadgetDoubleClick(smartListDrillDownActions, DataComboType.None, sysDocID, docNumber, isPreview);
						}
						break;
					}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!comboBoxPeriod.IsLoading && !isInit)
			{
				LoadData(isRefresh: true);
			}
		}

		private void FavoriteBankAccountsGadget_ClientSizeChanged(object sender, EventArgs e)
		{
			ResizeColumns();
		}

		private void FavoriteBankAccountsGadget_SizeChanged(object sender, EventArgs e)
		{
			ResizeColumns();
		}

		private void FavoriteBankAccountsGadget_Resize(object sender, EventArgs e)
		{
			ResizeColumns();
		}

		private void ResizeColumns()
		{
			pictureBoxWait.Left = base.Width / 2 - pictureBoxWait.Width / 2;
			pictureBoxWait.Top = base.Height / 2 - pictureBoxWait.Height / 2;
		}

		public void LoadData(bool isRefresh)
		{
			if (IsBusy)
			{
				return;
			}
			this.isRefresh = isRefresh;
			pictureBoxWait.Visible = true;
			pictureBoxWait.BringToFront();
			if (!isLoaded)
			{
				if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, gadgetID).Visible)
				{
					hasAccess = false;
				}
				isLoaded = true;
			}
			if (hasAccess)
			{
				backgroundWorker.RunWorkerAsync();
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!isDataLoaded || isRefresh)
				{
					customGadgetID = gadgetData.Tables[0].Rows[0]["CustomGadgetID"].ToString();
					if (e.Error == null)
					{
						if (gadgetData != null)
						{
							if (gadgetData.Tables[0].Rows[0]["AutoRefresh"] != DBNull.Value)
							{
								autoRefresh = bool.Parse(gadgetData.Tables[0].Rows[0]["AutoRefresh"].ToString());
							}
							if (gadgetData.Tables[0].Rows[0]["RefreshInterval"] != DBNull.Value)
							{
								refreshInterval = int.Parse(gadgetData.Tables[0].Rows[0]["RefreshInterval"].ToString());
							}
							if (autoRefresh)
							{
								timerMain.Interval = refreshInterval * 1000;
								Timer timer = timerMain;
								bool enabled = base.Enabled = true;
								timer.Enabled = enabled;
							}
						}
						DataSet dataSet = e.Result as DataSet;
						if (dataSet != null && dataSet.Tables.Count != 0)
						{
							gridControl.DataSource = dataSet.Tables[0];
							ColumnView columnView = (ColumnView)gridControl.FocusedView;
							if (columnView != null && columnView.Columns.Count > 0)
							{
								foreach (GridColumn column in columnView.Columns)
								{
									if (CheckVisibility(column))
									{
										column.Visible = false;
									}
									if (column.ColumnType == typeof(decimal) || column.ColumnType == typeof(int))
									{
										FormatInfo displayFormat = column.DisplayFormat;
										displayFormat.FormatType = FormatType.Numeric;
										displayFormat.FormatString = "#,##0";
									}
									if (column.ColumnType == typeof(decimal))
									{
										FormatInfo displayFormat2 = column.DisplayFormat;
										displayFormat2.FormatType = FormatType.Custom;
										displayFormat2.FormatString = Format.TotalAmountFormat;
									}
								}
							}
							if (this.DataLoadCompleted != null)
							{
								this.DataLoadCompleted(sender, e);
							}
							isRefresh = false;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				pictureBoxWait.Visible = false;
			}
		}

		private bool CheckVisibility(GridColumn col)
		{
			bool result = false;
			CustomGadgetData customGadgetByID = Factory.CustomGadgetSystem.GetCustomGadgetByID(customGadgetID.Trim());
			if (customGadgetByID == null || customGadgetByID.Tables.Count == 0 || customGadgetByID.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			if (customGadgetByID.Tables.Contains("ListHiddenFields") && customGadgetByID.Tables["ListHiddenFields"].Rows.Count > 0)
			{
				foreach (DataRow row in customGadgetByID.ListHiddenFieldsTable.Rows)
				{
					string str = row["FieldID"].ToString();
					if (col.Name == "col" + str)
					{
						result = true;
					}
				}
				return result;
			}
			return false;
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				_ = comboBoxPeriod.FromDate;
				_ = comboBoxPeriod.ToDate;
				CustomGadgetData customGadgetByID = Factory.CustomGadgetSystem.GetCustomGadgetByID(GadgetID);
				if (customGadgetByID.Tables.Count != 0 && customGadgetByID.CustomGadgetTable.Rows.Count != 0)
				{
					gadgetData = customGadgetByID;
					CustomGadget obj = (CustomGadget)Global.DeserializeFromStream((byte[])customGadgetByID.CustomGadgetTable.Rows[0]["GadgetData"]);
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.Parameters.AddWithValue("@FromDate", CommonLib.ToSqlDateTimeString(comboBoxPeriod.FromDate));
					sqlCommand.Parameters.AddWithValue("@EndDate", CommonLib.ToSqlDateTimeString(comboBoxPeriod.ToDate));
					foreach (GadgetParameter parameter in obj.Parameters)
					{
						if (!sqlCommand.Parameters.Contains(parameter.ParameterName))
						{
							sqlCommand.Parameters.AddWithValue(parameter.ParameterName, "NULL");
						}
					}
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					foreach (SqlParameter parameter2 in sqlCommand.Parameters)
					{
						arrayList.Add(parameter2.ParameterName);
						arrayList2.Add(parameter2.Value.ToString());
					}
					Array array = arrayList.ToArray(typeof(string));
					Array array2 = arrayList2.ToArray(typeof(string));
					DataSet dataSet = (DataSet)(e.Result = Factory.CustomGadgetSystemAsync.GetCustomGadgetData(GadgetID, (string[])array, (string[])array2));
				}
			}
			catch (Exception)
			{
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
		}

		private void timerMain_Tick(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
		}

		public void Init()
		{
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode = new DevExpress.XtraGrid.GridLevelNode();
			Micromind.DataControls.Libraries.ComboData comboData = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData2 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData3 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData4 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData5 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData6 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData7 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData8 = new Micromind.DataControls.Libraries.ComboData();
			gridControl = new DevExpress.XtraGrid.GridControl();
			gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			labelPeriod = new System.Windows.Forms.Label();
			comboBoxPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			imageList1 = new System.Windows.Forms.ImageList(components);
			timerMain = new System.Windows.Forms.Timer(components);
			((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView4).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).BeginInit();
			SuspendLayout();
			gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
			gridControl.EmbeddedNavigator.Appearance.BackColor2 = System.Drawing.Color.FromArgb(255, 192, 192);
			gridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			gridLevelNode.RelationName = "Level1";
			gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[1]
			{
				gridLevelNode
			});
			gridControl.Location = new System.Drawing.Point(0, 0);
			gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
			gridControl.MainView = gridView3;
			gridControl.Name = "gridControl";
			gridControl.Size = new System.Drawing.Size(415, 195);
			gridControl.TabIndex = 7;
			gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[2]
			{
				gridView3,
				gridView4
			});
			gridView3.GridControl = gridControl;
			gridView3.Name = "gridView3";
			gridView3.OptionsBehavior.Editable = false;
			gridView3.OptionsCustomization.AllowGroup = false;
			gridView3.OptionsSelection.UseIndicatorForSelection = false;
			gridView3.OptionsView.EnableAppearanceOddRow = true;
			gridView3.OptionsView.ShowGroupPanel = false;
			gridView3.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
			gridView3.OptionsView.ShowIndicator = false;
			gridView3.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
			gridView4.GridControl = gridControl;
			gridView4.Name = "gridView4";
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			pictureBoxWait.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBoxWait.BackColor = System.Drawing.Color.White;
			pictureBoxWait.Image = Micromind.DataControls.Properties.Resources.wait;
			pictureBoxWait.Location = new System.Drawing.Point(179, 95);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 9;
			pictureBoxWait.TabStop = false;
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(labelPeriod);
			panel1.Controls.Add(comboBoxPeriod);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 195);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(415, 27);
			panel1.TabIndex = 12;
			labelPeriod.AutoSize = true;
			labelPeriod.Location = new System.Drawing.Point(6, 6);
			labelPeriod.Name = "labelPeriod";
			labelPeriod.Size = new System.Drawing.Size(40, 13);
			labelPeriod.TabIndex = 6;
			labelPeriod.Text = "Period:";
			comboData.FieldType = System.Data.DbType.AnsiString;
			comboData.ID = "2";
			comboData.Name = "This Month";
			comboData.Tag = null;
			comboBoxPeriod.EditValue = comboData;
			comboBoxPeriod.Location = new System.Drawing.Point(51, 3);
			comboBoxPeriod.Name = "comboBoxPeriod";
			comboBoxPeriod.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
			comboBoxPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboData2.FieldType = System.Data.DbType.AnsiString;
			comboData2.ID = "0";
			comboData2.Name = "Today";
			comboData2.Tag = null;
			comboData3.FieldType = System.Data.DbType.AnsiString;
			comboData3.ID = "6";
			comboData3.Name = "This Week";
			comboData3.Tag = null;
			comboData4.FieldType = System.Data.DbType.AnsiString;
			comboData4.ID = "2";
			comboData4.Name = "This Month";
			comboData4.Tag = null;
			comboData5.FieldType = System.Data.DbType.AnsiString;
			comboData5.ID = "3";
			comboData5.Name = "This Year";
			comboData5.Tag = null;
			comboData6.FieldType = System.Data.DbType.AnsiString;
			comboData6.ID = "0";
			comboData6.Name = "Today";
			comboData6.Tag = null;
			comboData7.FieldType = System.Data.DbType.AnsiString;
			comboData7.ID = "6";
			comboData7.Name = "This Week";
			comboData7.Tag = null;
			comboData8.FieldType = System.Data.DbType.AnsiString;
			comboData8.ID = "3";
			comboData8.Name = "This Year";
			comboData8.Tag = null;
			comboBoxPeriod.Properties.Items.AddRange(new object[8]
			{
				comboData2,
				comboData3,
				comboData4,
				comboData5,
				comboData6,
				comboData7,
				comboData,
				comboData8
			});
			comboBoxPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxPeriod.Size = new System.Drawing.Size(106, 20);
			comboBoxPeriod.TabIndex = 5;
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			timerMain.Tick += new System.EventHandler(timerMain_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(gridControl);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBoxWait);
			base.Name = "CustomGadgetList";
			base.Size = new System.Drawing.Size(415, 222);
			((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView4).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).EndInit();
			ResumeLayout(false);
		}
	}
}
