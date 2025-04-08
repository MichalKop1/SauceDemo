using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace SeleniumWebDriverFirstScript;

public class LoggingWebDriver : IWebDriver, IActionExecutor
{
    private readonly IWebDriver driver;
    private readonly IActionExecutor actionExecutor;
    private static readonly ILog log = LogManager.GetLogger(typeof(LoggingWebDriver));

    public LoggingWebDriver(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        this.actionExecutor = driver as IActionExecutor
            ?? throw new ArgumentException("The IWebDriver object must implement IActionExecutor.", nameof(driver));
    }

    public string Url
    {
        get => driver.Url;
        set
        {
            log.Info($"Navigating to URL: {value}");
            driver.Url = value;
        }
    }

    public string Title
    {
        get
        {
            string title = driver.Title;
            log.Info($"Page title: {title}");
            return title;
        }
    }

    public string PageSource
    {
        get
        {
            log.Info("Fetching PageSource...");
            return driver.PageSource;
        }
    }

    public string CurrentWindowHandle
    {
        get
        {
            string handle = driver.CurrentWindowHandle;
            log.Info($"Current window handle: {handle}");
            return handle;
        }
    }

    public ReadOnlyCollection<string> WindowHandles
    {
        get
        {
            var handles = driver.WindowHandles;
            log.Info($"Window handles count: {handles.Count}");
            return handles;
        }
    }

    public void Close()
    {
        log.Info("Closing browser window...");
        driver.Close();
    }

    public void Quit()
    {
        log.Info("Quitting WebDriver...");
        driver.Quit();
    }

    public IOptions Manage()
    {
        log.Info("Accessing browser options...");
        return driver.Manage();
    }

    public INavigation Navigate()
    {
        log.Info("Accessing navigation commands...");
        return driver.Navigate();
    }

    public ITargetLocator SwitchTo()
    {
        log.Info("Switching context...");
        return driver.SwitchTo();
    }

    public IWebElement FindElement(By by)
    {
        log.Info($"Finding element: {by}");
        return driver.FindElement(by);
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        log.Info($"Finding elements: {by}");
        return driver.FindElements(by);
    }

    public void Dispose()
    {
        log.Info("Disposing WebDriver...");
        driver.Dispose();
    }

    public void PerformActions(IList<ActionSequence> actionSequenceList)
    {
        log.Info("Performing actions...");
        actionExecutor.PerformActions(actionSequenceList);
    }

    public void ResetInputState()
    {
        log.Info("Resetting input state...");
        actionExecutor.ResetInputState();
    }

    public bool IsActionExecutor => actionExecutor != null;
}
