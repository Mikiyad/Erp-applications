using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class UDFEntryControl : UserControl
	{
		private string tableName = "";

		private bool isSetupDone;

		private IContainer components;

		private DataEntryGrid dataEntryGrid1;

		private Panel panelDown;

		private Micromind.UISupport.Line line1;

		private UltraFormattedLinkLabel labelCustomize;

		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

		public DataEntryGrid DataGrid => dataEntryGrid1;

		public CellsCollection Fields
		{
			get
			{
				if (dataEntryGrid1.Rows.Count == 0)
				{
					return null;
				}
				dataEntryGrid1.PerformAction(UltraGridAction.ExitEditMode);
				return dataEntryGrid1.Rows[0].Cells;
			}
		}

		public event EventHandler SetupUDF;

		public UDFEntryControl()
		{
			InitializeComponent();
		}

		public void LoadSetup(string tableName)
		{
			try
			{
				this.tableName = tableName;
				dataEntryGrid1.SetupUI();
				dataEntryGrid1.DisplayLayout.Bands[0].CardView = true;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.ShowCaption = false;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.AutoFit = true;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.Style = CardStyle.StandardLabels;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.CardScrollbars = CardScrollbars.None;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = HAlign.Left;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.MaxCardAreaRows = 1;
				dataEntryGrid1.DisplayLayout.Bands[0].CardSettings.MaxCardAreaCols = 1;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CellPadding = 2;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CellSpacing = 1;
				dataEntryGrid1.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
				dataEntryGrid1.DisplayLayout.Appearance.BorderColor = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CardAreaAppearance.BackColor = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.CardAreaAppearance.BackColor2 = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.Select;
				Color color2 = dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor = (dataEntryGrid1.DisplayLayout.Bands[0].Override.HeaderAppearance.BackColor2 = Color.WhiteSmoke);
				dataEntryGrid1.AllowAddNew = false;
				DataSet uDFEntryFields = Factory.UDFSystem.GetUDFEntryFields(tableName);
				uDFEntryFields.Tables["UDF"].Rows.Clear();
				DataRow row = uDFEntryFields.Tables["UDF"].NewRow();
				uDFEntryFields.Tables["UDF"].Rows.Add(row);
				dataEntryGrid1.DataSource = uDFEntryFields.Tables[0];
				uDFEntryFields.Tables[0].AcceptChanges();
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["EntityID"].Hidden = true;
				foreach (DataRow row2 in uDFEntryFields.Tables["UDF_Setup"].Rows)
				{
					string key = row2["Column_Name"].ToString();
					if (dataEntryGrid1.DisplayLayout.Bands[0].Columns.Exists(key))
					{
						UltraGridColumn ultraGridColumn = dataEntryGrid1.DisplayLayout.Bands[0].Columns[key];
						if (ultraGridColumn.DataType == typeof(bool))
						{
							color2 = (ultraGridColumn.CellAppearance.BorderColor = (ultraGridColumn.CellAppearance.BackColor = Color.WhiteSmoke));
							ultraGridColumn.CellAppearance.BackColorDisabled = Color.WhiteSmoke;
							ultraGridColumn.CellClickAction = CellClickAction.Edit;
						}
						dataEntryGrid1.DisplayLayout.Bands[0].Columns[key].Header.Caption = row2["DisplayName"].ToString();
						if (row2["MaxLength"] != DBNull.Value)
						{
							dataEntryGrid1.DisplayLayout.Bands[0].Columns[key].MaxLength = int.Parse(row2["MaxLength"].ToString());
						}
					}
				}
				isSetupDone = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public DataSet GetData()
		{
			DataSet dataSet = new DataSet();
			DataTable table = dataEntryGrid1.DataSource as DataTable;
			dataSet.Merge(table);
			return dataSet;
		}

		public void ClearData()
		{
			if (dataEntryGrid1.DataSource != null)
			{
				dataEntryGrid1.PerformAction(UltraGridAction.ExitEditMode);
				foreach (UltraGridColumn column in dataEntryGrid1.DisplayLayout.Bands[0].Columns)
				{
					dataEntryGrid1.Rows[0].Cells[column].Value = DBNull.Value;
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataEntryGrid1.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ColumnName");
				dataTable.Columns.Add("DisplayName");
				dataTable.Columns.Add("DataType");
				dataEntryGrid1.DataSource = dataTable;
			}
			catch (Exception e)
			{
				dataEntryGrid1.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (this.SetupUDF != null)
			{
				this.SetupUDF(this, e);
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
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			dataEntryGrid1 = new Micromind.DataControls.DataEntryGrid();
			panelDown = new System.Windows.Forms.Panel();
			line1 = new Micromind.UISupport.Line();
			labelCustomize = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).BeginInit();
			panelDown.SuspendLayout();
			SuspendLayout();
			dataEntryGrid1.AllowAddNew = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGrid1.DisplayLayout.Appearance = appearance;
			dataEntryGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataEntryGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataEntryGrid1.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataEntryGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGrid1.DisplayLayout.Override.CellAppearance = appearance8;
			dataEntryGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataEntryGrid1.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataEntryGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataEntryGrid1.DisplayLayout.Override.RowAppearance = appearance11;
			dataEntryGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataEntryGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			dataEntryGrid1.Location = new System.Drawing.Point(0, 26);
			dataEntryGrid1.Name = "dataEntryGrid1";
			dataEntryGrid1.ShowDeleteMenu = true;
			dataEntryGrid1.ShowInsertMenu = true;
			dataEntryGrid1.Size = new System.Drawing.Size(626, 307);
			dataEntryGrid1.TabIndex = 0;
			dataEntryGrid1.Text = "dataEntryGrid1";
			panelDown.Controls.Add(labelCustomize);
			panelDown.Controls.Add(line1);
			panelDown.Dock = System.Windows.Forms.DockStyle.Top;
			panelDown.Location = new System.Drawing.Point(0, 0);
			panelDown.Name = "panelDown";
			panelDown.Size = new System.Drawing.Size(626, 26);
			panelDown.TabIndex = 1;
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Bottom;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 25);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(626, 1);
			line1.TabIndex = 0;
			line1.TabStop = false;
			labelCustomize.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			appearance13.ForeColor = System.Drawing.Color.FromArgb(102, 165, 221);
			appearance13.TextHAlignAsString = "Right";
			labelCustomize.Appearance = appearance13;
			appearance14.ForeColor = System.Drawing.Color.FromArgb(26, 88, 134);
			appearance14.TextHAlignAsString = "Right";
			labelCustomize.LinkAppearance = appearance14;
			labelCustomize.Location = new System.Drawing.Point(486, 9);
			labelCustomize.Name = "labelCustomize";
			labelCustomize.Size = new System.Drawing.Size(130, 13);
			labelCustomize.TabIndex = 62;
			labelCustomize.TabStop = true;
			labelCustomize.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCustomize.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCustomize.Value = "Setup Fields";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			labelCustomize.VisitedLinkAppearance = appearance15;
			labelCustomize.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelCustomize_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(dataEntryGrid1);
			base.Controls.Add(panelDown);
			base.Name = "UDFEntryControl";
			base.Size = new System.Drawing.Size(626, 333);
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).EndInit();
			panelDown.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
