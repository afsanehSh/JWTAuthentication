using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Common
{
    public class HandleException: Exception
    {
        public string Message { get; set; }
        public string ExceptionHint { get; set; }
        public int? ExceptionNo { get; set; }

        public HandleException()
        {

        }

        public HandleException(string message)
        {
            Message = message;
        }

        public HandleException(string message, string exceptionHint)
        {
            Message = message;
            ExceptionHint = exceptionHint;
        }

        public HandleException(string message, int exceptionNo)
        {
            Message = message;
            ExceptionNo = exceptionNo;
        }
    }
}
