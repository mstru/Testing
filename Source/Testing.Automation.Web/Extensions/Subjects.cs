using SqlKata;
using SqlKata.Compilers;
using System;
using System.Data;
using Testing.Automation.Common.Helper;
using Testing.Automation.Reporter.Exceptions;

namespace Testing.Automation.Web.Extensions
{
    /// <summary>
    /// Trieda na ziskanie minimalneho poctu validnych subjektov zuctovania.
    /// </summary>
    public static class Subjects
    {
        /// <summary>
        /// Minimum dfz isot
        /// </summary>
        private const string minimumDfz = "250000";

        /// <summary>
        /// Minimum eooz iszo
        /// </summary>
        private const string minimumEooz = "2000";

        /// <summary>
        /// DataTable
        /// </summary>
        private static DataTable myActualListValidOfSubjects;

        /// <summary>
        /// Enum
        /// </summary>
        public enum Column
        {
            NAZOV_SUBJEKTU = 0, KOD_SUBJEKTU = 1, EIC = 2, ZOHLAD_DPH_H = 3
        }

        /// <summary>
        /// Ziskanie subjetov zuctovania z DataTable.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string GetValidSubjects(int row, Column col)
        {
            return myActualListValidOfSubjects.Rows[row].Field<object>((int)col).ToString();
        }

        /// <summary>
        /// Nachystanie validnych subjektov zuctovania.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="minimumNumberOfSubjects">Minimálny počet potrebných subjektov zúčtovania</param>
        /// <returns></returns>
        public static T PrepareValidSubjects<T>(this T value, int minimumNumberOfSubjects)
        {
            myActualListValidOfSubjects = SqlHelper.ExecuteSqlCommand(SqlQuery);

            if (myActualListValidOfSubjects.Rows.Count < minimumNumberOfSubjects)
            {
                throw new VerificationException(
                    $"Failed, is insufficient count of subjects with valid contract. My actual list subjects with valid contract is '{myActualListValidOfSubjects.Rows.Count}' but should be '{minimumNumberOfSubjects}'");
            }

            return value;
        }

        /// <summary>
        /// SQl query vrati subjekty zuctovania vyhovujuce danej podmienke, pre dostatočné fz dfz/eooz - ISZO/ISOT.
        /// </summary>
        /// <returns>Naformátovaný sql dotaz.</returns>
        static string SqlQuery
        {
            get
            {
                var compiler = new SqlServerCompiler();

                var query = new Query()
                    .SelectRaw("DISTINCT ut.nazov_subjektu, ut.kod_subjektu, map.eic, round(fz_zo.eooz, 2), round(fz_dt.dfz, 2), zp.zohlad_dph_h")
                    .FromRaw("xmuser.t_map_subjekty map, xmuser.t_fz__hodnotenie_fz fz_zo, xmuser.t_fz__hodnotenie_dt fz_dt, xmuser.v_portal_subjekty_zmluvy ut, xmuser.v_fe__zmluvne_podmienky zp")
                    .WhereRaw("map.zuctovanie_id = ut.kod_subjektu and fz_zo.subjekt_id = ut.kod_subjektu and fz_dt.subjekt_id = ut.kod_subjektu and zp.subjekt_id = ut.kod_subjektu")
                    .WhereRaw("trunc(sysdate + 1) between nvl(map.platny_od, trunc(sysdate + 1)) and nvl(map.platny_do, trunc(sysdate + 1))")
                    .WhereRaw("trunc(sysdate + 1) between zp.obdobie_od and zp.obdobie_do")
                    .WhereRaw("trunc(sysdate + 1) between ut.obdobie_od and ut.obdobie_do")
                    .Where("zp.predmet_h", "=", 40)
                    .Where("map.platny", "=", 1)
                    .Where("fz_zo.platny", "=", 1)
                    .Where("fz_dt.platny", "=", 1)
                    .WhereRaw("fz_zo.datum = trunc(sysdate + 1)")
                    .WhereNull("fz_dt.datum")
                    .Where("fz_zo.eooz", ">", minimumEooz)
                    .Where("fz_dt.dfz", ">", minimumDfz)
                    .WhereNotContains("ut.nazov_subjektu", "AXPO Trading  AG")
                    .OrderBy("ut.nazov_subjektu");
                return compiler.Compile(query).ToString().Replace("[", "").Replace("]", "");
            }
        }
    }
}
