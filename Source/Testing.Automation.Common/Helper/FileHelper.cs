using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Testing.Automation.Common.Enums;

namespace Testing.Automation.Common.Helper
{
    /// <summary>
    /// Trieda na spracovanie stiahnutých súborov
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Oddelovač adresárov.
        /// </summary>
        public static readonly char Separator = Path.DirectorySeparatorChar;

        /// <summary>
        /// Vráti príponu súboru.
        /// </summary>
        /// <param name="format">Formát súboru.</param>
        /// <returns>Vráti príponu.</returns>
        public static string ReturnFileExtension(FileFormat format)
        {
            switch (format)
            {
                case FileFormat.csv:
                    return ".csv";
                case FileFormat.jpg:
                    return ".jpg";
                case FileFormat.pdf:
                    return ".pdf";
                case FileFormat.png:
                    return ".png";
                case FileFormat.svg:
                    return ".svg";
                case FileFormat.xlsx:
                    return ".xlsx";
                case FileFormat.xml:
                    return ".xml";
                case FileFormat.zip:
                    return ".zip";
                default:
                    return "none";
            }
        }

        /// <summary>
        /// Odstránenie všetkých priečinkov na zadanej ceste podľa vyhĺadávacieho vzora.
        /// </summary>
        /// <param name="folder">Cesta k priečinkom.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        public static void DeleteDirectories(string folder, string postFixFilesName)
        {
            GetAllDirectories(folder, postFixFilesName).ToList().ForEach(f => f.Delete(true));
        }

        /// <summary>
        /// Odstránenie všetkých priečinkov na zadanej ceste.
        /// </summary>
        /// <param name="folder">Cesta k priečinkom.</param>
        public static void DeleteDirectories(string folder)
        {
            GetAllDirectories(folder, string.Empty).ToList().ForEach(f => f.Delete(true));
        }

        /// <summary>
        /// Odstránenie všetkých súborov na zadanej ceste.
        /// </summary>
        /// <param name="folder">Cesta k súborom.</param>
        public static void DeleteFiles(string folder)
        {
            GetAllFiles(folder).ToList().ForEach(f => f.Delete());
        }

        /// <summary>
        /// Odstránenie všetkých súborov na zadanej ceste podľa vyhĺadávacieho vzora.
        /// </summary>
        /// <param name="folder">Cesta k súborom.</param>
        public static void DeleteFiles(string folder, string postFixFilesName)
        {
            GetAllFiles(folder, postFixFilesName).ToList().ForEach(f => f.Delete());
        }

        /// <summary>
        /// Odstránenie všetkých súborov na zadanej ceste podľa danej podmienky vytvorenia.
        /// </summary>
        /// <param name="folder">Cesta k súborom.</param>
        /// <param name="timeSpan">Podmienka pre časovo staré súbory v minutách.</param>
        public static void DeleteFilesOfGivenCreationTime(string folder, TimeSpan timeSpan)
        {
            GetAllFiles(folder).ToList().Where(f => f.CreationTime < System.DateTime.Now.Subtract(timeSpan)).ToList().ForEach(f => f.Delete());
        }

        /// <summary>
        /// Odstránenie všetkých súborov na zadanej ceste podľa vyhĺadávacieho vzora a danej podmienky vytvorenia.
        /// </summary>
        /// <param name="folder">Cesta k súborom.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <param name="timeSpan">Podmienka pre časovo staré súbory v minutách.</param>
        public static void DeleteFilesOfGivenCreationTime(string folder, string postFixFilesName, TimeSpan timeSpan)
        {
            GetAllFiles(folder, postFixFilesName).ToList().Where(f => f.CreationTime < System.DateTime.Now.Subtract(timeSpan)).ToList().ForEach(f => f.Delete());
        }

        /// <summary>
        /// Odstránenie všetkých priečinkov na zadanej ceste podľa vyhĺadávacieho vzora a danej podmienky vytvorenia.
        /// </summary>
        /// <param name="folder">Cesta k priečinkom.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <param name="timeSpan">Podmienka pre časovo staré súbory v minutách.</param>
        public static void DeleteDirectoriesOfGivenCreationTime(string folder, string postFixFilesName, TimeSpan timeSpan)
        {
            GetAllDirectories(folder, postFixFilesName).ToList().Where(f => f.CreationTime < System.DateTime.Now.Subtract(timeSpan)).ToList().ForEach(f => f.Delete(true));
        }

