using ArgumentsLib;
using FileUpdateModelLib;
using System;
using System.Security.Cryptography.X509Certificates;
using UpdateModelLib;
using Xunit;
using static FileUpdateModelLib.FileUpdateModel;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModulTest
    {
        private string[] args =
{
            "-enable",
            "-text",
            "Das ist ien Test"
        };

        //Arguments args = new Arguments()

        [Fact]
        public void FileUpdateModelSetNewModel_PassingTest()
        {
            FileUpdateModel update = new FileUpdateModel();

            Assert.Equal("file", update.Model);
        }

        [Fact]
        public void FileUpdateModelSetNewModel_FailingTest()
        {
            FileUpdateModel update = new FileUpdateModel();

            Assert.Throws<System.NullReferenceException>(() => update.Model = null);
        }

        [Fact]
        public void CheckingVerforeUpdate_PassingTest()
        {
            //FileUpdateModelConfig config = new FileUpdateModelConfig(/*args*/);
            FileUpdateModel update = new FileUpdateModel();

        }


    }
}
