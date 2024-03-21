using HeraclesCreatures;

class Program
{
    public static void Main(string[] args)
    {
        /*
        GameManager gameManager = new GameManager();
        Player Hercule = new Player();
        Creatures Tiger = new Creatures("Tiger");
        Potion Popo = new Potion();

        Hercule.AddCreature(Tiger);
        Hercule.AddItems(Popo);

        Console.WriteLine(Hercule.Creatures[0].stats.health);
        Hercule.Creatures[0].TakeDamage(30);
        Console.WriteLine(Hercule.Creatures[0].stats.health);
        Hercule.Items[0].Use(ref Tiger);
        Console.WriteLine(Hercule.Creatures[0].stats.health);

        
        gameManager.InitializeGame();
        gameManager.GameLoop();
        Console.WriteLine();
        */
        FileManager shoukran = new FileManager();
        CellData_ mabrouk = new CellData_();
        mabrouk = shoukran.GetMapObjectData("Ressources/Players/Player1.txt");
        mabrouk.printCellData();
    }
}
