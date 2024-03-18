namespace Cwiczenia3;

abstract class ContainerObj
{
    private double GrossWeight { get; set; }
    private double NetWeight { get; set; }
    private double Height { get; set; }
    private double Depth { get; set; }
    private string serialNum { get; set; }

    protected ContainerObj(double grossWeight, double netWeight, double height, double depth)
    {
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Height = height;
        Depth = depth;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Out.Write("Test");
    }
}