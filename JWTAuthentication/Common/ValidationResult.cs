using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Common
{
    public class ValidationResult
    {
        public HandleException Exception { get; set; }
        public ValidationResultState State { get; set; }
        public object Entity { get; set; }
        public ValidationResult(string errorMessage)
        {
            Exception = new HandleException(errorMessage);
            State = ValidationResultState.HasError;
        }

        public ValidationResult(string errorMessage, string exceptionHint)
        {
            Exception = new HandleException(errorMessage, exceptionHint);
            State = ValidationResultState.HasError;
        }

        public ValidationResult(string errorMessage, int exceptionNo)
        {
            Exception = new HandleException(errorMessage, exceptionNo);
            State = ValidationResultState.HasError;
        }

        public ValidationResult()
        {
            State = ValidationResultState.IsValid;
        }

        public bool IsOk()
        {
            return State == ValidationResultState.IsValid;
        }
    }

    public enum ValidationResultState
    {
        IsValid = 100,
        HasError = 101
    }
}
