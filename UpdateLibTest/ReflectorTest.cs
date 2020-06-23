using ArgumentsLib;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateLib;
using Xunit;

namespace UpdateLibTest
{
    public class ReflectorTest
    {
        private string Path = @".\Model";
        private UpdateLib.Reflector reflector;

        [Fact]
        public void CreatReference_PassingTest()
        {
            UpdateLib.Reflector reflector = new UpdateLib.Reflector(this.Path);

            Assert.NotNull(reflector);
        }

        [Fact]
        public void CreateReferenceWithNoPath()
        {
            UpdateLib.Reflector reflector;

            Assert.Throws<LibraryException>(() => reflector = new UpdateLib.Reflector(string.Empty));
        }

        [Fact]
        public void CreateRefereceWithNoDLLs()
        {
            Assert.Throws<LibraryException>(() => this.reflector = new UpdateLib.Reflector(@"..\"));
        }

        [Fact]
        public void GetInstance_PassingTest()
        {
            this.reflector = new UpdateLib.Reflector(Path);
            Assert.NotNull(this.reflector.GetInstance("file"));
        }

        [Fact]
        public void GetInstance_FailingTest()
        {
            this.reflector = new UpdateLib.Reflector(Path);
            Assert.Throws<ArgumentNullException>(() => this.reflector.GetInstance(string.Empty));
        }

        [Fact]
        public void GetInstanceWithWrongModel()
        {
            this.reflector = new UpdateLib.Reflector(Path);
            Assert.Throws<ArgumentNullException>(() => this.reflector.GetInstance("bliblablub"));
        }
    }
}
