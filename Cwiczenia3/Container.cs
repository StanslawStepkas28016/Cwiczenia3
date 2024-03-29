using System.Text;

namespace Cwiczenia3;

public abstract class Container
{
    public static HashSet<int> SerialSet { get; set; } = new HashSet<int>(); // .
    public double ContainerWeight { get; set; } // Waga kontenera (kg).
    public double LoadWeight { get; set; } // Waga towaru (kg).
    public double MaxLoad { get; set; } // Maksymalna ładowność - pojemność (kg).
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


    public abstract void LoadContainer(double weightToLoad);

    protected void GenerateAndSetSerialNumber(string suffix)
    {
        StringBuilder sb = new StringBuilder();
        var random = new Random();
        var next = random.Next(1, 1000);

        while (SerialSet.Contains(next))
        {
            next = random.Next(1, 1000);
        }

        sb.Append("KON").Append(SerialNum).Append(suffix).Append(next);
        SerialSet.Add(next);
        SerialNum = sb.ToString();
    }

    public abstract void EmptyContainer();

    public abstract string ToString();
}