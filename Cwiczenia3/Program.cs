using System.Text;

namespace Cwiczenia3;

public class OverfillException : Exception
{
    public OverfillException(string? message) : base(message)
    {
    }
}

public abstract class Container
{
    public static HashSet<int> SerialSet { get; set; } = new HashSet<int>(); // .
    public double ContainerWeight { get; set; } // Waga kontenera (kg).
    public double LoadWeight { get; set; } // Waga towaru (kg).
    public double MaxLoad { get; set; } // Maksymalna ładowność (kg).
    public double Height { get; set; } // Wysokość kontenera (cm).
    public double Depth { get; set; } // Głębokość kontenera (cm).
    public string SerialNum { get; set; } // Numer Seryjny.

    protected Container(double containerWeight, double loadWeight, double maxLoad, double height, double depth)
    {
        if (loadWeight > maxLoad)
        {
            throw new OverfillException("Masa ładunku, nie może przekraczać maksymalnego ładunku kontenera!");
        }

        ContainerWeight = containerWeight;
        LoadWeight = loadWeight;
        MaxLoad = maxLoad;
        Height = height;
        Depth = depth;
    }


    public abstract void LoadContainer(double WeightToLoad);

    protected void GenerateAndSetSerialNumber(string suffix)
    {
        StringBuilder sb = new StringBuilder();
        var random = new Random();
        var next = random.Next();

        while (SerialSet.Contains(next))
        {
            next = random.Next();
        }

        sb.Append(SerialNum).Append(suffix).Append(next);
        SerialSet.Add(next);
        SerialNum = sb.ToString();
    }

    protected void EmptyContainer()
    {
        LoadWeight = 0;
    }
}

public class LiquidContainer : Container
{
    public LiquidContainer(double containerWeight, double loadWeight, double maxLoad, double height, double depth) :
        base(containerWeight, loadWeight, maxLoad, height, depth)
    {
        GenerateAndSetSerialNumber("-L-");
    }

    public override void LoadContainer(double WeightToLoad)
    {
        // dla niebezpiecznego ładunku
        if (LoadWeight + WeightToLoad <= MaxLoad * 0.5)
        {
            
        }
        else
        {
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var liquidContainer = new LiquidContainer(200.0, 600, 500.0, 1500, 1500);
    }
}