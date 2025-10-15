using System.ComponentModel;
using Myra.Graphics2D.UI.Styles;

namespace Myra.Graphics2D.UI
{
	public class HorizontalProgressBar : ProgressBar
	{
		public override Orientation Orientation
		{
			get { return Orientation.Horizontal; }
		}

		[DefaultValue(HorizontalAlignment.Stretch)]
		public override HorizontalAlignment HorizontalAlignment
		{
			get => base.HorizontalAlignment;
			set => base.HorizontalAlignment = value;
		}

		[DefaultValue(VerticalAlignment.Top)]
		public override VerticalAlignment VerticalAlignment
		{
			get => base.VerticalAlignment;
			set => base.VerticalAlignment = value;
		}

		public HorizontalProgressBar(string styleName = Stylesheet.DefaultStyleName) : base(styleName)
		{
			base.HorizontalAlignment = HorizontalAlignment.Stretch;
			base.VerticalAlignment = VerticalAlignment.Top;
		}

		protected override void InternalSetStyle(Stylesheet stylesheet, string name)
		{
			ApplyProgressBarStyle(stylesheet.HorizontalProgressBarStyles.SafelyGetStyle(name));
		}
	}
}
