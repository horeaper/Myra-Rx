using System.Collections.ObjectModel;
using Myra.Graphics2D.UI;

namespace ReactiveUI.Myra
{
	public class MenuSetMethodBindingConverter : ISetMethodBindingConverter
	{
		public int GetAffinityForObjects(Type? fromType, Type? toType)
		{
			if (toType != typeof(ObservableCollection<IMenuItem>))
			{
				return 0;
			}

			return fromType?.GetInterfaces().Any(static x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>) && x.GetGenericArguments()[0].IsAssignableTo(typeof(IMenuItem))) ?? false
				? 10
				: 0;
		}

		public object PerformSet(object? toTarget, object? newValue, object?[]? arguments)
		{
			ArgumentNullException.ThrowIfNull(toTarget);

			if (toTarget is not ObservableCollection<IMenuItem> targetCollection)
			{
				throw new ArgumentException($"{nameof(toTarget)} must be of type ObservableCollection<IMenuItem>", nameof(toTarget));
			}
			if (newValue is not IEnumerable<IMenuItem> newValueEnumerable)
			{
				throw new ArgumentException("newValue must be IEnumerable<IMenuItem>", nameof(newValue));
			}

			targetCollection.Clear();
			foreach (var item in newValueEnumerable.ToList())
			{
				targetCollection.Add(item);
			}

			return targetCollection;
		}
	}
}