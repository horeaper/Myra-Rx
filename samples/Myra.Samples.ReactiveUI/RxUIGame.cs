using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using Myra.Samples.AllWidgets;

namespace Myra.Samples.ReactiveUI
{
	public class RxUIGame : Game
	{
		readonly GraphicsDeviceManager _graphics;
		MainView _mainView;
		Desktop _desktop;

		public RxUIGame()
		{
			_graphics = new GraphicsDeviceManager(this) {
				PreferredBackBufferWidth = 1024,
				PreferredBackBufferHeight = 720
			};
			Window.AllowUserResizing = false;
			IsMouseVisible = true;
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			MyraEnvironment.Platform = new MGPlatform(GraphicsDevice);

			_mainView = new MainView();
			_desktop = new Desktop {
				Root = _mainView,
			};
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			GraphicsDevice.Clear(Color.CornflowerBlue);
			_desktop.Render();
		}
	}
}
