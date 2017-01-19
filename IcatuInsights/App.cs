using Prism.Unity;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Practices.Unity;

using IcatuInsights.Views;
using System.Net.Http;

namespace IcatuInsights
{
    public class App : PrismApplication
    {
        public static HttpClient _httpClient { get; set; }

        protected override void OnInitialized()
        {
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