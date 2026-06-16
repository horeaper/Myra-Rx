namespace ReactiveUI.Myra
{
	public sealed class Registrations : IWantsToRegisterStuff
	{
		public void Register(IRegistrar registrar)
		{
			registrar.RegisterConstant<IPlatformOperations>(static () => new PlatformOperations());

			registrar.RegisterConstant<ICreatesCommandBinding>(static () => new CreatesMyraCommandBinding());
			registrar.RegisterConstant<ICreatesObservableForProperty>(static () => new MyraCreatesObservableForProperty());
			registrar.RegisterConstant<IActivationForViewFetcher>(static () => new ActivationForViewFetcher());
			registrar.RegisterConstant<ISetMethodBindingConverter>(static () => new ContainerSetMethodBindingConverter());
			registrar.RegisterConstant<ISetMethodBindingConverter>(static () => new MenuSetMethodBindingConverter());

			registrar.RegisterConstant<IBindingTypeConverter>(static () => new StringConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new ByteToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new IntegerToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new NullableIntegerToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new SingleToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new NullableSingleToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new DoubleToStringTypeConverter());
			registrar.RegisterConstant<IBindingTypeConverter>(static () => new DecimalToStringTypeConverter());
			registrar.RegisterConstant<IBindingFallbackConverter>(static () => new ComponentModelFallbackConverter());
		}
	}
}