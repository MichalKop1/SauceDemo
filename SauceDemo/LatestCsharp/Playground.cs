using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.LatestCsharp;

interface IPlayground
{
	public string Name { get; set; }
	public int GetDefaultInt()
	{
		return 42;
	}
}

public class Child : IPlayground
{
	public string Name { get; set; }
}

public class Test
{
	public Child child = new Child();

	public Test()
	{
		if (child is IPlayground playground)
		{
			_ = playground.GetDefaultInt();
		}
	}

}
