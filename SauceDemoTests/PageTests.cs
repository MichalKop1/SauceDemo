using log4net;
using log4net.Config;
using SauceDemo.Pages;
using SauceDemo;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace SauceDemoTests;

//[Parallelizable(ParallelScope.All)]
//[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[TestFixture]
public class PageTests
{
    private IndexPage indexPage = new IndexPage();

    protected static readonly ILog log = LogManager.GetLogger(typeof(PageTests));

    [SetUp]
    public void Setup()
    {
        indexPage = new IndexPage();
    }

    [Log]
    [TestCase("ggg", "jjjjjj")]
    public void LoginWithEmptyCredentials_PageShowsError(string name, string password)
    {
		
        // Arrange

        // Act
        string actual = indexPage.Open().FillInNameAndPassword(name, password).ClearNameAndPasswordFields().GetLoginErrorMessage();

        // Assert
        string expectedError = "Epic sadface: Username is required";
        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [TestCase("gfrewgerg", "secret_sauce")]
    public void LoginOnlyWithUsername_PageShowsError(string name, string password)
    {
		int[] example = [0, 1, 2, 2, 3, 0, 4, 2];
		int val = 2;

		Doooo(example, val);
		// Arrange

		// Act
		string actual = indexPage.Open().FillInNameAndPassword(name, password).ClearPasswordField().GetLoginErrorMessage();

        // Assert
        string expectedError = "Epic sadface: Password is required";
        Assert.That(actual, Is.EqualTo(expectedError));
    }

    [TestCase("standard_user", "secret_sauce")]
    public void LoginWithCorrectCredentials_LoginSucceeded(string name, string password)
    {
        // Arrange

        //Act
        DashboardPage dashboardPage = indexPage.Open().FillInNameAndPassword(name, password).LogIn();

        // Assert
        string label = "Swag Labs";
        string actual = dashboardPage.GetLabelText();
        Assert.That(actual, Is.EqualTo(label));
    }

    private void Doooo(int[] nums, int val)
    {
		int count = 0;
		for (int i = 0; i < nums.Length; i++)
		{
			for (int j = i + 1; j < nums.Length; j++)
			{
				if (nums[i] == val && nums[j] != val)
				{
					nums[i] = nums[j];
					nums[j] = val;
					count++;
				}
				else if (nums[i] != val)
				{
					count++;
					break;
				}
			}
		}
	}

    [TearDown]
    public void TearDown()
    {
        WebDriverFactory.QuitDriver();
    }
}