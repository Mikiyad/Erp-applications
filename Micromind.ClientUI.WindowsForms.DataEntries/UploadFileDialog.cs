using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class UploadFileDialog : Form
	{
		private const string FILTER_CONST = "Image Files (*.bmp, *.jpg, *.jpeg, *.png, *.tif, *.tiff) | *.bmp; *.jpg; *.jpeg; *.png; *.tif; *.tiff |MS Office Files (*.doc, *.docx, *.xls, *.xlsx) | *.doc; *.docx; *.xls; *.xlsx |Text Files (*.txt, *.csv) | *.txt; *.csv |All files (*.*) | *.*";

		private const string FILTER_CONST1 = "Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp;*.tiff;*.doc; *.docx; *.xls; *.xlsx;*.txt; *.csv;*.pdf; |All files (*.*) | *.*";

		private EntityDocData currentData;

		private const string TABLENAME_CONST = "EntityDocs";

		private const string IDFIELD_CONST = "EntityID";

		private bool isNewRecord = true;

		private bool isFileUploaded;

		private string entityID = "";

		private int entityRowIndex = -1;

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private IContainer components;

		private Button buttonCancel;

		private Line linePanelDown;

		private XPButton buttonOpenFileDialog;

		private MMTextBox textFilePath;

		private MMLabel mmLabel1;

		private Button buttonUpload;

		private MMTextBox textFileKeyword;

		private MMLabel mmLabel4;

		private MMLabel mmLabel3;

		private MMTextBox textFileDesc;

		private OpenFileDialog openFileDialog;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageSingle;

		private UltraTabPageControl tabPageMultiple;

		private Button buttonSelectMultiple;

		private Label label1;

		private DataGridList ultraGridDocs;

		private RadioButton radioButtonSingle;

		private RadioButton radioButtonMultiple;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem deleteToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string FilePath
		{
			get
			{
				return textFilePath.Text;
			}
			set
			{
				textFilePath.Text = value;
			}
		}

		public string FileDesc
		{
			get
			{
				return textFileDesc.Text;
			}
			set
			{
				textFileDesc.Text = value;
			}
		}

		public string FileKeyword
		{
			get
			{
				return textFileKeyword.Text;
			}
			set
			{
				textFileKeyword.Text = value;
			}
		}

		public string FileName
		{
			get;
			set;
		}

		public string EntitySysDocID
		{
			get;
			set;
		}

		public string EntityID
		{
			get
			{
				return entityID;
			}
			set
			{
				entityID = value;
			}
		}

		public int EntityRowIndex
		{
			get
			{
				return entityRowIndex;
			}
			set
			{
				entityRowIndex = value;
			}
		}

		public EntityTypesEnum EntityType
		{
			get
			{
				return entityType;
			}
			set
			{
				entityType = value;
			}
		}

		public bool IsFileUploaded
		{
			get
			{
				return isFileUploaded;
			}
			set
			{
				isFileUploaded = value;
			}
		}

		public UploadFileDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
			ultraGridDocs.DragDrop += ultraGridDocs_DragDrop;
			ultraGridDocs.DragEnter += ultraGridDocs_DragEnter;
			ultraGridDocs.ShowDeleteMenu = true;
			radioButtonMultiple.Enabled = true;
			base.FormClosing += UploadFileDialog_FormClosing;
			radioButtonMultiple.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "Multiple", defaultValue: false);
		}

		private void UploadFileDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			UserPreferences.SaveCurrentUserSetting(base.Name + "Multiple", radioButtonMultiple.Checked);
		}

		private void ultraGridDocs_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.All;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void ultraGridDocs_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			DataTable dataTable = ultraGridDocs.DataSource as DataTable;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					string path = (string)(dataRow["Path"] = array[i]);
					dataRow["Name"] = Path.GetFileName(path);
					int num = 1;
					while (dataTable.Select("Name = '" + dataRow["Name"].ToString() + "'").Length != 0)
					{
						dataRow["Name"] = Path.GetFileNameWithoutExtension(path) + "_" + num + Path.GetExtension(path);
						num++;
					}
					dataTable.Rows.Add(dataRow);
				}
				if (ultraGridDocs.Rows.Count > 0)
				{
					buttonUpload.Enabled = true;
				}
				else
				{
					buttonUpload.Enabled = false;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private bool ValidateData()
		{
			if (radioButtonSingle.Checked)
			{
				if (!File.Exists(textFilePath.Text))
				{
					ErrorHelper.WarningMessage("The file name you have specified does not exist.", "Please specify a valid file path.");
					return false;
				}
				string text = textFilePath.Text;
				string fileName = GetFileName(text);
				if (Array.Exists(fileName.Split('.'), (string element) => element.ToLower() == "txt"))
				{
					ErrorHelper.ErrorMessage("Text File can't Upload. Please select another file");
					return false;
				}
				if (isNewRecord && Factory.EntityDocSystem.IsFileExist(entityType, EntityID, fileName))
				{
					ErrorHelper.ErrorMessage("This file already exist. Please select another file");
					return false;
				}
			}
			else
			{
				foreach (UltraGridRow row in ultraGridDocs.Rows)
				{
					string text2 = row.Cells["Path"].Value.ToString();
					string text3 = row.Cells["Name"].Value.ToString();
					if (!File.Exists(text2))
					{
						ErrorHelper.WarningMessage("Could not find the file '" + text2 + "'");
						return false;
					}
					if (Factory.EntityDocSystem.IsFileExist(entityType, EntityID, text3))
					{
						ErrorHelper.WarningMessage("File name '" + text3 + "' already exists.");
						return false;
					}
				}
			}
			return true;
		}

		private void UploadFileDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp;*.tiff;*.doc; *.docx; *.xls; *.xlsx;*.txt; *.csv;*.pdf; |All files (*.*) | *.*";
			openFileDialog.DefaultExt = "txt";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (!new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage("Invalid file. Please try again later!");
					buttonUpload.Enabled = false;
				}
				else
				{
					textFilePath.Text = openFileDialog.FileName;
					buttonUpload.Enabled = true;
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private bool SaveData()
		{
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				return Factory.EntityDocSystem.SaveEntityDoc(currentData);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetData()
		{
			try
			{
				currentData = new EntityDocData();
				if (radioButtonSingle.Checked)
				{
					DataRow dataRow = currentData.EntityDocTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EntityID"] = EntityID;
					dataRow["RowIndex"] = EntityRowIndex;
					dataRow["EntityType"] = EntityType;
					if (EntitySysDocID != "")
					{
						dataRow["EntitySysDocID"] = EntitySysDocID;
					}
					string text = (string)(dataRow["EntityDocName"] = (FileName = GetFileName(FilePath)));
					text = (string)(dataRow["EntityDocDesc"] = (FileDesc = textFileDesc.Text.Trim()));
					text = (string)(dataRow["EntityDocKeyword"] = (FileKeyword = textFileKeyword.Text.Trim()));
					byte[] array = (byte[])(dataRow["FileData"] = GetInputFile(""));
					dataRow.EndEdit();
					currentData.EntityDocTable.Rows.Add(dataRow);
				}
				else
				{
					foreach (UltraGridRow row in ultraGridDocs.Rows)
					{
						DataRow dataRow = currentData.EntityDocTable.NewRow();
						dataRow.BeginEdit();
						dataRow["EntityID"] = EntityID;
						dataRow["RowIndex"] = EntityRowIndex;
						dataRow["EntityType"] = EntityType;
						if (EntitySysDocID != "")
						{
							dataRow["EntitySysDocID"] = EntitySysDocID;
						}
						string text = (string)(dataRow["EntityDocName"] = (FileName = row.Cells["Name"].Value.ToString()));
						text = (string)(dataRow["EntityDocDesc"] = (FileDesc = row.Cells["Description"].Value.ToString()));
						text = (string)(dataRow["EntityDocKeyword"] = (FileKeyword = row.Cells["Keywords"].Value.ToString()));
						byte[] array2 = (byte[])(dataRow["FileData"] = GetInputFile(row.Cells["Path"].Value.ToString()));
						dataRow.EndEdit();
						currentData.EntityDocTable.Rows.Add(dataRow);
					}
				}
				return true;
			}
			catch (FileNotFoundException ex)
			{
				ErrorHelper.WarningMessage("Could not find the file: " + ex.FileName);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private byte[] GetInputFile(string filePath)
		{
			byte[] result = null;
			if (filePath == "")
			{
				filePath = FilePath;
			}
			string extension = Path.GetExtension(Path.GetFileName(filePath));
			string a = string.Empty;
			extension = extension.ToLower();
			try
			{
				switch (extension)
				{
				case ".doc":
					a = "application/vnd.ms-word";
					break;
				case ".docx":
					a = "application/vnd.ms-word";
					break;
				case ".xls":
					a = "application/vnd.ms-excel";
					break;
				case ".xlsx":
					a = "application/vnd.ms-excel";
					break;
				case ".jpg":
				case ".jpeg":
					a = "image/jpg";
					break;
				case ".png":
					a = "image/png";
					break;
				case ".gif":
					a = "image/gif";
					break;
				case ".pdf":
					a = "application/pdf";
					break;
				case ".tif":
					a = "image/tif";
					break;
				case ".bmp":
					a = "image/bmp";
					break;
				case ".dwg":
					a = "application/autocad_dwg";
					break;
				}
				if (a != string.Empty)
				{
					FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
					return new BinaryReader(fileStream).ReadBytes(checked((int)fileStream.Length));
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string GetFileName(string filePath)
		{
			if (filePath != string.Empty)
			{
				return filePath.Substring(checked(FilePath.LastIndexOf("\\") + 1));
			}
			return string.Empty;
		}

		private void UploadFileDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				ultraGridDocs.ApplyUIDesign();
				ultraGridDocs.AllowDrop = true;
				DataTable dataTable = new DataSet().Tables.Add("Files");
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
				ultraGridDocs.ContextMenuStrip = contextMenuStrip1;
			}
			catch (Exception e)
			{
				ultraGridDocs.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.bmp;*.tiff;*.doc; *.docx; *.xls; *.xlsx;*.txt; *.csv;*.pdf; |All files (*.*) | *.*";
			openFileDialog.Multiselect = true;
			openFileDialog.DefaultExt = "jpg";
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
				buttonUpload.Enabled = true;
			}
		}

		private void UploadFileDialog_Load(object sender, EventArgs e)
		{
			SetupGrid();
			if (EntityType == EntityTypesEnum.ExternalReports)
			{
				radioButtonMultiple.Visible = false;
			}
			else
			{
				radioButtonMultiple.Visible = true;
			}
		}

		private void radioButtonSingle_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonSingle.Checked)
			{
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs["Single"];
			}
			else
			{
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs["Multiple"];
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.UploadFileDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonUpload = new System.Windows.Forms.Button();
			textFilePath = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textFileKeyword = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textFileDesc = new Micromind.UISupport.MMTextBox();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			tabPageSingle = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPageMultiple = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGridDocs = new Micromind.UISupport.DataGridList(components);
			label1 = new System.Windows.Forms.Label();
			buttonSelectMultiple = new System.Windows.Forms.Button();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			radioButtonMultiple = new System.Windows.Forms.RadioButton();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			tabPageSingle.SuspendLayout();
			tabPageMultiple.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).BeginInit();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(406, 313);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonUpload.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonUpload.Enabled = false;
			buttonUpload.Location = new System.Drawing.Point(303, 312);
			buttonUpload.Name = "buttonUpload";
			buttonUpload.Size = new System.Drawing.Size(97, 25);
			buttonUpload.TabIndex = 3;
			buttonUpload.Text = "Upload";
			buttonUpload.UseVisualStyleBackColor = true;
			buttonUpload.Click += new System.EventHandler(buttonUpload_Click);
			textFilePath.BackColor = System.Drawing.Color.WhiteSmoke;
			textFilePath.CustomReportFieldName = "";
			textFilePath.CustomReportKey = "";
			textFilePath.CustomReportValueType = 1;
			textFilePath.IsComboTextBox = false;
			textFilePath.Location = new System.Drawing.Point(101, 13);
			textFilePath.MaxLength = 1000;
			textFilePath.Name = "textFilePath";
			textFilePath.ReadOnly = true;
			textFilePath.Size = new System.Drawing.Size(305, 20);
			textFilePath.TabIndex = 6;
			textFilePath.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(3, 16);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(59, 13);
			mmLabel1.TabIndex = 11;
			mmLabel1.Text = "Select File:";
			textFileKeyword.BackColor = System.Drawing.Color.White;
			textFileKeyword.CustomReportFieldName = "";
			textFileKeyword.CustomReportKey = "";
			textFileKeyword.CustomReportValueType = 1;
			textFileKeyword.IsComboTextBox = false;
			textFileKeyword.Location = new System.Drawing.Point(101, 62);
			textFileKeyword.MaxLength = 255;
			textFileKeyword.Name = "textFileKeyword";
			textFileKeyword.Size = new System.Drawing.Size(336, 20);
			textFileKeyword.TabIndex = 2;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(3, 65);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 24;
			mmLabel4.Text = "Search Keywords:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(3, 42);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(63, 13);
			mmLabel3.TabIndex = 24;
			mmLabel3.Text = "Description:";
			textFileDesc.BackColor = System.Drawing.Color.White;
			textFileDesc.CustomReportFieldName = "";
			textFileDesc.CustomReportKey = "";
			textFileDesc.CustomReportValueType = 1;
			textFileDesc.IsComboTextBox = false;
			textFileDesc.Location = new System.Drawing.Point(101, 39);
			textFileDesc.MaxLength = 255;
			textFileDesc.Name = "textFileDesc";
			textFileDesc.Size = new System.Drawing.Size(336, 20);
			textFileDesc.TabIndex = 1;
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(412, 13);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog.TabIndex = 0;
			buttonOpenFileDialog.Text = "...";
			buttonOpenFileDialog.UseVisualStyleBackColor = false;
			buttonOpenFileDialog.Click += new System.EventHandler(buttonOpenFileDialog_Click);
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 300);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(507, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageSingle);
			ultraTabControl1.Controls.Add(tabPageMultiple);
			ultraTabControl1.Location = new System.Drawing.Point(3, 31);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.False;
			ultraTabControl1.Size = new System.Drawing.Size(496, 260);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
			ultraTabControl1.TabIndex = 25;
			ultraTab.Key = "Single";
			ultraTab.TabPage = tabPageSingle;
			ultraTab.Text = "Single File";
			ultraTab2.Key = "Multiple";
			ultraTab2.TabPage = tabPageMultiple;
			ultraTab2.Text = "Multiple Files";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(496, 260);
			tabPageSingle.Controls.Add(mmLabel1);
			tabPageSingle.Controls.Add(buttonOpenFileDialog);
			tabPageSingle.Controls.Add(mmLabel4);
			tabPageSingle.Controls.Add(textFileDesc);
			tabPageSingle.Controls.Add(textFileKeyword);
			tabPageSingle.Controls.Add(textFilePath);
			tabPageSingle.Controls.Add(mmLabel3);
			tabPageSingle.Location = new System.Drawing.Point(-10000, -10000);
			tabPageSingle.Name = "tabPageSingle";
			tabPageSingle.Size = new System.Drawing.Size(496, 260);
			tabPageMultiple.Controls.Add(buttonSelectMultiple);
			tabPageMultiple.Controls.Add(label1);
			tabPageMultiple.Controls.Add(ultraGridDocs);
			tabPageMultiple.Location = new System.Drawing.Point(0, 0);
			tabPageMultiple.Name = "tabPageMultiple";
			tabPageMultiple.Size = new System.Drawing.Size(496, 260);
			ultraGridDocs.AllowDrop = true;
			ultraGridDocs.AllowUnfittedView = false;
			ultraGridDocs.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ultraGridDocs.DisplayLayout.Appearance = appearance;
			ultraGridDocs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
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
			ultraGridDocs.Location = new System.Drawing.Point(9, 30);
			ultraGridDocs.Name = "ultraGridDocs";
			ultraGridDocs.ShowDeleteMenu = false;
			ultraGridDocs.ShowMinusInRed = true;
			ultraGridDocs.ShowNewMenu = false;
			ultraGridDocs.Size = new System.Drawing.Size(480, 226);
			ultraGridDocs.TabIndex = 5;
			ultraGridDocs.Text = "dataGrid";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Location = new System.Drawing.Point(43, 13);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(251, 13);
			label1.TabIndex = 6;
			label1.Text = "Select multiple files or drag and drop them to the list.";
			buttonSelectMultiple.Location = new System.Drawing.Point(9, 8);
			buttonSelectMultiple.Name = "buttonSelectMultiple";
			buttonSelectMultiple.Size = new System.Drawing.Size(31, 22);
			buttonSelectMultiple.TabIndex = 7;
			buttonSelectMultiple.Text = "...";
			buttonSelectMultiple.UseVisualStyleBackColor = true;
			buttonSelectMultiple.Click += new System.EventHandler(button1_Click);
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Checked = true;
			radioButtonSingle.Location = new System.Drawing.Point(14, 8);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(73, 17);
			radioButtonSingle.TabIndex = 26;
			radioButtonSingle.Text = "Single File";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtonSingle_CheckedChanged);
			radioButtonMultiple.AutoSize = true;
			radioButtonMultiple.Location = new System.Drawing.Point(103, 8);
			radioButtonMultiple.Name = "radioButtonMultiple";
			radioButtonMultiple.Size = new System.Drawing.Size(85, 17);
			radioButtonMultiple.TabIndex = 26;
			radioButtonMultiple.Text = "Multiple Files";
			radioButtonMultiple.UseVisualStyleBackColor = true;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				deleteToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			deleteToolStripMenuItem.Text = "Delete...";
			base.AcceptButton = buttonUpload;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(501, 344);
			base.Controls.Add(radioButtonMultiple);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(buttonUpload);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(buttonCancel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UploadFileDialog";
			Text = "Add File";
			base.Activated += new System.EventHandler(UploadFileDialog_Activated);
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(UploadFileDialog_FormClosed);
			base.Load += new System.EventHandler(UploadFileDialog_Load);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			tabPageSingle.ResumeLayout(false);
			tabPageSingle.PerformLayout();
			tabPageMultiple.ResumeLayout(false);
			tabPageMultiple.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
