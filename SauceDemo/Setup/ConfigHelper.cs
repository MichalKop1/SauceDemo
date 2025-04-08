using System.Text.Json;
using System.Text.Json.Serialization;

namespace SauceDemo;

public class ConfigHelper
{
	public string Url { get; set; }
	public string Browser { get; private set; }
	public string[] SupportedBrowsers { get; private set; }

	[JsonConstructor]
	private ConfigHelper(string url, string browser, string[] supportedBrowsers)
	{
		Url = url;
		Browser = browser;
		SupportedBrowsers = supportedBrowsers;
		ValidateBrowser();
	}

	public static ConfigHelper Load(string configPath)
	{
		var json = File.ReadAllText(configPath);
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		return JsonSerializer.Deserialize<ConfigHelper>(json, options)
			?? throw new InvalidOperationException("Invalid configuration");
	}

	private void ValidateBrowser()
	{
		if (string.IsNullOrEmpty(Url)) throw new ArgumentException("Url cannot be null or empty");

		if (string.IsNullOrEmpty(Browser))throw new ArgumentException("Browser name cannot be null or empty");

		if (!SupportedBrowsers.Contains(Browser, StringComparer.OrdinalIgnoreCase))
		{
			throw new ArgumentException($"Browser '{Browser}' is not supported. Valid options: {string.Join(", ", SupportedBrowsers)}");
		}
	}

	public Browsers GetBrowserType()
	{
		return Enum.Parse<Browsers>(Browser, ignoreCase: true);
	}
}

