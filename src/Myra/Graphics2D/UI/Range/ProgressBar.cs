using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Myra.Events;
using Myra.Graphics2D.UI.Styles;
using Myra.Utility;

#if MONOGAME || FNA
using Microsoft.Xna.Framework;
#elif STRIDE
using Stride.Core.Mathematics;
#else
using System.Drawing;
using Color = FontStashSharp.FSColor;
#endif

namespace Myra.Graphics2D.UI
{
	public abstract class ProgressBar : Widget
	{
		private float _value;
		private float _minimum;
		private float _maximum;
		private IBrush _filler;

		[Browsable(false)]
		[XmlIgnore]
		public abstract Orientation Orientation { get; }

		[Category("Behavior")]
		[DefaultValue(0.0f)]
		[Bindable(true)]
		public float Minimum
		{
			get => _minimum;
			set
			{
				if (value == _minimum)
				{
					return;
				}
				
				_minimum = value;
				OnPropertyChanged();
			}
		}

		[Category("Behavior")]
		[DefaultValue(100.0f)]
		[Bindable(true)]
		public float Maximum
		{
			get => _maximum;
			set
			{
				if (value == _maximum)
				{
					return;
				}
				
				_maximum = value;
				OnPropertyChanged();
			}
		}

		[Category("Behavior")]
		[DefaultValue(0.0f)]
		[Bindable(true)]
		public float Value
		{
			get => _value;
			set
			{
				if (_value.EpsilonEquals(value))
				{
					return;
				}

				var oldValue = _value;
				_value = value;
				OnPropertyChanged();
				ValueChanged?.Invoke(this, new ValueChangedEventArgs<float>(oldValue, value));
			}
		}

		[Category("Appearance")]
		[Bindable(true)]
		public IBrush Filler
		{
			get => _filler;
			set
			{
				if (value == _filler)
				{
					return;
				}
				
				_filler = value;
				OnPropertyChanged();
			}
		}

		public event EventHandler<ValueChangedEventArgs<float>> ValueChanged;

		protected ProgressBar(string styleName)
		{
			Maximum = 100;
			SetStyle(styleName);
		}

		public void ApplyProgressBarStyle(ProgressBarStyle style)
		{
			ApplyWidgetStyle(style);

			if (style.Filler == null)
				return;

			Filler = style.Filler;
		}

		public override void InternalRender(RenderContext context)
		{
			base.InternalRender(context);

			if (Filler == null)
			{
				return;
			}

			var v = _value;
			if (v < Minimum)
			{
				v = Minimum;
			}

			if (v > Maximum)
			{
				v = Maximum;
			}

			var delta = Maximum - Minimum;
			if (delta.IsZero())
			{
				return;
			}

			var filledPart = (v - Minimum) / delta;
			if (filledPart.EpsilonEquals(0.0f))
			{
				return;
			}

			var bounds = ActualBounds;
			if (Orientation == Orientation.Horizontal)
			{
				Filler.Draw(context,
					new Rectangle(bounds.X, bounds.Y, (int)(filledPart * bounds.Width), bounds.Height),
					Color.White);
			}
			else
			{
				Filler.Draw(context,
					new Rectangle(bounds.X, bounds.Y, bounds.Width, (int)(filledPart * bounds.Height)),
					Color.White);
			}
		}

		protected internal override void CopyFrom(Widget w)
		{
			base.CopyFrom(w);

			var progressBar = (ProgressBar)w;

			Minimum = progressBar.Minimum;
			Maximum = progressBar.Maximum;
			Value = progressBar.Value;
			Filler = progressBar.Filler;
		}
	}
}
