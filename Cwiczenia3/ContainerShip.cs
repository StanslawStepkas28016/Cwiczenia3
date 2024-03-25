namespace Cwiczenia3;

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

    public ContainerShip(double maxSpeed, int maxContainers, double maxLoad)
    {
        OnBoard = new List<Container>();
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxLoad = maxLoad;
        ShipNumber = ShipCounter++;
    }

    public void AddContainerList(List<Container> list)
    {
        double loadSumTemp = 0;
        var tempList = new List<Container>();

        foreach (var container in list)
        {
            loadSumTemp += container.ContainerWeight + container.LoadWeight;

            if (loadSumTemp > MaxLoad)
            {
                HazardNotification("Waga kontenerów razem z towarem przekracza ładowność statku," +
                                   " zmniejsz ilość kontenerów," +
                                   " bądź ich wagę!");
                return;
            }

            CurrentLoad += container.ContainerWeight + container.LoadWeight;
            ContainersInShip += 1;
            tempList.Add(container);
        }

        foreach (var container in tempList)
        {
            OnBoard.Add(container);
        }
    }

    public void ReplaceContainer(Container toReplaceContainer, Container replacementContainer)
    {
        if (OnBoard.Contains(toReplaceContainer))
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
        else
        {
            HazardNotification("Statek nie zawiera kontenera : "
                               + toReplaceContainer.SerialNum
                               + ", nie można go zamienić!");
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
            if (OnBoard.Contains(container))
            {
                OnBoard.Remove(container);
            }
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
        Console.Out.WriteLine(str + " - Informacja dla statku nr : " + ShipNumber);
    }

    public void GetShipInfo()
    {
        if (OnBoard.Any())
        {
            Console.Out.WriteLine("Statek o numerze : " + ShipNumber +
                                  " ma na pokładzie (" + CurrentLoad + "kg - " + ShipCounter + " statków : {");

            foreach (var container in OnBoard)
            {
                Console.Out.WriteLine(container.ToString());
            }

            Console.Out.WriteLine("}");
        }
        else
        {
            HazardNotification("Statek jest pusty, nie można wypisać informacji!");
        }
    }
}