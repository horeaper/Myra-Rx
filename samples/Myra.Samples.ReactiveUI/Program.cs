using ReactiveUI.Builder;
using ReactiveUI.Myra;

namespace Myra.Samples.RxUI
{
	class Program
	{
		static void Main(string[] args)
		{
			RxAppBuilder.CreateReactiveUIBuilder().WithMyra().Build();

			using var game = new RxUIGame();
			game.Run();
		}
	}
}