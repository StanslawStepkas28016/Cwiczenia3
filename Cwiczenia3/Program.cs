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

public interface IHazardNotifier
{
    // Do prób wykonania niebezpiecznej operacji.
    public void HazardNotification(string str);
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazard { get; set; }

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

    public override void EmptyContainer()
    {
        if (LoadWeight == 0)
        {
            HazardNotification("Kontener nie jest napełniony, nie można go opróżnić!");
        }
        else
        {
            LoadWeight = 0;
        }
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

    public override string ToString()
    {
        return "Kontner o numerze : " + SerialNum + ", przechowuje ciecz w ilości : " + LoadWeight + "kg.";
    }
}

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double containerWeight, double loadWeight, double maxLoad, double height, double depth,
        double pressure) : base(containerWeight, loadWeight, maxLoad, height, depth)
    {
        Pressure = pressure;
        GenerateAndSetSerialNumber("-G-");
    }


    public void HazardNotification(string str)
    {
        Console.Out.WriteLine(str + " - Informacja dla konterea o numerze : " + this.SerialNum);
    }

    public override void EmptyContainer()
    {
        if (LoadWeight == 0)
        {
            HazardNotification("Kontener nie jest napełniony, nie można go opróżnić!");
        }
        else
        {
            LoadWeight = 0.05 * LoadWeight;
        }
    }

    public override void LoadContainer(double weightToLoad)
    {
        if (weightToLoad + LoadWeight > MaxLoad)
        {
            HazardNotification("Masa towaru nie może przekraczać maksymalnej masy ładunku kontenera!");
        }
        else
        {
            LoadWeight += weightToLoad;
        }
    }

    public override string ToString()
    {
        return "Kontner o numerze : " + SerialNum + ", przechowuje gaz w ilości : " + LoadWeight + "kg.";
    }
}

public class CoolingContainer : Container, IHazardNotifier
{
    public double Temperature { get; set; }
    public string ProductType { get; set; }

    public static Dictionary<string, double> PossibleProducts = new Dictionary<string, double>()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public CoolingContainer(double containerWeight, double loadWeight, double maxLoad, double height, double depth,
        double temperature, string productType) : base(containerWeight, loadWeight, maxLoad, height, depth)
    {
        GenerateAndSetSerialNumber("-C-");

        if (!PossibleProducts.ContainsKey(productType))
        {
            HazardNotification("Produkt " + productType + " nie istnieje w bazie dostępnych produktów.");

            Console.Out.Write("Dostępne produkty to : ");
            foreach (var key in PossibleProducts.Keys)
            {
                Console.Out.Write(key + ", ");
            }

            Console.Out.WriteLine();
        }
        else
        {
            if (temperature < PossibleProducts[productType])
            {
                HazardNotification("Temperatura produktu nie może być niższa niż temperatura sugerowania," +
                                   " dokładnie : " + PossibleProducts[productType]);
            }
            else
            {
                Temperature = temperature;
                ProductType = productType;
            }
        }
    }

    public override void LoadContainer(double weightToLoad)
    {
        if (weightToLoad + LoadWeight > MaxLoad)
        {
            HazardNotification("Masa towaru nie może przekraczać maksymalnej masy ładunku kontenera!");
        }
        else
        {
            LoadWeight += weightToLoad;
        }
    }

    public override void EmptyContainer()
    {
        LoadWeight = 0;
    }

    public void HazardNotification(string str)
    {
        Console.Out.WriteLine(str + " - Informacja dla konterea o numerze : " + this.SerialNum);
    }

    public override string ToString()
    {
        return "Kontner o numerze : " + SerialNum + ", przechowuje produkt chłodzony : "
               + ProductType + ", w ilości : " + LoadWeight + "kg.";
    }
}

public class ContainerShip : IHazardNotifier
{
    private List<Container> OnBoard { get; set; }
    public double MaxSpeed { get; set; }
    public int MaxContainers { get; set; }
    public double MaxLoad { get; set; }
    public int ShipNumber { get; set; }
    private double CurrentLoad;
    private int ContainersInShip;
    public static int ShipCounter = 1;

