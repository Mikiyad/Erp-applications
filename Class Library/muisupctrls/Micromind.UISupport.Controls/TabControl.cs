using Micromind.UISupport.Collections;
using Micromind.UISupport.Common;
using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Micromind.UISupport.Controls
{
	[ToolboxBitmap(typeof(TabControl))]
	[DefaultProperty("Appearance")]
	[DefaultEvent("SelectionChanged")]
	[Designer(typeof(TabControlDesigner))]
	public class TabControl : UserControl
	{
		public enum HideTabsModes
		{
			ShowAlways,
			HideAlways,
			HideUsingLogic,
			HideWithoutMouse
		}

		protected enum ImageStrip
		{
			LeftEnabled,
			LeftDisabled,
			RightEnabled,
			RightDisabled,
			Close,
			Error
		}

		protected enum PositionIndex
		{
			BorderTop,
			BorderLeft,
			BorderBottom,
			BorderRight,
			ImageGapTop,
			ImageGapLeft,
			ImageGapBottom,
			ImageGapRight,
			TextOffset,
			TextGapLeft,
			TabsBottomGap,
			ButtonOffset
		}

		protected class MultiRect
		{
			protected Rectangle _rect;

			protected int _index;

			public int Index => _index;

			public Rectangle Rect
			{
				get
				{
					return _rect;
				}
				set
				{
					_rect = value;
				}
			}

			public int X
			{
				get
				{
					return _rect.X;
				}
				set
				{
					_rect.X = value;
				}
			}

			public int Y
			{
				get
				{
					return _rect.Y;
				}
				set
				{
					_rect.Y = value;
				}
			}

			public int Width
			{
				get
				{
					return _rect.Width;
				}
				set
				{
					_rect.Width = value;
				}
			}

			public int Height
			{
				get
				{
					return _rect.Height;
				}
				set
				{
					_rect.Height = value;
				}
			}

			public MultiRect(Rectangle rect, int index)
			{
				_rect = rect;
				_index = index;
			}
		}

		protected class HostPanel : Panel
		{
			public HostPanel()
			{
				SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			}

			protected override void OnSizeChanged(EventArgs e)
			{
				foreach (Control control in base.Controls)
				{
					control.Size = base.Size;
				}
				base.OnSizeChanged(e);
			}
		}

		public delegate void DoubleClickTabHandler(TabControl sender, TabPage page);

		protected static int[,] _position;

		protected static int _plainBorder;

		protected static int _plainBorderDouble;

		protected static int _tabsAreaStartInset;

		protected static int _tabsAreaEndInset;

		protected static float _alphaIDE;

		protected static int _buttonGap;

		protected static int _buttonWidth;

		protected static int _buttonHeight;

		protected static int _imageButtonWidth;

		protected static int _imageButtonHeight;

		protected static int _multiBoxAdjust;

		protected readonly Rectangle _nullPosition = new Rectangle(-999, -999, 0, 0);

		protected static ImageList _internalImages;

		protected int _textHeight;

		protected int _imageWidth;

		protected int _imageHeight;

		protected int _imageGapTopExtra;

		protected int _imageGapBottomExtra;

		protected Rectangle _pageRect;

		protected Rectangle _pageAreaRect;

		protected Rectangle _tabsAreaRect;

		protected int _ctrlTopOffset;

		protected int _ctrlLeftOffset;

		protected int _ctrlRightOffset;

		protected int _ctrlBottomOffset;

		protected int _styleIndex;

		protected int _pageSelected;

		protected int _startPage;

		protected int _hotTrackPage;

		protected int _topYPos;

		protected int _bottomYPos;

		protected int _leaveTimeout;

		protected bool _dragFromControl;

		protected bool _mouseOver;

		protected bool _multiline;

		protected bool _multilineFullWidth;

		protected bool _shrinkPagesToFit;

		protected bool _changed;

		protected bool _positionAtTop;

		protected bool _showClose;

		protected bool _showArrows;

		protected bool _insetPlain;

		protected bool _insetBorderPagesOnly;

		protected bool _selectedTextOnly;

		protected bool _rightScroll;

		protected bool _leftScroll;

		protected bool _dimUnselected;

		protected bool _boldSelected;

		protected bool _hotTrack;

		protected bool _hoverSelect;

		protected bool _recalculate;

		protected bool _leftMouseDown;

		protected bool _leftMouseDownDrag;

		protected bool _ignoreDownDrag;

		protected bool _defaultColor;

		protected bool _defaultFont;

		protected bool _recordFocus;

		protected bool _idePixelArea;

		protected bool _idePixelBorder;

		protected ContextMenu _contextMenu;

		protected Point _leftMouseDownPos;

		protected Color _hotTextColor;

		protected Color _textColor = Color.FromArgb(16, 37, 127);

		protected Color _textInactiveColor;

		protected Color _backIDE = Color.FromArgb(239, 247, 253);

		protected Color _buttonActiveColor;

		protected Color _buttonInactiveColor;

		protected Color _backLight;

		protected Color _backLightLight;

		protected Color _backDark;

		protected Color _backDarkDark;

		protected Color _borderColor = Color.FromArgb(127, 157, 185);

		protected Color _unselectedTabColor = Color.FromArgb(191, 211, 237);

		protected VisualStyle _style;

		protected HideTabsModes _hideTabsMode;

		protected Timer _overTimer;

		protected HostPanel _hostPanel;

		protected VisualAppearance _appearance;

		protected ImageList _imageList;

		protected ArrayList _tabRects;

		protected TabPageCollection _tabPages;

		protected InertButton _closeButton;

		protected InertButton _leftArrow;

		protected InertButton _rightArrow;

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TabPageCollection TabPages => _tabPages;

		[Category("Appearance")]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (value != null && value != base.Font)
				{
					_defaultFont = (value == SystemInformation.MenuFont);
					DefineFont(value);
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public override Color ForeColor
		{
			get
			{
				return _textColor;
			}
			set
			{
				if (_textColor != value)
				{
					_textColor = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		public Color BorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				if (_borderColor != value)
				{
					_borderColor = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (BackColor != value)
				{
					_defaultColor = (value == SystemColors.Control);
					DefineBackColor(value);
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual Color ButtonActiveColor
		{
			get
			{
				return _buttonActiveColor;
			}
			set
			{
				if (_buttonActiveColor != value)
				{
					_buttonActiveColor = value;
					DefineButtonImages();
				}
			}
		}

		[Category("Appearance")]
		public virtual Color ButtonInactiveColor
		{
			get
			{
				return _buttonInactiveColor;
			}
			set
			{
				if (_buttonInactiveColor != value)
				{
					_buttonInactiveColor = value;
					DefineButtonImages();
				}
			}
		}

		public virtual VisualAppearance Appearance
		{
			get
			{
				return _appearance;
			}
			set
			{
				if (_appearance != value)
				{
					SetAppearance(value);
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(VisualStyle), "IDE")]
		public virtual VisualStyle Style
		{
			get
			{
				return _style;
			}
			set
			{
				if (_style != value)
				{
					_style = value;
					SetStyleIndex();
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Behavour")]
		public virtual ContextMenu ContextPopupMenu
		{
			get
			{
				return _contextMenu;
			}
			set
			{
				_contextMenu = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool HotTrack
		{
			get
			{
				return _hotTrack;
			}
			set
			{
				if (_hotTrack != value)
				{
					_hotTrack = value;
					if (!_hotTrack)
					{
						_hotTrackPage = -1;
					}
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual Color HotTextColor
		{
			get
			{
				return _hotTextColor;
			}
			set
			{
				if (_hotTextColor != value)
				{
					_hotTextColor = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual Color TextColor
		{
			get
			{
				return _textColor;
			}
			set
			{
				if (_textColor != value)
				{
					_textColor = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual Color TextInactiveColor
		{
			get
			{
				return _textInactiveColor;
			}
			set
			{
				if (_textInactiveColor != value)
				{
					_textInactiveColor = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Browsable(false)]
		public virtual Rectangle TabsAreaRect => _tabsAreaRect;

		[Category("Appearance")]
		public virtual ImageList ImageList
		{
			get
			{
				return _imageList;
			}
			set
			{
				if (_imageList != value)
				{
					_imageList = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool PositionTop
		{
			get
			{
				return _positionAtTop;
			}
			set
			{
				if (_positionAtTop != value)
				{
					_positionAtTop = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool ShowClose
		{
			get
			{
				return _showClose;
			}
			set
			{
				if (_showClose != value)
				{
					_showClose = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool ShowArrows
		{
			get
			{
				return _showArrows;
			}
			set
			{
				if (_showArrows != value)
				{
					_showArrows = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool ShrinkPagesToFit
		{
			get
			{
				return _shrinkPagesToFit;
			}
			set
			{
				if (_shrinkPagesToFit != value)
				{
					_shrinkPagesToFit = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool BoldSelectedPage
		{
			get
			{
				return _boldSelected;
			}
			set
			{
				if (_boldSelected != value)
				{
					_boldSelected = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool MultilineFullWidth
		{
			get
			{
				return _multilineFullWidth;
			}
			set
			{
				if (_multilineFullWidth != value)
				{
					_multilineFullWidth = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool Multiline
		{
			get
			{
				return _multiline;
			}
			set
			{
				if (_multiline != value)
				{
					_multiline = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(0)]
		public virtual int ControlLeftOffset
		{
			get
			{
				return _ctrlLeftOffset;
			}
			set
			{
				if (_ctrlLeftOffset != value)
				{
					_ctrlLeftOffset = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(0)]
		public virtual int ControlTopOffset
		{
			get
			{
				return _ctrlTopOffset;
			}
			set
			{
				if (_ctrlTopOffset != value)
				{
					_ctrlTopOffset = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(0)]
		public virtual int ControlRightOffset
		{
			get
			{
				return _ctrlRightOffset;
			}
			set
			{
				if (_ctrlRightOffset != value)
				{
					_ctrlRightOffset = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(0)]
		public virtual int ControlBottomOffset
		{
			get
			{
				return _ctrlBottomOffset;
			}
			set
			{
				if (_ctrlBottomOffset != value)
				{
					_ctrlBottomOffset = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public virtual bool InsetPlain
		{
			get
			{
				return _insetPlain;
			}
			set
			{
				if (_insetPlain != value)
				{
					_insetPlain = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool InsetBorderPagesOnly
		{
			get
			{
				return _insetBorderPagesOnly;
			}
			set
			{
				if (_insetBorderPagesOnly != value)
				{
					_insetBorderPagesOnly = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		public virtual bool IDEPixelBorder
		{
			get
			{
				return _idePixelBorder;
			}
			set
			{
				if (_idePixelBorder != value)
				{
					_idePixelBorder = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public virtual bool IDEPixelArea
		{
			get
			{
				return _idePixelArea;
			}
			set
			{
				if (_idePixelArea != value)
				{
					_idePixelArea = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool SelectedTextOnly
		{
			get
			{
				return _selectedTextOnly;
			}
			set
			{
				if (_selectedTextOnly != value)
				{
					_selectedTextOnly = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Behavour")]
		[DefaultValue(200)]
		public int MouseLeaveTimeout
		{
			get
			{
				return _leaveTimeout;
			}
			set
			{
				if (_leaveTimeout != value)
				{
					_leaveTimeout = value;
					_overTimer.Interval = value;
				}
			}
		}

		[Category("Behavour")]
		[DefaultValue(true)]
		public bool DragFromControl
		{
			get
			{
				return _dragFromControl;
			}
			set
			{
				_dragFromControl = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue(typeof(HideTabsModes), "ShowAlways")]
		public virtual HideTabsModes HideTabsMode
		{
			get
			{
				return _hideTabsMode;
			}
			set
			{
				if (_hideTabsMode != value)
				{
					_hideTabsMode = value;
					Recalculate();
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		public virtual bool HoverSelect
		{
			get
			{
				return _hoverSelect;
			}
			set
			{
				if (_hoverSelect != value)
				{
					_hoverSelect = value;
					_recalculate = true;
					Invalidate();
				}
			}
		}

		[Category("Behavour")]
		[DefaultValue(true)]
		public virtual bool RecordFocus
		{
			get
			{
				return _recordFocus;
			}
			set
			{
				if (_recordFocus != value)
				{
					_recordFocus = value;
				}
			}
		}

		[Browsable(false)]
		[DefaultValue(-1)]
		public virtual int SelectedIndex
		{
			get
			{
				return _pageSelected;
			}
			set
			{
				if (value < 0 || value >= _tabPages.Count || _pageSelected == value)
				{
					return;
				}
				CancelEventArgs cancelEventArgs = new CancelEventArgs();
				OnSelectionChanging(cancelEventArgs);
				if (cancelEventArgs.Cancel)
				{
					return;
				}
				if (_pageSelected != -1)
				{
					DeselectPage(_tabPages[_pageSelected]);
				}
				_pageSelected = value;
				if (_pageSelected != -1)
				{
					SelectPage(_tabPages[_pageSelected]);
					if (_pageSelected < _startPage)
					{
						_startPage = _pageSelected;
					}
				}
				if (_boldSelected)
				{
					Recalculate();
					Invalidate();
				}
				OnSelectionChanged(EventArgs.Empty);
				Invalidate();
			}
		}

		[Browsable(false)]
		[DefaultValue(null)]
		public virtual TabPage SelectedTab
		{
			get
			{
				if (_pageSelected == -1)
				{
					return null;
				}
				return _tabPages[_pageSelected];
			}
			set
			{
				if (value != null)
				{
					int num = _tabPages.IndexOf(value);
					if (num != -1)
					{
						SelectedIndex = num;
					}
				}
			}
		}

		public event EventHandler ClosePressed;

		public event CancelEventHandler SelectionChanging;

		public event EventHandler SelectionChanged;

		public event EventHandler PageGotFocus;

		public event EventHandler PageLostFocus;

		public event CancelEventHandler PopupMenuDisplay;

		public event MouseEventHandler PageDragStart;

		public event MouseEventHandler PageDragMove;

		public event MouseEventHandler PageDragEnd;

		public event MouseEventHandler PageDragQuit;

		public event DoubleClickTabHandler DoubleClickTab;

		static TabControl()
		{
			_position = new int[2, 12]
			{
				{
					3,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					2,
					1,
					3,
					2
				},
				{
					6,
					2,
					2,
					3,
					3,
					1,
					1,
					0,
					1,
					1,
					2,
					0
				}
			};
			_plainBorder = 3;
			_plainBorderDouble = 6;
			_tabsAreaStartInset = 5;
			_tabsAreaEndInset = 5;
			_alphaIDE = 1.5f;
			_buttonGap = 3;
			_buttonWidth = 14;
			_buttonHeight = 14;
			_imageButtonWidth = 12;
			_imageButtonHeight = 12;
			_multiBoxAdjust = 2;
			_internalImages = ResourceHelper.LoadBitmapStrip(Type.GetType("Micromind.UISupport.Controls.TabControl"), "Micromind.UISupport.Resources.ImagesTabControl.bmp", new Size(_imageButtonWidth, _imageButtonHeight), new Point(0, 0));
		}

		public TabControl()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			_tabRects = new ArrayList();
			_tabPages = new TabPageCollection();
			_tabPages.Clearing += OnClearingPages;
			_tabPages.Cleared += OnClearedPages;
			_tabPages.Inserting += OnInsertingPage;
			_tabPages.Inserted += OnInsertedPage;
			_tabPages.Removing += OnRemovingPage;
			_tabPages.Removed += OnRemovedPage;
			_startPage = -1;
			_pageSelected = -1;
			_hotTrackPage = -1;
			_imageList = null;
			_insetPlain = true;
			_multiline = false;
			_multilineFullWidth = false;
			_dragFromControl = true;
			_mouseOver = false;
			_leftScroll = false;
			_defaultFont = true;
			_defaultColor = true;
			_rightScroll = false;
			_hoverSelect = false;
			_leftMouseDown = false;
			_ignoreDownDrag = true;
			_selectedTextOnly = false;
			_leftMouseDownDrag = false;
			_insetBorderPagesOnly = false;
			_hideTabsMode = HideTabsModes.ShowAlways;
			_recordFocus = true;
			_styleIndex = 1;
			_leaveTimeout = 200;
			_ctrlTopOffset = 0;
			_ctrlLeftOffset = 0;
			_ctrlRightOffset = 0;
			_ctrlBottomOffset = 0;
			_style = VisualStyle.IDE;
			_buttonActiveColor = Color.FromArgb(128, ForeColor);
			_buttonInactiveColor = _buttonActiveColor;
			_textColor = Control.DefaultForeColor;
			_textInactiveColor = Color.FromArgb(128, _textColor);
			_hotTextColor = SystemColors.ActiveCaption;
			_hostPanel = new HostPanel();
			_hostPanel.Location = new Point(-1, -1);
			_hostPanel.Size = new Size(0, 0);
			_hostPanel.MouseEnter += OnPageMouseEnter;
			_hostPanel.MouseLeave += OnPageMouseLeave;
			_closeButton = new InertButton(_internalImages, 4);
			_leftArrow = new InertButton(_internalImages, 0, 1);
			_rightArrow = new InertButton(_internalImages, 2, 3);
			_closeButton.BorderWidth = (_leftArrow.BorderWidth = (_rightArrow.BorderWidth = 1));
			_closeButton.Click += OnCloseButton;
			_leftArrow.Click += OnLeftArrow;
			_rightArrow.Click += OnRightArrow;
			_leftArrow.Size = (_rightArrow.Size = (_closeButton.Size = new Size(_buttonWidth, _buttonHeight)));
			base.Controls.AddRange(new Control[4]
			{
				_closeButton,
				_leftArrow,
				_rightArrow,
				_hostPanel
			});
			_imageWidth = 16;
			_imageHeight = 16;
			SetAppearance(VisualAppearance.Micromind);
			_overTimer = new Timer();
			_overTimer.Interval = _leaveTimeout;
			_overTimer.Tick += OnMouseTick;
			SystemEvents.UserPreferenceChanged += OnPreferenceChanged;
			DefineFont(SystemInformation.MenuFont);
			DefineBackColor(SystemColors.Control);
			DefineButtonImages();
			Recalculate();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				SystemEvents.UserPreferenceChanged -= OnPreferenceChanged;
			}
			base.Dispose(disposing);
		}

		private bool ShouldSerializeFont()
		{
			return !_defaultFont;
		}

		private bool ShouldSerializeForeColor()
		{
			return _textColor != Control.DefaultForeColor;
		}

		private bool ShouldSerializeBackColor()
		{
			return BackColor != SystemColors.Control;
		}

		private bool ShouldSerializeButtonActiveColor()
		{
			return _buttonActiveColor != Color.FromArgb(128, ForeColor);
		}

		public void ResetButtonActiveColor()
		{
			ButtonActiveColor = Color.FromArgb(128, ForeColor);
		}

		private bool ShouldSerializeButtonInactiveColor()
		{
			return _buttonInactiveColor != Color.FromArgb(128, ForeColor);
		}

		public void ResetButtonInactiveColor()
		{
			ButtonInactiveColor = Color.FromArgb(128, ForeColor);
		}

		public void ResetAppearance()
		{
			Appearance = VisualAppearance.MultiForm;
		}

		public void ResetStyle()
		{
			Style = VisualStyle.IDE;
		}

		protected bool ShouldSerializeContextPopupMenu()
		{
			return _contextMenu != null;
		}

		public void ResetContextPopupMenu()
		{
			ContextPopupMenu = null;
		}

		public void ResetHotTrack()
		{
			HotTrack = false;
		}

		private bool ShouldSerializeHotTextColor()
		{
			return _hotTextColor != SystemColors.ActiveCaption;
		}

		public void ResetHotTextColor()
		{
			HotTextColor = SystemColors.ActiveCaption;
		}

		private bool ShouldSerializeTextColor()
		{
			return _textColor != Control.DefaultForeColor;
		}

		public void ResetTextColor()
		{
			TextColor = Control.DefaultForeColor;
		}

		private bool ShouldSerializeTextInactiveColor()
		{
			return _textInactiveColor != Color.FromArgb(128, Control.DefaultForeColor);
		}

		public void TextTextInactiveColor()
		{
			TextInactiveColor = Color.FromArgb(128, Control.DefaultForeColor);
		}

		private bool ShouldSerializeImageList()
		{
			return _imageList != null;
		}

		public void ResetImageList()
		{
			ImageList = null;
		}

		protected bool ShouldSerializePositionTop()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return _positionAtTop;
			default:
				return !_positionAtTop;
			}
		}

		public void ResetPositionTop()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				PositionTop = false;
				break;
			default:
				PositionTop = true;
				break;
			}
		}

		protected bool ShouldSerializeShowClose()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return _showClose;
			default:
				return !_showClose;
			}
		}

		public void ResetShowClose()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				ShowClose = false;
				break;
			default:
				ShowClose = true;
				break;
			}
		}

		protected bool ShouldSerializeShowArrows()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return _showArrows;
			default:
				return !_showArrows;
			}
		}

		public void ResetShowArrows()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				ShowArrows = false;
				break;
			default:
				ShowArrows = true;
				break;
			}
		}

		protected bool ShouldSerializeShrinkPagesToFit()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return !_shrinkPagesToFit;
			default:
				return _shrinkPagesToFit;
			}
		}

		public void ResetShrinkPagesToFit()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				ShrinkPagesToFit = true;
				break;
			default:
				ShrinkPagesToFit = false;
				break;
			}
		}

		protected bool ShouldSerializeBoldSelectedPage()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return _boldSelected;
			default:
				return !_boldSelected;
			}
		}

		public void ResetBoldSelectedPage()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				BoldSelectedPage = false;
				break;
			default:
				BoldSelectedPage = true;
				break;
			}
		}

		public void ResetMultilineFullWidth()
		{
			MultilineFullWidth = false;
		}

		public void ResetMultiline()
		{
			Multiline = false;
		}

		public void ResetControlLeftOffset()
		{
			ControlLeftOffset = 0;
		}

		public void ResetControlTopOffset()
		{
			ControlTopOffset = 0;
		}

		public void ResetControlRightOffset()
		{
			ControlRightOffset = 0;
		}

		public void ResetControlBottomOffset()
		{
			ControlBottomOffset = 0;
		}

		public void ResetInsetPlain()
		{
			InsetPlain = true;
		}

		public void ResetInsetBorderPagesOnly()
		{
			InsetBorderPagesOnly = true;
		}

		protected bool ShouldSerializeIDEPixelBorder()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				return _idePixelBorder;
			default:
				return !_idePixelBorder;
			}
		}

		public void ResetIDEPixelBorder()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
				IDEPixelBorder = false;
				break;
			default:
				IDEPixelBorder = true;
				break;
			}
		}

		public void ResetIDEPixelArea()
		{
			IDEPixelArea = true;
		}

		public void ResetSelectedTextOnly()
		{
			SelectedTextOnly = false;
		}

		public void ResetMouseLeaveTimeout()
		{
			_leaveTimeout = 200;
		}

		public void ResetDragFromControl()
		{
			DragFromControl = true;
		}

		protected bool ShouldSerializeHideTabsMode()
		{
			return HideTabsMode != HideTabsModes.ShowAlways;
		}

		public void ResetHideTabsMode()
		{
			HideTabsMode = HideTabsModes.ShowAlways;
		}

		public void ResetHoverSelect()
		{
			HoverSelect = false;
		}

		public void ResetRecordFocus()
		{
			RecordFocus = true;
		}

		public virtual void MakePageVisible(TabPage page)
		{
			MakePageVisible(_tabPages.IndexOf(page));
		}

		public virtual void MakePageVisible(int index)
		{
			if (_shrinkPagesToFit || _multiline || index < 0 || index >= _tabPages.Count)
			{
				return;
			}
			if (index < _startPage)
			{
				_startPage = index;
				_recalculate = true;
				Invalidate();
				return;
			}
			int maximumDrawPos = GetMaximumDrawPos();
			Rectangle rectangle = (Rectangle)_tabRects[index];
			if (rectangle.Right < maximumDrawPos)
			{
				return;
			}
			int num = index;
			int num2 = maximumDrawPos - rectangle.Width - _tabsAreaRect.Left - _tabsAreaStartInset;
			while (num != 0)
			{
				Rectangle rectangle2 = (Rectangle)_tabRects[num - 1];
				if (rectangle2.Width > num2)
				{
					break;
				}
				num--;
				num2 -= rectangle2.Width;
			}
			_startPage = num;
			_recalculate = true;
			Invalidate();
		}

		protected override bool ProcessMnemonic(char key)
		{
			int count = _tabPages.Count;
			int num = SelectedIndex + 1;
			int num2 = 0;
			while (num2 < count)
			{
				if (num >= count)
				{
					num = 0;
				}
				TabPage tabPage = _tabPages[num];
				tabPage.Title.IndexOf('&');
				if (Control.IsMnemonic(key, tabPage.Title))
				{
					SelectedTab = tabPage;
					return true;
				}
				num2++;
				num++;
			}
			return false;
		}

		protected override void OnResize(EventArgs e)
		{
			Recalculate();
			Invalidate();
			base.OnResize(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			Recalculate();
			Invalidate();
			base.OnSizeChanged(e);
		}

		public virtual void OnPopupMenuDisplay(CancelEventArgs e)
		{
			if (this.PopupMenuDisplay != null)
			{
				this.PopupMenuDisplay(this, e);
			}
		}

		public virtual void OnSelectionChanging(CancelEventArgs e)
		{
			if (this.SelectionChanging != null)
			{
				this.SelectionChanging(this, e);
			}
		}

		public virtual void OnSelectionChanging(CancelEventArgs e, object pageIndex)
		{
			if (this.SelectionChanging != null)
			{
				this.SelectionChanging(pageIndex, e);
			}
		}

		public virtual void OnSelectionChanged(EventArgs e)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, e);
			}
		}

		public virtual void OnClosePressed(EventArgs e)
		{
			if (this.ClosePressed != null)
			{
				this.ClosePressed(this, e);
			}
		}

		public virtual void OnPageGotFocus(EventArgs e)
		{
			if (this.PageGotFocus != null)
			{
				this.PageGotFocus(this, e);
			}
		}

		public virtual void OnPageLostFocus(EventArgs e)
		{
			if (this.PageLostFocus != null)
			{
				this.PageLostFocus(this, e);
			}
		}

		public virtual void OnPageDragStart(MouseEventArgs e)
		{
			if (this.PageDragStart != null)
			{
				this.PageDragStart(this, e);
			}
		}

		public virtual void OnPageDragMove(MouseEventArgs e)
		{
			if (this.PageDragMove != null)
			{
				this.PageDragMove(this, e);
			}
		}

		public virtual void OnPageDragEnd(MouseEventArgs e)
		{
			if (this.PageDragEnd != null)
			{
				this.PageDragEnd(this, e);
			}
		}

		public virtual void OnPageDragQuit(MouseEventArgs e)
		{
			if (this.PageDragQuit != null)
			{
				this.PageDragQuit(this, e);
			}
		}

		public virtual void OnDoubleClickTab(TabPage page)
		{
			if (this.DoubleClickTab != null)
			{
				this.DoubleClickTab(this, page);
			}
		}

		protected virtual void OnCloseButton(object sender, EventArgs e)
		{
			OnClosePressed(EventArgs.Empty);
		}

		protected virtual void OnLeftArrow(object sender, EventArgs e)
		{
			_startPage--;
			_recalculate = true;
			Invalidate();
		}

		protected virtual void OnRightArrow(object sender, EventArgs e)
		{
			_startPage++;
			_recalculate = true;
			Invalidate();
		}

		protected virtual void DefineFont(Font newFont)
		{
			base.Font = newFont;
			_textHeight = newFont.Height;
			if (_imageHeight >= _textHeight)
			{
				_imageGapTopExtra = 0;
				_imageGapBottomExtra = 0;
			}
			else
			{
				int num = _textHeight - _imageHeight;
				_imageGapTopExtra = num / 2;
				_imageGapBottomExtra = num - _imageGapTopExtra;
			}
		}

		protected virtual void DefineBackColor(Color newColor)
		{
			base.BackColor = newColor;
			_backLight = ControlPaint.Light(newColor);
			_backLightLight = ControlPaint.LightLight(newColor);
			_backDark = ControlPaint.Dark(newColor);
			_backDarkDark = ControlPaint.DarkDark(newColor);
			_backIDE = ColorHelper.TabBackgroundFromBaseColor(newColor);
		}

		protected virtual void DefineButtonImages()
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			ColorMap colorMap = new ColorMap();
			ColorMap colorMap2 = new ColorMap();
			colorMap.OldColor = Color.Black;
			colorMap.NewColor = _buttonActiveColor;
			colorMap2.OldColor = Color.White;
			colorMap2.NewColor = _buttonInactiveColor;
			imageAttributes.SetRemapTable(new ColorMap[2]
			{
				colorMap,
				colorMap2
			}, ColorAdjustType.Bitmap);
			_leftArrow.ImageAttributes = imageAttributes;
			_rightArrow.ImageAttributes = imageAttributes;
			_closeButton.ImageAttributes = imageAttributes;
		}

		protected virtual void SetAppearance(VisualAppearance appearance)
		{
			switch (appearance)
			{
			case VisualAppearance.MultiForm:
			case VisualAppearance.MultiBox:
			case VisualAppearance.Micromind:
				_shrinkPagesToFit = true;
				_positionAtTop = true;
				_showClose = false;
				_showArrows = false;
				_boldSelected = false;
				_idePixelArea = true;
				IDEPixelBorder = false;
				break;
			case VisualAppearance.MultiDocument:
				_shrinkPagesToFit = false;
				_showClose = true;
				_showArrows = true;
				_boldSelected = true;
				_idePixelArea = true;
				IDEPixelBorder = false;
				break;
			}
			_hotTrack = false;
			_dimUnselected = true;
			if (_tabPages.Count > 0)
			{
				_startPage = 0;
			}
			else
			{
				_startPage = -1;
			}
			_appearance = appearance;
			SetStyleIndex();
		}

		protected virtual void SetStyleIndex()
		{
			switch (_appearance)
			{
			case VisualAppearance.MultiBox:
				_styleIndex = 1;
				break;
			case VisualAppearance.MultiDocument:
			case VisualAppearance.MultiForm:
				_styleIndex = (int)_style;
				break;
			}
		}

		protected virtual bool HideTabsCalculation()
		{
			bool result = false;
			switch (_hideTabsMode)
			{
			case HideTabsModes.ShowAlways:
				result = false;
				break;
			case HideTabsModes.HideAlways:
				result = true;
				break;
			case HideTabsModes.HideUsingLogic:
				result = (_tabPages.Count <= 1);
				break;
			case HideTabsModes.HideWithoutMouse:
				result = !_mouseOver;
				break;
			}
			return result;
		}

		protected virtual void Recalculate()
		{
			_recalculate = false;
			int num = _position[_styleIndex, 4] + _imageGapTopExtra + _imageHeight + _imageGapBottomExtra + _position[_styleIndex, 6] + _position[_styleIndex, 2];
			int num2 = _position[_styleIndex, 0] + num + _position[_styleIndex, 10];
			bool flag = HideTabsCalculation();
			if (flag)
			{
				_pageAreaRect = new Rectangle(0, 0, base.Width, base.Height);
				_tabsAreaRect = new Rectangle(0, 0, 0, 0);
			}
			else if (_positionAtTop)
			{
				_pageAreaRect = new Rectangle(0, num2, base.Width, base.Height - num2);
				_tabsAreaRect = new Rectangle(0, 0, base.Width, num2);
			}
			else
			{
				_tabsAreaRect = new Rectangle(0, base.Height - num2, base.Width, num2);
				_pageAreaRect = new Rectangle(0, 0, base.Width, base.Height - num2);
			}
			int num3 = 0;
			if (!flag && _tabPages.Count > 0)
			{
				Rectangle tabPosition = _positionAtTop ? new Rectangle(0, _tabsAreaRect.Bottom - num - _position[_styleIndex, 0], _position[_styleIndex, 1] + _position[_styleIndex, 3], num) : new Rectangle(0, _tabsAreaRect.Top + _position[_styleIndex, 0], _position[_styleIndex, 1] + _position[_styleIndex, 3], num);
				int num4 = _tabsAreaRect.Left + _tabsAreaStartInset;
				num3 = GetMaximumDrawPos();
				int xWidth = num3 - num4;
				if (_multiline)
				{
					RecalculateMultilineTabs(num4, num3, tabPosition, num);
				}
				else
				{
					RecalculateSinglelineTabs(xWidth, num4, tabPosition);
				}
			}
			_pageRect = _pageAreaRect;
			if (_style == VisualStyle.Plain && _appearance != VisualAppearance.MultiBox)
			{
				_pageRect = _pageAreaRect;
				_pageRect.X += _plainBorderDouble;
				_pageRect.Width -= _plainBorderDouble * 2 - 1;
				if (!_positionAtTop)
				{
					_pageRect.Y += _plainBorderDouble;
				}
				_pageRect.Height -= _plainBorderDouble - 1;
				if (flag)
				{
					_pageRect.Height -= _plainBorderDouble;
					if (_positionAtTop)
					{
						_pageRect.Y += _plainBorderDouble;
					}
				}
			}
			int num5 = _ctrlLeftOffset;
			int num6 = _ctrlRightOffset;
			int num7 = _ctrlTopOffset;
			int num8 = _ctrlBottomOffset;
			if (_idePixelBorder && _style == VisualStyle.IDE)
			{
				num5 += 2;
				num6 += 2;
				if (_positionAtTop | flag)
				{
					num8 += 2;
				}
				if (!_positionAtTop | flag)
				{
					num7 += 2;
				}
			}
			Point location = new Point(_pageRect.Left + num5, _pageRect.Top + num7);
			Size size = new Size(_pageRect.Width - num5 - num6, _pageRect.Height - num7 - num8);
			if (_style == VisualStyle.Plain && _insetBorderPagesOnly)
			{
				location.X -= _plainBorderDouble;
				size.Width += _plainBorderDouble * 2;
				if (flag || _positionAtTop)
				{
					size.Height += _plainBorderDouble;
				}
				if (flag || !_positionAtTop)
				{
					location.Y -= _plainBorderDouble;
					size.Height += _plainBorderDouble;
				}
			}
			_hostPanel.Size = size;
			_hostPanel.Location = location;
			if (Appearance == VisualAppearance.Micromind)
			{
				Point location2 = _hostPanel.Location;
				_hostPanel.Location = new Point(1, location2.Y);
				Size size2 = _hostPanel.Size;
				_hostPanel.Size = new Size(size2.Width - 2, size2.Height - 1);
			}
			if (_tabPages.Count > 0)
			{
				_rightScroll = (((Rectangle)_tabRects[_tabPages.Count - 1]).Right > num3);
			}
			else
			{
				_rightScroll = false;
			}
			_leftScroll = (_startPage > 0);
			RecalculateButtons();
		}

		protected virtual void RecalculateMultilineTabs(int xStartPos, int xEndPos, Rectangle tabPosition, int tabButtonHeight)
		{
			using (Graphics g = CreateGraphics())
			{
				if (_appearance == VisualAppearance.MultiBox)
				{
					xEndPos -= 2;
				}
				int num = 0;
				_topYPos = tabPosition.Y;
				int num2 = xStartPos;
				int num3 = tabPosition.Y;
				int num4 = 0;
				int num5 = tabButtonHeight + 1;
				int num6 = 0;
				int num7 = 0;
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new ArrayList());
				for (int i = 0; i < _tabPages.Count; i++)
				{
					TabPage page = _tabPages[i];
					int num8 = GetTabPageSpace(g, page);
					if (num > 0 && num2 + num8 > xEndPos)
					{
						num2 = xStartPos;
						num3 += num5;
						_bottomYPos = tabPosition.Y;
						_tabsAreaRect.Height += num5;
						_pageAreaRect.Height -= num5;
						if (_positionAtTop)
						{
							_pageAreaRect.Y += num5;
						}
						else
						{
							num7 -= num5;
							_tabsAreaRect.Y -= num5;
						}
						arrayList.Add(new ArrayList());
						num4++;
					}
					if (num8 > xEndPos - xStartPos)
					{
						num8 = xEndPos - xStartPos;
					}
					Rectangle rect = new Rectangle(num2, num3, num8, tabButtonHeight);
					ArrayList obj = arrayList[arrayList.Count - 1] as ArrayList;
					MultiRect value = new MultiRect(rect, i);
					obj.Add(value);
					if (i == _pageSelected)
					{
						num6 = num4;
					}
					num2 += num8 + 1;
					num++;
				}
				int num9 = 0;
				if (_multilineFullWidth)
				{
					num4++;
				}
				foreach (ArrayList item in arrayList)
				{
					if (num9 < num4)
					{
						int count = item.Count;
						MultiRect multiRect = (MultiRect)item[count - 1];
						if (multiRect.Rect.Right < xEndPos - 1)
						{
							int num10 = (xEndPos - multiRect.Rect.Right - 1) / count;
							int num11 = 0;
							for (int j = 0; j < count; j++)
							{
								MultiRect obj2 = (MultiRect)item[j];
								obj2.X += num11;
								obj2.Width += num10;
								num11 += num10;
							}
							multiRect.Width += xEndPos - multiRect.Rect.Right - 1;
						}
					}
					num9++;
				}
				if (_positionAtTop)
				{
					if (num6 != arrayList.Count - 1)
					{
						int y = ((MultiRect)((ArrayList)arrayList[arrayList.Count - 1])[0]).Rect.Y;
						for (int k = num6 + 1; k < arrayList.Count; k++)
						{
							ArrayList arrayList3 = (ArrayList)arrayList[k];
							for (int l = 0; l < arrayList3.Count; l++)
							{
								((MultiRect)arrayList3[l]).Y -= num5;
							}
						}
						ArrayList arrayList4 = (ArrayList)arrayList[num6];
						for (int m = 0; m < arrayList4.Count; m++)
						{
							((MultiRect)arrayList4[m]).Y = y;
						}
					}
				}
				else if (num6 != 0)
				{
					int y2 = ((MultiRect)((ArrayList)arrayList[0])[0]).Rect.Y;
					for (int n = 0; n < num6; n++)
					{
						ArrayList arrayList5 = (ArrayList)arrayList[n];
						for (int num12 = 0; num12 < arrayList5.Count; num12++)
						{
							((MultiRect)arrayList5[num12]).Y += num5;
						}
					}
					ArrayList arrayList6 = (ArrayList)arrayList[num6];
					for (int num13 = 0; num13 < arrayList6.Count; num13++)
					{
						((MultiRect)arrayList6[num13]).Y = y2;
					}
				}
				foreach (ArrayList item2 in arrayList)
				{
					foreach (MultiRect item3 in item2)
					{
						Rectangle rect2 = item3.Rect;
						rect2.Y += num7;
						_tabRects[item3.Index] = rect2;
					}
				}
			}
		}

		protected virtual void RecalculateSinglelineTabs(int xWidth, int xStartPos, Rectangle tabPosition)
		{
			using (Graphics g = CreateGraphics())
			{
				_topYPos = tabPosition.Y;
				_bottomYPos = _topYPos;
				for (int i = 0; i < _tabPages.Count; i++)
				{
					if (i < _startPage)
					{
						_tabRects[i] = _nullPosition;
					}
					else
					{
						_tabRects[i] = tabPosition;
					}
				}
				xWidth -= _tabPages.Count * (tabPosition.Width + 1);
				if (xWidth > 0)
				{
					ArrayList arrayList = new ArrayList();
					new ArrayList();
					for (int j = _startPage; j < _tabPages.Count; j++)
					{
						arrayList.Add(_tabPages[j]);
					}
					int num;
					do
					{
						ArrayList arrayList2 = arrayList;
						arrayList = new ArrayList();
						num = ((!_shrinkPagesToFit) ? 999 : (xWidth / _tabPages.Count));
						foreach (TabPage item in arrayList2)
						{
							if (item.TabVisible)
							{
								int index = _tabPages.IndexOf(item);
								Rectangle rectangle = (Rectangle)_tabRects[index];
								int num2 = GetTabPageSpace(g, item) - rectangle.Width;
								if (num2 > num)
								{
									num2 = num;
									arrayList.Add(item);
								}
								rectangle.Width += num2;
								_tabRects[index] = rectangle;
								xWidth -= num2;
							}
						}
					}
					while (arrayList.Count > 0 && num > 0 && xWidth > 0);
				}
				for (int k = _startPage; k < _tabPages.Count; k++)
				{
					if (_tabPages[k].TabVisible)
					{
						Rectangle rectangle2 = (Rectangle)_tabRects[k];
						rectangle2.X = xStartPos;
						if (_tabPages[k].TabCellWidth > 0)
						{
							rectangle2.Width = _tabPages[k].TabCellWidth;
						}
						if (Appearance == VisualAppearance.Micromind && k == 0)
						{
							rectangle2.X = 1;
						}
						_tabRects[k] = rectangle2;
						xStartPos = ((Appearance != VisualAppearance.Micromind) ? (xStartPos + (rectangle2.Width + 1)) : (xStartPos + (rectangle2.Width + 7)));
					}
				}
			}
		}

		protected virtual void RecalculateButtons()
		{
			int num = 0;
			if (_multiline)
			{
				int num2 = _position[_styleIndex, 4] + _imageGapTopExtra + _imageHeight + _imageGapBottomExtra + _position[_styleIndex, 6] + _position[_styleIndex, 2];
				int num3 = _position[_styleIndex, 0] + num2 + _position[_styleIndex, 10];
				num = _position[_styleIndex, 11] + (num3 - _buttonHeight) / 2;
				if (!_positionAtTop)
				{
					num = _tabsAreaRect.Height - num - _buttonHeight;
				}
			}
			else
			{
				num = _position[_styleIndex, 11] + (_tabsAreaRect.Height - _buttonHeight) / 2;
			}
			int num4 = _tabsAreaRect.Right - _buttonWidth - _buttonGap;
			if (_showClose)
			{
				_closeButton.Location = new Point(num4, _tabsAreaRect.Top + num);
				if (num4 < 1)
				{
					_closeButton.Hide();
				}
				else
				{
					_closeButton.Show();
				}
				num4 -= _buttonWidth;
			}
			else
			{
				_closeButton.Hide();
			}
			if (_showArrows)
			{
				_rightArrow.Location = new Point(num4, _tabsAreaRect.Top + num);
				if (num4 < 1)
				{
					_rightArrow.Hide();
				}
				else
				{
					_rightArrow.Show();
				}
				num4 -= _buttonWidth;
				_leftArrow.Location = new Point(num4, _tabsAreaRect.Top + num);
				if (num4 < 1)
				{
					_leftArrow.Hide();
				}
				else
				{
					_leftArrow.Show();
				}
				num4 -= _buttonWidth;
				_leftArrow.Enabled = _leftScroll;
				_rightArrow.Enabled = _rightScroll;
			}
			else
			{
				_leftArrow.Hide();
				_rightArrow.Hide();
			}
			if (_appearance == VisualAppearance.MultiBox || _style == VisualStyle.Plain)
			{
				InertButton closeButton = _closeButton;
				InertButton leftArrow = _leftArrow;
				Color color = _rightArrow.BackColor = BackColor;
				Color color4 = closeButton.BackColor = (leftArrow.BackColor = color);
			}
			else
			{
				InertButton closeButton2 = _closeButton;
				InertButton leftArrow2 = _leftArrow;
				Color color = _rightArrow.BackColor = _backIDE;
				Color color4 = closeButton2.BackColor = (leftArrow2.BackColor = color);
			}
		}

		protected virtual int GetMaximumDrawPos()
		{
			int num = _tabsAreaRect.Right - _tabsAreaEndInset;
			if (_showClose)
			{
				num -= _buttonWidth + _buttonGap;
			}
			if (_showArrows)
			{
				num -= _buttonWidth * 2;
			}
			return num;
		}

		protected virtual int GetTabPageSpace(Graphics g, TabPage page)
		{
			int num = _position[_styleIndex, 1] + _position[_styleIndex, 3];
			if (page.Icon != null || ((_imageList != null || page.ImageList != null) && page.ImageIndex != -1))
			{
				num += _position[_styleIndex, 5] + _imageWidth + _position[_styleIndex, 7];
			}
			if (page.Title.Length > 0 && (!_selectedTextOnly || (_selectedTextOnly && _pageSelected == _tabPages.IndexOf(page))))
			{
				Font font = base.Font;
				if (_boldSelected && page.Selected)
				{
					font = new Font(font, FontStyle.Bold);
				}
				SizeF sizeF = g.MeasureString(page.Title, font);
				num += _position[_styleIndex, 9] + (int)sizeF.Width + 1;
			}
			return num;
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (_recalculate)
			{
				Recalculate();
			}
			using (SolidBrush brush = new SolidBrush(BackColor))
			{
				e.Graphics.FillRectangle(brush, _pageAreaRect);
				if (_style == VisualStyle.Plain || _appearance == VisualAppearance.MultiBox)
				{
					e.Graphics.FillRectangle(brush, _tabsAreaRect);
				}
				else if (_appearance == VisualAppearance.Micromind)
				{
					using (SolidBrush brush2 = new SolidBrush(base.Parent.BackColor))
					{
						e.Graphics.FillRectangle(brush2, _tabsAreaRect);
					}
				}
				else
				{
					using (SolidBrush brush3 = new SolidBrush(_backIDE))
					{
						e.Graphics.FillRectangle(brush3, _tabsAreaRect);
					}
				}
			}
			if (_appearance != VisualAppearance.MultiBox)
			{
				bool flag = HideTabsCalculation();
				switch (_style)
				{
				case VisualStyle.Plain:
				{
					int num4 = _pageAreaRect.Height + _plainBorderDouble;
					int num5 = _pageAreaRect.Top;
					if (flag)
					{
						num4 -= _plainBorderDouble;
					}
					else if (_positionAtTop)
					{
						num5 -= _plainBorderDouble;
					}
					if (_insetBorderPagesOnly)
					{
						if (!flag)
						{
							DrawHelper.DrawPlainRaisedBorderTopOrBottom(e.Graphics, new Rectangle(0, num5, base.Width, num4), _backLightLight, base.BackColor, _backDark, _backDarkDark, _positionAtTop);
						}
					}
					else
					{
						DrawHelper.DrawPlainRaisedBorder(e.Graphics, new Rectangle(_pageAreaRect.Left, num5, _pageAreaRect.Width, num4), _backLightLight, base.BackColor, _backDark, _backDarkDark);
					}
					if (_tabPages.Count <= 0 || !_insetPlain)
					{
						break;
					}
					Rectangle rectangle = new Rectangle(_pageAreaRect.Left + _plainBorder, num5 + _plainBorder, _pageAreaRect.Width - _plainBorderDouble, num4 - _plainBorderDouble);
					if (_insetBorderPagesOnly)
					{
						if (!flag)
						{
							DrawHelper.DrawPlainSunkenBorderTopOrBottom(e.Graphics, new Rectangle(0, rectangle.Top, base.Width, rectangle.Height), _backLightLight, base.BackColor, _backDark, _backDarkDark, _positionAtTop);
						}
					}
					else
					{
						DrawHelper.DrawPlainSunkenBorder(e.Graphics, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height), _backLightLight, base.BackColor, _backDark, _backDarkDark);
					}
					break;
				}
				case VisualStyle.IDE:
				{
					using (Pen pen6 = new Pen(_backDarkDark))
					{
						using (Pen pen = new Pen(_backDark))
						{
							using (Pen pen3 = new Pen(_backLightLight))
							{
								using (Pen pen5 = new Pen(base.BackColor))
								{
									int num = _position[_styleIndex, 0];
									if (_positionAtTop)
									{
										using (SolidBrush brush4 = new SolidBrush(base.BackColor))
										{
											e.Graphics.FillRectangle(brush4, 0, _tabsAreaRect.Bottom - num, _tabsAreaRect.Width, num);
										}
										int num2 = 0;
										if (_idePixelBorder)
										{
											using (new Pen(ControlPaint.LightLight(ForeColor)))
											{
												e.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
											}
											num2++;
										}
										else if (Appearance != VisualAppearance.Micromind && _idePixelArea)
										{
											e.Graphics.DrawLine(pen, 0, _tabsAreaRect.Top, _tabsAreaRect.Width, _tabsAreaRect.Top);
										}
										if (!flag)
										{
											e.Graphics.DrawLine(pen3, num2, _tabsAreaRect.Bottom - num, _tabsAreaRect.Width - num2 * 2, _tabsAreaRect.Bottom - num);
										}
									}
									else
									{
										using (SolidBrush brush5 = new SolidBrush(base.BackColor))
										{
											e.Graphics.FillRectangle(brush5, 0, _tabsAreaRect.Top, _tabsAreaRect.Width, num);
										}
										int num3 = 0;
										if (_idePixelBorder)
										{
											using (new Pen(ControlPaint.LightLight(ForeColor)))
											{
												e.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
											}
											num3++;
										}
										else if (_idePixelArea)
										{
											e.Graphics.DrawLine(pen5, 0, _tabsAreaRect.Bottom - 1, _tabsAreaRect.Width, _tabsAreaRect.Bottom - 1);
										}
										if (!flag)
										{
											e.Graphics.DrawLine(pen6, num3, _tabsAreaRect.Top + 2, _tabsAreaRect.Width - num3 * 2, _tabsAreaRect.Top + 2);
										}
									}
								}
							}
						}
					}
					break;
				}
				}
			}
			if (Appearance == VisualAppearance.Micromind)
			{
				Pen pen7 = new Pen(new SolidBrush(BorderColor), 2f);
				e.Graphics.DrawLine(pen7, 0, base.ClientRectangle.Bottom, base.ClientRectangle.Width, base.ClientRectangle.Bottom);
				e.Graphics.DrawLine(pen7, 0, 3, 0, base.ClientRectangle.Bottom);
				e.Graphics.DrawLine(pen7, base.ClientRectangle.Width, _hostPanel.Top - 3, base.ClientRectangle.Width, base.ClientRectangle.Bottom);
				pen7 = new Pen(new SolidBrush(BorderColor), 1f);
				e.Graphics.DrawLine(pen7, 0, _hostPanel.Top - 3, base.ClientRectangle.Width, _hostPanel.Top - 3);
			}
			ClipDrawingTabs(e.Graphics);
			foreach (TabPage tabPage in _tabPages)
			{
				if (tabPage.TabVisible)
				{
					DrawTab(tabPage, e.Graphics, highlightText: false);
				}
			}
			base.OnPaint(e);
		}

		protected virtual Rectangle ClippingRectangle()
		{
			int num = _tabsAreaRect.Width - GetMaximumDrawPos();
			return new Rectangle(_tabsAreaRect.Left, _tabsAreaRect.Top, _tabsAreaRect.Width - num, _tabsAreaRect.Height);
		}

		protected virtual void ClipDrawingTabs(Graphics g)
		{
			Rectangle rect = ClippingRectangle();
			g.Clip = new Region(rect);
		}

		protected virtual void DrawTab(TabPage page, Graphics g, bool highlightText)
		{
			if (page.TabVisible)
			{
				Rectangle rectTab = (Rectangle)_tabRects[_tabPages.IndexOf(page)];
				if (page.TabCellWidth > 0)
				{
					rectTab.Width = page.TabCellWidth;
				}
				DrawTabBorder(ref rectTab, page, g);
				int xDraw = rectTab.Left + _position[_styleIndex, 1];
				int xMax = rectTab.Right - _position[_styleIndex, 3];
				DrawTabImage(rectTab, page, g, xMax, ref xDraw);
				DrawTabText(rectTab, page, g, highlightText, xMax, xDraw);
			}
		}

		protected virtual void DrawTabImage(Rectangle rectTab, TabPage page, Graphics g, int xMax, ref int xDraw)
		{
			Icon icon = page.Icon;
			Image image = null;
			if (icon == null && page.ImageIndex != -1)
			{
				try
				{
					ImageList imageList = page.ImageList;
					if (imageList == null)
					{
						imageList = _imageList;
					}
					if (imageList != null)
					{
						image = imageList.Images[page.ImageIndex];
					}
				}
				catch (Exception)
				{
					image = _internalImages.Images[5];
				}
			}
			if ((image == null && icon == null) || xDraw + _position[_styleIndex, 5] > xMax)
			{
				return;
			}
			xDraw += _position[_styleIndex, 5];
			int num = rectTab.Top + _position[_styleIndex, 4] + _imageGapTopExtra;
			if (xDraw + _imageWidth <= xMax)
			{
				if (image != null)
				{
					g.DrawImage(image, new Rectangle(xDraw, num, _imageWidth, _imageHeight));
				}
				else
				{
					g.DrawIcon(icon, new Rectangle(xDraw, num, _imageWidth, _imageHeight));
				}
				xDraw += _imageWidth + _position[_styleIndex, 7];
				return;
			}
			int num2 = xMax - xDraw;
			if (num2 > 0)
			{
				if (image != null)
				{
					g.DrawImage(image, new Point[3]
					{
						new Point(xDraw, num),
						new Point(xDraw + num2, num),
						new Point(xDraw, num + _imageHeight)
					}, new Rectangle(0, 0, num2, _imageHeight), GraphicsUnit.Pixel);
				}
				xDraw = xMax;
			}
		}

		protected virtual void DrawTabText(Rectangle rectTab, TabPage page, Graphics g, bool highlightText, int xMax, int xDraw)
		{
			if ((!_selectedTextOnly || (_selectedTextOnly && page.Selected)) && xDraw < xMax)
			{
				Font font = base.Font;
				Color color = highlightText ? _hotTextColor : ((!_dimUnselected || page.Selected) ? _textColor : _textInactiveColor);
				if (_boldSelected && page.Selected)
				{
					font = new Font(font, FontStyle.Bold);
				}
				SolidBrush solidBrush = new SolidBrush(color);
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = (StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
				stringFormat.Trimming = StringTrimming.None;
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
				int num = rectTab.Top + _position[_styleIndex, 4];
				int num2 = rectTab.Bottom - _position[_styleIndex, 6] - _position[_styleIndex, 2];
				num += _position[_styleIndex, 8];
				xDraw += _position[_styleIndex, 9];
				if (xDraw < xMax)
				{
					g.DrawString(layoutRectangle: new Rectangle(xDraw, num, xMax - xDraw, num2 - num), s: page.Title, font: font, brush: solidBrush, format: stringFormat);
				}
				solidBrush.Dispose();
			}
		}

		protected virtual void DrawTabBorder(ref Rectangle rectTab, TabPage page, Graphics g)
		{
			if (_appearance == VisualAppearance.MultiBox)
			{
				rectTab.Y -= _multiBoxAdjust;
				DrawMultiBoxBorder(page, g, rectTab);
				return;
			}
			switch (_style)
			{
			case VisualStyle.Plain:
				DrawPlainTabBorder(page, g, rectTab);
				break;
			case VisualStyle.IDE:
				DrawIDETabBorder(page, g, rectTab);
				break;
			}
		}

		protected virtual void DrawMultiBoxBorder(TabPage page, Graphics g, Rectangle rectPage)
		{
			if (page.Selected)
			{
				using (SolidBrush brush = new SolidBrush(_backLightLight))
				{
					g.FillRectangle(brush, rectPage);
				}
				using (Pen pen = new Pen(_backDarkDark))
				{
					g.DrawRectangle(pen, rectPage);
				}
				return;
			}
			using (SolidBrush brush2 = new SolidBrush(BackColor))
			{
				g.FillRectangle(brush2, rectPage.X + 1, rectPage.Y, rectPage.Width - 1, rectPage.Height);
			}
			int num = _tabPages.IndexOf(page);
			bool flag = num == _tabPages.Count - 1 || (num < _tabPages.Count - 1 && !_tabPages[num + 1].Selected);
			if (_multiline && !flag)
			{
				flag = true;
				if (num < _tabPages.Count - 1 && _tabPages[num + 1].Selected)
				{
					Rectangle rectangle = (Rectangle)_tabRects[num];
					Rectangle rectangle2 = (Rectangle)_tabRects[num + 1];
					if (rectangle.Y == rectangle2.Y)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				using (Pen pen3 = new Pen(_backLightLight))
				{
					using (Pen pen2 = new Pen(_backDark))
					{
						g.DrawLine(pen2, rectPage.Right, rectPage.Top + 2, rectPage.Right, rectPage.Bottom - _position[_styleIndex, 10] - 1);
						g.DrawLine(pen3, rectPage.Right + 1, rectPage.Top + 2, rectPage.Right + 1, rectPage.Bottom - _position[_styleIndex, 10] - 1);
					}
				}
			}
		}

		protected virtual void DrawPlainTabBorder(TabPage page, Graphics g, Rectangle rectPage)
		{
			using (Pen pen = new Pen(_backLightLight))
			{
				using (Pen pen3 = new Pen(_backDark))
				{
					using (Pen pen2 = new Pen(_backDarkDark))
					{
						int num = 0;
						int num2 = 0;
						using (SolidBrush brush = new SolidBrush(base.BackColor))
						{
							if (page.Selected)
							{
								Rectangle rect = new Rectangle(y: (!_positionAtTop) ? (rectPage.Top - _position[_styleIndex, 0] / 2) : (rectPage.Top + _position[_styleIndex, 0] / 2), x: rectPage.Left, width: rectPage.Width - 1, height: rectPage.Height);
								g.FillRectangle(brush, rect);
								num = -2;
								num2 = -1;
							}
						}
						if (_positionAtTop)
						{
							g.DrawLine(pen, rectPage.Left, rectPage.Bottom, rectPage.Left, rectPage.Top + 2);
							g.DrawLine(pen, rectPage.Left + 1, rectPage.Top + 1, rectPage.Left + 1, rectPage.Top + 2);
							g.DrawLine(pen, rectPage.Left + 2, rectPage.Top + 1, rectPage.Right - 2, rectPage.Top + 1);
							g.DrawLine(pen2, rectPage.Right, rectPage.Bottom - num2, rectPage.Right, rectPage.Top + 2);
							g.DrawLine(pen3, rectPage.Right - 1, rectPage.Bottom - num2, rectPage.Right - 1, rectPage.Top + 2);
							g.DrawLine(pen3, rectPage.Right - 2, rectPage.Top + 1, rectPage.Right - 2, rectPage.Top + 2);
							g.DrawLine(pen2, rectPage.Right - 2, rectPage.Top, rectPage.Right, rectPage.Top + 2);
						}
						else
						{
							g.DrawLine(pen, rectPage.Left, rectPage.Top + num, rectPage.Left, rectPage.Bottom - 2);
							g.DrawLine(pen3, rectPage.Left + 1, rectPage.Bottom - 1, rectPage.Left + 1, rectPage.Bottom - 2);
							g.DrawLine(pen3, rectPage.Left + 2, rectPage.Bottom - 1, rectPage.Right - 2, rectPage.Bottom - 1);
							g.DrawLine(pen2, rectPage.Left + 2, rectPage.Bottom, rectPage.Right - 2, rectPage.Bottom);
							g.DrawLine(pen2, rectPage.Right, rectPage.Top, rectPage.Right, rectPage.Bottom - 2);
							g.DrawLine(pen3, rectPage.Right - 1, rectPage.Top + num2, rectPage.Right - 1, rectPage.Bottom - 2);
							g.DrawLine(pen3, rectPage.Right - 2, rectPage.Bottom - 1, rectPage.Right - 2, rectPage.Bottom - 2);
							g.DrawLine(pen2, rectPage.Right - 2, rectPage.Bottom, rectPage.Right, rectPage.Bottom - 2);
						}
					}
				}
			}
		}

		protected virtual void DrawIDETabBorder(TabPage page, Graphics g, Rectangle rectPage)
		{
			using (Pen pen3 = new Pen(_backLightLight))
			{
				using (Pen pen = new Pen(base.BackColor))
				{
					using (new Pen(_backDark))
					{
						using (Pen pen2 = new Pen(_backDarkDark))
						{
							if (page.Selected)
							{
								using (SolidBrush brush = new SolidBrush(BackColor))
								{
									g.FillRectangle(brush, rectPage);
								}
								if (_positionAtTop)
								{
									g.DrawLine(pen, rectPage.Left, rectPage.Bottom, rectPage.Right - 1, rectPage.Bottom);
									if (Appearance != VisualAppearance.Micromind)
									{
										g.DrawLine(pen2, rectPage.Right, rectPage.Top, rectPage.Right, rectPage.Bottom);
									}
								}
								else
								{
									g.DrawLine(pen3, rectPage.Left, rectPage.Top - 1, rectPage.Left, rectPage.Bottom);
									g.DrawLine(pen2, rectPage.Left + 1, rectPage.Bottom, rectPage.Right, rectPage.Bottom);
									g.DrawLine(pen2, rectPage.Right, rectPage.Top - 1, rectPage.Right, rectPage.Bottom);
									g.DrawLine(pen, rectPage.Left + 1, rectPage.Top - 1, rectPage.Right - 1, rectPage.Top - 1);
								}
							}
							else
							{
								if (Appearance == VisualAppearance.Micromind)
								{
									using (SolidBrush brush2 = new SolidBrush(_unselectedTabColor))
									{
										g.FillRectangle(brush2, rectPage);
									}
								}
								else
								{
									using (SolidBrush brush3 = new SolidBrush(_backIDE))
									{
										g.FillRectangle(brush3, rectPage);
									}
								}
								if (Appearance == VisualAppearance.Micromind)
								{
									g.DrawLine(new Pen(new SolidBrush(BorderColor), 1f), rectPage.Left, rectPage.Bottom, rectPage.Right - 1, rectPage.Bottom);
									g.DrawLine(new Pen(new SolidBrush(BorderColor), 1f), rectPage.Left, 3, rectPage.Right - 1, 3);
								}
								int num = _tabPages.IndexOf(page);
								bool flag = num == _tabPages.Count - 1 || (num < _tabPages.Count - 1 && !_tabPages[num + 1].Selected);
								if (_multiline && !flag)
								{
									flag = true;
									if (num < _tabPages.Count - 1 && _tabPages[num + 1].Selected)
									{
										Rectangle rectangle = (Rectangle)_tabRects[num];
										Rectangle rectangle2 = (Rectangle)_tabRects[num + 1];
										if (rectangle.Y == rectangle2.Y)
										{
											flag = false;
										}
									}
								}
								if (Appearance != VisualAppearance.Micromind && flag)
								{
									using (Pen pen4 = new Pen(_textInactiveColor))
									{
										g.DrawLine(pen4, rectPage.Right, rectPage.Top + 2, rectPage.Right, rectPage.Bottom - _position[_styleIndex, 10] - 1);
									}
								}
							}
							if (Appearance == VisualAppearance.Micromind)
							{
								Pen pen5 = new Pen(new SolidBrush(BorderColor), 1f);
								if (!page.Selected)
								{
									g.DrawLine(pen5, rectPage.Left, rectPage.Bottom, rectPage.Right - 1, rectPage.Bottom);
								}
								g.DrawLine(pen5, rectPage.Left, 3, rectPage.Right - 1, 3);
								if (TabPages.IndexOf(page) != 0)
								{
									g.DrawLine(pen5, rectPage.Left, 3, rectPage.Left, rectPage.Height + 3);
								}
								g.DrawLine(pen5, rectPage.Right, 3, rectPage.Right, rectPage.Height + 3);
							}
						}
					}
				}
			}
		}

		protected virtual void OnClearingPages()
		{
			if (_pageSelected != -1)
			{
				DeselectPage(_tabPages[_pageSelected]);
				_pageSelected = -1;
				_startPage = -1;
			}
			foreach (TabPage tabPage in _tabPages)
			{
				RemoveTabPage(tabPage);
			}
			_tabRects.Clear();
		}

		protected virtual void OnClearedPages()
		{
			Recalculate();
			CancelEventArgs e = new CancelEventArgs();
			OnSelectionChanging(e);
			OnSelectionChanged(EventArgs.Empty);
			Invalidate();
		}

		protected virtual void OnInsertingPage(int index, object value)
		{
			if (_pageSelected != -1 && _pageSelected >= index)
			{
				_pageSelected++;
			}
		}

		protected virtual void OnInsertedPage(int index, object value)
		{
			bool flag = false;
			TabPage tabPage = value as TabPage;
			tabPage.PropertyChanged += OnPagePropertyChanged;
			AddTabPage(tabPage);
			if (_pageSelected == -1 || tabPage.Selected)
			{
				CancelEventArgs e = new CancelEventArgs();
				OnSelectionChanging(e);
				if (_pageSelected != -1)
				{
					DeselectPage(_tabPages[_pageSelected]);
				}
				_pageSelected = _tabPages.IndexOf(tabPage);
				if (_startPage == -1)
				{
					_startPage = 0;
				}
				flag = true;
			}
			_tabRects.Add(default(Rectangle));
			if (flag)
			{
				Recalculate();
				SelectPage(tabPage);
				OnSelectionChanged(EventArgs.Empty);
			}
			Recalculate();
			Invalidate();
		}

		protected virtual void OnRemovingPage(int index, object value)
		{
			TabPage tabPage = value as TabPage;
			tabPage.PropertyChanged -= OnPagePropertyChanged;
			RemoveTabPage(tabPage);
			_changed = false;
			if (_pageSelected == index)
			{
				CancelEventArgs e = new CancelEventArgs();
				OnSelectionChanging(e);
				_changed = true;
				DeselectPage(tabPage);
			}
		}

		protected virtual void OnRemovedPage(int index, object value)
		{
			if (_startPage >= index)
			{
				_startPage--;
				if (_startPage == -1 && _tabPages.Count > 0)
				{
					_startPage = 0;
				}
			}
			if (_pageSelected >= index)
			{
				_pageSelected--;
				if (_pageSelected == -1 && _tabPages.Count > 0)
				{
					_pageSelected = 0;
				}
				if (_pageSelected != -1)
				{
					SelectPage(_tabPages[_pageSelected]);
				}
			}
			if (_changed)
			{
				_changed = false;
				OnSelectionChanged(EventArgs.Empty);
			}
			_tabRects.RemoveAt(0);
			Recalculate();
			Invalidate();
		}

		protected virtual void AddTabPage(TabPage page)
		{
			page.Shown = false;
			if (page.Control != null)
			{
				Form form = page.Control as Form;
				page.Control.Hide();
				if (form == null)
				{
					page.Control.GotFocus += OnPageEnter;
					page.Control.LostFocus += OnPageLeave;
					page.Control.MouseEnter += OnPageMouseEnter;
					page.Control.MouseLeave += OnPageMouseLeave;
					page.Control.Dock = DockStyle.None;
					page.Control.Location = new Point(0, 0);
					page.Control.Size = _hostPanel.Size;
					_hostPanel.Controls.Add(page.Control);
				}
				else
				{
					form.Activated += OnPageEnter;
					form.Deactivate += OnPageLeave;
					form.MouseEnter += OnPageMouseEnter;
					form.MouseLeave += OnPageMouseLeave;
					form.TopLevel = false;
					form.Parent = _hostPanel;
					form.FormBorderStyle = FormBorderStyle.None;
					form.Dock = DockStyle.None;
					form.Location = new Point(0, 0);
					form.Size = _hostPanel.Size;
				}
				if (page.Control is Form || page.Control is Panel)
				{
					page.Control.MouseDown += OnPageMouseDown;
				}
				RecursiveMonitor(page.Control, monitor: true);
			}
			else
			{
				page.Hide();
				page.GotFocus += OnPageEnter;
				page.LostFocus += OnPageLeave;
				page.MouseEnter += OnPageMouseEnter;
				page.MouseLeave += OnPageMouseLeave;
				page.Dock = DockStyle.None;
				page.MouseDown += OnPageMouseDown;
				RecursiveMonitor(page, monitor: true);
				page.Location = new Point(10, 0);
				page.Size = _hostPanel.Size;
				_hostPanel.Controls.Add(page);
			}
		}

		protected virtual void RemoveTabPage(TabPage page)
		{
			if (page.Control != null)
			{
				RemoveTabPageControl(page.Control);
			}
			else
			{
				RemoveTabPagePanel(page);
			}
		}

		protected virtual void RemoveTabPageControl(Control c)
		{
			RecursiveMonitor(c, monitor: false);
			Form form = c as Form;
			if (c is Form || c is Panel)
			{
				c.MouseDown -= OnPageMouseDown;
			}
			if (form == null)
			{
				c.GotFocus -= OnPageEnter;
				c.LostFocus -= OnPageLeave;
				c.MouseEnter -= OnPageMouseEnter;
				c.MouseLeave -= OnPageMouseLeave;
				ControlHelper.Remove(_hostPanel.Controls, c);
			}
			else
			{
				form.Activated -= OnPageEnter;
				form.Deactivate -= OnPageLeave;
				form.MouseEnter -= OnPageMouseEnter;
				form.MouseLeave -= OnPageMouseLeave;
				ControlHelper.RemoveForm(_hostPanel, form);
			}
		}

		protected virtual void RemoveTabPagePanel(TabPage page)
		{
			RecursiveMonitor(page, monitor: false);
			page.MouseDown -= OnPageMouseDown;
			page.GotFocus -= OnPageEnter;
			page.LostFocus -= OnPageLeave;
			page.MouseEnter -= OnPageMouseEnter;
			page.MouseLeave -= OnPageMouseLeave;
			ControlHelper.Remove(_hostPanel.Controls, page);
		}

		protected virtual void OnPageMouseDown(object sender, MouseEventArgs e)
		{
			Control control = sender as Control;
			if (!control.ContainsFocus)
			{
				control.Focus();
			}
		}

		protected virtual void RecursiveMonitor(Control top, bool monitor)
		{
			foreach (Control control in top.Controls)
			{
				if (monitor)
				{
					control.GotFocus += OnPageEnter;
					control.LostFocus += OnPageLeave;
					control.MouseEnter += OnPageMouseEnter;
					control.MouseLeave += OnPageMouseLeave;
				}
				else
				{
					control.GotFocus -= OnPageEnter;
					control.LostFocus -= OnPageLeave;
					control.MouseEnter -= OnPageMouseEnter;
					control.MouseLeave -= OnPageMouseLeave;
				}
				RecursiveMonitor(control, monitor);
			}
		}

		protected virtual void OnPageEnter(object sender, EventArgs e)
		{
			OnPageGotFocus(e);
		}

		protected virtual void OnPageLeave(object sender, EventArgs e)
		{
			OnPageLostFocus(e);
		}

		protected virtual void OnPageMouseEnter(object sender, EventArgs e)
		{
			_mouseOver = true;
			_overTimer.Stop();
			if (_hideTabsMode == HideTabsModes.HideWithoutMouse)
			{
				Recalculate();
				Invalidate();
			}
		}

		protected virtual void OnPageMouseLeave(object sender, EventArgs e)
		{
			_overTimer.Start();
		}

		protected virtual void OnMouseTick(object sender, EventArgs e)
		{
			_mouseOver = false;
			_overTimer.Stop();
			if (_hideTabsMode == HideTabsModes.HideWithoutMouse)
			{
				Recalculate();
				Invalidate();
			}
		}

		protected virtual void OnPagePropertyChanged(TabPage page, TabPage.Property prop, object oldValue)
		{
			switch (prop)
			{
			case TabPage.Property.TabCellWidth:
				break;
			case TabPage.Property.Control:
			{
				Control control = oldValue as Control;
				if (control != null)
				{
					RemoveTabPageControl(control);
				}
				else
				{
					RemoveTabPagePanel(page);
				}
				AddTabPage(page);
				if (_pageSelected != -1 && page == _tabPages[_pageSelected])
				{
					SelectPage(page);
				}
				Recalculate();
				Invalidate();
				break;
			}
			case TabPage.Property.Visible:
				_recalculate = true;
				Invalidate();
				break;
			case TabPage.Property.Title:
			case TabPage.Property.ImageIndex:
			case TabPage.Property.ImageList:
			case TabPage.Property.Icon:
				_recalculate = true;
				Invalidate();
				break;
			case TabPage.Property.Selected:
				if (page.Selected)
				{
					MovePageSelection(page);
					MakePageVisible(page);
				}
				break;
			}
		}

		protected virtual Control FindFocus(Control root)
		{
			if (root.Focused)
			{
				return root;
			}
			foreach (Control control2 in root.Controls)
			{
				Control control = FindFocus(control2);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		protected virtual void DeselectPage(TabPage page)
		{
			page.Selected = false;
			if (page.Control != null)
			{
				if (_recordFocus)
				{
					if (page.Control.ContainsFocus)
					{
						page.StartFocus = FindFocus(page.Control);
					}
					else
					{
						page.StartFocus = null;
					}
				}
				page.Control.Hide();
				return;
			}
			if (_recordFocus)
			{
				if (page.ContainsFocus)
				{
					page.StartFocus = FindFocus(page);
				}
				else
				{
					page.StartFocus = null;
				}
			}
			page.Hide();
		}

		protected virtual void SelectPage(TabPage page)
		{
			page.Selected = true;
			if (page.Control != null)
			{
				HandleShowingTabPage(page, page.Control);
			}
			else
			{
				HandleShowingTabPage(page, page);
			}
		}

		protected virtual void HandleShowingTabPage(TabPage page, Control c)
		{
			if (!page.Shown)
			{
				if (c is Form)
				{
					c.Show();
					c.Hide();
				}
				page.Shown = true;
			}
			foreach (TabPage tabPage in _tabPages)
			{
				if (tabPage != page)
				{
					tabPage.Hide();
				}
			}
			c.Show();
			c.BringToFront();
			if (page.StartFocus != null)
			{
				page.StartFocus.Focus();
			}
			else
			{
				c.Focus();
			}
		}

		protected virtual void MovePageSelection(TabPage page)
		{
			int num = _tabPages.IndexOf(page);
			if (num == _pageSelected)
			{
				return;
			}
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			OnSelectionChanging(cancelEventArgs, num);
			if (!cancelEventArgs.Cancel)
			{
				if (_pageSelected != -1)
				{
					DeselectPage(_tabPages[_pageSelected]);
				}
				_pageSelected = num;
				if (_pageSelected != -1)
				{
					SelectPage(_tabPages[_pageSelected]);
				}
				if (_boldSelected || _selectedTextOnly || !_shrinkPagesToFit || _multiline)
				{
					Recalculate();
					Invalidate();
				}
				OnSelectionChanged(EventArgs.Empty);
				Invalidate();
			}
		}

		internal bool WantDoubleClick(IntPtr hWnd, Point mousePos)
		{
			if (!ControlWantDoubleClick(hWnd, mousePos, _leftArrow) && !ControlWantDoubleClick(hWnd, mousePos, _rightArrow))
			{
				return ControlWantDoubleClick(hWnd, mousePos, _closeButton);
			}
			return true;
		}

		internal void ExternalMouseTest(IntPtr hWnd, Point mousePos)
		{
			if (!ControlMouseTest(hWnd, mousePos, _leftArrow) && !ControlMouseTest(hWnd, mousePos, _rightArrow) && !ControlMouseTest(hWnd, mousePos, _closeButton))
			{
				InternalMouseDown(mousePos);
			}
		}

		protected virtual bool ControlWantDoubleClick(IntPtr hWnd, Point mousePos, Control check)
		{
			if (check.Visible)
			{
				if (check.Enabled && hWnd == check.Handle)
				{
					if (check == _leftArrow)
					{
						OnLeftArrow(null, EventArgs.Empty);
					}
					if (check == _rightArrow)
					{
						OnRightArrow(null, EventArgs.Empty);
					}
					return true;
				}
				Rectangle rectangle = new Rectangle(check.Location.X, check.Location.Y, check.Width, check.Height);
				if (rectangle.Contains(mousePos))
				{
					return true;
				}
			}
			return false;
		}

		protected virtual bool ControlMouseTest(IntPtr hWnd, Point mousePos, Control check)
		{
			if (hWnd == check.Handle && check.Visible && check.Enabled && check.ClientRectangle.Contains(mousePos))
			{
				if (check == _leftArrow)
				{
					OnLeftArrow(null, EventArgs.Empty);
				}
				if (check == _rightArrow)
				{
					OnRightArrow(null, EventArgs.Empty);
				}
				return true;
			}
			return false;
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			Point mousePosition = Control.MousePosition;
			int count = _tabRects.Count;
			for (int i = 0; i < count; i++)
			{
				Rectangle rectangle = (Rectangle)_tabRects[i];
				if (rectangle != _nullPosition && RectangleToScreen(rectangle).Contains(mousePosition))
				{
					OnDoubleClickTab(_tabPages[i]);
					break;
				}
			}
			base.OnDoubleClick(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (_leftMouseDownDrag)
			{
				if (e.Button == MouseButtons.Left)
				{
					OnPageDragEnd(e);
				}
				else
				{
					OnPageDragQuit(e);
				}
				_leftMouseDownDrag = false;
				_ignoreDownDrag = true;
			}
			if (e.Button == MouseButtons.Left)
			{
				_leftMouseDown = false;
			}
			else if (e.Button == MouseButtons.Right)
			{
				Point pt = new Point(e.X, e.Y);
				if (_tabsAreaRect.Contains(pt))
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					OnPopupMenuDisplay(cancelEventArgs);
					_ = cancelEventArgs.Cancel;
				}
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			InternalMouseDown(new Point(e.X, e.Y));
			base.OnMouseDown(e);
		}

		protected virtual void InternalMouseDown(Point mousePos)
		{
			int num = 0;
			while (true)
			{
				if (num < _tabPages.Count)
				{
					if (((Rectangle)_tabRects[num]).Contains(mousePos))
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			if (_leftArrow.Visible)
			{
				if (mousePos.X >= _leftArrow.Left)
				{
					return;
				}
			}
			else if (_closeButton.Visible && mousePos.X >= _closeButton.Left)
			{
				return;
			}
			if (Control.MouseButtons == MouseButtons.Left)
			{
				_leftMouseDown = true;
				_ignoreDownDrag = false;
				_leftMouseDownDrag = false;
				_leftMouseDownPos = mousePos;
			}
			MovePageSelection(_tabPages[num]);
			MakePageVisible(_tabPages[num]);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_leftMouseDown)
			{
				if (!_leftMouseDownDrag)
				{
					Point pt = new Point(e.X, e.Y);
					bool flag = false;
					if (_dragFromControl)
					{
						flag = !base.ClientRectangle.Contains(pt);
					}
					else
					{
						Rectangle rectangle = new Rectangle(_leftMouseDownPos, new Size(0, 0));
						rectangle.Inflate(SystemInformation.DoubleClickSize);
						flag = !rectangle.Contains(pt);
					}
					if (flag && !_ignoreDownDrag)
					{
						OnPageDragStart(e);
						_leftMouseDownDrag = true;
					}
				}
				else
				{
					OnPageDragMove(e);
				}
			}
			else if (_hotTrack || _hoverSelect)
			{
				int num = -1;
				bool flag2 = false;
				Point pt2 = new Point(e.X, e.Y);
				for (int i = 0; i < _tabPages.Count; i++)
				{
					if (((Rectangle)_tabRects[i]).Contains(pt2))
					{
						num = i;
						break;
					}
				}
				if (_hoverSelect && !_multiline && num != -1 && num != _pageSelected)
				{
					MovePageSelection(_tabPages[num]);
					flag2 = true;
				}
				if (_hotTrack && !flag2 && num != _hotTrackPage)
				{
					Graphics graphics = CreateGraphics();
					ClipDrawingTabs(graphics);
					if (_hotTrackPage != -1)
					{
						DrawTab(_tabPages[_hotTrackPage], graphics, highlightText: false);
					}
					_hotTrackPage = num;
					if (_hotTrackPage != -1)
					{
						DrawTab(_tabPages[_hotTrackPage], graphics, highlightText: true);
					}
					graphics.Dispose();
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			_mouseOver = true;
			_overTimer.Stop();
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (_hotTrack)
			{
				int num = -1;
				if (num != _hotTrackPage)
				{
					Graphics graphics = CreateGraphics();
					ClipDrawingTabs(graphics);
					if (_hotTrackPage != -1)
					{
						DrawTab(_tabPages[_hotTrackPage], graphics, highlightText: false);
					}
					_hotTrackPage = num;
					graphics.Dispose();
				}
			}
			_overTimer.Start();
			base.OnMouseLeave(e);
		}

		protected virtual void OnPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (_defaultFont)
			{
				DefineFont(SystemInformation.MenuFont);
				Recalculate();
				Invalidate();
			}
		}

		private void InitializeComponent()
		{
			base.Name = "TabControl";
			base.Size = new System.Drawing.Size(176, 96);
		}

		protected override void OnSystemColorsChanged(EventArgs e)
		{
			if (_defaultColor)
			{
				DefineBackColor(Control.DefaultBackColor);
				Recalculate();
				Invalidate();
			}
			base.OnSystemColorsChanged(e);
		}
	}
}
