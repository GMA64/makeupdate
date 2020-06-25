using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using UpdateModelLib;

namespace FileUpdateModelLib
{
    public class FileUpdateModel : UpdateModel
    {
        public override event WriteLine UpdateMessage;
        private const string model = "File";
        private readonly string tempDirectory;
        private FileUpdateModelConfig config;
        private FileUpdateVersionCheck check;
        private FileUpdateModelUpdater updater;
        private FileUpdateModelMakeBackup backup;

        public FileUpdateModel()
        {
            base.Model = model;
            this.tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        public override void Loadagruments()
        {
            this.updater = new FileUpdateModelUpdater(base.Arguments);
            this.check = new FileUpdateVersionCheck(base.Arguments);
            this.config = new FileUpdateModelConfig(base.Arguments);
            this.backup = new FileUpdateModelMakeBackup(base.Arguments);
        }

        public override void BeforeUpdate()
        {
            this.UpdateMessage("Before Update");

            if (!this.config.NoZip)
            {
                this.UpdateMessage("Begin to Unpack the zip file");
                UnpackSource();
            }

            // check data class aufrufen
            if (!this.config.SkipVersionCheck)
            {
                this.UpdateMessage("Start to check if there is a Newer Version");
                this.check.CheckVersion();
            }

            if (!this.config.NoBackup)
            {
                this.UpdateMessage("Start to make an Update");
                this.backup.MakeBackup();
            }
        }

        private void UnpackSource()
        {
            Directory.CreateDirectory(this.tempDirectory);
            ZipFile.ExtractToDirectory(this.config.Source, this.tempDirectory);
        }

        public override void Update()
        {
            this.UpdateMessage("Update");

            this.UpdateMessage("Clearing the destination folder");
            this.updater.ClearFolders();

            this.UpdateMessage("Copy new files to destination folder");
            this.updater.CopyProgramToFolder();
        }
       
        public override void AfterUpdate()
        {
            this.UpdateMessage("After Update");

            this.UpdateMessage("Clearing Backup");
            this.backup.ClearBackup();
        }

    }
}