    public ContainerShip(List<Container> containersOnShip, double maxSpeed, int maxContainers, double maxLoad)
    {
        OnBoard = new List<Container>(containersOnShip);
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxLoad = maxLoad;
        ShipNumber = ShipCounter++;
    }

    public void AddContainerList(List<Container> list)
    {
        foreach (var container in list)
        {
            CurrentLoad += container.ContainerWeight + container.LoadWeight;
            ContainersInShip += 1;
            OnBoard.Add(container);
        }
    }

    public void ReplaceContainer(Container toReplaceContainer, Container replacementContainer)
    {
        for (var i = 0; i < OnBoard.Count; i++)
        {
            if (OnBoard[i].Equals(toReplaceContainer))
            {
                OnBoard[i] = replacementContainer;
                break;
            }
        }
    }

    public void AddContainer(Container container)
    {
        if (ShipCounter <= MaxLoad)
        {
            if (container.LoadWeight + container.ContainerWeight + MaxLoad > MaxLoad)
            {
                CurrentLoad += container.ContainerWeight + container.LoadWeight;
                OnBoard.Add(container);
            }
            else
            {
                HazardNotification("Nie można załadować kontenera na statek," +
                                   " jego waga przekracza dopuszczalną na ten moment wagę," +
                                   " która wynosi : " + (MaxLoad - CurrentLoad) + "!");
            }
        }
        else
        {
            HazardNotification("Przekroczono ładowność statku, " +
                               "maksymalna ilość kontenerów na statku to : " + MaxContainers);
        }
    }

    public void RemoveContainer(Container container)
    {
        if (OnBoard.Any())
        {
            OnBoard.Remove(container);
        }
        else
        {
            HazardNotification("Na statku nie ma żadnych kontenerów, nie można usunąć kontenera!");
        }
    }

    public void EmptyShip()
    {
        OnBoard.Clear();
    }

    public void HazardNotification(string str)
    {
        Console.Out.WriteLine(str + " - Informacja dla statku: " + ShipNumber);
    }

    public void GetShipInfo()
    {
        Console.Out.WriteLine("Statek o numerze : " + ShipNumber +
                              " ma na pokładzie (" + CurrentLoad + "kg - " + ShipCounter + " statków : {");

        foreach (var container in OnBoard)
        {
            Console.Out.WriteLine(container.ToString());
        }

        Console.Out.WriteLine("}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // LiquidContainerTest();
        // GasContainerTest();
        // CoolingContainerTest();
        ShipTest();
    }

    private static void ShipTest()
    {
        List<Container> toPut = new List<Container>()
        {
            new GasContainer(200.0, 400, 500.0,
                1500, 1500, 1240),
            new LiquidContainer(200.0, 400, 500.0,
                1500, 1500, false),
            new CoolingContainer(200.0, 400,
                500.0, 1500, 1500, 13.9, "Bananas")
        };

        var containerShip = new ContainerShip(toPut, 100, 20, 100_000);
        containerShip.GetShipInfo();
    }

    private static void CoolingContainerTest()
    {
        // Test dodania za niskiej temperatury.
        var coolingContainerA = new CoolingContainer(200.0, 400,
            500.0, 1500, 1500, 12.5, "Bananas");
        // Test dodania produktu, który nie występuje w dictionary.
        var coolingContainerB = new CoolingContainer(200.0, 400,
            500.0, 1500, 1500, 12.5, "Mango");
    }

    private static void GasContainerTest()
    {
        var gasContainer = new GasContainer(200.0, 400, 500.0, 1500, 1500, 1240);
        Console.Out.WriteLine(gasContainer.ToString());
    }

    private static void LiquidContainerTest()
    {
        // Test ładowalności (gross Load mniejszy niż 90% i isHazard false) - musi przejść. 
        var liquidContainerA = new LiquidContainer(200.0, 400, 500.0, 1500, 1500, false);
        Console.Out.WriteLine(liquidContainerA.SerialNum);
        liquidContainerA.LoadContainer(20);

        // Test ładowalności (gross Load większy niż 50% i isHazard true) - nie może przejść. 
        var liquidContainerB = new LiquidContainer(200.0, 400, 500.0, 1500, 1500, true);
        liquidContainerB.LoadContainer(90);
    }
}