namespace Cwiczenia3;

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