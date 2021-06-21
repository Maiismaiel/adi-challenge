using Newtonsoft.Json;
using System.IO;

namespace AdidasChallenge.EndToEndTests.Configs
{
    public class ConfigProvider
    {
        private readonly AppSettings _appSettings;

        public ConfigProvider()
        {
            string directoryName = Path.GetDirectoryName(typeof(ConfigProvider).Assembly.Location);
            var appsettingsJson = File.ReadAllText($"{directoryName}/appsettings.json");
            _appSettings = JsonConvert.DeserializeObject<AppSettings>(appsettingsJson);
        }

        public AppSettings AppSettings
        {
            get
            {
                return _appSettings;
            }
        }

    }
}
