using Acr.UserDialogs;

namespace IcatuInsights
{
    public static class CustomDialogs
    {
        public static void ShowLoadingWithMessage(IUserDialogs userDialogs, string msg)
        {
            userDialogs.ShowLoading(msg);
        }

        public static void HideLoading(IUserDialogs userDialogs)
        {
            userDialogs.HideLoading();
        }
    }
}