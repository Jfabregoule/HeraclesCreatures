using HeraclesCreatures.GameObject;
using HeraclesCreatures.Source;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Items;

class Program
{
    public static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        Player Hercule = new Player();
        Creatures Tiger = new Creatures("Tiger");
        Potion Popo = new Potion();

        Hercule.AddCreature(Tiger);
        Hercule.AddItems(Popo);

        Console.WriteLine(Hercule.Creatures[0].Stats.health);
        Hercule.Creatures[0].TakeDamage(30);
        Console.WriteLine(Hercule.Creatures[0].Stats.health);
        Hercule.Items[0].Use(ref Tiger);
        Console.WriteLine(Hercule.Creatures[0].Stats.health);

        gameManager.InitializeGame();
        gameManager.GameLoop();
        Console.WriteLine();
    }
}