using Infragistics.Win;
using Infragistics.Win.UltraWinTree;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class TreeGridList : UltraTree
	{
		private bool showMinusInRed = true;

		private IContainer components;

		private ToolStripMenuItem toolStripMenuItemOpen;

		private ToolStripMenuItem toolStripMenuItemNew;

		private ToolStripMenuItem toolStripMenuItemDelete;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem toolStripMenuItemAutoFit;

		private ToolStripMenuItem toolStripMenuItemColumnChooser;

		private PictureBox pictureBoxInactive;

		private PictureBox pictureBoxPhoto;

		public ContextMenuStrip contextMenuStrip1;

		public ContextMenuStrip DropDownMenu
		{
			get
			{
				return contextMenuStrip1;
			}
			set
			{
				contextMenuStrip1 = value;
			}
		}

		public bool ShowMinusInRed
		{
			get
			{
				return showMinusInRed;
			}
			set
			{
				showMinusInRed = value;
			}
		}

		public bool ShowNewMenu
		{
			get
			{
				return toolStripMenuItemNew.Visible;
			}
			set
			{
				toolStripMenuItemNew.Visible = value;
			}
		}

		public bool ShowDeleteMenu
		{
			get
			{
				return toolStripMenuItemDelete.Visible;
			}
			set
			{
				toolStripMenuItemDelete.Visible = value;
			}
		}

		public event EventHandler NewMenuClicked;

		public event EventHandler OpenMenuClicked;

		public event EventHandler DeleteMenuClicked;

		public TreeGridList()
		{
			InitializeComponent();
		}

		public void SetAlternateRowsColor()
		{
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			appearance.BackColor = Color.LightBlue;
			appearance2.BackColor = Color.LightYellow;
			object[] all = base.Nodes.All;
			for (int i = 0; i < all.Length; i++)
			{
				UltraTreeNode ultraTreeNode = (UltraTreeNode)all[i];
				if (ultraTreeNode.Index % 2 == 0)
				{
					ultraTreeNode.Override.NodeAppearance.BackColor = Color.Blue;
				}
				else
				{
					ultraTreeNode.Override.NodeAppearance.BackColor = Color.Yellow;
				}
			}
		}

		public TreeGridList(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			if (ContextMenuStrip == null)
			{
				ContextMenuStrip = contextMenuStrip1;
			}
			base.MouseDown += TreeGridList_MouseDown;
		}

		private void TreeGridList_MouseDown(object sender, MouseEventArgs e)
		{
		}

		public void SetInactiveColumn(string key)
		{
			ValueList valueList = new ValueList();
			ValueListItem item = new ValueListItem
			{
				Appearance = 
				{
					ImageBackground = pictureBoxInactive.Image
				},
				DataValue = "True",
				DisplayText = " "
			};
			valueList.ValueListItems.Add(item);
			item = new ValueListItem
			{
				DataValue = "False",
				DisplayText = " "
			};
			valueList.ValueListItems.Add(item);
		}

		public void SetPhotoColumn(string key)
		{
			ValueList valueList = new ValueList();
			ValueListItem item = new ValueListItem
			{
				Appearance = 
				{
					ImageBackground = pictureBoxPhoto.Image
				},
				DataValue = 1,
				DisplayText = " "
			};
			valueList.ValueListItems.Add(item);
			item = new ValueListItem
			{
				DataValue = 0,
				DisplayText = " "
			};
			valueList.ValueListItems.Add(item);
		}

		public void ApplyUIDesign()
		{
			if (base.ColumnSettings != null)
			{
				base.Indent = 12;
				base.HideSelection = false;
				base.ColumnSettings.AutoFitColumns = Infragistics.Win.UltraWinTree.AutoFitColumns.ResizeAllColumns;
				base.ViewStyle = ViewStyle.OutlookExpress;
				base.ColumnSettings.ColumnHeaderAppearance.BackColor = Color.FromArgb(177, 207, 248);
				base.ColumnSettings.ColumnHeaderAppearance.BackColor2 = Color.FromArgb(226, 236, 250);
				base.ColumnSettings.ColumnHeaderAppearance.BackGradientStyle = GradientStyle.Vertical;
				base.ColumnSettings.ColumnHeaderAppearance.FontData.Name = "Tahoma";
				base.ColumnSettings.ColumnHeaderAppearance.BorderColor = Color.Silver;
				base.ColumnSettings.ColumnHeaderAppearance.TextHAlign = HAlign.Center;
				base.Override.CellClickAction = CellClickAction.SelectNodeOnly;
				base.Override.ShowExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
				ApplyFormat();
			}
		}

		public void ApplyFormat()
		{
		}

		public void AutoFitColumns()
		{
			Refresh();
		}

		private void toolStripMenuItemAutoFit_Click(object sender, EventArgs e)
		{
			AutoFitColumns();
		}

		private void toolStripMenuItemColumnChooser_Click(object sender, EventArgs e)
		{
		}

		private void toolStripMenuItemNew_Click(object sender, EventArgs e)
		{
			if (this.NewMenuClicked != null)
			{
				this.NewMenuClicked(sender, e);
			}
		}

		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			if (this.OpenMenuClicked != null)
			{
				this.OpenMenuClicked(sender, e);
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteMenuClicked != null)
			{
				this.DeleteMenuClicked(sender, e);
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.UISupport.TreeGridList));
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemAutoFit = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemColumnChooser = new System.Windows.Forms.ToolStripMenuItem();
			pictureBoxInactive = new System.Windows.Forms.PictureBox();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxInactive).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripMenuItemOpen,
				toolStripMenuItemNew,
				toolStripMenuItemDelete,
				toolStripSeparator1,
				toolStripMenuItemAutoFit,
				toolStripMenuItemColumnChooser
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(165, 120);
			toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			toolStripMenuItemOpen.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemOpen.Text = "Open";
			toolStripMenuItemOpen.Click += new System.EventHandler(toolStripMenuItemOpen_Click);
			toolStripMenuItemNew.Name = "toolStripMenuItemNew";
			toolStripMenuItemNew.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemNew.Text = "New...";
			toolStripMenuItemNew.Click += new System.EventHandler(toolStripMenuItemNew_Click);
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemDelete.Text = "Delete";
			toolStripMenuItemDelete.Click += new System.EventHandler(toolStripMenuItemDelete_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
			toolStripMenuItemAutoFit.Name = "toolStripMenuItemAutoFit";
			toolStripMenuItemAutoFit.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemAutoFit.Text = "Auto Fit Columns";
			toolStripMenuItemAutoFit.Click += new System.EventHandler(toolStripMenuItemAutoFit_Click);
			toolStripMenuItemColumnChooser.Name = "toolStripMenuItemColumnChooser";
			toolStripMenuItemColumnChooser.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemColumnChooser.Text = "Column Chooser...";
			toolStripMenuItemColumnChooser.Click += new System.EventHandler(toolStripMenuItemColumnChooser_Click);
			pictureBoxInactive.Image = (System.Drawing.Image)resources.GetObject("pictureBoxInactive.Image");
			pictureBoxInactive.Location = new System.Drawing.Point(0, 0);
			pictureBoxInactive.Name = "pictureBoxInactive";
			pictureBoxInactive.Size = new System.Drawing.Size(100, 50);
			pictureBoxInactive.TabIndex = 0;
			pictureBoxInactive.TabStop = false;
			pictureBoxPhoto.Image = (System.Drawing.Image)resources.GetObject("pictureBoxPhoto.Image");
			pictureBoxPhoto.Location = new System.Drawing.Point(0, 0);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(100, 50);
			pictureBoxPhoto.TabIndex = 0;
			pictureBoxPhoto.TabStop = false;
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBoxInactive).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
