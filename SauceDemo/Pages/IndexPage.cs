using log4net;
using log4net.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]


namespace SauceDemo.Pages;

public class IndexPage
{
    private static string Url { get; } = "https://www.saucedemo.com/";
    protected static readonly ILog log = LogManager.GetLogger(typeof(IndexPage));
    private readonly IWebDriver driver;

    public IndexPage(IWebDriver driver)
    {
        log.Info("Object set up");
        this.driver = driver ?? throw new ArgumentException(nameof(driver));
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

    public DashboardPage LogIn(out string message)
    {
        var wait5s = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
       

        var errorUserNamePasswordNotMatch = wait5s.Until(driver => driver.FindElements(By.ClassName("error-message-container")));
        
        var errorMessages = errorUserNamePasswordNotMatch;

        string errorMessage = string.Empty;

        if (errorMessages.Count() > 0)
        {
            message = errorMessages.FirstOrDefault().Text;
        }
        else
        {
            message = string.Empty;
        }

        return new DashboardPage(driver);
    }
}
