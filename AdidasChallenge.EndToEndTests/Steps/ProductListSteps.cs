using AdidasChallenge.EndToEndTests.Drivers;
using FluentAssertions;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Steps
{
    [Binding]
    public class ProductListSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MobileDriver _androidDriver;
        private readonly ProductApiDriver _productApiDriver;
        private AppiumDriver<AppiumWebElement> _appiumDriver;
        private ReadOnlyCollection<AppiumWebElement> _productsItems;

        public ProductListSteps(ScenarioContext scenarioContext, MobileDriver androidDriver, ProductApiDriver productApiDriver)
        {
            _scenarioContext = scenarioContext;
            _androidDriver = androidDriver;
            _productApiDriver = productApiDriver;
        }

        [Scope(Feature = "ProductList")]
        [Given(@"product name is '(.*)' Created")]
        public void GivenProductNameIsCreated(string productName)
        {
            //I assume that we run the test on a clean database
            _productApiDriver.AddProduct(new Product
            {
                Description = "any",
                Id = Guid.NewGuid().ToString(),
                Name = productName,
                ImgUrl = "https://assets.adidas.com/images/w_320,h_320,f_auto,q_auto:sensitive,fl_lossy/6634cf36274b4ea5ac46ac4e00b2021e_9366/Superstar_Shoes_Black_FY0071_01_standard.jpg"
            });
        }

        [When(@"mobile app is open")]
        public void WhenMobileAppIsOpen()
        {
            //open the app
            _appiumDriver = _androidDriver.Current;
           // Thread.Sleep(TimeSpan.FromSeconds(2));
        }

         

        [Then(@"home screen has at least a product")]
        public void ThenHomeScreenHasAtLeastAProduct()
        {
            // this to make sure we have at least one product when app opens
            var productsView = _appiumDriver.FindElementById("com.example.challenge:id/recyclerview");
            // get all childeren items of the view
            _productsItems = productsView.FindElementsByClassName("android.view.ViewGroup");
            _productsItems.Count.Should().BeGreaterThan(0);
        }

        [Then(@"home screen has product name is '(.*)'")]
        public void ThenHomeScreenHasProductNameIs(string productName)
        {
            // If search functionality is implemented I would have searched the product by my product name , then asserted that there is a result
            // as there is no search .. I will just add this implementation as an example
            var productFound = false;
            foreach (var item in _productsItems)
            {
                var productNameElement = item.FindElementById("com.example.challenge:id/productName"); 
               if(productNameElement.Text.Contains(productName))
                {
                    productFound = true;
                    break;
                }
            }

            productFound.Should().BeTrue();

        } 

    }
}
