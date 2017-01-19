using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IcatuInsights
{
    public sealed class HttpAccessInstance
    {
        static volatile HttpAccessInstance instance;
        static readonly object syncLock = new object();
        static HttpClient _httpClient;
        static string BASE_URL = string.Empty;

        public HttpAccessInstance()
        {
            try
            {   
                var url = new Uri("https://randomuser.me/");
                var httpClient = new HttpClient
                {
                    BaseAddress = !string.IsNullOrEmpty(BASE_URL) ? new Uri(BASE_URL) : url,
                    Timeout = TimeSpan.FromSeconds(40)
                };

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                _httpClient = httpClient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HttpClient GetClient
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                            instance = new HttpAccessInstance();
                    }
                }

                return _httpClient;
            }
        }

        public static void SetBaseURL(string url)
        {
            BASE_URL = url;

            if (instance != null)
                _httpClient.BaseAddress = new Uri(BASE_URL);
        }
    }
}