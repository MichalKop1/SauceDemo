using log4net;
using log4net.Config;
using OpenQA.Selenium;
using SauceDemo.Pages;
using SeleniumWebDriverFirstScriptTests;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace SauceDemoTests;

[TestFixture(Browsers.Chrome)]
[TestFixture(Browsers.Firefox)]
[TestFixture(Browsers.Edge)]
public class PageTests
{
    private readonly Browsers browser;
    private IWebDriver driver;
    private IndexPage indexPage;

    protected static readonly ILog log = LogManager.GetLogger(typeof(PageTests));

    public PageTests(Browsers browser)
    {
        this.browser = browser;
    }

    [SetUp]
    public void Setup()
    {
        log.Info("Setting up the objects...");
        var options = new WebDriverBuilder().Headless().Build(browser);
        var baseDriver = WebDriverFactory.GetDriver(browser, options);

        driver = new LoggingWebDriver(baseDriver);
        
        indexPage = new IndexPage(driver);
    }

    [Test]
    public void LoginWithEmptyCredentials_PageShowsError()
    {
        // Arrange
        string name = "ggg";
        string password = "jjjjjj";
        string actual;

        // Act
        var page = indexPage.Open().FillInNameAndPassword(name,password).ClearNameAndPasswordFields().LogIn(out actual);

        // Assert
        string expectedError = "Epic sadface: Username is required";
        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [Test]
    public void LoginOnlyWithUsername_PageShowsError()
    {
        // Arrange
        string name = "gfrewgerg";
        string password = "secret_sauce";
        string actual;

        // Act
        var page = indexPage.Open().FillInNameAndPassword(name, password).ClearPasswordField().LogIn(out actual);

        // Assert
        string expectedError = "Epic sadface: Password is required";
        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [Test]
    public void LoginWithCorrectCredentials_LoginSucceeded()
    {
        // Arrange
        string name = "standard_user";
        string password = "secret_sauce";
        string message;

        //Act
        DashboardPage dashboardPage = indexPage.Open().FillInNameAndPassword(name, password).LogIn(out message);

        // Assert
        string label = "Swag Labs";
        string actual = dashboardPage.GetLabelText();
        Assert.That(actual, Is.EqualTo(label));
    }

    [TearDown]
    public void TearDown()
    {
        WebDriverFactory.QuitDriver();
        driver.Dispose();
    }
}