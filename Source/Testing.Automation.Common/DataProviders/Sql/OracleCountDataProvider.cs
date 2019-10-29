using Oracle.DataAccess.Client;
using System;

namespace Testing.Automation.Common.DataProviders.Sql
{
    /// <summary>
    /// TODO
    /// </summary>
    public class OracleCountDataProvider : OracleDataProviderBase, IExternalDataProvider<OracleQuery, OracleCountResult>
    {
        private OracleQuery _query;

        private readonly string _targetDatabase;

        /// <summary>
        /// Vytvorenie inštancie novej <see cref="OracleDataProviderBase"/> triedy
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="targetDatabase"></param>
        public OracleCountDataProvider(EnvironmentSettings settings, string targetDatabase) : base()
        {
            _targetDatabase = targetDatabase;
        }

        /// <summary>
        /// Definuje dopyt, ktorý sa má vykonať voči poskytovateľovi
        /// </summary>
        /// <param name="query"></param>
        public void DefineQuery(OracleQuery query)
        {
            _query = query;
        }

        /// <summary>
        /// Vykoná dotaz voči poskytovateľovi údajov
        /// </summary>
        /// <returns></returns>
        public OracleCountResult Execute()
        {
            var retVal = new OracleCountResult
            {
                Success = false
            };

            int nCount = -1;

            using (OracleConnection oOracleConnection = GetDatabaseConnection(DatabaseSettings))
            {
                var command = new OracleCommand(_query.Query, oOracleConnection);

                try
                {
                    oOracleConnection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        nCount = Convert.ToInt16(reader[0]);
                    }
                    reader.Close();
                    retVal.ResultCount = nCount;
                    retVal.Success = true;
                }
                catch (Exception ex)
                {
                    retVal.ExceptionMessage =
                        $"Exception occured while getting data from {_targetDatabase} database using count query \"{_query.Query}\". Exception thrown is: {ex}";
                }

                return retVal;
            }
        }
    }
}
