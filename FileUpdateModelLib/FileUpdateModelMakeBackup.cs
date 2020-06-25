using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace FileUpdateModelLib
{
    public class FileUpdateModelMakeBackup
    {
        private string backupPath;
        private FileUpdateModelConfig config;
        public FileUpdateModelMakeBackup(Arguments args)
        {
            config = new FileUpdateModelConfig(args);
        }

        public void MakeBackup()
        {
            MakeNewFolderForBackup();
            CopyFolder(this.backupPath, this.config.Destination);
        }

        public void LoadBackup()
        {
            CopyFolder(this.config.Destination, this.backupPath);

        }

        public void ClearBackup()
        {
            DirectoryInfo dir = new DirectoryInfo(this.backupPath);
            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }

                Directory.Delete(this.backupPath);
            }
            else
                throw new DirectoryNotFoundException("Directory does not exist or could not be found" + this.backupPath);
        }

        #region MakeBackup
        private void MakeNewFolderForBackup()
        {
            this.backupPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (File.Exists(backupPath))
                ClearBackupFolder();
            else
                Directory.CreateDirectory(backupPath);
        }

        private void ClearBackupFolder()
        {
            DirectoryInfo dir = new DirectoryInfo(this.backupPath);

            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        #endregion
        private void CopyFolder(string destinationFolder, string sourceFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceFolder);

            if (!dir.Exists)
                throw new DirectoryNotFoundException("Directory does not exist");

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destinationFolder, file.Name);
                file.CopyTo(temppath, true);
            }
        }
    }
}
