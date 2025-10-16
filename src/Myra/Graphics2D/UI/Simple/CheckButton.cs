using Myra.Attributes;
using Myra.Graphics2D.UI.Styles;
using System;
using System.ComponentModel;

namespace Myra.Graphics2D.UI
{
	[StyleTypeName("CheckBox")]
	public class CheckButton : CheckButtonBase
	{
		[Category("Behavior")]
		[DefaultValue(false)]
		[Bindable(true)]
		public bool IsChecked
		{
			get => IsPressed;
			set
			{
				if (value == IsPressed)
				{
					return;
				}

				IsPressed = value;
				OnPropertyChanged();
			}
		}

		public event EventHandler IsCheckedChanged
		{
			add
			{
				PressedChanged += value;
			}

			remove
			{
				PressedChanged -= value;
			}
		}

		public CheckButton(string styleName = Stylesheet.DefaultStyleName)
		{
			SetStyle(styleName);
		}

		protected override void InternalSetStyle(Stylesheet stylesheet, string name)
		{
			base.InternalSetStyle(stylesheet, name);

			var style = stylesheet.CheckBoxStyles.SafelyGetStyle(name);
			ApplyCheckButtonStyle(style);
		}
	}
}
