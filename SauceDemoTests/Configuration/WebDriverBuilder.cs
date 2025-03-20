using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SeleniumWebDriverFirstScriptTests;

public class WebDriverBuilder
{
    private bool headless;
    private bool incognito;
    private readonly IWebDriver driver;

    public WebDriverBuilder Headless()
    {
        headless = true;
        return this;
    }

    public WebDriverBuilder Incognito()
    {
        incognito = true;
        return this;
    }

    public DriverOptions Build(Browsers browser)
    {
        DriverOptions options;
        switch (browser)
        {
            case Browsers.Chrome:
                var chromeOptions = new ChromeOptions();
                if (headless) chromeOptions.AddArgument("--headless");
                if (incognito) chromeOptions.AddArgument("--incognito");
                options = chromeOptions;
                break;

            case Browsers.Firefox:
                var firefoxOptions = new FirefoxOptions();
                if (headless) firefoxOptions.AddArgument("--headless");
                if (incognito) firefoxOptions.AddArgument("--incognito");
                options = firefoxOptions;
                break;

            case Browsers.Edge:
                var edgeOptions = new EdgeOptions();
                if (headless) edgeOptions.AddArgument("--headless");
                if (incognito) edgeOptions.AddArgument("--incognito");
                options = edgeOptions;
                break;

            default:
                throw new ArgumentException("Unsuported browser");
        }

        return options;
    }
}
