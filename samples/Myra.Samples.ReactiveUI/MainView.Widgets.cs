using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace Myra.Samples.ReactiveUI
{
	partial class MainView
	{
		private Label _sliderText;
		private HorizontalSlider _slider;
		private Label _label;
		private TextBox _textBox;
		private Button _button;

		void InitializeComponent()
		{
			_sliderText = new Label {
				Margin = new Thickness(5, 0)
			};

			_slider = new HorizontalSlider {
				Margin = new Thickness(5, 0)
			};

			_label = new Label {
				Margin = new Thickness(5, 0)
			};

			_textBox = new TextBox {
				Margin = new Thickness(5, 0)
			};

			_button = new Button {
				Padding = new Thickness(15, 3),
				Margin = new Thickness(5, 0),
				Content = new Label {
					Text = "Command",
				}
			};

			var layoutRoot = new VerticalStackPanel {
				Width = 300,
				Spacing = 5,
			};
			layoutRoot.Widgets.Add(_sliderText);
			layoutRoot.Widgets.Add(_slider);
			layoutRoot.Widgets.Add(_label);
			layoutRoot.Widgets.Add(_textBox);
			layoutRoot.Widgets.Add(_button);

			Widgets.Add(layoutRoot);
		}
	}
}
