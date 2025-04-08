using log4net;
using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class DashboardPage
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(IndexPage));
    private readonly IWebDriver driver;

    public DashboardPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentException(nameof(driver));
    }

    public DashboardPage Open()
    {
        return this;
    }

    public string GetLabelText()
    {
        var labelText = driver.FindElement(By.ClassName("app_logo"));
        string text = labelText.Text;

        return text;
    }
}
