﻿using ArgumentsLib;
using System;
using System.Collections.Generic;

namespace UpdateModelLib
{
    public delegate void WriteLine(object o);

    public abstract class UpdateModel
    {
        public abstract event WriteLine UpdateMessage;
        private string model;

        public abstract void BeforeUpdate();
        public abstract void Update();
        public abstract void AfterUpdate();

        public Arguments Arguments { get; set; }
        public string Model { get => this.model; set => this.model = value.Trim().ToLower(); }
    }
}
