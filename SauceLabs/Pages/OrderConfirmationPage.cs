using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoAutomation.Pages
{
    public class OrderConfirmationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By ConfirmationMessage = By.ClassName("complete-header");

        public OrderConfirmationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public string GetConfirmationMessage()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(ConfirmationMessage)).Text;
        }
    }
}