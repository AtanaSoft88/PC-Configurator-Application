using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc.Configurator.Constants
{
    public static class GlobalConstants
    {
        public const string JSON_FILE_PATH = @"..//..//..//DataSets//pc-store-inventory.json";
        public const string ERROR = "ERROR: The selected configuration is not valid.";
        public const string CPU_RAM_INCOMPATIBLE = "{0}. {1} of type {2} is not compatible with the {3}";
        public const string CPU_MOTHERBOARD_INCOMPATIBLE = "{0}. {1} of type {2} is not compatible with the {3}";
        public const string ERROR_CHOOSE_DIFFERENT_COMPONENTS = "ERROR: Please choose different component types";
    }
}
