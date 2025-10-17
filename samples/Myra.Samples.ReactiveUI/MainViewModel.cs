using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Myra.Samples.RxUI
{
	public partial class MainViewModel : ReactiveObject
	{
		[Reactive]
		int _sliderValue;

		[Reactive]
		string _textValue = string.Empty;

		[Reactive]
		bool _boolValue = true;

		[Reactive]
		int _comboValue;

		IObservable<bool> _canExecute;

		public MainViewModel()
		{
			_canExecute = this.WhenAnyValue(static x => x.BoolValue);
		}

		[ReactiveCommand(CanExecute = nameof(_canExecute))]
		void ResetSliderValue()
		{
			SliderValue = 50;
		}
	}
}
