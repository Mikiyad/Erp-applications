using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;

namespace Micromind.UISupport.GUIControls.Others
{
	public class MMSPictureBox : PictureEdit
	{
		private IContainer components;

		private new RepositoryItemPictureEdit fProperties;

		public MMSPictureBox()
		{
			InitializeComponent();
		}

		public MMSPictureBox(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
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
			fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			((System.ComponentModel.ISupportInitialize)fProperties).BeginInit();
			SuspendLayout();
			fProperties.AllowAnimationOnValueChanged = DevExpress.Utils.DefaultBoolean.True;
			fProperties.AllowScrollViaMouseDrag = true;
			fProperties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
			fProperties.Name = "fProperties";
			fProperties.ShowMenu = false;
			fProperties.ShowScrollBars = true;
			fProperties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
			fProperties.ZoomAccelerationFactor = 5.0;
			fProperties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.MouseWheel;
			((System.ComponentModel.ISupportInitialize)fProperties).EndInit();
			ResumeLayout(false);
		}
	}
}
