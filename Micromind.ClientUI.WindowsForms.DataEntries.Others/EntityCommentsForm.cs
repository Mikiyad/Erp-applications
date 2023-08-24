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
	public class EntityCommentsForm : Form, IForm
	{
		private EntityDocData currentData;

		private string tempFilePath = "";

		private bool isLoading;

		private string entitySysDocID = "";

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

		private ScreenAccessRight screenRight;

		private IContainer components;

		private MMTextBox textEntityID;

		private OpenFileDialog openFileDialog;

		private ImageList imageList;

		private FormManager formManager;

		private MMTextBox textEntityName;

		private ToolStrip toolStripFile;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripAddButton;

		private ToolStripButton toolStripDeleteButton;

		private ToolStripButton toolStripEditButton;

		private ToolStripMenuItem contextMenuItemAddFile;

		private ToolStripMenuItem contextMenuItemEditFile;

		private ToolStripMenuItem contextMenuItemDeleteFile;

		private ToolStripMenuItem contextMenuItemOpenFile;

		private ContextMenuStrip contextMenuStrip;

		private ToolStripSeparator toolStripSeparator1;

		private MMLabel lableEntityID;

		private EntityCommentsControl entityCommentsControl;

		private Label label1;

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
				entityCommentsControl.EntityType = value;
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
			get
			{
				return entitySysDocID;
			}
			set
			{
				entitySysDocID = value;
				entityCommentsControl.EntitySysDocID = value;
			}
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
				entityCommentsControl.EntityID = value;
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

		public EntityCommentsForm()
		{
			InitializeComponent();
			AddEvents();
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void AddEvents()
		{
			entityCommentsControl.Grid.AfterSelectChange += entityCommentsControl_AfterSelectChange;
			entityCommentsControl.Grid.DoubleClick += entityCommentsControl_DoubleClick;
			entityCommentsControl.Grid.AfterRowActivate += entityCommentsControl_AfterRowActivate;
			base.FormClosing += EntityCommentsForm_FormClosing;
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

		private void EntityCommentsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void entityCommentsControl_AfterRowActivate(object sender, EventArgs e)
		{
			try
			{
				_ = isLoading;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void entityCommentsControl_DoubleClick(object sender, EventArgs e)
		{
		}

		private void entityCommentsControl_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
		{
			SetToolbarButtons();
		}

		private void SetToolbarButtons()
		{
			ToolStripButton toolStripButton = toolStripDeleteButton;
			bool enabled = toolStripEditButton.Enabled = (entityCommentsControl.Grid.ActiveRow != null);
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

		private void EntityCommentsForm_Load(object sender, EventArgs e)
		{
			try
			{
				isLoading = true;
				SetSecurity();
				if (EntityTypeLabel != "")
				{
					lableEntityID.Text = EntityTypeLabel;
				}
				else
				{
					lableEntityID.Text = EntityTypeName;
				}
				SetSecurity();
				if (!base.IsDisposed)
				{
					SetupGrid();
					entityCommentsControl.Grid.SetupGrid();
					LoadData();
					entityCommentsControl.Grid.ActiveRow = null;
					entityCommentsControl.Grid.Selected.Rows.Clear();
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
				entityCommentsControl.LoadData(EntityType, EntityID, EntitySysDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
		}

		private void toolStripAddButton_Click(object sender, EventArgs e)
		{
			AddFile();
		}

		private void toolStripDeleteButton_Click(object sender, EventArgs e)
		{
			entityCommentsControl.Delete();
		}

		private void toolStripEditButton_Click(object sender, EventArgs e)
		{
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				entityCommentsControl.Edit();
			}
		}

		private void toolStripOpenButton_Click(object sender, EventArgs e)
		{
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				EditComment();
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
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				EditFile();
			}
		}

		private void contextMenuItemDeleteFile_Click(object sender, EventArgs e)
		{
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				DeleteComment();
			}
		}

		private void contextMenuItemEditComment_Click(object sender, EventArgs e)
		{
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				EditComment();
			}
		}

		private void AddFile()
		{
			_ = textEntityID.Text;
			entityType.ToString();
			entityCommentsControl.NewComment();
		}

		private void EditFile()
		{
			string text = textEntityID.Text;
			EntityTypesEnum entityTypesEnum = entityType;
			entityCommentsControl.Grid.ActiveRow.Cells["File Name"].Value.ToString();
			EditFileDialog editFileDialog = new EditFileDialog();
			editFileDialog.EntityID = text;
			editFileDialog.EntityType = entityTypesEnum;
			if (entityCommentsControl.Grid.ActiveRow != null)
			{
				editFileDialog.FileName = entityCommentsControl.Grid.ActiveRow.Cells["File Name"].Value.ToString();
				editFileDialog.FileDesc = entityCommentsControl.Grid.ActiveRow.Cells["Description"].Value.ToString();
				editFileDialog.FileKeyword = entityCommentsControl.Grid.ActiveRow.Cells["Keywords"].Value.ToString();
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

		private void DeleteComment()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete the file?") != DialogResult.No)
				{
					if (Factory.EntityDocSystem.DeleteEntityDoc(currentData))
					{
						LoadData();
						ErrorHelper.InformationMessage("File successfully deleted!");
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

		private void EditCommentLast()
		{
		}

		private void EditComment()
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
				if (entityCommentsControl.Grid.ActiveRow == null)
				{
					return "";
				}
				string text = entityCommentsControl.Grid.ActiveRow.Cells["File Name"].Value.ToString();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.EntityCommentsForm));
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			imageList = new System.Windows.Forms.ImageList(components);
			toolStripFile = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripAddButton = new System.Windows.Forms.ToolStripButton();
			toolStripDeleteButton = new System.Windows.Forms.ToolStripButton();
			toolStripEditButton = new System.Windows.Forms.ToolStripButton();
			contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			contextMenuItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuItemEditFile = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			contextMenuItemDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			entityCommentsControl = new Micromind.DataControls.EntityCommentsControl();
			textEntityName = new Micromind.UISupport.MMTextBox();
			textEntityID = new Micromind.UISupport.MMTextBox();
			lableEntityID = new Micromind.UISupport.MMLabel();
			label1 = new System.Windows.Forms.Label();
			toolStripFile.SuspendLayout();
			contextMenuStrip.SuspendLayout();
			SuspendLayout();
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "preview.png");
			imageList.Images.SetKeyName(1, "void.png");
			toolStripFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				toolStripButtonPrint,
				toolStripAddButton,
				toolStripDeleteButton,
				toolStripEditButton
			});
			toolStripFile.Location = new System.Drawing.Point(20, 0);
			toolStripFile.Name = "toolStripFile";
			toolStripFile.Size = new System.Drawing.Size(671, 25);
			toolStripFile.TabIndex = 0;
			toolStripFile.Text = "toolStrip1";
			toolStripFile.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStripFile_ItemClicked);
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripAddButton.Image = Micromind.ClientUI.Properties.Resources.newfile;
			toolStripAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripAddButton.Name = "toolStripAddButton";
			toolStripAddButton.Size = new System.Drawing.Size(108, 22);
			toolStripAddButton.Text = "New Comment";
			toolStripAddButton.Click += new System.EventHandler(toolStripAddButton_Click);
			toolStripDeleteButton.Image = Micromind.ClientUI.Properties.Resources.Delete;
			toolStripDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDeleteButton.Name = "toolStripDeleteButton";
			toolStripDeleteButton.Size = new System.Drawing.Size(60, 22);
			toolStripDeleteButton.Text = "Delete";
			toolStripDeleteButton.Click += new System.EventHandler(toolStripDeleteButton_Click);
			toolStripEditButton.Image = Micromind.ClientUI.Properties.Resources.edit1;
			toolStripEditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripEditButton.Name = "toolStripEditButton";
			toolStripEditButton.Size = new System.Drawing.Size(47, 22);
			toolStripEditButton.Text = "Edit";
			toolStripEditButton.Click += new System.EventHandler(toolStripEditButton_Click);
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
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
			contextMenuItemDeleteFile.Image = Micromind.ClientUI.Properties.Resources.Delete;
			contextMenuItemDeleteFile.Name = "contextMenuItemDeleteFile";
			contextMenuItemDeleteFile.Size = new System.Drawing.Size(128, 22);
			contextMenuItemDeleteFile.Text = "Delete File";
			contextMenuItemDeleteFile.Click += new System.EventHandler(contextMenuItemDeleteFile_Click);
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
			entityCommentsControl.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			entityCommentsControl.EntityID = null;
			entityCommentsControl.EntitySysDocID = null;
			entityCommentsControl.EntityType = Micromind.Common.Data.EntityTypesEnum.None;
			entityCommentsControl.Location = new System.Drawing.Point(12, 73);
			entityCommentsControl.Name = "entityCommentsControl";
			entityCommentsControl.Size = new System.Drawing.Size(667, 353);
			entityCommentsControl.TabIndex = 6;
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
			textEntityID.Location = new System.Drawing.Point(92, 36);
			textEntityID.MaxLength = 15;
			textEntityID.Name = "textEntityID";
			textEntityID.ReadOnly = true;
			textEntityID.Size = new System.Drawing.Size(160, 20);
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
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(17, 39);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 7;
			label1.Text = "Comment for:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(691, 438);
			base.Controls.Add(label1);
			base.Controls.Add(entityCommentsControl);
			base.Controls.Add(toolStripFile);
			base.Controls.Add(formManager);
			base.Controls.Add(textEntityName);
			base.Controls.Add(textEntityID);
			base.Controls.Add(lableEntityID);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EntityCommentsForm";
			Text = "Comments";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(EntityCommentsForm_Load);
			toolStripFile.ResumeLayout(false);
			toolStripFile.PerformLayout();
			contextMenuStrip.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
