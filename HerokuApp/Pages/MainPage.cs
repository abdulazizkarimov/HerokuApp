using OpenQA.Selenium;

namespace HerokuApp.Pages;

public class MainPage(IWebDriver driver)
{
    private IWebElement FormAuthenticationLink => driver.FindElement(By.CssSelector("a[href='/login']"));
    private IWebElement SortableDataTablesLink => driver.FindElement(By.CssSelector("a[href='/tables']"));

    public LoginPage ClickFormAuthenticationLink()
    {
        FormAuthenticationLink.Click();
        return new LoginPage(driver);
    }

    public SortableDataTablesPage ClickSortableDataTablesLink()
    {
        SortableDataTablesLink.Click();
        return new SortableDataTablesPage(driver);
    }
}
