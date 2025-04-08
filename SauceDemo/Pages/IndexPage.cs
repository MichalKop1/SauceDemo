using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace SauceDemo.Pages;

public class IndexPage
{
    //private static string Url { get; } = "https://www.saucedemo.com/";
    protected readonly ILog log = LogManager.GetLogger(typeof(IndexPage));
    private readonly IWebDriver driver;
    private readonly ConfigHelper configHelper;

	public static By UserNameField => By.Id("user-name");
	public static By UserPasswordField => By.Id("password");
	public static By LoginButton => By.Id("login-button");
	public static By ErrorMessage => By.ClassName("error-message-container");

	public IndexPage()
    {
        string path = "appconfig.json";
        configHelper = ConfigHelper.Load(path);

		var browser = configHelper.GetBrowserType();
		var options = new WebDriverBuilder().Headless().Build(browser);

		this.driver = WebDriverFactory.GetDriver(browser, options) ?? throw new ArgumentException(nameof(driver));
    }

    public IndexPage Open()
    {
        driver.Url = configHelper.Url;
        return this;
    }

    public IndexPage FillInNameAndPassword(string name, string password)
    {
        var userNameField = driver.FindElement(UserNameField);
        var userPasswordField = driver.FindElement(UserPasswordField);

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
        var userNameField = driver.FindElement(UserNameField);
        var userPasswordField = driver.FindElement(UserPasswordField);

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
        var userPasswordField = driver.FindElement(UserPasswordField);
        userPasswordField.Click();
        userPasswordField.SendKeys(Keys.Control + "a");
        userPasswordField.SendKeys(Keys.Backspace);

        return this;
    }

    public string GetLoginErrorMessage()
    {
		var wait5s = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

		var loginButton = driver.FindElement(LoginButton);
		loginButton.Click();

		var errorUserNamePasswordNotMatch = wait5s.Until(driver => driver.FindElements(By.ClassName("error-message-container")));

        return errorUserNamePasswordNotMatch[0].Text;
	}

    public DashboardPage LogIn()
    {
        var loginButton = driver.FindElement(LoginButton);
        loginButton.Click();

        return new DashboardPage(driver);
    }
}
