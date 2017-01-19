using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

using Xamarin.Forms;

using Microsoft.Azure.Mobile.Analytics;

using System.Threading.Tasks;
using System.Collections.Generic;
using Acr.UserDialogs;

namespace IcatuInsights.ViewModels
{
    public class FirstPageViewModel : BindableBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        readonly INavigationService _navigationService;
        readonly IPageDialogService _pageDialogService;
        readonly IUserDialogs _userDialogs;

        public Command Logon
        {
            get
            {
                return new Command(async () =>
                {
                    CustomDialogs.ShowLoadingWithMessage(_userDialogs, "Verificando");
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    {
                        await _pageDialogService.DisplayAlertAsync("Usuário/senha inválidos",
                                                             string.Empty,
                                                             "OK");
                        // Tracking
                        Analytics.TrackEvent(GetEnumDescriptionAttribute.GetDescription(AnalytcsEventsType.UsuarioSenhaEmBranco),
                                             new Dictionary<string, string> { { string.Empty, string.Empty } });
                        CustomDialogs.HideLoading(_userDialogs);
                    }

                    await Task.Delay(9000);
                    await Navigate(PageNames.SecondPage);
                });
            }
        }

        async Task Navigate(string page)
        {
            // Tracking
            Analytics.TrackEvent(GetEnumDescriptionAttribute.GetDescription(AnalytcsEventsType.LoginValido),
                                             new Dictionary<string, string> { { Username, Password } });
            await _navigationService.NavigateAsync(page);
        }

        public FirstPageViewModel(INavigationService navigationService,
                                  IPageDialogService pageDialogService,
                                  IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _userDialogs = userDialogs;
        }

        // XAML Previewer (Comentar o construtor com DI)
        public FirstPageViewModel() { }
    }
}