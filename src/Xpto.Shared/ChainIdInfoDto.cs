namespace Xpto.Shared;

public class ChainIdInfoDto
{
    public string Name { get; set; }
    public long ChainId { get; set; }
    public NativeCurrency NativeCurrency { get; set; }
}

public class NativeCurrency
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public int Decimals { get; set; }
}


