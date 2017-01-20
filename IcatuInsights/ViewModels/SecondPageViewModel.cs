using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using PropertyChanged;

using Xamarin.Forms;

using Microsoft.Azure.Mobile.Analytics;

using Acr.UserDialogs;
using Plugin.GoogleAnalytics;

namespace IcatuInsights.ViewModels
{
    [ImplementPropertyChanged]
    public class SecondPageViewModel : BindableBase
    {
        public Person Persons { get; private set; }

        public Command<Result> PersonSelectedCmd
        {
            get
            {
                return new Command<Result>(async (Result obj) =>
                {
                    CustomDialogs.ShowLoadingWithMessage(_userDialogs, "Carregando");
                    if (obj == null)
                    {
                        CustomDialogs.HideLoading(_userDialogs);
                        throw new InvalidOperationException("Pessoa selecionada inválida!");
                    }

                    await Navigate(obj, PageNames.DetailPage);
                });
            }
        }

        readonly INavigationService _navigationService;
        readonly IUserDialogs _userDialogs;
        readonly IPageDialogService _pageDialogService;

        async void GetData()
        {
            try
            {
                CustomDialogs.ShowLoadingWithMessage(_userDialogs, "Carregando pessoas");
                if (App._httpClient == null)
                    App._httpClient = HttpAccessInstance.GetClient;

                HttpAccessInstance.SetBaseURL("https://randomuser.me/");

                var data = await App._httpClient.GetAsync("api/?results=50");
                if (!data.IsSuccessStatusCode)
                    throw new ArgumentException("Erro ao acessar API de pessoas!");

                var jsonString = await data.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(jsonString))
                    throw new ArgumentNullException(nameof(jsonString), "Dados retornardos vazios da API de pessoas!");

                Persons = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(jsonString);
                CustomDialogs.HideLoading(_userDialogs);
            }
            catch
            {
                CustomDialogs.HideLoading(_userDialogs);
                await _pageDialogService.DisplayAlertAsync("Erro ao carregar pessoas, por favor tente novamente",
                                                     string.Empty,
                                                     "OK");
            }
        }

        public async Task Navigate(Result person, string page)
        {
            GoogleAnalytics.Current.Tracker.SendEvent(GetEnumDescriptionAttribute.GetDescription(AnalytcsEventsType.PessoaSelecionada), "Pessoa Seleciona", person.Name.FullName);
            // Tracking
            Analytics.TrackEvent(GetEnumDescriptionAttribute.GetDescription(AnalytcsEventsType.PessoaSelecionada),
                                             new Dictionary<string, string> { { "Pessoa seleciona", person.Name.First } });

            var pageParameters = new NavigationParameters();
            pageParameters.Add("Person", person);

            await _navigationService.NavigateAsync(page, pageParameters);
            CustomDialogs.HideLoading(_userDialogs);
        }

        public SecondPageViewModel(INavigationService navigationService,
                                   IUserDialogs userDialogs,
                                   IPageDialogService pageDialogService)
        {
            GoogleAnalytics.Current.Tracker.SendView("SecondPage");
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            GetData();
        }
    }
}