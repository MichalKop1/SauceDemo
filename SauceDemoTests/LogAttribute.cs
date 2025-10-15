using NUnit.Framework.Interfaces;

namespace SauceDemoTests
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class LogAttribute : Attribute, ITestAction
	{
		public ActionTargets Targets => ActionTargets.Test;

		public void AfterTest(ITest test)
		{
			
		}

		public void BeforeTest(ITest test)
		{
			
		}
	}
}
