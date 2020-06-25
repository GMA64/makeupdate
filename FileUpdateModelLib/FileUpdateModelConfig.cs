using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUpdateModelLib
{
    public class FileUpdateModelConfig
    {
        public FileUpdateModelConfig(Arguments args)
        {
            this.Source = args.GetValue<string>("source");
            this.Destination = args.GetValue<string>("destination");
            this.Program = args.GetValue<string>("program");
            this.SkipVersionCheck = args.GetValue<bool>("skipversion");
            this.NoBackup = args.GetValue<bool>("nobackup");
            this.NoZip = args.GetValue<bool>("nozip");
            this.StartAfterUpdate = args.GetValue<bool>("start");
        }

        public string Source { get; set; }
        public string Destination { get; set; }
        public string Program { get; set; }

        public bool SkipVersionCheck { get; set; }
        public bool NoBackup { get; set; }
        public bool NoZip { get; set; }
        public bool StartAfterUpdate { get; set; }
    }
}
