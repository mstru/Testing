using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Testing.Automation.Web.Interfaces;
using OpenQA.Selenium;

namespace Testing.Automation.Web.Setup
{
    /// <summary>
    ///     Vytvaranie instancie relacii pre session.
    /// </summary>
    /// <typeparam name="TSession"></typeparam>
    public class Threaded<TSession> where TSession : Session
    {
        public const string InvalidSessionTypeFormat =
            "The instance type specified ({0}) is not a valid session.  It must have a constructor that takes an IDriverEnvironment and/or ISettings.";

        private static readonly ThreadLocal<TSession> ThreadLocalSession = new ThreadLocal<TSession>();

        private static TSession Current
        {
            get; set;
        }

        /// <summary>
        ///     Umožňuje vytvorenie typu založeného na relácii, pomocou typu odvodeného od IDriverEnvironment, ktorý systém môže inicializacia 
        ///     pomocou konstruktora bez parametrov.  
        /// </summary>
        /// <typeparam name="TDriverEnvironment"></typeparam>
        /// <returns></returns>
        public static TSession With<TDriverEnvironment>() where TDriverEnvironment : IDriverEnvironment, new()
        {
            return With(new TDriverEnvironment());
        }

        public static TSession With<TDriverEnvironment>(ISettings settings)
            where TDriverEnvironment : IDriverEnvironment, new()
        {
            return With(new TDriverEnvironment(), settings);
        }

        /// <summary>
        ///     Umožňuje vytvorenie typu založeného na relácii pomocou inštancie typu IDriverEnvironment.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static TSession With(IDriverEnvironment environment)
        {
            if (Settings.ReinitializeTestDestroy || Current == null)
            {
                if (Current != null)
                {
                    Current.EndWebDriverInstance();
                    Current = null;
                }

                Current = GetInstanceOf<TSession>(environment);
                Settings.ReinitializeTestDestroy = false;
            }

            return Current;
        }

        public static TSession With(IDriverEnvironment environment, ISettings settings)
        {
            if (Current != null)
            {
                Current.EndWebDriverInstance();
                Current = null;
            }

            Current = GetInstanceOf<TSession>(environment, settings);
            return Current;
        }

        public static TResult WithCurrent<TResult>(Func<TSession, TResult> action)
        {
            if (Current == null)
            {
                throw new NullReferenceException(
                    "You cannot access the CurrentBlock without first initializing the Session by calling With<TDriverEnvironment>().");
            }
            return action(Current);
        }

        public static TBlock NavigateToBase<TBlock>(string url = null) where TBlock : IBlock
        {
            return WithCurrent(s => s.NavigateTo<TBlock>(url));
        }

        public static TBlock NavigateTo<TBlock>() where TBlock : IBlock
        {
            return WithCurrent(s => s.NavigateTo<TBlock>(true));
        }

        private static T GetInstanceOf<T>(params object[] constructorArgs)
            where T : Session
        {
            var type = typeof (T);

            IList<Type> constructorSignature = constructorArgs.Select(arg => arg.GetType()).ToList();

            var constructor = type.GetConstructor(constructorSignature.ToArray());

            if (constructor != null)
            {
                return constructor.Invoke(constructorArgs.ToArray()) as T;
            }

            var message = string.Format(InvalidSessionTypeFormat, type);
            throw new ArgumentException(message);
        }

        public static TBlock CurrentBlock<TBlock>(IWebElement tag = null) where TBlock : IBlock
        {
            if (Current == null)
            {
                throw new NullReferenceException(
                    "You cannot access the CurrentBlock without first initializing the Session by calling With<TDriverEnvironment>().");
            }

            return Current.CurrentBlock<TBlock>();
        }
    }
}