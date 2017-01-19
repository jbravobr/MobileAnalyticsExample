using Prism.Mvvm;
using Prism.Navigation;

using System;

using PropertyChanged;
using Prism.Services;
using Xamarin.Forms;
using System.IO;

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

                HttpAccessInstance.SetBaseURL("http://api.forismatic.com/api/1.0/");

                var data = await App._httpClient.GetAsync("?method=getQuote&key=457653&format=json&lang=en");
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
            _pageDialogService = pageDialogService;
            GetQuote();
        }
    }
}