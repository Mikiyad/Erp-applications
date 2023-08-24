using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Customers
{
	public class BarCodeReport : Form, IForm
	{
		private SysDocTypes doctype;

		private string tableName = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private Label label5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraGroupBox ultraGroupBox2;

		private ProductSelector productSelector;

		private XPButton buttonGRN;

		private Label label2;

		private SysDocTypeComboBox comboBoxSysDocType;

		private Label label4;

		private GroupBox groupBoxQtyCheck;

		private CheckBox checkBoxExactQty;

		private CheckBox checkBoxIncludeItemQty;

		private NumberTextBox textBoxQty;

		private Label label3;

		private GroupBox groupBox1;

		private Panel panel1;

		private RadioButton radioButtonImport;

		private RadioButton radioButtonLocal;

		private DateControl dateControl1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7009;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public string SysDocID
		{
			get
			{
				return comboBoxSysDoc.SelectedID;
			}
			set
			{
				comboBoxSysDoc.SelectedID = value;
			}
		}

		public string VoucherID
		{
			get
			{
				return textBoxVoucherNumber.Text;
			}
			set
			{
				textBoxVoucherNumber.Text = value;
			}
		}

		public BarCodeReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				radioButtonLocal.Checked = true;
				textBoxQty.Text = 1.ToString();
				comboBoxSysDoc.SelectedIndex = 0;
				SetSecurity();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			decimal num = 1m;
			decimal result = 1m;
			decimal num2 = 1m;
			num = int.Parse(textBoxQty.Text);
			DataSet data = Factory.PurchaseReceiptSystem.GetBarCodeReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, SysDocID, VoucherID, tableName, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			DataTable dataTable = new DataTable("BarCodeTable");
			dataTable = data.Tables[0].Clone();
			if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row2 in data.Tables[0].Rows)
				{
					decimal.TryParse(row2["Qty"].ToString(), out result);
					num2 = num * result;
					if (!data.Tables[0].Columns.Contains("PrintQty"))
					{
						data.Tables[0].Columns.Add("PrintQty");
					}
					row2["PrintQty"] = num2;
				}
				foreach (DataRow row3 in data.Tables[0].Rows)
				{
					num2 = 1m;
					string str = row3["ProductID"].ToString();
					decimal d = decimal.Parse(row3["Qty"].ToString());
					num2 = ((!checkBoxIncludeItemQty.Checked) ? num : (num * d));
					DataRow[] array = data.Tables[0].Select("ProductID='" + str + "'");
					int num3 = (int)num2;
					for (int i = 0; i < num3; i = checked(i + 1))
					{
						DataRow[] array2 = array;
						foreach (DataRow row in array2)
						{
							dataTable.ImportRow(row);
						}
					}
				}
			}
			if (data.Tables.Count > 1)
			{
				data.Tables[1].Columns.Add("SysDocTypeName");
				foreach (DataRow row4 in data.Tables[1].Rows)
				{
					if (row4["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row4["SysDocType"].ToString());
						row4["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
			}
			if (data.Tables.Contains(dataTable.ToString()))
			{
				data.Tables.Remove(dataTable.ToString());
			}
			data.Tables.Add(dataTable);
			label4.Text = data.Tables[0].Rows.Count + " labels are ready to print";
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Barcode Report");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Barcode Report.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonGRN_Click(object sender, EventArgs e)
		{
			if (comboBoxSysDoc.SelectedID != "")
			{
				SysDocID = comboBoxSysDoc.SelectedID;
			}
			VoucherID = textBoxVoucherNumber.Text;
			int objectID = (int)Enum.Parse(value: Factory.SystemDocumentSystem.GetBarCodeSystemDocumentType(SysDocID).ToString(), enumType: typeof(SysDocTypes));
			DoubleString doubleString = Factory.ApprovalSystem.GetTableName(1, objectID);
			tableName = doubleString.FirstString;
			try
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
				selectDocumentDialog.Text = "Select Value";
				selectDocumentDialog.IsMultiSelect = true;
				selectDocumentDialog.ValidateSelection += form_ValidateSelection;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						SysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
						if (selectedRow.Cells.Exists("Doc Number"))
						{
							VoucherID = selectedRow.Cells["Doc Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("VoucherID"))
						{
							VoucherID = selectedRow.Cells["VoucherID"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Number"))
						{
							VoucherID = selectedRow.Cells["Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Batch Number"))
						{
							VoucherID = selectedRow.Cells["Batch Number"].Text.ToString();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			SelectDocumentDialog obj = sender as SelectDocumentDialog;
			DataSet dataSet = new DataSet();
			dataSet = (obj.DataSource = Factory.PurchaseReceiptSystem.GetVoucherNumbersFromTransaction(tableName, SysDocID, "", e.From, e.To));
		}

		private void FillSysDoc()
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.SystemDocumentSystem.GetItemBarCodeSystemDoc();
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				comboBoxSysDoc.DataSource = dataSet;
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			int num = 0;
			foreach (UltraGridRow row in selectDocumentDialog.Grid.Rows)
			{
				if (row.Cells["C"].Value != null && row.Cells["C"].Value.ToString() != "" && bool.Parse(row.Cells["C"].Value.ToString()) && bool.Parse(row.Cells["C"].Value.ToString()))
				{
					num = checked(num + 1);
					if (num > 1)
					{
						ErrorHelper.WarningMessage("Cannot select more than one GRN!");
						selectDocumentDialog.CanClose = false;
						break;
					}
				}
			}
		}

		private void radioButtonLocal_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonLocal.Checked)
			{
				comboBoxSysDoc.FilterByType(SysDocTypes.GoodsReceivedNote);
				comboBoxSysDoc.SelectedIndex = 0;
				textBoxVoucherNumber.Clear();
			}
		}

		private void radioButtonImport_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonImport.Checked)
			{
				comboBoxSysDoc.FilterByType(SysDocTypes.ImportGoodsReceivedNote);
				comboBoxSysDoc.SelectedIndex = 0;
				textBoxVoucherNumber.Clear();
			}
		}

		private void comboBoxSysDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			doctype = (SysDocTypes)byte.Parse(comboBoxSysDocType.SelectedID);
			comboBoxSysDoc.FilterByType(doctype);
		}

		private void textBoxQty_TextChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxIncludeItemQty_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxIncludeItemQty.Checked)
			{
				checkBoxExactQty.Checked = false;
			}
		}

		private void checkBoxExactQty_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxExactQty.Checked)
			{
				checkBoxIncludeItemQty.Checked = false;
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
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.BarCodeReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			label2 = new System.Windows.Forms.Label();
			comboBoxSysDocType = new Micromind.DataControls.SysDocTypeComboBox();
			buttonGRN = new Micromind.UISupport.XPButton();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			label4 = new System.Windows.Forms.Label();
			groupBoxQtyCheck = new System.Windows.Forms.GroupBox();
			textBoxQty = new Micromind.UISupport.NumberTextBox();
			label3 = new System.Windows.Forms.Label();
			checkBoxExactQty = new System.Windows.Forms.CheckBox();
			checkBoxIncludeItemQty = new System.Windows.Forms.CheckBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			panel1 = new System.Windows.Forms.Panel();
			radioButtonImport = new System.Windows.Forms.RadioButton();
			radioButtonLocal = new System.Windows.Forms.RadioButton();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDocType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			groupBoxQtyCheck.SuspendLayout();
			groupBox1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(370, 406);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(474, 406);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			textBoxVoucherNumber.Location = new System.Drawing.Point(397, 27);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.ReadOnly = true;
			textBoxVoucherNumber.Size = new System.Drawing.Size(132, 20);
			textBoxVoucherNumber.TabIndex = 158;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(321, 31);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(70, 13);
			label1.TabIndex = 157;
			label1.Text = "Doc Number:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(166, 29);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(44, 13);
			label5.TabIndex = 156;
			label5.Text = "Doc ID:";
			ultraGroupBox2.Controls.Add(productSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 53);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 207);
			ultraGroupBox2.TabIndex = 159;
			ultraGroupBox2.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 183);
			productSelector.TabIndex = 0;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(9, 27);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 13);
			label2.TabIndex = 163;
			label2.Text = "Doc Type:";
			comboBoxSysDocType.Assigned = false;
			comboBoxSysDocType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDocType.CustomReportFieldName = "";
			comboBoxSysDocType.CustomReportKey = "";
			comboBoxSysDocType.CustomReportValueType = 1;
			comboBoxSysDocType.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDocType.DisplayLayout.Appearance = appearance;
			comboBoxSysDocType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDocType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocType.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDocType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSysDocType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDocType.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSysDocType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDocType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDocType.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDocType.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSysDocType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDocType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocType.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDocType.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSysDocType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDocType.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocType.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSysDocType.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSysDocType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDocType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDocType.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSysDocType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDocType.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSysDocType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDocType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDocType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDocType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDocType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDocType.Editable = true;
			comboBoxSysDocType.FilterString = "";
			comboBoxSysDocType.HasAllAccount = false;
			comboBoxSysDocType.HasCustom = false;
			comboBoxSysDocType.IsDataLoaded = false;
			comboBoxSysDocType.Location = new System.Drawing.Point(63, 25);
			comboBoxSysDocType.MaxDropDownItems = 12;
			comboBoxSysDocType.Name = "comboBoxSysDocType";
			comboBoxSysDocType.ShowInactiveItems = false;
			comboBoxSysDocType.ShowQuickAdd = true;
			comboBoxSysDocType.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDocType.TabIndex = 164;
			comboBoxSysDocType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDocType.SelectedIndexChanged += new System.EventHandler(comboBoxSysDocType_SelectedIndexChanged);
			buttonGRN.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonGRN.BackColor = System.Drawing.Color.DarkGray;
			buttonGRN.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonGRN.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonGRN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonGRN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonGRN.Location = new System.Drawing.Point(534, 25);
			buttonGRN.Name = "buttonGRN";
			buttonGRN.Size = new System.Drawing.Size(34, 24);
			buttonGRN.TabIndex = 160;
			buttonGRN.Text = "...";
			buttonGRN.UseVisualStyleBackColor = false;
			buttonGRN.Click += new System.EventHandler(buttonGRN_Click);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance13;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(214, 26);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(103, 20);
			comboBoxSysDoc.TabIndex = 155;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(14, 365);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(38, 13);
			label4.TabIndex = 167;
			label4.Text = "status:";
			groupBoxQtyCheck.Controls.Add(textBoxQty);
			groupBoxQtyCheck.Controls.Add(label3);
			groupBoxQtyCheck.Controls.Add(checkBoxExactQty);
			groupBoxQtyCheck.Controls.Add(checkBoxIncludeItemQty);
			groupBoxQtyCheck.Location = new System.Drawing.Point(17, 276);
			groupBoxQtyCheck.Name = "groupBoxQtyCheck";
			groupBoxQtyCheck.Size = new System.Drawing.Size(230, 86);
			groupBoxQtyCheck.TabIndex = 170;
			groupBoxQtyCheck.TabStop = false;
			groupBoxQtyCheck.Text = "Quantity to Print";
			textBoxQty.AllowDecimal = true;
			textBoxQty.CustomReportFieldName = "";
			textBoxQty.CustomReportKey = "";
			textBoxQty.CustomReportValueType = 1;
			textBoxQty.IsComboTextBox = false;
			textBoxQty.IsModified = false;
			textBoxQty.Location = new System.Drawing.Point(65, 55);
			textBoxQty.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxQty.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxQty.Name = "textBoxQty";
			textBoxQty.NullText = "0";
			textBoxQty.Size = new System.Drawing.Size(59, 20);
			textBoxQty.TabIndex = 173;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 58);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(52, 13);
			label3.TabIndex = 172;
			label3.Text = "Quantity: ";
			checkBoxExactQty.AutoSize = true;
			checkBoxExactQty.Location = new System.Drawing.Point(119, 24);
			checkBoxExactQty.Name = "checkBoxExactQty";
			checkBoxExactQty.Size = new System.Drawing.Size(72, 17);
			checkBoxExactQty.TabIndex = 171;
			checkBoxExactQty.Text = "Exact Qty";
			checkBoxExactQty.UseVisualStyleBackColor = true;
			checkBoxExactQty.CheckedChanged += new System.EventHandler(checkBoxExactQty_CheckedChanged);
			checkBoxIncludeItemQty.AutoSize = true;
			checkBoxIncludeItemQty.Checked = true;
			checkBoxIncludeItemQty.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxIncludeItemQty.Location = new System.Drawing.Point(10, 24);
			checkBoxIncludeItemQty.Name = "checkBoxIncludeItemQty";
			checkBoxIncludeItemQty.Size = new System.Drawing.Size(103, 17);
			checkBoxIncludeItemQty.TabIndex = 170;
			checkBoxIncludeItemQty.Text = "Include Item Qty";
			checkBoxIncludeItemQty.UseVisualStyleBackColor = true;
			checkBoxIncludeItemQty.CheckedChanged += new System.EventHandler(checkBoxIncludeItemQty_CheckedChanged);
			groupBox1.Controls.Add(panel1);
			groupBox1.Location = new System.Drawing.Point(268, 331);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(189, 42);
			groupBox1.TabIndex = 172;
			groupBox1.TabStop = false;
			groupBox1.Visible = false;
			panel1.Controls.Add(radioButtonImport);
			panel1.Controls.Add(radioButtonLocal);
			panel1.Location = new System.Drawing.Point(6, 11);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(157, 27);
			panel1.TabIndex = 161;
			radioButtonImport.AutoSize = true;
			radioButtonImport.Location = new System.Drawing.Point(78, 5);
			radioButtonImport.Name = "radioButtonImport";
			radioButtonImport.Size = new System.Drawing.Size(54, 17);
			radioButtonImport.TabIndex = 1;
			radioButtonImport.Text = "Import";
			radioButtonImport.UseVisualStyleBackColor = true;
			radioButtonLocal.AutoSize = true;
			radioButtonLocal.Checked = true;
			radioButtonLocal.Location = new System.Drawing.Point(8, 5);
			radioButtonLocal.Name = "radioButtonLocal";
			radioButtonLocal.Size = new System.Drawing.Size(51, 17);
			radioButtonLocal.TabIndex = 0;
			radioButtonLocal.TabStop = true;
			radioButtonLocal.Text = "Local";
			radioButtonLocal.UseVisualStyleBackColor = true;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(268, 276);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(283, 56);
			dateControl1.TabIndex = 171;
			dateControl1.ToDate = new System.DateTime(2017, 10, 26, 23, 59, 59, 59);
			dateControl1.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(582, 437);
			base.Controls.Add(groupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(groupBoxQtyCheck);
			base.Controls.Add(label4);
			base.Controls.Add(comboBoxSysDocType);
			base.Controls.Add(label2);
			base.Controls.Add(buttonGRN);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(label1);
			base.Controls.Add(label5);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "BarCodeReport";
			Text = "BarCode Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxSysDocType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			groupBoxQtyCheck.ResumeLayout(false);
			groupBoxQtyCheck.PerformLayout();
			groupBox1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
