namespace Cwiczenia3;

public abstract class Container
{
    public double GrossWeight { get; set; }
    public double NetWeight { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }

    protected Container(double grossWeight, double height, double netWeight, double depth)
    {
        GrossWeight = grossWeight;
        Height = height;
        NetWeight = netWeight;
        Depth = depth;
    }

    public static void Main(string[] args)
    {
        Console.Out.Write("asdasd");
    }
}