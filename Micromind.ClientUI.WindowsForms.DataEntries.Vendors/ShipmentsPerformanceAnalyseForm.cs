using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class ShipmentsPerformanceAnalyseForm : Form
	{
		private RecurringInvoiceData currentData;

		private bool activatebin = CompanyPreferences.ActivateBin;

		private bool materialReservationOnSO = CompanyPreferences.MaterialReservationONSO;

		private bool isNewRecord = true;

		private bool allowEdit = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private IContainer components;

		private XPButton buttonFill;

		private BinComboBox comboBoxGridBin;

		private RackComboBox comboBoxGridRack;

		private Label label2;

		private LocationComboBox locationComboBox2;

		private Label label4;

		private DateTimePicker dateTimePickerFrom;

		private DateTimePicker dateTimePickerTo;

		private BackgroundWorker _worker;

		private TextBox textBoxVendorName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private vendorsFlatComboBox comboBoxVendor;

		private CheckBox checkBoxShowClosed;

		private ComboBox comboBoxStatus;

		private LinkLabel linkLabelPO;

		private LinkLabel linkLabelGRN;

		private VendorClassComboBox comboBoxToClass;

		private VendorClassComboBox comboBoxFromClass;

		private Label label5;

		private Label label3;

		private Label label1;

		private Label label6;

		private DataGridList dataGridList;

		private ContextMenuStrip contextMenuStripDelete;

		private ToolStripMenuItem openPurchaseOrderToolStripMenuItem;

		private ToolStripMenuItem openGRNToolStripMenuItem;

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public ShipmentsPerformanceAnalyseForm()
		{
			InitializeComponent();
			base.Activated += IssueLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += UpdateLotDetailsForm_Load;
			base.FormClosing += UpdateLotDetailsForm_FormClosing;
			dataGridList.DoubleClickRow += dataGridItems_RowDoubleClick;
			dataGridList.ContextMenuStrip = contextMenuStripDelete;
		}

		private void UpdateLotDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridList.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void UpdateLotDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupListGrid();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
				ClearForm();
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridList.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("Voucher Number");
				dataTable.Columns.Add("GRN Date", typeof(DateTime));
				dataTable.Columns.Add("PO SysDocID");
				dataTable.Columns.Add("PO VoucherID");
				dataTable.Columns.Add("Container Number");
				dataTable.Columns.Add("VendorID");
				dataTable.Columns.Add("Vendor Name");
				dataTable.Columns.Add("Category Name");
				dataTable.Columns.Add("MinGuarantee", typeof(decimal));
				dataTable.Columns.Add("Group Name");
				dataTable.Columns.Add("Reference1");
				dataTable.Columns.Add("Reference2");
				dataTable.Columns.Add("Claim Amount", typeof(decimal));
				dataTable.Columns.Add("Claim AmountFC", typeof(decimal));
				dataTable.Columns.Add("Claim CurrencyID", typeof(string));
				dataTable.Columns.Add("Status");
				dataGridList.DataSource = dataTable;
				dataGridList.LoadLayout();
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void IssueLotSelectionForm_Activated(object sender, EventArgs e)
		{
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private void checkBoxShowAvailableLots_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonFill_Click(object sender, EventArgs e)
		{
			try
			{
				int num = 0;
				num = (checkBoxShowClosed.Checked ? 2 : 0);
				DataSet containerData = Factory.PurchaseReceiptSystem.GetContainerData(comboBoxFromClass.SelectedID, comboBoxToClass.SelectedID, num, dateTimePickerFrom.Value, dateTimePickerTo.Value);
				DataTable dataTable = dataGridList.DataSource as DataTable;
				if (dataTable.Rows.Count > 0)
				{
					dataTable.Rows.Clear();
				}
				if (containerData.Tables[0].Rows.Count != 0)
				{
					foreach (DataRow row in containerData.Tables[0].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["SysdocID"] = row["SysDocID"];
						dataRow2["Voucher Number"] = row["VoucherID"];
						dataRow2["GRN Date"] = row["TransactionDate"];
						dataRow2["VendorID"] = row["VendorID"];
						dataRow2["Vendor Name"] = row["Vendor"];
						dataRow2["Container Number"] = row["ContainerNumber"];
						dataRow2["Category Name"] = row["CategoryName"];
						dataRow2["Claim Amount"] = row["ClaimAmount"];
						dataRow2["Claim AmountFC"] = row["ClaimAmountFC"];
						dataRow2["Claim CurrencyID"] = row["ClaimCurrencyID"];
						dataRow2["Status"] = row["Status"];
						dataRow2["Group Name"] = row["GroupName"];
						dataRow2["Reference1"] = row["ClaimRef1"];
						dataRow2["Reference2"] = row["ClaimRef2"];
						if (row["POSysdocID"] != DBNull.Value)
						{
							dataRow2["PO SysDocID"] = row["POSysdocID"];
							dataRow2["PO VoucherID"] = row["POVoucherID"];
						}
						else if (row["SourceSysDocID"] != DBNull.Value)
						{
							dataRow2["PO SysDocID"] = row["SourceSysDocID"];
							dataRow2["PO VoucherID"] = row["SourceVoucherID"];
						}
						dataRow2["MinGuarantee"] = row["MinGuarantee"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
				}
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
		}

		private void xpButtonNew_Click(object sender, EventArgs e)
		{
			ClearForm();
		}

		private void ClearForm()
		{
			try
			{
				dateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				dateTimePickerTo.Value = DateTime.Now;
				checkBoxShowClosed.Checked = false;
				buttonFill.PerformClick();
				SetupListGrid();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridIncome_AfterCellUpdate(object sender, CellEventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
		}

		private void dataGridItems_RowDoubleClick(object sender, DoubleClickRowEventArgs e)
		{
			UpdateShipmentClaimStatusForm updateShipmentClaimStatusForm = new UpdateShipmentClaimStatusForm();
			updateShipmentClaimStatusForm.SystemDocID = dataGridList.ActiveRow.Cells["SysDocID"].Value.ToString();
			updateShipmentClaimStatusForm.VoucherID = dataGridList.ActiveRow.Cells["Voucher Number"].Value.ToString();
			updateShipmentClaimStatusForm.LoadData();
			updateShipmentClaimStatusForm.ShowDialog();
			buttonFill.PerformClick();
		}

		private void linkLabelPO_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelGRN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void linkLabelPO_Leave(object sender, EventArgs e)
		{
			linkLabelPO.Visible = false;
		}

		private void linkLabelGRN_Leave(object sender, EventArgs e)
		{
			linkLabelGRN.Visible = false;
		}

		private void dataGridItems_BeforeEnterEditMode(object sender, CancelEventArgs e)
		{
		}

		private void openPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow == null)
			{
				return;
			}
			string text = dataGridList.ActiveRow.Cells["PO VoucherID"].Text.ToString();
			string text2 = dataGridList.ActiveRow.Cells["PO SysDocID"].Text.ToString();
			if (!(text == ""))
			{
				switch (int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", text2).ToString()))
				{
				case 31:
					FormActivator.BringFormToFront(FormActivator.PurchaseOrderFormObj);
					FormActivator.PurchaseOrderFormObj.EditDocument(text2, text);
					break;
				case 38:
					FormActivator.BringFormToFront(FormActivator.PurchaseOrderImportFormObj);
					FormActivator.PurchaseOrderImportFormObj.EditDocument(text2, text);
					break;
				}
			}
		}

		private void openGRNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridList.ActiveRow == null)
			{
				return;
			}
			string text = dataGridList.ActiveRow.Cells["Voucher Number"].Value.ToString();
			string text2 = dataGridList.ActiveRow.Cells["SysDocID"].Value.ToString();
			if (!(text == ""))
			{
				switch (int.Parse(Factory.DatabaseSystem.GetFieldValue("System_Document", "SysDocType", "SysDocID", text2).ToString()))
				{
				case 32:
					FormActivator.BringFormToFront(FormActivator.PurchaseReceiptFormObj);
					FormActivator.PurchaseReceiptFormObj.EditDocument(text2, text);
					break;
				case 50:
					FormActivator.BringFormToFront(FormActivator.ImportPurchaseGRNFormObj);
					FormActivator.ImportPurchaseGRNFormObj.EditDocument(text2, text);
					break;
				}
			}
		}

		private void SetupListGrid()
		{
			dataGridList.ApplyUIDesign();
			dataGridList.ApplyFormat();
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
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.ShipmentsPerformanceAnalyseForm));
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
			dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			_worker = new System.ComponentModel.BackgroundWorker();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxShowClosed = new System.Windows.Forms.CheckBox();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			linkLabelPO = new System.Windows.Forms.LinkLabel();
			linkLabelGRN = new System.Windows.Forms.LinkLabel();
			label5 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			contextMenuStripDelete = new System.Windows.Forms.ContextMenuStrip(components);
			openPurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openGRNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			comboBoxToClass = new Micromind.DataControls.VendorClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.VendorClassComboBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			buttonFill = new Micromind.UISupport.XPButton();
			comboBoxGridRack = new Micromind.DataControls.RackComboBox();
			comboBoxGridBin = new Micromind.DataControls.BinComboBox();
			contextMenuStripDelete.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).BeginInit();
			SuspendLayout();
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(109, 45);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(39, 13);
			label2.TabIndex = 21;
			label2.Text = "From:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(288, 46);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(24, 13);
			label4.TabIndex = 24;
			label4.Text = "To:";
			dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFrom.Location = new System.Drawing.Point(152, 42);
			dateTimePickerFrom.Name = "dateTimePickerFrom";
			dateTimePickerFrom.Size = new System.Drawing.Size(114, 20);
			dateTimePickerFrom.TabIndex = 25;
			dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTo.Location = new System.Drawing.Point(325, 42);
			dateTimePickerTo.Name = "dateTimePickerTo";
			dateTimePickerTo.Size = new System.Drawing.Size(114, 20);
			dateTimePickerTo.TabIndex = 26;
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(522, 12);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(334, 20);
			textBoxVendorName.TabIndex = 131;
			textBoxVendorName.TabStop = false;
			textBoxVendorName.Visible = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(535, 12);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 132;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			ultraFormattedLinkLabel4.Visible = false;
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance2;
			checkBoxShowClosed.AutoSize = true;
			checkBoxShowClosed.Location = new System.Drawing.Point(458, 46);
			checkBoxShowClosed.Name = "checkBoxShowClosed";
			checkBoxShowClosed.Size = new System.Drawing.Size(88, 17);
			checkBoxShowClosed.TabIndex = 133;
			checkBoxShowClosed.Text = "Show Closed";
			checkBoxShowClosed.UseVisualStyleBackColor = true;
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Open",
				"Pending",
				"Closed"
			});
			comboBoxStatus.Location = new System.Drawing.Point(656, 158);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(118, 21);
			comboBoxStatus.TabIndex = 147;
			comboBoxStatus.Visible = false;
			linkLabelPO.AutoSize = true;
			linkLabelPO.ForeColor = System.Drawing.Color.White;
			linkLabelPO.LinkColor = System.Drawing.Color.Blue;
			linkLabelPO.Location = new System.Drawing.Point(611, 40);
			linkLabelPO.Name = "linkLabelPO";
			linkLabelPO.Size = new System.Drawing.Size(55, 13);
			linkLabelPO.TabIndex = 148;
			linkLabelPO.TabStop = true;
			linkLabelPO.Text = "linkLabel1";
			linkLabelPO.Visible = false;
			linkLabelPO.VisitedLinkColor = System.Drawing.Color.White;
			linkLabelPO.Click += new System.EventHandler(linkLabelPO_Click);
			linkLabelPO.Leave += new System.EventHandler(linkLabelPO_Leave);
			linkLabelGRN.AutoSize = true;
			linkLabelGRN.ForeColor = System.Drawing.Color.White;
			linkLabelGRN.LinkColor = System.Drawing.Color.Blue;
			linkLabelGRN.Location = new System.Drawing.Point(611, 23);
			linkLabelGRN.Name = "linkLabelGRN";
			linkLabelGRN.Size = new System.Drawing.Size(55, 13);
			linkLabelGRN.TabIndex = 149;
			linkLabelGRN.TabStop = true;
			linkLabelGRN.Text = "linkLabel1";
			linkLabelGRN.Visible = false;
			linkLabelGRN.VisitedLinkColor = System.Drawing.Color.White;
			linkLabelGRN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelGRN_LinkClicked);
			linkLabelGRN.Leave += new System.EventHandler(linkLabelGRN_Leave);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(288, 23);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(24, 13);
			label5.TabIndex = 154;
			label5.Text = "To:";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 23);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(73, 13);
			label3.TabIndex = 155;
			label3.Text = "Vendor Class-";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 45);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(93, 13);
			label1.TabIndex = 156;
			label1.Text = "Transaction Date-";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(109, 23);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(39, 13);
			label6.TabIndex = 157;
			label6.Text = "From:";
			contextMenuStripDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				openPurchaseOrderToolStripMenuItem,
				openGRNToolStripMenuItem
			});
			contextMenuStripDelete.Name = "contextMenuStripDelete";
			contextMenuStripDelete.Size = new System.Drawing.Size(156, 48);
			openPurchaseOrderToolStripMenuItem.Name = "openPurchaseOrderToolStripMenuItem";
			openPurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			openPurchaseOrderToolStripMenuItem.Text = "Purchase Order";
			openPurchaseOrderToolStripMenuItem.Click += new System.EventHandler(openPurchaseOrderToolStripMenuItem_Click);
			openGRNToolStripMenuItem.Name = "openGRNToolStripMenuItem";
			openGRNToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			openGRNToolStripMenuItem.Text = "GRN";
			openGRNToolStripMenuItem.Click += new System.EventHandler(openGRNToolStripMenuItem_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance3;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance10;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance12;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance13;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(10, 72);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(911, 340);
			dataGridList.TabIndex = 185;
			dataGridList.Text = "dataGridList1";
			comboBoxToClass.Assigned = false;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance15;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(324, 19);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(115, 20);
			comboBoxToClass.TabIndex = 153;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.Assigned = false;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance27;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(152, 19);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(114, 20);
			comboBoxFromClass.TabIndex = 152;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxVendorName;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance39;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(529, 12);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(114, 20);
			comboBoxVendor.TabIndex = 130;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendor.Visible = false;
			buttonFill.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFill.BackColor = System.Drawing.Color.DarkGray;
			buttonFill.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFill.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFill.Location = new System.Drawing.Point(780, 42);
			buttonFill.Name = "buttonFill";
			buttonFill.Size = new System.Drawing.Size(96, 24);
			buttonFill.TabIndex = 4;
			buttonFill.Text = "Refresh";
			buttonFill.UseVisualStyleBackColor = false;
			buttonFill.Click += new System.EventHandler(buttonFill_Click);
			comboBoxGridRack.Assigned = false;
			comboBoxGridRack.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridRack.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridRack.CustomReportFieldName = "";
			comboBoxGridRack.CustomReportKey = "";
			comboBoxGridRack.CustomReportValueType = 1;
			comboBoxGridRack.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridRack.DisplayLayout.Appearance = appearance51;
			comboBoxGridRack.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridRack.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxGridRack.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxGridRack.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridRack.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridRack.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridRack.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridRack.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxGridRack.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridRack.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxGridRack.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxGridRack.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridRack.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridRack.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxGridRack.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridRack.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxGridRack.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridRack.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridRack.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridRack.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridRack.Editable = true;
			comboBoxGridRack.FilterBinID = "";
			comboBoxGridRack.FilterString = "";
			comboBoxGridRack.HasAllAccount = false;
			comboBoxGridRack.HasCustom = false;
			comboBoxGridRack.IsDataLoaded = false;
			comboBoxGridRack.Location = new System.Drawing.Point(312, 216);
			comboBoxGridRack.MaxDropDownItems = 12;
			comboBoxGridRack.Name = "comboBoxGridRack";
			comboBoxGridRack.ShowInactiveItems = false;
			comboBoxGridRack.ShowQuickAdd = true;
			comboBoxGridRack.Size = new System.Drawing.Size(100, 20);
			comboBoxGridRack.TabIndex = 19;
			comboBoxGridRack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridRack.Visible = false;
			comboBoxGridBin.Assigned = false;
			comboBoxGridBin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridBin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridBin.CustomReportFieldName = "";
			comboBoxGridBin.CustomReportKey = "";
			comboBoxGridBin.CustomReportValueType = 1;
			comboBoxGridBin.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBin.DisplayLayout.Appearance = appearance63;
			comboBoxGridBin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxGridBin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxGridBin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBin.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBin.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBin.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxGridBin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBin.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxGridBin.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxGridBin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBin.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxGridBin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBin.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxGridBin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridBin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridBin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridBin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridBin.Editable = true;
			comboBoxGridBin.FilterString = "";
			comboBoxGridBin.HasAllAccount = false;
			comboBoxGridBin.HasCustom = false;
			comboBoxGridBin.IsDataLoaded = false;
			comboBoxGridBin.Location = new System.Drawing.Point(254, 175);
			comboBoxGridBin.MaxDropDownItems = 12;
			comboBoxGridBin.Name = "comboBoxGridBin";
			comboBoxGridBin.ShowInactiveItems = false;
			comboBoxGridBin.ShowQuickAdd = true;
			comboBoxGridBin.Size = new System.Drawing.Size(100, 20);
			comboBoxGridBin.TabIndex = 18;
			comboBoxGridBin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridBin.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(933, 426);
			base.Controls.Add(dataGridList);
			base.Controls.Add(label6);
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(label5);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(linkLabelGRN);
			base.Controls.Add(linkLabelPO);
			base.Controls.Add(checkBoxShowClosed);
			base.Controls.Add(textBoxVendorName);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(comboBoxVendor);
			base.Controls.Add(dateTimePickerTo);
			base.Controls.Add(dateTimePickerFrom);
			base.Controls.Add(label4);
			base.Controls.Add(label2);
			base.Controls.Add(buttonFill);
			base.Controls.Add(comboBoxGridRack);
			base.Controls.Add(comboBoxGridBin);
			base.Controls.Add(comboBoxStatus);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ShipmentsPerformanceAnalyseForm";
			Text = "Shipments Performance Analysis";
			contextMenuStripDelete.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
