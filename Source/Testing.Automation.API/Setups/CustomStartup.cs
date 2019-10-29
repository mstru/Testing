﻿namespace Testing.Automation.API.Setups
{
    using System.Linq;
    using Owin;

    public class CustomStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                if (context.Request.Method == "GET" && context.Request.Uri.OriginalString.EndsWith("/cookies"))
                {
                    context.Response.StatusCode = 200;
                    return context.Response.WriteAsync(string.Join("!", context.Request.Cookies.Select(c => string.Format("{0}+{1}", c.Key, c.Value))));
                }

                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/test"))
                {
                    context.Response.StatusCode = 302;
                    return context.Response.WriteAsync("Found!");
                }

                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/json"))
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"IntegerValue\":1,\"StringValue\":\"Test\"}");
                }

                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/nomodel"))
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"id\":1}");
                }

                if (context.Request.Headers.ContainsKey("CustomHeader"))
                {
                    return context.Response.WriteAsync("OK!");
                }

                if (!context.Request.Headers.ContainsKey("CustomHeader"))
                {
                    context.Response.StatusCode = 404;
                    return context.Response.WriteAsync("Header not found!");
                }

                context.Response.StatusCode = 404;
                return context.Response.WriteAsync("Not found!");
            });
        }
    }
}
