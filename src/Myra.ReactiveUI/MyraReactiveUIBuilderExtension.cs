using System.Diagnostics.CodeAnalysis;
using System.Reactive.Concurrency;
using ReactiveUI.Builder;

namespace ReactiveUI.Myra
{
	public static class MyraReactiveUIBuilderExtension
	{
		public static IScheduler MyraMainThreadScheduler { get; } = new WaitForDispatcherScheduler(static () => new SynchronizationContextScheduler(SynchronizationContext.Current!));

		[SuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "Not using reflection")]
		[SuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "Not using reflection")]
		public static IReactiveUIBuilder WithMyra(this IReactiveUIBuilder builder)
		{
			ArgumentNullException.ThrowIfNull(builder);

			return ((IReactiveUIBuilder)builder.WithCoreServices())
				.WithMainThreadScheduler(MyraMainThreadScheduler)
				.WithTaskPoolScheduler(TaskPoolScheduler.Default)
				.WithPlatformModule<Registrations>();
		}
	}
}