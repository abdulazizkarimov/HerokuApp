using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using HerokuApp.Base;
using HerokuApp.Helpers;
using HerokuApp.Pages;

namespace HerokuApp.Tests;

[TestFixture]
public class HerokuAppTests : BaseTest
{
    [Test]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Abdul Aziz Karimov")]
    [AllureLink("Website", "https://the-internet.herokuapp.com/")]
    [AllureIssue("BUG-1")]
    [AllureTms("TC-1")]
    public void TC_1_Login_WithValidCredentials_ShouldLogUserInSuccessfully()
    {
        // Arrange
        driver.Navigate().GoToUrl(BaseUrl);
        var mainPage = new MainPage(driver);

        // Act
        var loginPage = mainPage.ClickFormAuthenticationLink();
        var secureAreaPage = loginPage.LoginAs("tomsmith", "SuperSecretPassword!");

        // Assert
        Assert.That(secureAreaPage.IsLoaded(), Is.True, "User should be logged in successfully.");
    }

    [Test]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Abdul Aziz Karimov")]
    [AllureLink("Website", "https://the-internet.herokuapp.com/")]
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

        driver.Navigate().GoToUrl(BaseUrl);
        var mainPage = new MainPage(driver);

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
}
