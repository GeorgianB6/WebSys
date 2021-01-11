using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.PageObjects.Controllers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnitTestProject1.PageObjects
{
    public class LogInPage
    {
        private IWebDriver driver;
        public LoggedOutMenuItemControl menuItemControl => new LoggedOutMenuItemControl(driver);

        public LogInPage(IWebDriver browser)
        {
            driver = browser;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private By email = By.Id("loginusername");
        private IWebElement TxtUsername()
        {
            return driver.FindElement(email);
        }

        private By password = By.Id("loginpassword");
        private IWebElement TxtPassword()
        {
            return driver.FindElement(password);
        }

        private By logIn = By.XPath("//button[@onclick='logIn()']");
        private IWebElement BtnLogIn()
        {
            return driver.FindElement(logIn);
        }

        public void NavigateToLogInPage()
        {
            menuItemControl.NavigateToLogInPage();
        }

        public void LogInApplication(string username, string password)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(email));

            
            TxtUsername().SendKeys(username);
            TxtPassword().SendKeys(password);
            BtnLogIn().Click();
        }

    }
}