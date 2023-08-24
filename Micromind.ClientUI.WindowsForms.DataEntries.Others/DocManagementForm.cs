using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class DocManagementForm : Form, IForm
	{
		private EntityDocData currentData;

		private string tempFilePath = "";

		private bool isLoading;

		private EditFileDialog uploadFileDialog = new EditFileDialog();

		private const string TABLENAME_CONST = "EntityDocs";

		private const string IDFIELD_CONST = "EntityID";

		private bool isNewRecord = true;

		private EntityTypesEnum entityType;

		private string entityTypeLabel = "";

		private string entityID = "";

		private string entityName = "";

		private int entityRowIndex = -1;

		private const string FILTER_CONST = "All Files (*.*)|*.*";

		private bool needDelete;

		private ArrayList tempFileList = new ArrayList();

		private bool IsAllowView = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private MMLabel lableEntityID;

		private MMTextBox textEntityID;

		private OpenFileDialog openFileDialog;

		private ImageList imageList;

		private FormManager formManager;

		private MMTextBox textEntityName;

		private DataGridList ultraGridDocs;

		private ToolStrip toolStripFile;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripAddButton;

		private ToolStripButton toolStripDeleteButton;

		private ToolStripButton toolStripOpenButton;

		private ToolStripButton toolStripEditButton;

		private ToolStripMenuItem contextMenuItemAddFile;

		private ToolStripMenuItem contextMenuItemEditFile;

		private ToolStripMenuItem contextMenuItemDeleteFile;

		private ToolStripMenuItem contextMenuItemOpenFile;

		private ContextMenuStrip contextMenuStrip;

		private ToolStripSeparator toolStripSeparator1;

		private SplitContainer splitContainer1;

		private Label label1;

		private ToolStripButton toolStripButtonPreviewPane;

		private Line line1;

		private UltraPictureBox pictureBoxPreview;

		private WebBrowser webBrowser;

		private ToolStripButton toolStripScanButton;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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

		public string EntityTypeLabel
		{
			get
			{
				return entityTypeLabel;
			}
			set
			{
				entityTypeLabel = value;
			}
		}

		private string EntityTypeName => EntityType.ToString().Substring(0, EntityType.ToString().LastIndexOf("s"));

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
				string text2 = entityID = (textEntityID.Text = value);
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

		public string EntityName
		{
			get
			{
				return entityName;
			}
			set
			{
				string text2 = entityName = (textEntityName.Text = value);
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		public DocManagementForm()
		{
			InitializeComponent();
			AddEvents();
			splitContainer1.Panel2Collapsed = true;
			Global.GlobalSettings.LoadFormProperties(this);
			toolStripButtonPreviewPane.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "PreviewPane", defaultValue: false);
		}

		private void AddEvents()
		{
			base.Load += DocManagementForm_Load;
			ultraGridDocs.AfterSelectChange += ultraGridDocs_AfterSelectChange;
			ultraGridDocs.DoubleClick += ultraGridDocs_DoubleClick;
			ultraGridDocs.AfterRowActivate += ultraGridDocs_AfterRowActivate;
			webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
			base.FormClosing += DocManagementForm_FormClosing;
		}

		private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (tempFilePath != "" && needDelete)
			{
				try
				{
					Application.DoEvents();
					Thread.Sleep(100);
					Application.DoEvents();
					File.Delete(tempFilePath);
					needDelete = false;
				}
				catch (Exception)
				{
					tempFileList.Add(tempFilePath);
				}
				tempFilePath = "";
			}
		}

		private void DocManagementForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			UserPreferences.SaveCurrentUserSetting(base.Name + "PreviewPane", toolStripButtonPreviewPane.Checked);
			Global.GlobalSettings.SaveFormProperties(this);
			foreach (string tempFile in tempFileList)
			{
				try
				{
					if (File.Exists(tempFile))
					{
						File.Delete(tempFile);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void ultraGridDocs_AfterRowActivate(object sender, EventArgs e)
		{
			try
			{
				if (IsAllowView && !isLoading)
				{
					if (tempFilePath != "")
					{
						needDelete = true;
						webBrowser.Url = null;
						webBrowser.Navigate("");
						Application.DoEvents();
					}
					needDelete = false;
					if (!toolStripButtonPreviewPane.Checked || ultraGridDocs.ActiveRow == null || !ultraGridDocs.ActiveRow.IsDataRow)
					{
						pictureBoxPreview.Visible = false;
					}
					else
					{
						string text = ultraGridDocs.ActiveRow.Cells["File Name"].Value.ToString();
						switch (Path.GetExtension(text).ToLower())
						{
						case "jpg":
						case "png":
						case "gif":
						case "jpeg":
						case "tif":
						case "bmp":
						{
							byte[] entityFile = Factory.EntityDocSystem.GetEntityFile(entityType, entityID, EntitySysDocID, text);
							if (entityFile.IsNullOrEmpty())
							{
								ErrorHelper.WarningMessage("File not found.");
							}
							else
							{
								MemoryStream memoryStream = new MemoryStream(entityFile);
								byte[] buffer = new byte[memoryStream.Length];
								memoryStream.Read(buffer, 0, checked((int)memoryStream.Length));
								bool flag = false;
								if (memoryStream != null)
								{
									DataSet entityDocByID = Factory.EntityDocSystem.GetEntityDocByID(entityType, entityID, text);
									flag = CheckFile(entityDocByID.Tables[0].Rows[0]["EntityDocPath"].ToString());
								}
								pictureBoxPreview.Image = null;
								if (flag)
								{
									pictureBoxPreview.Image = Image.FromStream(memoryStream);
									pictureBoxPreview.Visible = true;
									pictureBoxPreview.BringToFront();
									webBrowser.Visible = false;
								}
							}
							break;
						}
						default:
						{
							string urlString = SaveToDisk();
							webBrowser.Visible = true;
							webBrowser.BringToFront();
							webBrowser.Navigate(urlString);
							tempFilePath = urlString;
							break;
						}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				pictureBoxPreview.Visible = false;
			}
		}

		public bool CheckFile(string file)
		{
			bool flag = false;
			try
			{
				Image image = Image.FromFile(file);
				Graphics.FromImage(image);
				_ = image.RawFormat;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void ultraGridDocs_DoubleClick(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void ultraGridDocs_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
		{
			SetToolbarButtons();
		}

		private void SetToolbarButtons()
		{
			ToolStripButton toolStripButton = toolStripDeleteButton;
			ToolStripButton toolStripButton2 = toolStripEditButton;
			bool flag2 = toolStripOpenButton.Enabled = (ultraGridDocs.ActiveRow != null);
			bool enabled = toolStripButton2.Enabled = flag2;
			toolStripButton.Enabled = enabled;
		}

		private void ClearForm()
		{
			textEntityID.Clear();
			formManager.ResetDirty();
			textEntityID.Focus();
		}

		private void EntityDocGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void EntityDocGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
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

		private void DocManagementForm_Load(object sender, EventArgs e)
		{
			try
			{
				isLoading = true;
				if (EntityTypeLabel != "")
				{
					lableEntityID.Text = EntityTypeLabel;
				}
				else
				{
					lableEntityID.Text = EntityTypeName;
				}
				SetSecurity();
				ultraGridDocs.ApplyUIDesign();
				if (!base.IsDisposed)
				{
					LoadData();
					SetupGrid();
					ultraGridDocs.ActiveRow = null;
					ultraGridDocs.Selected.Rows.Clear();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				isLoading = false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				UltraGridColumn ultraGridColumn = ultraGridDocs.DisplayLayout.Bands[0].Columns["Entity ID"];
				UltraGridColumn ultraGridColumn2 = ultraGridDocs.DisplayLayout.Bands[0].Columns["File Name"];
				UltraGridColumn ultraGridColumn3 = ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"];
				Activation activation2 = ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn3.CellActivation = activation2;
				Activation activation7 = ultraGridColumn.CellActivation = (ultraGridColumn2.CellActivation = activation4);
				ultraGridDocs.DisplayLayout.Bands[0].Columns["File Name"].Width = 100;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Description"].Width = 150;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Keywords"].Width = 150;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["File Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["File Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				ultraGridDocs.DisplayLayout.Bands[0].Columns["Entity ID"].Hidden = true;
			}
			catch (Exception e)
			{
				ultraGridDocs.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (textEntityID.Text.Trim().Length == 0 || entityType.ToString().Length == 0)
			{
				ErrorHelper.InformationMessage("Required field can't be left empty");
				return false;
			}
			return true;
		}

		public void LoadData()
		{
			try
			{
				if (!(entityID == ""))
				{
					DataSet entityDocList = Factory.EntityDocSystem.GetEntityDocList(entityType, EntitySysDocID, entityID, entityRowIndex);
					ultraGridDocs.DataSource = entityDocList;
					SetToolbarButtons();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textEntityID.Text = dataRow["EntityID"].ToString();
			}
		}

		private bool GetData()
		{
			try
			{
				UltraGridRow activeRow = ultraGridDocs.ActiveRow;
				currentData = new EntityDocData();
				DataRow dataRow = (!isNewRecord) ? currentData.EntityDocTable.Rows[0] : currentData.EntityDocTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EntityID"] = activeRow.Cells["Entity ID"].Value.ToString();
				dataRow["EntityType"] = (int)entityType;
				dataRow["EntitySysDocID"] = EntitySysDocID;
				dataRow["EntityDocName"] = activeRow.Cells["File Name"].Value.ToString();
				dataRow["EntityDocDesc"] = activeRow.Cells["Description"].Value.ToString();
				dataRow["EntityDocKeyword"] = activeRow.Cells["Keywords"].Value.ToString();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EntityDocTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void toolStripAddButton_Click(object sender, EventArgs e)
		{
			AddFile();
		}

		private void toolStripDeleteButton_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				DeleteFile();
			}
		}

		private void toolStripEditButton_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				EditFile();
			}
		}

		private void toolStripOpenButton_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				OpenFile();
			}
		}

		private void toolStripFile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void contextMenuItemAddFile_Click(object sender, EventArgs e)
		{
			AddFile();
		}

		private void contextMenuItemEditFile_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				EditFile();
			}
		}

		private void contextMenuItemDeleteFile_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				DeleteFile();
			}
		}

		private void contextMenuItemOpenFile_Click(object sender, EventArgs e)
		{
			if (ultraGridDocs.ActiveRow != null)
			{
				OpenFile();
			}
		}

		private void AddFile()
		{
			string text = textEntityID.Text;
			entityType.ToString();
			if (new UploadFileDialog
			{
				EntityType = entityType,
				EntityID = text,
				EntitySysDocID = EntitySysDocID,
				EntityRowIndex = entityRowIndex
			}.ShowDialog(this) == DialogResult.OK)
			{
				LoadData();
				IsAllowView = false;
			}
		}

		private void EditFile()
		{
			string text = textEntityID.Text;
			EntityTypesEnum entityTypesEnum = entityType;
			ultraGridDocs.ActiveRow.Cells["File Name"].Value.ToString();
			EditFileDialog editFileDialog = new EditFileDialog();
			editFileDialog.EntityID = text;
			editFileDialog.EntityType = entityTypesEnum;
			if (ultraGridDocs.ActiveRow != null)
			{
				editFileDialog.FileName = ultraGridDocs.ActiveRow.Cells["File Name"].Value.ToString();
				editFileDialog.FileDesc = ultraGridDocs.ActiveRow.Cells["Description"].Value.ToString();
				editFileDialog.FileKeyword = ultraGridDocs.ActiveRow.Cells["Keywords"].Value.ToString();
				if (editFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					LoadData();
				}
			}
		}

		private void ScanFile()
		{
			string text = textEntityID.Text;
			entityType.ToString();
			ScanFileDetailForm scanFileDetailForm = new ScanFileDetailForm();
			scanFileDetailForm.EntityType = entityType;
			scanFileDetailForm.EntityID = text;
			scanFileDetailForm.EntitySysDocID = EntitySysDocID;
			scanFileDetailForm.EntityRowIndex = entityRowIndex;
			scanFileDetailForm.ShowDialog(this);
			LoadData();
		}

		private void DeleteFile()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete the file?") != DialogResult.No && GetData())
				{
					if (Factory.EntityDocSystem.DeleteEntityDoc(currentData))
					{
						LoadData();
						ErrorHelper.InformationMessage("File successfully deleted!");
						pictureBoxPreview.Visible = false;
					}
					else
					{
						ErrorHelper.ErrorMessage("File could not be deleted. Please try again later!");
					}
				}
			}
			catch (FieldAccessException)
			{
				ErrorHelper.WarningMessage("Cannot delete the file. File maybe in use or you do not have sufficient access.");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void OpenFileLast()
		{
		}

		private void OpenFile()
		{
			try
			{
				string text = SaveToDisk();
				if (!(text == ""))
				{
					tempFileList.Add(text);
					Process process = new Process();
					process.StartInfo = new ProcessStartInfo(text)
					{
						UseShellExecute = true,
						WindowStyle = ProcessWindowStyle.Normal
					};
					process.Start();
					needDelete = false;
				}
			}
			catch
			{
				throw;
			}
		}

		private string SaveToDisk()
		{
			try
			{
				if (ultraGridDocs.ActiveRow == null)
				{
					return "";
				}
				string text = ultraGridDocs.ActiveRow.Cells["File Name"].Value.ToString();
				byte[] entityFile = Factory.EntityDocSystem.GetEntityFile(entityType, entityID, EntitySysDocID, text);
				if (entityFile.IsNullOrEmpty())
				{
					ErrorHelper.WarningMessage("File not found.");
					return "";
				}
				MemoryStream memoryStream = new MemoryStream(entityFile);
				byte[] buffer = new byte[memoryStream.Length];
				memoryStream.Read(buffer, 0, checked((int)memoryStream.Length));
				if (memoryStream != null)
				{
					Factory.EntityDocSystem.GetEntityDocByID(entityType, entityID, text).Tables[0].Rows[0]["EntityDocPath"].ToString();
				}
				string tempFileName = Path.GetTempFileName();
				string text2 = Path.GetDirectoryName(tempFileName) + "\\" + Path.GetFileNameWithoutExtension(tempFileName) + Path.GetExtension(text);
				FileStream fileStream = new FileStream(text2, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				memoryStream.WriteTo(fileStream);
				fileStream.Close();
				return text2;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void toolStripButtonPreviewPane_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPreviewPane_CheckedChanged(object sender, EventArgs e)
		{
			if (toolStripButtonPreviewPane.Checked)
			{
				splitContainer1.Panel2Collapsed = false;
			}
			else
			{
				splitContainer1.Panel2Collapsed = true;
			}
		}

		public bool SaveEntityDoc(EntityDocData entityDocData, out string newpath)
		{
			bool result = false;
			string text = "";
			try
			{
				byte b = 1;
				string text2 = entityDocData.EntityDocTable.Rows[0]["EntityID"].ToString();
				string text3 = entityDocData.EntityDocTable.Rows[0]["EntitySysDocID"].ToString();
				EntityTypesEnum entityTypesEnum = (EntityTypesEnum)int.Parse(entityDocData.EntityDocTable.Rows[0]["EntityType"].ToString());
				text = Factory.DatabaseSystem.GetFieldValue("Company", "FileSavingPath", "CompanyID", b).ToString();
				if (text == "" || text == null)
				{
					string executablePath = Application.ExecutablePath;
					text = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				}
				string text4 = text + "\\NEW\\" + entityTypesEnum.ToString() + "\\" + text2;
				if (entityTypesEnum == EntityTypesEnum.Transactions)
				{
					text4 = text + "\\NEW\\" + entityTypesEnum.ToString() + "\\" + text3 + "\\" + text2;
				}
				string text5 = "";
				foreach (DataRow row in entityDocData.EntityDocTable.Rows)
				{
					string text6 = row["EntityDocName"].ToString();
					row["EntityDocDesc"].ToString();
					row["EntityDocKeyword"].ToString();
					string text7 = text6;
					string text8 = text4 + "\\" + text7;
					int num = 1;
					while (File.Exists(text8))
					{
						text7 = Path.GetFileNameWithoutExtension(text6) + "_" + num + Path.GetExtension(text6);
						text8 = text4 + "\\" + text7;
						row["EntityDocName"] = text7;
						num = checked(num + 1);
					}
					byte[] bytes = (byte[])row["FileData"];
					if (IsFileSavedToDisk(text4, text7, bytes))
					{
						row["EntityDocPath"] = text8;
						newpath = text8;
						text5 = text8;
					}
				}
				newpath = text5;
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private bool IsFileSavedToDisk(string docPath, string docName, byte[] bytes)
		{
			try
			{
				if (!Directory.Exists(docPath))
				{
					Directory.CreateDirectory(docPath);
				}
				File.WriteAllBytes(docPath + "\\" + docName, bytes);
				return true;
			}
			catch
			{
				throw;
			}
		}

		private void toolStripScanButton_Click(object sender, EventArgs e)
		{
			ScanFile();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.DocManagementForm));
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
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			imageList = new System.Windows.Forms.ImageList(components);
			toolStripFile = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripAddButton = new System.Windows.Forms.ToolStripButton();
			toolStripScanButton = new System.Windows.Forms.ToolStripButton();
			toolStripDeleteButton = new System.Windows.Forms.ToolStripButton();
			toolStripOpenButton = new System.Windows.Forms.ToolStripButton();
			toolStripEditButton = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreviewPane = new System.Windows.Forms.ToolStripButton();
			contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			contextMenuItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuItemEditFile = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			contextMenuItemDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridDocs = new Micromind.UISupport.DataGridList(components);
			formManager = new Micromind.DataControls.FormManager();
			textEntityName = new Micromind.UISupport.MMTextBox();
			textEntityID = new Micromind.UISupport.MMTextBox();
			lableEntityID = new Micromind.UISupport.MMLabel();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			webBrowser = new System.Windows.Forms.WebBrowser();
			pictureBoxPreview = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			line1 = new Micromind.UISupport.Line();
			label1 = new System.Windows.Forms.Label();
			toolStripFile.SuspendLayout();
			contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "preview.png");
			imageList.Images.SetKeyName(1, "void.png");
			toolStripFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStripFile.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				toolStripButtonPrint,
				toolStripAddButton,
				toolStripScanButton,
				toolStripDeleteButton,
				toolStripOpenButton,
				toolStripEditButton,
				toolStripButtonPreviewPane
			});
			toolStripFile.Location = new System.Drawing.Point(20, 0);
			toolStripFile.Name = "toolStripFile";
			toolStripFile.Size = new System.Drawing.Size(671, 31);
			toolStripFile.TabIndex = 0;
			toolStripFile.Text = "toolStrip1";
			toolStripFile.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStripFile_ItemClicked);
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripAddButton.Image = Micromind.ClientUI.Properties.Resources.newfile;
			toolStripAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripAddButton.Name = "toolStripAddButton";
			toolStripAddButton.Size = new System.Drawing.Size(78, 28);
			toolStripAddButton.Text = "Add File";
			toolStripAddButton.Click += new System.EventHandler(toolStripAddButton_Click);
			toolStripScanButton.Image = Micromind.ClientUI.Properties.Resources.scanner1;
			toolStripScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripScanButton.Name = "toolStripScanButton";
			toolStripScanButton.Size = new System.Drawing.Size(81, 28);
			toolStripScanButton.Text = "Scan File";
			toolStripScanButton.Click += new System.EventHandler(toolStripScanButton_Click);
			toolStripDeleteButton.Image = Micromind.ClientUI.Properties.Resources.Delete;
			toolStripDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDeleteButton.Name = "toolStripDeleteButton";
			toolStripDeleteButton.Size = new System.Drawing.Size(68, 28);
			toolStripDeleteButton.Text = "Delete";
			toolStripDeleteButton.Click += new System.EventHandler(toolStripDeleteButton_Click);
			toolStripOpenButton.Image = (System.Drawing.Image)resources.GetObject("toolStripOpenButton.Image");
			toolStripOpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripOpenButton.Name = "toolStripOpenButton";
			toolStripOpenButton.Size = new System.Drawing.Size(64, 28);
			toolStripOpenButton.Text = "Open";
			toolStripOpenButton.Click += new System.EventHandler(toolStripOpenButton_Click);
			toolStripEditButton.Image = Micromind.ClientUI.Properties.Resources.edit1;
			toolStripEditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripEditButton.Name = "toolStripEditButton";
			toolStripEditButton.Size = new System.Drawing.Size(55, 28);
			toolStripEditButton.Text = "Edit";
			toolStripEditButton.Click += new System.EventHandler(toolStripEditButton_Click);
			toolStripButtonPreviewPane.CheckOnClick = true;
			toolStripButtonPreviewPane.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreviewPane.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPreviewPane.Image");
			toolStripButtonPreviewPane.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreviewPane.Name = "toolStripButtonPreviewPane";
			toolStripButtonPreviewPane.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreviewPane.Text = "Show/Hide preview pane";
			toolStripButtonPreviewPane.CheckedChanged += new System.EventHandler(toolStripButtonPreviewPane_CheckedChanged);
			toolStripButtonPreviewPane.Click += new System.EventHandler(toolStripButtonPreviewPane_Click);
			contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				contextMenuItemAddFile,
				contextMenuItemEditFile,
				contextMenuItemOpenFile,
				toolStripSeparator1,
				contextMenuItemDeleteFile
			});
			contextMenuStrip.Name = "contextMenuStrip";
			contextMenuStrip.Size = new System.Drawing.Size(129, 98);
			contextMenuItemAddFile.Image = Micromind.ClientUI.Properties.Resources.newfile;
			contextMenuItemAddFile.Name = "contextMenuItemAddFile";
			contextMenuItemAddFile.Size = new System.Drawing.Size(128, 22);
			contextMenuItemAddFile.Text = "Add File...";
			contextMenuItemAddFile.Click += new System.EventHandler(contextMenuItemAddFile_Click);
			contextMenuItemEditFile.Image = Micromind.ClientUI.Properties.Resources.edit1;
			contextMenuItemEditFile.Name = "contextMenuItemEditFile";
			contextMenuItemEditFile.Size = new System.Drawing.Size(128, 22);
			contextMenuItemEditFile.Text = "Edit File...";
			contextMenuItemEditFile.Click += new System.EventHandler(contextMenuItemEditFile_Click);
			contextMenuItemOpenFile.Image = (System.Drawing.Image)resources.GetObject("contextMenuItemOpenFile.Image");
			contextMenuItemOpenFile.Name = "contextMenuItemOpenFile";
			contextMenuItemOpenFile.Size = new System.Drawing.Size(128, 22);
			contextMenuItemOpenFile.Text = "Open File";
			contextMenuItemOpenFile.Click += new System.EventHandler(contextMenuItemOpenFile_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
			contextMenuItemDeleteFile.Image = Micromind.ClientUI.Properties.Resources.Delete;
			contextMenuItemDeleteFile.Name = "contextMenuItemDeleteFile";
			contextMenuItemDeleteFile.Size = new System.Drawing.Size(128, 22);
			contextMenuItemDeleteFile.Text = "Delete File";
			contextMenuItemDeleteFile.Click += new System.EventHandler(contextMenuItemDeleteFile_Click);
			ultraGridDocs.AllowUnfittedView = false;
			ultraGridDocs.ContextMenuStrip = contextMenuStrip;
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
			ultraGridDocs.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraGridDocs.LoadLayoutFailed = false;
			ultraGridDocs.Location = new System.Drawing.Point(0, 0);
			ultraGridDocs.Name = "ultraGridDocs";
			ultraGridDocs.ShowDeleteMenu = false;
			ultraGridDocs.ShowMinusInRed = true;
			ultraGridDocs.ShowNewMenu = false;
			ultraGridDocs.Size = new System.Drawing.Size(223, 367);
			ultraGridDocs.TabIndex = 4;
			ultraGridDocs.Text = "dataGrid";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 5;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textEntityName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textEntityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textEntityName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textEntityName.CustomReportFieldName = "";
			textEntityName.CustomReportKey = "";
			textEntityName.CustomReportValueType = 1;
			textEntityName.IsComboTextBox = false;
			textEntityName.IsModified = false;
			textEntityName.Location = new System.Drawing.Point(254, 36);
			textEntityName.MaxLength = 15;
			textEntityName.Name = "textEntityName";
			textEntityName.ReadOnly = true;
			textEntityName.Size = new System.Drawing.Size(425, 20);
			textEntityName.TabIndex = 3;
			textEntityID.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textEntityID.BackColor = System.Drawing.Color.WhiteSmoke;
			textEntityID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textEntityID.CustomReportFieldName = "";
			textEntityID.CustomReportKey = "";
			textEntityID.CustomReportValueType = 1;
			textEntityID.IsComboTextBox = false;
			textEntityID.IsModified = false;
			textEntityID.Location = new System.Drawing.Point(86, 36);
			textEntityID.MaxLength = 15;
			textEntityID.Name = "textEntityID";
			textEntityID.ReadOnly = true;
			textEntityID.Size = new System.Drawing.Size(163, 20);
			textEntityID.TabIndex = 2;
			lableEntityID.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			lableEntityID.AutoSize = true;
			lableEntityID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lableEntityID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			lableEntityID.IsFieldHeader = false;
			lableEntityID.IsRequired = false;
			lableEntityID.Location = new System.Drawing.Point(16, 39);
			lableEntityID.Name = "lableEntityID";
			lableEntityID.PenWidth = 1f;
			lableEntityID.ShowBorder = false;
			lableEntityID.Size = new System.Drawing.Size(0, 13);
			lableEntityID.TabIndex = 1;
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.BackColor = System.Drawing.Color.White;
			splitContainer1.Location = new System.Drawing.Point(12, 62);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(ultraGridDocs);
			splitContainer1.Panel2.Controls.Add(webBrowser);
			splitContainer1.Panel2.Controls.Add(pictureBoxPreview);
			splitContainer1.Panel2.Controls.Add(line1);
			splitContainer1.Panel2.Controls.Add(label1);
			splitContainer1.Size = new System.Drawing.Size(670, 367);
			splitContainer1.SplitterDistance = 223;
			splitContainer1.TabIndex = 6;
			webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			webBrowser.Location = new System.Drawing.Point(1, 0);
			webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser.Name = "webBrowser";
			webBrowser.Size = new System.Drawing.Size(442, 367);
			webBrowser.TabIndex = 4;
			webBrowser.Visible = false;
			pictureBoxPreview.BorderShadowColor = System.Drawing.Color.Empty;
			pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			pictureBoxPreview.Location = new System.Drawing.Point(1, 0);
			pictureBoxPreview.Name = "pictureBoxPreview";
			pictureBoxPreview.Size = new System.Drawing.Size(442, 367);
			pictureBoxPreview.TabIndex = 3;
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Left;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 0);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 367);
			line1.TabIndex = 2;
			line1.TabStop = false;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label1.Location = new System.Drawing.Point(81, 112);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(273, 125);
			label1.TabIndex = 1;
			label1.Text = "No Preview Available";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(691, 438);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(toolStripFile);
			base.Controls.Add(formManager);
			base.Controls.Add(textEntityName);
			base.Controls.Add(textEntityID);
			base.Controls.Add(lableEntityID);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DocManagementForm";
			Text = "File Attachments";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(DocManagementForm_Load);
			toolStripFile.ResumeLayout(false);
			toolStripFile.PerformLayout();
			contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGridDocs).EndInit();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
