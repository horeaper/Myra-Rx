using Myra.ReactiveUI;
using ReactiveUI.Builder;
using Splat;

namespace Myra.Samples.ReactiveUI
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ReactiveUIBuilder(Locator.CurrentMutable, Locator.Current).WithMyra();
			builder.BuildApp();

			using var game = new RxUIGame();
			game.Run();
		}
	}
}
