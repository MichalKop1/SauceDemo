using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SeleniumWebDriverFirstScriptTests;

public static class WebDriverFactory
{
    public static IWebDriver GetDriver(Browsers browser)
    {
        return browser switch
        {
            Browsers.Chrome => new ChromeDriver(),
            Browsers.Firefox => new FirefoxDriver(),
            Browsers.Edge => new EdgeDriver(),
            _ => throw new ArgumentException("Unsuported browser")
        };
    }

    public static IWebDriver GetDriver(Browsers browser, DriverOptions options)
    {
        return browser switch
        {
            Browsers.Chrome => new ChromeDriver(options as ChromeOptions),
            Browsers.Firefox => new FirefoxDriver(options as FirefoxOptions),
            Browsers.Edge => new EdgeDriver(options as EdgeOptions),
            _ => throw new ArgumentException("Unsuported browser")
        };
    }
}
