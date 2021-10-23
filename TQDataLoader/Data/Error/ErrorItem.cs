using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Data.Error
{
    public class ErrorItem
    {
        public ErrorTypeEnum ErrorType { get; private set; }

        public string ErrorMessage { get; private set; }

        public ErrorItem(ErrorTypeEnum errorType, string errorMessage)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }
    }
}
