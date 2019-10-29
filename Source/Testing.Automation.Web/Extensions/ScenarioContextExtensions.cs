using TechTalk.SpecFlow;

namespace Testing.Automation.Web.Extensions
{
    public class ScenarioContextExtensions
    {
        public static void SaveValue<T>(string key, T value)
        {
            if (ScenarioContext.Current != null)
            {
                if (ScenarioContext.Current.ContainsKey(key))
                {
                    ScenarioContext.Current[key] = value;
                }
                else
                {
                    ScenarioContext.Current.Add(key, value);
                }
            }
        }

        public static T GetValue<T>(string key)
        {
            if (ScenarioContext.Current.ContainsKey(key))
            {
                return ScenarioContext.Current.Get<T>(key);
            }

            return default(T);
        }
    }
}
