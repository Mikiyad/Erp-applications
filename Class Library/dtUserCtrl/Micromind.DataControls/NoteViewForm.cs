using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class NoteViewForm : Form
	{
		public bool IsNewNote;

		private Note note1;

		private PictureBox pictureBox1;

		private Panel panel2;

		private Panel panel3;

		private Panel panelHeader;

		private PictureBox pictureBoxResize;

		private PictureBox pictureBoxClose;

		private Panel panelMain;

		private ContextMenu contextMenu;

		private MenuItem menuItemMinimize;

		private MenuItem menuItemSetFlag;

		private MenuItem menuItemFlagToDo;

		private MenuItem menuItemFlagImportant;

		private MenuItem menuItemFlagQuestion;

		private MenuItem menuItemFlagReminder;

		private MenuItem menuItem2;

		private MenuItem menuItemTitleTodayDate;

		private MenuItem menuItemTitleTodayDateTime;

		private MenuItem menuItemTitleFirstLineFromNote;

		private MenuItem menuItem5;

		private MenuItem menuItemExportToMSOutlook;

		private MenuItem menuItem3;

		private MenuItem menuItemSetReminder;

		private MenuItem menuItemUsers;

		private MenuItem menuItem8;

		private MenuItem menuItemNoteIsInactive;

		private MenuItem menuItem7;

		private MenuItem menuItemDelete;

		private MenuItem menuItemSaveAs;

		private MenuItem menuItem1;

		private MenuItem menuItemBlue;

		private MenuItem menuItemGreen;

		private MenuItem menuItemPink;

		private MenuItem menuItemYellow;

		private MenuItem menuItemWhite;

		private SaveFileDialog saveFileDialog;

		private Container components;

		private Point mouseDownPoint;

		private int lastX;

		private int lastY;

		private int MaxNoteSize = 50000;

		private bool isFirstTime = true;

		private bool isDeleted;

		public string NoteText => note1.PlainText;

		public Color NoteColor
		{
			get
			{
				return note1.BackColor;
			}
			set
			{
				note1.BackColor = value;
			}
		}

		public int NoteID
		{
			get
			{
				return note1.NoteID;
			}
			set
			{
				note1.NoteID = value;
			}
		}

		public event EventHandler OnDeleteNote;

		public NoteViewForm()
		{
			InitializeComponent();
			BackColor = note1.BackColor;
			note1.Resizeable = false;
			note1.Scrollbar = RichTextBoxScrollBars.None;
			note1.BackColorChanged += note1_BackColorChanged;
			note1.NoteColorChanged += note1_NoteColorChanged;
			note1.OnSaveNote += note1_OnSaveNote;
			note1.NoteCreator = Global.CurrentUserID;
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.DataControls.NoteViewForm));
			note1 = new Micromind.DataControls.Note();
			pictureBoxClose = new System.Windows.Forms.PictureBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			panelHeader = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			panel3 = new System.Windows.Forms.Panel();
			pictureBoxResize = new System.Windows.Forms.PictureBox();
			panelMain = new System.Windows.Forms.Panel();
			contextMenu = new System.Windows.Forms.ContextMenu();
			menuItemSetFlag = new System.Windows.Forms.MenuItem();
			menuItemFlagToDo = new System.Windows.Forms.MenuItem();
			menuItemFlagImportant = new System.Windows.Forms.MenuItem();
			menuItemFlagQuestion = new System.Windows.Forms.MenuItem();
			menuItemFlagReminder = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemTitleTodayDate = new System.Windows.Forms.MenuItem();
			menuItemTitleTodayDateTime = new System.Windows.Forms.MenuItem();
			menuItemTitleFirstLineFromNote = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemBlue = new System.Windows.Forms.MenuItem();
			menuItemGreen = new System.Windows.Forms.MenuItem();
			menuItemPink = new System.Windows.Forms.MenuItem();
			menuItemYellow = new System.Windows.Forms.MenuItem();
			menuItemWhite = new System.Windows.Forms.MenuItem();
			menuItem5 = new System.Windows.Forms.MenuItem();
			menuItemSaveAs = new System.Windows.Forms.MenuItem();
			menuItemExportToMSOutlook = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemSetReminder = new System.Windows.Forms.MenuItem();
			menuItemUsers = new System.Windows.Forms.MenuItem();
			menuItem8 = new System.Windows.Forms.MenuItem();
			menuItemNoteIsInactive = new System.Windows.Forms.MenuItem();
			menuItem7 = new System.Windows.Forms.MenuItem();
			menuItemDelete = new System.Windows.Forms.MenuItem();
			menuItemMinimize = new System.Windows.Forms.MenuItem();
			saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			panelMain.SuspendLayout();
			SuspendLayout();
			note1.Cursor = System.Windows.Forms.Cursors.Default;
			note1.DateTimeStamp = new System.DateTime(0L);
			note1.Dock = System.Windows.Forms.DockStyle.Fill;
			note1.IsDirty = true;
			note1.IsInactive = false;
			note1.IsLocationColorTitleReminderDateDirty = true;
			note1.IsReadOnly = false;
			note1.Location = new System.Drawing.Point(0, 16);
			note1.Name = "note1";
			note1.NoteBackColor = System.Drawing.Color.FromArgb(255, 255, 204);
			note1.NoteColor = "255,255,204";
			note1.NoteCreator = -1;
			note1.NoteFlag = Micromind.Common.Data.NoteFlags.ToDo;
			note1.NoteID = -1;
			note1.PlainText = "";
			note1.ReminderDate = new System.DateTime(0L);
			note1.Resizeable = true;
			note1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Tahoma;}}\r\n{\\*\\generator Riched20 5.40.11.2210;}\\viewkind4\\uc1\\pard\\f0\\fs23\\par\r\n}\r\n\0";
			note1.Scrollbar = System.Windows.Forms.RichTextBoxScrollBars.Both;
			note1.ShowBorder = false;
			note1.ShowHeader = false;
			note1.Size = new System.Drawing.Size(342, 206);
			note1.TabIndex = 4;
			note1.TitleText = "";
			pictureBoxClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			pictureBoxClose.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxClose.Image");
			pictureBoxClose.Location = new System.Drawing.Point(328, 2);
			pictureBoxClose.Name = "pictureBoxClose";
			pictureBoxClose.Size = new System.Drawing.Size(10, 10);
			pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBoxClose.TabIndex = 5;
			pictureBoxClose.TabStop = false;
			pictureBoxClose.Click += new System.EventHandler(pictureBoxClose_Click);
			pictureBox1.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(14, 14);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBox1.TabIndex = 6;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
			panelHeader.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
			panelHeader.Location = new System.Drawing.Point(21, 0);
			panelHeader.Name = "panelHeader";
			panelHeader.Size = new System.Drawing.Size(305, 14);
			panelHeader.TabIndex = 3;
			panelHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseUp);
			panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseMove);
			panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseDown);
			panel2.Controls.Add(pictureBox1);
			panel2.Controls.Add(panelHeader);
			panel2.Controls.Add(pictureBoxClose);
			panel2.Dock = System.Windows.Forms.DockStyle.Top;
			panel2.Location = new System.Drawing.Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(342, 16);
			panel2.TabIndex = 7;
			panel3.Controls.Add(pictureBoxResize);
			panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel3.Location = new System.Drawing.Point(0, 222);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(342, 16);
			panel3.TabIndex = 9;
			panel3.Paint += new System.Windows.Forms.PaintEventHandler(panel3_Paint);
			pictureBoxResize.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			pictureBoxResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			pictureBoxResize.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxResize.Image");
			pictureBoxResize.Location = new System.Drawing.Point(326, 0);
			pictureBoxResize.Name = "pictureBoxResize";
			pictureBoxResize.Size = new System.Drawing.Size(18, 16);
			pictureBoxResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBoxResize.TabIndex = 9;
			pictureBoxResize.TabStop = false;
			pictureBoxResize.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBoxResize_MouseMove);
			pictureBoxResize.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBoxResize_MouseDown);
			panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelMain.Controls.Add(note1);
			panelMain.Controls.Add(panel2);
			panelMain.Controls.Add(panel3);
			panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			panelMain.Location = new System.Drawing.Point(0, 0);
			panelMain.Name = "panelMain";
			panelMain.Size = new System.Drawing.Size(344, 240);
			panelMain.TabIndex = 10;
			contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[14]
			{
				menuItemSetFlag,
				menuItem2,
				menuItem1,
				menuItem5,
				menuItemSaveAs,
				menuItemExportToMSOutlook,
				menuItem3,
				menuItemSetReminder,
				menuItemUsers,
				menuItem8,
				menuItemNoteIsInactive,
				menuItem7,
				menuItemDelete,
				menuItemMinimize
			});
			contextMenu.Popup += new System.EventHandler(contextMenu_Popup);
			menuItemSetFlag.Index = 0;
			menuItemSetFlag.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4]
			{
				menuItemFlagToDo,
				menuItemFlagImportant,
				menuItemFlagQuestion,
				menuItemFlagReminder
			});
			menuItemSetFlag.Text = "Flag";
			menuItemFlagToDo.Index = 0;
			menuItemFlagToDo.Text = "To Do";
			menuItemFlagToDo.Click += new System.EventHandler(menuItemFlagToDo_Click);
			menuItemFlagImportant.Index = 1;
			menuItemFlagImportant.Text = "Important";
			menuItemFlagImportant.Click += new System.EventHandler(menuItemFlagImportant_Click);
			menuItemFlagQuestion.Index = 2;
			menuItemFlagQuestion.Text = "Question";
			menuItemFlagQuestion.Click += new System.EventHandler(menuItemFlagQuestion_Click);
			menuItemFlagReminder.Index = 3;
			menuItemFlagReminder.Text = "Reminder for Later";
			menuItemFlagReminder.Click += new System.EventHandler(menuItemFlagReminder_Click);
			menuItem2.Index = 1;
			menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[3]
			{
				menuItemTitleTodayDate,
				menuItemTitleTodayDateTime,
				menuItemTitleFirstLineFromNote
			});
			menuItem2.Text = "Title";
			menuItemTitleTodayDate.Index = 0;
			menuItemTitleTodayDate.Text = "Set to today's date";
			menuItemTitleTodayDate.Click += new System.EventHandler(menuItemTitleTodayDate_Click);
			menuItemTitleTodayDateTime.Index = 1;
			menuItemTitleTodayDateTime.Text = "Set to today's date and time";
			menuItemTitleTodayDateTime.Click += new System.EventHandler(menuItemTitleTodayDateTime_Click);
			menuItemTitleFirstLineFromNote.Index = 2;
			menuItemTitleFirstLineFromNote.Text = "Use first line from note";
			menuItemTitleFirstLineFromNote.Click += new System.EventHandler(menuItemTitleFirstLineFromNote_Click);
			menuItem1.Index = 2;
			menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[5]
			{
				menuItemBlue,
				menuItemGreen,
				menuItemPink,
				menuItemYellow,
				menuItemWhite
			});
			menuItem1.Text = "Color";
			menuItemBlue.Index = 0;
			menuItemBlue.Text = "Blue";
			menuItemBlue.Click += new System.EventHandler(menuItemBlue_Click);
			menuItemGreen.Index = 1;
			menuItemGreen.Text = "Green";
			menuItemGreen.Click += new System.EventHandler(menuItemGreen_Click);
			menuItemPink.Index = 2;
			menuItemPink.Text = "Pink";
			menuItemPink.Click += new System.EventHandler(menuItemPink_Click);
			menuItemYellow.Index = 3;
			menuItemYellow.Text = "Yellow";
			menuItemYellow.Click += new System.EventHandler(menuItemYellow_Click);
			menuItemWhite.Index = 4;
			menuItemWhite.Text = "White";
			menuItemWhite.Click += new System.EventHandler(menuItemWhite_Click);
			menuItem5.Index = 3;
			menuItem5.Text = "-";
			menuItemSaveAs.Index = 4;
			menuItemSaveAs.Text = "Save As...";
			menuItemSaveAs.Click += new System.EventHandler(menuItemSaveAs_Click);
			menuItemExportToMSOutlook.Index = 5;
			menuItemExportToMSOutlook.Text = "Export to MS Outlook";
			menuItem3.Index = 6;
			menuItem3.Text = "-";
			menuItemSetReminder.Index = 7;
			menuItemSetReminder.Text = "Set Alarm...";
			menuItemSetReminder.Click += new System.EventHandler(menuItemSetReminder_Click);
			menuItemUsers.Index = 8;
			menuItemUsers.Text = "Assign Users...";
			menuItemUsers.Click += new System.EventHandler(menuItemUsers_Click);
			menuItem8.Index = 9;
			menuItem8.Text = "-";
			menuItemNoteIsInactive.Index = 10;
			menuItemNoteIsInactive.Text = "Note is Inactive";
			menuItemNoteIsInactive.Click += new System.EventHandler(menuItemNoteIsInactive_Click);
			menuItem7.Index = 11;
			menuItem7.Text = "-";
			menuItemDelete.Index = 12;
			menuItemDelete.Text = "Delete";
			menuItemDelete.Click += new System.EventHandler(menuItemDelete_Click);
			menuItemMinimize.Index = 13;
			menuItemMinimize.Text = "Close";
			menuItemMinimize.Click += new System.EventHandler(menuItemMinimize_Click);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.LightGoldenrodYellow;
			base.ClientSize = new System.Drawing.Size(344, 240);
			base.Controls.Add(panelMain);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "NoteViewForm";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Note";
			base.Load += new System.EventHandler(NoteViewForm_Load);
			base.Closed += new System.EventHandler(NoteViewForm_Closed);
			base.Activated += new System.EventHandler(NoteViewForm_Activated);
			base.Paint += new System.Windows.Forms.PaintEventHandler(NoteViewForm_Paint);
			base.Enter += new System.EventHandler(NoteViewForm_Enter);
			base.Deactivate += new System.EventHandler(NoteViewForm_Deactivate);
			panel2.ResumeLayout(false);
			panel3.ResumeLayout(false);
			panelMain.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void panelHeader_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int num = base.DesktopLocation.X + e.X - mouseDownPoint.X;
				int num2 = base.DesktopLocation.Y + e.Y - mouseDownPoint.Y;
				lastX = num;
				lastY = num2;
				SetDesktopLocation(lastX, lastY);
			}
		}

		private void panelHeader_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseDownPoint = new Point(e.X, e.Y);
			}
		}

		private void panelHeader_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void pictureBoxResize_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseDownPoint = new Point(e.X, e.Y);
			}
		}

		private void pictureBoxResize_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int num = base.Width + e.X - mouseDownPoint.X;
				int num2 = base.Height + e.Y - mouseDownPoint.Y;
				lastX = num;
				lastY = num2;
				if (lastX < 168)
				{
					lastX = 168;
				}
				if (lastY < 96)
				{
					lastY = 96;
				}
				base.Size = new Size(lastX, lastY);
			}
		}

		private void pictureBoxClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void NoteViewForm_Paint(object sender, PaintEventArgs e)
		{
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(Pens.Silver, 10, 1, panel3.Width, 1);
		}

		public bool LoadNote(int id)
		{
			int height = 50;
			int width = 50;
			NoteData notesByID = Factory.NoteSystem.GetNotesByID(id);
			if (notesByID == null)
			{
				return false;
			}
			if (notesByID.Tables["Notes"].Rows.Count > 0)
			{
				DataRow dataRow = notesByID.Tables["Notes"].Rows[0];
				try
				{
					height = int.Parse(dataRow["NoteHeight"].ToString());
				}
				catch
				{
				}
				try
				{
					width = int.Parse(dataRow["NoteWidth"].ToString());
				}
				catch
				{
				}
				try
				{
					int.Parse(dataRow["NoteXLocation"].ToString());
				}
				catch
				{
				}
				try
				{
					int.Parse(dataRow["NoteYLocation"].ToString());
				}
				catch
				{
				}
				note1.Height = height;
				note1.Width = width;
				note1.NoteColor = dataRow["NoteColor"].ToString();
				note1.NoteCreator = int.Parse(dataRow["CreatedBy"].ToString());
				note1.Text = dataRow["NoteText"].ToString();
				note1.TitleText = dataRow["TitleText"].ToString();
				ChangeFormTitle();
				try
				{
					note1.NoteFlag = (NoteFlags)byte.Parse(dataRow["NoteFlag"].ToString());
				}
				catch
				{
				}
				if (note1.NoteCreator != GetCurrentUserID())
				{
					note1.IsReadOnly = true;
				}
				note1.IsMinimized();
				try
				{
					if (dataRow["ReminderDate"] != DBNull.Value)
					{
						note1.ReminderDate = DateTime.Parse(dataRow["ReminderDate"].ToString());
					}
				}
				catch
				{
				}
				note1.NoteID = id;
				if (dataRow["DateTimeStamp"] != DBNull.Value)
				{
					note1.DateTimeStamp = DateTime.Parse(dataRow["DateTimeStamp"].ToString());
				}
				return false;
			}
			return false;
		}

		public bool Save()
		{
			if (isDeleted)
			{
				return true;
			}
			if (note1.IsDirty && !note1.IsReadOnly)
			{
				if (Global.CurrentUser.ToLower() != "sa" && note1.Text.Length > MaxNoteSize)
				{
					ErrorHelper.WarningMessage("This note1 is too big to be saved. Please reduce its size.", "Maximum size for a note1 is " + MaxNoteSize.ToString() + ".", "This limitation does not apply to user 'sa'.");
					note1.Focus();
					return false;
				}
				NoteData noteData = new NoteData();
				DataRow dataRow = noteData.NoteTable.NewRow();
				dataRow["CreatedBy"] = GetCurrentUserID();
				dataRow["NoteColor"] = note1.NoteColor;
				dataRow["IsInactive"] = note1.IsInactive;
				if (note1.ReminderDate != DateTime.MinValue)
				{
					dataRow["ReminderDate"] = note1.ReminderDate;
				}
				if (note1.DateTimeStamp != DateTime.MinValue)
				{
					dataRow["DateTimeStamp"] = note1.DateTimeStamp;
				}
				try
				{
					dataRow["NoteHeight"] = note1.Height;
				}
				catch
				{
				}
				try
				{
					dataRow["NoteWidth"] = note1.Width;
				}
				catch
				{
				}
				try
				{
					dataRow["NoteXLocation"] = note1.Location.X;
				}
				catch
				{
				}
				try
				{
					dataRow["NoteYLocation"] = note1.Location.Y;
				}
				catch
				{
				}
				dataRow["NoteText"] = note1.Text;
				string titleText = note1.TitleText;
				if (titleText.Length > 65)
				{
					dataRow["TitleText"] = titleText.Substring(0, 64);
				}
				else
				{
					dataRow["TitleText"] = titleText;
				}
				note1.IsDirty = false;
				noteData.NoteTable.Rows.Add(dataRow);
				if (!IsNewNote)
				{
					noteData.AcceptChanges();
					dataRow["NoteID"] = note1.NoteID;
				}
				try
				{
					if (IsNewNote)
					{
						NoteID = Factory.NoteSystem.CreateNote(noteData);
						IsNewNote = false;
					}
					else
					{
						Factory.NoteSystem.UpdateNote(noteData);
					}
					note1.DateTimeStamp = Factory.NoteSystem.GetNoteDateTimeStamp(note1.NoteID);
					SetNoteFlag();
					return true;
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
					return false;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
			ChangeFormTitle();
			return true;
		}

		private void SetNoteFlag()
		{
			if (NoteID >= 0)
			{
				try
				{
					Factory.NoteSystem.SetNoteFlag(Global.CurrentUserID, NoteID, note1.NoteFlag);
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		private void SetMenuFlagCheck()
		{
			switch (note1.NoteFlag)
			{
			case NoteFlags.Important:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = true;
				break;
			case NoteFlags.ToDo:
				menuItemFlagToDo.Checked = true;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = false;
				break;
			case NoteFlags.Reminder:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = true;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = false;
				break;
			case NoteFlags.Question:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = true;
				menuItemFlagImportant.Checked = false;
				break;
			}
		}

		private int GetCurrentUserID()
		{
			return Global.CurrentUserID;
		}

		private void NoteViewForm_Closed(object sender, EventArgs e)
		{
			Save();
		}

		private void NoteViewForm_Deactivate(object sender, EventArgs e)
		{
			panelHeader.BackColor = SystemColors.InactiveCaption;
		}

		private void NoteViewForm_Activated(object sender, EventArgs e)
		{
			panelHeader.BackColor = SystemColors.ActiveCaption;
			if (isFirstTime)
			{
				isFirstTime = false;
				ChangeFormTitle();
			}
		}

		private void NoteViewForm_Enter(object sender, EventArgs e)
		{
		}

		private void note1_BackColorChanged(object sender, EventArgs e)
		{
		}

		private void note1_NoteColorChanged(object sender, EventArgs e)
		{
			BackColor = note1.NoteBackColor;
			Panel panel = this.panel2;
			Panel panel2 = panel3;
			Color color = panelMain.BackColor = note1.NoteBackColor;
			Color color4 = panel.BackColor = (panel2.BackColor = color);
		}

		private void NoteViewForm_Load(object sender, EventArgs e)
		{
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			contextMenu.Show(pictureBox1, new Point(pictureBox1.Location.X, pictureBox1.Location.Y + pictureBox1.Height));
		}

		private void menuItemBlue_Click(object sender, EventArgs e)
		{
		}

		private void menuItemGreen_Click(object sender, EventArgs e)
		{
		}

		private void menuItemPink_Click(object sender, EventArgs e)
		{
		}

		private void menuItemYellow_Click(object sender, EventArgs e)
		{
		}

		private void menuItemWhite_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFlagToDo_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFlagImportant_Click(object sender, EventArgs e)
		{
			note1.NoteFlag = NoteFlags.Important;
		}

		private void menuItemFlagQuestion_Click(object sender, EventArgs e)
		{
			note1.NoteFlag = NoteFlags.Question;
		}

		private void menuItemFlagReminder_Click(object sender, EventArgs e)
		{
			note1.NoteFlag = NoteFlags.Reminder;
		}

		private void menuItemSaveNote_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void menuItemSetReminder_Click(object sender, EventArgs e)
		{
			using (SetReminderForm setReminderForm = new SetReminderForm())
			{
				if (note1.ReminderDate != DateTime.MinValue)
				{
					setReminderForm.ReminderDate = note1.ReminderDate;
				}
				else
				{
					setReminderForm.ReminderDate = DateTime.Now;
				}
				setReminderForm.ShowDialog();
				if (setReminderForm.DialogResult == DialogResult.OK)
				{
					note1.IsDirty = true;
					note1.ReminderDate = setReminderForm.ReminderDate;
				}
			}
		}

		private void menuItemUsers_Click(object sender, EventArgs e)
		{
		}

		private void menuItemNoteIsInactive_Click(object sender, EventArgs e)
		{
			note1.IsInactive = !note1.IsInactive;
			note1.IsDirty = true;
		}

		private void menuItemDelete_Click(object sender, EventArgs e)
		{
			DeleteNote();
		}

		private void DeleteNote()
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this note?") != DialogResult.Yes)
			{
				return;
			}
			if (Factory.NoteSystem.DeleteNote(NoteID))
			{
				isDeleted = true;
				if (this.OnDeleteNote != null)
				{
					this.OnDeleteNote(NoteID, null);
				}
				isDeleted = true;
				Close();
			}
			else
			{
				ErrorHelper.WarningMessage("Cannot delete this note.");
			}
		}

		private void menuItemMinimize_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void menuItemSaveAs_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "Plain Text (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
			StreamWriter streamWriter = null;
			saveFileDialog.FilterIndex = 1;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if (saveFileDialog.FilterIndex == 1)
					{
						streamWriter = new StreamWriter(saveFileDialog.FileName, append: false);
						string[] lines = note1.TextControl.Lines;
						foreach (string value in lines)
						{
							streamWriter.WriteLine(value);
						}
					}
					else
					{
						note1.TextControl.SaveFile(saveFileDialog.FileName);
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				finally
				{
					if (streamWriter != null)
					{
						streamWriter.Close();
						streamWriter = null;
					}
				}
			}
		}

		private void contextMenu_Popup(object sender, EventArgs e)
		{
			SetMenuFlagCheck();
		}

		private void ChangeFormTitle()
		{
			if (note1.Text.Trim() != "")
			{
				Text = note1.TitleText;
			}
			else
			{
				Text = "Untitled";
			}
		}

		private void menuItemTitleTodayDate_Click(object sender, EventArgs e)
		{
			note1.SetNoteTitle(NoteTitles.Date);
			ChangeFormTitle();
		}

		private void menuItemTitleTodayDateTime_Click(object sender, EventArgs e)
		{
			note1.SetNoteTitle(NoteTitles.DateAndTime);
			ChangeFormTitle();
		}

		private void menuItemTitleFirstLineFromNote_Click(object sender, EventArgs e)
		{
			note1.SetNoteTitle(NoteTitles.FirstLine);
			ChangeFormTitle();
		}

		private void note1_OnSaveNote(object sender, EventArgs e)
		{
			Save();
		}
	}
}
