using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Exceptions
{
    public class FilesizeTooLarge : Exception
    {
        public FilesizeTooLarge()
        {
        }

        public FilesizeTooLarge(string message) : base(message)
        {
        }
    }
}
