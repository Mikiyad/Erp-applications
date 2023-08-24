using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport.Controls
{
	[ToolboxItem(false)]
	[DefaultProperty("Title")]
	[DefaultEvent("PropertyChanged")]
	public class TabPage : Panel
	{
		public enum Property
		{
			Title,
			Control,
			ImageIndex,
			ImageList,
			Icon,
			Selected,
			TabCellWidth,
			Visible
		}

		public delegate void PropChangeHandler(TabPage page, Property prop, object oldValue);

		private bool tabVisible = true;

		protected string _title;

		protected Control _control;

		protected int _imageIndex;

		protected ImageList _imageList;

		protected Icon _icon;

		protected bool _selected;

		protected Control _startFocus;

		protected bool _shown;

		protected int _tabCellWidth;

		[DefaultValue("Page")]
		[Localizable(true)]
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title != value)
				{
					string title = _title;
					_title = value;
					OnPropertyChanged(Property.Title, title);
				}
			}
		}

		public bool TabVisible
		{
			get
			{
				return tabVisible;
			}
			set
			{
				if (tabVisible != value)
				{
					bool flag = tabVisible;
					tabVisible = value;
					OnPropertyChanged(Property.Visible, flag);
				}
			}
		}

		[DefaultValue("0")]
		[Localizable(true)]
		public int TabCellWidth
		{
			get
			{
				return _tabCellWidth;
			}
			set
			{
				if (_tabCellWidth != value)
				{
					int tabCellWidth = _tabCellWidth;
					_tabCellWidth = value;
					OnPropertyChanged(Property.TabCellWidth, tabCellWidth);
				}
			}
		}

		[DefaultValue(null)]
		public Control Control
		{
			get
			{
				return _control;
			}
			set
			{
				if (_control != value)
				{
					Control control = _control;
					_control = value;
					OnPropertyChanged(Property.Control, control);
				}
			}
		}

		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return _imageIndex;
			}
			set
			{
				if (_imageIndex != value)
				{
					int imageIndex = _imageIndex;
					_imageIndex = value;
					OnPropertyChanged(Property.ImageIndex, imageIndex);
				}
			}
		}

		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return _imageList;
			}
			set
			{
				if (_imageList != value)
				{
					ImageList imageList = _imageList;
					_imageList = value;
					OnPropertyChanged(Property.ImageList, imageList);
				}
			}
		}

		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				if (_icon != value)
				{
					Icon icon = _icon;
					_icon = value;
					OnPropertyChanged(Property.Icon, icon);
				}
			}
		}

		[DefaultValue(true)]
		public bool Selected
		{
			get
			{
				return _selected;
			}
			set
			{
				if (_selected != value)
				{
					bool selected = _selected;
					_selected = value;
					OnPropertyChanged(Property.Selected, selected);
				}
			}
		}

		[DefaultValue(null)]
		public Control StartFocus
		{
			get
			{
				return _startFocus;
			}
			set
			{
				_startFocus = value;
			}
		}

		internal bool Shown
		{
			get
			{
				return _shown;
			}
			set
			{
				_shown = value;
			}
		}

		public event PropChangeHandler PropertyChanged;

		public TabPage()
		{
			InternalConstruct("Page", null, null, -1, null);
		}

		public TabPage(string title)
		{
			InternalConstruct(title, null, null, -1, null);
		}

		public TabPage(string title, Control control)
		{
			InternalConstruct(title, control, null, -1, null);
		}

		public TabPage(string title, Control control, int imageIndex)
		{
			InternalConstruct(title, control, null, imageIndex, null);
		}

		public TabPage(string title, Control control, ImageList imageList, int imageIndex)
		{
			InternalConstruct(title, control, imageList, imageIndex, null);
		}

		public TabPage(string title, Control control, Icon icon)
		{
			InternalConstruct(title, control, null, -1, icon);
		}

		protected void InternalConstruct(string title, Control control, ImageList imageList, int imageIndex, Icon icon)
		{
			_title = title;
			_control = control;
			_imageIndex = imageIndex;
			_imageList = imageList;
			_icon = icon;
			_selected = false;
			_startFocus = null;
		}

		private void InitializeComponent()
		{
			base.TabStop = true;
		}

		public virtual void OnPropertyChanged(Property prop, object oldValue)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, prop, oldValue);
			}
		}
	}
}
