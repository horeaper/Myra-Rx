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
				this.Bind(ViewModel, static vm => vm.FloatValue, static view => view._slider.Value).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.FloatValue, static view => view._sliderText.Text).DisposeWith(d);

				this.Bind(ViewModel, static vm => vm.TextValue, static view => view._textBox.Text).DisposeWith(d);
				this.OneWayBind(ViewModel, static vm => vm.TextValue, static view => view._label.Text).DisposeWith(d);

				this.BindCommand(ViewModel, static vm => vm.SetSliderValueCommand, static view => view._button).DisposeWith(d);
			});

			ViewModel = new MainViewModel();
		}
	}
}
