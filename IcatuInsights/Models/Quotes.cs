using Newtonsoft.Json;

namespace IcatuInsights
{
    public class Quotes
    {
        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }

        [JsonProperty("quoteAuthor")]
        public string QuoteAuthor { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("senderLink")]
        public string SenderLink { get; set; }

        [JsonProperty("quoteLink")]
        public string QuoteLink { get; set; }
    }
}