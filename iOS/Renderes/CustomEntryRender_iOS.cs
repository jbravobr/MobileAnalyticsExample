using CoreAnimation;
using CoreGraphics;
using IcatuInsights;
using IcatuInsights.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntryRender), typeof(CustomEntryRender_iOS))]
namespace IcatuInsights.iOS
{
	public class CustomEntryRender_iOS : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.BorderStyle = UITextBorderStyle.None;

				var view = (Element as CustomEntryRender);
				if (view != null)
				{
					var borderLayer = new CALayer();
					borderLayer.MasksToBounds = true;
					borderLayer.Frame = new CGRect(0f, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40f, 1.0f);
					borderLayer.BorderColor = UIColor.FromRGB(255, 255, 255).CGColor;
					borderLayer.BorderWidth = 1.0f;

					Control.Layer.AddSublayer(borderLayer);
					Control.BorderStyle = UITextBorderStyle.None;
				}
			}
		}
	}
}
