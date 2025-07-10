using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoAutomation.Pages
{
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By FirstNameField = By.Id("first-name");
        private readonly By LastNameField = By.Id("last-name");
        private readonly By ZipCodeField = By.Id("postal-code");
        private readonly By ContinueButton = By.Id("continue");
        private readonly By FinishButton = By.Id("finish");

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void EnterCheckoutInformation(string firstName, string lastName, string zipCode)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(FirstNameField)).SendKeys(firstName);
            _driver.FindElement(LastNameField).SendKeys(lastName);
            _driver.FindElement(ZipCodeField).SendKeys(zipCode);
            _driver.FindElement(ContinueButton).Click();
        }

        public void CompleteCheckout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(FinishButton)).Click();
        }
    }
}