using HeraclesCreatures;
using System;

namespace HeraclesCreatures
{

    class Program
    {
        public static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            gameManager.GameLoop();

        }
    }
}