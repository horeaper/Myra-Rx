using System.ComponentModel;
using Myra.Graphics2D.UI;
using ReactiveUI;

namespace Myra.ReactiveUI
{
	public class ReactivePanel<TViewModel> : Panel, IViewFor<TViewModel>
		where TViewModel : class
	{
		[Bindable(true)]
		public TViewModel? ViewModel { get; set; }

		object? IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (TViewModel?)value;
		}
	}

	public class ReactiveWindow<TViewModel> : Window, IViewFor<TViewModel>
		where TViewModel : class
	{
		[Bindable(true)]
		public TViewModel? ViewModel { get; set; }

		object? IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (TViewModel?)value;
		}
	}

	public class ReactiveDialog<TViewModel> : Dialog, IViewFor<TViewModel>
		where TViewModel : class
	{
		[Bindable(true)]
		public TViewModel? ViewModel { get; set; }

		object? IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (TViewModel?)value;
		}
	}
}
