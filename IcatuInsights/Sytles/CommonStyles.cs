using Xamarin.Forms;

namespace IcatuInsights
{
    public static class CommonStyles
    {
        public static Color BrandColor = Color.FromHex("#F8B81F");

        public static readonly Style BrandNameOrnamentStyle = new Style(typeof(BoxView))
        {
            Setters =
            {
                new Setter
                {
                    Property = VisualElement.HeightRequestProperty,
                    Value=4
                },
                new Setter
                {
                    Property = View.VerticalOptionsProperty,
                    Value="End"
                },
                new Setter
                {
                    Property = View.HorizontalOptionsProperty,
                    Value="Start"
                },
                new Setter
                {
                    Property=VisualElement.WidthRequestProperty,
                    Value=40
                },
                new Setter
                {
                    Property = VisualElement.BackgroundColorProperty,
                    Value = BrandColor
                }
            }
        };
    }
}