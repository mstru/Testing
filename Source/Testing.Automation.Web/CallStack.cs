using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TechTalk.SpecFlow;
using Testing.Automation.Common;
using Testing.Automation.Reporter.Exceptions;

namespace Testing.Automation.Web
{
    public static class CallStack
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetCurrentMethod()
        {
            var frame = new StackFrame(1, false);

            return frame.GetMethod();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetCallingMethod()
        {
            var frame = new StackFrame(2, false);

            return frame.GetMethod();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetConstructingMethod()
        {
            var trace = new StackTrace(false);

            var frames = trace.GetFrames();

            if (frames == null)
            {
                throw new Exception("Unable to get the StackFrames from the StackTrace.");
            }

            if (frames[1].GetMethod().IsConstructor == false)
            {
                throw new ArgumentException("You may only call GetConstructingMethod from a Constructor.");
            }

            var frame = frames
                .Skip(1)
                .First(x => x.GetMethod().IsConstructor == false);

            return frame.GetMethod();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase GetFirstNonSeleniteMethod()
        {
            var trace = new StackTrace(false);

            var frames = trace.GetFrames();

            if (frames == null)
            {
                throw new Exception("Unable to get the StackFrames from the StackTrace.");
            }

            var method = frames
                .Select(x => x.GetMethod())
                .First(x => (x.DeclaringType != null)
                            && (x.DeclaringType.Assembly != typeof (CallStack).Assembly)                          
                            && (System.Attribute.IsDefined(x, typeof (CompilerGeneratedAttribute)) == false)
                            && (System.Attribute.IsDefined(x.DeclaringType, typeof (CompilerGeneratedAttribute)) == false));

            return method;
        }
    }
}