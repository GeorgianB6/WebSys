using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.PageObjects.Controllers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnitTestProject1.PageObjects
{
    public class PhoneDetailPage
    {
        private IWebDriver driver;
        public LoggedInMenuItemControl menuItemControl => new LoggedInMenuItemControl(driver);

        public PhoneDetailPage(IWebDriver browser)
        {
            driver = browser;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(cart));
        }

        private By cart = By.XPath("//a[@onlick='addToCart(1)']");
        private IWebElement BtnAddToCart => driver.FindElement(cart);

        public void addPhoneToCart()
        {
            BtnAddToCart.Click();
        }


    }
}