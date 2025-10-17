using System.Diagnostics;
using System.Reactive.Linq;
using Myra.Graphics2D.UI;
using Myra.MML;

namespace ReactiveUI.Myra
{
	public class ActivationForViewFetcher : IActivationForViewFetcher
	{
		public int GetAffinityForView(Type view)
		{
			return typeof(BaseObject).IsAssignableFrom(view) ? 10 : 0;
		}

		public IObservable<bool> GetActivationForView(IActivatableView? view)
		{
			if (view is Widget widget)
			{
				var placeChanged = Observable.FromEvent<EventHandler, bool>(
					eventHandler =>
					{
						void Handler(object? sender, EventArgs e) => eventHandler(widget.IsPlaced);
						return Handler;
					},
					h => widget.PlacedChanged += h,
					h => widget.PlacedChanged -= h);

				var visibleChanged = Observable.FromEvent<EventHandler, bool>(
					eventHandler =>
					{
						void Handler(object? sender, EventArgs e) => eventHandler(widget.IsPlaced);
						return Handler;
					}, h => widget.VisibleChanged += h, h => widget.VisibleChanged -= h);

				var controlActivation = placeChanged.Merge(visibleChanged).DistinctUntilChanged();

				if (view is Window window)
				{
					var windowClosed = Observable.FromEvent<EventHandler, bool>(
						eventHandler =>
						{
							void Handler(object? sender, EventArgs e) => eventHandler(window.Visible && window.IsPlaced);
							return Handler;
						},
						h => window.Closed += h,
						h => window.Closed -= h);

					controlActivation = controlActivation.Merge(windowClosed).DistinctUntilChanged();
				}

				return controlActivation;
			}

			if (view == null)
			{
				Trace.WriteLine("Expected a view of type Myra.Graphics2D.UI.Widget, it was null");
			}
			else
			{
				Trace.WriteLine($"Expected a view of type Myra.Graphics2D.UI.Widget, but it is {view.GetType()}.\r\n  You need to implement your own IActivationForViewFetcher.");
			}

			return Observable.Empty<bool>();
		}
	}
}