        /// <summary>
        /// Odstránenie všetkých priečinkov na zadanej ceste podľa danej podmienky vytvorenia.
        /// </summary>
        /// <param name="folder">Cesta k priečinkom.</param>
        /// <param name="timeSpan">Podmienka pre časovo staré súbory v minutách.</param>
        public static void DeleteDirectoriesOfGivenCreationTime(string folder, TimeSpan timeSpan)
        {
            GetAllDirectories(folder, string.Empty).ToList().Where(f => f.CreationTime < System.DateTime.Now.Subtract(timeSpan)).ToList().ForEach(f => f.Delete(true));
        }

        /// <summary>
        /// Získanie všetkých priečinkov na zadanej ceste podľa vyhĺadávacieho vzora.
        /// </summary>
        /// <param name="folder">Cesta k priečinkom.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <returns>Zoznam priečinkov zotriedených podľa času vytvorenia.</returns>
        public static ICollection<DirectoryInfo> GetAllDirectories(string folder, string postFixFilesName)
        {
            ICollection<DirectoryInfo> directories =
                new DirectoryInfo(folder).GetDirectories($"{postFixFilesName}*").OrderBy(f => f.CreationTime).ToList();
            return directories;
        }

        /// <summary>
        /// Získate súborov daného formátu, použite postfix FileName vo vyhľadávacom vzore.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát súboru.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetFilesOfGivenFormat(string folder, FileFormat format)
        {
            return GetFilesOfGivenFormat(folder, format, string.Empty);
        }

        /// <summary>
        /// Čakanie na navýšenie počtu súborov v adresári a získanie súborov daného formátu, použite postfix FileName vo vyhľadávacom vzore.
        /// </summary>
        /// <param name="format">Formát súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="filesNumber"></param>
        /// <param name="checkSize"></param>
        public static ICollection<FileInfo> WaitForFileOfGivenFormatAndReturnFiles(FileFormat format, string folder, int filesNumber, bool checkSize)
        {
            WaitForFileOfGivenFormatWithIncreaseCountReturnTrue(format, filesNumber, folder);
            return GetFilesOfGivenFormat(folder, format);
        }

        /// <summary>
        /// Získanie súborov daného formátu
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát súboru.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetFilesOfGivenFormat(string folder, FileFormat format, string postFixFilesName)
        {
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles($"*{postFixFilesName}{ReturnFileExtension(format)}").OrderBy(f => f.Name).ToList();
            return files;
        }

        /// <summary>
        /// Preberie súbory daného formátu zo všetkých podadresárov.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát súboru.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetFilesOfGivenFormatFromAllSubFolders(string folder, FileFormat format)
        {
            return GetFilesOfGivenFormatFromAllSubFolders(folder, format, string.Empty);
        }

        /// <summary>
        /// Získava súbory daného formátu zo všetkých podpriečinkov, použite postfix FileName vo vyhľadávacom vzore
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát súboru.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetFilesOfGivenFormatFromAllSubFolders(string folder, FileFormat format, string postFixFilesName)
        {
            List<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles($"*{postFixFilesName}{ReturnFileExtension(format)}", SearchOption.AllDirectories).OrderBy(f => f.Name).ToList();
            return files;
        }

        /// <summary>
        /// Preberie všetky súbory zo zložky, použite postfixFilesName vo vyhľadávacom vzore.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací vzor.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetAllFiles(string folder, string postFixFilesName)
        {
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles($"*{postFixFilesName}").OrderBy(f => f.Name).ToList();
            return files;
        }

        /// <summary>
        /// Preberie všetky súbory zo zložky, zosortuje podĺa času vytvorenia.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetAllFilesOrderByCreationTime(string folder)
        {
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles().OrderBy(f => f.CreationTime).ToList();
            return files;
        }

        /// <summary>
        /// Preberie všetky súbory zo všetkých podadresárov, použite postfixFilesName vo vyhľadávacom vzore.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="postFixFilesName">Postfix názov súborov pre vyhľadávací pattern.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetAllFilesFromAllSubFolders(string folder, string postFixFilesName)
        {
            ICollection<FileInfo> files =
                new DirectoryInfo(folder)
                    .GetFiles($"*{postFixFilesName}", SearchOption.AllDirectories).OrderBy(f => f.Name).ToList();
            return files;
        }

        /// <summary>
        /// Preberá všetky súbory zo všetkých podadresárov.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetAllFilesFromAllSubFolders(string folder)
        {
            return GetAllFilesFromAllSubFolders(folder, string.Empty);
        }

