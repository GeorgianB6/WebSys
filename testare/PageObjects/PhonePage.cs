using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.PageObjects.Controllers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnitTestProject1.PageObjects
{
    public class PhonePage
    {
        private IWebDriver driver;
        public LoggedInMenuItemControl menuItemControl => new LoggedInMenuItemControl(driver);

        public PhonePage(IWebDriver browser)
        {
            driver = browser;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(list));
        }

        private By list = By.Id("tbodyid");
        private IWebElement BtnList => driver.FindElement(list);

        private By phone = By.XPath("//a[@href='prod.html?idp_=1']");
        private IWebElement BtnPhone => driver.FindElement(phone);

        public PhoneDetailPage NavigateToPhoneDetailPage()
        {
            BtnPhone.Click();
            return new PhoneDetailPage(driver);
        }
    }
}