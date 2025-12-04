using System;
using HerokuApp.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace HerokuApp.StepDefinitions
{
    [Binding]
    public class SortTableByColumnHeaderOnTheSortableDataTablesPageStepDefinitions
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _context;
        private MainPage _mainPage;
        public SortTableByColumnHeaderOnTheSortableDataTablesPageStepDefinitions(ScenarioContext context)
        {
            _context = context;
            _driver = context.Get<IWebDriver>();
        }
        
        [Given("I went on {string}")]
        public void GivenIWentOn(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _mainPage = new MainPage(_driver);
        }

        [Given("I followed the {string} link")]
        public void GivenIFollowedTheLink(string p0)
        {
            // throw new PendingStepException();
        }

        [Given("I captured the {string} value for the row with email {string}")]
        public void GivenICapturedTheValueForTheRowWithEmail(string p0, string p1)
        {
            // throw new PendingStepException();
        }

        [When("I click the {string} column header")]
        public void WhenIClickTheColumnHeader(string p0)
        {
            // throw new PendingStepException();
        }

        [Then("the rows are sorted by the {string} column")]
        public void ThenTheRowsAreSortedByTheColumn(string p0)
        {
            // throw new PendingStepException();
        }

        [Then("the captured value is still present in the {string} column")]
        public void ThenTheCapturedValueIsStillPresentInTheColumn(string p0)
        {
            // throw new PendingStepException();
        }

        [Then("the {string} column contains the following values:")]
        public void ThenTheColumnContainsTheFollowingValues(string p0, DataTable dataTable)
        {
            // throw new PendingStepException();
        }
    }
}
