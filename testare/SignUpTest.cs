using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UnitTestProject1.PageObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class SignUpTest
    {
        private IWebDriver driver;
        private SignUpPage signUpPage;


        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            signUpPage = new SignUpPage(driver);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            driver.Navigate().GoToUrl("https://demoblaze.com/");
            signUpPage.menuItemControl.NavigateToSignUpPage();
        }

        [TestMethod]
        public void SignUp_CorrectEmail_CorrectPassword()
        {
            signUpPage.SignUpApplication("goodemail@uaic.ro", "helloworld");

            var expectedResult = "Sign up successful.";
            var actualResults = driver.SwitchTo().Alert().Text;

            Assert.AreEqual(expectedResult, actualResults);
        }

        [TestMethod]
        public void SignUp_IncorrectEmail_IncorrectPassword()
        {
            signUpPage.SignUpApplication("wrongemail@uaic.ro", "wrong");

            var expectedResult = "This user already exist.";
            var actualResults = driver.SwitchTo().Alert().Text;

            Assert.AreEqual(expectedResult, actualResults);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}