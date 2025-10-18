using System.Reactive;
using System.Reactive.Linq;
using Myra.Events;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.ColorPicker;
using Myra.Platform;

namespace ReactiveUI.Myra
{
	public static class RxEventExtensions
	{
		public static DesktopRxEvents Events(this Desktop item) => new(item);

		public static MenuItemRxEvents Events(this MenuItem item) => new(item);
		public static TabItemRxEvents Events(this TabItem item) => new(item);

		public static WidgetRxEvents Events(this Widget item) => new(item);
		public static ButtonBase2RxEvents Events(this ButtonBase2 item) => new(item);
		public static CheckButtonRxEvents Events(this CheckButton item) => new(item);
		public static RadioButtonRxEvents Events(this RadioButton item) => new(item);
		public static ToggleButtonRxEvents Events(this ToggleButton item) => new(item);
		public static SliderRxEvents Events(this Slider item) => new(item);
		public static ProgressBarRxEvents Events(this ProgressBar item) => new(item);
		public static SpinButtonRxEvents Events(this SpinButton item) => new(item);
		public static TextBoxRxEvents Events(this TextBox item) => new(item);
		public static ListViewRxEvents Events(this ListView item) => new(item);
	}

	public class MenuItemRxEvents(MenuItem data)
	{
		public IObservable<EventPattern<EventArgs>> Selected =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.Selected += x,
				x => data.Selected -= x);

