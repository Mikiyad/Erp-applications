using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomizeDashboardForm : Form
	{
		private List<string> usedGadgets = new List<string>();

		private List<string> availableGadgets = new List<string>();

		private string key = "";

		private IContainer components;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private Panel panelDashboard;

		private FlowLayoutPanel panelAvailable;

		private Label label7;

		private SplitContainer splitContainer1;

		private Label label1;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private Label label2;

		private Button buttonExport;

		private OpenFileDialog openFileDialog1;

		private SaveFileDialog saveFileDialog1;

		private Button button1;

		private FlowLayoutPanel panelCrossColumn;

		private Micromind.UISupport.Line line1;

		private FlowLayoutPanel panelLast;

		private FlowLayoutPanel panelRight;

		private Micromind.UISupport.Line line2;

		private FlowLayoutPanel panelLeft;

		private Micromind.UISupport.Line line3;

		private CheckBox checkBoxCrossColumn;

		private CheckBox checkBoxThreeColumn;

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

		public CustomizeDashboardForm()
		{
			InitializeComponent();
			if (key == "")
			{
				key = "DSHB" + base.Name;
			}
			panelLeft.DragEnter += panel_DragEnter;
			panelLeft.DragDrop += panel_DragDrop;
			panelRight.DragEnter += panel_DragEnter;
			panelRight.DragDrop += panel_DragDrop;
			panelLast.DragEnter += panel_DragEnter;
			panelLast.DragDrop += panel_DragDrop;
			panelCrossColumn.DragEnter += panel_DragEnter;
			panelCrossColumn.DragDrop += panel_DragDrop;
			panelAvailable.DragEnter += panel_DragEnter;
			panelAvailable.DragDrop += panel_DragDrop;
			panelLeft.AllowDrop = true;
			panelRight.AllowDrop = true;
			panelAvailable.AllowDrop = true;
			panelLast.AllowDrop = true;
			panelCrossColumn.AllowDrop = true;
			base.Load += CustomizeDashboardForm_Load;
		}

		private void CustomizeDashboardForm_Load(object sender, EventArgs e)
		{
			SetupLayout();
			LoadLayout();
		}

		private void panel_DragDrop(object sender, DragEventArgs e)
		{
			CustomizePanelItem customizePanelItem = (CustomizePanelItem)e.Data.GetData(typeof(CustomizePanelItem));
			FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)sender;
			FlowLayoutPanel flowLayoutPanel2 = (FlowLayoutPanel)customizePanelItem.Parent;
			if (flowLayoutPanel2 != flowLayoutPanel)
			{
				flowLayoutPanel.Controls.Add(customizePanelItem);
				Point pt = flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
				Control childAtPoint = flowLayoutPanel.GetChildAtPoint(pt);
				int childIndex = flowLayoutPanel.Controls.GetChildIndex(childAtPoint, throwException: false);
				flowLayoutPanel.Controls.SetChildIndex(customizePanelItem, childIndex);
				flowLayoutPanel.Invalidate();
				flowLayoutPanel2.Invalidate();
			}
			else
			{
				Point pt2 = flowLayoutPanel.PointToClient(new Point(e.X, e.Y));
				Control childAtPoint2 = flowLayoutPanel.GetChildAtPoint(pt2);
				int childIndex2 = flowLayoutPanel.Controls.GetChildIndex(childAtPoint2, throwException: false);
				flowLayoutPanel.Controls.SetChildIndex(customizePanelItem, childIndex2);
				flowLayoutPanel.Invalidate();
			}
		}

		private void panel_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		public bool LoadLayout()
		{
			try
			{
				byte[] binaryData = Factory.SettingSystem.GetBinaryData(Global.CurrentUser, key);
				FillLayout(binaryData);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool FillCustomLayout(byte[] layoutData)
		{
			try
			{
				return true;
			}
			catch
			{
				throw;
			}
		}

		private bool FillLayout(byte[] layoutData)
		{
			try
			{
				DashboardLayout dashboardLayout = new DashboardLayout();
				CustomGadgetData customGadgets = Factory.CustomGadgetSystem.GetCustomGadgets();
				DataTable customGadgetTable = customGadgets.CustomGadgetTable;
				if (layoutData.Length == 0)
				{
					return false;
				}
				object obj = GadgetsHelper.DeserializeFromStream(layoutData);
				if (obj == null)
				{
					return true;
				}
				dashboardLayout = (obj as DashboardLayout);
				checkBoxCrossColumn.Checked = dashboardLayout.IsCrossColumnRow;
				checkBoxThreeColumn.Checked = dashboardLayout.IsThreeColumn;
				panelLeft.Controls.Clear();
				panelRight.Controls.Clear();
				panelAvailable.Controls.Clear();
				panelLast.Controls.Clear();
				panelCrossColumn.Controls.Clear();
				foreach (GroupLayout groupLayout in dashboardLayout.LeftBarLayout.GroupLayoutList)
				{
					AddToPanel(groupLayout, customGadgetTable, panelLeft);
				}
				foreach (GroupLayout groupLayout2 in dashboardLayout.RightBarLayout.GroupLayoutList)
				{
					AddToPanel(groupLayout2, customGadgetTable, panelRight);
				}
				foreach (GroupLayout groupLayout3 in dashboardLayout.LastBarLayout.GroupLayoutList)
				{
					AddToPanel(groupLayout3, customGadgetTable, panelLast);
				}
				foreach (GroupLayout groupLayout4 in dashboardLayout.CrossBarLayout.GroupLayoutList)
				{
					AddToPanel(groupLayout4, customGadgetTable, panelCrossColumn);
				}
				foreach (KeyValuePair<GadgetTypes, GroupLayout> item in GadgetsHelper.GadgetsCollection)
				{
					if (!usedGadgets.Contains("SYS" + (int)item.Key))
					{
						GroupLayout value = item.Value;
						if (Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + value.GadgetType)).ToString()).Visible)
						{
							AddItem(panelAvailable, value);
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
					string text = row["CustomGadgetID"].ToString();
					string title = row["CustomGadgetName"].ToString();
					GadgetCategories category = GadgetCategories.General;
					ViewType chartType = ViewType.Bar;
					if (row["ChartType"] != DBNull.Value)
					{
						chartType = (ViewType)int.Parse(row["ChartType"].ToString());
					}
					if (row["CategoryID"] != DBNull.Value)
					{
						category = (GadgetCategories)int.Parse(row["CategoryID"].ToString());
					}
					GadgetStyles gadgetStyle = (GadgetStyles)int.Parse(row["GadgetStyle"].ToString());
					GadgetFilterOptions filterOption = GadgetFilterOptions.None;
					if (row["FilterOption"] != DBNull.Value)
					{
						filterOption = (GadgetFilterOptions)byte.Parse(row["GadgetStyle"].ToString());
					}
					if (!usedGadgets.Contains(text))
					{
						GroupLayout layout = new GroupLayout(GadgetTypes.Custom, text, category, title, 0, -1, expanded: false, gadgetStyle, chartType, filterOption, isCustom: true);
						if (Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, text).Visible)
						{
							AddItem(panelAvailable, layout);
							availableGadgets.Add(text);
						}
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private void AddToPanel(GroupLayout layout, DataTable customTable, FlowLayoutPanel panel)
		{
			if (!Security.IsAllowedSecurityRole((int)(5000 + layout.GadgetType)))
			{
				return;
			}
			AddItem(panel, layout);
			if (layout.GadgetType == GadgetTypes.Custom)
			{
				DataRow[] array = customTable.Select("CustomGadgetID = '" + layout.Code + "'");
				if (array.Length != 0)
				{
					layout.Title = array[0]["CustomGadgetName"].ToString();
					layout.GadgetStyle = (GadgetStyles)int.Parse(array[0]["GadgetStyle"].ToString());
					usedGadgets.Add(layout.Code);
				}
			}
			else
			{
				usedGadgets.Add("SYS" + (int)layout.GadgetType);
			}
		}

		private void AddItem(FlowLayoutPanel panel, GroupLayout layout)
		{
			CustomizePanelItem customizePanelItem = new CustomizePanelItem();
			if (layout.Title == null || layout.Title == "")
			{
				layout.Title = GadgetsHelper.GetGadgetTitle(layout.GadgetType);
			}
			customizePanelItem.Layout = layout;
			panel.Controls.Add(customizePanelItem);
		}

		private byte[] GetData()
		{
			DashboardLayout dashboardLayout = new DashboardLayout();
			dashboardLayout.IsCrossColumnRow = checkBoxCrossColumn.Checked;
			dashboardLayout.IsThreeColumn = checkBoxThreeColumn.Checked;
			foreach (CustomizePanelItem control in panelLeft.Controls)
			{
				if (control != null)
				{
					dashboardLayout.LeftBarLayout.GroupLayoutList.Add(control.Layout);
				}
			}
			foreach (CustomizePanelItem control2 in panelRight.Controls)
			{
				if (control2 != null)
				{
					dashboardLayout.RightBarLayout.GroupLayoutList.Add(control2.Layout);
				}
			}
			foreach (CustomizePanelItem control3 in panelLast.Controls)
			{
				if (control3 != null)
				{
					dashboardLayout.LastBarLayout.GroupLayoutList.Add(control3.Layout);
				}
			}
			foreach (CustomizePanelItem control4 in panelCrossColumn.Controls)
			{
				if (control4 != null)
				{
					dashboardLayout.CrossBarLayout.GroupLayoutList.Add(control4.Layout);
				}
			}
			return GadgetsHelper.SerializeToStream(dashboardLayout).ToArray();
		}

		public bool SaveLayout()
		{
			try
			{
				byte[] data = GetData();
				return (byte)(1 & (Factory.SettingSystem.SaveSettingStream(key, Global.CurrentUser, data) ? 1 : 0)) != 0;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveLayout())
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					ErrorHelper.ErrorMessage("Cannot save the layout.", "Please try again.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonExport_Click(object sender, EventArgs e)
		{
			try
			{
				byte[] data = GetData();
				saveFileDialog1.AddExtension = true;
				saveFileDialog1.Filter = "Axolon Dashboard |*.axd";
				saveFileDialog1.FileName = "Dashboard";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK && data != null)
				{
					new PublicFunctions().ByteArrayToFile(saveFileDialog1.FileName, data);
					ErrorHelper.InformationMessage("Custom report exported successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog1.Filter = "Axolon Dashboard|*.axd";
				openFileDialog1.FileName = "";
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					byte[] layoutData = File.ReadAllBytes(openFileDialog1.FileName);
					FillLayout(layoutData);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void checkBoxThreeColumn_CheckedChanged(object sender, EventArgs e)
		{
			panelLast.Visible = checkBoxThreeColumn.Checked;
			SetupLayout();
		}

		private void SetupLayout()
		{
			if (panelLast.Visible)
			{
				FlowLayoutPanel flowLayoutPanel = panelLeft;
				FlowLayoutPanel flowLayoutPanel2 = panelRight;
				int num2 = panelLast.Width = panelDashboard.Width / 3;
				int num5 = flowLayoutPanel.Width = (flowLayoutPanel2.Width = num2);
			}
			else
			{
				int num5 = panelLeft.Width = (panelRight.Width = panelDashboard.Width / 2);
			}
		}

		private void checkBoxCrossColumn_CheckedChanged(object sender, EventArgs e)
		{
			panelCrossColumn.Visible = checkBoxCrossColumn.Checked;
			SetupLayout();
		}

		private void CustomizeDashboardForm_Load_1(object sender, EventArgs e)
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
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			panelDashboard = new System.Windows.Forms.Panel();
			line1 = new Micromind.UISupport.Line();
			panelLast = new System.Windows.Forms.FlowLayoutPanel();
			panelRight = new System.Windows.Forms.FlowLayoutPanel();
			line2 = new Micromind.UISupport.Line();
			panelLeft = new System.Windows.Forms.FlowLayoutPanel();
			line3 = new Micromind.UISupport.Line();
			panelCrossColumn = new System.Windows.Forms.FlowLayoutPanel();
			panelAvailable = new System.Windows.Forms.FlowLayoutPanel();
			label7 = new System.Windows.Forms.Label();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			label1 = new System.Windows.Forms.Label();
			checkBoxCrossColumn = new System.Windows.Forms.CheckBox();
			checkBoxThreeColumn = new System.Windows.Forms.CheckBox();
			panelButtons = new System.Windows.Forms.Panel();
			button1 = new System.Windows.Forms.Button();
			buttonExport = new System.Windows.Forms.Button();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label2 = new System.Windows.Forms.Label();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			panelDashboard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			panelButtons.SuspendLayout();
			SuspendLayout();
			defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
			panelDashboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelDashboard.Controls.Add(line1);
			panelDashboard.Controls.Add(panelLast);
			panelDashboard.Controls.Add(panelRight);
			panelDashboard.Controls.Add(line2);
			panelDashboard.Controls.Add(panelLeft);
			panelDashboard.Controls.Add(line3);
			panelDashboard.Controls.Add(panelCrossColumn);
			panelDashboard.Location = new System.Drawing.Point(8, 39);
			panelDashboard.Name = "panelDashboard";
			panelDashboard.Size = new System.Drawing.Size(622, 399);
			panelDashboard.TabIndex = 4;
			panelDashboard.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Left;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(371, 76);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 321);
			line1.TabIndex = 3;
			line1.TabStop = false;
			panelLast.AllowDrop = true;
			panelLast.AutoScroll = true;
			panelLast.BackColor = System.Drawing.Color.White;
			panelLast.Dock = System.Windows.Forms.DockStyle.Right;
			panelLast.Location = new System.Drawing.Point(438, 76);
			panelLast.Name = "panelLast";
			panelLast.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
			panelLast.Size = new System.Drawing.Size(182, 321);
			panelLast.TabIndex = 2;
			panelLast.Visible = false;
			panelLast.Paint += new System.Windows.Forms.PaintEventHandler(flowLayoutPanel2_Paint);
			panelRight.AllowDrop = true;
			panelRight.AutoScroll = true;
			panelRight.BackColor = System.Drawing.Color.White;
			panelRight.Dock = System.Windows.Forms.DockStyle.Left;
			panelRight.Location = new System.Drawing.Point(181, 76);
			panelRight.Name = "panelRight";
			panelRight.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
			panelRight.Size = new System.Drawing.Size(190, 321);
			panelRight.TabIndex = 0;
			line2.BackColor = System.Drawing.Color.White;
			line2.Dock = System.Windows.Forms.DockStyle.Left;
			line2.DrawWidth = 1;
			line2.IsVertical = true;
			line2.LineBackColor = System.Drawing.Color.Black;
			line2.Location = new System.Drawing.Point(180, 76);
			line2.Name = "line2";
			line2.Size = new System.Drawing.Size(1, 321);
			line2.TabIndex = 4;
			line2.TabStop = false;
			panelLeft.AllowDrop = true;
			panelLeft.AutoScroll = true;
			panelLeft.BackColor = System.Drawing.Color.White;
			panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			panelLeft.Location = new System.Drawing.Point(0, 76);
			panelLeft.Name = "panelLeft";
			panelLeft.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
			panelLeft.Size = new System.Drawing.Size(180, 321);
			panelLeft.TabIndex = 0;
			line3.BackColor = System.Drawing.Color.White;
			line3.Dock = System.Windows.Forms.DockStyle.Top;
			line3.DrawWidth = 1;
			line3.IsVertical = false;
			line3.LineBackColor = System.Drawing.Color.Black;
			line3.Location = new System.Drawing.Point(0, 75);
			line3.Name = "line3";
			line3.Size = new System.Drawing.Size(620, 1);
			line3.TabIndex = 5;
			line3.TabStop = false;
			panelCrossColumn.AllowDrop = true;
			panelCrossColumn.AutoScroll = true;
			panelCrossColumn.BackColor = System.Drawing.Color.White;
			panelCrossColumn.Dock = System.Windows.Forms.DockStyle.Top;
			panelCrossColumn.Location = new System.Drawing.Point(0, 0);
			panelCrossColumn.Name = "panelCrossColumn";
			panelCrossColumn.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
			panelCrossColumn.Size = new System.Drawing.Size(620, 75);
			panelCrossColumn.TabIndex = 1;
			panelCrossColumn.Visible = false;
			panelAvailable.AllowDrop = true;
			panelAvailable.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panelAvailable.AutoScroll = true;
			panelAvailable.BackColor = System.Drawing.Color.AliceBlue;
			panelAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelAvailable.Location = new System.Drawing.Point(3, 32);
			panelAvailable.Name = "panelAvailable";
			panelAvailable.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
			panelAvailable.Size = new System.Drawing.Size(209, 404);
			panelAvailable.TabIndex = 5;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Arial", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.ForeColor = System.Drawing.Color.FromArgb(26, 88, 134);
			label7.Location = new System.Drawing.Point(3, 6);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(118, 25);
			label7.TabIndex = 7;
			label7.Text = "Dashboard";
			splitContainer1.IsSplitterFixed = true;
			splitContainer1.Location = new System.Drawing.Point(4, 43);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(label1);
			splitContainer1.Panel1.Controls.Add(panelAvailable);
			splitContainer1.Panel2.Controls.Add(checkBoxCrossColumn);
			splitContainer1.Panel2.Controls.Add(checkBoxThreeColumn);
			splitContainer1.Panel2.Controls.Add(label7);
			splitContainer1.Panel2.Controls.Add(panelDashboard);
			splitContainer1.Size = new System.Drawing.Size(864, 441);
			splitContainer1.SplitterDistance = 220;
			splitContainer1.TabIndex = 8;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Arial", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(26, 88, 134);
			label1.Location = new System.Drawing.Point(12, 12);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(138, 16);
			label1.TabIndex = 8;
			label1.Text = "Available Gadgets:";
			checkBoxCrossColumn.AutoSize = true;
			checkBoxCrossColumn.Location = new System.Drawing.Point(317, 14);
			checkBoxCrossColumn.Name = "checkBoxCrossColumn";
			checkBoxCrossColumn.Size = new System.Drawing.Size(108, 17);
			checkBoxCrossColumn.TabIndex = 9;
			checkBoxCrossColumn.Text = "Cross colums row";
			checkBoxCrossColumn.UseVisualStyleBackColor = true;
			checkBoxCrossColumn.Visible = false;
			checkBoxCrossColumn.CheckedChanged += new System.EventHandler(checkBoxCrossColumn_CheckedChanged);
			checkBoxThreeColumn.AutoSize = true;
			checkBoxThreeColumn.Location = new System.Drawing.Point(185, 13);
			checkBoxThreeColumn.Name = "checkBoxThreeColumn";
			checkBoxThreeColumn.Size = new System.Drawing.Size(92, 17);
			checkBoxThreeColumn.TabIndex = 8;
			checkBoxThreeColumn.Text = "Three Column";
			checkBoxThreeColumn.UseVisualStyleBackColor = true;
			checkBoxThreeColumn.CheckedChanged += new System.EventHandler(checkBoxThreeColumn_CheckedChanged);
			panelButtons.Controls.Add(button1);
			panelButtons.Controls.Add(buttonExport);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 497);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(872, 40);
			panelButtons.TabIndex = 9;
			button1.Location = new System.Drawing.Point(100, 9);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 3;
			button1.Text = "Import";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			buttonExport.Location = new System.Drawing.Point(19, 8);
			buttonExport.Name = "buttonExport";
			buttonExport.Size = new System.Drawing.Size(75, 23);
			buttonExport.TabIndex = 2;
			buttonExport.Text = "Export";
			buttonExport.UseVisualStyleBackColor = true;
			buttonExport.Click += new System.EventHandler(buttonExport_Click);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(660, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(872, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(762, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(19, 12);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(390, 13);
			label2.TabIndex = 10;
			label2.Text = "Drag and drop the gadgets you want to display in or remove from your dashboard.";
			openFileDialog1.FileName = "openFileDialog1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(872, 537);
			base.Controls.Add(label2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(splitContainer1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomizeDashboardForm";
			Text = "Customize Dashboard";
			base.Load += new System.EventHandler(CustomizeDashboardForm_Load_1);
			panelDashboard.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
