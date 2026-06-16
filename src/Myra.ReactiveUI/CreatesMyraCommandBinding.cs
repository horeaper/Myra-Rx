using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;
using Myra.Events;
using Myra.Graphics2D.UI;

namespace ReactiveUI.Myra
{
	public sealed class CreatesMyraCommandBinding : ICreatesCommandBinding
	{
		public int GetAffinityForObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.PublicProperties)] T>(bool hasEventTarget)
		{
			var type = typeof(T);

			if (typeof(ButtonBase).IsAssignableFrom(type) || typeof(MenuItem).IsAssignableFrom(type))
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

		[RequiresUnreferencedCode("String/reflection-based event binding may require members removed by trimming.")]
		public IDisposable? BindCommandToObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)] T>(ICommand? command, T? target, IObservable<object?> commandParameter)
			where T : class
		{
			ArgumentNullException.ThrowIfNull(target);

			if (command == null)
			{
				return Disposable.Empty;
			}

			if (target is ButtonBase)
			{
				return BindCommandToObject<T, MyraEventArgs>(command, target, commandParameter, "Click");
			}
			if (target is MenuItem)
			{
				return BindCommandToObject<T, MyraEventArgs>(command, target, commandParameter, "Selected");
			}
			if (target is Widget)
			{
				return BindCommandToObject<T, MyraEventArgs>(command, target, commandParameter, "TouchUp");
			}

			return null;
		}

		[RequiresUnreferencedCode("String/reflection-based event binding may require members removed by trimming.")]
		public IDisposable BindCommandToObject<T, TEventArgs>(ICommand? command, T? target, IObservable<object?> commandParameter, string eventName)
			where T : class
		{
			ArgumentNullException.ThrowIfNull(target);

			if (command == null)
			{
				return Disposable.Empty;
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
				var enabledProperty = targetType.GetProperty("Enabled", BindingFlags.Public | BindingFlags.Instance);
				if (enabledProperty != null)
				{
					ret.Add(Observable.FromEvent<EventHandler, bool>(
							eventHandler => (_, _) => eventHandler(command.CanExecute(latestParameter)),
							x => command.CanExecuteChanged += x,
							x => command.CanExecuteChanged -= x)
						.StartWith(command.CanExecute(latestParameter))
						.Subscribe(x => enabledProperty.SetValue(target, x, null)));
				}
			}

			return ret;
		}

		public IDisposable? BindCommandToObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)] T, TEventArgs>(ICommand? command, T? target, IObservable<object?> commandParameter, Action<EventHandler<TEventArgs>> addHandler, Action<EventHandler<TEventArgs>> removeHandler)
			where T : class
			where TEventArgs : EventArgs
		{
			ArgumentNullException.ThrowIfNull(target);
			ArgumentNullException.ThrowIfNull(addHandler);
			ArgumentNullException.ThrowIfNull(removeHandler);

			if (command == null)
			{
				return Disposable.Empty;
			}

			object? latestParameter = null;
			var targetType = target.GetType();

			void Handler(object? s, TEventArgs e)
			{
				var param = Volatile.Read(ref latestParameter);
				if (command.CanExecute(param))
				{
					command.Execute(param);
				}
			}

			var ret = new CompositeDisposable
			{
				commandParameter.Subscribe(x => Volatile.Write(ref latestParameter, x))
			};

			addHandler(Handler);
			ret.Add(Disposable.Create(() => removeHandler(Handler)));

			if (typeof(Widget).IsAssignableFrom(targetType) || typeof(MenuItem).IsAssignableFrom(targetType))
			{
				var enabledProperty = targetType.GetProperty("Enabled", BindingFlags.Public | BindingFlags.Instance);
				if (enabledProperty != null)
				{
					ret.Add(Observable.FromEvent<EventHandler, bool>(
							eventHandler => (_, _) => eventHandler(command.CanExecute(Volatile.Read(ref latestParameter))),
							x => command.CanExecuteChanged += x,
							x => command.CanExecuteChanged -= x)
						.StartWith(command.CanExecute(latestParameter))
						.Subscribe(x => enabledProperty.SetValue(target, x, null)));
				}
			}

			return ret;
		}
	}
}