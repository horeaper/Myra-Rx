using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace Myra.ReactiveUI
{
	public sealed class Registrations : IWantsToRegisterStuff
	{
		public void Register(Action<Func<object>, Type> registerFunction)
		{
			registerFunction(static () => new PlatformOperations(), typeof(IPlatformOperations));

			registerFunction(static () => new CreatesMyraCommandBinding(), typeof(ICreatesCommandBinding));
			registerFunction(static () => new MyraCreatesObservableForProperty(), typeof(ICreatesObservableForProperty));
			registerFunction(static () => new ActivationForViewFetcher(), typeof(IActivationForViewFetcher));

			registerFunction(static () => new StringConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new IntegerToStringTypeConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new NullableIntegerToStringTypeConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new SingleToStringTypeConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new DoubleToStringTypeConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new DecimalToStringTypeConverter(), typeof(IBindingTypeConverter));
			registerFunction(static () => new ComponentModelTypeConverter(), typeof(IBindingTypeConverter));

			if (!ModeDetector.InUnitTestRunner())
			{
				RxApp.MainThreadScheduler = new WaitForDispatcherScheduler(static () => new SynchronizationContextScheduler(SynchronizationContext.Current!));
			}
		}
	}
}
