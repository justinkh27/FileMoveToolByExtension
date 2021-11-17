using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMoveToolByExtension
{
    public class FileServices
    {
        public string Designate_Directory()
        {
            string directory_source = "";
            string directory_target = "";

            Console.WriteLine("Enter directory to backup and search files");
            directory_source = Console.ReadLine();

            return directory_source;
        }

        public string Target_Directory()
        {
            string directory_target = "";

            Console.WriteLine("Enter directory to move files");
            directory_target = Console.ReadLine();

            if (!Directory.Exists(directory_target))
            {
                Directory.CreateDirectory(directory_target);
            }

            return directory_target;
        }


        public async Task Directory_Backup(string directory_source)
        {

            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            string directory_target = directory_source + @"\DataBackup";
            ps.CreateNoWindow = true;
            ps.Arguments = @"/k robocopy " + directory_source + " " + directory_target;

            Process.Start(ps);
            await Task.Delay(2000);
            Console.WriteLine("Task Completed");
        }

        public List<string> FileSearch(string directory_source,string file_extension)
        {
            var userFiles = Directory.GetFiles(directory_source, file_extension, SearchOption.TopDirectoryOnly);
            return userFiles.ToList();
        }

        public void CopyFiles(List<string> userFiles, string directory_target)
        {
            foreach (var file in userFiles)
            {
                var fileName = Path.GetFileName(file);
                Console.WriteLine("Copying " + fileName + "...\n");
                var newFileCreation = Path.Combine(directory_target, fileName);
                Console.WriteLine("Copying to: " +  newFileCreation);

                System.IO.File.Copy(file, newFileCreation, true);

            }

            
        }


    }

}

