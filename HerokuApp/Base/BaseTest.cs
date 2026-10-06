using Allure.NUnit;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HerokuApp.Base;

[AllureNUnit]
public abstract class BaseTest
{
    protected IWebDriver driver;
    protected const string BaseUrl = "https://the-internet.herokuapp.com";

    [SetUp]
    [AllureBefore("Initialize the driver")]
    public void SetUp()
    {
        driver = new ChromeDriver();
    }

    [TearDown]
    [AllureAfter("Quit the driver")]
    public void TearDown()
    {
        driver.Dispose();
    }
}
