using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Testing.Automation.Common.Helper
{
    public class PropertyHelper
    {
        // <summary>
        // Získajte názov statickej alebo inštančnej vlastnosti z prístupu k vlastnostiam lambda.
        // </summary>
        // <typeparam name="T">Type of the property</typeparam>
        // <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        // <returns>The name of the property</returns>
        public static string GetPropertyNameAsString<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static string ReadProperty(string value, string path)
        {
            var enviroment = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(path))
                enviroment.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));

            return enviroment[value];
        }
    }
}
