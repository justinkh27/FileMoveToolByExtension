using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FileMoveToolByExtension
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", false, true)
                      .Build();

            string file_extension = configuration["FileFormat"];

            FileServices FS = new FileServices();

            string directory_source = FS.Designate_Directory();
            string directory_target = FS.Target_Directory();
            await FS.Directory_Backup(directory_source);

            var userFiles = FS.FileSearch(directory_source, file_extension);
            FS.CopyFiles(userFiles, directory_target);

        }
       
    }

}
