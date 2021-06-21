using AdidasChallenge.EndToEndTests.Drivers;
using FluentAssertions;
using OpenQA.Selenium.Appium;
using System;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Steps
{
    [Binding]
    public class ProductSearchSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MobileDriver _androidDriver;
        private AppiumDriver<AppiumWebElement> _appiumDriver;
        private string _searchPhrase;

        public ProductSearchSteps(ScenarioContext scenarioContext, MobileDriver androidDriver)
        {
            _scenarioContext = scenarioContext;
            _androidDriver = androidDriver;
        }

        [Given(@"app open on home screen")]
        public void GivenAppOpenOnHomeScreen()
        {
            _appiumDriver = _androidDriver.Current;
        }

        [Given(@"search phrase is '(.*)'")]
        public void GivenSearchPhraseIs(string searchPhrase)
        {
            _searchPhrase = searchPhrase;
        }

        [When(@"search phrase entered in the search box")]
        public void WhenSearchPhraseEnteredInTheSearchBox()
        {
            //When we have the search box implemented
            //we can add the search phrase optained from previous step to the search box using _appiumDriver

            /*
            var searchBox = _appiumDriver.FindElementById("searchboxId");
            searchBox.SendKeys(_searchPhrase);
            */

        }

        [Then(@"the matching products are showen in the result")]
        public void ThenTheMatchingProductsAreShowenInTheResult()
        {
            // Assert that result includes a product that matches the search phrase

            var productsView = _appiumDriver.FindElementById("com.example.challenge:id/recyclerview");
            // get all childeren items of the view
            var productsItems = productsView.FindElementsByClassName("android.view.ViewGroup");
            productsItems.Count.Should().BeGreaterThan(0);

            foreach (var item in productsItems)
            {
                var productNameElement = item.FindElementById("com.example.challenge:id/productName");
                productNameElement.Text.ToLower().Should().Contain(_searchPhrase.ToLower());
            }
        }
    }
}
