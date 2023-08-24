using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Micromind.DataControls.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.FlatDashboard
{
	public class GadgetContainer : PanelControl
	{
		private bool isEnlarged;

		private Label labelTitle = new Label();

		private Panel panelHeader = new Panel();

		private bool showRefreshButton = true;

		private Panel panelMain = new Panel();

		private IContainer components;

		private UltraButton buttonRefresh;

		private UltraButton buttonEnlarge;

		private ImageList imageList1;

		public FlatDashboard ParentDashboard
		{
			get;
			set;
		}

		public bool ShowRefreshButton
		{
			get
			{
				return showRefreshButton;
			}
			set
			{
				showRefreshButton = value;
				buttonRefresh.Visible = value;
			}
		}

		public bool IsEnlarged
		{
			get
			{
				return isEnlarged;
			}
			set
			{
				if (value)
				{
					buttonEnlarge.Appearance.Image = imageList1.Images["Shrink"];
					buttonEnlarge.HotTrackAppearance.Image = imageList1.Images["ShrinkBlue"];
				}
				else
				{
					buttonEnlarge.Appearance.Image = imageList1.Images["Expand"];
					buttonEnlarge.HotTrackAppearance.Image = imageList1.Images["ExpandBlue"];
				}
				isEnlarged = value;
			}
		}

		public string Title
		{
			get
			{
				return labelTitle.Text;
			}
			set
			{
				labelTitle.Text = value;
			}
		}

		public bool IsCustom
		{
			get;
			set;
		}

		public Panel ControlContainer => panelMain;

		public GroupLayout GadgetLayout
		{
			get;
			set;
		}

		public event EventHandler RefreshData;

		public GadgetContainer()
		{
			InitializeComponent();
			LookAndFeel.Style = LookAndFeelStyle.Flat;
			LookAndFeel.UseDefaultLookAndFeel = false;
			BackColor = Color.White;
			base.Controls.Add(panelHeader);
			panelHeader.Dock = DockStyle.Top;
			panelHeader.Height = 18;
			labelTitle.Text = Title;
			labelTitle.Location = new Point(0, 0);
			labelTitle.AutoSize = false;
			labelTitle.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			labelTitle.Height = 18;
			labelTitle.Width = base.Width - 60;
			labelTitle.TextAlign = ContentAlignment.TopLeft;
			labelTitle.Visible = true;
			labelTitle.ForeColor = Color.DimGray;
			labelTitle.Font = new Font("Tahoma", 10f);
			panelHeader.Controls.Add(labelTitle);
			panelHeader.Controls.Add(buttonRefresh);
			buttonRefresh.Size = new Size(18, 18);
			buttonRefresh.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			buttonRefresh.Top = 1;
			buttonRefresh.Left = panelHeader.Width - buttonRefresh.Width;
			buttonRefresh.Click += ButtonRefresh_Click;
			panelHeader.Controls.Add(buttonEnlarge);
			buttonEnlarge.Size = new Size(18, 18);
			buttonEnlarge.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			buttonEnlarge.Top = 1;
			buttonEnlarge.Left = panelHeader.Width - buttonRefresh.Width - 2 - buttonEnlarge.Width;
			buttonEnlarge.Click += ButtonEnlarge_Click;
			panelMain.Dock = DockStyle.Fill;
			base.Controls.Add(panelMain);
			panelMain.BringToFront();
		}

		public void AdjustForSmallGadgets()
		{
			panelHeader.Height = 18;
			labelTitle.Height = 18;
			labelTitle.Font = new Font("Tahoma", 9f);
			buttonRefresh.Size = new Size(18, 18);
		}

		private void ButtonRefresh_Click(object sender, EventArgs e)
		{
			if (this.RefreshData != null)
			{
				this.RefreshData(this, e);
			}
		}

		private void ButtonEnlarge_Click(object sender, EventArgs e)
		{
			FlatDashboard parentDashboard = ParentDashboard;
			if (IsEnlarged)
			{
				foreach (LayoutItem item in parentDashboard.LayoutControl.Root.Items)
				{
					item.Visibility = LayoutVisibility.Always;
				}
				IsEnlarged = false;
				parentDashboard.IsEnlarged = false;
			}
			else
			{
				foreach (LayoutItem item2 in parentDashboard.LayoutControl.Root.Items)
				{
					if (item2.Name != base.Name)
					{
						item2.Visibility = LayoutVisibility.Never;
					}
				}
				IsEnlarged = true;
				parentDashboard.IsEnlarged = true;
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.FlatDashboard.GadgetContainer));
			buttonRefresh = new Infragistics.Win.Misc.UltraButton();
			buttonEnlarge = new Infragistics.Win.Misc.UltraButton();
			imageList1 = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			SuspendLayout();
			buttonRefresh.AcceptsFocus = false;
			appearance.Image = Micromind.DataControls.Properties.Resources.refresh16x16dis1;
			appearance.ImageHAlign = Infragistics.Win.HAlign.Center;
			buttonRefresh.Appearance = appearance;
			buttonRefresh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
			buttonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
			appearance2.BorderColor = System.Drawing.Color.White;
			appearance2.Image = Micromind.DataControls.Properties.Resources.refresh16x16;
			appearance2.ImageHAlign = Infragistics.Win.HAlign.Center;
			buttonRefresh.HotTrackAppearance = appearance2;
			buttonRefresh.ImageSize = new System.Drawing.Size(14, 14);
			buttonRefresh.Location = new System.Drawing.Point(390, 0);
			buttonRefresh.Name = "buttonRefresh";
			appearance3.BorderColor = System.Drawing.Color.White;
			buttonRefresh.PressedAppearance = appearance3;
			buttonRefresh.Size = new System.Drawing.Size(25, 24);
			buttonRefresh.TabIndex = 0;
			buttonRefresh.UseAppStyling = false;
			buttonRefresh.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonRefresh.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
			buttonRefresh.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			buttonEnlarge.AcceptsFocus = false;
			appearance4.Image = Micromind.DataControls.Properties.Resources.expandgray18x18;
			appearance4.ImageHAlign = Infragistics.Win.HAlign.Center;
			buttonEnlarge.Appearance = appearance4;
			buttonEnlarge.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
			buttonEnlarge.Dock = System.Windows.Forms.DockStyle.Right;
			appearance5.BorderColor = System.Drawing.Color.White;
			appearance5.Image = Micromind.DataControls.Properties.Resources.expandblue18x18;
			appearance5.ImageHAlign = Infragistics.Win.HAlign.Center;
			buttonEnlarge.HotTrackAppearance = appearance5;
			buttonEnlarge.ImageSize = new System.Drawing.Size(14, 14);
			buttonEnlarge.Location = new System.Drawing.Point(390, 0);
			buttonEnlarge.Name = "buttonEnlarge";
			appearance6.BorderColor = System.Drawing.Color.White;
			buttonEnlarge.PressedAppearance = appearance6;
			buttonEnlarge.Size = new System.Drawing.Size(25, 24);
			buttonEnlarge.TabIndex = 0;
			buttonEnlarge.UseAppStyling = false;
			buttonEnlarge.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonEnlarge.UseHotTracking = Infragistics.Win.DefaultableBoolean.True;
			buttonEnlarge.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "expand");
			imageList1.Images.SetKeyName(1, "expandblue");
			imageList1.Images.SetKeyName(2, "shrink");
			imageList1.Images.SetKeyName(3, "shrinkblue");
			Appearance.BackColor = System.Drawing.Color.White;
			Appearance.Options.UseBackColor = true;
			MinimumSize = new System.Drawing.Size(50, 50);
			base.Size = new System.Drawing.Size(418, 279);
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
