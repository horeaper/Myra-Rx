using System.Reactive.Disposables.Fluent;
using ReactiveUI.Myra;
using ReactiveUI;

namespace Myra.Samples.RxUI
{
	public partial class MainView : ReactivePanel<MainViewModel>
	{
		public MainView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				this.Bind(ViewModel, static vm => vm.SliderValue, static view => view.slider.Value).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.SliderValue, static view => view.labelSlider.Text).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.SliderValue, static view => view.progressBar.Value).DisposeWith(d);

				this.Bind(ViewModel, static vm => vm.TextValue, static view => view.textBox.Text).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.TextValue, static view => view.labelText.Text).DisposeWith(d);
				this.BindCommand(ViewModel, static vm => vm.ResetSliderValueCommand, static view => view.buttonReset).DisposeWith(d);

				this.Bind(ViewModel, static vm => vm.BoolValue, static view => view.checkBox.IsPressed).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.BoolValue, static view => view.labelChecked.Text).DisposeWith(d);

				this.Bind(ViewModel, static vm => vm.ComboValue, static view => view.comboBox.SelectedIndex).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.ComboValue, static view => view.labelCombo.Text).DisposeWith(d);
			});

			ViewModel = new MainViewModel();
		}
	}
}
