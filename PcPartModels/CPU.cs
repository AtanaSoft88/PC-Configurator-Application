namespace Pc.Configurator.PartsExtractor;
public class CPU
{
    public string ComponentType { get;set; } = null!;
    public string PartNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Socket { get; set; } = null!;
    public string SupportedMemory { get; set; } = null!;
    public decimal Price { get; set; }
}

