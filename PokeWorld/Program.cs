using HeraclesCreatures.Source;
using HeraclesCreatures.Source.Combat;
using HeraclesCreatures.Source.Combat.Figthers.Enemy;
using HeraclesCreatures.Source.Combat.Figthers.Player;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Creatures.Moves;
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