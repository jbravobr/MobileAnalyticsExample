using CoreGraphics;
using IcatuInsights;
using IcatuInsights.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomListViewSeparatorRenderer), typeof(ListViewRenderer_iOS))]
namespace IcatuInsights.iOS
{
	public class ListViewRenderer_iOS : ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
			}

			if (e.NewElement != null)
			{
				var EmptyView = new UIView(new CGRect()) { BackgroundColor = UIColor.Clear };
				Control.TableFooterView = EmptyView;
			}
		}
	}
}