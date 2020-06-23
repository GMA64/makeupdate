using FileUpdateModelLib;
using System;
using System.Security.Cryptography.X509Certificates;
using UpdateModelLib;
using Xunit;

namespace FileUpdateModelLibTest
{
    public class FileUpdateModul
    {
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
        public void FileUpdateModelExecuteBeforeUpdate_PassingTest()
        {
            FileUpdateModel update = new FileUpdateModel();            
        }

    }
}
