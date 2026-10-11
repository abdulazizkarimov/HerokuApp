using OpenQA.Selenium;

namespace HerokuApp.Helpers
{
    public static class ScreenshotHelper
    {
        public static string TakeScreenshot(IWebDriver driver)
        {
            var dir = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"..\..\..\Screenshots"));
            Directory.CreateDirectory(dir);

            var fileName = $"{Sanitize(TestContext.CurrentContext.Test.Name)}_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            var path = Path.Combine(dir, fileName);

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);

            return path;
        }

        private static string Sanitize(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            return name.Replace(' ', '_').Replace(',', '_').Replace('(', '_').Replace(')', '_');
        }
    }
}
