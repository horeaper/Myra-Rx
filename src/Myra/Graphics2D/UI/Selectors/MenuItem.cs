using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Windows.Input;
using Myra.Attributes;
using Myra.MML;
using FontStashSharp.RichText;
using Myra.Events;


#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using Color = FontStashSharp.FSColor;
#endif

namespace Myra.Graphics2D.UI
{
	/// <summary>
	/// Represents a single item in a menu that can have sub-items, an image, and an optional shortcut text.
	/// </summary>
	public class MenuItem : BaseObject, IMenuItem
	{
		private string _shortcutText;
		private Color? _shortcutColor;
		private IImage _image;
		private string _text;
		private Color? _color;
		private bool _displayTextDirty = true;
		private string _displayText, _disabledDisplayText;

		private Menu _menu;
		private char? _underscoreChar;
		private int _index;
		private ICommand _command;
		private object _commandParameter;

		internal readonly Image ImageWidget = new Image
		{
			VerticalAlignment = VerticalAlignment.Center
		};

		internal readonly Label Label = new Label(null)
		{
			VerticalAlignment = VerticalAlignment.Center
		};

		internal readonly Label Shortcut = new Label(null)
		{
			VerticalAlignment = VerticalAlignment.Center
		};


		internal readonly Menu SubMenu = new VerticalMenu();

		/// <summary>
		/// Gets or sets the text displayed for this menu item. An ampersand (&amp;) marks a mnemonic character.
		/// </summary>
		[DefaultValue(null)]
		[Bindable(true)]
		public string Text
		{
			get { return _text; }
			set
			{
				if (value == _text)
				{
					return;
				}

				_text = value;
				OnPropertyChanged();
				_displayTextDirty = true;

				UnderscoreChar = null;
				if (value != null)
				{
					var underscoreIndex = value.IndexOf('&');
					if (underscoreIndex >= 0 && underscoreIndex + 1 < value.Length)
					{
						UnderscoreChar = char.ToLower(value[underscoreIndex + 1]);
					}
				}

				FireChanged();
			}
		}

		internal string DisplayText
		{
			get
			{
				UpdateDisplayText();
				return _displayText;
			}
		}

		internal string DisabledDisplayText
		{
			get
			{
				UpdateDisplayText();
				return _disabledDisplayText;
			}
		}

		/// <summary>
		/// Gets or sets the color of the menu item text.
		/// </summary>
		[DefaultValue(null)]
		[Bindable(true)]
		public Color? Color
		{
			get
			{
				return _color;
			}

			set
			{
				if (value == _color)
				{
					return;
				}

				_color = value;
				OnPropertyChanged();
				FireChanged();
			}
		}

		/// <summary>
		/// Gets or sets the image displayed next to the menu item text.
		/// </summary>
		[Bindable(true)]
		public IImage Image
		{
			get
			{
				return _image;
			}

			set
			{
				if (Equals(value, _image))
				{
					return;
				}

				_image = value;
				OnPropertyChanged();
				FireChanged();
			}
		}

		/// <summary>
		/// Gets or sets the text displayed as a keyboard shortcut hint next to the menu item.
		/// </summary>
		[DefaultValue(null)]
		[Bindable(true)]
		public string ShortcutText
		{
			get
			{
				return _shortcutText;
			}

			set
			{
				if (value == _shortcutText)
				{
					return;
				}

				_shortcutText = value;
				OnPropertyChanged();
				FireChanged();
			}
		}

		/// <summary>
		/// Gets or sets the color of the shortcut text.
		/// </summary>
		[DefaultValue(null)]
		[Bindable(true)]
		public Color? ShortcutColor
		{
			get
			{
				return _shortcutColor;
			}

			set
			{
				if (value == _shortcutColor)
				{
					return;
				}

				_shortcutColor = value;
				OnPropertyChanged();
				FireChanged();
			}
		}

