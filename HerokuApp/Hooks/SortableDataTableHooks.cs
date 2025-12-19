using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;

namespace HerokuApp.Hooks;

[Binding]
public class Hooks(ScenarioContext context)
{
    private readonly ScenarioContext _context = context;

    [BeforeScenario]
    public void StartBrowser()
    {
        var driver = new ChromeDriver();
        _context.Set<IWebDriver>(driver);
    }

    [AfterScenario]
    public void StopBrowser()
    {
        if (_context.TryGetValue(out IWebDriver driver))
        {
            driver.Dispose();
        }
    }
}
