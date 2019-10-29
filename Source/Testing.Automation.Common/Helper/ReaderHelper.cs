using System.Xml;

namespace Testing.Automation.Common.Helper
{
    /// <summary>
    /// Pomocna trieda pre pracu z testovacími súbormi (xml, conf,...)
    /// </summary>
    public class ReaderHelper
    {
        /// <summary>
        /// Modifikácia xml dokumentu v rámci jedneho nodu, potrebný namespace
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nameSpace"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void SelectSingleNodeWithNamespace(XmlDocument document, string nameSpace, string element, string attribute, string value)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("ns", nameSpace);
            var node = document.SelectSingleNode("//ns:" + element, nsmgr).Attributes[attribute].Value = value;
        }


        /// <summary>
        ///  Modofikácia xml dokumentu v ramci viacerych nodov, potrebný namespace
        /// </summary>
        /// <param name="document"></param>
        /// <param name="nameSpace"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void SelectNodesWithNamespace(XmlDocument document, string nameSpace, string element, string attribute, string value)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("ns", nameSpace);
            var node = document.SelectNodes("//ns:" + element, nsmgr);

            foreach (XmlNode elements in node)
            {
                elements.Attributes[attribute].InnerText = value;
            }
        }


        /// <summary>
        /// Modifikácia xml dokumentu v rámci jedneho nodu, bez namespace
        /// </summary>
        /// <param name="document"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void SelectSingleNode(XmlDocument document, string element, string attribute, object value)
        {
            document.SelectSingleNode("//" + element).Attributes[attribute].Value = value.ToString();
        }


        /// <summary>
        ///  Vráti hodnotu atributu v xml dokumente
        /// </summary>
        /// <param name="document"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetSingleNode(XmlDocument document, string element, string attribute)
        {
            return document.SelectSingleNode("//" + element).Attributes[attribute].InnerText;
        }

        /// <summary>
        ///  Vráti hodnotu atributu v xml dokumente, potrebný namespace
        /// </summary>
        /// <param name="document"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetSingleNodeWithNameSpace(XmlDocument document, string nameSpace,  string element, string attribute)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("ns", nameSpace);

            return document.SelectSingleNode("//ns:" + element, nsmgr).Attributes[attribute].InnerText;
        }

        /// <summary>
        ///  Vráti hodnotu počet elementov v xml dokumente, potrebný namespace
        /// </summary>
        /// <param name="document"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static int CountSingleNodeWithNameSpace(XmlDocument document, string nameSpace, string element)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("ns", nameSpace);
            var node = document.SelectNodes(element, nsmgr);

            return node.Count;
        }
    }
}
