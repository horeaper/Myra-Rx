using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;
using Myra.Graphics2D.UI;

namespace ReactiveUI.Myra
{
	public sealed class CreatesMyraCommandBinding : ICreatesCommandBinding
	{
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
			if (typeof(Widget).IsAssignableFrom(type))
			{
				return 4;
			}

			return 0;
		}

#if NET6_0_OR_GREATER
		public int GetAffinityForObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.PublicProperties)] T>(bool hasEventTarget)
#else
		public int GetAffinityForObject<T>(bool hasEventTarget)
#endif
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
			if (typeof(Widget).IsAssignableFrom(type))
			{
				return 4;
			}

			return 0;
		}

#if NET6_0_OR_GREATER
		[RequiresDynamicCode("BindCommandToObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("BindCommandToObject uses methods that may require unreferenced code")]
#endif
		public IDisposable? BindCommandToObject(ICommand? command, object? target, IObservable<object?> commandParameter)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			var type = target.GetType();
			if (typeof(ButtonBase2).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "Click");
			}
			if (typeof(MenuItem).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "Selected");
			}
			if (typeof(Widget).IsAssignableFrom(type))
			{
				return BindCommandToObject<EventArgs>(command, target, commandParameter, "TouchUp");
			}

			return null;
		}

#if NET6_0_OR_GREATER
		[RequiresDynamicCode("BindCommandToObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("BindCommandToObject uses methods that may require unreferenced code")]
#endif
		public IDisposable BindCommandToObject<TEventArgs>(ICommand? command, object? target, IObservable<object?> commandParameter, string eventName)
		{
			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

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
