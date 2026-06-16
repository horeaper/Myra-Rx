using System.Collections.ObjectModel;
using Myra.Graphics2D.UI;

namespace ReactiveUI.Myra
{
	public class ContainerSetMethodBindingConverter : ISetMethodBindingConverter
	{
		public int GetAffinityForObjects(Type? fromType, Type? toType)
		{
			if (toType != typeof(ObservableCollection<Widget>))
			{
				return 0;
			}

			return fromType?.GetInterfaces().Any(static x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>) && x.GetGenericArguments()[0].IsSubclassOf(typeof(Widget))) ?? false
				? 10
				: 0;
		}

		public object PerformSet(object? toTarget, object? newValue, object?[]? arguments)
		{
			ArgumentNullException.ThrowIfNull(toTarget);

			if (toTarget is not ObservableCollection<Widget> targetCollection)
			{
				throw new ArgumentException($"{nameof(toTarget)} must be of type ObservableCollection<Widget>", nameof(toTarget));
			}
			if (newValue is not IEnumerable<Widget> newValueEnumerable)
			{
				throw new ArgumentException("newValue must be IEnumerable<Widget>", nameof(newValue));
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