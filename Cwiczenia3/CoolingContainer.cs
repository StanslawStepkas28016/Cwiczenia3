namespace Cwiczenia3;

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