using System.Text;

namespace Cwiczenia3;

// dorobić exception
public abstract class Container
{
    public double ContainerWeight { get; set; } // Waga kontenera.
    public double Load { get; set; } // Waga towaru.
    public double Height { get; set; } // Wysokość kontenera.
    public double Depth { get; set; } // Głębokość kontenera.
    public string SerialNum { get; set; } // Numer Seryjny

    protected Container(double containerWeight, double load, double height, double depth, string serialNum)
    {
        ContainerWeight = containerWeight;
        Load = load;
        Height = height;
        Depth = depth;
        SerialNum = serialNum;
    }

    public void EmptyContainer()
    {
        Load = 0;
    }

    public void LoadContainer(double WeightToLoad)
    {
        Load += WeightToLoad;
    }

    public abstract void SetSerialNum();
}

public class LiquidContainer : Container
{
    public LiquidContainer(double containerWeight, double load, double height, double depth, string serialNum) : base(containerWeight, load, height, depth, serialNum)
    {
    }

    public override void SetSerialNum()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(SerialNum);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.Out.Write("Test");
    }
}