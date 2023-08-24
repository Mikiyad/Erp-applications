using DevExpress.Utils;
using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class formPayment : XtraForm
	{
		public bool IsNewRecord = true;

		private string changePaymentMethodID = "";

		private string changeAccountID = "";

		private DataTable paymentTable;

		private bool isEditable = true;

		private decimal TotalCurrency;

		private IContainer components;

		private InvoiceEntryGrid dataGridPayment;

		private UltraDataSource ultraDataSource1;

		private TextEdit textBoxTotal;

		private Label label3;

		private TextEdit textBoxPayment;

		private Label label1;

		private TextEdit textBoxChange;

		private Label labelChangeTitle;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private TextEdit textBoxBalance;

		private Label label4;

		private NumericKeypad numericKeypad1;

		private SimpleButton buttonVKeyboard;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private SimpleButton button5AED;

		private SimpleButton button20AED;

		private SimpleButton button500AED;

		private SimpleButton button50AED;

		private SimpleButton button100AED;

		private SimpleButton button200AED;

		private SimpleButton button1000AED;

		private SimpleButton button25FILS;

		private SimpleButton button1AED;

		private SimpleButton button50FILS;

		private SimpleButton button10AED;

		private SimpleButton buttonActual;

		private Label labelshiftClose;

		public decimal TotalDue
		{
			get
			{
				return decimal.Parse(textBoxTotal.Text);
			}
			set
			{
				textBoxTotal.Text = value.ToString(Format.TotalAmountFormat);
				CalculateTotal();
			}
		}

		public decimal Change
		{
			get
			{
				return decimal.Parse(textBoxChange.Text);
			}
			set
			{
				textBoxChange.Text = value.ToString(Format.TotalAmountFormat);
			}
		}

		public bool IsEditable
		{
			get
			{
				return isEditable;
			}
			set
			{
				isEditable = value;
			}
		}

		public DataTable PaymentTable
		{
			get
			{
				return paymentTable;
			}
			set
			{
				paymentTable = value;
			}
		}

		public formPayment()
		{
			InitializeComponent();
			dataGridPayment.AfterCellUpdate += invoiceEntryGrid1_AfterCellUpdate;
			base.Activated += formPayment_Activated;
			dataGridPayment.AfterEnterEditMode += invoiceEntryGrid1_AfterEnterEditMode;
			dataGridPayment.BeforeCellUpdate += dataGridPayment_BeforeCellUpdate;
			dataGridPayment.ExitEditModeOnLeave = true;
			numericKeypad1.DisplayControl = dataGridPayment;
			numericKeypad1.Visible = false;
		}

		private void dataGridPayment_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void invoiceEntryGrid1_AfterEnterEditMode(object sender, EventArgs e)
		{
			if (dataGridPayment.ActiveCell.Value != null && dataGridPayment.ActiveCell.Value.ToString() != "" && dataGridPayment.ActiveCell.Column.Key == "Amount")
			{
				if (decimal.Parse(dataGridPayment.ActiveCell.Value.ToString()) == 0m)
				{
					dataGridPayment.ActiveCell.Value = textBoxBalance.Text;
					dataGridPayment.ActiveCell.SelectAll();
				}
				dataGridPayment.PerformAction(UltraGridAction.EnterEditMode);
				dataGridPayment.ActiveCell.SelectAll();
			}
		}

		private void formPayment_Activated(object sender, EventArgs e)
		{
		}

		private void invoiceEntryGrid1_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount" && e.Cell.Value.ToString() == "")
			{
				e.Cell.Value = 0.ToString(Format.TotalAmountFormat);
			}
			CalculateTotal();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
			try
			{
				if (!isEditable)
				{
					SimpleButton simpleButton = buttonActual;
					bool enabled = buttonSave.Enabled = false;
					simpleButton.Enabled = enabled;
					labelshiftClose.Visible = true;
				}
				LoadPaymentMethods();
				if (!IsNewRecord)
				{
					FillData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool ValidateData()
		{
			decimal num = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			decimal d = default(decimal);
			foreach (UltraGridRow row in dataGridPayment.Rows)
			{
				if (int.Parse(row.Cells["MethodType"].Value.ToString()) != 1)
				{
					d += decimal.Parse(row.Cells["Amount"].Value.ToString());
				}
			}
			if (d != 0m && num > 0m && d > num)
			{
				ErrorHelper.InformationMessage("Total Non-Cash payments cannot exceed total due amount.");
				return false;
			}
			if (!IsEditable)
			{
				ErrorHelper.WarningMessage("Shift is closed.");
				return false;
			}
			return true;
		}

		private void LoadPaymentMethods()
		{
			DataRow[] array = CombosData.GetPOSPaymentMethods(refresh: false).Tables["POS_CashRegister_PaymentMethod"].Select("CashRegisterID = '" + Global.CurrentCashRegisterID + "'");
			dataGridPayment.SetupGrid();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("AccountID");
			dataTable.Columns.Add("PaymentMethodID");
			dataTable.Columns.Add("MethodType", typeof(byte));
			foreach (DataRow dataRow in array)
			{
				dataTable.Rows.Add(dataRow["DisplayName"].ToString(), 0, dataRow["AccountID"].ToString(), dataRow["PaymentMethodID"].ToString(), dataRow["MethodType"].ToString());
				if (byte.Parse(dataRow["MethodType"].ToString()) == 1 && changeAccountID == "")
				{
					changePaymentMethodID = dataRow["PaymentMethodID"].ToString();
					changeAccountID = dataRow["AccountID"].ToString();
					labelChangeTitle.Text = "Change " + dataRow["DisplayName"].ToString();
				}
			}
			dataGridPayment.DataSource = dataTable;
			if (dataGridPayment.Rows.Count > 0)
			{
				dataGridPayment.Rows[0].Cells[1].Activate();
			}
			dataGridPayment.DisplayLayout.Bands[0].Columns["AccountID"].Hidden = true;
			dataGridPayment.DisplayLayout.Bands[0].Columns["PaymentMethodID"].Hidden = true;
			dataGridPayment.DisplayLayout.Bands[0].Columns["MethodType"].Hidden = true;
			dataGridPayment.Font = new Font(dataGridPayment.Font.Name, 16f);
			dataGridPayment.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
			dataGridPayment.DisplayLayout.Bands[0].Override.CellAppearance.ForeColor = Color.FromArgb(16, 37, 127);
			dataGridPayment.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColor = Color.FromArgb(187, 213, 242);
			dataGridPayment.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayment.DisplayLayout.Bands[0].Columns["Amount"].Format = "#,##0.00";
			dataGridPayment.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.Disabled;
			dataGridPayment.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.ForeColorDisabled = Color.FromArgb(16, 37, 127);
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			foreach (UltraGridRow row in dataGridPayment.Rows)
			{
				decimal num3 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					num3 = decimal.Parse(row.Cells["Amount"].Value.ToString());
					num += num3;
				}
			}
			textBoxPayment.Text = num.ToString(Format.TotalAmountFormat);
			num2 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			if (num >= num2)
			{
				num5 = num - num2;
				num4 = default(decimal);
			}
			else
			{
				num4 = num2 - num;
			}
			textBoxBalance.Text = num4.ToString(Format.TotalAmountFormat);
			textBoxChange.Text = num5.ToString(Format.TotalAmountFormat);
			if (num4 == 0m)
			{
				textBoxBalance.ForeColor = Color.Green;
				buttonSave.Enabled = true;
			}
			else
			{
				textBoxBalance.ForeColor = Color.Red;
				buttonSave.Enabled = false;
			}
		}

		private void FillData()
		{
			if (!IsNewRecord && paymentTable != null)
			{
				foreach (DataRow row in paymentTable.Rows)
				{
					string value = row["PaymentMethodID"].ToString();
					decimal num = decimal.Parse(row["Amount"].ToString());
					int num2 = dataGridPayment.ExistCellValue("PaymentMethodID", value);
					if (num2 >= 0)
					{
						dataGridPayment.Rows[num2].Cells["Amount"].Value = num;
					}
					else
					{
						ErrorHelper.WarningMessage("One of the payment methods used in transaction is no longer available. Transaction payment may not be loaded correctly.");
					}
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			dataGridPayment.PerformAction(UltraGridAction.ExitEditMode);
			if (ValidateData())
			{
				if (paymentTable == null)
				{
					ErrorHelper.ErrorMessage("Payment Table is not provided.");
					base.DialogResult = DialogResult.Cancel;
					Close();
				}
				decimal num2 = Change = decimal.Parse(textBoxChange.Text);
				paymentTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridPayment.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
					if (!(result <= 0m))
					{
						DataRow dataRow = paymentTable.NewRow();
						dataRow["SysDocID"] = row.Index;
						dataRow["VoucherID"] = row.Index;
						dataRow["RegisterID"] = Global.CurrentCashRegisterID;
						dataRow["TransactionDate"] = DateTime.Now;
						if (byte.Parse(row.Cells["MethodType"].Value.ToString()) == 1)
						{
							if (!(result >= num2))
							{
								num2 -= result;
								continue;
							}
							dataRow["Amount"] = result - num2;
							num2 = default(decimal);
						}
						else
						{
							dataRow["Amount"] = row.Cells["Amount"].Value.ToString();
						}
						dataRow["AccountID"] = row.Cells["AccountID"].Value.ToString();
						dataRow["PaymentMethodID"] = row.Cells["PaymentMethodID"].Value.ToString();
						dataRow["PaymentMethodType"] = row.Cells["MethodType"].Value.ToString();
						dataRow.EndEdit();
						paymentTable.Rows.Add(dataRow);
					}
				}
				if (num2 > 0m)
				{
					DataRow dataRow2 = paymentTable.NewRow();
					dataRow2["SysDocID"] = 0;
					dataRow2["VoucherID"] = 0;
					dataRow2["RegisterID"] = Global.CurrentCashRegisterID;
					dataRow2["TransactionDate"] = DateTime.Now;
					dataRow2["Amount"] = -1m * num2;
					dataRow2["AccountID"] = changeAccountID;
					dataRow2["PaymentMethodID"] = changePaymentMethodID;
					dataRow2["PaymentMethodType"] = PaymentMethodTypes.Cash;
					dataRow2.EndEdit();
					paymentTable.Rows.Add(dataRow2);
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void buttonVKeyboard_Click(object sender, EventArgs e)
		{
		}

		private void button1000AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button1000AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button500AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button500AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button200AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button200AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button100AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button100AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button50AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button50AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button20AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button20AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button10AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button10AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button5AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button5AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button1AED_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button1AED.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button50FILS_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button50FILS.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void button25FILS_Click(object sender, EventArgs e)
		{
			TotalCurrency += decimal.Parse(button25FILS.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
		}

		private void buttonActual_Click(object sender, EventArgs e)
		{
			TotalCurrency = decimal.Parse(textBoxTotal.Text);
			dataGridPayment.ActiveCell.Value = TotalCurrency;
			TotalCurrency = default(decimal);
		}

		private void label2_Click(object sender, EventArgs e)
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
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.POS.formPayment));
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
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(components);
			textBoxTotal = new DevExpress.XtraEditors.TextEdit();
			label3 = new System.Windows.Forms.Label();
			textBoxPayment = new DevExpress.XtraEditors.TextEdit();
			label1 = new System.Windows.Forms.Label();
			textBoxChange = new DevExpress.XtraEditors.TextEdit();
			labelChangeTitle = new System.Windows.Forms.Label();
			buttonSave = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			textBoxBalance = new DevExpress.XtraEditors.TextEdit();
			label4 = new System.Windows.Forms.Label();
			numericKeypad1 = new Micromind.UISupport.NumericKeypad();
			groupBox1 = new System.Windows.Forms.GroupBox();
			button10AED = new DevExpress.XtraEditors.SimpleButton();
			button25FILS = new DevExpress.XtraEditors.SimpleButton();
			button1AED = new DevExpress.XtraEditors.SimpleButton();
			button50FILS = new DevExpress.XtraEditors.SimpleButton();
			button20AED = new DevExpress.XtraEditors.SimpleButton();
			button5AED = new DevExpress.XtraEditors.SimpleButton();
			groupBox2 = new System.Windows.Forms.GroupBox();
			button500AED = new DevExpress.XtraEditors.SimpleButton();
			button200AED = new DevExpress.XtraEditors.SimpleButton();
			button50AED = new DevExpress.XtraEditors.SimpleButton();
			button1000AED = new DevExpress.XtraEditors.SimpleButton();
			button100AED = new DevExpress.XtraEditors.SimpleButton();
			buttonActual = new DevExpress.XtraEditors.SimpleButton();
			dataGridPayment = new Micromind.DataControls.InvoiceEntryGrid();
			buttonVKeyboard = new DevExpress.XtraEditors.SimpleButton();
			labelshiftClose = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxPayment.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxChange.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxBalance.Properties).BeginInit();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayment).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			textBoxTotal.EditValue = "0.00";
			textBoxTotal.Location = new System.Drawing.Point(301, 12);
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18f, System.Drawing.FontStyle.Bold);
			textBoxTotal.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxTotal.Properties.Appearance.Options.UseFont = true;
			textBoxTotal.Properties.Appearance.Options.UseForeColor = true;
			textBoxTotal.Properties.Appearance.Options.UseTextOptions = true;
			textBoxTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxTotal.Properties.AutoHeight = false;
			textBoxTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxTotal.Properties.ReadOnly = true;
			textBoxTotal.Size = new System.Drawing.Size(224, 42);
			textBoxTotal.TabIndex = 28;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label3.Location = new System.Drawing.Point(147, 14);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(138, 33);
			label3.TabIndex = 29;
			label3.Text = "Total Due:";
			textBoxPayment.EditValue = "0.00";
			textBoxPayment.Location = new System.Drawing.Point(301, 324);
			textBoxPayment.Name = "textBoxPayment";
			textBoxPayment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			textBoxPayment.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxPayment.Properties.Appearance.Options.UseFont = true;
			textBoxPayment.Properties.Appearance.Options.UseForeColor = true;
			textBoxPayment.Properties.Appearance.Options.UseTextOptions = true;
			textBoxPayment.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxPayment.Properties.AutoHeight = false;
			textBoxPayment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxPayment.Properties.ReadOnly = true;
			textBoxPayment.Size = new System.Drawing.Size(224, 33);
			textBoxPayment.TabIndex = 28;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(115, 328);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(106, 24);
			label1.TabIndex = 29;
			label1.Text = "Payment:";
			textBoxChange.EditValue = "0.00";
			textBoxChange.Location = new System.Drawing.Point(301, 357);
			textBoxChange.Name = "textBoxChange";
			textBoxChange.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			textBoxChange.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxChange.Properties.Appearance.Options.UseFont = true;
			textBoxChange.Properties.Appearance.Options.UseForeColor = true;
			textBoxChange.Properties.Appearance.Options.UseTextOptions = true;
			textBoxChange.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxChange.Properties.AutoHeight = false;
			textBoxChange.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxChange.Properties.ReadOnly = true;
			textBoxChange.Size = new System.Drawing.Size(224, 33);
			textBoxChange.TabIndex = 28;
			labelChangeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelChangeTitle.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelChangeTitle.Location = new System.Drawing.Point(115, 358);
			labelChangeTitle.Name = "labelChangeTitle";
			labelChangeTitle.Size = new System.Drawing.Size(156, 24);
			labelChangeTitle.TabIndex = 29;
			labelChangeTitle.Text = "Change Cash:";
			buttonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonSave.Appearance.Options.UseFont = true;
			buttonSave.Enabled = false;
			buttonSave.Location = new System.Drawing.Point(411, 436);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(114, 56);
			buttonSave.TabIndex = 30;
			buttonSave.Text = "Pay && Save";
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(291, 436);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(114, 56);
			buttonCancel.TabIndex = 30;
			buttonCancel.Text = "Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxBalance.EditValue = "0.00";
			textBoxBalance.Location = new System.Drawing.Point(301, 390);
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			textBoxBalance.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
			textBoxBalance.Properties.Appearance.Options.UseFont = true;
			textBoxBalance.Properties.Appearance.Options.UseForeColor = true;
			textBoxBalance.Properties.Appearance.Options.UseTextOptions = true;
			textBoxBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxBalance.Properties.AutoHeight = false;
			textBoxBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxBalance.Properties.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(224, 33);
			textBoxBalance.TabIndex = 28;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label4.Location = new System.Drawing.Point(115, 392);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(106, 24);
			label4.TabIndex = 29;
			label4.Text = "Balance:";
			numericKeypad1.DisplayControl = null;
			numericKeypad1.Location = new System.Drawing.Point(531, 12);
			numericKeypad1.MaximumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.MinimumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.Name = "numericKeypad1";
			numericKeypad1.Size = new System.Drawing.Size(197, 244);
			numericKeypad1.TabIndex = 44;
			numericKeypad1.TabStop = false;
			groupBox1.Controls.Add(button10AED);
			groupBox1.Controls.Add(button25FILS);
			groupBox1.Controls.Add(button1AED);
			groupBox1.Controls.Add(button50FILS);
			groupBox1.Controls.Add(button20AED);
			groupBox1.Controls.Add(button5AED);
			groupBox1.Location = new System.Drawing.Point(541, 328);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(382, 164);
			groupBox1.TabIndex = 46;
			groupBox1.TabStop = false;
			button10AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button10AED.Appearance.Options.UseFont = true;
			button10AED.AutoWidthInLayoutControl = true;
			button10AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button10AED.Image = Micromind.ClientUI.Properties.Resources._10aed;
			button10AED.Location = new System.Drawing.Point(186, 13);
			button10AED.Name = "button10AED";
			button10AED.Size = new System.Drawing.Size(180, 56);
			button10AED.TabIndex = 52;
			button10AED.Text = "10";
			button10AED.Click += new System.EventHandler(button10AED_Click);
			button25FILS.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button25FILS.Appearance.Options.UseFont = true;
			button25FILS.AutoWidthInLayoutControl = true;
			button25FILS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button25FILS.Image = Micromind.ClientUI.Properties.Resources._25fils;
			button25FILS.Location = new System.Drawing.Point(302, 102);
			button25FILS.Name = "button25FILS";
			button25FILS.Size = new System.Drawing.Size(57, 50);
			button25FILS.TabIndex = 51;
			button25FILS.Text = ".25";
			button25FILS.Click += new System.EventHandler(button25FILS_Click);
			button1AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button1AED.Appearance.Options.UseFont = true;
			button1AED.AutoWidthInLayoutControl = true;
			button1AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button1AED.Image = (System.Drawing.Image)resources.GetObject("button1AED.Image");
			button1AED.Location = new System.Drawing.Point(176, 102);
			button1AED.Name = "button1AED";
			button1AED.Size = new System.Drawing.Size(57, 50);
			button1AED.TabIndex = 50;
			button1AED.Text = "1";
			button1AED.Click += new System.EventHandler(button1AED_Click);
			button50FILS.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button50FILS.Appearance.Options.UseFont = true;
			button50FILS.AutoWidthInLayoutControl = true;
			button50FILS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button50FILS.Image = Micromind.ClientUI.Properties.Resources._50fils;
			button50FILS.Location = new System.Drawing.Point(239, 102);
			button50FILS.Name = "button50FILS";
			button50FILS.Size = new System.Drawing.Size(57, 50);
			button50FILS.TabIndex = 49;
			button50FILS.Text = ".50";
			button50FILS.Click += new System.EventHandler(button50FILS_Click);
			button20AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button20AED.Appearance.Options.UseFont = true;
			button20AED.AutoWidthInLayoutControl = true;
			button20AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button20AED.Image = Micromind.ClientUI.Properties.Resources._20aed;
			button20AED.Location = new System.Drawing.Point(0, 13);
			button20AED.Name = "button20AED";
			button20AED.Size = new System.Drawing.Size(180, 56);
			button20AED.TabIndex = 54;
			button20AED.Text = "20";
			button20AED.Click += new System.EventHandler(button20AED_Click);
			button5AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button5AED.Appearance.Options.UseFont = true;
			button5AED.AutoWidthInLayoutControl = true;
			button5AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button5AED.Image = Micromind.ClientUI.Properties.Resources._5aed;
			button5AED.Location = new System.Drawing.Point(6, 75);
			button5AED.Name = "button5AED";
			button5AED.Size = new System.Drawing.Size(122, 56);
			button5AED.TabIndex = 48;
			button5AED.Text = "5";
			button5AED.Click += new System.EventHandler(button5AED_Click);
			groupBox2.Controls.Add(button500AED);
			groupBox2.Controls.Add(button200AED);
			groupBox2.Controls.Add(button50AED);
			groupBox2.Controls.Add(button1000AED);
			groupBox2.Controls.Add(button100AED);
			groupBox2.Location = new System.Drawing.Point(730, 0);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(193, 322);
			groupBox2.TabIndex = 47;
			groupBox2.TabStop = false;
			button500AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button500AED.Appearance.Options.UseFont = true;
			button500AED.AutoWidthInLayoutControl = true;
			button500AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button500AED.Image = Micromind.ClientUI.Properties.Resources._500aed;
			button500AED.Location = new System.Drawing.Point(7, 69);
			button500AED.Name = "button500AED";
			button500AED.Size = new System.Drawing.Size(180, 56);
			button500AED.TabIndex = 53;
			button500AED.Text = "500";
			button500AED.Click += new System.EventHandler(button500AED_Click);
			button200AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button200AED.Appearance.Options.UseFont = true;
			button200AED.AutoWidthInLayoutControl = true;
			button200AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button200AED.Image = Micromind.ClientUI.Properties.Resources._200aed;
			button200AED.Location = new System.Drawing.Point(7, 131);
			button200AED.Name = "button200AED";
			button200AED.Size = new System.Drawing.Size(180, 56);
			button200AED.TabIndex = 50;
			button200AED.Text = "200";
			button200AED.Click += new System.EventHandler(button200AED_Click);
			button50AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button50AED.Appearance.Options.UseFont = true;
			button50AED.AutoWidthInLayoutControl = true;
			button50AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button50AED.Image = Micromind.ClientUI.Properties.Resources._50aed;
			button50AED.Location = new System.Drawing.Point(8, 252);
			button50AED.Name = "button50AED";
			button50AED.Size = new System.Drawing.Size(180, 56);
			button50AED.TabIndex = 52;
			button50AED.Text = "50";
			button50AED.Click += new System.EventHandler(button50AED_Click);
			button1000AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button1000AED.Appearance.Options.UseFont = true;
			button1000AED.AutoWidthInLayoutControl = true;
			button1000AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button1000AED.Image = Micromind.ClientUI.Properties.Resources._1000aed;
			button1000AED.Location = new System.Drawing.Point(6, 11);
			button1000AED.Name = "button1000AED";
			button1000AED.Size = new System.Drawing.Size(180, 56);
			button1000AED.TabIndex = 49;
			button1000AED.Text = "1000";
			button1000AED.Click += new System.EventHandler(button1000AED_Click);
			button100AED.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			button100AED.Appearance.Options.UseFont = true;
			button100AED.AutoWidthInLayoutControl = true;
			button100AED.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			button100AED.Image = Micromind.ClientUI.Properties.Resources._100aed;
			button100AED.Location = new System.Drawing.Point(7, 190);
			button100AED.Name = "button100AED";
			button100AED.Size = new System.Drawing.Size(180, 56);
			button100AED.TabIndex = 51;
			button100AED.Text = "100";
			button100AED.Click += new System.EventHandler(button100AED_Click);
			buttonActual.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonActual.Appearance.Options.UseFont = true;
			buttonActual.Location = new System.Drawing.Point(577, 255);
			buttonActual.Name = "buttonActual";
			buttonActual.Size = new System.Drawing.Size(114, 39);
			buttonActual.TabIndex = 48;
			buttonActual.Text = "Exact";
			buttonActual.Click += new System.EventHandler(buttonActual_Click);
			dataGridPayment.AllowAddNew = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayment.DisplayLayout.Appearance = appearance;
			dataGridPayment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridPayment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridPayment.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayment.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayment.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridPayment.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPayment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayment.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridPayment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayment.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridPayment.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridPayment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridPayment.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridPayment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayment.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridPayment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayment.IncludeLotItems = false;
			dataGridPayment.LoadLayoutFailed = false;
			dataGridPayment.Location = new System.Drawing.Point(12, 60);
			dataGridPayment.Name = "dataGridPayment";
			dataGridPayment.ShowClearMenu = true;
			dataGridPayment.ShowDeleteMenu = true;
			dataGridPayment.ShowInsertMenu = true;
			dataGridPayment.ShowMoveRowsMenu = true;
			dataGridPayment.Size = new System.Drawing.Size(513, 265);
			dataGridPayment.TabIndex = 2;
			dataGridPayment.Text = "invoiceEntryGrid1";
			buttonVKeyboard.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonVKeyboard.Appearance.Options.UseFont = true;
			buttonVKeyboard.Image = (System.Drawing.Image)resources.GetObject("buttonVKeyboard.Image");
			buttonVKeyboard.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
			buttonVKeyboard.Location = new System.Drawing.Point(12, 451);
			buttonVKeyboard.Name = "buttonVKeyboard";
			buttonVKeyboard.Size = new System.Drawing.Size(53, 46);
			buttonVKeyboard.TabIndex = 45;
			buttonVKeyboard.Visible = false;
			buttonVKeyboard.Click += new System.EventHandler(buttonVKeyboard_Click);
			labelshiftClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelshiftClose.ForeColor = System.Drawing.Color.Red;
			labelshiftClose.Location = new System.Drawing.Point(542, 305);
			labelshiftClose.Name = "labelshiftClose";
			labelshiftClose.Size = new System.Drawing.Size(138, 17);
			labelshiftClose.TabIndex = 49;
			labelshiftClose.Text = "Shift closed";
			labelshiftClose.Visible = false;
			labelshiftClose.Click += new System.EventHandler(label2_Click);
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(928, 496);
			base.Controls.Add(labelshiftClose);
			base.Controls.Add(buttonActual);
			base.Controls.Add(groupBox2);
			base.Controls.Add(groupBox1);
			base.Controls.Add(buttonVKeyboard);
			base.Controls.Add(numericKeypad1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonSave);
			base.Controls.Add(label4);
			base.Controls.Add(labelChangeTitle);
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxBalance);
			base.Controls.Add(textBoxChange);
			base.Controls.Add(textBoxPayment);
			base.Controls.Add(textBoxTotal);
			base.Controls.Add(dataGridPayment);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPayment";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Payment";
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxPayment.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxChange.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxBalance.Properties).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPayment).EndInit();
			ResumeLayout(false);
		}
	}
}
