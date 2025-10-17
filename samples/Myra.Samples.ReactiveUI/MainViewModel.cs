using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Myra.Samples.RxUI
{
	public partial class MainViewModel : ReactiveObject
	{
		[Reactive]
		float _floatValue;

		[Reactive]
		string _textValue = string.Empty;

		IObservable<bool> _canExecute;

		public MainViewModel()
		{
			_canExecute = this.WhenAnyValue(static x => x.TextValue, static text => !string.IsNullOrEmpty(text));
		}

		[ReactiveCommand]
		void SetSliderValue()
		{
			FloatValue = 50;
		}
	}
}
