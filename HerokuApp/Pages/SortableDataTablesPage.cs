using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HerokuApp.Pages;

public class SortableDataTablesPage : IPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _waiter;
    private IWebElement FirstTable => _driver.FindElement(By.Id("table1"));
    private IWebElement SecondTable => _driver.FindElement(By.Id("table2"));

    public SortableDataTablesPage(IWebDriver driver)
    {
        _driver = driver;
        _waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool IsDisplayed()
    {
        return _waiter.Until(d => FirstTable.Displayed);
    }

    public IWebElement GetFirstTable()
    {
        return FirstTable;
    }

    public IWebElement GetSecondTable()
    {
        return SecondTable;
    }
}
