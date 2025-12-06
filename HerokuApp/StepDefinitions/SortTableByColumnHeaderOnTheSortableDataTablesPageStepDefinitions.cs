using HerokuApp.Helpers;
using HerokuApp.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace HerokuApp.StepDefinitions;

[Binding]
public class SortTableByColumnHeaderOnTheSortableDataTablesPageStepDefinitions
{
    private readonly IWebDriver _driver;
    private readonly ScenarioContext _context;
    private MainPage? _mainPage;
    private SortableDataTablesPage? _sortableDataTablesPage;
    private TableHelper? _table;

    public SortTableByColumnHeaderOnTheSortableDataTablesPageStepDefinitions(ScenarioContext context)
    {
        _context = context;
        _driver = context.Get<IWebDriver>();
    }

    [Given("I went on {string}")]
    public void GivenIWentOn(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }

    [When("I follow the {string} link on the Main Page")]
    public void GivenIFollowedTheLink(string linkText)
    {
        _mainPage = new MainPage(_driver);
        _mainPage.ClickLinkWithText(linkText);
    }

    [Then("the {string} page is opened")]
    public void ThenThePageIsOpened(string pageName)
    {
        IPage? page;

        switch (pageName)
        {
            case "Sortable Data Tables":
                _sortableDataTablesPage = new SortableDataTablesPage(_driver);
                page = _sortableDataTablesPage;
                break;

            default:
                page = null;
                break;
        }

        Assert.That(page?.IsDisplayed(), Is.True, $"{pageName} page should be displayed.");
    }

    [When("I capture the {string} value for the row with email {string}")]
    public void WhenICapturedTheValueForTheRowWithEmail(string column, string searchText)
    {
        _table = new TableHelper(_sortableDataTablesPage.GetSecondTable());
        _context["webSite"] = _table.GetValueFromRow(searchText, column);
    }

    [When("I click the {string} column header")]
    public void WhenIClickTheColumnHeader(string header)
    {
        _table.ClickHeader(header);
    }

    [Then("the rows are sorted by the {string} column")]
    public void ThenTheRowsAreSortedByTheColumn(string column)
    {
        Assert.That(_table.IsColumnSorted(column), Is.True, $"Table should be sorted by '{column}'.");
    }

    [Then("the captured value is still present in the {string} column")]
    public void ThenTheCapturedValueIsStillPresentInTheColumn(string column)
    {
        var capturedValue = (string)_context["webSite"];

        Assert.That(_table.IsValueInColumn(column, capturedValue), Is.True, $"Value '{capturedValue}' should be present in the '{column}' column.");
    }

    [Then("the {string} column contains the following values:")]
    public void ThenTheColumnContainsTheFollowingValues(string column, DataTable dataTable)
    {
        var values = dataTable.Rows.Select(r => r[column]).ToList();

        foreach (var value in values)
        {
            Assert.That(_table.IsValueInColumn(column, value), Is.True, $"Value '{value}' should be present in the '{column}' column.");
        }
    }
}
