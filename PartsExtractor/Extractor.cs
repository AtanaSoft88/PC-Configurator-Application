namespace Pc.Configurator.PartsExtractor
{
    public static class Extractor
    {
        public static List<CPU> GetAllCPU(ComputerParts parts)
        {
            var cpus = new List<CPU>();
            foreach (var cpuItem in parts.CPUs)
            {
                CPU cpu = new CPU()
                {
                    ComponentType = cpuItem.ComponentType,
                    PartNumber = cpuItem.PartNumber,
                    Name = cpuItem.Name,
                    SupportedMemory = cpuItem.SupportedMemory,
                    Socket = cpuItem.Socket,
                    Price = cpuItem.Price,
                };
                cpus.Add(cpu);
            }
            return cpus;
        }

        public static List<Memory> GetAllRams(ComputerParts parts) 
        {
            var rams = new List<Memory>();
            foreach (var memoryItem in parts.Memory)
            {
                Memory memory = new Memory()
                {
                    ComponentType = memoryItem.ComponentType,
                    PartNumber = memoryItem.PartNumber,
                    Name = memoryItem.Name,
                    Type = memoryItem.Type,
                    Price = memoryItem.Price,
                };
                rams.Add(memory);

            }
            return rams;
        }

        public static List<Motherboard> GetAllMotherBoards(ComputerParts parts) 
        {
            var motherboards = new List<Motherboard>();
            foreach (var mbItem in parts.Motherboards)
            {
                Motherboard motherboard = new Motherboard()
                {
                    ComponentType = mbItem.ComponentType,
                    PartNumber = mbItem.PartNumber,
                    Name = mbItem.Name,
                    Socket = mbItem.Socket,
                    Price = mbItem.Price,
                };
                motherboards.Add(motherboard);

            }
            return motherboards;
        }
    }
}
