using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using UnitTestProject1.PageObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class AddPhoneToCartTest
    {
        private IWebDriver driver;
        private PhoneDetailPage phoneDetailPage;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            driver.Navigate().GoToUrl("https://demoblaze.com/");
            var loginPage = new LogInPage(driver);
            loginPage.menuItemControl.NavigateToLogInPage();
            loginPage.LogInApplication("test@test.eu", "testare");

            var homePage = new HomePage(driver);

            var  phonePage = homePage.NavigateToPhonePage();
            phoneDetailPage = phonePage.NavigateToPhoneDetailPage();
            
        }

        [TestMethod]
        public void Product_Added_Successfully_In_Cart()
        {
            phoneDetailPage.addPhoneToCart();
            var expectedResult = "Product added.";
            var actualResults = driver.SwitchTo().Alert().Text;

            Assert.AreEqual(expectedResult, actualResults);

            driver.SwitchTo().Alert().Accept();
            var homePage = new HomePage(driver);
            homePage.NavigateToCartPage();

            var expectedText = "Samsung galaxy s6";
            var actualText = driver.FindElement(By.XPath("//*[@id='tbodyid']/tr/td[2]")).Text;
            Assert.AreEqual(actualText, expectedText);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }

    }
}
