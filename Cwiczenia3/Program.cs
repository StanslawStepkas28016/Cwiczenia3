using System.Linq.Expressions;
using System.Text;

namespace Cwiczenia3;

// dorobić exception
public abstract class Container
{
    public static HashSet<int> SerialSet { get; set; } = new HashSet<int>();
    public double ContainerWeight { get; set; } // Waga kontenera (kg).
    public double LoadWeight { get; set; } // Waga towaru (kg).
    public double MaxLoad { get; set; } // Maksymalna ładowność (kg).
    public double Height { get; set; } // Wysokość kontenera (cm).
    public double Depth { get; set; } // Głębokość kontenera (cm).
    public string SerialNum { get; set; } // Numer Seryjny.

    protected Container(double containerWeight, double loadWeight, double maxLoad, double height, double depth,
        string serialNum)
    {
        ContainerWeight = containerWeight;
        LoadWeight = loadWeight;
        MaxLoad = maxLoad;
        Height = height;
        Depth = depth;
        SerialNum = serialNum;
    }

    public void EmptyContainer()
    {
        LoadWeight = 0;
    }

    public abstract void LoadContainer(double WeightToLoad);

    protected void GenerateAndSetSerialNumber(string suffix)
    {
        StringBuilder sb = new StringBuilder();
        var next = new Random().Next();

        while (!SerialSet.Contains(next))
        {
            next = new Random().Next();
        }

        sb.Append(SerialNum).Append(suffix).Append(next);
        SerialSet.Add(next);
        SerialNum = sb.ToString();
    }

    public abstract void SetSerialNumAbstra();
}

public class LiquidContainer : Container
{
    public LiquidContainer(double containerWeight, double loadWeight, double maxLoad, double height, double depth,
        string serialNum) : base(containerWeight, loadWeight, maxLoad, height, depth, serialNum)
    {
    }

    public override void SetSerialNumAbstra()
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
        Console.Out.Write("Test");
    }
}