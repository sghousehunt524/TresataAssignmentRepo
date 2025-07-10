using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By UsernameField = By.Id("user-name");
        private readonly By PasswordField = By.Id("password");
        private readonly By LoginButton = By.Id("login-button");
        private readonly By ErrorMessage = By.CssSelector(".error-message-container");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void EnterCredentials(string username, string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(UsernameField)).SendKeys(username);
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClickLogin()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public string GetErrorMessage()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(ErrorMessage)).Text;
        }
    }
}