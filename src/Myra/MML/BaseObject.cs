using Myra.Events;
using Myra.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Myra.MML
{
	/// <summary>
	/// Base class for all objects that support identifiers and attached properties.
	/// </summary>
	public class BaseObject: IItemWithId, INotifyAttachedPropertyChanged, INotifyPropertyChanged
	{
		private string _id = null;
		private object _tag;

		/// <summary>
		/// Gets or sets the unique identifier for this object.
		/// </summary>
		[DefaultValue(null)]
		[Bindable(true)]
		public string Id
		{
			get
			{
				return _id;
			}

			set
			{
				if (value == _id)
				{
					return;
				}

				_id = value;
				OnPropertyChanged();
				OnIdChanged();
			}
		}

		/// <summary>
		/// Gets or sets an arbitrary object value that can be used to store custom information about this element.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public object Tag
		{
			get
			{
				return _tag;
			}

			set
			{
				if (value == _tag)
				{
					return;
				}

				_tag = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets the dictionary storing values for all attached properties on this object.
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public readonly Dictionary<int, object> AttachedPropertiesValues = new Dictionary<int, object>();

		/// <summary>
		/// Gets a dictionary of custom user attributes not mapped to the object.
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public Dictionary<string, string> UserData { get; private set; } = new Dictionary<string, string>();

		/// <summary>
		/// Occurs when the Id property changes.
		/// </summary>
		public event MyraEventHandler IdChanged;

		/// <summary>
		/// Called when the Id property changes.
		/// </summary>
		protected internal virtual void OnIdChanged()
		{
			IdChanged.Invoke(this, Graphics2D.UI.InputEventType.ValueChanged);
		}

		/// <summary>
		/// Called when an attached property changes on this object.
		/// </summary>
		/// <param name="propertyInfo">Information about the attached property that changed.</param>
		public virtual void OnAttachedPropertyChanged(BaseAttachedPropertyInfo propertyInfo)
		{
		}

		/// <summary>
		/// Occurs when a bindable property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Invoked whenever the effective value of any bindable property on this <see cref="T:Myra.MML.BaseObject" /> has been updated.
		/// </summary>
		/// <param name="propertyName">Name of the changed property.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}