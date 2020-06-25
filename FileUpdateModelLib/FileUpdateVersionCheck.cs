using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UpdateModelLib;

namespace FileUpdateModelLib
{
    public class FileUpdateVersionCheck
    {
        private readonly string tempDirectory;
        private FileUpdateModelConfig config;


        public FileUpdateVersionCheck(ArgumentsLib.Arguments args)
        {
            config = new FileUpdateModelConfig(args);
        }

        public void CheckVersion()
        {
            if (CheckVersionFile())
                return;
            if (CheckVersionProgram())
                return;
        }

        private bool CheckVersionFile()
        {
            string[] files = Directory.GetFiles(this.config.Destination).Where(s => s.EndsWith(".txt") || s.EndsWith(".version")).ToArray();

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.ToLower() == "version.txt" || file.ToLower() == "version.version")
                    {
                        string[] sourceVersion;
                        string[] destinationversion;

                        using (StreamReader reader = new StreamReader(this.config.Destination + @"\version.*"))
                        {
                            // VersionsCheck druchführen
                            destinationversion = reader.ReadToEnd().Split('.');
                        }
                        
                        string path;

                        if (this.config.NoZip)
                            path = this.config.Source;

                        else
                            path = this.tempDirectory;

                        using (StreamReader reader = new StreamReader(path + @"\version.*"))
                        {
                            // VersionsCheck druchführen
                            sourceVersion = reader.ReadToEnd().Split('.');
                        }

                        if (destinationversion.Length != sourceVersion.Length)
                            throw new Exception();

                        // Source       Version 1.0.1
                        // Destination  Version 2.0.1

                        for (int i = 0; i < destinationversion.Length; i++)
                        {
                            if (int.Parse(sourceVersion[i]) > int.Parse(destinationversion[i]))
                            {
                                return true;
                            }
                        }

                        throw new Exception("Source is older than Destination");
                    }
                }
            }
            return false;
        }

        private bool CheckVersionProgram()
        {
            string[] sourceOutput;
            string[] destinationOutput;

            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(this.config.Destination, this.config.Program),
                    Arguments = "version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                };

                proc.Start();
                destinationOutput = proc.StandardOutput.ReadToEnd().Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(" ", string.Empty).Split('.');
            }

            if (destinationOutput == null || destinationOutput.Length == 0)
                return false;


            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(this.config.Source, this.config.Program),
                    Arguments = "version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                proc.Start();
                sourceOutput = proc.StandardOutput.ReadToEnd().Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(" ", string.Empty).Split('.');
            }

            if (sourceOutput == null)
                throw new Exception();

            if (sourceOutput.Length != destinationOutput.Length)
                throw new Exception();

            for (int i = 0; i < destinationOutput.Length; i++)
            {
                if (int.Parse(sourceOutput[i]) > int.Parse(destinationOutput[i]))
                {
                    return true;
                }
            }

            throw new Exception("Source is older than Destination");
        }
    }
}