        /// <summary>
        /// Preberá všetky súbory zo zložky.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <returns>Zoznam súborov.</returns>
        public static ICollection<FileInfo> GetAllFiles(string folder)
        {
            return GetAllFiles(folder, string.Empty);
        }

        /// <summary>
        /// Ziskanie súboru podľa jeho mena v danóm priečinku.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="fileName">Názov súboru.</param>
        /// <returns>File info súboru.</returns>
        public static FileInfo GetFileByName(string folder, string fileName)
        {
            FileInfo file =
                new DirectoryInfo(folder)
                    .GetFiles(fileName).First();
            return file;
        }

        /// <summary>
        /// Spočítanie súborov podľa daného formátu.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát.</param>
        /// <returns>Počet súborov v podpriečinku.</returns>
        public static int CountFiles(string folder, FileFormat format)
        {
            var fileNum = GetFilesOfGivenFormat(folder, format).Count();
            return fileNum;
        }

        /// <summary>
        /// Spočítanie súborov.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <returns>Počet súborov v podpriečinku.</returns>
        public static int CountFiles(string folder)
        {
            var fileNum = GetAllFiles(folder).Count;
            return fileNum;
        }

        /// <summary>
        /// Ziskanie posledného súboru podľa daného formátu.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="format">Formát.</param>
        /// <returns>Posledný súbor daného formátu.</returns>
        public static FileInfo GetLastFile(string folder, FileFormat format)
        {
            var lastFile =
                new DirectoryInfo(folder).GetFiles()
                    .Where(f => f.Extension == ReturnFileExtension(format))
                    .OrderByDescending(f => f.LastWriteTime)
                    .First();
            return lastFile;
        }

