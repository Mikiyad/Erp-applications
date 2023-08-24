using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using Infragistics.Win.FormattedLinkLabel;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class Dashboard : UserControl
	{
		public delegate void GadgetDoubleClick(SmartListDrillDownActions action, DataComboType cardType, string sysDocID, string docNumber, bool isPreview);

		public enum DashboardSection
		{
			Left,
			Right,
			Last,
			CrossTop
		}

		private DashboardLayout dashboardLayout = new DashboardLayout();

		private bool isFirstTimeLoading = true;

		private string key = "";

		private bool hasError;

		private IContainer components;

		private Panel panelTop;

		private LabelControl labelCustomize;

		private SplitContainer splitContainerHorizontal;

		private DashboardNavBar navBarCross;

		private SplitContainer splitContainerMain;

		private DashboardNavBar navBarLeft;

		private SplitContainer splitContainerSecond;

		private DashboardNavBar navBarRight;

		private DashboardNavBar navBarLast;

		public bool IsLoaded => !isFirstTimeLoading;

		public string DashboardKey
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
			}
		}

		public NavGroupCollection LeftGadgets => navBarLeft.Groups;

		public NavGroupCollection RightGadgets => navBarRight.Groups;

		public NavGroupCollection LastGadgets => navBarLast.Groups;

		public NavGroupCollection crossColumnGadgets => navBarCross.Groups;

		public event GadgetDoubleClick GadgetListDoubleClick;

		public void OnGadgetDoubleClick(SmartListDrillDownActions action, DataComboType cardType, string sysDocID, string docNumber, bool isPreview)
		{
			if (this.GadgetListDoubleClick != null)
			{
				this.GadgetListDoubleClick(action, cardType, sysDocID, docNumber, isPreview);
			}
		}

		public Dashboard()
		{
			InitializeComponent();
			if (key == "")
			{
				key = "DSHB" + base.Name;
			}
			if (base.ParentForm != null)
			{
				key += base.ParentForm.Name;
			}
		}

		public void AddGadget(GroupLayout layout, DashboardSection section)
		{
			DashboardNavBar dashboardNavBar;
			switch (section)
			{
			default:
				return;
			case DashboardSection.Left:
				dashboardNavBar = navBarLeft;
				break;
			case DashboardSection.Right:
				dashboardNavBar = navBarRight;
				break;
			case DashboardSection.Last:
				dashboardNavBar = navBarLast;
				break;
			case DashboardSection.CrossTop:
				dashboardNavBar = navBarCross;
				break;
			}
			IGadget gadget = GadgetsHelper.CreateGadget(layout);
			if (gadget.GadgetStyle == GadgetStyles.Link)
			{
				NavBarGroup navBarGroup = new NavBarGroup(gadget.GadgetTitle);
				dashboardNavBar.Groups.Add(navBarGroup);
				navBarGroup.AppearanceBackground.BorderColor = Color.White;
				navBarGroup.Expanded = layout.Expanded;
				navBarGroup.GroupStyle = NavBarGroupStyle.SmallIconsList;
				navBarGroup.Tag = gadget;
				if (gadget.Icon != null)
				{
					navBarGroup.LargeImage = gadget.Icon;
				}
				ILinkGadget obj = (ILinkGadget)gadget;
				obj.Links = layout.Links;
				obj.ParentNavBarGroup = navBarGroup;
				gadget.LoadData(isRefresh: false);
				return;
			}
			NavBarGroup navBarGroup2 = new NavBarGroup(gadget.GadgetTitle);
			dashboardNavBar.Groups.Add(navBarGroup2);
			navBarGroup2.AppearanceBackground.BorderColor = Color.White;
			navBarGroup2.GroupClientHeight = layout.Height;
			navBarGroup2.Expanded = layout.Expanded;
			navBarGroup2.GroupStyle = NavBarGroupStyle.SmallIconsList;
			navBarGroup2.GroupStyle = NavBarGroupStyle.ControlContainer;
			if (gadget.Icon != null)
			{
				navBarGroup2.LargeImage = gadget.Icon;
			}
			if (navBarGroup2.ControlContainer != null)
			{
				navBarGroup2.ControlContainer.ControlAdded += ControlContainer_ControlAdded;
				navBarGroup2.ControlContainer.Controls.Add((Control)gadget);
			}
			navBarGroup2.GroupClientHeight = layout.Height;
		}

		private void ControlContainer_ControlAdded(object sender, ControlEventArgs e)
		{
			NavBarGroupControlContainer navBarGroupControlContainer = sender as NavBarGroupControlContainer;
			IGadget gadget = e.Control as IGadget;
			if (gadget != null)
			{
				Control control = e.Control;
				control.Left = 0;
				control.Top = 0;
				control.Width = navBarGroupControlContainer.Width;
				control.Height = navBarGroupControlContainer.Height;
				control.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				gadget.LoadData(isRefresh: false);
			}
		}

		public bool LoadLayout()
		{
			try
			{
				bool flag = true;
				if (!Factory.IsDBConnected)
				{
					return true;
				}
				navBarLeft.ExplorerBarGroupInterval = 8;
				navBarRight.ExplorerBarGroupInterval = 8;
				navBarLast.ExplorerBarGroupInterval = 8;
				byte[] binaryData = Factory.SettingSystemAsync.GetBinaryData(Global.CurrentUser, key);
				DataTable customGadgetTable = Factory.CustomGadgetSystem.GetCustomGadgets().CustomGadgetTable;
				if (binaryData == null)
				{
					return false;
				}
				if (binaryData.Length == 0)
				{
					return false;
				}
				object obj = GadgetsHelper.DeserializeFromStream(binaryData);
				if (obj == null)
				{
					return true;
				}
				dashboardLayout = (obj as DashboardLayout);
				if (dashboardLayout.IsCrossColumnRow)
				{
					splitContainerHorizontal.Panel1.Show();
					splitContainerHorizontal.Panel1Collapsed = false;
				}
				else
				{
					splitContainerHorizontal.Panel1.Hide();
					splitContainerHorizontal.Panel1Collapsed = true;
				}
				if (dashboardLayout.IsThreeColumn)
				{
					splitContainerSecond.Panel2.Show();
					splitContainerSecond.Panel2Collapsed = false;
				}
				else
				{
					splitContainerSecond.Panel2.Hide();
					splitContainerSecond.Panel2Collapsed = true;
				}
				navBarLeft.Groups.Clear();
				navBarRight.Groups.Clear();
				navBarCross.Groups.Clear();
				navBarLast.Groups.Clear();
				foreach (GroupLayout groupLayout in dashboardLayout.LeftBarLayout.GroupLayoutList)
				{
					AddToBar(groupLayout, customGadgetTable, DashboardSection.Left);
				}
				foreach (GroupLayout groupLayout2 in dashboardLayout.RightBarLayout.GroupLayoutList)
				{
					AddToBar(groupLayout2, customGadgetTable, DashboardSection.Right);
				}
				foreach (GroupLayout groupLayout3 in dashboardLayout.LastBarLayout.GroupLayoutList)
				{
					AddToBar(groupLayout3, customGadgetTable, DashboardSection.Last);
				}
				foreach (GroupLayout groupLayout4 in dashboardLayout.CrossBarLayout.GroupLayoutList)
				{
					AddToBar(groupLayout4, customGadgetTable, DashboardSection.CrossTop);
				}
				if (isFirstTimeLoading)
				{
					isFirstTimeLoading = false;
					int width = dashboardLayout.LeftBarLayout.Width;
					int width2 = dashboardLayout.RightBarLayout.Width;
					_ = dashboardLayout.LastBarLayout.Width;
					if (width > 0 || width2 > 0)
					{
						int width3 = splitContainerHorizontal.Width;
						width = width3 * width / 100;
						width2 = width3 * width2 / 100;
						if (splitContainerMain.Panel1MinSize > width)
						{
							splitContainerMain.SplitterDistance = splitContainerMain.Panel1MinSize + 1;
						}
						else
						{
							splitContainerMain.SplitterDistance = width;
						}
						if (splitContainerSecond.Panel1MinSize > width2)
						{
							splitContainerSecond.SplitterDistance = splitContainerSecond.Panel1MinSize + 1;
						}
						else
						{
							splitContainerSecond.SplitterDistance = width2;
						}
					}
				}
				if (flag)
				{
					hasError = false;
				}
				return flag;
			}
			catch (Exception e)
			{
				hasError = true;
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void AddToBar(GroupLayout group, DataTable customTable, DashboardSection section)
		{
			if (group == null || !Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, group.Code).Visible)
			{
				return;
			}
			if (group.IsCustom)
			{
				DataRow[] array = customTable.Select("CustomGadgetID = '" + group.Code + "'");
				if (array.Length == 0)
				{
					return;
				}
				group.Title = array[0]["CustomGadgetName"].ToString();
				group.ValueColumn = array[0]["ChartValueColumn"].ToString();
				group.ArgumentColumn = array[0]["ChartArgColumn"].ToString();
				if (array[0]["ChartType"] != DBNull.Value)
				{
					group.ChartType = int.Parse(array[0]["ChartType"].ToString());
				}
				if (array[0]["ColorEach"] != DBNull.Value)
				{
					group.ColorEach = bool.Parse(array[0]["ColorEach"].ToString());
				}
				if (array[0]["ColorPaletteName"] != DBNull.Value)
				{
					group.ChartType = int.Parse(array[0]["ColorPaletteName"].ToString());
				}
				if (array[0]["FilterOption"] != DBNull.Value)
				{
					group.FilterOption = (GadgetFilterOptions)int.Parse(array[0]["FilterOption"].ToString());
				}
				else
				{
					group.FilterOption = GadgetFilterOptions.None;
				}
				if (array[0]["GadgetStyle"] != DBNull.Value)
				{
					group.GadgetStyle = (GadgetStyles)int.Parse(array[0]["GadgetStyle"].ToString());
				}
				else
				{
					group.GadgetStyle = GadgetStyles.List;
				}
			}
			AddGadget(group, section);
		}

		public bool SaveLayout()
		{
			try
			{
				bool flag = true;
				dashboardLayout = new DashboardLayout();
				dashboardLayout.IsCrossColumnRow = !splitContainerHorizontal.Panel1Collapsed;
				dashboardLayout.IsThreeColumn = !splitContainerSecond.Panel2Collapsed;
				foreach (NavBarGroup group5 in navBarLeft.Groups)
				{
					SaveGadget(navBarLeft, group5, dashboardLayout.LeftBarLayout);
				}
				foreach (NavBarGroup group6 in navBarRight.Groups)
				{
					SaveGadget(navBarRight, group6, dashboardLayout.RightBarLayout);
				}
				foreach (NavBarGroup group7 in navBarLast.Groups)
				{
					SaveGadget(navBarLast, group7, dashboardLayout.LastBarLayout);
				}
				foreach (NavBarGroup group8 in navBarCross.Groups)
				{
					SaveGadget(navBarCross, group8, dashboardLayout.CrossBarLayout);
				}
				int width = splitContainerHorizontal.Panel2.Width;
				int width2 = splitContainerMain.Panel1.Width * 100 / width;
				int width3 = splitContainerSecond.Panel1.Width * 100 / width;
				dashboardLayout.LeftBarLayout.Width = width2;
				dashboardLayout.RightBarLayout.Width = width3;
				MemoryStream memoryStream = GadgetsHelper.SerializeToStream(dashboardLayout);
				return flag & Factory.SettingSystem.SaveSettingStream(key, Global.CurrentUser, memoryStream.ToArray());
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SaveGadget(DashboardNavBar navBar, NavBarGroup group, NavBarLayout barLayout)
		{
			bool colorEach = false;
			string colorPaletteName = "Office";
			IGadget groupGadget = navBar.GetGroupGadget(group);
			if (groupGadget == null)
			{
				return;
			}
			bool flag = groupGadget.GadgetType == GadgetTypes.Custom;
			ViewType chartType = ViewType.Bar;
			GadgetFilterOptions filterOption = GadgetFilterOptions.None;
			if (flag)
			{
				chartType = ((ICustomGadget)groupGadget).ChartType;
				filterOption = ((ICustomGadget)groupGadget).FilterOption;
				if (groupGadget.GetType() == typeof(ICustomGadgetChart))
				{
					ICustomGadgetChart obj = groupGadget as ICustomGadgetChart;
					colorEach = obj.ColorEach;
					colorPaletteName = obj.ColorPaletteName;
				}
			}
			GroupLayout groupLayout = new GroupLayout(groupGadget.GadgetType, groupGadget.GadgetID, groupGadget.Category, groupGadget.GadgetTitle, group.GroupClientHeight, navBar.Groups.IndexOf(group), group.Expanded, groupGadget.GadgetStyle, chartType, filterOption, flag);
			groupLayout.colorPaletteName = colorPaletteName;
			groupLayout.ColorEach = colorEach;
			if (groupGadget is ILinkGadget)
			{
				groupLayout.Links = ((ILinkGadget)groupGadget).Links;
			}
			barLayout.GroupLayoutList.Add(groupLayout);
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!hasError)
			{
				SaveLayout();
			}
			if (new CustomizeDashboardForm
			{
				DashboardKey = key
			}.ShowDialog(this) == DialogResult.OK)
			{
				LoadLayout();
			}
		}

		public void Clear()
		{
			navBarLeft.Groups.Clear();
			navBarRight.Groups.Clear();
			navBarCross.Groups.Clear();
			navBarLast.Groups.Clear();
		}

		public void SetDisable(bool isDisable)
		{
			base.Enabled = !isDisable;
			if (isDisable)
			{
				Clear();
			}
		}

		private void panelTop_Click(object sender, EventArgs e)
		{
		}

		private void labelCustomize_Click(object sender, EventArgs e)
		{
			SaveLayout();
			if (new CustomizeDashboardForm
			{
				DashboardKey = key
			}.ShowDialog(this) == DialogResult.OK)
			{
				LoadLayout();
			}
		}

		private void labelCustomize_MouseEnter(object sender, EventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		private void labelCustomize_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
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
			panelTop = new System.Windows.Forms.Panel();
			labelCustomize = new DevExpress.XtraEditors.LabelControl();
			splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
			navBarCross = new Micromind.DataControls.DashboardNavBar(components);
			splitContainerMain = new System.Windows.Forms.SplitContainer();
			navBarLeft = new Micromind.DataControls.DashboardNavBar(components);
			splitContainerSecond = new System.Windows.Forms.SplitContainer();
			navBarRight = new Micromind.DataControls.DashboardNavBar(components);
			navBarLast = new Micromind.DataControls.DashboardNavBar(components);
			panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerHorizontal).BeginInit();
			splitContainerHorizontal.Panel1.SuspendLayout();
			splitContainerHorizontal.Panel2.SuspendLayout();
			splitContainerHorizontal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)navBarCross).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
			splitContainerMain.Panel1.SuspendLayout();
			splitContainerMain.Panel2.SuspendLayout();
			splitContainerMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)navBarLeft).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainerSecond).BeginInit();
			splitContainerSecond.Panel1.SuspendLayout();
			splitContainerSecond.Panel2.SuspendLayout();
			splitContainerSecond.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)navBarRight).BeginInit();
			((System.ComponentModel.ISupportInitialize)navBarLast).BeginInit();
			SuspendLayout();
			panelTop.BackColor = System.Drawing.Color.White;
			panelTop.Controls.Add(labelCustomize);
			panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			panelTop.Location = new System.Drawing.Point(0, 0);
			panelTop.Name = "panelTop";
			panelTop.Size = new System.Drawing.Size(1062, 25);
			panelTop.TabIndex = 16;
			panelTop.Click += new System.EventHandler(panelTop_Click);
			labelCustomize.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			labelCustomize.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
			labelCustomize.Appearance.Options.UseForeColor = true;
			labelCustomize.Appearance.Options.UseTextOptions = true;
			labelCustomize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelCustomize.Location = new System.Drawing.Point(988, 7);
			labelCustomize.Name = "labelCustomize";
			labelCustomize.Size = new System.Drawing.Size(61, 13);
			labelCustomize.TabIndex = 63;
			labelCustomize.Text = "Customize...";
			labelCustomize.Click += new System.EventHandler(labelCustomize_Click);
			labelCustomize.MouseEnter += new System.EventHandler(labelCustomize_MouseEnter);
			labelCustomize.MouseLeave += new System.EventHandler(labelCustomize_MouseLeave);
			splitContainerHorizontal.BackColor = System.Drawing.Color.FromArgb(233, 233, 233);
			splitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerHorizontal.Location = new System.Drawing.Point(0, 25);
			splitContainerHorizontal.Name = "splitContainerHorizontal";
			splitContainerHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainerHorizontal.Panel1.Controls.Add(navBarCross);
			splitContainerHorizontal.Panel1Collapsed = true;
			splitContainerHorizontal.Panel2.Controls.Add(splitContainerMain);
			splitContainerHorizontal.Size = new System.Drawing.Size(1062, 557);
			splitContainerHorizontal.SplitterDistance = 252;
			splitContainerHorizontal.SplitterWidth = 7;
			splitContainerHorizontal.TabIndex = 21;
			navBarCross.ActiveGroup = null;
			navBarCross.BackColor = System.Drawing.Color.White;
			navBarCross.Dock = System.Windows.Forms.DockStyle.Fill;
			navBarCross.DragDropFlags = (DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop);
			navBarCross.ExplorerBarGroupInterval = 2;
			navBarCross.ExplorerBarGroupOuterIndent = 2;
			navBarCross.ExplorerBarShowGroupButtons = false;
			navBarCross.Location = new System.Drawing.Point(0, 0);
			navBarCross.Name = "navBarCross";
			navBarCross.OptionsNavPane.ExpandedWidth = 150;
			navBarCross.Size = new System.Drawing.Size(150, 252);
			navBarCross.StoreDefaultPaintStyleName = true;
			navBarCross.TabIndex = 5;
			navBarCross.Text = "dashboardNavBar2";
			splitContainerMain.BackColor = System.Drawing.Color.FromArgb(233, 233, 233);
			splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerMain.Location = new System.Drawing.Point(0, 0);
			splitContainerMain.Name = "splitContainerMain";
			splitContainerMain.Panel1.Controls.Add(navBarLeft);
			splitContainerMain.Panel2.Controls.Add(splitContainerSecond);
			splitContainerMain.Size = new System.Drawing.Size(1062, 557);
			splitContainerMain.SplitterDistance = 478;
			splitContainerMain.SplitterWidth = 7;
			splitContainerMain.TabIndex = 18;
			navBarLeft.ActiveGroup = null;
			navBarLeft.Appearance.Background.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
			navBarLeft.Appearance.Background.Options.UseBackColor = true;
			navBarLeft.BackColor = System.Drawing.Color.White;
			navBarLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			navBarLeft.DragDropFlags = (DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop);
			navBarLeft.Location = new System.Drawing.Point(0, 0);
			navBarLeft.Name = "navBarLeft";
			navBarLeft.OptionsNavPane.ExpandedWidth = 478;
			navBarLeft.Size = new System.Drawing.Size(478, 557);
			navBarLeft.StoreDefaultPaintStyleName = true;
			navBarLeft.TabIndex = 3;
			navBarLeft.Text = "dashboardNavBar1";
			splitContainerSecond.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerSecond.Location = new System.Drawing.Point(0, 0);
			splitContainerSecond.Name = "splitContainerSecond";
			splitContainerSecond.Panel1.Controls.Add(navBarRight);
			splitContainerSecond.Panel2.Controls.Add(navBarLast);
			splitContainerSecond.Panel2Collapsed = true;
			splitContainerSecond.Size = new System.Drawing.Size(577, 557);
			splitContainerSecond.SplitterDistance = 401;
			splitContainerSecond.SplitterWidth = 7;
			splitContainerSecond.TabIndex = 0;
			navBarRight.ActiveGroup = null;
			navBarRight.Appearance.Background.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
			navBarRight.Appearance.Background.Options.UseBackColor = true;
			navBarRight.BackColor = System.Drawing.Color.White;
			navBarRight.Dock = System.Windows.Forms.DockStyle.Fill;
			navBarRight.DragDropFlags = (DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop);
			navBarRight.Location = new System.Drawing.Point(0, 0);
			navBarRight.Name = "navBarRight";
			navBarRight.OptionsNavPane.ExpandedWidth = 577;
			navBarRight.Size = new System.Drawing.Size(577, 557);
			navBarRight.StoreDefaultPaintStyleName = true;
			navBarRight.TabIndex = 4;
			navBarRight.Text = "dashboardNavBar2";
			navBarLast.ActiveGroup = null;
			navBarLast.BackColor = System.Drawing.Color.White;
			navBarLast.Dock = System.Windows.Forms.DockStyle.Fill;
			navBarLast.DragDropFlags = (DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop);
			navBarLast.Location = new System.Drawing.Point(0, 0);
			navBarLast.Name = "navBarLast";
			navBarLast.OptionsNavPane.ExpandedWidth = 96;
			navBarLast.Size = new System.Drawing.Size(96, 100);
			navBarLast.StoreDefaultPaintStyleName = true;
			navBarLast.TabIndex = 5;
			navBarLast.Text = "dashboardNavBar2";
			AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(233, 233, 233);
			base.Controls.Add(splitContainerHorizontal);
			base.Controls.Add(panelTop);
			base.Name = "Dashboard";
			base.Size = new System.Drawing.Size(1062, 582);
			panelTop.ResumeLayout(false);
			panelTop.PerformLayout();
			splitContainerHorizontal.Panel1.ResumeLayout(false);
			splitContainerHorizontal.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerHorizontal).EndInit();
			splitContainerHorizontal.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)navBarCross).EndInit();
			splitContainerMain.Panel1.ResumeLayout(false);
			splitContainerMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
			splitContainerMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)navBarLeft).EndInit();
			splitContainerSecond.Panel1.ResumeLayout(false);
			splitContainerSecond.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerSecond).EndInit();
			splitContainerSecond.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)navBarRight).EndInit();
			((System.ComponentModel.ISupportInitialize)navBarLast).EndInit();
			ResumeLayout(false);
		}
	}
}
