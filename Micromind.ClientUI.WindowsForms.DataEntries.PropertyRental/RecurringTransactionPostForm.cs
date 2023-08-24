using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class RecurringTransactionPostForm : Form
	{
		private RecurringInvoiceData currentData;

		private bool activatebin = CompanyPreferences.ActivateBin;

		private bool materialReservationOnSO = CompanyPreferences.MaterialReservationONSO;

		private bool isNewRecord = true;

		private bool allowEdit = true;

		private DataSet companyInfo;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonSave;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private DataEntryGrid dataGridItems;

		private XPButton buttonFill;

		private BinComboBox comboBoxGridBin;

		private RackComboBox comboBoxGridRack;

		private Label label2;

		private LocationComboBox locationComboBox2;

		private Label label4;

		private XPButton xpButtonNew;

		private DateTimePicker dateTimePickerFrom;

		private DateTimePicker dateTimePickerTo;

		private BackgroundWorker _worker;

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

		public RecurringTransactionPostForm()
		{
			InitializeComponent();
			base.Activated += IssueLotSelectionForm_Activated;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += UpdateLotDetailsForm_Load;
			base.FormClosing += UpdateLotDetailsForm_FormClosing;
			dataGridItems.CellChange += dataGridItems_CellChange;
		}

		private void UpdateLotDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridItems.SaveLayout();
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
				dataGridItems.SetupUI();
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			DataRow dataRow = null;
			if ((currentData != null) & (currentData.Tables.Count > 0))
			{
				DataTable dataTable2 = currentData.Tables["Product_Lot"];
				for (int i = 0; i < dataTable2.Rows.Count; i = checked(i + 1))
				{
					dataRow = dataTable2.Rows[i];
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["LotID"] = dataRow["LotNumber"];
					dataRow2["Heat"] = dataRow["Reference"];
					dataRow2["Caste"] = dataRow["Reference2"];
					dataRow2["Consign#"] = dataRow["Consign#"];
					dataRow2["ExpiryDate"] = dataRow["ExpiryDate"];
					dataRow2["Productiondate"] = dataRow["ProductionDate"];
					dataRow2["LotQty"] = dataRow["AvailableQty"];
					dataRow2["BinID"] = dataRow["BinID"];
					dataRow2["RackID"] = dataRow["RackID"];
					dataTable.Rows.Add(dataRow2);
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.SetupUI();
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Select", typeof(bool));
				dataTable.Columns.Add("TransactionDate", typeof(DateTime));
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("Tenant");
				dataTable.Columns.Add("Value");
				dataTable.Columns.Add("TransactionID");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("CreatedSysDocID");
				dataTable.Columns.Add("CreatedVoucherID");
				dataTable.Columns.Add("Note", typeof(string));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Base SysDocID";
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Base VoucherID";
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionID"].Hidden = true;
				dataGridItems.AllowAddNew = false;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void IssueLotSelectionForm_Activated(object sender, EventArgs e)
		{
		}

		private void Grid_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				InitWorker();
			}
			catch (Exception e2)
			{
				base.DialogResult = DialogResult.None;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void labelCustomize_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void checkBoxUnitPriceOverWrite_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxShowAvailableLots_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void LoadData()
		{
		}

		private void buttonFill_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet periodicInvoice = Factory.RecurringInvoiceSystem.GetPeriodicInvoice();
				GetData(periodicInvoice);
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
				dateTimePickerFrom.Value = DateTime.Now;
				dateTimePickerTo.Value = DateTime.Now;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				buttonSave.Enabled = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void GetData(DataSet data)
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in data.Tables[0].Rows)
			{
				string text = row["TransactionID"].ToString();
				string value = row["SysDocID"].ToString();
				string value2 = row["VoucherID"].ToString();
				DateTime t = DateTime.Parse(row["LastRunDate"].ToString());
				DateTime dateTime = DateTime.Parse(row["EndDate"].ToString());
				DateTime t2 = dateTimePickerTo.Value;
				if (dateTime < t2)
				{
					t2 = dateTime;
				}
				DateTime lastPostedDate = Factory.RecurringInvoiceSystem.GetLastPostedDate(text);
				string a = row["RepeateEvery"].ToString();
				int num = int.Parse(row["Interval"].ToString());
				lastPostedDate = ((!(a == "M")) ? lastPostedDate.AddDays(num) : lastPostedDate.AddMonths(num));
				if (t < t2)
				{
					DateTime dateTime2 = lastPostedDate;
					while (dateTime2 <= t2)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["TransactionID"] = text;
						dataRow2["TransactionDate"] = dateTime2;
						dataRow2["SysDocID"] = value;
						dataRow2["VoucherID"] = value2;
						dataRow2["CreatedSysDocID"] = "";
						dataRow2["CreatedVoucherID"] = "";
						dataRow2["Status"] = "Wait";
						dataRow2["Select"] = true;
						dataRow2["Tenant"] = row["Tenant"].ToString();
						dataRow2["Value"] = row["Total"].ToString();
						string text2 = "";
						int month = int.Parse(dateTime2.Month.ToString());
						text2 = "RENT FOR " + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month) + "-" + dateTime2.Year.ToString() + " ";
						if (a == "M")
						{
							text2 = text2 + " [" + dateTime2.ToShortDateString() + " to " + dateTime2.AddMonths(num).AddDays(-1.0).ToShortDateString() + "]";
						}
						else if (a == "D")
						{
							text2 = text2 + " [" + dateTime2.ToShortDateString() + " to " + dateTime2.AddDays(num).ToShortDateString() + "]";
						}
						dataRow2["Note"] = text2;
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
						dateTime2 = ((!(a == "M")) ? dateTime2.AddDays(num) : dateTime2.AddMonths(num));
					}
				}
			}
		}

		private bool SaveRecurringData()
		{
			bool flag = true;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!bool.Parse(row.Cells["Select"].Value.ToString()))
				{
					return false;
				}
				string text = row.Cells["TransactionID"].Value.ToString();
				currentData = Factory.RecurringInvoiceSystem.GetRecurringInvoiceByID(text);
				if (currentData == null)
				{
					return false;
				}
				string text2 = row.Cells["SysDocID"].Value.ToString();
				string nextDocumentNumber = Factory.SystemDocumentSystem.GetNextDocumentNumber(text2);
				row.Cells["CreatedSysDocID"].Value = text2;
				row.Cells["CreatedVoucherID"].Value = nextDocumentNumber;
				DataRow dataRow = currentData.RecurringInvoiceTransactionTable.Rows[0];
				dataRow.BeginEdit();
				dataRow["LastRunDate"] = DateTime.Parse(row.Cells["TransactionDate"].Value.ToString());
				dataRow["LastSysDocID"] = text2;
				dataRow["LastVoucherID"] = nextDocumentNumber;
				dataRow["Status"] = false;
				dataRow.EndEdit();
				dataRow = currentData.RecurringTransactionDetailsTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TransactionID"] = text;
				dataRow["TransactionDate"] = DateTime.Parse(row.Cells["TransactionDate"].Value.ToString());
				dataRow["SysDocID"] = row.Cells["SysDocID"].Value.ToString();
				dataRow["VoucherID"] = row.Cells["VoucherID"].Value.ToString();
				dataRow["CreatedSysDocID"] = text2;
				dataRow["CreatedVoucherID"] = nextDocumentNumber;
				dataRow["InvoiceNote"] = row.Cells["Note"].Value.ToString();
				dataRow.EndEdit();
				currentData.RecurringTransactionDetailsTable.Rows.Add(dataRow);
				flag &= Factory.RecurringInvoiceSystem.CreateRecurringInvoice(currentData, isUpdate: false, isPosting: true);
			}
			return flag;
		}

		private void dataGridIncome_AfterCellUpdate(object sender, CellEventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Select")
			{
				try
				{
					string voucherID = dataGridItems.ActiveRow.Cells["VoucherID"].Value.ToString();
					string sysdocID = dataGridItems.ActiveRow.Cells["SysDocID"].Value.ToString();
					DateTime selectedDate = DateTime.Parse(dataGridItems.ActiveRow.Cells["TransactionDate"].Value.ToString());
					bool flag = bool.Parse(dataGridItems.ActiveRow.Cells["Select"].Value.ToString());
					if (!ValidSelection(sysdocID, voucherID, selectedDate, flag))
					{
						dataGridItems.ActiveRow.Cells["Select"].Value = flag;
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private bool ValidSelection(string sysdocID, string voucherID, DateTime selectedDate, bool selection)
		{
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["SysDocID"].Value.ToString() == sysdocID && row.Cells["VoucherID"].Value.ToString() == voucherID)
				{
					if (!selection)
					{
						if (DateTime.Parse(row.Cells["TransactionDate"].Value.ToString()) == selectedDate)
						{
							return true;
						}
						if (!bool.Parse(row.Cells["Select"].Value.ToString()))
						{
							MessageBox.Show("wrong selection");
							row.Cells["Select"].Value = selection;
							return false;
						}
					}
					else if (DateTime.Parse(row.Cells["TransactionDate"].Value.ToString()) > selectedDate && bool.Parse(row.Cells["Select"].Value.ToString()))
					{
						MessageBox.Show("wrong selection");
						row.Cells["Select"].Value = selection;
						return false;
					}
				}
			}
			return true;
		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
			}
			else
			{
				try
				{
					base.UseWaitCursor = true;
					bool flag = true;
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (!bool.Parse(row.Cells["Select"].Value.ToString()))
						{
							break;
						}
						row.Cells["Status"].Value = "Processing";
						string text = row.Cells["TransactionID"].Value.ToString();
						currentData = Factory.RecurringInvoiceSystem.GetRecurringInvoiceByID(text);
						if (currentData == null)
						{
							break;
						}
						string newSysDocID = row.Cells["SysDocID"].Value.ToString();
						string newvoucherID = Factory.SystemDocumentSystem.GetNextDocumentNumber(newSysDocID);
						DataRow dataRow = currentData.RecurringInvoiceTransactionTable.Rows[0];
						dataRow.BeginEdit();
						dataRow["LastRunDate"] = DateTime.Parse(row.Cells["TransactionDate"].Value.ToString());
						dataRow["LastSysDocID"] = newSysDocID;
						dataRow["LastVoucherID"] = newvoucherID;
						DateTime dateTime = DateTime.Parse(row.Cells["TransactionDate"].Value.ToString());
						DateTime t = DateTime.Parse(dataRow["EndDate"].ToString());
						int num = int.Parse(dataRow["Interval"].ToString());
						dateTime = ((!(dataRow["RepeateEvery"].ToString() == "M")) ? dateTime.AddDays(num) : dateTime.AddMonths(num));
						if (dateTime >= t)
						{
							dataRow["Status"] = true;
						}
						else
						{
							dataRow["Status"] = false;
						}
						dataRow.EndEdit();
						dataRow = currentData.RecurringTransactionDetailsTable.NewRow();
						dataRow.BeginEdit();
						dataRow["TransactionID"] = text;
						dataRow["TransactionDate"] = DateTime.Parse(row.Cells["TransactionDate"].Value.ToString());
						dataRow["SysDocID"] = row.Cells["SysDocID"].Value.ToString();
						dataRow["VoucherID"] = row.Cells["VoucherID"].Value.ToString();
						dataRow["CreatedSysDocID"] = newSysDocID;
						dataRow["CreatedVoucherID"] = newvoucherID;
						dataRow["InvoiceNote"] = row.Cells["Note"].Value.ToString();
						dataRow.EndEdit();
						currentData.RecurringTransactionDetailsTable.Rows.Add(dataRow);
						flag &= Factory.RecurringInvoiceSystem.CreateRecurringInvoice(currentData, !IsNewRecord, isPosting: true);
						Invoke((MethodInvoker)delegate
						{
							Thread.Sleep(2);
							row.Cells["Status"].Value = "Processed";
							row.Cells["CreatedSysDocID"].Value = newSysDocID;
							row.Cells["CreatedVoucherID"].Value = newvoucherID;
						});
					}
				}
				catch (Exception ex)
				{
					new Thread(CloseIt).Start();
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void RecurringTransactionPostForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_worker.WorkerSupportsCancellation = true;
			_worker.CancelAsync();
			_worker.Dispose();
			_worker = null;
		}

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ErrorHelper.InformationMessage("Posting Completed");
			base.UseWaitCursor = false;
			buttonSave.Enabled = false;
		}

		public void CloseIt()
		{
			Thread.Sleep(2000);
			Interaction.AppActivate(Process.GetCurrentProcess().Id);
			SendKeys.SendWait(" ");
		}

		private void InitWorker()
		{
			if (_worker != null)
			{
				_worker.Dispose();
			}
			_worker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			_worker.DoWork += DoWork;
			_worker.RunWorkerCompleted += RunWorkerCompleted;
			_worker.RunWorkerAsync();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.RecurringTransactionPostForm));
			panelButtons = new System.Windows.Forms.Panel();
			xpButtonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonFill = new Micromind.UISupport.XPButton();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			comboBoxGridRack = new Micromind.DataControls.RackComboBox();
			comboBoxGridBin = new Micromind.DataControls.BinComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
			dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			_worker = new System.ComponentModel.BackgroundWorker();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(xpButtonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 421);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(801, 40);
			panelButtons.TabIndex = 6;
			xpButtonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonNew.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButtonNew.BackColor = System.Drawing.Color.DarkGray;
			xpButtonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButtonNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButtonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonNew.Location = new System.Drawing.Point(587, 8);
			xpButtonNew.Name = "xpButtonNew";
			xpButtonNew.Size = new System.Drawing.Size(96, 24);
			xpButtonNew.TabIndex = 1;
			xpButtonNew.Text = "&New";
			xpButtonNew.UseVisualStyleBackColor = false;
			xpButtonNew.Click += new System.EventHandler(xpButtonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.DarkGray;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(482, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "Post";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(801, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(691, 8);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonFill.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFill.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonFill.BackColor = System.Drawing.Color.DarkGray;
			buttonFill.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFill.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFill.Location = new System.Drawing.Point(693, 34);
			buttonFill.Name = "buttonFill";
			buttonFill.Size = new System.Drawing.Size(96, 24);
			buttonFill.TabIndex = 4;
			buttonFill.Text = "Fill Data";
			buttonFill.UseVisualStyleBackColor = false;
			buttonFill.Click += new System.EventHandler(buttonFill_Click);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 34);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(39, 13);
			label2.TabIndex = 21;
			label2.Text = "From:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(194, 34);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(24, 13);
			label4.TabIndex = 24;
			label4.Text = "To:";
			comboBoxGridRack.Assigned = false;
			comboBoxGridRack.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridRack.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridRack.CustomReportFieldName = "";
			comboBoxGridRack.CustomReportKey = "";
			comboBoxGridRack.CustomReportValueType = 1;
			comboBoxGridRack.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridRack.DisplayLayout.Appearance = appearance;
			comboBoxGridRack.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridRack.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridRack.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridRack.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridRack.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridRack.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridRack.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridRack.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridRack.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridRack.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridRack.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridRack.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridRack.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridRack.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridRack.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridRack.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridRack.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridRack.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridRack.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridBin.DisplayLayout.Appearance = appearance13;
			comboBoxGridBin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridBin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridBin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridBin.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridBin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridBin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridBin.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridBin.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridBin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridBin.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridBin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridBin.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridBin.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridBin.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridBin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridBin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridBin.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridBin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridBin.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance25;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(10, 59);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(778, 346);
			dataGridItems.TabIndex = 5;
			dataGridItems.Text = "dataEntryGrid1";
			dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFrom.Location = new System.Drawing.Point(58, 30);
			dateTimePickerFrom.Name = "dateTimePickerFrom";
			dateTimePickerFrom.Size = new System.Drawing.Size(108, 20);
			dateTimePickerFrom.TabIndex = 25;
			dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTo.Location = new System.Drawing.Point(224, 30);
			dateTimePickerTo.Name = "dateTimePickerTo";
			dateTimePickerTo.Size = new System.Drawing.Size(108, 20);
			dateTimePickerTo.TabIndex = 26;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(801, 461);
			base.Controls.Add(dateTimePickerTo);
			base.Controls.Add(dateTimePickerFrom);
			base.Controls.Add(label4);
			base.Controls.Add(label2);
			base.Controls.Add(buttonFill);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.Controls.Add(comboBoxGridRack);
			base.Controls.Add(comboBoxGridBin);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RecurringTransactionPostForm";
			Text = "Recurring Transaction Post";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(RecurringTransactionPostForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridRack).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridBin).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
