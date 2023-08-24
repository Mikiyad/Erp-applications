using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class AttachementDetailsForm : Form, IForm
	{
		private SystemDocumentData currentData;

		private bool isNewRecord = true;

		private bool isAccountsDirty;

		public SysDocTypes sysDocType = SysDocTypes.None;

		public string formtype = "";

		public string voucherid = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private DataGridList ultraGridDocs;

		private Button buttonSelectMultiple;

		private Label label1;

		private OpenFileDialog openFileDialog;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem deleteToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus() | isAccountsDirty;

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
			}
		}

		public string SysDocID
		{
			get
			{
				return textBoxCode.Text;
			}
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string VoucherID
		{
			get
			{
				return voucherid;
			}
			set
			{
				voucherid = value;
			}
		}

		public string FormType
		{
			get
			{
				return formtype;
			}
			set
			{
				formtype = value;
			}
		}

		public SysDocTypes SysDocType
		{
			get
			{
				return sysDocType;
			}
			set
			{
				sysDocType = value;
			}
		}

		public AttachementDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += LocationDetailsForm_Load;
		}

		private void comboBoxGainLossAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new SystemDocumentData();
				}
				foreach (UltraGridRow row in ultraGridDocs.Rows)
				{
					DataRow dataRow = currentData.SystemDocDetailTable.NewRow();
					dataRow.BeginEdit();
					dataRow["SysDocID"] = SysDocID;
					dataRow["SysDocType"] = sysDocType;
					dataRow["RowIndex"] = -1;
					dataRow["PrintTemplate"] = row.Cells["Name"].Value.ToString();
					dataRow["TemplateDescription"] = row.Cells["Description"].Value.ToString();
					dataRow["TemplateKeyword"] = row.Cells["Keywords"].Value.ToString();
					dataRow.EndEdit();
					currentData.SystemDocDetailTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				Close();
			}
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					DataSet docAttachements = Factory.SystemDocumentSystem.GetDocAttachements(textBoxCode.Text, SysDocType);
					DataTable dataTable = ultraGridDocs.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in docAttachements.Tables["System_Doc_Detail"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						textBoxCode.Text = row["SysDocID"].ToString();
						SysDocType = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), row["SysDocType"].ToString());
						dataRow2["Name"] = row["PrintTemplate"];
						dataRow2["Description"] = row["TemplateDescription"];
						dataRow2["Keywords"] = row["TemplateKeyword"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.SystemDocDetailTable.Rows.Count != 0)
			{
				DataRow dataRow = currentData.SystemDocDetailTable.Rows[0];
				textBoxCode.Text = dataRow["LocationID"].ToString();
				sysDocType = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), dataRow["SysDocType"].ToString());
			}
		}

		private bool SaveData()
		{
			try
			{
				if (!ValidateData())
				{
					return false;
				}
				if (!GetData())
				{
					return false;
				}
				DataTable systemDocDetailTable = currentData.SystemDocDetailTable;
				DataSet dataSet = new DataSet();
				dataSet.Merge(systemDocDetailTable);
				return Factory.SystemDocumentSystem.InsertDocAttachementDetail(dataSet, textBoxCode.Text, sysDocType);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (textBoxCode.Text == "")
			{
				ErrorHelper.InformationMessage("Please select sysDoc ");
				return false;
			}
			for (int i = 0; i < ultraGridDocs.Rows.Count; i = checked(i + 1))
			{
				if (ultraGridDocs.Rows[i].Cells["Description"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please mention Format Name for file selected.");
					ultraGridDocs.Rows[i].Activate();
					return false;
				}
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			if (ultraGridDocs.DisplayLayout.Bands[0].Columns.Exists("Name"))
			{
				text2 = ultraGridDocs.ActiveRow.Cells["Name"].Value.ToString();
			}
			string[] array = text2.Split('.');
			text = textBoxCode.Text;
			string voucherID = voucherid;
			FormHelper formHelper = new FormHelper();
			new DocumentViewForm();
			formHelper.GetTransactionPreviewDoc(text, voucherID, array[0], isPrint: true);
		}

		private void ClearForm()
		{
			isAccountsDirty = false;
			textBoxCode.Clear();
			(ultraGridDocs.DataSource as DataTable).Rows.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void LocationGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LocationGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			if (ultraGridDocs.DisplayLayout.Bands[0].Columns.Exists("Name"))
			{
				text2 = ultraGridDocs.ActiveRow.Cells["Name"].Value.ToString();
			}
			string[] array = text2.Split('.');
			text = textBoxCode.Text;
			string voucherID = voucherid;
			FormHelper formHelper = new FormHelper();
			new DocumentViewForm();
			formHelper.GetTransactionPreviewDoc(text, voucherID, array[0], isPrint: false);
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				return Factory.LocationSystem.DeleteLocation(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void LocationDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetupGrid();
				LoadData(SysDocID);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Location);
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void buttonSelectMultiple_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Multiselect = true;
			openFileDialog.DefaultExt = "*.repx";
			string printTemplatePath = PrintHelper.PrintTemplatePath;
			openFileDialog.InitialDirectory = printTemplatePath + "\\Documents";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				DataTable dataTable = ultraGridDocs.DataSource as DataTable;
				for (int i = 0; i < openFileDialog.FileNames.Length; i = checked(i + 1))
				{
					DataRow dataRow = dataTable.NewRow();
					FileInfo fileInfo = new FileInfo(openFileDialog.FileNames[i]);
					dataRow["Path"] = fileInfo.FullName;
					dataRow["Name"] = fileInfo.Name;
					dataTable.Rows.Add(dataRow);
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				ultraGridDocs.ApplyUIDesign();
				ultraGridDocs.AllowDrop = true;
				DataSet dataSet = new DataSet();
				if (FormType == "sysDoc")
				{
					DataTable dataTable = dataSet.Tables.Add("Files");
					dataTable.Columns.Add("Name");
					dataTable.Columns.Add("Path");
					dataTable.Columns.Add("Description");
					dataTable.Columns.Add("Keywords");
					ultraGridDocs.DataSource = dataTable;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Path"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].CellActivation = Activation.AllowEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].CellClickAction = CellClickAction.Edit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].CellClickAction = CellClickAction.Edit;
					int num3 = ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].MaxLength = (ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].MaxLength = 255);
					ultraGridDocs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
					ultraGridDocs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Path"].Hidden = true;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].Hidden = true;
					ultraGridDocs.ContextMenuStrip = contextMenuStrip1;
					XPButton xPButton = buttonDelete;
					bool visible = buttonNew.Visible = false;
					xPButton.Visible = visible;
					buttonSave.Visible = true;
					buttonSelectMultiple.Visible = true;
					xpButton1.Visible = false;
					Text = "Add More Templates";
					label1.Visible = true;
				}
				else if (FormType == "Transaction")
				{
					DataTable dataTable2 = dataSet.Tables.Add("Files");
					dataTable2.Columns.Add("Name");
					dataTable2.Columns.Add("Path");
					dataTable2.Columns.Add("Description");
					dataTable2.Columns.Add("Keywords");
					ultraGridDocs.DataSource = dataTable2;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Path"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].CellActivation = Activation.NoEdit;
					int num3 = ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].MaxLength = (ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].MaxLength = 255);
					ultraGridDocs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
					ultraGridDocs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Path"].Hidden = true;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].Hidden = true;
					ultraGridDocs.DisplayLayout.Bands[0].Columns["Name"].Hidden = true;
					ultraGridDocs.ContextMenuStrip = contextMenuStrip1;
					XPButton xPButton2 = buttonDelete;
					bool visible = buttonNew.Visible = true;
					xPButton2.Visible = visible;
					buttonSave.Visible = false;
					buttonSelectMultiple.Visible = false;
					Text = "Other Print Templates";
					label1.Visible = false;
					buttonDelete.Text = "Preview";
					buttonNew.Text = "Print";
				}
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Name"].Header.Caption = "File Name";
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].Header.Caption = "Print Format Name";
			}
			catch (Exception e)
			{
				ultraGridDocs.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void UploadFileDialog_Load(object sender, EventArgs e)
		{
			SetupGrid();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UltraGridRow activeRow = ultraGridDocs.ActiveRow;
			if (activeRow != null)
			{
				try
				{
					activeRow.Delete();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error occured during deleting the row.\n" + ex.Message, "Error deleting row", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("Please select a single row to delete.");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.AttachementDetailsForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelCode = new Micromind.UISupport.MMLabel();
			ultraGridDocs = new Micromind.UISupport.DataGridList(components);
			buttonSelectMultiple = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).BeginInit();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(502, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 22);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 224);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(535, 40);
			panelButtons.TabIndex = 7;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(535, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(213, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "Preview";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(424, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(115, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Print";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Visible = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(318, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(87, 151);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			checkBoxInactive.Visible = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(116, 9);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(69, 20);
			textBoxCode.TabIndex = 0;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(23, 13);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(91, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Sys Doc Code:";
			ultraGridDocs.AllowDrop = true;
			ultraGridDocs.AllowUnfittedView = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ultraGridDocs.DisplayLayout.Appearance = appearance;
			ultraGridDocs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4;
			ultraGridDocs.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridDocs.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ultraGridDocs.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridDocs.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ultraGridDocs.DisplayLayout.MaxColScrollRegions = 1;
			ultraGridDocs.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridDocs.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ultraGridDocs.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ultraGridDocs.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ultraGridDocs.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ultraGridDocs.DisplayLayout.Override.CellAppearance = appearance8;
			ultraGridDocs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ultraGridDocs.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridDocs.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ultraGridDocs.DisplayLayout.Override.HeaderAppearance = appearance10;
			ultraGridDocs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ultraGridDocs.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ultraGridDocs.DisplayLayout.Override.RowAppearance = appearance11;
			ultraGridDocs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ultraGridDocs.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ultraGridDocs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ultraGridDocs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ultraGridDocs.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ultraGridDocs.LoadLayoutFailed = false;
			ultraGridDocs.Location = new System.Drawing.Point(6, 37);
			ultraGridDocs.Name = "ultraGridDocs";
			ultraGridDocs.ShowDeleteMenu = false;
			ultraGridDocs.ShowMinusInRed = true;
			ultraGridDocs.ShowNewMenu = false;
			ultraGridDocs.Size = new System.Drawing.Size(515, 172);
			ultraGridDocs.TabIndex = 26;
			ultraGridDocs.Text = "dataGrid";
			buttonSelectMultiple.Location = new System.Drawing.Point(192, 8);
			buttonSelectMultiple.Name = "buttonSelectMultiple";
			buttonSelectMultiple.Size = new System.Drawing.Size(31, 22);
			buttonSelectMultiple.TabIndex = 28;
			buttonSelectMultiple.Text = "...";
			buttonSelectMultiple.UseVisualStyleBackColor = true;
			buttonSelectMultiple.Click += new System.EventHandler(buttonSelectMultiple_Click);
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Location = new System.Drawing.Point(226, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(251, 13);
			label1.TabIndex = 27;
			label1.Text = "Select multiple files or drag and drop them to the list.";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				deleteToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			deleteToolStripMenuItem.Text = "Delete...";
			deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(535, 264);
			base.Controls.Add(buttonSelectMultiple);
			base.Controls.Add(label1);
			base.Controls.Add(ultraGridDocs);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			MinimumSize = new System.Drawing.Size(543, 291);
			base.Name = "AttachementDetailsForm";
			Text = "Add More Templates";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
