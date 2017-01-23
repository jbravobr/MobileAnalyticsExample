using Foundation;
using Microsoft.Azure.Mobile;
using UIKit;

namespace IcatuInsights.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			//Mobile Center Insights Init
			MobileCenter.Configure("ead5a287-487d-4248-af2b-773e8e717a32");

			//Circle Image Init
			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

			return base.FinishedLaunching (app, options);
		}
	}
}
