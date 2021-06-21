using AdidasChallenge.EndToEndTests.Drivers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace AdidasChallenge.EndToEndTests.Steps
{
    [Binding]
    public class ReviewPostingSteps
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly MobileDriver _androidDriver;
        private readonly ProductApiDriver _productApiDriver;
        private readonly ReviewApiDriver _reviewApiDriver;
        private AppiumDriver<AppiumWebElement> _appiumDriver;
        private ReadOnlyCollection<AppiumWebElement> _productsItems;
        private string _productId;
        private AppiumWebElement _firstReview;
        private string _reviewText;

        public ReviewPostingSteps(ScenarioContext scenarioContext, MobileDriver androidDriver, ProductApiDriver productApiDriver, ReviewApiDriver reviewApiDriver)
        {
            _scenarioContext = scenarioContext;
            _androidDriver = androidDriver;
            _productApiDriver = productApiDriver;
            _reviewApiDriver = reviewApiDriver;
        }

        [Scope(Feature = "ReviewPosting")]
        [Given(@"product name is '(.*)' Created")]
        public void GivenProductNameIsCreated(string productName)
        {
            _productId = Guid.NewGuid().ToString();
            //I assume that we run the test on a clean database
            _productApiDriver.AddProduct(new Product
            {
                Description = "any",
                Id = Guid.NewGuid().ToString(),
                Name = productName,
                ImgUrl = "https://assets.adidas.com/images/w_320,h_320,f_auto,q_auto:sensitive,fl_lossy/6634cf36274b4ea5ac46ac4e00b2021e_9366/Superstar_Shoes_Black_FY0071_01_standard.jpg"
            });
        }

        [Given(@"a review text is '(.*)' and rate is '(.*)'")]
        public void GivenAReviewTextIsAndRateIs(string text, int rating)
        {
            _reviewText = text;
            _reviewApiDriver.AddReview(new Review
            {
                ProductId = _productId,
                Rating = rating,
                Text = text
            });
        }

        [When(@"navigate to product review")]
        public void WhenNavigateToProductReview()
        {
            _appiumDriver = _androidDriver.Current;
            //We can replace this with driver waiting 
            Thread.Sleep(TimeSpan.FromSeconds(2));
            var productsView = _appiumDriver.FindElementById("com.example.challenge:id/recyclerview");
            // get all childeren items of the view
            var productsItems = productsView.FindElementsByClassName("android.view.ViewGroup");

            // as our app is not connected to our local service ( container), we will just simulate it for the first product
            // click first product
            var firstProduct = productsItems.First();
            firstProduct.Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            var listofReviewItems = _appiumDriver.FindElementById("com.example.challenge:id/reviewsRecycler");

            // as our app is not connected to our local service ( container), we will just simulate it for the first review
            _firstReview = listofReviewItems.FindElementsByClassName("android.widget.TextView")?.First();
           
        }

        [Then(@"then proudct is shown")]
        public void ThenThenProudctIsShown()
        {
            // assert that our reveiw include our reveiw text
            _firstReview.Text.Should().Contain(_reviewText);
        }
    }
}
