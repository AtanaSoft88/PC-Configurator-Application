using Pc.Configurator.Constants;
using Pc.Configurator.DataProcessor;
using Pc.Configurator.PartsExtractor;
using System.Text;
namespace Pc.Configurator
{
    public static class PcConfigurator
    {
        public static void Configure(Queue<string> input)
        {
            ComputerParts parts = DataInitializer.GetComponentsAsObj();
            List<CPU> cpus = Extractor.GetAllCPU(parts);
            List<Memory> rams = Extractor.GetAllRams(parts);
            List<Motherboard> mbs = Extractor.GetAllMotherBoards(parts);

            while (input.Count() != 0)
            {
                decimal sumPrice = 0.0M;
                StringBuilder sb = new StringBuilder();
                string partNumber = input.Dequeue();
                Console.WriteLine($"Please enter part number(s): {partNumber}");
                if (partNumber.Split(", ").Count() == 3)
                {
                    string cpuSn = partNumber.Split(", ")[0];
                    CPU currentCpu = cpus.FirstOrDefault(c => c.PartNumber == cpuSn);

                    string mbSn = partNumber.Split(", ")[1];
                    Motherboard currentMb = mbs.FirstOrDefault(c => c.PartNumber == mbSn);

                    string ramSn = partNumber.Split(", ")[2];
                    Memory currentRam = rams.FirstOrDefault(c => c.PartNumber == ramSn);

                    bool isCpuAndRamCompatible = true;
                    bool isCpuAndMbCompatible = true;

                    if (currentCpu != null && currentRam != null && currentMb != null)
                    {
                        if (currentCpu.Socket == currentMb.Socket && currentCpu.SupportedMemory == currentRam.Type)
                        {
                            sb.AppendLine($"\t{currentCpu.ComponentType} - {currentCpu.Name} –{currentCpu.Socket}, {currentCpu.SupportedMemory}");
                            sb.AppendLine($"\t{currentMb.ComponentType} – {currentMb.Name} - {currentMb.Socket}");
                            sb.AppendLine($"\t{currentRam.ComponentType} – {currentRam.Name} - {currentRam.Type}");
                            sumPrice = currentCpu.Price + currentRam.Price + currentMb.Price;
                            sb.AppendLine($"\tPrice: {sumPrice:f0}");
                        }
                        else
                        {
                            if (currentCpu.Socket != currentMb.Socket)
                            {
                                isCpuAndMbCompatible = false;
                            }
                            if (currentCpu.SupportedMemory != currentRam.Type)
                            {
                                isCpuAndRamCompatible = false;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(GlobalConstants.ERROR_CHOOSE_DIFFERENT_COMPONENTS);
                        Console.WriteLine(new String('*', 60));
                        continue;
                    }
                    int count = 1;
                    if (!isCpuAndMbCompatible || !isCpuAndRamCompatible)
                    {
                        sb.AppendLine(GlobalConstants.ERROR);
                        if (isCpuAndRamCompatible == false)
                        {
                            sb.AppendLine(String.Format(GlobalConstants.CPU_RAM_INCOMPATIBLE,
                                                                                            count,
                                                                                            currentRam.ComponentType,
                                                                                            currentRam.Type, 
                                                                                            currentCpu.ComponentType));

                        }
                        if (isCpuAndMbCompatible == false)
                        {
                            sb.AppendLine(String.Format(GlobalConstants.CPU_MOTHERBOARD_INCOMPATIBLE,
                                                                                                    (++count),
                                                                                                    currentMb.ComponentType,
                                                                                                    currentMb.Socket, 
                                                                                                    currentCpu.ComponentType));
                        }
                    }
                }
                else  // Components type < 3
                {
                    int combinations = 1;
                    Queue<CPU> collectionCpus = new Queue<CPU>();
                    Queue<Motherboard> collectionMbs = new Queue<Motherboard>();
                    Queue<Memory> collectionMemory = new Queue<Memory>();
                    var components = partNumber.Split(", ");
                    for (int i = 0; i < components.Length; i++)
                    {
                        string component = components[i];
                        CPU currentCpu = cpus.FirstOrDefault(c => c.PartNumber == component);
                        Motherboard currentMb = mbs.FirstOrDefault(c => c.PartNumber == component);
                        Memory currentRam = rams.FirstOrDefault(c => c.PartNumber == component);
                        if (components.Length == 2 && currentCpu == null && currentRam == null || currentCpu==null && currentMb == null || currentMb == null  && currentRam == null)
                             
                        {
                            Console.WriteLine(GlobalConstants.ERROR_CHOOSE_DIFFERENT_COMPONENTS);
                            Console.WriteLine(new String('*', 60));
                            break;
                        }
                        if (currentCpu != null) // if cpu is given
                        {
                            foreach (var motherboard in mbs)
                            {
                                if (motherboard.Socket == currentCpu.Socket)
                                {
                                    collectionMbs.Enqueue(motherboard);
                                }
                            }

                            foreach (var memory in rams)
                            {
                                if (memory.Type == currentCpu.SupportedMemory)
                                {
                                    collectionMemory.Enqueue(memory);
                                }
                            }
                            int totalCombinations = collectionMemory.Count() >= collectionMbs.Count() ? collectionMemory.Count() : collectionMbs.Count();

                            sb.AppendLine($"There are {totalCombinations} possible combinations:");
                            while (totalCombinations > 0)
                            {
                                sb.AppendLine($"Combination {combinations++}");
                                sb.AppendLine($"\t{currentCpu.ComponentType} - {currentCpu.Name} –{currentCpu.Socket}, {currentCpu.SupportedMemory}");
                                sb.AppendLine($"\t{collectionMbs.Peek().ComponentType} – {collectionMbs.Peek().Name} - {collectionMbs.Peek().Socket}");
                                sb.AppendLine($"\t{collectionMemory.Peek().ComponentType} – {collectionMemory.Peek().Name} - {collectionMemory.Peek().Type}");
                                sumPrice = currentCpu.Price + collectionMbs.Peek().Price + collectionMemory.Peek().Price;
                                sb.AppendLine($"\tPrice: {sumPrice:f0}");
                                collectionMbs.Dequeue();
                                collectionMemory.Dequeue();
                                totalCombinations--;
                            }
                            combinations = 1;
                        }
                        if (currentMb != null) // if Motherboard is given
                        {

                            foreach (var cpu in cpus)
                            {
                                if (cpu.Socket == currentMb.Socket)
                                {
                                    if (currentCpu == null)
                                    {
                                        currentCpu = cpu;
                                    }
                                    collectionCpus.Enqueue(cpu);
                                }
                            }
                            foreach (var memory in rams)
                            {
                                if (memory.Type == currentCpu.SupportedMemory)
                                {
                                    collectionMemory.Enqueue(memory);
                                }
                            }
                            int totalCombinations = collectionMemory.Count() >= collectionMbs.Count() ? collectionMemory.Count() : collectionMbs.Count();

                            sb.AppendLine($"There are {totalCombinations} possible combinations:");
                            while (totalCombinations > 0)
                            {
                                sb.AppendLine($"Combination {combinations++}");
                                sb.AppendLine($"\t{collectionCpus.Peek().ComponentType} - {collectionCpus.Peek().Name} –{collectionCpus.Peek().Socket}, {currentCpu.SupportedMemory}");
                                sb.AppendLine($"\t{currentMb.ComponentType} – {currentMb.Name} - {currentMb.Socket}");
                                sb.AppendLine($"\t{collectionMemory.Peek().ComponentType} – {collectionMemory.Peek().Name} - {collectionMemory.Peek().Type}");
                                sumPrice = collectionCpus.Peek().Price + currentMb.Price + collectionMemory.Peek().Price;
                                sb.AppendLine($"\tPrice: {sumPrice:f0}");
                                collectionCpus.Dequeue();
                                collectionMemory.Dequeue();
                                totalCombinations--;
                            }
                            combinations = 1;
                        }
                        if (currentRam != null) // if Memory is given 
                        {
                            var setSocketsCpu = new HashSet<string>();

                            foreach (var cpu in cpus)
                            {
                                if (cpu.SupportedMemory == currentRam.Type)
                                {
                                    collectionCpus.Enqueue(cpu);
                                    setSocketsCpu.Add(cpu.Socket);
                                }
                            }
                            foreach (var mb in mbs.OrderByDescending(x => x.Socket))
                            {
                                if (setSocketsCpu.Contains(mb.Socket))
                                {
                                    collectionMbs.Enqueue(mb);
                                }

                            }
                            int totalCombinations = collectionCpus.Count() >= collectionMbs.Count() ? collectionCpus.Count() : collectionMbs.Count();

                            sb.AppendLine($"There are {totalCombinations} possible combinations:");
                            while (totalCombinations > 0)
                            {
                                sb.AppendLine($"Combination {combinations++}");
                                sb.AppendLine($"\t{collectionCpus.Peek().ComponentType} - {collectionCpus.Peek().Name} –{collectionCpus.Peek().Socket}, {collectionCpus.Peek().SupportedMemory}");
                                sb.AppendLine($"\t{collectionMbs.Peek().ComponentType} – {collectionMbs.Peek().Name} - {collectionMbs.Peek().Socket}");
                                sb.AppendLine($"\t{currentRam.ComponentType} – {currentRam.Name} - {currentRam.Type}");
                                sumPrice = collectionCpus.Peek().Price + collectionMbs.Peek().Price + currentRam.Price;
                                sb.AppendLine($"\tPrice: {sumPrice:f0}");
                                collectionCpus.Dequeue();
                                collectionMbs.Dequeue();
                                totalCombinations--;
                            }
                            combinations = 1;
                        }
                    }
                }
                Console.WriteLine(sb.ToString().TrimEnd());
                Console.WriteLine(new String('*', 60));
            }
        }
    }
}
