using HerokuApp.Base;
using HerokuApp.Helpers;
using HerokuApp.Pages;

namespace HerokuApp.Tests;

[TestFixture]
public class SortableDataTableTests : BaseTest
{
    [Test]
    public void SortableDataTable_SortTheTable_InitialValuesArePresentAfterSorting()
    {
        // Arrange
        const string email = "jsmith@gmail.com";
        const string headerWebSite = "Web Site";
        const string headerFirstName = "First Name";

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
        Assert.That(table.IsColumnSorted(headerFirstName), Is.True, $"Table should be sorted by '{headerFirstName}'");
    }
}
