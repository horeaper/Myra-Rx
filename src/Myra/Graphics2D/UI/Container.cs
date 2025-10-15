using Myra.Attributes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Drawing;
#endif

namespace Myra.Graphics2D.UI
{
	public abstract class Container : Widget
	{
		[Content]
		[Browsable(false)]
		public virtual ObservableCollection<Widget> Widgets => Children;

		[DefaultValue(HorizontalAlignment.Stretch)]
		public override HorizontalAlignment HorizontalAlignment
		{
			get => base.HorizontalAlignment;
			set => base.HorizontalAlignment = value;
		}

		[DefaultValue(VerticalAlignment.Stretch)]
		public override VerticalAlignment VerticalAlignment
		{
			get => base.VerticalAlignment;
			set => base.VerticalAlignment = value;
		}

		protected Container()
		{
			base.HorizontalAlignment = HorizontalAlignment.Stretch;
			base.VerticalAlignment = VerticalAlignment.Stretch;
		}

		[Obsolete("Use Widgets.Add")]
		public void AddChild(Widget child)
		{
			Widgets.Add(child);
		}

		[Obsolete("Use Widgets.Remove")]
		public void RemoveChild(Widget child)
		{
			Widgets.Remove(child);
		}

		public override bool InputFallsThrough(Point localPos) => Background == null;

		protected internal override void CopyFrom(Widget w)
		{
			base.CopyFrom(w);

			var container = (Container)w;
			foreach(var child in container.Widgets)
			{
				Widgets.Add(child.Clone());
			}
		}
	}
}
