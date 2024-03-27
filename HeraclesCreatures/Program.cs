using System;

namespace HeraclesCreatures
{
    class Program
    {
        public static void Main(string[] args)
        {
            SetConsoleSizeToScreen();

            GameManager gameManager = new GameManager();
            gameManager.GameLoop();
        }

        public static void SetConsoleSizeToScreen()
        {
            int width = Console.LargestWindowWidth;
            int height = Console.LargestWindowHeight;
            
            Console.SetWindowSize(width, height);
        }
    }
}
