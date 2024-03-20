using HeraclesCreatures.GameObject;
using HeraclesCreatures.Source;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Items;

class Program
{
    public static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();

        gameManager.InitializeGame();
        gameManager.GameLoop();
        Console.WriteLine();
    }
}