using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class RecordInfoDialog : DialogBoxBaseForm
	{
		private bool hasAccessRight = true;

		private XPButton buttonOK;

		private Line line1;

		private MMLabel baLabel1;

		private MMLabel labelDateCreated;

		private MMLabel labelCreatedBy;

		private MMLabel baLabel3;

		private MMLabel baLabel4;

		private MMLabel baLabel6;

		private MMLabel baLabel7;

		private MMLabel labelHeaderTitle;

		private MMLabel lablelUpdatedBy;

		private MMLabel labelDateUpdated;

		private Container components;

		private string tableName = "";

		private string fieldName = "";

		private object fieldID;

		private string title = "";

		private bool isFirstActivated;

		public string TableName
		{
			set
			{
				tableName = value;
			}
		}

		public string FieldName
		{
			set
			{
				fieldName = value;
			}
		}

		public object FieldID
		{
			set
			{
				fieldID = value;
			}
		}

		public string Title
		{
			set
			{
				title = value;
			}
		}

		public RecordInfoDialog()
		{
			InitializeComponent();
		}

		private void Init()
		{
			RefreshData();
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.UISupport.RecordInfoDialog));
			buttonOK = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			baLabel1 = new Micromind.UISupport.MMLabel();
			labelDateCreated = new Micromind.UISupport.MMLabel();
			labelCreatedBy = new Micromind.UISupport.MMLabel();
			baLabel3 = new Micromind.UISupport.MMLabel();
			lablelUpdatedBy = new Micromind.UISupport.MMLabel();
			baLabel4 = new Micromind.UISupport.MMLabel();
			labelDateUpdated = new Micromind.UISupport.MMLabel();
			baLabel6 = new Micromind.UISupport.MMLabel();
			baLabel7 = new Micromind.UISupport.MMLabel();
			labelHeaderTitle = new Micromind.UISupport.MMLabel();
			SuspendLayout();
			buttonOK.AccessibleDescription = resourceManager.GetString("buttonOK.AccessibleDescription");
			buttonOK.AccessibleName = resourceManager.GetString("buttonOK.AccessibleName");
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("buttonOK.Anchor");
			buttonOK.BackColor = System.Drawing.Color.Silver;
			buttonOK.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("buttonOK.BackgroundImage");
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("buttonOK.Dock");
			buttonOK.Enabled = (bool)resourceManager.GetObject("buttonOK.Enabled");
			buttonOK.FlatStyle = (System.Windows.Forms.FlatStyle)resourceManager.GetObject("buttonOK.FlatStyle");
			buttonOK.Font = (System.Drawing.Font)resourceManager.GetObject("buttonOK.Font");
			buttonOK.Image = (System.Drawing.Image)resourceManager.GetObject("buttonOK.Image");
			buttonOK.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("buttonOK.ImageAlign");
			buttonOK.ImageIndex = (int)resourceManager.GetObject("buttonOK.ImageIndex");
			buttonOK.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("buttonOK.ImeMode");
			buttonOK.Location = (System.Drawing.Point)resourceManager.GetObject("buttonOK.Location");
			buttonOK.Name = "buttonOK";
			buttonOK.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("buttonOK.RightToLeft");
			buttonOK.Size = (System.Drawing.Size)resourceManager.GetObject("buttonOK.Size");
			buttonOK.TabIndex = (int)resourceManager.GetObject("buttonOK.TabIndex");
			buttonOK.Text = resourceManager.GetString("buttonOK.Text");
			buttonOK.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("buttonOK.TextAlign");
			buttonOK.Visible = (bool)resourceManager.GetObject("buttonOK.Visible");
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.AccessibleDescription = resourceManager.GetString("line1.AccessibleDescription");
			line1.AccessibleName = resourceManager.GetString("line1.AccessibleName");
			line1.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("line1.Anchor");
			line1.AutoScroll = (bool)resourceManager.GetObject("line1.AutoScroll");
			line1.AutoScrollMargin = (System.Drawing.Size)resourceManager.GetObject("line1.AutoScrollMargin");
			line1.AutoScrollMinSize = (System.Drawing.Size)resourceManager.GetObject("line1.AutoScrollMinSize");
			line1.BackColor = System.Drawing.Color.White;
			line1.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("line1.BackgroundImage");
			line1.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("line1.Dock");
			line1.DrawWidth = 1;
			line1.Enabled = (bool)resourceManager.GetObject("line1.Enabled");
			line1.Font = (System.Drawing.Font)resourceManager.GetObject("line1.Font");
			line1.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("line1.ImeMode");
			line1.LineBackColor = System.Drawing.Color.Silver;
			line1.Location = (System.Drawing.Point)resourceManager.GetObject("line1.Location");
			line1.Name = "line1";
			line1.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("line1.RightToLeft");
			line1.Size = (System.Drawing.Size)resourceManager.GetObject("line1.Size");
			line1.TabIndex = (int)resourceManager.GetObject("line1.TabIndex");
			line1.TabStop = false;
			line1.Visible = (bool)resourceManager.GetObject("line1.Visible");
			baLabel1.AccessibleDescription = resourceManager.GetString("baLabel1.AccessibleDescription");
			baLabel1.AccessibleName = resourceManager.GetString("baLabel1.AccessibleName");
			baLabel1.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("baLabel1.Anchor");
			baLabel1.AutoSize = (bool)resourceManager.GetObject("baLabel1.AutoSize");
			baLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel1.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("baLabel1.Dock");
			baLabel1.Enabled = (bool)resourceManager.GetObject("baLabel1.Enabled");
			baLabel1.Font = (System.Drawing.Font)resourceManager.GetObject("baLabel1.Font");
			baLabel1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			baLabel1.Image = (System.Drawing.Image)resourceManager.GetObject("baLabel1.Image");
			baLabel1.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel1.ImageAlign");
			baLabel1.ImageIndex = (int)resourceManager.GetObject("baLabel1.ImageIndex");
			baLabel1.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("baLabel1.ImeMode");
			baLabel1.IsFieldHeader = true;
			baLabel1.Location = (System.Drawing.Point)resourceManager.GetObject("baLabel1.Location");
			baLabel1.Name = "baLabel1";
			baLabel1.PenWidth = 1f;
			baLabel1.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("baLabel1.RightToLeft");
			baLabel1.ShowBorder = false;
			baLabel1.Size = (System.Drawing.Size)resourceManager.GetObject("baLabel1.Size");
			baLabel1.TabIndex = (int)resourceManager.GetObject("baLabel1.TabIndex");
			baLabel1.Text = resourceManager.GetString("baLabel1.Text");
			baLabel1.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel1.TextAlign");
			baLabel1.Visible = (bool)resourceManager.GetObject("baLabel1.Visible");
			labelDateCreated.AccessibleDescription = resourceManager.GetString("labelDateCreated.AccessibleDescription");
			labelDateCreated.AccessibleName = resourceManager.GetString("labelDateCreated.AccessibleName");
			labelDateCreated.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("labelDateCreated.Anchor");
			labelDateCreated.AutoSize = (bool)resourceManager.GetObject("labelDateCreated.AutoSize");
			labelDateCreated.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			labelDateCreated.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("labelDateCreated.Dock");
			labelDateCreated.Enabled = (bool)resourceManager.GetObject("labelDateCreated.Enabled");
			labelDateCreated.Font = (System.Drawing.Font)resourceManager.GetObject("labelDateCreated.Font");
			labelDateCreated.ForeColor = System.Drawing.Color.Black;
			labelDateCreated.Image = (System.Drawing.Image)resourceManager.GetObject("labelDateCreated.Image");
			labelDateCreated.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelDateCreated.ImageAlign");
			labelDateCreated.ImageIndex = (int)resourceManager.GetObject("labelDateCreated.ImageIndex");
			labelDateCreated.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("labelDateCreated.ImeMode");
			labelDateCreated.IsFieldHeader = false;
			labelDateCreated.Location = (System.Drawing.Point)resourceManager.GetObject("labelDateCreated.Location");
			labelDateCreated.Name = "labelDateCreated";
			labelDateCreated.PenWidth = 1f;
			labelDateCreated.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("labelDateCreated.RightToLeft");
			labelDateCreated.ShowBorder = true;
			labelDateCreated.Size = (System.Drawing.Size)resourceManager.GetObject("labelDateCreated.Size");
			labelDateCreated.TabIndex = (int)resourceManager.GetObject("labelDateCreated.TabIndex");
			labelDateCreated.Text = resourceManager.GetString("labelDateCreated.Text");
			labelDateCreated.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelDateCreated.TextAlign");
			labelDateCreated.Visible = (bool)resourceManager.GetObject("labelDateCreated.Visible");
			labelCreatedBy.AccessibleDescription = resourceManager.GetString("labelCreatedBy.AccessibleDescription");
			labelCreatedBy.AccessibleName = resourceManager.GetString("labelCreatedBy.AccessibleName");
			labelCreatedBy.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("labelCreatedBy.Anchor");
			labelCreatedBy.AutoSize = (bool)resourceManager.GetObject("labelCreatedBy.AutoSize");
			labelCreatedBy.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			labelCreatedBy.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("labelCreatedBy.Dock");
			labelCreatedBy.Enabled = (bool)resourceManager.GetObject("labelCreatedBy.Enabled");
			labelCreatedBy.Font = (System.Drawing.Font)resourceManager.GetObject("labelCreatedBy.Font");
			labelCreatedBy.ForeColor = System.Drawing.Color.Black;
			labelCreatedBy.Image = (System.Drawing.Image)resourceManager.GetObject("labelCreatedBy.Image");
			labelCreatedBy.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelCreatedBy.ImageAlign");
			labelCreatedBy.ImageIndex = (int)resourceManager.GetObject("labelCreatedBy.ImageIndex");
			labelCreatedBy.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("labelCreatedBy.ImeMode");
			labelCreatedBy.IsFieldHeader = false;
			labelCreatedBy.Location = (System.Drawing.Point)resourceManager.GetObject("labelCreatedBy.Location");
			labelCreatedBy.Name = "labelCreatedBy";
			labelCreatedBy.PenWidth = 1f;
			labelCreatedBy.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("labelCreatedBy.RightToLeft");
			labelCreatedBy.ShowBorder = true;
			labelCreatedBy.Size = (System.Drawing.Size)resourceManager.GetObject("labelCreatedBy.Size");
			labelCreatedBy.TabIndex = (int)resourceManager.GetObject("labelCreatedBy.TabIndex");
			labelCreatedBy.Text = resourceManager.GetString("labelCreatedBy.Text");
			labelCreatedBy.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelCreatedBy.TextAlign");
			labelCreatedBy.Visible = (bool)resourceManager.GetObject("labelCreatedBy.Visible");
			baLabel3.AccessibleDescription = resourceManager.GetString("baLabel3.AccessibleDescription");
			baLabel3.AccessibleName = resourceManager.GetString("baLabel3.AccessibleName");
			baLabel3.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("baLabel3.Anchor");
			baLabel3.AutoSize = (bool)resourceManager.GetObject("baLabel3.AutoSize");
			baLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel3.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("baLabel3.Dock");
			baLabel3.Enabled = (bool)resourceManager.GetObject("baLabel3.Enabled");
			baLabel3.Font = (System.Drawing.Font)resourceManager.GetObject("baLabel3.Font");
			baLabel3.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			baLabel3.Image = (System.Drawing.Image)resourceManager.GetObject("baLabel3.Image");
			baLabel3.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel3.ImageAlign");
			baLabel3.ImageIndex = (int)resourceManager.GetObject("baLabel3.ImageIndex");
			baLabel3.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("baLabel3.ImeMode");
			baLabel3.IsFieldHeader = true;
			baLabel3.Location = (System.Drawing.Point)resourceManager.GetObject("baLabel3.Location");
			baLabel3.Name = "baLabel3";
			baLabel3.PenWidth = 1f;
			baLabel3.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("baLabel3.RightToLeft");
			baLabel3.ShowBorder = false;
			baLabel3.Size = (System.Drawing.Size)resourceManager.GetObject("baLabel3.Size");
			baLabel3.TabIndex = (int)resourceManager.GetObject("baLabel3.TabIndex");
			baLabel3.Text = resourceManager.GetString("baLabel3.Text");
			baLabel3.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel3.TextAlign");
			baLabel3.Visible = (bool)resourceManager.GetObject("baLabel3.Visible");
			lablelUpdatedBy.AccessibleDescription = resourceManager.GetString("lablelUpdatedBy.AccessibleDescription");
			lablelUpdatedBy.AccessibleName = resourceManager.GetString("lablelUpdatedBy.AccessibleName");
			lablelUpdatedBy.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("lablelUpdatedBy.Anchor");
			lablelUpdatedBy.AutoSize = (bool)resourceManager.GetObject("lablelUpdatedBy.AutoSize");
			lablelUpdatedBy.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lablelUpdatedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			lablelUpdatedBy.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("lablelUpdatedBy.Dock");
			lablelUpdatedBy.Enabled = (bool)resourceManager.GetObject("lablelUpdatedBy.Enabled");
			lablelUpdatedBy.Font = (System.Drawing.Font)resourceManager.GetObject("lablelUpdatedBy.Font");
			lablelUpdatedBy.ForeColor = System.Drawing.Color.Black;
			lablelUpdatedBy.Image = (System.Drawing.Image)resourceManager.GetObject("lablelUpdatedBy.Image");
			lablelUpdatedBy.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("lablelUpdatedBy.ImageAlign");
			lablelUpdatedBy.ImageIndex = (int)resourceManager.GetObject("lablelUpdatedBy.ImageIndex");
			lablelUpdatedBy.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("lablelUpdatedBy.ImeMode");
			lablelUpdatedBy.IsFieldHeader = false;
			lablelUpdatedBy.Location = (System.Drawing.Point)resourceManager.GetObject("lablelUpdatedBy.Location");
			lablelUpdatedBy.Name = "lablelUpdatedBy";
			lablelUpdatedBy.PenWidth = 1f;
			lablelUpdatedBy.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("lablelUpdatedBy.RightToLeft");
			lablelUpdatedBy.ShowBorder = true;
			lablelUpdatedBy.Size = (System.Drawing.Size)resourceManager.GetObject("lablelUpdatedBy.Size");
			lablelUpdatedBy.TabIndex = (int)resourceManager.GetObject("lablelUpdatedBy.TabIndex");
			lablelUpdatedBy.Text = resourceManager.GetString("lablelUpdatedBy.Text");
			lablelUpdatedBy.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("lablelUpdatedBy.TextAlign");
			lablelUpdatedBy.Visible = (bool)resourceManager.GetObject("lablelUpdatedBy.Visible");
			baLabel4.AccessibleDescription = resourceManager.GetString("baLabel4.AccessibleDescription");
			baLabel4.AccessibleName = resourceManager.GetString("baLabel4.AccessibleName");
			baLabel4.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("baLabel4.Anchor");
			baLabel4.AutoSize = (bool)resourceManager.GetObject("baLabel4.AutoSize");
			baLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel4.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("baLabel4.Dock");
			baLabel4.Enabled = (bool)resourceManager.GetObject("baLabel4.Enabled");
			baLabel4.Font = (System.Drawing.Font)resourceManager.GetObject("baLabel4.Font");
			baLabel4.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			baLabel4.Image = (System.Drawing.Image)resourceManager.GetObject("baLabel4.Image");
			baLabel4.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel4.ImageAlign");
			baLabel4.ImageIndex = (int)resourceManager.GetObject("baLabel4.ImageIndex");
			baLabel4.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("baLabel4.ImeMode");
			baLabel4.IsFieldHeader = true;
			baLabel4.Location = (System.Drawing.Point)resourceManager.GetObject("baLabel4.Location");
			baLabel4.Name = "baLabel4";
			baLabel4.PenWidth = 1f;
			baLabel4.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("baLabel4.RightToLeft");
			baLabel4.ShowBorder = false;
			baLabel4.Size = (System.Drawing.Size)resourceManager.GetObject("baLabel4.Size");
			baLabel4.TabIndex = (int)resourceManager.GetObject("baLabel4.TabIndex");
			baLabel4.Text = resourceManager.GetString("baLabel4.Text");
			baLabel4.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel4.TextAlign");
			baLabel4.Visible = (bool)resourceManager.GetObject("baLabel4.Visible");
			labelDateUpdated.AccessibleDescription = resourceManager.GetString("labelDateUpdated.AccessibleDescription");
			labelDateUpdated.AccessibleName = resourceManager.GetString("labelDateUpdated.AccessibleName");
			labelDateUpdated.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("labelDateUpdated.Anchor");
			labelDateUpdated.AutoSize = (bool)resourceManager.GetObject("labelDateUpdated.AutoSize");
			labelDateUpdated.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDateUpdated.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			labelDateUpdated.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("labelDateUpdated.Dock");
			labelDateUpdated.Enabled = (bool)resourceManager.GetObject("labelDateUpdated.Enabled");
			labelDateUpdated.Font = (System.Drawing.Font)resourceManager.GetObject("labelDateUpdated.Font");
			labelDateUpdated.ForeColor = System.Drawing.Color.Black;
			labelDateUpdated.Image = (System.Drawing.Image)resourceManager.GetObject("labelDateUpdated.Image");
			labelDateUpdated.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelDateUpdated.ImageAlign");
			labelDateUpdated.ImageIndex = (int)resourceManager.GetObject("labelDateUpdated.ImageIndex");
			labelDateUpdated.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("labelDateUpdated.ImeMode");
			labelDateUpdated.IsFieldHeader = false;
			labelDateUpdated.Location = (System.Drawing.Point)resourceManager.GetObject("labelDateUpdated.Location");
			labelDateUpdated.Name = "labelDateUpdated";
			labelDateUpdated.PenWidth = 1f;
			labelDateUpdated.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("labelDateUpdated.RightToLeft");
			labelDateUpdated.ShowBorder = true;
			labelDateUpdated.Size = (System.Drawing.Size)resourceManager.GetObject("labelDateUpdated.Size");
			labelDateUpdated.TabIndex = (int)resourceManager.GetObject("labelDateUpdated.TabIndex");
			labelDateUpdated.Text = resourceManager.GetString("labelDateUpdated.Text");
			labelDateUpdated.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelDateUpdated.TextAlign");
			labelDateUpdated.Visible = (bool)resourceManager.GetObject("labelDateUpdated.Visible");
			baLabel6.AccessibleDescription = resourceManager.GetString("baLabel6.AccessibleDescription");
			baLabel6.AccessibleName = resourceManager.GetString("baLabel6.AccessibleName");
			baLabel6.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("baLabel6.Anchor");
			baLabel6.AutoSize = (bool)resourceManager.GetObject("baLabel6.AutoSize");
			baLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel6.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("baLabel6.Dock");
			baLabel6.Enabled = (bool)resourceManager.GetObject("baLabel6.Enabled");
			baLabel6.Font = (System.Drawing.Font)resourceManager.GetObject("baLabel6.Font");
			baLabel6.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			baLabel6.Image = (System.Drawing.Image)resourceManager.GetObject("baLabel6.Image");
			baLabel6.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel6.ImageAlign");
			baLabel6.ImageIndex = (int)resourceManager.GetObject("baLabel6.ImageIndex");
			baLabel6.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("baLabel6.ImeMode");
			baLabel6.IsFieldHeader = true;
			baLabel6.Location = (System.Drawing.Point)resourceManager.GetObject("baLabel6.Location");
			baLabel6.Name = "baLabel6";
			baLabel6.PenWidth = 1f;
			baLabel6.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("baLabel6.RightToLeft");
			baLabel6.ShowBorder = false;
			baLabel6.Size = (System.Drawing.Size)resourceManager.GetObject("baLabel6.Size");
			baLabel6.TabIndex = (int)resourceManager.GetObject("baLabel6.TabIndex");
			baLabel6.Text = resourceManager.GetString("baLabel6.Text");
			baLabel6.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel6.TextAlign");
			baLabel6.Visible = (bool)resourceManager.GetObject("baLabel6.Visible");
			baLabel7.AccessibleDescription = resourceManager.GetString("baLabel7.AccessibleDescription");
			baLabel7.AccessibleName = resourceManager.GetString("baLabel7.AccessibleName");
			baLabel7.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("baLabel7.Anchor");
			baLabel7.AutoSize = (bool)resourceManager.GetObject("baLabel7.AutoSize");
			baLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			baLabel7.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("baLabel7.Dock");
			baLabel7.Enabled = (bool)resourceManager.GetObject("baLabel7.Enabled");
			baLabel7.Font = (System.Drawing.Font)resourceManager.GetObject("baLabel7.Font");
			baLabel7.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			baLabel7.Image = (System.Drawing.Image)resourceManager.GetObject("baLabel7.Image");
			baLabel7.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel7.ImageAlign");
			baLabel7.ImageIndex = (int)resourceManager.GetObject("baLabel7.ImageIndex");
			baLabel7.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("baLabel7.ImeMode");
			baLabel7.IsFieldHeader = true;
			baLabel7.Location = (System.Drawing.Point)resourceManager.GetObject("baLabel7.Location");
			baLabel7.Name = "baLabel7";
			baLabel7.PenWidth = 1f;
			baLabel7.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("baLabel7.RightToLeft");
			baLabel7.ShowBorder = false;
			baLabel7.Size = (System.Drawing.Size)resourceManager.GetObject("baLabel7.Size");
			baLabel7.TabIndex = (int)resourceManager.GetObject("baLabel7.TabIndex");
			baLabel7.Text = resourceManager.GetString("baLabel7.Text");
			baLabel7.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("baLabel7.TextAlign");
			baLabel7.Visible = (bool)resourceManager.GetObject("baLabel7.Visible");
			labelHeaderTitle.AccessibleDescription = resourceManager.GetString("labelHeaderTitle.AccessibleDescription");
			labelHeaderTitle.AccessibleName = resourceManager.GetString("labelHeaderTitle.AccessibleName");
			labelHeaderTitle.Anchor = (System.Windows.Forms.AnchorStyles)resourceManager.GetObject("labelHeaderTitle.Anchor");
			labelHeaderTitle.AutoSize = (bool)resourceManager.GetObject("labelHeaderTitle.AutoSize");
			labelHeaderTitle.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelHeaderTitle.Dock = (System.Windows.Forms.DockStyle)resourceManager.GetObject("labelHeaderTitle.Dock");
			labelHeaderTitle.Enabled = (bool)resourceManager.GetObject("labelHeaderTitle.Enabled");
			labelHeaderTitle.Font = (System.Drawing.Font)resourceManager.GetObject("labelHeaderTitle.Font");
			labelHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelHeaderTitle.Image = (System.Drawing.Image)resourceManager.GetObject("labelHeaderTitle.Image");
			labelHeaderTitle.ImageAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelHeaderTitle.ImageAlign");
			labelHeaderTitle.ImageIndex = (int)resourceManager.GetObject("labelHeaderTitle.ImageIndex");
			labelHeaderTitle.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("labelHeaderTitle.ImeMode");
			labelHeaderTitle.IsFieldHeader = true;
			labelHeaderTitle.Location = (System.Drawing.Point)resourceManager.GetObject("labelHeaderTitle.Location");
			labelHeaderTitle.Name = "labelHeaderTitle";
			labelHeaderTitle.PenWidth = 1f;
			labelHeaderTitle.RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("labelHeaderTitle.RightToLeft");
			labelHeaderTitle.ShowBorder = false;
			labelHeaderTitle.Size = (System.Drawing.Size)resourceManager.GetObject("labelHeaderTitle.Size");
			labelHeaderTitle.TabIndex = (int)resourceManager.GetObject("labelHeaderTitle.TabIndex");
			labelHeaderTitle.Text = resourceManager.GetString("labelHeaderTitle.Text");
			labelHeaderTitle.TextAlign = (System.Drawing.ContentAlignment)resourceManager.GetObject("labelHeaderTitle.TextAlign");
			labelHeaderTitle.Visible = (bool)resourceManager.GetObject("labelHeaderTitle.Visible");
			base.AcceptButton = buttonOK;
			base.AccessibleDescription = resourceManager.GetString("$this.AccessibleDescription");
			base.AccessibleName = resourceManager.GetString("$this.AccessibleName");
			AutoScaleBaseSize = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScaleBaseSize");
			AutoScroll = (bool)resourceManager.GetObject("$this.AutoScroll");
			base.AutoScrollMargin = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScrollMargin");
			base.AutoScrollMinSize = (System.Drawing.Size)resourceManager.GetObject("$this.AutoScrollMinSize");
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("$this.BackgroundImage");
			base.CancelButton = buttonOK;
			base.ClientSize = (System.Drawing.Size)resourceManager.GetObject("$this.ClientSize");
			base.Controls.Add(labelHeaderTitle);
			base.Controls.Add(baLabel7);
			base.Controls.Add(lablelUpdatedBy);
			base.Controls.Add(baLabel4);
			base.Controls.Add(labelDateUpdated);
			base.Controls.Add(baLabel6);
			base.Controls.Add(labelCreatedBy);
			base.Controls.Add(baLabel3);
			base.Controls.Add(labelDateCreated);
			base.Controls.Add(baLabel1);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Enabled = (bool)resourceManager.GetObject("$this.Enabled");
			Font = (System.Drawing.Font)resourceManager.GetObject("$this.Font");
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.ImeMode = (System.Windows.Forms.ImeMode)resourceManager.GetObject("$this.ImeMode");
			base.Location = (System.Drawing.Point)resourceManager.GetObject("$this.Location");
			base.MaximizeBox = false;
			MaximumSize = (System.Drawing.Size)resourceManager.GetObject("$this.MaximumSize");
			base.MinimizeBox = false;
			MinimumSize = (System.Drawing.Size)resourceManager.GetObject("$this.MinimumSize");
			base.Name = "RecordInfoDialog";
			RightToLeft = (System.Windows.Forms.RightToLeft)resourceManager.GetObject("$this.RightToLeft");
			base.StartPosition = (System.Windows.Forms.FormStartPosition)resourceManager.GetObject("$this.StartPosition");
			Text = resourceManager.GetString("$this.Text");
			base.Load += new System.EventHandler(NewShipperForm_Load);
			base.Activated += new System.EventHandler(RecordInfoDialog_Activated);
			ResumeLayout(false);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void NewShipperForm_Load(object sender, EventArgs e)
		{
			InitDialog();
			Init();
		}

		public void OnActivated()
		{
		}

		private bool SetSecurity()
		{
			return true;
		}

		private bool ReadInfo()
		{
			if (!hasAccessRight)
			{
				return false;
			}
			try
			{
				PublicFunctions.StartWaiting(this);
				DatabaseData tableUserStats = Factory.DatabaseSystem.GetTableUserStats(tableName, fieldName, fieldID);
				if (tableUserStats != null && tableUserStats.DatabaseTable.Rows.Count > 0)
				{
					DataRow dataRow = tableUserStats.DatabaseTable.Rows[0];
					if (dataRow["DateCreated"] != DBNull.Value)
					{
						DateTime dateTime = DateTime.Parse(dataRow["DateCreated"].ToString());
						labelDateCreated.Text = dateTime.ToLongDateString() + " " + dateTime.ToShortTimeString();
					}
					if (dataRow["DateTimeStamp"] != DBNull.Value)
					{
						DateTime dateTime = DateTime.Parse(dataRow["DateTimeStamp"].ToString());
						labelDateUpdated.Text = dateTime.ToLongDateString() + " " + dateTime.ToShortTimeString();
					}
					labelCreatedBy.Text = dataRow["CreatedByLoginName"].ToString();
					lablelUpdatedBy.Text = dataRow["UpdatedByLoginName"].ToString();
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			return true;
		}

		public void RefreshData()
		{
			Application.DoEvents();
			Refresh();
			if (!SetSecurity())
			{
				hasAccessRight = false;
			}
		}

		private void RecordInfoDialog_Activated(object sender, EventArgs e)
		{
			if (!isFirstActivated)
			{
				isFirstActivated = true;
				labelHeaderTitle.Text = title;
				ReadInfo();
			}
		}
	}
}
