using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using Myra.MML;

namespace ReactiveUI.Myra
{
	public sealed class MyraCreatesObservableForProperty : ICreatesObservableForProperty
	{
#if NET6_0_OR_GREATER
		[RequiresDynamicCode("GetAffinityForObject uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("GetAffinityForObject uses methods that may require unreferenced code")]
#endif
		public int GetAffinityForObject(Type type, string propertyName, bool beforeChanged = false)
		{
			if (!typeof(BaseObject).IsAssignableFrom(type))
			{
				return 0;
			}

			var prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			if (prop == null)
			{
				return 0;
			}

			var attr = prop.GetCustomAttribute<BindableAttribute>(true);
			return !beforeChanged && attr?.Bindable == true ? 8 : 0;
		}

		public IObservable<IObservedChange<object?, object?>> GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged = false, bool suppressWarnings = false)
		{
			var type = sender.GetType();
			var ev = type.GetEvent("PropertyChanged", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			if (ev == null)
			{
				throw new NotSupportedException($"Could not find PropertyChanged event for property {propertyName} on type {type.Name}");
			}

			return Observable.Create<IObservedChange<object, object?>>(subj =>
			{
				var completed = false;
				var handler = new PropertyChangedEventHandler((o, e) =>
				{
					if (completed)
					{
						return;
					}
					if (e.PropertyName != propertyName)
					{
						return;
					}

					try
					{
						subj.OnNext(new ObservedChange<object, object?>(sender, expression, null));
					}
					catch (Exception ex)
					{
						subj.OnError(ex);
						completed = true;
					}
				});

				ev.AddEventHandler(sender, handler);
				return Disposable.Create(() => ev.RemoveEventHandler(sender, handler));
			});
		}
	}
}
