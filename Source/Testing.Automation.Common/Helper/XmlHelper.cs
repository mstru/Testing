﻿using System;
using System.Linq;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;

namespace Testing.Automation.Common.Helper
{

    /// <summary>
    /// Pomocná trieda pre čítanie a písanie XML z objektov.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Like Add, but chainable.
        /// </summary>
        /// <param name="el">The parent element.</param>
        /// <param name="children">The elements to add.</param>
        /// <returns>Itself</returns>
        public static XElement AddEl(this XElement el, params XElement[] children)
        {
            el.Add(children.Cast<object>());
            return el;
        }

        /// <summary>
        /// Gets the string value of an attribute, and null if the attribute doesn't exist.
        /// </summary>
        /// <param name="el">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The string value of the attribute if it exists, null otherwise.</returns>
        public static string Attr(this XElement el, string name)
        {
            var attr = el.Attribute(name);
            return attr == null ? null : attr.Value;
        }

        /// <summary>
        /// Gets a typed value from an attribute.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="el">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute value</returns>
        public static T Attr<T>(this XElement el, string name)
        {

            var attr = el.Attribute(name);
            var type = typeof(T);

            if (attr == null ||
                ((!type.IsValueType || Nullable.GetUnderlyingType(type) != null) && type != typeof(string) &&
                 "null".Equals(attr.Value, StringComparison.Ordinal)))
            {

                return default(T);
            }

            if (type == typeof(string))
            {
                return (T)(object)attr.Value;
            }
            if (type == typeof(int))
            {
                return (T)(object)(int)attr; // Pretty, eh? OK, if you're so smart, find a better way. I'm waiting. Seriously, I'd love to not do this.
            }
            if (type == typeof(bool))
            {
                return (T)(object)(bool)attr;
            }
            if (type == typeof(System.DateTime))
            {
                return (T)(object)(System.DateTime)attr;
            }
            if (type == typeof(double))
            {
                return (T)(object)(double)attr;
            }
            if (type == typeof(float))
            {
                return (T)(object)(float)attr;
            }
            if (type == typeof(decimal))
            {
                return (T)(object)(decimal)attr;
            }
            if (type == typeof(int?))
            {
                return (T)(object)(int?)attr;
            }
            if (type == typeof(bool?))
            {
                return (T)(object)(bool?)attr;
            }
            if (type == typeof(System.DateTime?))
            {
                return (T)(object)(System.DateTime?)attr;
            }
            if (type == typeof(double?))
            {
                return (T)(object)(double?)attr;
            }
            if (type == typeof(float?))
            {
                return (T)(object)(float?)attr;
            }
            if (type == typeof(decimal?))
            {
                return (T)(object)(decimal?)attr;
            }
            throw new InvalidCastException(String.Format("Couldn't find how to deserialize to type {0}.", type.Name));
        }

        /// <summary>
        /// Sets an attribute value. This is chainable.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="el">The element.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Itself</returns>
        public static XElement Attr<T>(this XElement el, string name, T value)
        {
            el.SetAttributeValue(name, value);
            return el;
        }

        /// <summary>
        /// Returns the text contents of a child element.
        /// </summary>
        /// <param name="el">The parent element.</param>
        /// <param name="name">The name of the child element.</param>
        /// <returns>The text for the child element, and null if it doesn't exist.</returns>
        public static string El(this XDocument el, string name)
        {
            var childElement = el.Element(name);
            return childElement == null ? null : childElement.Value;
        }

        /// <summary>
        /// Serialize a serializable object to XML string.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="xmlObject">Type of object</param>
        /// <param name="useNamespaces">Use of XML namespaces</param>
        /// <returns>XML string</returns>
        public static string SerializeToXmlString<T>(T xmlObject, bool useNamespaces = true)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;

