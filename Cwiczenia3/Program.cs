using System.Collections.Specialized;
using System.Text;

namespace Cwiczenia3;

public class OverfillException : Exception
{
    public OverfillException(string? message) : base(message)
    {
    }
}

public interface IHazardNotifier
{
    // Do prób wykonania niebezpiecznej operacji.
    public void HazardNotification(string str);
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
        var gasContainer = new GasContainer(200.0, 600, 100_000,
            1500, 1500, 1240);

        List<Container> toPut = new List<Container>()
        {
            gasContainer,
            new LiquidContainer(200.0, 400, 500.0,
                1500, 1500, false),
            new CoolingContainer(200.0, 400,
                500.0, 1500, 1500, 13.9, "Bananas")
        };

        var containerShip = new ContainerShip(100, 20, 100_000);
        containerShip.AddContainerList(toPut);
        containerShip.RemoveContainer(gasContainer);
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