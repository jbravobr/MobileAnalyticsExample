using Android.App;
using Android.Content.PM;
using Android.OS;

using Microsoft.Azure.Mobile;
using Xamarin.Forms;

namespace IcatuInsights.Droid
{
    [Activity(Label = "IcatuInsights.Droid",
              Icon = "@drawable/icon",
              Theme = "@style/MyTheme",
              MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Mobile Center Insights Init
            MobileCenter.Configure("ead5a287-487d-4248-af2b-773e8e717a32");

            //Circle Image Init
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            //UserDialogs
            Acr.UserDialogs.UserDialogs.Init((Activity)Forms.Context);

            LoadApplication(new App());
        }
    }
}