using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HerokuApp.Pages;

public class SecureAreaPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _waiter;
    private IWebElement MessageLabel => _driver.FindElement(By.Id("flash"));

    public SecureAreaPage(IWebDriver driver)
    {
        _driver = driver;
        _waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool IsLoaded()
    {
        _waiter.Until(d => d.Url.Contains("/secure"));
        _waiter.Until(d => MessageLabel.Displayed);

        return _driver.Url.Contains("/secure") &&
            MessageLabel.Displayed &&
            GetMessage().Contains("You logged into a secure area!");
    }

    public string GetMessage()
    {
        _waiter.Until(d => MessageLabel.Displayed);
        return MessageLabel.Text;
    }
}
