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

public interface IHazardNotifier
{
    // Do prób wykonania niebezpiecznej operacji.
    public void HazardNotification(string str);
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazard;


    public LiquidContainer(double containerWeight, double loadWeight, double maxLoad, double height, double depth,
        bool isHazard) :
        base(containerWeight, loadWeight, maxLoad, height, depth)
    {
        this.IsHazard = isHazard;
        GenerateAndSetSerialNumber("-L-");
    }

    public void HazardNotification(string str)
    {
        Console.Out.WriteLine(str + " - Informacja dla konterea o numerze : " + this.SerialNum);
    }

    public override void LoadContainer(double weightToLoad)
    {
        // Sprawdzenie, czy masa towaru do załadowania przekracza MaxLoad.
        bool isLoadable = (LoadWeight + weightToLoad) < MaxLoad;

        if (isLoadable)
        {
            // Jeżeli przechowywany jest niebezpieczny ładunek, możemy załadować kontener do 50% pojemności (50% MaxLoad'u).
            if (this.IsHazard)
            {
                if (weightToLoad + LoadWeight > 0.5 * MaxLoad)
                    HazardNotification("Konter zawiera niebezpieczny ładunek, " +
                                       "towar który jest ładowany nie może być większy niż 50% pojemności kontenera!");
                else
                    LoadWeight += weightToLoad;
            }
            else
            {
                if (weightToLoad + LoadWeight > 0.9 * MaxLoad)
                    HazardNotification("Kontener na ciecz (niezawierający " +
                                       "substancji niebezpiczniej) może być wypełniony tylko do 90% pojemności kontenera!");
                else
                    LoadWeight += weightToLoad;
            }
        }
        else
        {
            HazardNotification("Masa towaru do załadowania przekracza masę możliwą do załadowania!");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var liquidContainer = new LiquidContainer(200.0, 400, 500.0, 1500, 1500, false);
        liquidContainer.LoadContainer(20);
    }
}