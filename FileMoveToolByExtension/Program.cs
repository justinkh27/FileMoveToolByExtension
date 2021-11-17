using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FileMoveToolByExtension // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", false, true)
                      .Build();

            string fileExtension = configuration["FileFormat"];

            #region designate directories and make backup

            Console.WriteLine("Choose Directory to backup");
            var DirToBackup = Console.ReadLine();
            Console.WriteLine("Creating back up folder on desktop");
            //make back up folder on logged in user desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var backupFolderPath = desktopPath + @"\FileBackup";

            MakeBackUpDir(backupFolderPath, DirToBackup);

            #endregion

            #region Enter search path for files
            // Backup created, now need to enter search path for images, and the directory to copy them
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Enter path to search for files");
            string RootPath = Console.ReadLine();
            Console.WriteLine("Enter path to copy file");
            string DestinationPath = Console.ReadLine();
            Console.WriteLine("\n");
            #endregion

            //Function to search for files and copy them to new DIR
            var userFiles = Directory.GetFiles(RootPath, fileExtension, SearchOption.AllDirectories);

            Directory.CreateDirectory(DestinationPath);

            foreach (var file in userFiles)
            {
                var fileName = Path.GetFileName(file);
                Console.WriteLine(fileName);
                Console.WriteLine("Copying...\n");
                var newFileCreation = Path.Combine(DestinationPath, fileName);
                Console.WriteLine(newFileCreation);

                System.IO.File.Copy(file, newFileCreation, true);

            }

            var DestFiles = Directory.GetFiles(DestinationPath, fileExtension, SearchOption.AllDirectories);
            foreach (var item in DestFiles)
            {
                var fileName = Path.GetFileName(item);
                Console.WriteLine(fileName);
            }
        }

        #region Robocopy to create MIRROR directory for backup. 
        public static void MakeBackUpDir(string backupDir, string DirToBackup)
        {

            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            ps.CreateNoWindow = true;
            ps.Arguments = @"/k robocopy " + DirToBackup + " " + backupDir + " /mir";
            Process.Start(ps);
        }
        #endregion

    }
}