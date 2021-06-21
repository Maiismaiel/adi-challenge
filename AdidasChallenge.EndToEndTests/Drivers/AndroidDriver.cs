using AdidasChallenge.EndToEndTests.Configs;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Drivers
{
    [Binding]
    public class MobileDriver : IDisposable
    {
        private readonly AppiumConfig _appiumConfig;
        private readonly Lazy<AppiumDriver<AppiumWebElement>> _currentAppiumDriverLazy;
        private bool _isDisposed;

        public MobileDriver(ConfigProvider configProvider)
        {
            _appiumConfig = configProvider.AppSettings?.AppiumConfig;
            _currentAppiumDriverLazy = new Lazy<AppiumDriver<AppiumWebElement>>(CreateAppiumDriver);
        }

        /// <summary>
        /// The Selenium IAppiumDriver instance
        /// </summary>
        public AppiumDriver<AppiumWebElement> Current => _currentAppiumDriverLazy.Value;

        /// <summary>
        /// Creates the Selenium web driver (opens a browser)
        /// </summary>
        /// <returns></returns>
        private AppiumDriver<AppiumWebElement> CreateAppiumDriver()
        {
            var driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, _appiumConfig.Platform);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, _appiumConfig.DeviceName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, _appiumConfig.ApkPath);

            var appiumService = new AppiumServiceBuilder()
               .WithIPAddress(_appiumConfig.IpAddress)
               .UsingPort(_appiumConfig.Port)
               .Build();

            appiumService.Start();
            return new AndroidDriver<AppiumWebElement>(appiumService, driverOptions);
        }

        /// <summary>
        /// Disposes the Selenium web driver (closing the browser)
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentAppiumDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
