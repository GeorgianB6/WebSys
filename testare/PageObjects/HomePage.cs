using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.PageObjects.Controllers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnitTestProject1.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        public LoggedInMenuItemControl menuItemControl => new LoggedInMenuItemControl(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(welcomeUserEmail));
        }

        private By welcomeUserEmail = By.Id("nameofuser");
        private IWebElement LblWelcomeUserEmail => driver.FindElement(welcomeUserEmail);

        private By phone = By.XPath("//a[contains(text(),'phone')]");
        private IWebElement BtnPhone => driver.FindElement(phone);

        public PhonePage NavigateToPhonePage()
        {
            BtnPhone.Click();
            return new PhonePage(driver);
        }

        private By cart = By.Id("cartur");
        private IWebElement BtnCart => driver.FindElement(cart);

        public CartPage NavigateToCartPage()
        {
            BtnPhone.Click();
            return new CartPage(driver);
        }
    }
}