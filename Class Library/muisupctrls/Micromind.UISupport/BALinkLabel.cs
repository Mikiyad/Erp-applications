using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class BALinkLabel : LinkLabel
	{
		private bool tobeAligned = true;

		private Container components;

		private string originalText = "";

		private string description = "";

		private bool availableInEdition = true;

		public bool ToBeAligned
		{
			get
			{
				return tobeAligned;
			}
			set
			{
				tobeAligned = value;
			}
		}

		public string OriginalText
		{
			get
			{
				return originalText;
			}
			set
			{
				originalText = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public bool AvailableInEdition
		{
			get
			{
				return availableInEdition;
			}
			set
			{
				availableInEdition = value;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				LinkArea linkArea = base.LinkArea;
				linkArea.Length = value.Length + 50;
				base.LinkArea = linkArea;
			}
		}

		public BALinkLabel()
		{
			InitializeComponent();
			LinkArea linkArea = base.LinkArea;
			linkArea.Start = 0;
			linkArea.Length = Text.Length + 100;
			base.LinkArea = linkArea;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public override string ToString()
		{
			return Text;
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
	}
}