		/// <summary>
		/// Gets or sets the parent menu of this item.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public Menu Menu
		{
			get
			{
				return _menu;
			}

			set
			{
				if (value == _menu)
				{
					return;
				}
				
				_menu = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets the collection of submenu items nested under this menu item.
		/// </summary>
		[Browsable(false)]
		[Content]
		public ObservableCollection<IMenuItem> Items
		{
			get { return SubMenu.Items; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this menu item is enabled.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public bool Enabled
		{
			get { return ImageWidget.Enabled; }

			set
			{
				if (value == ImageWidget.Enabled)
				{
					return;
				}

				ImageWidget.Enabled = Label.Enabled = Shortcut.Enabled = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets the mnemonic character for this menu item (the character after the ampersand in Text).
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public char? UnderscoreChar
		{
			get
			{
				return _underscoreChar;
			}

			private set
			{
				if (value == _underscoreChar)
				{
					return;
				}
				
				_underscoreChar = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this menu item can be opened (has submenu items).
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public bool CanOpen
		{
			get
			{
				return Items.Count > 0;
			}
		}

		/// <summary>
		/// Gets or sets the zero-based index of this menu item within its parent menu.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public int Index
		{
			get
			{
				return _index;
			}

			set
			{
				if (value == _index)
				{
					return;
				}
				
				_index = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the command to invoke when this button is pressed.
		/// </summary>
		[Bindable(true)]
		public ICommand Command
		{
			get
			{
				return _command;
			}

			set
			{
				if (value == _command)
				{
					return;
				}

				var oldCommand = _command;
				_command = value;
				OnPropertyChanged();
				OnCommandChanged(oldCommand);
			}
		}

		/// <summary>
		/// Gets or sets the parameter to pass to the <see cref="P:Myra.Graphics2D.UI.Command" /> property.
		/// </summary>
		[Bindable(true)]
		public object CommandParameter
		{
			get
			{
				return _commandParameter;
			}

			set
			{
				if (value == _commandParameter)
				{
					return;
				}

				_commandParameter = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Occurs when this menu item is selected.
		/// </summary>
		public event MyraEventHandler Selected;

		/// <summary>
		/// Occurs when any property of this menu item changes.
		/// </summary>
		public event MyraEventHandler Changed;

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class with the specified id, text, color, and tag.
		/// </summary>
		/// <param name="id">The unique identifier for this menu item.</param>
		/// <param name="text">The display text for this menu item.</param>
		/// <param name="color">The color of the menu item text, or null to use the style default.</param>
		/// <param name="tag">Arbitrary user data to associate with this menu item.</param>
		public MenuItem(string id, string text, Color? color, object tag)
		{
			Id = id;
			Text = text;
			Color = color;
			Tag = tag;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class with the specified id, text, and color.
		/// </summary>
		/// <param name="id">The unique identifier for this menu item.</param>
		/// <param name="text">The display text for this menu item.</param>
		/// <param name="color">The color of the menu item text, or null to use the style default.</param>
		public MenuItem(string id, string text, Color? color) : this(id, text, color, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class with the specified id and text.
		/// </summary>
		/// <param name="id">The unique identifier for this menu item.</param>
		/// <param name="text">The display text for this menu item.</param>
		public MenuItem(string id, string text) : this(id, text, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class with the specified id.
		/// </summary>
		/// <param name="id">The unique identifier for this menu item.</param>
		public MenuItem(string id) : this(id, string.Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class with default values.
		/// </summary>
		public MenuItem() : this(string.Empty)
		{
		}

		private void UpdateDisplayText()
		{
			if (!_displayTextDirty)
			{
				return;
			}

			if (UnderscoreChar == null)
			{
				_disabledDisplayText = _displayText = Text;
			}
			else
			{
				var originalColor = Menu.MenuStyle.LabelStyle.TextColor;
				if (Color != null)
				{
					originalColor = Color.Value;
				}

				var specialCharColor = Menu.MenuStyle.SpecialCharColor;
				var underscoreIndex = Text.IndexOf('&');

				var underscoreChar = Text[underscoreIndex + 1];

				_disabledDisplayText = Text.Substring(0, underscoreIndex) + Text.Substring(underscoreIndex + 1);

				if (specialCharColor != null)
				{
					_displayText = Text.Substring(0, underscoreIndex) +
						@"/c[" + specialCharColor.Value.ToHexString() + "]" +
						underscoreChar.ToString() +
						@"/c[" + originalColor.ToHexString() + "]" +
						Text.Substring(underscoreIndex + 2);
				}
				else
				{
					_displayText = _disabledDisplayText;
				}
			}

			_displayTextDirty = false;
		}

		internal MenuItem FindMenuItemById(string id)
		{
			if (Id == id)
			{
				return this;
			}

			foreach (var item in SubMenu.Items)
			{
				var asMenuItem = item as MenuItem;
				if (asMenuItem == null)
				{
					continue;
				}

				var result = asMenuItem.FindMenuItemById(id);
				if (result != null)
				{
					return result;
				}
			}

			return null;
		}

		/// <summary>
		/// Raises the Selected event to indicate that this menu item has been selected.
		/// </summary>
		public void FireSelected()
		{
			var ev = Selected;

			if (ev != null)
			{
				ev(this, new MyraEventArgs(InputEventType.SelectionChanged));
			}

			ExecuteCommand();
		}

		/// <summary>
		/// Called when the Id property changes, and fires the Changed event.
		/// </summary>
		protected internal override void OnIdChanged()
		{
			base.OnIdChanged();

			FireChanged();
		}

		/// <summary>
		/// Fires the Changed event to notify listeners that this item has changed.
		/// </summary>
		protected void FireChanged()
		{
			var ev = Changed;
			if (ev != null)
			{
				ev(this, new MyraEventArgs(InputEventType.ValueChanged));
			}
		}

		private void ExecuteCommand()
		{
			if (Command?.CanExecute(CommandParameter) == true)
			{
				Command.Execute(CommandParameter);
			}
		}

		private void OnCommandChanged(ICommand oldCommand)
		{
			if (oldCommand != null)
			{
				oldCommand.CanExecuteChanged -= Command_CanExecuteChanged;
			}
			if (Command != null)
			{
				Command.CanExecuteChanged += Command_CanExecuteChanged;
			}
			UpdateCanExecute();
		}

		private void Command_CanExecuteChanged(object sender, EventArgs e)
		{
			UpdateCanExecute();
		}

		private void UpdateCanExecute()
		{
			if (Command != null)
			{
				Enabled = Command.CanExecute(CommandParameter);
			}
			else
			{
				Enabled = true;
			}
		}
	}
}