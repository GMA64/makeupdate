using ArgumentsLib;
using System;
using System.Linq;
using UpdateLib;
using Xunit;

namespace UpdateLibTest
{
    public class UpdateTest
    {

        private string[] args =
        {
            "-enable",
            "-text",
            "Das ist ien Test"
        };


        [Fact]
        public void CreateReference_PassingTest()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @"./Marshaler",
                ModelPath = @"./Model",
                Model = "File",
                Schema = "enable,text*"
            };

            Update update = new Update(config, args);

            Assert.NotNull(update);
        }

        [Fact]
        public void CreateReferenceWithNullConfig_FailingTest()
        {
            Update update;

            Assert.Throws<NullReferenceException>(() => update = new Update(null, args));
        }

        [Fact]
        public void CreateReferenceWithNullArgs_FailingTest()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @"./Marshaler",
                ModelPath = @"./Model",
                Model = "File",
                Schema = "enable,text*"
            };
            Update update;

            Assert.Throws<ArgumentNullException>(() => update = new Update(config, null));
        }

        [Fact]
        public void CreatingReferenceWithWrongMarshalerPath()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = "",
                ModelPath = @"./Model",
                Model = "File",
                Schema = "enable,text*"
            };

            Update update;

            Assert.Throws<LibraryArgumentException>(() => update = new Update(config, args));
        }


        [Fact]
        public void CreatingReferenceWithWrongModelPath()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = "",
                Model = "File",
                Schema = "enable,text*"
            };

            Update update;

            Assert.Throws<LibraryException>(() => update = new Update(config, args));
        }

        [Fact]
        public void CreatingReferenceNoModel()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = "",
                Schema = "enable,text*"
            };

            Update update = new Update(config, args);

            Assert.NotNull(update);
        }

        [Fact]
        public void CreatingReferenceNoSchema()
        {
            UpdateConfig config = new UpdateConfig
            {
                MarshalerPath = @".\Marshaler",
                ModelPath = @".\Model",
                Model = "File",
                Schema = ""
            };

            Update update;

            Assert.Throws<LibraryArgumentException>(() => update = new Update(config, args));
        }
    }
}
