using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.WinExplorer;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.FlatDashboard
{
	public class AddDashboardGadgetForm : Form
	{
		private ArrayList usedGadgets = new ArrayList();

		private List<string> availableGadgets = new List<string>();

		private IContainer components;

		private Panel panelButtons;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonClose;

		private GridControl gridControl1;

		private WinExplorerView winExplorerView1;

		private ImageCollection imageCollection1;

		private PanelControl panelControl1;

		private Label label1;

		private System.Windows.Forms.ComboBox comboBoxViewOption;

		private Label label2;

		public ArrayList UsedGadgets
		{
			get
			{
				return usedGadgets;
			}
			set
			{
				usedGadgets = value;
			}
		}

		public event EventHandler GadgetSelected;

		public AddDashboardGadgetForm()
		{
			InitializeComponent();
			base.Load += AddDashboardGadgetForm_Load;
			winExplorerView1.ItemDoubleClick += WinExplorerView1_ItemDoubleClick;
			base.FormClosing += AddDashboardGadgetForm_FormClosing;
		}

		private void WinExplorerView1_ItemDoubleClick(object sender, WinExplorerViewItemDoubleClickEventArgs e)
		{
			OnGadgetSelected(sender, e);
			winExplorerView1.DeleteRow(winExplorerView1.FocusedRowHandle);
		}

		private void AddDashboardGadgetForm_Load(object sender, EventArgs e)
		{
			LoadLayout();
			foreach (GadgetViewOption value in Enum.GetValues(typeof(GadgetViewOption)))
			{
				comboBoxViewOption.Items.Add(value);
			}
			GadgetViewOption selectedIndex = GadgetViewOption.ExtraLarge;
			object userSetting = Factory.SettingSystem.GetUserSetting(Global.CurrentUser, base.Name + "GadgetViewOption", 1);
			if (userSetting != null && userSetting.ToString() != "")
			{
				selectedIndex = ((int.Parse(userSetting.ToString()) <= 0) ? GadgetViewOption.ExtraLarge : ((GadgetViewOption)Enum.ToObject(typeof(GadgetViewOption), int.Parse(userSetting.ToString()))));
			}
			comboBoxViewOption.SelectedIndex = (int)selectedIndex;
		}

		public bool LoadLayout()
		{
			try
			{
				LoadGadgets();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadGadgets()
		{
			try
			{
				CustomGadgetData customGadgets = Factory.CustomGadgetSystem.GetCustomGadgets();
				_ = customGadgets.CustomGadgetTable;
				DataSet dataSet = new DataSet();
				DataTable dataTable = dataSet.Tables.Add("Gadgets");
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("Layout", typeof(byte[]));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Category");
				dataTable.Columns.Add("Image", typeof(Image));
				dataTable.Columns.Add("ImageIndex", typeof(int));
				dataTable.Columns.Add("Title");
				string text = "";
				foreach (KeyValuePair<GadgetTypes, GroupLayout> item in GadgetsHelper.GadgetsCollection)
				{
					if (!usedGadgets.Contains("SYS" + (int)item.Key))
					{
						GroupLayout value = item.Value;
						value.Code = ((int)(5000 + value.GadgetType)).ToString();
						if (Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + value.GadgetType)).ToString()).Visible && !usedGadgets.Contains(value.Code))
						{
							AddItem(dataTable, value);
							if (value.GadgetType == GadgetTypes.Custom)
							{
								usedGadgets.Add(value.Code);
							}
							else
							{
								usedGadgets.Add("SYS" + (int)item.Key);
							}
						}
					}
				}
				foreach (DataRow row in customGadgets.CustomGadgetTable.Rows)
				{
					text = row["CustomGadgetID"].ToString();
					string title = row["CustomGadgetName"].ToString();
					ViewType chartType = ViewType.Bar;
					if (row["ChartType"] != DBNull.Value)
					{
						chartType = (ViewType)int.Parse(row["ChartType"].ToString());
					}
					GadgetStyles gadgetStyle = (GadgetStyles)int.Parse(row["GadgetStyle"].ToString());
					GadgetFilterOptions filterOption = GadgetFilterOptions.None;
					if (row["FilterOption"] != DBNull.Value)
					{
						filterOption = (GadgetFilterOptions)byte.Parse(row["GadgetStyle"].ToString());
					}
					GadgetCategories category = GadgetCategories.None;
					if (!row["CategoryID"].IsDBNullOrEmpty())
					{
						category = (GadgetCategories)byte.Parse(row["CategoryID"].ToString());
					}
					if (!usedGadgets.Contains(text))
					{
						GroupLayout groupLayout = new GroupLayout(GadgetTypes.Custom, text, category, title, 0, -1, expanded: false, gadgetStyle, chartType, filterOption, isCustom: true);
						if (Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, text).Visible && !usedGadgets.Contains(groupLayout.Code))
						{
							groupLayout.IsCustom = true;
							AddItem(dataTable, groupLayout);
							availableGadgets.Add(text);
						}
					}
				}
				gridControl1.DataSource = dataSet.Tables[0];
				winExplorerView1.ColumnSet.TextColumn = winExplorerView1.Columns["Title"];
				winExplorerView1.ColumnSet.DescriptionColumn = winExplorerView1.Columns["Description"];
				winExplorerView1.ColumnSet.ExtraLargeImageIndexColumn = winExplorerView1.Columns["ImageIndex"];
				winExplorerView1.ColumnSet.ExtraLargeImageColumn = winExplorerView1.Columns["Image"];
				winExplorerView1.ColumnSet.GroupColumn = winExplorerView1.Columns["Category"];
				winExplorerView1.OptionsView.AnimationType = GridAnimationType.NeverAnimate;
				return true;
			}
			catch
			{
				throw;
			}
		}

		private void AddItem(DataTable gTable, GroupLayout layout)
		{
			GadgetStyles gadgetStyle = layout.GadgetStyle;
			DataRow dataRow = gTable.NewRow();
			dataRow["Code"] = layout.Code;
			dataRow["Description"] = layout.Description;
			dataRow["Category"] = layout.Category.ToString();
			dataRow["Title"] = layout.Title;
			dataRow["Layout"] = Global.SerializeToStream(layout).ToArray();
			dataRow["Image"] = PublicFunctions.GetGadgetThumbnailImage(layout.Code);
			switch (gadgetStyle)
			{
			case GadgetStyles.Chart:
				dataRow["ImageIndex"] = 0;
				break;
			case GadgetStyles.Gauge:
				dataRow["ImageIndex"] = 3;
				break;
			case GadgetStyles.Link:
				dataRow["ImageIndex"] = 2;
				break;
			case GadgetStyles.Number:
				dataRow["ImageIndex"] = 4;
				break;
			default:
				dataRow["ImageIndex"] = 1;
				break;
			}
			gTable.Rows.Add(dataRow);
		}

		public void OnGadgetSelected(object sender, EventArgs e)
		{
			if (this.GadgetSelected != null)
			{
				this.GadgetSelected(sender, e);
			}
		}

		private void AddDashboardGadgetForm_Load_1(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void textBoxFind_TextChanged(object sender, EventArgs e)
		{
		}

		private void AddDashboardGadgetForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				UserPreferences.SaveCurrentUserSetting(base.Name + "GadgetViewOption", comboBoxViewOption.SelectedIndex);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxViewOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetGridStyle((GadgetViewOption)comboBoxViewOption.SelectedIndex);
		}

		private void SetGridStyle(GadgetViewOption option)
		{
			switch (option)
			{
			case GadgetViewOption.Small:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Small;
				break;
			case GadgetViewOption.Medium:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Medium;
				break;
			case GadgetViewOption.Large:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Large;
				break;
			case GadgetViewOption.ExtraLarge:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.ExtraLarge;
				break;
			case GadgetViewOption.List:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.List;
				break;
			case GadgetViewOption.Tiles:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Tiles;
				break;
			case GadgetViewOption.Content:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Content;
				break;
			case GadgetViewOption.Default:
				winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Default;
				break;
			}
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
			DevExpress.XtraGrid.GridLevelNode gridLevelNode = new DevExpress.XtraGrid.GridLevelNode();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.FlatDashboard.AddDashboardGadgetForm));
			panelButtons = new System.Windows.Forms.Panel();
			comboBoxViewOption = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			linePanelDown = new Micromind.UISupport.Line();
			buttonClose = new Micromind.UISupport.XPButton();
			gridControl1 = new DevExpress.XtraGrid.GridControl();
			winExplorerView1 = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
			imageCollection1 = new DevExpress.Utils.ImageCollection();
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			label1 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)winExplorerView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(comboBoxViewOption);
			panelButtons.Controls.Add(label2);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 627);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(994, 40);
			panelButtons.TabIndex = 10;
			comboBoxViewOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxViewOption.FormattingEnabled = true;
			comboBoxViewOption.Location = new System.Drawing.Point(95, 7);
			comboBoxViewOption.Name = "comboBoxViewOption";
			comboBoxViewOption.Size = new System.Drawing.Size(152, 21);
			comboBoxViewOption.TabIndex = 16;
			comboBoxViewOption.SelectedIndexChanged += new System.EventHandler(comboBoxViewOption_SelectedIndexChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 8);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(67, 13);
			label2.TabIndex = 15;
			label2.Text = "View Option:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(994, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(884, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 1;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(buttonCancel_Click);
			gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			gridLevelNode.RelationName = "Level1";
			gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[1]
			{
				gridLevelNode
			});
			gridControl1.Location = new System.Drawing.Point(0, 36);
			gridControl1.MainView = winExplorerView1;
			gridControl1.Name = "gridControl1";
			gridControl1.Size = new System.Drawing.Size(994, 591);
			gridControl1.TabIndex = 12;
			gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1]
			{
				winExplorerView1
			});
			winExplorerView1.ExtraLargeImages = imageCollection1;
			winExplorerView1.GridControl = gridControl1;
			winExplorerView1.Name = "winExplorerView1";
			winExplorerView1.OptionsFind.AlwaysVisible = true;
			winExplorerView1.OptionsFind.FindDelay = 100;
			winExplorerView1.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
			winExplorerView1.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.ExtraLarge;
			winExplorerView1.OptionsViewStyles.ExtraLarge.ImageSize = new System.Drawing.Size(256, 200);
			imageCollection1.ImageSize = new System.Drawing.Size(256, 256);
			imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			imageCollection1.Images.SetKeyName(0, "Chart");
			imageCollection1.Images.SetKeyName(1, "List");
			imageCollection1.Images.SetKeyName(2, "Link");
			imageCollection1.Images.SetKeyName(3, "Gauge");
			imageCollection1.Images.SetKeyName(4, "Odometer");
			panelControl1.Controls.Add(label1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			panelControl1.Location = new System.Drawing.Point(0, 0);
			panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
			panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(994, 36);
			panelControl1.TabIndex = 13;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 12);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(304, 13);
			label1.TabIndex = 1;
			label1.Text = "Double click gadgets you would like to add to your dashboard:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(994, 667);
			base.Controls.Add(gridControl1);
			base.Controls.Add(panelControl1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddDashboardGadgetForm";
			Text = "Add Dashboard Gadget";
			base.Load += new System.EventHandler(AddDashboardGadgetForm_Load_1);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)winExplorerView1).EndInit();
			((System.ComponentModel.ISupportInitialize)imageCollection1).EndInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(false);
			panelControl1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