            if (useNamespaces)
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                xmlSerializer.Serialize(xmlTextWriter, xmlObject, xmlSerializerNamespaces);
            }
            else
                xmlSerializer.Serialize(xmlTextWriter, xmlObject);

            string output = Encoding.UTF8.GetString(memoryStream.ToArray());
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (output.StartsWith(_byteOrderMarkUtf8))
            {
                output = output.Remove(0, _byteOrderMarkUtf8.Length);
            }

            return output;
        }

        /// <summary>
        /// Serialize a serializable object to XML string and create a XML file.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="xmlObject">Type of object</param>
        /// <param name="filename">XML filename with .XML extension</param>
        /// <param name="useNamespaces">Use of XML namespaces</param>
        public static void SerializeToXmlFile<T>(T xmlObject, string filename, bool useNamespaces = true)
        {
            try
            {
                File.WriteAllText(filename, SerializeToXmlString<T>(xmlObject, useNamespaces));
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Returns the text contents of a child element.
        /// </summary>
        /// <param name="el">The parent element.</param>
        /// <param name="name">The name of the child element.</param>
        /// <returns>The text for the child element, and null if it doesn't exist.</returns>
        public static string El(this XDocument el, string elName, string atrName)
        {
            var childElement = el.Element(elName).Attribute(atrName);
            return childElement == null ? null : childElement.Value;
        }

        public static string GetElementValue(this XDocument doc, string elName, string atrName)
        {
            var result = doc.XPathSelectElement(elName).Attribute(atrName); 
            return result == null ? null : result.Value;
        }


        /// <summary>
        /// Creates and sets the value of a child element. This is chainable.s
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="el">The parent element.</param>
        /// <param name="name">The name of the child element.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Itself</returns>
        public static XElement El<T>(this XElement el, string name, T value)
        {
            el.SetElementValue(name, value);
            return el;
        }

        /// <summary>
        /// Sets a property value from an attribute of the same name.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target object.</typeparam>
        /// <typeparam name="TProperty">The type of the target property</typeparam>
        /// <param name="el">The element.</param>
        /// <param name="target">The target object.</param>
        /// <param name="targetExpression">The property expression.</param>
        /// <returns>Itself</returns>
        public static XElement FromAttr<TTarget, TProperty>(this XElement el, TTarget target,
            Expression<Func<TTarget, TProperty>> targetExpression)
        {

            var propertyInfo = GetPropertyInfo(targetExpression);
            var name = propertyInfo.Name;
            var attr = el.Attribute(name);

            if (attr == null) return el;
            propertyInfo.SetValue(target, el.Attr<TProperty>(name), null);
            return el;
        }

        /// <summary>
        /// Sets an attribute with the value of a property of the same name.
        /// </summary>
        /// <typeparam name="TTarget">The type of the object.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="el">The element.</param>
        /// <param name="target">The object.</param>
        /// <param name="targetExpression">The property expression.</param>
        /// <returns>Itself</returns>
        public static XElement ToAttr<TTarget, TProperty>(this XElement el, TTarget target,
            Expression<Func<TTarget, TProperty>> targetExpression)
        {
            var propertyInfo = GetPropertyInfo(targetExpression);
            var name = propertyInfo.Name;
            var val = propertyInfo.GetValue(target, null);

            if (typeof(TProperty) == typeof(string))
            {
                el.Attr(name, (string)val);
            }
            else if (val == null)
            {
                el.Attr(name, "null");
            }
            else if (typeof(TProperty) == typeof(int))
            {
                el.Attr(name, (int)val);
            }
            else if (typeof(TProperty) == typeof(bool))
            {
                el.Attr(name, (bool)val);
            }
            else if (typeof(TProperty) == typeof(System.DateTime))
            {
                el.Attr(name, (System.DateTime)val);
            }
            else if (typeof(TProperty) == typeof(double))
            {
                el.Attr(name, (double)val);
            }
            else if (typeof(TProperty) == typeof(float))
            {
                el.Attr(name, (float)val);
            }
            else if (typeof(TProperty) == typeof(decimal))
            {
                el.Attr(name, (decimal)val);
            }
            else if (typeof(TProperty) == typeof(int?))
            {
                el.Attr(name, (int?)val);
            }
            else if (typeof(TProperty) == typeof(bool?))
            {
                el.Attr(name, (bool?)val);
            }
            else if (typeof(TProperty) == typeof(System.DateTime?))
            {
                el.Attr(name, (System.DateTime?)val);
            }
            else if (typeof(TProperty) == typeof(double?))
            {
                el.Attr(name, (double?)val);
            }
            else if (typeof(TProperty) == typeof(float?))
            {
                el.Attr(name, (float?)val);
            }
            else if (typeof(TProperty) == typeof(decimal?))
            {
                el.Attr(name, (decimal?)val);
            }
            return el;
        }

        public static PropertyInfo GetPropertyInfo<TContext, TProperty>(Expression<Func<TContext, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression is not a member expression.");
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null) throw new InvalidOperationException("Expression is not for a property.");
            return propertyInfo;
        }

        /// <summary>
        /// Gives context to an XElement, enabling chained property operations.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="el">The element.</param>
        /// <param name="context">The context.</param>
        /// <returns>The element with context.</returns>
        public static XElementWithContext<TContext> With<TContext>(this XElement el, TContext context)
        {
            return new XElementWithContext<TContext>(el, context);
        }

        public class XElementWithContext<TContext>
        {
            public XElementWithContext(XElement element, TContext context)
            {
                Element = element;
                Context = context;
            }

            public XElement Element { get; private set; }
            public TContext Context { get; private set; }

            public static implicit operator XElement(XElementWithContext<TContext> elementWithContext)
            {
                return elementWithContext.Element;
            }

            /// <summary>
            /// Replaces the current context with a new one, enabling chained action on different objects.
            /// </summary>
            /// <typeparam name="TNewContext">The type of the new context.</typeparam>
            /// <param name="context">The new context.</param>
            /// <returns>A new XElementWithContext, that has the new context.</returns>
            public XElementWithContext<TNewContext> With<TNewContext>(TNewContext context)
            {
                return new XElementWithContext<TNewContext>(Element, context);
            }

            /// <summary>
            /// Sets the value of a context property as an attribute of the same name on the element.
            /// </summary>
            /// <typeparam name="TProperty">The type of the property.</typeparam>
            /// <param name="targetExpression">The property expression.</param>
            /// <returns>Itself</returns>
            public XElementWithContext<TContext> ToAttr<TProperty>(
                Expression<Func<TContext, TProperty>> targetExpression)
            {
                Element.ToAttr(Context, targetExpression);
                return this;
            }

            /// <summary>
            /// Gets an attribute on the element and sets the property of the same name on the context with its value.
            /// </summary>
            /// <typeparam name="TProperty">The type of the property.</typeparam>
            /// <param name="targetExpression">The property expression.</param>
            /// <returns>Itself</returns>
            public XElementWithContext<TContext> FromAttr<TProperty>(
                Expression<Func<TContext, TProperty>> targetExpression)
            {
                Element.FromAttr(Context, targetExpression);
                return this;
            }

            /// <summary>
            /// Evaluates an attribute from an expression.
            /// It's a nice strongly-typed way to read attributes.
            /// </summary>
            /// <typeparam name="TProperty">The type of the property.</typeparam>
            /// <param name="expression">The property expression.</param>
            /// <returns>The attribute, ready to be cast.</returns>
            public TProperty Attr<TProperty>(Expression<Func<TContext, TProperty>> expression)
            {
                var propertyInfo = GetPropertyInfo(expression);
                var name = propertyInfo.Name;
                return Element.Attr<TProperty>(name);
            }
        }
    }
}
