using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SeleniumWebDriverFirstScriptTests;

public static class WebDriverFactory
{
    private static IWebDriver? instance;
    public static IWebDriver GetDriver(Browsers browser)
    {
        if (instance is null)
        {
            return browser switch
            {
                Browsers.Chrome => new ChromeDriver(),
                Browsers.Firefox => new FirefoxDriver(),
                Browsers.Edge => new EdgeDriver(),
                _ => throw new ArgumentException("Unsuported browser")
            };
        }

        return instance;
    }

    public static IWebDriver GetDriver(Browsers browser, DriverOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (instance is null)
        {
            return browser switch
            {
                Browsers.Chrome => new ChromeDriver((ChromeOptions)options),
                Browsers.Firefox => new FirefoxDriver((FirefoxOptions)options),
                Browsers.Edge => new EdgeDriver((EdgeOptions)options),
                _ => throw new ArgumentException("Unsuported browser")
            };
        }

        return instance;
    }

    public static void QuitDriver()
    {
        if (instance is not null)
        {
            instance.Quit();
            instance = null;
        }
    }
}
