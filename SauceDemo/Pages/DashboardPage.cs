using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Pages;

public class DashboardPage
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(IndexPage));
    private readonly IWebDriver driver;

    public DashboardPage(IWebDriver driver)
    {
        log.Info("Setting up dashboard page");
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
