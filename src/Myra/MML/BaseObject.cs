using Myra.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Myra.MML
{
	public class BaseObject: IItemWithId, INotifyAttachedPropertyChanged, INotifyPropertyChanged
	{
		private string _id;
		private object _tag;

		[DefaultValue(null)]
		[Bindable(true)]
		public string Id
		{
			get => _id;
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
		
		[Browsable(false)]
		[XmlIgnore]
		[Bindable(true)]
		public object Tag
		{
			get => _tag;
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

		[XmlIgnore]
		[Browsable(false)]
		public readonly Dictionary<int, object> AttachedPropertiesValues = new Dictionary<int, object>();

		/// <summary>
		/// Holds custom user attributes not mapped to the object
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public Dictionary<string, string> UserData { get; private set; } = new Dictionary<string, string>();

		/// <summary>
		/// External files used by this object
		/// </summary>
		[XmlIgnore]
		[Browsable(false)]
		public Dictionary<string, string> Resources { get; private set; } = new Dictionary<string, string>();

		public event EventHandler IdChanged;

		protected internal virtual void OnIdChanged()
		{
			IdChanged.Invoke(this);
		}

		public virtual void OnAttachedPropertyChanged(BaseAttachedPropertyInfo propertyInfo)
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
