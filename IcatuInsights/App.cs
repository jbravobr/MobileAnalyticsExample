using Prism.Unity;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Practices.Unity;

using IcatuInsights.Views;
using System.Net.Http;
using Plugin.GoogleAnalytics;

namespace IcatuInsights
{
    public class App : PrismApplication
    {
        public static HttpClient _httpClient { get; set; }

        protected override void OnInitialized()
        {

            GoogleAnalytics.Current.Config.TrackingId = "UA-90605167-1";
            GoogleAnalytics.Current.Config.AppId = "90605167";
            GoogleAnalytics.Current.Config.AppName = "MobileAnalyticsExample";
            GoogleAnalytics.Current.Config.AppVersion = "1.0.0.0";
            GoogleAnalytics.Current.InitTracker();


            // Mobile Center Init
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

            // Enable Analytics
            Analytics.Enabled = true;

            // Enable Crashes
            Crashes.Enabled = true;

            // Start Crashes Thread on Server
            //Crashes.GenerateTestCrash();

            // App First Page Load
            NavigationService.NavigateAsync(PageNames.FirstPage);

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<FirstPage>();
            Container.RegisterTypeForNavigation<SecondPage>();
            Container.RegisterTypeForNavigation<DetailPage>();

            Container.RegisterInstance(Acr.UserDialogs.UserDialogs.Instance);
        }
    }
}