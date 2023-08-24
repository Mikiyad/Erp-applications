using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class EntityCommentsControl : UserControl
	{
		private IContainer components;

		private UltraPanel ultraPanel1;

		private UltraPanel ultraPanel2;

		private MMSNoteGrid mmsNoteGrid1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem newCommentToolStripMenuItem;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem printToolStripMenuItem;

		public string EntityID
		{
			get;
			set;
		}

		public string EntitySysDocID
		{
			get;
			set;
		}

		public EntityTypesEnum EntityType
		{
			get;
			set;
		}

		public MMSNoteGrid Grid => mmsNoteGrid1;

		public EntityCommentsControl()
		{
			InitializeComponent();
			Grid.DoubleClickRow += Grid_DoubleClickRow;
		}

		private void Grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			Edit();
		}

		public void NewComment()
		{
			EditCommentDialog editCommentDialog = new EditCommentDialog();
			editCommentDialog.IsNew = true;
			if (editCommentDialog.ShowDialog() == DialogResult.OK && SaveComment(-1, editCommentDialog.Comment, isUpdate: false))
			{
				_ = Grid.DataSource;
				DataTable obj = Grid.DataSource as DataTable;
				DataRow dataRow = obj.NewRow();
				dataRow["Comment"] = editCommentDialog.Comment;
				obj.Rows.Add(dataRow);
			}
		}

		private EntityCommentData GetData(int commentID, string comment)
		{
			EntityCommentData entityCommentData = new EntityCommentData();
			DataRow dataRow = entityCommentData.EntityCommentsTable.NewRow();
			if (commentID >= 0)
			{
				dataRow["CommentID"] = commentID;
			}
			else
			{
				dataRow["CommentID"] = -1;
			}
			dataRow["EntityID"] = EntityID;
			dataRow["EntityType"] = (int)EntityType;
			if (!EntitySysDocID.IsNullOrEmpty())
			{
				dataRow["EntitySysDocID"] = EntitySysDocID;
			}
			dataRow["Note"] = comment;
			entityCommentData.EntityCommentsTable.Rows.Add(dataRow);
			if (commentID >= 0)
			{
				entityCommentData.AcceptChanges();
				dataRow.SetModified();
			}
			return entityCommentData;
		}

		private bool SaveComment(int commentID, string comment, bool isUpdate)
		{
			try
			{
				EntityCommentData data = GetData(commentID, comment);
				bool flag = true;
				if (isUpdate)
				{
					return Factory.EntityCommentSystem.UpdateEntityComment(data);
				}
				return Factory.EntityCommentSystem.CreateEntityComment(data);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool Delete()
		{
			try
			{
				UltraGridRow activeRow = Grid.ActiveRow;
				if (activeRow == null)
				{
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo("Are you sure to delete this comment?") == DialogResult.Yes)
				{
					int commentID = int.Parse(activeRow.Cells["CommentID"].Value.ToString());
					if (Factory.EntityCommentSystem.DeleteEntityComment(commentID))
					{
						Grid.ActiveRow.Delete(displayPrompt: false);
						return true;
					}
					return false;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool LoadData(EntityTypesEnum entityType, string entityID, string sysDocID)
		{
			try
			{
				if (entityID == "")
				{
					return false;
				}
				DataSet entityCommentList = Factory.EntityCommentSystem.GetEntityCommentList(entityType, entityID);
				if (entityCommentList == null || entityCommentList.Tables.Count == 0 || entityCommentList.Tables[0].Rows.Count == 0)
				{
					return false;
				}
				DataTable dataTable = Grid.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in entityCommentList.Tables[0].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["CommentID"] = row["CommentID"];
					dataRow2["Comment"] = row["Note"];
					string text = "";
					text = "Added by: " + row["CreatedBy"].ToString();
					if (!row["DateCreated"].IsDBNullOrEmpty())
					{
						text = text + "  On: " + DateTime.Parse(row["DateCreated"].ToString());
					}
					if (!row["UpdatedBy"].IsDBNullOrEmpty())
					{
						text = text + "       Updated by: " + row["UpdatedBy"].ToString();
						if (!row["DateUpdated"].IsDBNullOrEmpty())
						{
							text = text + "  On: " + DateTime.Parse(row["DateUpdated"].ToString());
						}
					}
					dataRow2["Info"] = text;
					dataTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Grid.PrintPreview();
		}

		public void Edit()
		{
			try
			{
				if (Grid.ActiveRow != null)
				{
					UltraGridRow activeRow = Grid.ActiveRow;
					EditCommentDialog editCommentDialog = new EditCommentDialog();
					editCommentDialog.IsNew = false;
					editCommentDialog.Comment = activeRow.Cells["Comment"].Value.ToString();
					if (editCommentDialog.ShowDialog() == DialogResult.OK && SaveComment(int.Parse(activeRow.Cells["CommentID"].Value.ToString()), editCommentDialog.Comment, isUpdate: true))
					{
						activeRow.Cells["Comment"].Value = editCommentDialog.Comment;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void newCommentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewComment();
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Edit();
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
			ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
			ultraPanel2 = new Infragistics.Win.Misc.UltraPanel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			newCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			mmsNoteGrid1 = new Micromind.DataControls.MMSNoteGrid();
			ultraPanel1.SuspendLayout();
			ultraPanel2.ClientArea.SuspendLayout();
			ultraPanel2.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)mmsNoteGrid1).BeginInit();
			SuspendLayout();
			appearance.BorderColor = System.Drawing.Color.LightGray;
			ultraPanel1.Appearance = appearance;
			ultraPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ultraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			ultraPanel1.Location = new System.Drawing.Point(0, 455);
			ultraPanel1.Name = "ultraPanel1";
			ultraPanel1.Size = new System.Drawing.Size(812, 47);
			ultraPanel1.TabIndex = 1;
			ultraPanel1.Visible = false;
			appearance2.BorderColor = System.Drawing.Color.LightGray;
			ultraPanel2.Appearance = appearance2;
			ultraPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ultraPanel2.ClientArea.Controls.Add(mmsNoteGrid1);
			ultraPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraPanel2.Location = new System.Drawing.Point(0, 0);
			ultraPanel2.Name = "ultraPanel2";
			ultraPanel2.Size = new System.Drawing.Size(812, 455);
			ultraPanel2.TabIndex = 2;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				newCommentToolStripMenuItem,
				editToolStripMenuItem,
				deleteToolStripMenuItem,
				toolStripSeparator1,
				printToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(165, 120);
			newCommentToolStripMenuItem.Name = "newCommentToolStripMenuItem";
			newCommentToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			newCommentToolStripMenuItem.Text = "New Comment...";
			newCommentToolStripMenuItem.Click += new System.EventHandler(newCommentToolStripMenuItem_Click);
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			editToolStripMenuItem.Text = "Edit";
			editToolStripMenuItem.Click += new System.EventHandler(editToolStripMenuItem_Click);
			deleteToolStripMenuItem.Image = Micromind.DataControls.Properties.Resources.delete;
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
			printToolStripMenuItem.Name = "printToolStripMenuItem";
			printToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			printToolStripMenuItem.Text = "Print...";
			printToolStripMenuItem.Click += new System.EventHandler(printToolStripMenuItem_Click);
			mmsNoteGrid1.ContextMenuStrip = contextMenuStrip1;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			mmsNoteGrid1.DisplayLayout.Appearance = appearance3;
			mmsNoteGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			mmsNoteGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			mmsNoteGrid1.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			mmsNoteGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			mmsNoteGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			mmsNoteGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			mmsNoteGrid1.DisplayLayout.MaxColScrollRegions = 1;
			mmsNoteGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			mmsNoteGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			mmsNoteGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			mmsNoteGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			mmsNoteGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			mmsNoteGrid1.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			mmsNoteGrid1.DisplayLayout.Override.CellAppearance = appearance10;
			mmsNoteGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			mmsNoteGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			mmsNoteGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			mmsNoteGrid1.DisplayLayout.Override.HeaderAppearance = appearance12;
			mmsNoteGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			mmsNoteGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			mmsNoteGrid1.DisplayLayout.Override.RowAppearance = appearance13;
			mmsNoteGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			mmsNoteGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			mmsNoteGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			mmsNoteGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			mmsNoteGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			mmsNoteGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			mmsNoteGrid1.Location = new System.Drawing.Point(0, 0);
			mmsNoteGrid1.Name = "mmsNoteGrid1";
			mmsNoteGrid1.Size = new System.Drawing.Size(810, 453);
			mmsNoteGrid1.TabIndex = 1;
			mmsNoteGrid1.Text = "mmsNoteGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(ultraPanel2);
			base.Controls.Add(ultraPanel1);
			base.Name = "EntityCommentsControl";
			base.Size = new System.Drawing.Size(812, 502);
			ultraPanel1.ResumeLayout(false);
			ultraPanel2.ClientArea.ResumeLayout(false);
			ultraPanel2.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)mmsNoteGrid1).EndInit();
			ResumeLayout(false);
		}
	}
}
