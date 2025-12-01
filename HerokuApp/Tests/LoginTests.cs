using HerokuApp.Base;
using HerokuApp.Pages;

namespace HerokuApp.Tests;

[TestFixture]
public class LoginTests : BaseTest
{
    [Test]
    public void Login_WithValidCredentials_ShouldLogUserInSuccessfully()
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
}
