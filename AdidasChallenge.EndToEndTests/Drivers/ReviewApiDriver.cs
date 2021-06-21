using AdidasChallenge.EndToEndTests.Configs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Drivers
{
    [Binding]
    public class ReviewApiDriver
    {
        private readonly ConfigProvider _configProvider;
        private static readonly HttpClient _client = new HttpClient();
        public ReviewApiDriver(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (_client.BaseAddress == null)
                _client.BaseAddress = new System.Uri(_configProvider.AppSettings?.ReviewsApiConfig?.BaseUrl);
        }

        public void AddReview(Review review)
        {
            var json = JsonConvert.SerializeObject(review);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var respponse = _client.PostAsync($"reviews/{review.ProductId}", body).Result;
            if (!respponse.IsSuccessStatusCode)
                throw new System.Exception($"Product is not crearted successfully, StatusCode {respponse.StatusCode}");
        }
    }

    public class Review
    {
        [JsonProperty("id")]
        public string ProductId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; } 
    }
}
