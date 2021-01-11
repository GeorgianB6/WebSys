using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.PageObjects.Controllers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UnitTestProject1.PageObjects
{
    public class SignUpPage
    {
        private IWebDriver driver;
        public LoggedOutMenuItemControl menuItemControl => new LoggedOutMenuItemControl(driver);

        public SignUpPage(IWebDriver browser)
        {
            driver = browser;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private By email = By.Id("sign-username");
        private IWebElement TxtUsername()
        {
            return driver.FindElement(email);
        }

        private By password = By.Id("sign-password");
        private IWebElement TxtPassword()
        {
            return driver.FindElement(password);
        }

        private By signUp = By.XPath("//button[@onclick='register()']");
        private IWebElement BtnSignUp()
        {
            return driver.FindElement(signUp);
        }

        public void NavigateToSignUpPage()
        {
            menuItemControl.NavigateToSignUpPage();
        }

        public void SignUpApplication(string username, string password)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(email));
            TxtUsername().SendKeys(username);
            TxtPassword().SendKeys(password);
            BtnSignUp().Click();
        }

    }
}