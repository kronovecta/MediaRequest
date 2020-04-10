using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Exceptions
{
    public class FileTypeNotAllowedException : Exception
    {
        public FileTypeNotAllowedException()
        {
        }

        public FileTypeNotAllowedException(string message) : base(message)
        {
        }
    }
}
