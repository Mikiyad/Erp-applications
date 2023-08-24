using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class ScreenNoteForm : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private bool isDirty = true;

		private string oldText = "";

		private NoteData data;

		private int noteID = -1;

		private int screenID = -1;

		private string noteColor;

		private Panel panelBottom;

		private XPButton buttonDelete;

		private XPButton buttonAttach;

		private Label labelLastUpdated;

		private TextControl note;

		private Panel panelTop;

		private Label labelHeader;

		private Timer timer1;

		private IContainer components;

		public string HeaderText
		{
			set
			{
				labelHeader.Text = value;
			}
		}

		public string NoteText => note.Text;

		public ScreenNoteForm()
		{
			InitializeComponent();
			base.Opacity = 0.0;
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Main.ScreenNoteForm));
			panelBottom = new System.Windows.Forms.Panel();
			buttonAttach = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			panelTop = new System.Windows.Forms.Panel();
			labelLastUpdated = new System.Windows.Forms.Label();
			labelHeader = new System.Windows.Forms.Label();
			timer1 = new System.Windows.Forms.Timer(components);
			note = new Micromind.UISupport.TextControl();
			panelBottom.SuspendLayout();
			panelTop.SuspendLayout();
			SuspendLayout();
			panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelBottom.Controls.Add(buttonAttach);
			panelBottom.Controls.Add(buttonDelete);
			componentResourceManager.ApplyResources(panelBottom, "panelBottom");
			panelBottom.Name = "panelBottom";
			buttonAttach.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAttach.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAttach.BtnStyle = Micromind.UISupport.XPStyle.Default;
			componentResourceManager.ApplyResources(buttonAttach, "buttonAttach");
			buttonAttach.Name = "buttonAttach";
			buttonAttach.Click += new System.EventHandler(buttonAttach_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			componentResourceManager.ApplyResources(buttonDelete, "buttonDelete");
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			panelTop.BackColor = System.Drawing.Color.FromArgb(245, 249, 219);
			panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelTop.Controls.Add(labelLastUpdated);
			panelTop.Controls.Add(labelHeader);
			componentResourceManager.ApplyResources(panelTop, "panelTop");
			panelTop.ForeColor = System.Drawing.Color.Black;
			panelTop.Name = "panelTop";
			componentResourceManager.ApplyResources(labelLastUpdated, "labelLastUpdated");
			labelLastUpdated.BackColor = System.Drawing.Color.Transparent;
			labelLastUpdated.ForeColor = System.Drawing.Color.Black;
			labelLastUpdated.Name = "labelLastUpdated";
			componentResourceManager.ApplyResources(labelHeader, "labelHeader");
			labelHeader.BackColor = System.Drawing.Color.Transparent;
			labelHeader.ForeColor = System.Drawing.Color.Black;
			labelHeader.Name = "labelHeader";
			timer1.Enabled = true;
			timer1.Interval = 80;
			timer1.Tick += new System.EventHandler(timer1_Tick);
			note.AcceptsTab = true;
			note.BackColor = System.Drawing.Color.LightYellow;
			componentResourceManager.ApplyResources(note, "note");
			note.Name = "note";
			note.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(note_LinkClicked);
			note.BackColorChanged += new System.EventHandler(note_BackColorChanged);
			componentResourceManager.ApplyResources(this, "$this");
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.Controls.Add(note);
			base.Controls.Add(panelTop);
			base.Controls.Add(panelBottom);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ScreenNoteForm";
			base.Load += new System.EventHandler(ScreenNoteForm_Load);
			base.Activated += new System.EventHandler(ScreenNoteForm_Activated);
			base.Closing += new System.ComponentModel.CancelEventHandler(ScreenNoteForm_Closing);
			base.Resize += new System.EventHandler(ScreenNoteForm_Resize);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(ScreenNoteForm_KeyDown);
			panelBottom.ResumeLayout(false);
			panelTop.ResumeLayout(false);
			panelTop.PerformLayout();
			ResumeLayout(false);
		}

		public void LoadNote(int screenID)
		{
			try
			{
				if (data != null)
				{
					data.Dispose();
					data = null;
				}
				this.screenID = screenID;
				PublicFunctions.StartWaiting(this);
				data = Factory.NoteSystem.GetNoteByScreenID(Global.CurrentUserID, screenID);
				if (data != null && data.NoteTable.Rows.Count > 0)
				{
					note.LoadRTFFormatText(data.NoteTable.Rows[0]["NoteText"].ToString());
					try
					{
						DateTime dateTime = DateTime.Parse(data.NoteTable.Rows[0]["DateTimeStamp"].ToString());
						labelLastUpdated.Text = dateTime.ToShortDateString() + "     " + dateTime.ToLongTimeString();
					}
					catch
					{
					}
					noteColor = data.NoteTable.Rows[0]["NoteColor"].ToString();
					noteID = int.Parse(data.NoteTable.Rows[0]["NoteID"].ToString());
					oldText = note.Text;
					note.LoadBackcolor(noteColor);
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
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private bool SaveNote()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				string rTFFormatText = note.GetRTFFormatText();
				if (rTFFormatText.Length > 50000)
				{
					ErrorHelper.WarningMessage("Note size is too big. Please reduce the note size.");
					return false;
				}
				if (data != null && data.NoteTable.Rows.Count > 0)
				{
					DataRow dataRow = data.NoteTable.Rows[0];
					dataRow["NoteText"] = rTFFormatText;
					int currentUserID = Global.CurrentUserID;
					if (currentUserID != -1)
					{
						dataRow["CreatedBy"] = currentUserID;
					}
					dataRow["NoteScreenID"] = screenID;
					dataRow["NoteColor"] = note.GetBackColor();
					Factory.NoteSystem.UpdateNote(data);
				}
				else
				{
					NoteData noteData = new NoteData();
					DataRow dataRow2 = noteData.NoteTable.NewRow();
					dataRow2["NoteText"] = rTFFormatText;
					dataRow2["NoteScreenID"] = screenID;
					dataRow2["NoteColor"] = note.GetBackColor();
					int currentUserID2 = Global.CurrentUserID;
					if (currentUserID2 != -1)
					{
						dataRow2["CreatedBy"] = currentUserID2;
					}
					noteData.NoteTable.Rows.Add(dataRow2);
					Factory.NoteSystem.CreateNote(noteData);
				}
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
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			isDirty = false;
			return true;
		}

		private void buttonAttach_Click(object sender, EventArgs e)
		{
			if (SaveNote())
			{
				Close();
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (data != null && data.NoteTable.Rows.Count > 0)
				{
					if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this note?") != DialogResult.Yes)
					{
						return;
					}
					PublicFunctions.StartWaiting(this);
					Factory.NoteSystem.DeleteNote(noteID);
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				return;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			isDirty = false;
			Close();
		}

		private void ScreenNoteForm_Closing(object sender, CancelEventArgs e)
		{
			if (isDirty && note.Text != oldText && ErrorHelper.QuestionMessageYesNo("Would you like to save changes to this note?") == DialogResult.Yes && !SaveNote())
			{
				e.Cancel = true;
			}
			SaveFormLocation();
		}

		private void ScreenNoteForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				note.Focus();
				LoadFormLocation();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SaveFormLocation()
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void LoadFormLocation()
		{
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void ScreenNoteForm_Resize(object sender, EventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void note_BackColorChanged(object sender, EventArgs e)
		{
			panelTop.BackColor = note.BackColor;
		}

		private void ScreenNoteForm_Activated(object sender, EventArgs e)
		{
			note.Focus();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			base.Opacity += base.Opacity + 0.2;
			checked
			{
				if (base.Opacity > 0.6)
				{
					timer1.Interval -= 20;
				}
				if (base.Opacity == 1.0)
				{
					timer1.Enabled = false;
				}
			}
		}

		private void note_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			try
			{
				Process.Start(e.LinkText);
			}
			catch
			{
			}
		}

		private void ScreenNoteForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
