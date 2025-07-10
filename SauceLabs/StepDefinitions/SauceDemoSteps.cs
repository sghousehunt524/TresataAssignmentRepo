using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.StepDefinitions
{
    [Binding]
    public class SauceDemoSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly ProductsPage _productsPage;
        private readonly CartPage _cartPage;
        private readonly CheckoutPage _checkoutPage;
        private readonly OrderConfirmationPage _orderConfirmationPage;

        public SauceDemoSteps(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _loginPage = new LoginPage(_driver);
            _productsPage = new ProductsPage(_driver);
            _cartPage = new CartPage(_driver);
            _checkoutPage = new CheckoutPage(_driver);
            _orderConfirmationPage = new OrderConfirmationPage(_driver);           
            
        }

        [Given(@"I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            _loginPage.NavigateTo();
        }

        [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
        public void GivenIAmLoggedInAs(string username, string password)
        {
            _loginPage.NavigateTo();
            _loginPage.EnterCredentials(username, password);
            _loginPage.ClickLogin();
        }       

        [When(@"I enter valid credentials with username ""(.*)"" and password ""(.*)""")]
        [When(@"I enter credentials with username ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterCredentials(string username, string password)
        {
            _loginPage.EnterCredentials(username, password);
        }

        [When(@"I enter invalid credentials with username ""(.*)"" and password ""(.*)""")]
        public void WhenIenterinvalidcredentialswithusernameandpassword(string username, string password)
        {
            _loginPage.EnterCredentials(username, password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLogin();
        }

        [Then(@"I should be redirected to the products page")]
        public void ThenIShouldBeRedirectedToTheProductsPage()
        {
            Assert.IsTrue(_productsPage.IsInventoryDisplayed(), "Products page is not displayed.");
        }

        [Then(@"I should see the product inventory")]
        public void ThenIShouldSeeTheProductInventory()
        {
            Assert.IsTrue(_productsPage.IsInventoryDisplayed(), "Product inventory is not visible.");
        }

        [Then(@"I should see an error message ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedError)
        {
            var actualError = _loginPage.GetErrorMessage();
            Assert.AreEqual(expectedError, actualError, "Error message does not match.");
        }

        [When(@"I add the product ""(.*)"" to the cart")]
        public void WhenIAddTheProductToTheCart(string productName)
        {
            _productsPage.AddProductToCart(productName);
        }

        [Then(@"I should see the cart badge updated to ""(.*)""")]
        public void ThenIShouldSeeTheCartBadgeUpdatedTo(string expectedCount)
        {
            var actualCount = _productsPage.GetCartBadgeCount();
            Assert.AreEqual(expectedCount, actualCount, "Cart badge count does not match.");
        }

        [Then(@"I should see the product ""(.*)"" in the cart")]
        public void ThenIShouldSeeTheProductInTheCart(string productName)
        {
            _productsPage.NavigateToCart();
            Assert.IsTrue(_cartPage.IsProductInCart(productName), $"Product {productName} not found in cart.");
        }

        [When(@"I navigate to the cart")]
        public void WhenINavigateToTheCart()
        {
            _productsPage.NavigateToCart();
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            _cartPage.ProceedToCheckout();
        }

        [When(@"I enter checkout information with first name ""(.*)"", last name ""(.*)"", and zip code ""(.*)""")]
        public void WhenIEnterCheckoutInformation(string firstName, string lastName, string zipCode)
        {
            _checkoutPage.EnterCheckoutInformation(firstName, lastName, zipCode);
        }

        [When(@"I complete the checkout process")]
        public void WhenICompleteTheCheckoutProcess()
        {
            _checkoutPage.CompleteCheckout();
        }

        [Then(@"I should see the order confirmation message ""(.*)""")]
        public void ThenIShouldSeeTheOrderConfirmationMessage(string expectedMessage)
        {
            var actualMessage = _orderConfirmationPage.GetConfirmationMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Order confirmation message does not match.");
        }

        [When(@"I click the menu button")]
        public void WhenIClickTheMenuButton()
        {
            _productsPage.ClickMenu();
        }

        [When(@"I click the logout link")]
        public void WhenIClickTheLogoutLink()
        {
            _productsPage.ClickLogout();
        }

        [Then(@"I should be redirected to the login page")]
        public void ThenIShouldBeRedirectedToTheLoginPage()
        {
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"), "Not redirected to login page.");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }
    }
}