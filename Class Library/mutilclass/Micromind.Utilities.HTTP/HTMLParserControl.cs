using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.Utilities.HTTP
{
	public class HTMLParserControl : UserControl
	{
		private ParseHTML parse = new ParseHTML();

		private Container components;

		public string SourceURL
		{
			get
			{
				return parse.SourceURL;
			}
			set
			{
				parse.SourceURL = value;
			}
		}

		public string Source
		{
			get
			{
				return Source;
			}
			set
			{
				Source = parse.Source;
			}
		}

		public HTMLParserControl()
		{
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
			components = new System.ComponentModel.Container();
		}

		public char Parse()
		{
			return parse.Parse();
		}

		public string GetContent()
		{
			return parse.GetContent();
		}

		public string GetMeta(string metaName)
		{
			return parse.GetMeta(metaName);
		}

		public string BuildTag()
		{
			return parse.BuildTag();
		}

		public AttributeList GetTag()
		{
			return parse.GetTag();
		}

		public string GetPage(string url)
		{
			return new HTTPClient(null, null).GetPage(url);
		}

		public bool Eof()
		{
			return parse.Eof();
		}
	}
}
