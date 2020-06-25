using ArgumentsLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace VersionChecker
{
    public class VersionCheck
    {
       

        public Arguments Arguments { get; set; }
        public class UpdateModelConfig
        {
            public UpdateModelConfig(Arguments args)
            {
                this.Source = args.GetValue<string>("source");
                this.Destination = args.GetValue<string>("destination");
                this.Program = args.GetValue<string>("program");
            }

            public string Source { get; set; }
            public string Destination { get; set; }
            public string Program { get; set; }

            //public bool SkipVersionCheck { get; set; }
            //public  bool NoBackup { get; set; }
            //public bool NoZip { get; set; }
            //public bool SartAfterUpdate { get; set; }

        }
    }
}
