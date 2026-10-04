using Allure.NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HerokuApp.Base;

[AllureNUnit]
public abstract class BaseTest
{
    protected IWebDriver driver;
    protected const string BaseUrl = "https://the-internet.herokuapp.com";

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Dispose();
    }
}
