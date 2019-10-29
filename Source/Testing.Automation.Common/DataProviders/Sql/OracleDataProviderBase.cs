using Oracle.DataAccess.Client;
using Testing.Automation.Common;

namespace Testing.Automation.Common.DataProviders.Sql
{
    /// <summary>
    /// TODO
    /// </summary>
    public class OracleDataProviderBase
    {
        public OracleDataProviderBase()
        {
        }

        /// <summary>
        /// Nastavenia používané pre akcie databázy, ako sú poverenia, databázové servery a názvy databáz..
        /// </summary>
        public static EnvironmentSettings DatabaseSettings { get; set; }

        public static OracleConnection GetDatabaseConnection(EnvironmentSettings environmentSettings)
        {
            if (environmentSettings == null)
            {
                environmentSettings = new EnvironmentSettings();
            }

            string sConnectionString = string.Empty;

            if (environmentSettings.Sid.StartsWith("ISZO"))
                environmentSettings.Password = environmentSettings.Password1;

            if (environmentSettings.Sid.StartsWith("OFS"))
                environmentSettings.Password = environmentSettings.Password2;

            sConnectionString =
                 string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={2}))); User Id={3}; Password={4}; Persist Security Info=False",
                     environmentSettings.Host,
                     environmentSettings.Port,
                     environmentSettings.Sid,
                     environmentSettings.Username,
                     environmentSettings.Password);

            return new OracleConnection(sConnectionString);
        }

        public static string GetOdbcConnection(EnvironmentSettings environmentSettings = null)
        {
            if (environmentSettings == null)
            {
                environmentSettings = new EnvironmentSettings();
            }

            string sConnectionString = string.Empty;

            if (environmentSettings.Sid.StartsWith("ISZO"))
                environmentSettings.Password = environmentSettings.Password1;

            if (environmentSettings.Sid.StartsWith("OFS"))
                environmentSettings.Password = environmentSettings.Password2;

            if (environmentSettings.Password == null)
                environmentSettings.Password = environmentSettings.Password1;

            sConnectionString =
                 string.Format("Dsn={0};uid={1};pwd={2};charset=UTF8;Unicode=True",
                     environmentSettings.Sid,
                     environmentSettings.Username,
                     environmentSettings.Password);

            return sConnectionString;
        }
    }
}
