using AdidasChallenge.EndToEndTests.Configs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Drivers
{
    [Binding]
    public class ProductApiDriver
    {
        private readonly ConfigProvider _configProvider;
        private static readonly HttpClient _client = new HttpClient();
        public ProductApiDriver(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (_client.BaseAddress == null)
                _client.BaseAddress = new System.Uri(_configProvider.AppSettings?.ProductApiConfig?.BaseUrl);
        }

        public void AddProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var respponse = _client.PostAsync("product", body).Result;
            if (!respponse.IsSuccessStatusCode)
                throw new System.Exception($"Product is not crearted successfully, StatusCode {respponse.StatusCode}");
        }
    }

    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imgUrl")]
        public string ImgUrl { get; set; }
    }
}
