using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace SauceDemo.Pages;

public class IndexPage
{
    private static string Url { get; } = "https://www.saucedemo.com/";
    protected static readonly ILog log = LogManager.GetLogger(typeof(IndexPage));
    private readonly IWebDriver driver;

    public IndexPage()
    {
        string path = "appconfig.json";
        ConfigHelper configHelper = ConfigHelper.Load("appconfig.json");

		var browser = configHelper.GetBrowserType();
		var options = new WebDriverBuilder().Headless().Build(browser);

		this.driver = WebDriverFactory.GetDriver(browser, options) ?? throw new ArgumentException(nameof(driver));
    }

    public IndexPage Open()
    {
        driver.Url = Url;
        return this;
    }

    public IndexPage FillInNameAndPassword(string name, string password)
    {
        var userNameField = driver.FindElement(By.Id("user-name"));
        var userPasswordField = driver.FindElement(By.Id("password"));

        Actions inputCredentials = new Actions(driver);
        inputCredentials.Click(userNameField)
            .SendKeys(name)
            .Pause(TimeSpan.FromMilliseconds(300))
            .Click(userPasswordField)
            .SendKeys(password)
            .Perform();

        return this;
    }

    public IndexPage ClearNameAndPasswordFields()
    {
        var userNameField = driver.FindElement(By.Id("user-name"));
        var userPasswordField = driver.FindElement(By.Id("password"));

        userNameField.Click();
        userNameField.SendKeys(Keys.Control + "a");
        userNameField.SendKeys(Keys.Backspace);

        userPasswordField.Click();
        userPasswordField.SendKeys(Keys.Control + "a");
        userPasswordField.SendKeys(Keys.Backspace);

        return this;
    }

    public IndexPage ClearPasswordField()
    {
        var userPasswordField = driver.FindElement(By.Id("password"));
        userPasswordField.Click();
        userPasswordField.SendKeys(Keys.Control + "a");
        userPasswordField.SendKeys(Keys.Backspace);

        return this;
    }

    public string GetLoginErrorMessage()
    {
		var wait5s = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

		var loginButton = driver.FindElement(By.Id("login-button"));
		loginButton.Click();

		var errorUserNamePasswordNotMatch = wait5s.Until(driver => driver.FindElements(By.ClassName("error-message-container")));

        return errorUserNamePasswordNotMatch[0].Text;
	}

    public DashboardPage LogIn()
    {
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();

        return new DashboardPage(driver);
    }
}
