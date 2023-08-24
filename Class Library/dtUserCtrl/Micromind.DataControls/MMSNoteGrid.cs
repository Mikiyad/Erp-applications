using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.ComponentModel;
using System.Data;

namespace Micromind.DataControls
{
	public class MMSNoteGrid : UltraGrid
	{
		private IContainer components;

		public MMSNoteGrid()
		{
			InitializeComponent();
		}

		public MMSNoteGrid(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataSet().Tables.Add();
				dataTable.Columns.Add("CommentID", typeof(int));
				dataTable.Columns.Add("Comment");
				dataTable.Columns.Add("Info");
				base.DataSource = dataTable;
				Activation activation3 = base.DisplayLayout.Bands[0].Columns["CommentID"].CellActivation = (base.DisplayLayout.Bands[0].Columns["Comment"].CellActivation = Activation.NoEdit);
				base.DisplayLayout.Bands[0].Columns["Info"].CellActivation = Activation.NoEdit;
				base.DisplayLayout.Bands[0].Columns["CommentID"].Hidden = true;
				base.DisplayLayout.GroupByBox.Hidden = true;
				base.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
				base.DisplayLayout.Override.DefaultRowHeight = 40;
				new UltraGridLayout();
				base.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.ColumnLayout;
				base.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
				base.DisplayLayout.Bands[0].Override.RowSpacingAfter = 7;
				base.DisplayLayout.Bands[0].Override.AllowColSwapping = AllowColSwapping.NotAllowed;
				base.DisplayLayout.Bands[0].Override.BorderStyleCell = UIElementBorderStyle.None;
				base.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.Rounded4;
				base.DisplayLayout.Bands[0].ColHeadersVisible = false;
				base.DisplayLayout.Bands[0].Columns["Comment"].RowLayoutColumnInfo.OriginX = 1;
				base.DisplayLayout.Bands[0].Columns["Comment"].RowLayoutColumnInfo.OriginY = 0;
				base.DisplayLayout.Bands[0].Indentation = 2;
				base.DisplayLayout.Bands[0].Override.CellMultiLine = DefaultableBoolean.True;
				base.DisplayLayout.Bands[0].Override.CellSpacing = 10;
				base.DisplayLayout.Bands[0].Override.RowSizing = RowSizing.AutoFixed;
				base.DisplayLayout.Bands[0].Override.RowSizingAutoMaxLines = 3;
				base.DisplayLayout.Bands[0].Override.RowSpacingBefore = 1;
				base.DisplayLayout.Bands[0].Override.RowSpacingAfter = 4;
				base.DisplayLayout.Bands[0].Columns["Comment"].RowLayoutColumnInfo.LabelSpan = 1;
				base.DisplayLayout.Bands[0].Columns["Comment"].RowLayoutColumnInfo.SpanX = 2;
				base.DisplayLayout.Bands[0].Columns["Comment"].RowLayoutColumnInfo.SpanY = 1;
				base.DisplayLayout.Bands[0].Columns["Info"].RowLayoutColumnInfo.OriginX = 1;
				base.DisplayLayout.Bands[0].Columns["Info"].RowLayoutColumnInfo.OriginY = 2;
				base.DisplayLayout.Bands[0].Columns["Info"].RowLayoutColumnInfo.LabelSpan = 2;
				base.DisplayLayout.Bands[0].Columns["Info"].RowLayoutColumnInfo.SpanX = 2;
				base.DisplayLayout.Bands[0].Columns["Info"].RowLayoutColumnInfo.SpanY = 2;
				base.DisplayLayout.Bands[0].Columns["Info"].CellAppearance.FontData.SizeInPoints = 7f;
			}
			catch
			{
				throw;
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
		}
	}
}
