namespace Cwiczenia3;

public abstract class Container
{
    private double GrossWeight { get; set; } // Waga kontener + towar.
    private double NetWeight { get; set; } // Waga kontenera.
    private double Height { get; set; }
    private double Depth { get; set; }
    private string serialNum { get; set; }

    protected Container(double grossWeight, double netWeight, double height, double depth)
    {
        GrossWeight = grossWeight;
        NetWeight = netWeight;
        Height = height;
        Depth = depth;
    }

    protected void emptyContainer()
    {
        GrossWeight -= NetWeight;
    }

    protected void loadContainer(double WeightToLoad)
    {
        GrossWeight = NetWeight + WeightToLoad;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Out.Write("Test");
    }
}