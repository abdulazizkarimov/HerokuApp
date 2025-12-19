using OpenQA.Selenium;

namespace HerokuApp.Pages;

public class LoginPage(IWebDriver driver)
{
    private IWebElement UsernameField => driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => driver.FindElement(By.CssSelector(".radius"));

    public void EnterUsername(string username)
    {
        UsernameField.Clear();
        UsernameField.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        PasswordField.Clear();
        PasswordField.SendKeys(password);
    }

    public void ClickLogin()
    {
        LoginButton.Submit();
    }

    public SecureAreaPage LoginAs(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLogin();

        return new SecureAreaPage(driver);
    }
}
