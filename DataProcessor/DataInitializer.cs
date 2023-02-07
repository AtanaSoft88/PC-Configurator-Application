using Newtonsoft.Json;
using Pc.Configurator.PartsExtractor;
using Pc.Configurator.Constants;

namespace Pc.Configurator.DataProcessor
{
    public static class DataInitializer
    {
        public static ComputerParts GetComponentsAsObj()
        {            
            var jsonItems = File.ReadAllText(GlobalConstants.JSON_FILE_PATH);            
            var components = JsonConvert.DeserializeObject<ComputerParts>(jsonItems);
            return components;
        }
    }
}
