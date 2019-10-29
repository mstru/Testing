using System;

namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IJavaScriptInvoker
    {
        string InvokeScript(string script);
        string InvokeScript(string jqueryExpression, params object[] text);
        Boolean InvokeScriptWithCondition(string script, string conditions);
        Boolean InvokeScriptWithCondition(string script);
    }
}
