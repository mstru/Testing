using System.Collections.Generic;
using System.IO;
using Testing.Automation.Common;
using Testing.Automation.Common.Enums;
using Testing.Automation.Common.Helper;

namespace Testing.Automation.Web.Extensions
{
    /// <summary>
    /// Rozšírená trieda pre prácu pri exportoch
    /// </summary>
    public static class Exporting
    {
        /// <summary>
        /// Čakanie na navýšenie počtu súborov v adresári a získanie súborov daného formátu, použite postfix FileName vo vyhľadávacom vzore.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ICollection<FileInfo> WaitFileOfGivenFormat<T>(this T value, FileFormat format)
        {
            return FileHelper
                .WaitForFileOfGivenFormatAndReturnFiles(format, BaseConfiguration.PathToDownloadFolder, 0, true);
        }

        /// <summary>
        /// Vymazanie súborov v danom adresári.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Delete<T>(this T obj)
        {
            FileHelper
                .DeleteFiles(BaseConfiguration.PathToDownloadFolder);
            return obj;
        }
    }
}
