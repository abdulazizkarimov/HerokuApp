using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using HerokuApp.Base;
using HerokuApp.Helpers;
using HerokuApp.Pages;
using HerokuApp.TestData;
using System.Text;

namespace HerokuApp.Tests;

[TestFixture]
[AllureOwner("Abdul Aziz Karimov")]
[AllureLink("Website", "https://the-internet.herokuapp.com/")]
[AllureSuite("Essential features")]
public class HerokuAppTests : BaseTest
{
    [Test]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureIssue("BUG-1")]
    [AllureTms("TC-1")]
    public void TC_1_Login_WithValidCredentials_ShouldLogUserInSuccessfully()
    {
        // Arrange
        var credentials = Users.ValidUser;
        var mainPage = NavigateToMainPage();
        var path = ScreenshotHelper.TakeScreenshot(driver);
        AllureApi.AddAttachment("MainPage.png", "image/png", path);
        AllureApi.AddAttachment("data.txt", "text/plain", Encoding.UTF8.GetBytes("This is the file content."));

        // Act
        var loginPage = AllureApi.Step("Open Form Authentication page",
            () => mainPage.ClickFormAuthenticationLink());

        var secureAreaPage = AllureApi.Step($"Log in as '{credentials.Login}'",
            () => loginPage.LoginAs(credentials.Login, credentials.Password));

        // Assert
        AllureApi.Step($"Asserting Security Area Page is loaded");
        Assert.That(secureAreaPage.IsLoaded(), Is.True, "User should be logged in successfully.");
    }

    [Test]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureIssue("BUG-2")]
    [AllureTms("TC-2")]
    public void TC_2_SortableDataTable_SortTheTable_InitialValuesArePresentAfterSorting()
    {
        // Arrange
        const string email = "jsmith@gmail.com";
        const string headerWebSite = "Web Site";
        const string headerFirstName = "First Name";
        const string headerLastName = "Last Name";
        List<string> lastNames = ["Smith", "Bach", "Doe", "Conway"];

        var mainPage = NavigateToMainPage();

        // Act
        var sortableDataTablesPage = mainPage.ClickSortableDataTablesLink();

        // Assert
        Assert.That(sortableDataTablesPage.IsDisplayed(), Is.True, "Sortable Data Tables page should be displayed.");

        // Act
        var table = new TableHelper(sortableDataTablesPage.GetSecondTable());
        var webSite = table.GetValueFromRow(email, headerWebSite);

        table.ClickHeader(headerFirstName);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(table.IsColumnSorted(headerFirstName), Is.True, $"Table should be sorted by '{headerFirstName}'.");
            Assert.That(table.IsValueInColumn(headerWebSite, webSite), Is.True, $"Value '{webSite}' should be present in the '{headerWebSite}' column.");
            foreach (var lastName in lastNames)
            {
                Assert.That(table.IsValueInColumn(headerLastName, lastName), Is.True, $"Value '{lastName}' should be present in the '{headerLastName}' column.");
            }
        });
    }

    [TestCase("johndoe", "qwerty")]
    [TestCase("johndoe@example.com", "qwerty")]
    public void TestAuthentication(string login, string password)
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(login, Is.Not.Null);
            Assert.That(password, Is.Not.Null);
        }
    }

    [AllureStep("Navigate to Main Page")]
    private MainPage NavigateToMainPage()
    {
        AllureHelper.AddParameter("Url", BaseUrl);
        driver.Navigate().GoToUrl(BaseUrl);
        return new MainPage(driver);
    }
}
