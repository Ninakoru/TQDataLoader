using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Code
{
    public static class GlobalConstants
    {
        public const char COMMA_CHAR = ',';
        public const char QUOTE_CHAR = '"';

        public const string EMPTY_STRING = "";
        public const string SPACE_STRING = " ";
        public const string SEMICOLON_STRING = ";";
        public const string COMMA_STRING = ",";
        public const string CSV_SEPARATOR = COMMA_STRING;
        public const string DIRECTORY_SEPARATOR = "\\";
        public const string DBR_FILE_EXTENSION = ".dbr";
        public const string CSV_FILE_EXTENSION = ".csv";
        public const string TEXT_FILE_EXTENSION = ".txt";
        public const string OPENING_BRACKET_STRING = "{";
        public const string CLOSING_BRACKET_STRING = "}";

        public const string TEMPLATE_KEYNAME = "templateName";
        public const string GAME_DESCRIPTION_KEY = "gameDescription";
        public const string FILE_NAME_KEY = "fileName";
        public const string PATH_KEY = "path";
        public const string MONSTER_REF_KEY = "monsterRef";
    }
}
