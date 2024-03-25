namespace Cwiczenia3;

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