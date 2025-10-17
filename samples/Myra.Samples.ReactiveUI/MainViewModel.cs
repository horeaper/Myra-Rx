using ReactiveUI;

namespace Myra.Samples.ReactiveUI
{
	public class MainViewModel : ReactiveObject
	{
		public float FloatValue
		{
			get => _FloatValue;
			set => this.RaiseAndSetIfChanged(ref _FloatValue, value);
		}

		float _FloatValue;

		public string TextValue
		{
			get => _TextValue;
			set => this.RaiseAndSetIfChanged(ref _TextValue, value);
		}

		string _TextValue = string.Empty;
	}
}
