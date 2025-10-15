using TechTalk.SpecFlow;

namespace SauceDemoTests;

[Binding]
internal class GherkinTest
{
	[BeforeScenario]
	public void BeforeScenario()
	{
		// This method will be called before each scenario
	}

	[Given(@"I want to check if it is Sunday")]
	public void GivenIAmOnTheSauceDemoLoginPage()
	{
		// Code to navigate to the Sauce Demo login page
	}

	[When(@"I am at my computer")]
	public void IAmAtComputer()
	{

	}

	[Then(@"Run the (.*)")]
	public void ThenRunThe(string script)
	{

	}

	[Then(@"return the (.*)")]
	public void ReturnThat(string value)
	{

	}

	[Then(@"It should not be (.*)")]
	public void ShouldNotBeInvalidDay(string day)
	{

	}
}
