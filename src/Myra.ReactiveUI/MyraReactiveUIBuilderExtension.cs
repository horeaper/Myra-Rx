using System.Reactive.Concurrency;
using ReactiveUI.Builder;

namespace ReactiveUI.Myra
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
	}
}
