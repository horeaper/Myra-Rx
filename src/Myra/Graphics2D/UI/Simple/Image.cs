using System.ComponentModel;
using Myra.Graphics2D.UI.Styles;
using Myra.Utility;
using Myra.Attributes;


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
	/// <summary>
	/// Specifies how an image is resized to fit available space.
	/// </summary>
	public enum ImageResizeMode
	{
		/// <summary>
		/// Stretch the image to fill available space without preserving aspect ratio.
		/// </summary>
		Stretch,

		/// <summary>
		/// Resize the image while maintaining its aspect ratio.
		/// </summary>
		KeepAspectRatio
	}

	/// <summary>
	/// An image widget that displays a texture region with optional resize modes.
	/// </summary>
	public class Image : Widget
	{
		private IImage[] _renderables = new IImage[WidgetVisualStateTotal];

		private Color _color = Color.White;
		private ImageResizeMode _resizeMode;

#if MONOGAME
		private bool _isAnisotropicFiltering = false;

		/// <summary>
		/// Gets or sets a value indicating whether anisotropic filtering is applied to the image.
		/// </summary>
		[DefaultValue(false)]
		[Bindable(true)]
		public bool IsAnisotropicFiltering
		{
			get
			{
				return _isAnisotropicFiltering;
			}
			set
			{
				_isAnisotropicFiltering = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}
#endif

		/// <summary>
		/// Gets or sets the renderable to display in the image widget's normal state.
		/// </summary>
		[Category("Appearance/Renderable")]
		[StylePropertyPath("Image")]
		[Bindable(true)]
		public IImage Renderable
		{
			get
			{
				return _renderables[WidgetVisualStateNormal];
			}

			set
			{
				if (Equals(value, _renderables[WidgetVisualStateNormal]))
				{
					return;
				}

				_renderables[WidgetVisualStateNormal] = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}

		/// <summary>
		/// Gets or sets the renderable to display when the image widget is disabled.
		/// </summary>
		[Category("Appearance/Renderable")]
		[StylePropertyPath("DisabledImage")]
		[Bindable(true)]
		public IImage DisabledRenderable
		{
			get
			{
				return _renderables[WidgetVisualStateDisabled];
			}

			set
			{
				if (Equals(value, _renderables[WidgetVisualStateDisabled]))
				{
					return;
				}

				_renderables[WidgetVisualStateDisabled] = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}

		/// <summary>
		/// Gets or sets the renderable to display when the mouse is over the image widget.
		/// </summary>
		[Category("Appearance/Renderable")]
		[StylePropertyPath("OverImage")]
		[Bindable(true)]
		public IImage OverRenderable
		{
			get
			{
				return _renderables[WidgetVisualStateOver];
			}

			set
			{
				if (Equals(value, _renderables[WidgetVisualStateOver]))
				{
					return;
				}

				_renderables[WidgetVisualStateOver] = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}

		/// <summary>
		/// Gets or sets the renderable to display when the image widget has focus.
		/// </summary>
		[Category("Appearance/Renderable")]
		[StylePropertyPath("FocusedImage")]
		[Bindable(true)]
		public IImage FocusedRenderable
		{
			get
			{
				return _renderables[WidgetVisualStateFocused];
			}

			set
			{
				if (Equals(value, _renderables[WidgetVisualStateFocused]))
				{
					return;
				}

				_renderables[WidgetVisualStateFocused] = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}

		/// <summary>
		/// Gets or sets the renderable to display when the image widget is pressed.
		/// </summary>
		[Category("Appearance/Renderable")]
		[StylePropertyPath("PressedImage")]
		[Bindable(true)]
		public IImage PressedRenderable
		{
			get
			{
				return _renderables[WidgetVisualStatePressed];
			}

			set
			{
				if (Equals(value, _renderables[WidgetVisualStatePressed]))
				{
					return;
				}

				_renderables[WidgetVisualStatePressed] = value;
				OnPropertyChanged();
				InvalidateMeasure();
			}
		}

		/// <summary>
		/// Gets or sets the color multiplier applied when rendering the image.
		/// </summary>
		[Category("Appearance")]
		[DefaultValue("#FFFFFFFF")]
		[Bindable(true)]
		public Color Color
		{
			get
			{
				return _color;
			}

			set
			{
				if (value.Equals(_color))
				{
					return;
				}
				
				_color = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets how the image is resized to fit available space.
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(ImageResizeMode.Stretch)]
		[Bindable(true)]
		public ImageResizeMode ResizeMode
		{
			get
			{
				return _resizeMode;
			}

			set
			{
				if (value == _resizeMode)
				{
					return;
				}
				
				_resizeMode = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Measures the size required for the image, considering all image states (normal, over, pressed).
		/// </summary>
		/// <param name="availableSize">The available size for the image.</param>
		/// <returns>The measured size needed for the image.</returns>
		protected override Point InternalMeasure(Point availableSize)
		{
			var result = Mathematics.PointZero;

			for (var i = 0; i < WidgetVisualStateTotal; ++i)
			{
				if (_renderables[i] != null)
				{
					var sz = _renderables[i].Size;

					if (sz.X > result.X)
					{
						result.X = sz.X;
					}

					if (sz.Y > result.Y)
					{
						result.Y = sz.Y;
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Renders the image with the appropriate state-based image (normal, over, or pressed).
		/// </summary>
		/// <param name="context">The render context to draw with.</param>
		public override void InternalRender(RenderContext context)
		{
			var image = GetCurrentVisual(_renderables);
			if (image == null)
			{
				return;
			}

			var bounds = ActualBounds;
			if (ResizeMode == ImageResizeMode.KeepAspectRatio)
			{
				var aspect = (float)image.Size.X / image.Size.Y;
				bounds.Height = (int)(bounds.Width * aspect);
			}

#if MONOGAME
			context.SetAnisotropicFilteringMode(_isAnisotropicFiltering);
#endif
			image.Draw(context, bounds, Color);
#if MONOGAME
			context.SetAnisotropicFilteringMode(false);
#endif
		}

		/// <summary>
		/// Applies the specified widget style to this image.
		/// </summary>
		/// <param name="style">The widget style to apply.</param>
		protected override void ApplyStyle(WidgetStyle style)
		{
			base.ApplyStyle(style);

			var imageStyle = (ImageStyle)style;
			Renderable = imageStyle.Image;
			DisabledRenderable = imageStyle.DisabledImage;
			FocusedRenderable = imageStyle.FocusedImage;
			OverRenderable = imageStyle.OverImage;
			PressedRenderable = imageStyle.PressedImage;
		}

		/// <summary>
		/// Applies the specified pressable image style to this image.
		/// </summary>
		/// <param name="style">The pressable image style to apply.</param>
		public void ApplyImageStyle(ImageStyle style) => ApplyStyle(style);

		/// <summary>
		/// Copies all properties from another widget to this image.
		/// </summary>
		/// <param name="w">The widget to copy properties from.</param>
		protected internal override void CopyFrom(Widget w)
		{
			base.CopyFrom(w);

			var image = (Image)w;

			Color = image.Color;
			ResizeMode = image.ResizeMode;

			for (var i = 0; i < WidgetVisualStateTotal; ++i)
			{
				_renderables[i] = image._renderables[i];
			}
		}
	}
}