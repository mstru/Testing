using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Testing.Automation.API.Builders.Helper
{
    public class JSONHelper
    {
        private static JToken containerToken;

        /// <summary>
        /// Parsuje retazec podla zadaneho elementu a vrati hodnotu
        /// </summary>
        /// <param name="json">api odpoved</param>
        /// <param name="element">vrati hodotu podla zadaneho elementu</param>
        /// <returns></returns>
        public static string ResponseParserFind(string json, string element)
        {
            containerToken = JToken.Parse(json);
            return FindToken(containerToken, element, json);
        }

        /// <summary>
        /// Vrati pocet children tokenov
        /// </summary>
        /// <param name="json">api odpoved</param>
        /// <returns></returns>
        public static int ResponseParserCount(string json)
        {
            containerToken = JToken.Parse(json);
            return CountToken(containerToken, json);
        }

        private static string FindToken(JToken containerToken, string element, string json)
        {
            json = json.Trim();
            if ((json.StartsWith("{") && json.EndsWith("}")) || //For object
                (json.StartsWith("[") && json.EndsWith("]")))   //For array
            {
                if (containerToken.Type == JTokenType.Object)
                {
                    var o = JObject.Parse(json);

                    foreach (JToken token in o.SelectTokens(element))
                    {
                        return token.ToString();
                    }
                }

                if (containerToken.Type == JTokenType.Array)
                {
                    var o = JArray.Parse(json);

                    foreach (JToken token in o.Select(m => (string)m.SelectToken(element)).ToList())
                    {
                        return token.ToString();
                    }
                }
            }
            else
            {
                return null;
            }

            return null;
        }

        private static int CountToken(JToken containerToken, string json)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                var o = JObject.Parse(json);

                if (json == "[]")
                    return 0;
                else
                    return (int)o.Children().Count();
            }

            if (containerToken.Type == JTokenType.Array)
            {
                var o = JArray.Parse(json);
                if (json == "[]")
                    return 0;
                else
                    return (int)o.Children().Count();
            }
            return 0;
        }


        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }

        public static string XPathSelectElement(string json, string element)
        {
            var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json),
                new System.Xml.XmlDictionaryReaderQuotas());
            var root = XElement.Load(jsonReader);

            return root.XPathSelectElement(element).Value;
        }


        public static List<string> ResponseConvertToList(string jsonResponse)
        {
            List<string> jsonList = new List<string>();
            string[] arr = new[] { jsonResponse };

            foreach (string arrValue in arr)
            {
                jsonList.Add(arrValue);
            }

            return jsonList;
        }
    }
}
