using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TQDataLoader.Data.Error;

namespace TQDataLoader.Code.Error
{
    public class ErrorHandler
    {
        private const string NO_FILES_DIRS_MESSAGE = "No directories of files found";
        private const string NOT_VALID_DIR_PATH = "Directory path is not valid";

        private List<ErrorItem> errors;

        public ErrorHandler() {
            errors = new List<ErrorItem>
            {
                new ErrorItem(ErrorTypeEnum.NoFilesDirectoriesFound, NO_FILES_DIRS_MESSAGE),
                new ErrorItem(ErrorTypeEnum.NotValidDirectoryPath, NOT_VALID_DIR_PATH)
            };
        }

        public ErrorItem GetError(ErrorTypeEnum errorType)
        {
            var error = errors.FirstOrDefault(x => x.ErrorType == errorType);

            if (error == null) throw new Exception("Error enum not defined in ErrorHandler");
            
            return error;
        }
    }
}
