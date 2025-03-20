using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Pages;
using SeleniumWebDriverFirstScriptTests;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]


namespace SauceDemoTests;

public class PageTests
{
    private IWebDriver driver;
    private IndexPage indexPage;

    protected static readonly ILog log = LogManager.GetLogger(typeof(PageTests));

    [SetUp]
    public void Setup()
    {
        log.Info("Setting up the objects...");
        var options = new WebDriverBuilder().Build(Browsers.Chrome);
        var baseDriver = WebDriverFactory.GetDriver(Browsers.Chrome, options);

        driver = new LoggingWebDriver(baseDriver);
        indexPage = new IndexPage(driver);
    }

    [Test]
    public void LoginWithEmptyCredentials_PageShowsError()
    {
        string name = "ggg";
        string password = "jjjjjj";
        string actual;

        var page = indexPage.Open().FillInNameAndPassword(name,password).ClearNameAndPasswordFields().LogIn(out actual);

        string expectedError = "Epic sadface: Username is required";

        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [Test]
    public void LoginOnlyWithUsername_PageShowsError()
    {
        string name = "gfrewgerg";
        string password = "secret_sauce";
        string actual;

        var page = indexPage.Open().FillInNameAndPassword(name, password).ClearPasswordField().LogIn(out actual);

        string expectedError = "Epic sadface: Password is required";

        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [Test]
    public void LoginWithCorrectCredentials_LoginSucceeded()
    {
        string name = "standard_user";
        string password = "secret_sauce";
        string message;

        DashboardPage dashboardPage = indexPage.Open().FillInNameAndPassword(name, password).LogIn(out message);

        string label = "Swag Labs";
        string actual = dashboardPage.GetLabelText();

        Assert.That(actual, Is.EqualTo(label));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}