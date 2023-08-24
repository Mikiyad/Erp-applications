using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class Note : UserControl
	{
		private bool allowResize = true;

		private bool showPanelTop = true;

		private bool showBorder = true;

		public bool IsMaximized;

		private DateTime reminderDate = DateTime.MinValue;

		private int prevWidth = -1;

		private int prevHeight = -1;

		private int noteCreator = -1;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private bool isDirty;

		private bool isLocationColorTitleReminderDateDirty;

		private string noteID = "";

		private Color oldColor = Color.Black;

		private bool isInactive;

		private bool leftResize;

		private bool rightResize;

		private bool topResize;

		private bool downResize;

		private bool rightTopResize;

		private bool leftTopResize;

		private bool rightDownResize;

		private bool leftDownResize;

		private bool isMoving;

		private int prevX;

		private int prevY;

		private NoteFlags noteFlag;

		private ContextMenu contextMenu;

		private MenuItem menuItem3;

		private SaveFileDialog saveFileDialog;

		private MenuItem menuItemDelete;

		private MenuItem menuItemMinimize;

		private MenuItem menuItemFlagToDo;

		private MenuItem menuItemFlagImportant;

		private MenuItem menuItemFlagQuestion;

		private MenuItem menuItemFlagReminder;

		private PictureBox pictureBoxFlag;

		private MenuItem menuItemUsers;

		private MenuItem menuItem8;

		private ToolTip toolTip;

		private TextControl textControl;

		private MenuItem menuItemSaveNote;

		private MenuItem menuItemFlagRecycled;

		private MenuItem menuItem4;

		private MenuItem menuItemSetFlag;

		private MenuItem menuItem1;

		private MenuItem menuItemSetReminder;

		private MenuItem menuItem2;

		private MenuItem menuItemTitleTodayDate;

		private MenuItem menuItemTitleTodayDateTime;

		private MenuItem menuItemTitleFirstLineFromNote;

		private MenuItem menuItem5;

		private Panel panelBottom;

		private Panel panelTop;

		private Label labelBottom;

		private ImageList imageList;

		private MenuItem menuItem7;

		private MenuItem menuItemNoteIsInactive;

		private MenuItem menuItemExportToMSOutlook;

		private Panel panelHeader;

		private PictureBox pictureBoxClose;

		private PictureBox pictureBoxResize;

		private Panel panelMain;

		private Label labelTitle;

		private MenuItem menuItemNewNote;

		private MenuItem menuItemNewNoteSep;

		private IContainer components;

		private Point mouseDownPoint;

		private int lastX;

		private int lastY;

		public TextControl TextControl => textControl;

		public bool ShowHeader
		{
			get
			{
				return showPanelTop;
			}
			set
			{
				showPanelTop = value;
				panelTop.Visible = value;
				panelBottom.Visible = value;
			}
		}

		public bool Resizeable
		{
			get
			{
				return allowResize;
			}
			set
			{
				allowResize = value;
			}
		}

		public bool ShowBorder
		{
			get
			{
				return showBorder;
			}
			set
			{
				showBorder = value;
				if (value)
				{
					panelMain.BorderStyle = BorderStyle.FixedSingle;
				}
				else
				{
					panelMain.BorderStyle = BorderStyle.None;
				}
			}
		}

		public string NoteColor
		{
			get
			{
				return textControl.GetBackColor();
			}
			set
			{
				try
				{
					textControl.LoadBackcolor(value);
					NoteBackColor = textControl.BackColor;
				}
				catch
				{
				}
			}
		}

		public Color NoteBackColor
		{
			get
			{
				return textControl.BackColor;
			}
			set
			{
				Label label = labelBottom;
				PictureBox pictureBox = pictureBoxFlag;
				Panel panel = panelMain;
				Panel panel2 = panelTop;
				Color color2 = textControl.BackColor = value;
				Color color4 = panel2.BackColor = color2;
				Color color6 = panel.BackColor = color4;
				Color color9 = label.BackColor = (pictureBox.BackColor = color6);
				isLocationColorTitleReminderDateDirty = true;
			}
		}

		public RichTextBoxScrollBars Scrollbar
		{
			get
			{
				return textControl.ScrollBars;
			}
			set
			{
				textControl.ScrollBars = value;
			}
		}

		public string TitleText
		{
			get
			{
				return labelTitle.Text;
			}
			set
			{
				if (value.Length > 64)
				{
					labelTitle.Text = value.Substring(0, 63) + "...";
				}
				else
				{
					labelTitle.Text = value;
				}
			}
		}

		public string ToolTipText
		{
			get
			{
				string text = "";
				string[] lines = textControl.Lines;
				if (lines.Length != 0)
				{
					int i = 0;
					int num = base.Width;
					for (string text2 = lines[0]; text2.Length - i > 0; i += num)
					{
						if (num >= text2.Length - i)
						{
							num = text2.Length - i;
						}
						text = text + text2.Substring(i, num) + "\n";
					}
				}
				return text.Trim();
			}
		}

		public bool IsDirty
		{
			get
			{
				return isDirty;
			}
			set
			{
				isDirty = value;
			}
		}

		public bool IsLocationColorTitleReminderDateDirty
		{
			get
			{
				return isLocationColorTitleReminderDateDirty;
			}
			set
			{
				isLocationColorTitleReminderDateDirty = value;
			}
		}

		public new string Text
		{
			get
			{
				return textControl.Rtf;
			}
			set
			{
				textControl.Rtf = value;
			}
		}

		public string Rtf
		{
			get
			{
				return textControl.Rtf;
			}
			set
			{
				textControl.Rtf = value;
			}
		}

		public bool DisplayTitleBar
		{
			set
			{
				labelBottom.Visible = value;
			}
		}

		public bool AllowDelete
		{
			set
			{
				menuItemDelete.Enabled = value;
			}
		}

		public new Color BackColor
		{
			get
			{
				return NoteBackColor;
			}
			set
			{
				NoteBackColor = value;
			}
		}

		public int MinWidth => pictureBoxFlag.Width + 2;

		public int MinHeight => textControl.Top + 3;

		public NoteFlags NoteFlag
		{
			get
			{
				return noteFlag;
			}
			set
			{
				noteFlag = value;
				if (reminderDate != DateTime.MinValue && reminderDate <= DateTime.Now && noteFlag != NoteFlags.Recycled)
				{
					pictureBoxFlag.Image = imageList.Images[5];
				}
				else
				{
					switch (value)
					{
					case NoteFlags.ToDo:
						pictureBoxFlag.Image = imageList.Images[0];
						break;
					case NoteFlags.Important:
						pictureBoxFlag.Image = imageList.Images[1];
						break;
					case NoteFlags.Question:
						pictureBoxFlag.Image = imageList.Images[2];
						break;
					case NoteFlags.Reminder:
						pictureBoxFlag.Image = imageList.Images[3];
						break;
					case NoteFlags.Recycled:
						pictureBoxFlag.Image = imageList.Images[4];
						break;
					}
				}
				SetMenuFlagCheck();
			}
		}

		public int NoteID
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		public int NoteCreator
		{
			get
			{
				return noteCreator;
			}
			set
			{
				noteCreator = value;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return textControl.ReadOnly;
			}
			set
			{
				textControl.ReadOnly = value;
				menuItemSaveNote.Enabled = !value;
				menuItemSetReminder.Enabled = !value;
			}
		}

		public DateTime DateTimeStamp
		{
			get
			{
				return dateTimeStamp;
			}
			set
			{
				dateTimeStamp = value;
				if (value != DateTime.MinValue)
				{
					labelBottom.Text = value.ToShortDateString() + " " + value.ToShortTimeString();
				}
			}
		}

		public DateTime ReminderDate
		{
			get
			{
				return reminderDate;
			}
			set
			{
				reminderDate = value;
				SetReminderIcon();
			}
		}

		public bool IsInactive
		{
			get
			{
				return isInactive;
			}
			set
			{
				isInactive = value;
				menuItemNoteIsInactive.Checked = isInactive;
			}
		}

		public string PlainText
		{
			get
			{
				return textControl.Text;
			}
			set
			{
				textControl.Text = value;
			}
		}

		public event EventHandler OnMaximize;

		public event EventHandler InactivateNoteChanged;

		public event EventHandler OnSaveNote;

		public event EventHandler OnFlagChanged;

		public event EventHandler OnMinimizeNote;

		public event EventHandler OnAssignNoteUsers;

		public event EventHandler OnReminderCreated;

		public event EventHandler OnNewNote;

		public event EventHandler OnNoteInActivated;

		public event EventHandler NoteColorChanged;

		public Note()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			InitializeComponent();
			oldColor = textControl.ForeColor;
			panelTop.Visible = showPanelTop;
			panelBottom.Visible = showPanelTop;
			textControl.BackColorChanged += textControl_BackColorChanged;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
			if (textControl != null)
			{
				textControl.Dispose();
				textControl = null;
			}
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.DataControls.Note));
			panelMain = new System.Windows.Forms.Panel();
			textControl = new Micromind.UISupport.TextControl();
			panelBottom = new System.Windows.Forms.Panel();
			pictureBoxResize = new System.Windows.Forms.PictureBox();
			labelBottom = new System.Windows.Forms.Label();
			contextMenu = new System.Windows.Forms.ContextMenu();
			menuItemNewNote = new System.Windows.Forms.MenuItem();
			menuItemNewNoteSep = new System.Windows.Forms.MenuItem();
			menuItemMinimize = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemSetFlag = new System.Windows.Forms.MenuItem();
			menuItemFlagToDo = new System.Windows.Forms.MenuItem();
			menuItemFlagImportant = new System.Windows.Forms.MenuItem();
			menuItemFlagQuestion = new System.Windows.Forms.MenuItem();
			menuItemFlagReminder = new System.Windows.Forms.MenuItem();
			menuItem4 = new System.Windows.Forms.MenuItem();
			menuItemFlagRecycled = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemTitleTodayDate = new System.Windows.Forms.MenuItem();
			menuItemTitleTodayDateTime = new System.Windows.Forms.MenuItem();
			menuItemTitleFirstLineFromNote = new System.Windows.Forms.MenuItem();
			menuItem5 = new System.Windows.Forms.MenuItem();
			menuItemSaveNote = new System.Windows.Forms.MenuItem();
			menuItemExportToMSOutlook = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemSetReminder = new System.Windows.Forms.MenuItem();
			menuItemUsers = new System.Windows.Forms.MenuItem();
			menuItem8 = new System.Windows.Forms.MenuItem();
			menuItemNoteIsInactive = new System.Windows.Forms.MenuItem();
			menuItem7 = new System.Windows.Forms.MenuItem();
			menuItemDelete = new System.Windows.Forms.MenuItem();
			panelTop = new System.Windows.Forms.Panel();
			pictureBoxClose = new System.Windows.Forms.PictureBox();
			panelHeader = new System.Windows.Forms.Panel();
			labelTitle = new System.Windows.Forms.Label();
			pictureBoxFlag = new System.Windows.Forms.PictureBox();
			saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			toolTip = new System.Windows.Forms.ToolTip(components);
			imageList = new System.Windows.Forms.ImageList(components);
			panelMain.SuspendLayout();
			panelBottom.SuspendLayout();
			panelTop.SuspendLayout();
			panelHeader.SuspendLayout();
			SuspendLayout();
			panelMain.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
			panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelMain.Controls.Add(textControl);
			panelMain.Controls.Add(panelBottom);
			panelMain.Controls.Add(panelTop);
			panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			panelMain.ImeMode = System.Windows.Forms.ImeMode.On;
			panelMain.Location = new System.Drawing.Point(0, 0);
			panelMain.Name = "panelMain";
			panelMain.Size = new System.Drawing.Size(400, 200);
			panelMain.TabIndex = 1;
			panelMain.Resize += new System.EventHandler(panel1_Resize);
			panelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(panel1_MouseUp);
			panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(panel1_MouseMove);
			panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(panel1_MouseDown);
			textControl.AcceptsTab = true;
			textControl.AllowDrop = true;
			textControl.BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
			textControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textControl.Dock = System.Windows.Forms.DockStyle.Fill;
			textControl.Font = new System.Drawing.Font("Tahoma", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textControl.Location = new System.Drawing.Point(0, 16);
			textControl.MaxLength = 32000;
			textControl.Name = "textControl";
			textControl.Size = new System.Drawing.Size(398, 164);
			textControl.TabIndex = 0;
			textControl.Text = "";
			textControl.MouseDown += new System.Windows.Forms.MouseEventHandler(textControl_MouseDown);
			textControl.TextChanged += new System.EventHandler(textControl_TextChanged_1);
			textControl.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(textControl_LinkClicked);
			panelBottom.BackColor = System.Drawing.Color.Transparent;
			panelBottom.Controls.Add(pictureBoxResize);
			panelBottom.Controls.Add(labelBottom);
			panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelBottom.Location = new System.Drawing.Point(0, 180);
			panelBottom.Name = "panelBottom";
			panelBottom.Size = new System.Drawing.Size(398, 18);
			panelBottom.TabIndex = 1;
			panelBottom.Paint += new System.Windows.Forms.PaintEventHandler(panelBottom_Paint);
			pictureBoxResize.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			pictureBoxResize.BackColor = System.Drawing.Color.Transparent;
			pictureBoxResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			pictureBoxResize.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxResize.Image");
			pictureBoxResize.Location = new System.Drawing.Point(380, 3);
			pictureBoxResize.Name = "pictureBoxResize";
			pictureBoxResize.Size = new System.Drawing.Size(18, 16);
			pictureBoxResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBoxResize.TabIndex = 10;
			pictureBoxResize.TabStop = false;
			pictureBoxResize.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBoxResize_MouseMove);
			pictureBoxResize.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBoxResize_MouseDown);
			labelBottom.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelBottom.BackColor = System.Drawing.Color.Transparent;
			labelBottom.ContextMenu = contextMenu;
			labelBottom.Cursor = System.Windows.Forms.Cursors.Default;
			labelBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelBottom.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelBottom.Location = new System.Drawing.Point(1, 2);
			labelBottom.Name = "labelBottom";
			labelBottom.Size = new System.Drawing.Size(376, 15);
			labelBottom.TabIndex = 3;
			labelBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			labelBottom.DoubleClick += new System.EventHandler(labelBottom_DoubleClick);
			labelBottom.MouseDown += new System.Windows.Forms.MouseEventHandler(labelBottom_MouseDown);
			contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[16]
			{
				menuItemNewNote,
				menuItemNewNoteSep,
				menuItemMinimize,
				menuItem1,
				menuItemSetFlag,
				menuItem2,
				menuItem5,
				menuItemSaveNote,
				menuItemExportToMSOutlook,
				menuItem3,
				menuItemSetReminder,
				menuItemUsers,
				menuItem8,
				menuItemNoteIsInactive,
				menuItem7,
				menuItemDelete
			});
			contextMenu.Popup += new System.EventHandler(contextMenu_Popup);
			menuItemNewNote.Index = 0;
			menuItemNewNote.Text = "New Note";
			menuItemNewNote.Click += new System.EventHandler(menuItemNewNote_Click);
			menuItemNewNoteSep.Index = 1;
			menuItemNewNoteSep.Text = "-";
			menuItemMinimize.Index = 2;
			menuItemMinimize.Text = "Close";
			menuItemMinimize.Click += new System.EventHandler(menuItemMinimize_Click);
			menuItem1.Index = 3;
			menuItem1.Text = "-";
			menuItemSetFlag.Index = 4;
			menuItemSetFlag.MenuItems.AddRange(new System.Windows.Forms.MenuItem[6]
			{
				menuItemFlagToDo,
				menuItemFlagImportant,
				menuItemFlagQuestion,
				menuItemFlagReminder,
				menuItem4,
				menuItemFlagRecycled
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
			menuItem4.Index = 4;
			menuItem4.Text = "-";
			menuItemFlagRecycled.Index = 5;
			menuItemFlagRecycled.Text = "Recycled";
			menuItemFlagRecycled.Click += new System.EventHandler(menuItemFlagRecycled_Click);
			menuItem2.Index = 5;
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
			menuItem5.Index = 6;
			menuItem5.Text = "-";
			menuItemSaveNote.Index = 7;
			menuItemSaveNote.Text = "Save";
			menuItemSaveNote.Click += new System.EventHandler(menuItemSaveNote_Click);
			menuItemExportToMSOutlook.Index = 8;
			menuItemExportToMSOutlook.Text = "Export to MS Outlook";
			menuItemExportToMSOutlook.Click += new System.EventHandler(menuItemExportToMSOutlook_Click);
			menuItem3.Index = 9;
			menuItem3.Text = "-";
			menuItemSetReminder.Index = 10;
			menuItemSetReminder.Text = "Set Alarm...";
			menuItemSetReminder.Click += new System.EventHandler(menuItemSetReminder_Click);
			menuItemUsers.Index = 11;
			menuItemUsers.Text = "Assign Users...";
			menuItemUsers.Click += new System.EventHandler(menuItemUsers_Click);
			menuItem8.Index = 12;
			menuItem8.Text = "-";
			menuItemNoteIsInactive.Index = 13;
			menuItemNoteIsInactive.Text = "Note is Inactive";
			menuItemNoteIsInactive.Click += new System.EventHandler(menuItemNoteIsInactive_Click);
			menuItem7.Index = 14;
			menuItem7.Text = "-";
			menuItemDelete.Index = 15;
			menuItemDelete.Text = "Delete";
			menuItemDelete.Click += new System.EventHandler(menuItemDelete_Click);
			panelTop.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
			panelTop.Controls.Add(pictureBoxClose);
			panelTop.Controls.Add(panelHeader);
			panelTop.Controls.Add(pictureBoxFlag);
			panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			panelTop.ImeMode = System.Windows.Forms.ImeMode.On;
			panelTop.Location = new System.Drawing.Point(0, 0);
			panelTop.Name = "panelTop";
			panelTop.Size = new System.Drawing.Size(398, 16);
			panelTop.TabIndex = 3;
			pictureBoxClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			pictureBoxClose.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxClose.Image");
			pictureBoxClose.Location = new System.Drawing.Point(384, 0);
			pictureBoxClose.Name = "pictureBoxClose";
			pictureBoxClose.Size = new System.Drawing.Size(10, 10);
			pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBoxClose.TabIndex = 6;
			pictureBoxClose.TabStop = false;
			pictureBoxClose.Click += new System.EventHandler(pictureBoxClose_Click);
			panelHeader.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
			panelHeader.Controls.Add(labelTitle);
			panelHeader.Location = new System.Drawing.Point(24, 0);
			panelHeader.Name = "panelHeader";
			panelHeader.Size = new System.Drawing.Size(360, 14);
			panelHeader.TabIndex = 4;
			panelHeader.Click += new System.EventHandler(panelHeader_Click);
			panelHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseUp);
			panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseMove);
			panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(panelHeader_MouseDown);
			labelTitle.BackColor = System.Drawing.Color.Transparent;
			labelTitle.ContextMenu = contextMenu;
			labelTitle.Cursor = System.Windows.Forms.Cursors.Default;
			labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			labelTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelTitle.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelTitle.ForeColor = System.Drawing.Color.White;
			labelTitle.Location = new System.Drawing.Point(0, 0);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new System.Drawing.Size(360, 14);
			labelTitle.TabIndex = 4;
			labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			labelTitle.DoubleClick += new System.EventHandler(labelTitle_DoubleClick);
			labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(labelTitle_MouseMove);
			labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(labelTitle_MouseDown);
			pictureBoxFlag.BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
			pictureBoxFlag.ContextMenu = contextMenu;
			pictureBoxFlag.Cursor = System.Windows.Forms.Cursors.Default;
			pictureBoxFlag.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxFlag.Image");
			pictureBoxFlag.Location = new System.Drawing.Point(1, 0);
			pictureBoxFlag.Name = "pictureBoxFlag";
			pictureBoxFlag.Size = new System.Drawing.Size(16, 16);
			pictureBoxFlag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxFlag.TabIndex = 3;
			pictureBoxFlag.TabStop = false;
			pictureBoxFlag.Click += new System.EventHandler(pictureBoxFlag_Click_1);
			pictureBoxFlag.MouseUp += new System.Windows.Forms.MouseEventHandler(pictureBoxFlag_MouseUp);
			pictureBoxFlag.DoubleClick += new System.EventHandler(pictureBoxFlag_DoubleClick);
			pictureBoxFlag.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBoxFlag_MouseMove);
			pictureBoxFlag.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBoxFlag_MouseDown);
			imageList.ImageSize = new System.Drawing.Size(16, 16);
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			BackColor = System.Drawing.Color.FromArgb(255, 255, 200);
			base.Controls.Add(panelMain);
			base.Name = "Note";
			base.Size = new System.Drawing.Size(400, 200);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Note_KeyPress);
			base.LocationChanged += new System.EventHandler(Note_LocationChanged);
			base.Load += new System.EventHandler(UserControl1_Load);
			base.Enter += new System.EventHandler(Note_Enter);
			base.Leave += new System.EventHandler(Note_Leave);
			panelMain.ResumeLayout(false);
			panelBottom.ResumeLayout(false);
			panelTop.ResumeLayout(false);
			panelHeader.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void UserControl1_Load(object sender, EventArgs e)
		{
			textControl.Focus();
		}

		private void menuItemSetColor_Click(object sender, EventArgs e)
		{
		}

		public void FindString(string str)
		{
			textControl.SelectAll();
			textControl.SelectionColor = oldColor;
			int num = -1;
			do
			{
				num++;
				if (num >= textControl.Text.Length)
				{
					break;
				}
				num = textControl.Find(str, num, RichTextBoxFinds.None);
				if (num >= 0)
				{
					oldColor = textControl.ForeColor;
					textControl.SelectionColor = Color.YellowGreen;
					if (IsMinimized())
					{
						RestoreView(minimize: true);
					}
				}
			}
			while (num >= 0);
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			textControl.Text += DateTime.Now.ToString();
			textControl.SelectionStart = textControl.Text.Length;
		}

		private void menuItemSave_Click(object sender, EventArgs e)
		{
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.DefaultExt = "txt";
			saveFileDialog.Filter = "Text Only (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
				{
					string[] lines = textControl.Lines;
					foreach (string value in lines)
					{
						streamWriter.WriteLine(value);
					}
				}
			}
		}

		private void menuItemCopy_Click(object sender, EventArgs e)
		{
			textControl.Copy();
		}

		private void menuItemPaste_Click(object sender, EventArgs e)
		{
			textControl.Paste();
		}

		private void menuItem4SelectAll_Click(object sender, EventArgs e)
		{
			textControl.SelectAll();
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
			textControl.Cut();
		}

		private void menuItemUndo_Click(object sender, EventArgs e)
		{
			textControl.Undo();
		}

		private void menuItemUndo_Popup(object sender, EventArgs e)
		{
		}

		private void contextMenu_Popup(object sender, EventArgs e)
		{
			if (base.Height == MinHeight && base.Width == MinWidth)
			{
				menuItemMinimize.Text = "Open";
			}
			else
			{
				menuItemMinimize.Text = "Close";
			}
			if (this.OnAssignNoteUsers == null)
			{
				menuItemUsers.Enabled = false;
			}
			else
			{
				menuItemUsers.Enabled = true;
			}
			if (this.OnSaveNote == null)
			{
				menuItemSaveNote.Enabled = false;
			}
			else
			{
				menuItemSaveNote.Enabled = true;
			}
			if (this.OnNewNote == null)
			{
				menuItemNewNote.Visible = false;
				menuItemNewNoteSep.Visible = false;
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			Resize(panelMain);
		}

		private new void Resize(object sender)
		{
			if (!Resizeable)
			{
				textControl.Top = 5;
				textControl.Left = 2;
				textControl.Height = base.Height;
				textControl.Width = base.Width;
				return;
			}
			_ = (Panel)sender;
			pictureBoxFlag.Location = new Point(0, 0);
			panelTop.Top = 2;
			panelTop.Left = 2;
			panelTop.Width = base.ClientSize.Width - 4;
			if (base.ClientSize.Height == MinHeight && base.ClientSize.Width == MinWidth)
			{
				if (this.OnMinimizeNote != null)
				{
					this.OnMinimizeNote(this, null);
				}
			}
			else
			{
				textControl.Focus();
			}
			textControl.Top = panelTop.Top + panelTop.ClientSize.Height + 1;
			textControl.Height = base.ClientSize.Height - panelTop.ClientSize.Height - panelBottom.ClientSize.Height - 8;
			textControl.Width = base.ClientSize.Width - 10;
			textControl.Left = 3;
			panelBottom.Top = base.ClientSize.Height - panelBottom.Height - 4;
			panelBottom.Left = panelTop.Left;
			panelBottom.Width = panelTop.ClientSize.Width;
			isLocationColorTitleReminderDateDirty = true;
		}

		private void panel1_Resize(object sender, EventArgs e)
		{
			Resize(sender);
			isLocationColorTitleReminderDateDirty = true;
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void labelTop_MouseMove(object sender, MouseEventArgs e)
		{
			panel1_MouseMove(sender, e);
		}

		private void labelTop_MouseUp(object sender, MouseEventArgs e)
		{
			panel1_MouseUp(sender, e);
		}

		private void labelTop_MouseDown(object sender, MouseEventArgs e)
		{
			panel1_MouseDown(sender, e);
		}

		private void textControl_TextChanged(object sender, EventArgs e)
		{
			isDirty = true;
		}

		private void menuItemDelete_Click(object sender, EventArgs e)
		{
		}

		private void menuItemBlue_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Blue;
			isDirty = true;
		}

		private void menuItemGreen_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Green;
			isDirty = true;
		}

		private void menuItemPink_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Pink;
			isDirty = true;
		}

		private void menuItemWhite_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.White;
			isDirty = true;
		}

		public void Minimize()
		{
			if (base.Height != MinHeight)
			{
				prevHeight = base.Height;
			}
			if (base.Width != MinWidth)
			{
				prevWidth = base.Width;
			}
			base.Height = MinHeight;
			base.Width = MinWidth;
		}

		public void HalfSize()
		{
			if (base.Height <= textControl.Top + 3)
			{
				if (prevHeight != -1 && prevWidth != -1 && prevHeight > textControl.Top + 3)
				{
					base.Height = prevHeight;
					base.Width = prevWidth;
				}
				else
				{
					base.Height = 165;
					base.Width = 195;
				}
			}
			else
			{
				prevHeight = base.Height;
				prevWidth = base.Width;
				base.Height = textControl.Top + 3;
			}
		}

		private void menuItemMinimize_Click(object sender, EventArgs e)
		{
			RestoreView(minimize: true);
		}

		public void RestoreView(bool minimize)
		{
			if (base.Height == MinHeight && base.Width == MinWidth)
			{
				if (prevHeight != -1 && prevWidth != -1)
				{
					base.Height = prevHeight;
					base.Width = prevWidth;
				}
				else
				{
					base.Height = 165;
					base.Width = 195;
				}
			}
			else if (minimize)
			{
				Minimize();
			}
			else
			{
				HalfSize();
			}
			BringToFront();
		}

		private void labelTop_DoubleClick(object sender, EventArgs e)
		{
		}

		private void menuItemCyan_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Cyan;
		}

		private void menuItemOrange_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Orange;
		}

		private void menuItemPurple_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Purple;
		}

		private void menuItemRed_Click(object sender, EventArgs e)
		{
			NoteBackColor = Color.Red;
		}

		private void menuItemFlagToDo_Click(object sender, EventArgs e)
		{
			NoteFlag = NoteFlags.ToDo;
			if (this.OnFlagChanged != null)
			{
				this.OnFlagChanged(this, null);
			}
		}

		private void menuItemFlagImportant_Click(object sender, EventArgs e)
		{
			NoteFlag = NoteFlags.Important;
			if (this.OnFlagChanged != null)
			{
				this.OnFlagChanged(this, null);
			}
		}

		private void menuItemFlagQuestion_Click(object sender, EventArgs e)
		{
			NoteFlag = NoteFlags.Question;
			if (this.OnFlagChanged != null)
			{
				this.OnFlagChanged(this, null);
			}
		}

		private void menuItemFlagReminder_Click(object sender, EventArgs e)
		{
			NoteFlag = NoteFlags.Reminder;
			if (this.OnFlagChanged != null)
			{
				this.OnFlagChanged(this, null);
			}
		}

		private void labelTop_Click(object sender, EventArgs e)
		{
			BringToFront();
		}

		private void pictureBoxFlag_MouseMove(object sender, MouseEventArgs e)
		{
			panel1_MouseMove(sender, e);
			if (IsMinimized())
			{
				string titleText = TitleText;
				if (titleText.Length > 0)
				{
					toolTip.SetToolTip(pictureBoxFlag, titleText);
				}
			}
			else
			{
				toolTip.RemoveAll();
			}
		}

		public bool IsMinimized()
		{
			if (base.Width == MinWidth)
			{
				return base.Height == MinHeight;
			}
			return false;
		}

		private void pictureBoxFlag_MouseDown(object sender, MouseEventArgs e)
		{
			panel1_MouseDown(sender, e);
		}

		private void pictureBoxFlag_MouseUp(object sender, MouseEventArgs e)
		{
			panel1_MouseUp(sender, e);
		}

		private void pictureBoxFlag_DoubleClick(object sender, EventArgs e)
		{
			RestoreView(minimize: true);
		}

		private void Note_LocationChanged(object sender, EventArgs e)
		{
		}

		private void menuItemUsers_Click(object sender, EventArgs e)
		{
			if (this.OnAssignNoteUsers != null)
			{
				this.OnAssignNoteUsers(this, null);
			}
		}

		private void textControl_Leave(object sender, EventArgs e)
		{
			if (this.OnNoteInActivated != null)
			{
				this.OnNoteInActivated(this, null);
			}
		}

		private void pictureBoxFlag_Click(object sender, EventArgs e)
		{
			BringToFront();
		}

		private void panel1_Click(object sender, EventArgs e)
		{
			BringToFront();
		}

		private void Note_Click(object sender, EventArgs e)
		{
			BringToFront();
		}

		private void textControl_MouseEnter(object sender, EventArgs e)
		{
			BringToFront();
		}

		public void AssingNoteUsers(ArrayList users)
		{
			try
			{
				Save();
				if (NoteID != -1)
				{
					Factory.NoteSystem.AssignNoteUsers(NoteID, users, NoteFlag);
				}
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

		public void Save()
		{
			if (this.OnSaveNote != null)
			{
				this.OnSaveNote(this, null);
			}
		}

		private void menuItemSaveNote_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void menuItemFlagRecycled_Click(object sender, EventArgs e)
		{
			NoteFlag = NoteFlags.Recycled;
			if (this.OnFlagChanged != null)
			{
				this.OnFlagChanged(this, null);
			}
			isDirty = true;
		}

		private void SetMenuFlagCheck()
		{
			switch (noteFlag)
			{
			case NoteFlags.Recycled:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = false;
				menuItemFlagRecycled.Checked = true;
				break;
			case NoteFlags.Important:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = true;
				menuItemFlagRecycled.Checked = false;
				break;
			case NoteFlags.ToDo:
				menuItemFlagToDo.Checked = true;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = false;
				menuItemFlagRecycled.Checked = false;
				break;
			case NoteFlags.Reminder:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = true;
				menuItemFlagQuestion.Checked = false;
				menuItemFlagImportant.Checked = false;
				menuItemFlagRecycled.Checked = false;
				break;
			case NoteFlags.Question:
				menuItemFlagToDo.Checked = false;
				menuItemFlagReminder.Checked = false;
				menuItemFlagQuestion.Checked = true;
				menuItemFlagImportant.Checked = false;
				menuItemFlagRecycled.Checked = false;
				break;
			}
		}

		private void textControl_BackColorChanged(object sender, EventArgs e)
		{
			NoteBackColor = textControl.BackColor;
			if (this.NoteColorChanged != null)
			{
				this.NoteColorChanged(sender, e);
			}
		}

		private void textControl_MouseDown(object sender, MouseEventArgs e)
		{
			BringToFront();
		}

		private void textControl_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				Process.Start(e.LinkText);
			}
			catch
			{
			}
		}

		private void Note_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void SetReminderIcon()
		{
			NoteFlag = noteFlag;
		}

		private void menuItemSetReminder_Click(object sender, EventArgs e)
		{
			using (SetReminderForm setReminderForm = new SetReminderForm())
			{
				if (reminderDate != DateTime.MinValue)
				{
					setReminderForm.ReminderDate = reminderDate;
				}
				else
				{
					setReminderForm.ReminderDate = DateTime.Now;
				}
				setReminderForm.ShowDialog();
				if (setReminderForm.DialogResult == DialogResult.OK)
				{
					isDirty = true;
					reminderDate = setReminderForm.ReminderDate;
					SetReminderIcon();
					if (this.OnReminderCreated != null)
					{
						this.OnReminderCreated(this, null);
					}
				}
			}
		}

		internal void SetNoteTitle(NoteTitles noteTitle)
		{
			switch (noteTitle)
			{
			case NoteTitles.Date:
				labelBottom.Text = DateTime.Now.ToLongDateString();
				break;
			case NoteTitles.DateAndTime:
				labelBottom.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
				break;
			case NoteTitles.FirstLine:
				TitleText = ToolTipText;
				break;
			default:
				labelBottom.Text = DateTime.Now.ToLongDateString();
				break;
			}
			isLocationColorTitleReminderDateDirty = true;
		}

		private void menuItemTitleTodayDate_Click(object sender, EventArgs e)
		{
			SetNoteTitle(NoteTitles.Date);
		}

		private void menuItemTitleTodayDateTime_Click(object sender, EventArgs e)
		{
			SetNoteTitle(NoteTitles.DateAndTime);
		}

		private void menuItemTitleFirstLineFromNote_Click(object sender, EventArgs e)
		{
			SetNoteTitle(NoteTitles.FirstLine);
		}

		private void labelBottom_MouseEnter(object sender, EventArgs e)
		{
		}

		private void labelBottom_MouseDown(object sender, MouseEventArgs e)
		{
			BringToFront();
		}

		private void textControl_TextChanged_1(object sender, EventArgs e)
		{
			isDirty = true;
		}

		private void labelBottom_DoubleClick(object sender, EventArgs e)
		{
			RestoreView(minimize: false);
		}

		private void menuItemNoteIsInactive_Click(object sender, EventArgs e)
		{
			IsInactive = !isInactive;
			isDirty = true;
			if (this.InactivateNoteChanged != null)
			{
				this.InactivateNoteChanged(this, null);
			}
		}

		private void menuItemExportToMSOutlook_Click(object sender, EventArgs e)
		{
		}

		private void panelHeader_MouseMove(object sender, MouseEventArgs e)
		{
			if (!Resizeable || IsMinimized())
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				if (isMoving)
				{
					SetBounds(base.Location.X + (e.X - prevX), base.Location.Y + (e.Y - prevY), base.Width, base.Height);
					isLocationColorTitleReminderDateDirty = true;
				}
				if (leftResize)
				{
					if (base.Width - e.X > MinWidth)
					{
						base.Width -= e.X;
					}
					base.Left += e.X;
				}
				else if (rightResize)
				{
					if (base.Width + e.X - base.Width > MinWidth)
					{
						base.Width += e.X - base.Width;
					}
				}
				else if (downResize)
				{
					if (base.Height + e.Y - base.Height > MinHeight)
					{
						base.Height += e.Y - base.Height;
					}
				}
				else if (topResize)
				{
					base.Top += e.Y;
					if (base.Height - e.Y > MinHeight)
					{
						base.Height -= e.Y;
					}
				}
				else if (rightDownResize)
				{
					if (base.Height + e.Y - base.Height > MinHeight)
					{
						base.Height += e.Y - base.Height;
					}
					if (base.Width + e.X - base.Width > MinWidth)
					{
						base.Width += e.X - base.Width;
					}
				}
				else if (leftTopResize)
				{
					if (base.Height - e.Y > MinHeight)
					{
						base.Height -= e.Y;
					}
					if (base.Width - e.X > MinWidth)
					{
						base.Width -= e.X;
					}
					base.Top += e.Y;
					base.Left += e.X;
				}
				else if (rightTopResize)
				{
					if (base.Height - e.Y > MinHeight)
					{
						base.Height -= e.Y;
					}
					base.Top += e.Y;
					if (base.Width + e.X - base.Width > MinWidth)
					{
						base.Width += e.X - base.Width;
					}
				}
				else if (leftDownResize)
				{
					base.Left += e.X;
					if (panelMain.Width - e.X > MinWidth)
					{
						base.Width -= e.X;
					}
					if (panelMain.Height + e.Y - panelMain.Height > MinHeight)
					{
						base.Height += e.Y - panelMain.Height;
					}
				}
			}
			else if ((e.X > base.Width - 5 && e.Y > base.Height - 5) || (e.X < 5 && e.Y < 5))
			{
				Cursor = Cursors.SizeNWSE;
			}
			else if ((e.X > base.Width - 5 && e.Y < 5) || (e.X < 5 && e.Y > base.Height - 5))
			{
				Cursor = Cursors.SizeNESW;
			}
			else if (e.X < 5 || e.X > base.Width - 5)
			{
				Cursor = Cursors.SizeWE;
			}
			else if (e.Y < 5 || e.Y > base.Height - 5)
			{
				Cursor = Cursors.SizeNS;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}

		private void panelHeader_MouseDown(object sender, MouseEventArgs e)
		{
			Focus();
			BringToFront();
			isMoving = true;
			if (e.X < 0)
			{
				prevX = 0;
			}
			else
			{
				prevX = e.X;
			}
			if (e.Y < 0)
			{
				prevY = 0;
			}
			else
			{
				prevY = e.Y;
			}
		}

		private void panelHeader_MouseUp(object sender, MouseEventArgs e)
		{
			rightResize = false;
			leftResize = false;
			topResize = false;
			downResize = false;
			rightTopResize = false;
			rightDownResize = false;
			leftTopResize = false;
			leftDownResize = false;
			isMoving = false;
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
				if (lastX < 100)
				{
					lastX = 100;
				}
				if (lastY < 55)
				{
					lastY = 55;
				}
				base.Size = new Size(lastX, lastY);
			}
		}

		private void panelBottom_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(Pens.Silver, 10, 1, panelBottom.Width - 15, 1);
		}

		private void Note_Enter(object sender, EventArgs e)
		{
			panelHeader.BackColor = SystemColors.ActiveCaption;
		}

		private void Note_Leave(object sender, EventArgs e)
		{
			panelHeader.BackColor = SystemColors.InactiveCaption;
		}

		private void panelHeader_Click(object sender, EventArgs e)
		{
			textControl.Focus();
			BringToFront();
		}

		private void pictureBoxClose_Click(object sender, EventArgs e)
		{
			Minimize();
		}

		private void pictureBoxFlag_Click_1(object sender, EventArgs e)
		{
			contextMenu.Show(pictureBoxFlag, new Point(pictureBoxFlag.Location.X, pictureBoxFlag.Location.Y + pictureBoxFlag.Height));
		}

		private void labelTitle_MouseMove(object sender, MouseEventArgs e)
		{
			panelHeader_MouseMove(sender, e);
		}

		private void labelTitle_MouseDown(object sender, MouseEventArgs e)
		{
			panelHeader_MouseDown(sender, e);
		}

		private void labelTitle_DoubleClick(object sender, EventArgs e)
		{
			if (IsMaximized)
			{
				if (prevHeight != -1 && prevWidth != -1)
				{
					base.Height = prevHeight;
					base.Width = prevWidth;
				}
				IsMaximized = false;
				return;
			}
			prevHeight = base.Height;
			prevWidth = base.Width;
			if (this.OnMaximize != null)
			{
				this.OnMaximize(this, null);
				IsMaximized = true;
			}
		}

		private void menuItemNewNote_Click(object sender, EventArgs e)
		{
			if (this.OnNewNote != null)
			{
				this.OnNewNote(null, null);
			}
		}
	}
}
