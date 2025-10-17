using System.Diagnostics.CodeAnalysis;
using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace Myra.ReactiveUI
{
	public sealed class Registrations : IWantsToRegisterStuff
	{
#if NET6_0_OR_GREATER
		[RequiresDynamicCode("Register uses methods that require dynamic code generation")]
		[RequiresUnreferencedCode("Register uses methods that may require unreferenced code")]
#endif
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
#if NET6_0_OR_GREATER
			registerFunction(static () => new ComponentModelTypeConverter(), typeof(IBindingTypeConverter));
#endif

			if (!ModeDetector.InUnitTestRunner())
			{
				RxApp.MainThreadScheduler = new WaitForDispatcherScheduler(static () => new SynchronizationContextScheduler(SynchronizationContext.Current!));
			}
		}
	}
}
