using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Testing.Automation.Common;

namespace Testing.Automation.Common.DataProviders.Sql
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <summary>
    /// Trieda sa používa na vykonávanie dotazov Sql a na čítanie údajov z databázy.
    /// </summary>
    public class OracleQueryDataProvider : OracleDataProviderBase
    {
        /// <summary>
        /// Metóda sa používa na vykonanie dotazu SQL (select) a na čítanie každého riadku zo stĺpca.
        /// </summary>
        /// <param name="command">SQL query.</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <param name="column">Názov stĺpca.</param>
        /// <returns>Collections všetkých dát pre stĺpec.</returns>
        public static ICollection<string> ExecuteSqlCommand(string command, string column, EnvironmentSettings DatabaseSettings = null)
        {
            var resultList = new List<string>();

            using (OracleConnection oOracleConnection = GetDatabaseConnection(DatabaseSettings))
            {
                oOracleConnection.Open();

                OracleGlobalization info = oOracleConnection.GetSessionInfo();
                info.DateFormat = "DD.MM.RR HH24:MI:SS";
                oOracleConnection.SetSessionInfo(info);

                using (var oracleCommand = new OracleCommand(command, oOracleConnection))
                {
                    using (var oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        if (!oracleDataReader.HasRows)
                        {
                            return resultList;
                        }

                        while (oracleDataReader.Read())
                        {
                            resultList.Add(oracleDataReader[column].ToString());
                        }

                        Dispose(oOracleConnection, oracleDataReader);
                    }
                }
            }

            if (resultList.Count == 0)
            {
                throw new Exception(string.Format(CultureInfo.CurrentCulture, "No result for: {0} \n {1}", command, column));
            }

            return resultList;
        }

        /// <summary>
        /// Metóda sa používa na vykonanie dotazu SQL (select) a na čítanie každého riadku zo stĺpca.
        /// </summary>
        /// <param name="command">SQL query.</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <param name="column">Názov stĺpca.</param>
        /// <returns>Dictionary všetkých dát pre stĺpec.</returns>
        public static Dictionary<string, string> ExecuteSqlCommand(string command, IEnumerable<string> columns, EnvironmentSettings DatabaseSettings = null)
        {
            var resultList = new Dictionary<string, string>();
            var resultTemp = new Dictionary<string, string>();

            using (OracleConnection oOracleConnection = GetDatabaseConnection(DatabaseSettings))
            {
                oOracleConnection.Open();

                OracleGlobalization info = oOracleConnection.GetSessionInfo();
                info.DateFormat = "DD.MM.RR HH24:MI:SS";
                oOracleConnection.SetSessionInfo(info);

                using (var oracleCommand = new OracleCommand(command, oOracleConnection))
                {
                    using (var oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        if (!oracleDataReader.HasRows)
                        {
                            return resultList;
                        }

                        while (oracleDataReader.Read())
                        {
                            for (int i = 0; i < oracleDataReader.FieldCount; i++)
                            {
                                resultTemp[oracleDataReader.GetName(i)] = oracleDataReader.GetValue(i).ToString();
                            }
                        }
                        Dispose(oOracleConnection, oracleDataReader);
                    }
                }
            }

            foreach (string column in columns)
            {
                string keyValue;

                if (resultTemp.TryGetValue(column, out keyValue))
                {
                    resultList[column] = keyValue;
                }
                else
                {
                    throw new Exception(string.Format(CultureInfo.CurrentCulture, "Exception while trying to get results from sql query, lack of column '{0}'", column));
                }
            }

            return resultList;
        }

        /// <summary>
        /// Metóda sa používa na vykonanie dotazu SQL (select) a na čítanie každého riadku pre všetky stĺpce.
        /// </summary>
        /// <param name="command">SQL query.</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <returns>DataTable všetkých dát pre stĺpec.</returns>
        public static DataTable ExecuteSqlCommand(string command, EnvironmentSettings DatabaseSettings = null)
        {
            var resultList = new DataTable();

            using (OracleConnection oOracleConnection = GetDatabaseConnection(DatabaseSettings))
            {
                oOracleConnection.Open();

                OracleGlobalization info = oOracleConnection.GetSessionInfo();
                info.DateFormat = "DD.MM.RR HH24:MI:SS";
                oOracleConnection.SetSessionInfo(info);

                using (var oracleCommand = new OracleCommand(command, oOracleConnection))
                {
                    using (var oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        if (!oracleDataReader.HasRows)
                        {
                            return resultList;
                        }

                        resultList.Load(oracleDataReader);

                        Dispose(oOracleConnection, oracleDataReader);
                    }
                }
            }

            if (resultList.Rows.Count == 0)
            {
                throw new Exception(string.Format(CultureInfo.CurrentCulture, "No result for: {0}", command));
            }

            return resultList;
        }

        /// <summary>
        /// Metóda sa používa na vykonanie dotazu SQL (Commit).
        /// </summary>
        /// <param name="SQL">SQL queary.</param>
        /// <param name="connectionString">Pripojenie.</param>
        public static void ExecuteSqlCommandCommit(string SQL, EnvironmentSettings DatabaseSettings = null)
        {
            try
            {
                DataTable dt = new DataTable();
                OracleDataReader reader = null;

                using (OracleConnection oOracleConnection = GetDatabaseConnection(DatabaseSettings))
                {
                    try
                    {
                        OracleCommand command = new OracleCommand(SQL, oOracleConnection);
                        oOracleConnection.Open();

                        OracleTransaction transaction = oOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                        command.Transaction = transaction;
                        command.CommandText = SQL;
                        command.ExecuteNonQuery();          

                        transaction.Commit();
                    }
                    finally
                    {
                        Dispose(oOracleConnection, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(CultureInfo.CurrentCulture, "Exception while trying to commit for: {0} /n {1}", SQL, ex));
            }
        }


        private static void Dispose(OracleConnection connection, OracleDataReader oracleDataReader)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                }

                if (oracleDataReader != null)
                {
                    oracleDataReader.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error disposing OracleHelper class: {0}", ex.Message));
            }
        }
    }
}
