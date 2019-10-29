using System.Collections.Generic;

namespace Testing.Automation.Common.Helper
{
    public static class ListHelper
    {
        private static List<string> list = new List<string>();

        public static List<string> TheList
        {
            get { return list; }
        }

        public class MultipleList
        {
            public List<KeyValuePair<string, double>> List1 { get; set; }
            public List<KeyValuePair<string, double>> List2 { get; set; }
        }
    }
}
