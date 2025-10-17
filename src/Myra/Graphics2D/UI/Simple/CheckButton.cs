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
			set => IsPressed = value;
		}

		public event EventHandler IsCheckedChanged
		{
			add => PressedChanged += value;
			remove => PressedChanged -= value;
		}

		public CheckButton(string styleName = Stylesheet.DefaultStyleName)
		{
			SetStyle(styleName);
			PressedChanged += CheckButton_PressedChanged;
		}

		private void CheckButton_PressedChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(IsChecked));
		}

		protected override void InternalSetStyle(Stylesheet stylesheet, string name)
		{
			base.InternalSetStyle(stylesheet, name);

			var style = stylesheet.CheckBoxStyles.SafelyGetStyle(name);
			ApplyCheckButtonStyle(style);
		}
	}
}
