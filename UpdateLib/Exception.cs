using System;
using System.Collections.Generic;
using System.Text;
using UpdateModelLib;

namespace UpdateLib
{
    public class LibraryException : UpdateException
    {
        public LibraryException(ErrorCode errorCode, string errorParameter) : base(errorCode, errorParameter) { }

        public override string ErrorMessage()
        {
            switch (base.ErrorCode)
            {
                case ErrorCode.OK:
                    return "TILT: Should not be reached!";
                case ErrorCode.GLOBAL:
                    return $"There was an Error with '{base.ErrorParameter}'";
                case ErrorCode.IVALID_MODEL:
                    return $"Model Error '{base.ErrorParameter}' unexpected";
                default:
                    return string.Empty;
            }
        }
    }
}
