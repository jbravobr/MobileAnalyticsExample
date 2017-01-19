using Humanizer;

namespace IcatuInsights
{
    public static class StringOperations
    {
        public static string NormalizeName(string text)
        {
            return To.TitleCase.Transform(text);
        }
    }
}