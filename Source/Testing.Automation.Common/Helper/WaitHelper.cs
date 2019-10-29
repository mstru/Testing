using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Testing.Automation.Common.Helper
{
    /// <summary>
    /// Trieda obsahuje metódy čakania.
    /// </summary>
    public static class WaitHelper
    {
        /// <summary>
        /// Počká na stav s daným časovým limitom.
        /// </summary>
        /// <param name="condition">Podmienka, ktorá sa má splniť.</param>
        /// <param name="timeout">Hodnota časového limitu [seconds] udáva, ako dlho sa má počkať.</param>
        /// <param name="message">Chybová správa.</param>
        public static void Wait(Func<bool> condition, TimeSpan timeout, string message)
        {
            Wait(condition, timeout, TimeSpan.FromSeconds(1), message);
        }

        /// <summary>
        /// Počká na stav s daným časovým limitom.
        /// </summary>
        /// <param name="condition">Podmienka, ktorá sa má splniť.</param>
        /// <param name="timeout">Hodnota časového limitu [sekundách] udáva, ako dlho sa má počkať.</param>
        /// <param name="sleepInterval">Hodnota [sekundách] udáva, ako často sa má skontrolovať, či je zadaná podmienka pravdivá.</param>
        /// <param name="message">Chybová správa.</param>
        public static void Wait(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval, string message)
        {
            var start = System.DateTime.Now;
            var result = false;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(condition, canceller.Token);

            while ((System.DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    task = Task.Factory.StartNew(
                        () =>
                        {
                            using (canceller.Token.Register(Thread.CurrentThread.Abort))
                            {
                                return condition();
                            }

                        }, canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();

            if (!result)
            {
                throw new TimeoutException(string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s), {1}", timeout.TotalSeconds, message));
            }
        }

        public static T Wait<T>(this T value, Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval, string message)
        {
            var start = System.DateTime.Now;
            var result = false;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(condition, canceller.Token);

            while ((System.DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    task = Task.Factory.StartNew(
                        () =>
                        {
                            using (canceller.Token.Register(Thread.CurrentThread.Abort))
                            {
                                return condition();
                            }

                        }, canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();

            if (!result)
            {
                throw new TimeoutException(string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s), {1}", timeout.TotalSeconds, message));
            }

            return value;
        }


        /// <summary>
        /// Počká na stav s daným časovým limitom.
        /// </summary>
        /// <param name="condition">Podmienka, ktorá sa má splniť.</param>
        /// <param name="timeout">Hodnota časového limitu [sekundách] udáva, ako dlho sa má počkať.</param>
        /// <param name="sleepInterval">Hodnota [sekundách] udáva, ako často sa má skontrolovať, či je zadaná podmienka pravdivá.</param>
        /// <returns></returns>
        public static bool Wait(Func<bool> condition, TimeSpan timeout, TimeSpan sleepInterval)
        {
            var result = false;
            var start = System.DateTime.Now;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(condition, canceller.Token);

            while ((System.DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    task = Task.Factory.StartNew(
                        () =>
                        {
                            using (canceller.Token.Register(Thread.CurrentThread.Abort))
                            {
                                return condition();
                            }
                        },
                              canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();
            return result;
        }

        /// <summary>
        /// Pomocná metóda pre prácu s sql príkazom, počká na stav s daným časovým limitom.
        /// </summary>
        /// <param name="timeout">Hodnota časového limitu [sekundách] udáva, ako dlho sa má počkať.</param>
        /// <param name="sleepInterval">Hodnota [sekundách] udáva, ako často sa má skontrolovať, či je zadaná podmienka pravdivá.</param>
        /// <param name="sqlCommand">SQL príkaz.</param>
        /// <param name="connectionString">Pripojenie do databázy.</param>
        /// <param name="dbcolumn">Stĺpec ktorý nás zaujíma.</param>
        /// <param name="message">Chybová správa.</param>
        public static void Wait(TimeSpan timeout, TimeSpan sleepInterval, string sqlCommand, string dbcolumn, string expectedValue, string message, EnvironmentSettings DatabaseSettings = null)
        {
            var start = System.DateTime.Now;
            var result = false;

            while ((System.DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                ICollection<string> dbValue = SqlHelper.ExecuteSqlCommand1(sqlCommand, dbcolumn, DatabaseSettings);

                if (dbValue.Count != 0)
                {
                    if (dbValue.FirstOrDefault().Equals(expectedValue))
                    {
                        result = true;
                        break;
                    }
                }
                Thread.Sleep(sleepInterval);
            }
            if (!result)
            {
                throw new Exception((string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s). {2}{1}", timeout.TotalSeconds, message, Environment.NewLine)));
            }
        }

        /// <summary>
        /// Pomocná metóda pre prácu s sql príkazom, počká na stav s daným časovým limitom.
        /// </summary>
        /// <param name="timeout">Hodnota časového limitu [sekundách] udáva, ako dlho sa má počkať.</param>
        /// <param name="sleepInterval">Hodnota [sekundách] udáva, ako často sa má skontrolovať, či je zadaná podmienka pravdivá.</param>
        /// <param name="sqlCommand">SQL príkaz.</param>
        /// <param name="dbcolumn">Stĺpec ktorý nás zaujíma.</param>
        /// <param name="message">Chybová správa.</param>
        public static T Wait<T>(this T value, TimeSpan timeout, TimeSpan sleepInterval, string sqlCommand, string dbcolumn, string waitValue, string message, EnvironmentSettings DatabaseSettings = null)
        {
            var start = System.DateTime.Now;
            var result = false;

            while ((System.DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                ICollection<string> dbValue = SqlHelper.ExecuteSqlCommand1(sqlCommand, dbcolumn, DatabaseSettings);

                if (dbValue.Count != 0)
                {
                    if (dbValue.FirstOrDefault().Equals(waitValue))
                    {
                        result = true;
                        break;
                    }
                }
                Thread.Sleep(sleepInterval);
            }
            if (!result)
            {
                throw new Exception((string.Format(CultureInfo.CurrentCulture, "Timeout after {0} second(s). {2}{1}", timeout.TotalSeconds, message, Environment.NewLine)));
            }

            return value;
        }
    }
}
