using System.Reactive.Disposables.Fluent;
using Myra.ReactiveUI;
using ReactiveUI;

namespace Myra.Samples.ReactiveUI
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
			});

			ViewModel = new MainViewModel();
		}
	}
}
