using Prism.Mvvm;
using Prism.Navigation;

using System;

using PropertyChanged;
using Prism.Services;
using Xamarin.Forms;
using System.IO;
using Plugin.GoogleAnalytics;

namespace IcatuInsights.ViewModels
{
    [ImplementPropertyChanged]
    public class DetailPageViewModel : BindableBase, INavigationAware
    {
        public string Quote { get; private set; }
        public Result Person { get; private set; }

        readonly IPageDialogService _pageDialogService;

        async void GetQuote()
        {
            try
            {
                if (App._httpClient == null)
                    App._httpClient = HttpAccessInstance.GetClient;

                HttpAccessInstance.SetBaseURL(APIConstants.QUOTE_API);

                var data = await App._httpClient.GetAsync(APIConstants.QUOTE_API_PARAMS);
                if (!data.IsSuccessStatusCode)
                    throw new InvalidOperationException("Erro ao acessar API de citações");

                var jsonString = await data.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(jsonString))
                    throw new InvalidOperationException("Erro ao ler informações da API de citações");

                Quote = (Newtonsoft.Json.JsonConvert.DeserializeObject<Quotes>(jsonString)).QuoteText;
            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync($"Erro ao carregar detalhes de {Person.Name.FullName}, por favor tente novamente",
                                                     string.Empty,
                                                     "OK");
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Person"))
                Person = (Result)parameters["Person"];
        }

        public DetailPageViewModel(IPageDialogService pageDialogService)
        {
            GoogleAnalytics.Current.Tracker.SendView("DetailPage");
            _pageDialogService = pageDialogService;
            GetQuote();
        }
    }
}