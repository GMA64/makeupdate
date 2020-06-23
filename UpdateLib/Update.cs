using ArgumentsLib;
using System;
using System.Collections.Generic;
using UpdateModelLib;

namespace UpdateLib
{
    public class Update : IDisposable
    {
        public event WriteLine UpdateMessage;

        private UpdateConfig config;
        private Arguments arguments;
        private UpdateModel model;
        private Reflector reflector;

        public Update(UpdateConfig config, IEnumerable<string> args)
        {
            this.arguments = new Arguments(config.MarshalerPath, config.Schema, args);
            this.config = config;
            this.reflector = new Reflector(config.ModelPath);
        }

        public void ExecuteUpdate()
        {
            UpdateMessage?.Invoke("Trying to load update type!");

            LoadUpdateModel();
            ExecuteUpdateModel();
        }

        private void LoadUpdateModel()
        {
            //Reflection
            if (!string.IsNullOrWhiteSpace(config.Model))
                model = reflector.GetInstance(this.config.Model);
            else
                model = reflector.GetInstance(this.arguments.GetValue<string>("using"));

            model.UpdateMessage += this.UpdateMessage;
            model.Arguments = arguments;

        }

        private void ExecuteUpdateModel()
        {
            model.BeforeUpdate();
            model.Update();
            model.AfterUpdate();
        }

        public void Dispose()
        {
            model.UpdateMessage -= this.UpdateMessage;
        }
    }
}
