using System.ComponentModel;
using Myra.Graphics2D.UI.Styles;

namespace Myra.Graphics2D.UI
{
	public class VerticalProgressBar : ProgressBar
	{
		public override Orientation Orientation
		{
			get { return Orientation.Vertical; }
		}

		[DefaultValue(HorizontalAlignment.Left)]
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

		public VerticalProgressBar(string styleName = Stylesheet.DefaultStyleName) : base(styleName)
		{
			base.HorizontalAlignment = HorizontalAlignment.Left;
			base.VerticalAlignment = VerticalAlignment.Stretch;
		}

		protected override void InternalSetStyle(Stylesheet stylesheet, string name)
		{
			ApplyProgressBarStyle(stylesheet.VerticalProgressBarStyles.SafelyGetStyle(name));
		}
	}
}
