using System.Reactive.Concurrency;
using ReactiveUI;
using ReactiveUI.Builder;

namespace Myra.ReactiveUI
{
	public static class MyraReactiveUIBuilderExtension
	{
		public static IScheduler MyraMainThreadScheduler { get; } = new WaitForDispatcherScheduler(static () => new SynchronizationContextScheduler(SynchronizationContext.Current!));

		public static IReactiveUIBuilder WithMyra(this IReactiveUIBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder
				.WithMainThreadScheduler(MyraMainThreadScheduler)
				.WithPlatformModule<Registrations>();
		}

		public static IReactiveUIBuilder WithMyraScheduler(this IReactiveUIBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder
				.WithMainThreadScheduler(MyraMainThreadScheduler);
		}
	}
}
