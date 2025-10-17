using Myra.Graphics2D.UI.Styles;
using Myra.Utility;
using System.ComponentModel;
using System.Xml.Serialization;
using System;
using System.Windows.Input;
using Myra.Events;

namespace Myra.Graphics2D.UI
{
	public abstract class ButtonBase2 : ContentControl
	{
		private bool _isPressed = false;
		private bool _isClicked = false;

		private IBrush _pressedBackground;
		private ICommand _command; 
		private object _commandParameter;

		[Category("Appearance")]
		[Bindable(true)]
		public virtual IBrush PressedBackground
		{
			get => _pressedBackground;
			set
			{
				if (value == _pressedBackground)
				{
					return;
				}

				_pressedBackground = value;
				OnPropertyChanged();
			}
		}

		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public virtual bool IsPressed
		{
			get => _isPressed;
			set
			{
				if (value == _isPressed)
				{
					return;
				}

				_isPressed = value;
				OnPropertyChanged();
				OnPressedChanged();
			}
		}

		[Bindable(true)]
		public ICommand Command
		{
			get => _command;
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
		
		[Bindable(true)]
		public object CommandParameter
		{
			get => _commandParameter;
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

		public event EventHandler Click;
		public event EventHandler PressedChanged;

		/// <summary>
		/// Fires when the value is about to be changed
		/// Set Cancel to true if you want to cancel the change
		/// </summary>
		public event EventHandler<ValueChangingEventArgs<bool>> PressedChangingByUser;


		public void DoClick()
		{
			OnTouchDown();
			OnTouchUp();
		}

		public virtual void OnPressedChanged()
		{
			PressedChanged.Invoke(this);

			var asPressable = Content as IPressable;
			if (asPressable != null)
			{
				asPressable.IsPressed = IsPressed;
			}
		}

		protected void SetValueByUser(bool value)
		{
			if (value != IsPressed && PressedChangingByUser != null)
			{
				var args = new ValueChangingEventArgs<bool>(_isPressed, value);
				PressedChangingByUser(this, args);

				if (args.Cancel)
				{
					return;
				}
			}

			IsPressed = value;
		}

		protected abstract void InternalOnTouchUp();
		protected abstract void InternalOnTouchDown();

		public override void OnTouchUp()
		{
			base.OnTouchUp();

			if (!Enabled)
			{
				return;
			}

			InternalOnTouchUp();

			if (_isClicked)
			{
				Click.Invoke(this);
				ExecuteCommand();
				_isClicked = false;
			}
		}

		public override void OnTouchDown()
		{
			base.OnTouchDown();

			if (!Enabled)
			{
				return;
			}

			InternalOnTouchDown();

			_isClicked = true;
		}

		public override IBrush GetCurrentBackground()
		{
			var result = base.GetCurrentBackground();

			if (Enabled)
			{
				if (IsPressed && PressedBackground != null)
				{
					result = PressedBackground;
				}
				else if (UseOverBackground && OverBackground != null)
				{
					result = OverBackground;
				}
			}
			else
			{
				if (DisabledBackground != null)
				{
					result = DisabledBackground;
				}
			}

			return result;
		}

		public void ApplyButtonStyle(ButtonStyle style)
		{
			ApplyWidgetStyle(style);

			PressedBackground = style.PressedBackground;
		}

		public void ApplyImageButtonStyle(ImageButtonStyle style)
		{
			ApplyButtonStyle(style);

			if (style.ImageStyle != null)
			{
				var image = (Image)Content;
				image.ApplyPressableImageStyle(style.ImageStyle);
			}
		}

		protected override void InternalSetStyle(Stylesheet stylesheet, string name)
		{
			ApplyButtonStyle(stylesheet.ButtonStyles.SafelyGetStyle(name));
		}

		protected internal override void CopyFrom(Widget w)
		{
			base.CopyFrom(w);

			var buttonBase = (ButtonBase2)w;
			PressedBackground = buttonBase.PressedBackground;
			IsPressed = buttonBase.IsPressed;
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
