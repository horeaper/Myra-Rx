using FontStashSharp;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace Myra.Samples.RxUI
{
	partial class MainView
	{
		Label labelSlider;
		HorizontalSlider slider;
		Label labelText;
		TextBox textBox;
		Button buttonReset;
		Label labelChecked;
		CheckButton checkBox;
		Label labelCombo;
		ComboView comboBox;

		void InitializeComponent()
		{
			labelSlider = new Label {
				Margin = new Thickness(5, 0),
			};
			slider = new HorizontalSlider {
				Margin = new Thickness(5, 0),
			};

			labelText = new Label {
				Margin = new Thickness(5, 0),
			};
			textBox = new TextBox {
				Margin = new Thickness(5, 0),
			};
			buttonReset = new Button {
				Padding = new Thickness(15, 3),
				Margin = new Thickness(5, 0),
				Content = new Label {
					Text = "Reset",
				}
			};

			labelChecked = new Label {
				Margin = new Thickness(5, 0),
			};
			checkBox = new CheckButton {
				Margin = new Thickness(5, 0),
				Content = new Label {
					Text = "Enable Button",
					Margin = new Thickness(5, 0),
				},
			};

			labelCombo = new Label {
				Margin = new Thickness(5, 0),
			};
			comboBox = new ComboView {
				Width = 100,
				Margin = new Thickness(5, 0),
			};
			comboBox.Widgets.Add(new Label {
				Text = "Red",
				TextColor = FSColor.Red,
				Margin = new Thickness(5, 3),
			});
			comboBox.Widgets.Add(new Label {
				Text = "Green",
				TextColor = FSColor.Red,
				Margin = new Thickness(5, 3),
			});
			comboBox.Widgets.Add(new Label {
				Text = "Blue",
				TextColor = FSColor.Red,
				Margin = new Thickness(5, 3),
			});

			var layoutRoot = new VerticalStackPanel {
				Width = 300,
				Spacing = 5,
			};
			layoutRoot.Widgets.Add(labelSlider);
			layoutRoot.Widgets.Add(slider);
			layoutRoot.Widgets.Add(labelText);
			layoutRoot.Widgets.Add(textBox);
			layoutRoot.Widgets.Add(buttonReset);
			layoutRoot.Widgets.Add(labelChecked);
			layoutRoot.Widgets.Add(checkBox);
			layoutRoot.Widgets.Add(labelCombo);
			layoutRoot.Widgets.Add(comboBox);

			Widgets.Add(layoutRoot);
		}
	}
}