		public IObservable<EventPattern<EventArgs>> Changed =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.Changed += x,
				x => data.Changed -= x);
	}

	public class TabItemRxEvents(TabItem data)
	{
		public IObservable<EventPattern<EventArgs>> Changed =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.Changed += x,
				x => data.Changed -= x);

		public IObservable<EventPattern<EventArgs>> SelectedChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.SelectedChanged += x,
				x => data.SelectedChanged -= x);
	}

	public class DesktopRxEvents(Desktop data)
	{
		public IObservable<EventPattern<EventArgs>> MouseMoved =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.MouseMoved += x,
				x => data.MouseMoved -= x);

		public IObservable<EventPattern<EventArgs>> TouchMoved =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchMoved += x,
				x => data.TouchMoved -= x);

		public IObservable<EventPattern<EventArgs>> TouchDown =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchDown += x,
				x => data.TouchDown -= x);

		public IObservable<EventPattern<EventArgs>> TouchUp =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchUp += x,
				x => data.TouchUp -= x);

		public IObservable<EventPattern<EventArgs>> TouchDoubleClick =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchDoubleClick += x,
				x => data.TouchDoubleClick -= x);

		public IObservable<EventPattern<GenericEventArgs<float>>> MouseWheelChanged =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<float>>, GenericEventArgs<float>>(
				x => data.MouseWheelChanged += x,
				x => data.MouseWheelChanged -= x);

		public IObservable<EventPattern<GenericEventArgs<Keys>>> KeyUp =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<Keys>>, GenericEventArgs<Keys>>(
				x => data.KeyUp += x,
				x => data.KeyUp -= x);

		public IObservable<EventPattern<GenericEventArgs<Keys>>> KeyDown =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<Keys>>, GenericEventArgs<Keys>>(
				x => data.KeyDown += x,
				x => data.KeyDown -= x);

		public IObservable<EventPattern<GenericEventArgs<char>>> Char =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<char>>, GenericEventArgs<char>>(
				x => data.Char += x,
				x => data.Char -= x);
	}

	public class WidgetRxEvents(Widget data)
	{
		public IObservable<EventPattern<EventArgs>> PlacedChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.PlacedChanged += x,
				x => data.PlacedChanged -= x);

		public IObservable<EventPattern<EventArgs>> VisibleChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.VisibleChanged += x,
				x => data.VisibleChanged -= x);

		public IObservable<EventPattern<EventArgs>> EnabledChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.EnabledChanged += x,
				x => data.EnabledChanged -= x);

		public IObservable<EventPattern<EventArgs>> LocationChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.LocationChanged += x,
				x => data.LocationChanged -= x);

		public IObservable<EventPattern<EventArgs>> SizeChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.SizeChanged += x,
				x => data.SizeChanged -= x);

		public IObservable<EventPattern<EventArgs>> ArrangeUpdated =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.ArrangeUpdated += x,
				x => data.ArrangeUpdated -= x);

		public IObservable<EventPattern<EventArgs>> MouseLeft =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.MouseLeft += x,
				x => data.MouseLeft -= x);

		public IObservable<EventPattern<EventArgs>> MouseEntered =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.MouseEntered += x,
				x => data.MouseEntered -= x);

		public IObservable<EventPattern<EventArgs>> MouseMoved =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.MouseMoved += x,
				x => data.MouseMoved -= x);

		public IObservable<EventPattern<EventArgs>> TouchLeft =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchLeft += x,
				x => data.TouchLeft -= x);

		public IObservable<EventPattern<EventArgs>> TouchEntered =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchEntered += x,
				x => data.TouchEntered -= x);

		public IObservable<EventPattern<EventArgs>> TouchMoved =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchMoved += x,
				x => data.TouchMoved -= x);

		public IObservable<EventPattern<EventArgs>> TouchDown =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchDown += x,
				x => data.TouchDown -= x);

		public IObservable<EventPattern<EventArgs>> TouchUp =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchUp += x,
				x => data.TouchUp -= x);

		public IObservable<EventPattern<EventArgs>> TouchDoubleClick =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.TouchDoubleClick += x,
				x => data.TouchDoubleClick -= x);

		public IObservable<EventPattern<EventArgs>> KeyboardFocusChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.KeyboardFocusChanged += x,
				x => data.KeyboardFocusChanged -= x);

		public IObservable<EventPattern<GenericEventArgs<float>>> MouseWheelChanged =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<float>>, GenericEventArgs<float>>(
				x => data.MouseWheelChanged += x,
				x => data.MouseWheelChanged -= x);

		public IObservable<EventPattern<GenericEventArgs<Keys>>> KeyUp =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<Keys>>, GenericEventArgs<Keys>>(
				x => data.KeyUp += x,
				x => data.KeyUp -= x);

		public IObservable<EventPattern<GenericEventArgs<Keys>>> KeyDown =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<Keys>>, GenericEventArgs<Keys>>(
				x => data.KeyDown += x,
				x => data.KeyDown -= x);

		public IObservable<EventPattern<GenericEventArgs<char>>> Char =>
			Observable.FromEventPattern<EventHandler<GenericEventArgs<char>>, GenericEventArgs<char>>(
				x => data.Char += x,
				x => data.Char -= x);
	}

	public class ButtonBase2RxEvents(ButtonBase2 data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> Click =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.Click += x,
				x => data.Click -= x);


		public IObservable<EventPattern<EventArgs>> PressedChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.PressedChanged += x,
				x => data.PressedChanged -= x);

		public IObservable<EventPattern<ValueChangingEventArgs<bool>>> PressedChangingByUser =>
			Observable.FromEventPattern<EventHandler<ValueChangingEventArgs<bool>>, ValueChangingEventArgs<bool>>(
				x => data.PressedChangingByUser += x,
				x => data.PressedChangingByUser -= x);
	}

	public class CheckButtonRxEvents(CheckButton data) : ButtonBase2RxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> IsCheckedChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.IsCheckedChanged += x,
				x => data.IsCheckedChanged -= x);
	}

	public class RadioButtonRxEvents(RadioButton data) : ButtonBase2RxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> IsCheckedChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.IsCheckedChanged += x,
				x => data.IsCheckedChanged -= x);
	}

	public class ToggleButtonRxEvents(ToggleButton data) : ButtonBase2RxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> IsToggledChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.IsToggledChanged += x,
				x => data.IsToggledChanged -= x);
	}

	public class SliderRxEvents(Slider data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<ValueChangedEventArgs<float>>> ValueChanged =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<float>>, ValueChangedEventArgs<float>>(
				x => data.ValueChanged += x,
				x => data.ValueChanged -= x);

		public IObservable<EventPattern<ValueChangedEventArgs<float>>> ValueChangedByUser =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<float>>, ValueChangedEventArgs<float>>(
				x => data.ValueChangedByUser += x,
				x => data.ValueChangedByUser -= x);
	}

	public class ProgressBarRxEvents(ProgressBar data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<ValueChangedEventArgs<float>>> ValueChanged =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<float>>, ValueChangedEventArgs<float>>(
				x => data.ValueChanged += x,
				x => data.ValueChanged -= x);
	}

	public class SpinButtonRxEvents(SpinButton data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<ValueChangingEventArgs<float?>>> ValueChanging =>
			Observable.FromEventPattern<EventHandler<ValueChangingEventArgs<float?>>, ValueChangingEventArgs<float?>>(
				x => data.ValueChanging += x,
				x => data.ValueChanging -= x);

		public IObservable<EventPattern<ValueChangedEventArgs<float?>>> ValueChanged =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<float?>>, ValueChangedEventArgs<float?>>(
				x => data.ValueChanged += x,
				x => data.ValueChanged -= x);

		public IObservable<EventPattern<ValueChangedEventArgs<float?>>> ValueChangedByUser =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<float?>>, ValueChangedEventArgs<float?>>(
				x => data.ValueChangedByUser += x,
				x => data.ValueChangedByUser -= x);
	}

	public class TextBoxRxEvents(TextBox data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<ValueChangingEventArgs<string>>> ValueChanging =>
			Observable.FromEventPattern<EventHandler<ValueChangingEventArgs<string>>, ValueChangingEventArgs<string>>(
				x => data.ValueChanging += x,
				x => data.ValueChanging -= x);

		public IObservable<EventPattern<ValueChangedEventArgs<string>>> TextChanged =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<string>>, ValueChangedEventArgs<string>>(
				x => data.TextChanged += x,
				x => data.TextChanged -= x);

		public IObservable<EventPattern<ValueChangedEventArgs<string>>> TextChangedByUser =>
			Observable.FromEventPattern<EventHandler<ValueChangedEventArgs<string>>, ValueChangedEventArgs<string>>(
				x => data.TextChangedByUser += x,
				x => data.TextChangedByUser -= x);

		public IObservable<EventPattern<TextDeletedEventArgs>> TextDeleted =>
			Observable.FromEventPattern<EventHandler<TextDeletedEventArgs>, TextDeletedEventArgs>(
				x => data.TextDeleted += x,
				x => data.TextDeleted -= x);

		public IObservable<EventPattern<EventArgs>> CursorPositionChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.CursorPositionChanged += x,
				x => data.CursorPositionChanged -= x);
	}

	public class ListViewRxEvents(ListView data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> SelectedIndexChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.SelectedIndexChanged += x,
				x => data.SelectedIndexChanged -= x);
	}

	public class ColorPickerPanelRxEvents(ColorPickerPanel data) : WidgetRxEvents(data)
	{
		public IObservable<EventPattern<EventArgs>> ColorChanged =>
			Observable.FromEventPattern<EventHandler, EventArgs>(
				x => data.ColorChanged += x,
				x => data.ColorChanged -= x);
	}
}
