using OpenQA.Selenium;

namespace UnitTestProject1.PageObjects.Controllers
{
    public class MenuItemControl
    {
        public IWebDriver driver;

        public MenuItemControl(IWebDriver browser)
        {
            driver = browser;
        }

        private By home = By.XPath("//a[@href='index.html']");
        private IWebElement BtnHome => driver.FindElement(home);

        private By contact = By.CssSelector("a[data-target='#exampleModal']");
        private IWebElement BtnContact => driver.FindElement(contact);

        private By aboutUs = By.CssSelector("a[data-target='#videoModal']");
        private IWebElement BtnAboutUs => driver.FindElement(aboutUs);

        private By cart = By.Id("cartur");
        private IWebElement BtnCart => driver.FindElement(cart);
    }

    public class LoggedOutMenuItemControl: MenuItemControl
    {
        
        private By logIn = By.Id("login2");
        private IWebElement BtnLogIn => driver.FindElement(logIn);
        
        private By signUp = By.Id("signin2");
        private IWebElement BtnSignUp => driver.FindElement(signUp);

        public LoggedOutMenuItemControl(IWebDriver browser) : base(browser)
        {
        }

        public LogInPage NavigateToLogInPage()
        {
            BtnLogIn.Click();
            return new LogInPage(driver);
        }

        public SignUpPage NavigateToSignUpPage()
        {
            BtnSignUp.Click();
            return new SignUpPage(driver);
        }
    }

    public class LoggedInMenuItemControl: MenuItemControl
    {
        private By logOut = By.Id("logout2");
        private IWebElement BtnLogOut => driver.FindElement(logOut);

        private By welcomeUserEmail = By.Id("nameofuser");
        private IWebElement LblWelcomeUserEmail=>driver.FindElement(welcomeUserEmail);

        public LoggedInMenuItemControl(IWebDriver browser) : base(browser)
        {
        }

        public string welcomeUserEmailText => LblWelcomeUserEmail.Text;
    }
}