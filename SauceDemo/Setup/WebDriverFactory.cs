using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SauceDemo;

public static class WebDriverFactory
{
    private static readonly ThreadLocal<IWebDriver> instance = new ThreadLocal<IWebDriver>();
    public static IWebDriver GetDriver(Browsers browser)
    {
		if (!instance.IsValueCreated)
		{
            instance.Value = browser switch
            {
                Browsers.Chrome => new ChromeDriver(),
                Browsers.Firefox => new FirefoxDriver(),
                Browsers.Edge => new EdgeDriver(),
                _ => throw new ArgumentException("Unsuported browser")
            };
        }

        return instance.Value;
    }

    public static IWebDriver GetDriver(Browsers browser, DriverOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

		if (!instance.IsValueCreated)
		{
            instance.Value = browser switch
            {
                Browsers.Chrome => new ChromeDriver((ChromeOptions)options),
                Browsers.Firefox => new FirefoxDriver((FirefoxOptions)options),
                Browsers.Edge => new EdgeDriver((EdgeOptions)options),
                _ => throw new ArgumentException("Unsuported browser")
            };
        }

        return instance.Value;
    }

    public static void QuitDriver()
    {
        if (instance.IsValueCreated)
        {
            instance.Value.Quit();
            instance.Value = null;
        }
    }
}
