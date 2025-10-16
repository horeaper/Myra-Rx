using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;
using Myra.Graphics2D.UI;
using ReactiveUI;

namespace Myra.ReactiveUI
{
	public sealed class CreatesMyraCommandBinding : ICreatesCommandBinding
	{
		[RequiresDynamicCode("GetAffinityForObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("GetAffinityForObject uses methods that may require unreferenced code")]
		public int GetAffinityForObject(Type type, bool hasEventTarget)
		{
			if (typeof(ButtonBase2).IsAssignableFrom(type) || typeof(MenuItem).IsAssignableFrom(type))
			{
				return 10;
			}
			if (hasEventTarget)
			{
				return 6;
			}

			return 0;
		}

		public int GetAffinityForObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.PublicProperties)] T>(bool hasEventTarget)
		{
			var type = typeof(T);

			if (typeof(ButtonBase2).IsAssignableFrom(type) || typeof(MenuItem).IsAssignableFrom(type))
			{
				return 10;
			}
			if (hasEventTarget)
			{
				return 6;
			}

			return 0;
		}

		[RequiresDynamicCode("BindCommandToObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("BindCommandToObject uses methods that may require unreferenced code")]
		public IDisposable? BindCommandToObject(ICommand? command, object? target, IObservable<object?> commandParameter)
		{
			ArgumentNullException.ThrowIfNull(command);
			ArgumentNullException.ThrowIfNull(target);

			var type = target.GetType();
			if (typeof(CheckButtonBase).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "PressedChanged");
			}
			if (typeof(ButtonBase2).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "Click");
			}
			if (typeof(MenuItem).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "Selected");
			}

			return null;
		}

		[RequiresDynamicCode("BindCommandToObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("BindCommandToObject uses methods that may require unreferenced code")]
		public IDisposable BindCommandToObject<TEventArgs>(ICommand? command, object? target, IObservable<object?> commandParameter, string eventName)
		{
			ArgumentNullException.ThrowIfNull(command);
			ArgumentNullException.ThrowIfNull(target);

			var ret = new CompositeDisposable();
			object? latestParameter = null;
			var targetType = target.GetType();

			ret.Add(commandParameter.Subscribe(x => latestParameter = x));

			var evt = Observable.FromEventPattern<TEventArgs>(target, eventName);
			ret.Add(evt.Subscribe(_ =>
			{
				if (command.CanExecute(latestParameter))
				{
					command.Execute(latestParameter);
				}
			}));

			if (typeof(Widget).IsAssignableFrom(targetType) || typeof(MenuItem).IsAssignableFrom(targetType))
			{
				var enabledProperty = targetType.GetProperty("Enabled", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
				if (enabledProperty != null)
				{
					object? latestParam = null;
					ret.Add(commandParameter.Subscribe(x => latestParam = x));

					ret.Add(Observable.FromEvent<EventHandler, bool>(
							eventHandler => (_, __) => eventHandler(command.CanExecute(latestParam)),
							x => command.CanExecuteChanged += x,
							x => command.CanExecuteChanged -= x)
						.StartWith(command.CanExecute(latestParam))
						.Subscribe(x => enabledProperty.SetValue(target, x, null)));
				}
			}

			return ret;
		}
	}
}
