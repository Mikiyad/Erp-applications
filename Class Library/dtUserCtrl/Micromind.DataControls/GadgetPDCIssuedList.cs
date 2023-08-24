using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GadgetPDCIssuedList : UserControl, IGadget
	{
		private bool hasAccess = true;

		private bool isDataLoaded;

		private bool isRefresh;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private IContainer components;

		private GridControl gridControl;

		private GridView gridView3;

		private GridView gridView4;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private ImageList imageList1;

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

		public bool IsBusy => backgroundWorker.IsBusy;

		public string GadgetTitle
		{
			get
			{
				return "PDCs Issued";
			}
			set
			{
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
			}
		}

		public GadgetTypes GadgetType => GadgetTypes.PDCIssuedList;

		public string GadgetID
		{
			get
			{
				return "SYS" + (int)GadgetType;
			}
			set
			{
			}
		}

		public string Description
		{
			get
			{
				return "List of PDCs issued and not yet matured.";
			}
			set
			{
			}
		}

		public GadgetCategories Category
		{
			get
			{
				return GadgetCategories.Accounts;
			}
			set
			{
			}
		}

		public ViewType ChartType
		{
			set
			{
			}
		}

		public event EventHandler DataLoadCompleted;

		public GadgetPDCIssuedList()
		{
			InitializeComponent();
			if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + GadgetType)).ToString()).Visible)
			{
				hasAccess = false;
			}
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
		}

		private void FavoriteBankAccountsGadget_ClientSizeChanged(object sender, EventArgs e)
		{
			Resize();
		}

		private void FavoriteBankAccountsGadget_SizeChanged(object sender, EventArgs e)
		{
			Resize();
		}

		private void FavoriteBankAccountsGadget_Resize(object sender, EventArgs e)
		{
			Resize();
		}

		private new void Resize()
		{
			pictureBoxWait.Left = base.Width / 2 - pictureBoxWait.Width / 2;
			pictureBoxWait.Top = base.Height / 2 - pictureBoxWait.Height / 2;
		}

		public void LoadData(bool isRefresh)
		{
			if (!IsBusy)
			{
				this.isRefresh = isRefresh;
				pictureBoxWait.Visible = true;
				pictureBoxWait.BringToFront();
				if (hasAccess)
				{
					backgroundWorker.RunWorkerAsync();
				}
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!isDataLoaded || isRefresh)
				{
					DataSet dataSet = e.Result as DataSet;
					if (dataSet.Tables.Count > 0)
					{
						DataTable dataTable = dataSet.Tables[0];
						dataTable.Columns.Remove("ChequeID");
						dataTable.Columns.Remove("Currency");
						dataTable.Columns.Remove("PDCAccountID");
						gridControl.DataSource = dataTable;
						ColumnView columnView = (ColumnView)gridControl.FocusedView;
						if (columnView != null && columnView.Columns.Count > 0)
						{
							FormatInfo displayFormat = columnView.Columns["Amount"].DisplayFormat;
							displayFormat.FormatType = FormatType.Numeric;
							displayFormat.FormatString = "#,##0.00";
						}
					}
					if (this.DataLoadCompleted != null)
					{
						this.DataLoadCompleted(sender, e);
					}
					isRefresh = false;
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

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				DateTime chequeDate = DateTime.Today.AddDays(7.0);
				DataSet dataSet = (DataSet)(e.Result = Factory.IssuedChequeSystemAsync.GetChequesToClearList(chequeDate));
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
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
			gridControl = new DevExpress.XtraGrid.GridControl();
			gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			imageList1 = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView4).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			SuspendLayout();
			gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
			gridControl.EmbeddedNavigator.Appearance.BackColor2 = System.Drawing.Color.FromArgb(255, 192, 192);
			gridControl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			gridControl.Location = new System.Drawing.Point(0, 0);
			gridControl.LookAndFeel.SkinName = "Office 2010 Silver";
			gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
			gridControl.MainView = gridView3;
			gridControl.Name = "gridControl";
			gridControl.Size = new System.Drawing.Size(415, 222);
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
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(gridControl);
			base.Controls.Add(pictureBoxWait);
			base.Name = "GadgetPDCIssuedList";
			base.Size = new System.Drawing.Size(415, 222);
			((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView4).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			ResumeLayout(false);
		}
	}
}
