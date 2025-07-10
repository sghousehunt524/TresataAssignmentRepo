using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoAutomation.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By InventoryList = By.ClassName("inventory_list");
        private readonly By CartBadge = By.ClassName("shopping_cart_badge");
        private readonly By MenuButton = By.Id("react-burger-menu-btn");
        private readonly By LogoutLink = By.Id("logout_sidebar_link");

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public bool IsInventoryDisplayed()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(InventoryList)).Displayed;
        }

        public void AddProductToCart(string productName)
        {
            var addToCartButton = By.Id($"add-to-cart-{productName.ToLower().Replace(" ", "-")}");
            _wait.Until(ExpectedConditions.ElementToBeClickable(addToCartButton)).Click();
        }

        public string GetCartBadgeCount()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(CartBadge)).Text;
        }

        public void NavigateToCart()
        {
            _driver.FindElement(By.ClassName("shopping_cart_link")).Click();
        }

        public void ClickMenu()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(MenuButton)).Click();
        }

        public void ClickLogout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LogoutLink)).Click();
        }
    }
}