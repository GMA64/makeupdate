using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileUpdateModelLib
{
    class FileUpdateModelUpdater
    {
        private FileUpdateModelConfig config;
     
        public FileUpdateModelUpdater(Arguments args)
        {
            config = new FileUpdateModelConfig(args);
        }

        public void ClearFolders()
        {
            DirectoryInfo dir = new DirectoryInfo(this.config.Destination);

            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
            }
            else
            {
                throw new DriveNotFoundException();
            }
        }
        public void CopyProgramToFolder()
        {
            DirectoryInfo dir = new DirectoryInfo(this.config.Source);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("The source Directory does not exist or could not be found " + this.config.Source);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(this.config.Destination, file.Name);
                file.CopyTo(temppath, true);
            }
        }
    }
}
