using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomLibrary.ErrorHandling
{
    public class CustomExceptions
    {
        //預設自定義Exception
        public class AppException : Exception
        {
            public AppException(string message) : base(message)
            {
            }
        }
    }
}
