using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class ReportTemplatesUpdateForm : Form
	{
		private EntityDocData currentData;

		private const string TABLENAME_CONST = "EntityDocs";

		private const string IDFIELD_CONST = "EntityID";

		private bool isNewRecord = true;

		private bool isFileUploaded;

		private string entityID = "";

		private EntityTypesEnum entityType = EntityTypesEnum.Customers;

		private BackgroundWorker backgroundWorker = new BackgroundWorker();

		private IContainer components;

		private Line linePanelDown;

		private MMTextBox textFilePath;

		private MMLabel mmLabel1;

		private Button buttonUpload;

		private OpenFileDialog openFileDialog;

		private Label labelMsg;

		private ProgressBar progressBar;

		private MMSDateTimePicker dateTimePickerDate;

		private MMLabel mmLabel2;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string SourceTemplatePath
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

		public string DestinationTemplatePath => Application.StartupPath + Global.PrintTemplatePathFolder;

		public ReportTemplatesUpdateForm()
		{
			InitializeComponent();
			AddEvents();
			base.StartPosition = FormStartPosition.CenterParent;
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.ProgressChanged += backgrounWorker_ProgressChanged;
			backgroundWorker.RunWorkerCompleted += backgrounWorker_RunWorkerCompleted;
			backgroundWorker.DoWork += backgrounWorker_DoWork;
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				textFilePath.Text = Global.PrintTemplateServerPath;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void UploadFileDialog_Activated(object sender, EventArgs e)
		{
		}

		private bool ValidateData()
		{
			if (textFilePath.Text == string.Empty)
			{
				ErrorHelper.ErrorMessage("Please define server report templates path");
				return false;
			}
			return true;
		}

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			buttonUpload.Enabled = false;
			if (ValidateData())
			{
				progressBar.Style = ProgressBarStyle.Marquee;
				backgroundWorker.RunWorkerAsync();
			}
		}

		private void backgrounWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string sourceTemplatePath = SourceTemplatePath;
			string destinationTemplatePath = DestinationTemplatePath;
			int num = 0;
			_ = string.Empty;
			checked
			{
				try
				{
					if (Directory.Exists(SourceTemplatePath))
					{
						string[] directories = Directory.GetDirectories(sourceTemplatePath, "*", SearchOption.AllDirectories);
						foreach (string text in directories)
						{
							if (!Directory.Exists(text.Replace(sourceTemplatePath, destinationTemplatePath)))
							{
								Directory.CreateDirectory(text.Replace(sourceTemplatePath, destinationTemplatePath));
							}
						}
						directories = Directory.GetFiles(SourceTemplatePath, "*", SearchOption.AllDirectories);
						foreach (string obj in directories)
						{
							num++;
							string destFileName = obj.Replace(sourceTemplatePath, destinationTemplatePath);
							File.Copy(obj, destFileName, overwrite: true);
							if (progressBar.Value <= 100)
							{
								backgroundWorker.ReportProgress(num += 5);
							}
						}
					}
				}
				catch
				{
					ErrorHelper.ErrorMessage("Sorry! Report templates could not copied.");
					progressBar.Style = ProgressBarStyle.Blocks;
					progressBar.Value = 0;
					throw;
				}
			}
		}

		private void backgrounWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}

		private void backgrounWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			progressBar.Style = ProgressBarStyle.Blocks;
			progressBar.Value = 100;
			ErrorHelper.InformationMessage("Report templates successfully copied!");
			Close();
		}

		private void CopyAndReplaceAll()
		{
			string sourceTemplatePath = SourceTemplatePath;
			string destinationTemplatePath = DestinationTemplatePath;
			_ = string.Empty;
			try
			{
				if (Directory.Exists(SourceTemplatePath))
				{
					string[] directories = Directory.GetDirectories(sourceTemplatePath, "*", SearchOption.AllDirectories);
					foreach (string text in directories)
					{
						if (!Directory.Exists(text.Replace(sourceTemplatePath, destinationTemplatePath)))
						{
							Directory.CreateDirectory(text.Replace(sourceTemplatePath, destinationTemplatePath));
						}
					}
					directories = Directory.GetFiles(SourceTemplatePath, "*", SearchOption.AllDirectories);
					foreach (string obj in directories)
					{
						string destFileName = obj.Replace(sourceTemplatePath, destinationTemplatePath);
						File.Copy(obj, destFileName, overwrite: true);
					}
				}
				ErrorHelper.InformationMessage("Files successfully copied!");
			}
			catch (Exception)
			{
				ErrorHelper.ErrorMessage("Sorry files could not copied!");
				throw;
			}
		}

		private void buttonUpdatePrintTemplate_Click(object sender, EventArgs e)
		{
			PrintTemplateMap.CreateXml();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.ReportTemplatesUpdateForm));
			buttonUpload = new System.Windows.Forms.Button();
			textFilePath = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			labelMsg = new System.Windows.Forms.Label();
			progressBar = new System.Windows.Forms.ProgressBar();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel2 = new Micromind.UISupport.MMLabel();
			SuspendLayout();
			buttonUpload.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonUpload.Location = new System.Drawing.Point(323, 113);
			buttonUpload.Name = "buttonUpload";
			buttonUpload.Size = new System.Drawing.Size(105, 36);
			buttonUpload.TabIndex = 3;
			buttonUpload.Text = "Update";
			buttonUpload.UseVisualStyleBackColor = true;
			buttonUpload.Click += new System.EventHandler(buttonUpload_Click);
			textFilePath.BackColor = System.Drawing.Color.WhiteSmoke;
			textFilePath.CustomReportFieldName = "";
			textFilePath.CustomReportKey = "";
			textFilePath.CustomReportValueType = 1;
			textFilePath.IsComboTextBox = false;
			textFilePath.IsModified = false;
			textFilePath.Location = new System.Drawing.Point(120, 29);
			textFilePath.MaxLength = 1000;
			textFilePath.Name = "textFilePath";
			textFilePath.ReadOnly = true;
			textFilePath.Size = new System.Drawing.Size(305, 20);
			textFilePath.TabIndex = 6;
			textFilePath.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(14, 31);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(103, 13);
			mmLabel1.TabIndex = 11;
			mmLabel1.Text = "Print Template Path:";
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 108);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(444, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			labelMsg.AutoSize = true;
			labelMsg.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelMsg.ForeColor = System.Drawing.Color.Black;
			labelMsg.Location = new System.Drawing.Point(249, 18);
			labelMsg.Name = "labelMsg";
			labelMsg.Size = new System.Drawing.Size(0, 15);
			labelMsg.TabIndex = 16;
			progressBar.BackColor = System.Drawing.SystemColors.Control;
			progressBar.Location = new System.Drawing.Point(0, 0);
			progressBar.Name = "progressBar";
			progressBar.Size = new System.Drawing.Size(443, 10);
			progressBar.TabIndex = 17;
			dateTimePickerDate.Checked = false;
			dateTimePickerDate.CustomFormat = " ";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerDate.Location = new System.Drawing.Point(120, 53);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.ShowCheckBox = true;
			dateTimePickerDate.Size = new System.Drawing.Size(124, 20);
			dateTimePickerDate.TabIndex = 19;
			dateTimePickerDate.Value = new System.DateTime(0L);
			dateTimePickerDate.Visible = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(14, 56);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(74, 13);
			mmLabel2.TabIndex = 20;
			mmLabel2.Text = "Modified after:";
			mmLabel2.Visible = false;
			base.AcceptButton = buttonUpload;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(438, 152);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(progressBar);
			base.Controls.Add(labelMsg);
			base.Controls.Add(textFilePath);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(buttonUpload);
			base.Controls.Add(linePanelDown);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ReportTemplatesUpdateForm";
			Text = "Report Templates - Update";
			base.Activated += new System.EventHandler(UploadFileDialog_Activated);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
