using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SauceDemoAutomation.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By CartItem = By.ClassName("cart_item");
        private readonly By CheckoutButton = By.Id("checkout");

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public bool IsProductInCart(string productName)
        {
            var itemName = _wait.Until(ExpectedConditions.ElementIsVisible(CartItem))
                                .FindElement(By.ClassName("inventory_item_name")).Text;
            return itemName.Contains(productName);
        }

        public void ProceedToCheckout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(CheckoutButton)).Click();
        }
    }
}