        /// <summary>
        /// Ziskanie posledného súboru.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <returns>Posledný súbor.</returns>
        public static FileInfo GetLastFile(string folder)
        {
            var lastFile = new DirectoryInfo(folder).GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .First();
            return lastFile;
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do zvýšenia počtu súborov v podadresári danej podmienky, kontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesNumber">Počiatočné číslo.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static void WaitForFileOfGivenFormatWithIncreaseCount(FileFormat format, double waitTime, int filesNumber, string folder, bool checkSize)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);

            WaitHelper.Wait(
                () => CountFiles(folder, format) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            if (checkSize)
            {
                timeoutMessage = "Checking if size of last file > 0 bytes";

                WaitHelper.Wait(
                    () => GetLastFile(folder).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
                    
            }
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do zvýšenia počtu súborov v podadresári danej podmienky, kontroluje veľkosť aktuálneho súboru po dokončení vráti true.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesNumber">Počiatočné číslo.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static bool WaitForFileOfGivenFormatWithIncreaseCountReturnTrue(FileFormat format, double waitTime, int filesNumber, string folder, bool checkSize)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);
            WaitHelper.Wait(
                () => CountFiles(folder, format) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            if (checkSize)
            {
                timeoutMessage = "Checking if size of last file > 0 bytes";
                WaitHelper.Wait(
                    () => GetLastFile(folder).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
            return true;
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do rovného počtu súborov v podadresári danej podmienky, kontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesNumber">Počiatočné číslo.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static void WaitForFileOfGivenFormatWithoutIncreaseCount(FileFormat format, double waitTime, int filesNumber, string folder, bool checkSize)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to count in {0}", folder);

            WaitHelper.Wait(
                () => CountFiles(folder, format) == filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            if (checkSize)
            {
                timeoutMessage = "Checking if size of last file > 0 bytes";

                WaitHelper.Wait(
                    () => GetLastFile(folder).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            }
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do zvýšenia počtu súborov v podadresári, kontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="filesNumber">Počiatočné číslo súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static void WaitForFileOfGivenFormatWithIncreaseCount(FileFormat format, int filesNumber, string folder)
        {
            WaitForFileOfGivenFormatWithIncreaseCount(format, BaseConfiguration.MediumTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do zvýšenia počtu súborov v podadresári, kontroluje veľkosť aktuálneho súboru po dokončení vráti true.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="filesNumber">Počiatočné číslo súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static bool WaitForFileOfGivenFormatWithIncreaseCountReturnTrue(FileFormat format, int filesNumber, string folder)
        {
            return WaitForFileOfGivenFormatWithIncreaseCountReturnTrue(format, BaseConfiguration.MediumTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Čaká na súbor daného typu pre daný časový limit až do rovného počtu súborov v podadresári danej podmienky, kontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="format">Formát.</param>
        /// <param name="filesNumber">Počiatočné číslo súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static void WaitForFileOfGivenFormatWithoutIncreaseCount(FileFormat format, int filesNumber, string folder)
        {
            WaitForFileOfGivenFormatWithoutIncreaseCount(format, BaseConfiguration.MediumTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Čaká na súbor s daným menom a s daným časovým limitom, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesName">Názov súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static void WaitForFileOfGivenName(double waitTime, string filesName, string folder, bool checkSize)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file {0} in folder {1}", filesName, folder);
            WaitHelper.Wait(
                () => File.Exists($"{folder}{Separator}{filesName}"), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            if (checkSize)
            {
                timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Checking if size of file {0} > 0 bytes", filesName);
                WaitHelper.Wait(
                    () => GetFileByName(folder, filesName).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
        }

        /// <summary>
        /// Čaká na súbor s daným menom a s daným časovým limitom, a vráti jeho názov
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesName">Názov súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static FileInfo WaitForFileOfGivenNameAndReturn(double waitTime, string filesName, string folder)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file {0} in folder {1}", filesName, folder);
            WaitHelper.Wait(
                () => File.Exists($"{folder}{Separator}{filesName}"), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            return FileHelper.GetFileByName(folder, filesName);
        }



        /// <summary>
        /// Čaká na súbor s názvom a s daným časovým limitom, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesName">Názov súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static void WaitForFileOfGivenName(double waitTime, string filesName, string folder)
        {
            WaitForFileOfGivenName(waitTime, filesName, folder, true);
        }

        /// <summary>
        /// Čaká na súbor s názvom, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="filesName">Názov súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static void WaitForFileOfGivenName(string filesName, string folder)
        {
            WaitForFileOfGivenName(BaseConfiguration.MediumTimeout, filesName, folder);
        }

        /// <summary>
        /// Čaká na súbor s názvom, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="filesName">Názov súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static void WaitForFileOfGivenName(string filesName, string folder, bool checkSize)
        {
            WaitForFileOfGivenName(BaseConfiguration.MediumTimeout, filesName, folder, checkSize);
        }

        /// <summary>
        /// Čaká na súbor pre daný časový limit, kým sa počet súborov nezvýši v podadresári, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="filesNumber">Počiatočné číslo súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="checkSize">Skontrolovanie veľkosti súboru.</param>
        public static void WaitForFile(double waitTime, int filesNumber, string folder, bool checkSize)
        {
            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting for file number to increase in {0}", folder);
            WaitHelper.Wait(
                () => CountFiles(folder) > filesNumber, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);

            if (checkSize)
            {
                timeoutMessage = "Checking if size of last file > 0 bytes";

                WaitHelper.Wait(
                    () => GetLastFile(folder).Length > 0, TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            }
        }

        /// <summary>
        /// Čaká na súbor pre daný časový limit, kým sa počet súborov nezvýši v podadresári, skontroluje veľkosť aktuálneho súboru.
        /// </summary>
        /// <param name="filesNumber">Počiatočné číslo súboru.</param>
        /// <param name="folder">Zložka.</param>
        public static void WaitForFile(int filesNumber, string folder)
        {
            WaitForFile(BaseConfiguration.MediumTimeout, filesNumber, folder, true);
        }

        /// <summary>
        /// Premenujte súbor a skontrolujte, či bol súbor premenovaný s daným časovým limitom, skráťi názov súboru v prípade potreby odstráňi "_".
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="oldName">Starý názov.</param>
        /// <param name="newName">Nový názov.</param>
        /// <param name="subFolder">Podpriečinok.</param>
        /// <returns>Vráti nový názov v prípade jeho skrátenia.</returns>
        public static string RenameFile(double waitTime, string oldName, string newName, string subFolder)
        {
            newName = NameHelper.ShortenFileName(subFolder, newName, "_", 255);

            if (File.Exists(newName))
            {
                File.Delete(newName);
            }

            // Use ProcessStartInfo class
            string command = "/c ren " + '\u0022' + oldName + '\u0022' + " " + '\u0022' + newName +
                             '\u0022';
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe")
            {
                WorkingDirectory = subFolder,
                Arguments = command
            };
            Thread.Sleep(1000);

            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting till file will be renamed {0}", subFolder);
            Process.Start(cmdsi);
            WaitHelper.Wait(() => File.Exists(subFolder + Separator + newName), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            return newName;
        }

        /// <summary>
        /// Skopíruje súbor a skontroluje, či bol súbor skopírovaný s daným časovým limitom, skráti názov súboru, ak je to potrebné, odstráňi "_".
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="oldName">Starý názov.</param>
        /// <param name="newName">Nový názov.</param>
        /// <param name="workingDirectory">Pracovný priečinok.</param>
        /// <returns>Nový názov.</returns>
        public static string CopyFile(double waitTime, string oldName, string newName, string workingDirectory)
        {
            return CopyFile(waitTime, oldName, newName, workingDirectory, workingDirectory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime">Čakanie.</param>
        /// <param name="oldName">Starý názov.</param>
        /// <param name="newName">Nový názov.</param>
        /// <param name="oldSubFolder">Starý subFolder</param>
        /// <param name="newSubFolder">Nový subFolder.</param>
        /// <returns>Nový názov.</returns>
        public static string CopyFile(double waitTime, string oldName, string newName, string oldSubFolder, string newSubFolder)
        {
            newName = NameHelper.ShortenFileName(newSubFolder, newName, "_", 255);

            if (File.Exists(newSubFolder + Separator + newName))
            {
                File.Delete(newSubFolder + Separator + newName);
            }

            // Use ProcessStartInfo class
            string command = "/c copy " + '\u0022' + oldName + '\u0022' + " " + '\u0022' + newSubFolder + Separator + newName +
                             '\u0022';
            ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe")
            {
                WorkingDirectory = oldSubFolder,
                Arguments = command
            };
            Thread.Sleep(1000);

            var timeoutMessage = string.Format(CultureInfo.CurrentCulture, "Waiting till file will be copied {0}", newSubFolder);
            Process.Start(cmdsi);
            WaitHelper.Wait(() => File.Exists(newSubFolder + Separator + newName), TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(1), timeoutMessage);
            return newName;
        }

        /// <summary>
        /// Premenuje súbor daného formátu a skontroluje, či bol súbor premenovaný, skráťi názov súboru v prípade potreby odstráňi "_".
        /// </summary>
        /// <param name="oldName">Starý názov.</param>
        /// <param name="newName">Nový názov.</param>
        /// <param name="subFolder">SubFolder.</param>
        /// <param name="format">Formát.</param>
        /// <returns>Nový názov.</returns>
        public static string RenameFile(string oldName, string newName, string subFolder, FileFormat format)
        {
            newName = newName + ReturnFileExtension(format);
            return RenameFile(BaseConfiguration.MediumTimeout, oldName, newName, subFolder);
        }

        /// <summary>
        /// Získa priečinok z app.config ako hodnotu daného kľúča.
        /// </summary>
        /// <param name="appConfigValue">Hodnota konfigurácie aplikácie.</param>
        /// <param name="currentFolder">Základný adresár.</param>
        /// <returns>
        /// Cesta k súboru.
        /// </returns>
        public static string GetFolder(string appConfigValue, string currentFolder)
        {           
            string folder;

            if (string.IsNullOrEmpty(appConfigValue))
            {
                folder = currentFolder;
            }
            else
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    folder = currentFolder + appConfigValue;
                }
                else
                {
                    folder = appConfigValue;
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            return folder;
        }

        /// <summary>
        /// Modifikácia app.config file.
        /// </summary>
        /// <param name="KeyName">Key hodnota.</param>
        /// <param name="KeyValue">Value hodnota.</param>
        public static void UpdateAppSettings(string KeyName, string KeyValue)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "appSettings")
                {
                    foreach (XmlNode xNode in xElement.ChildNodes)
                    {
                        if (xNode.Attributes != null)
                        {
                            if (xNode.Attributes[0].Value == KeyName)
                            {
                                xNode.Attributes[1].Value = KeyValue;
                            }
                        }
                    }
                }
            }
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        /// <summary>
        /// Vytvorenie relatívnej cesty
        /// </summary>
        /// <param name="absolutePath">Absolútna cesta</param>
        /// <param name="pivotFolder">Názov adresára</param>
        /// <returns>Relatívna cesta k súboru.</returns>
        public static string MakeRelativePath(string absolutePath, string pivotFolder, string startpath, int countSeg = 0)
        {
            string folder = pivotFolder;
            Uri pathUri = new Uri(absolutePath);

            foreach (var f in pathUri.Segments) countSeg++;
            string s = pathUri.Segments[countSeg - 1];
            return Path.Combine($"{startpath}\\{folder}", s);
        }
    }
}
