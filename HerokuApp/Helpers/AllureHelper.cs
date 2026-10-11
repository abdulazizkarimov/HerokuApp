using Allure.Net.Commons;

namespace HerokuApp.Helpers
{
    public static class AllureHelper
    {
        public static void AddParameter(string name, string value) =>
            AllureLifecycle.Instance.UpdateStep(step =>
                step.parameters.Add(new Parameter { name = name, value = value }));
    }
}
