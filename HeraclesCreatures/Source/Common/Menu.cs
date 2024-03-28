using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class Menu
    {
        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        InputManager _inputManager;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        #endregion Properties

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Events                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Events



        #endregion Events

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Methods                                           |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Menu(InputManager inputManager) 
        {
            _inputManager = inputManager;
        }

        public static void DrawMenu(int selectedOption)
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");

            Console.Write("\\");
            Console.Write(new string(' ', 24));
            if (selectedOption == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write("    Team");
            Console.ResetColor();
            Console.Write(new string(' ', 40));
            if (selectedOption == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write("    Load");
            Console.Write("                             /");
            Console.ResetColor();
            Console.WriteLine(new string(' ', 24));

            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");

            Console.Write("\\");
            Console.Write(new string(' ', 24));
            if (selectedOption == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write("    Save");
            Console.ResetColor();
            Console.Write(new string(' ', 40));
            if (selectedOption == 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">");
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write("    Leave");
            Console.Write("                            /");
            Console.ResetColor();
            Console.WriteLine(new string(' ', 15));

            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
        }


        public int CheckMenu()
        {
            if (_inputManager.IsAnyKeyPressed() && _inputManager.GetKeyDown(ConsoleKey.Escape))
            {
                Console.Clear();
                _inputManager.Update();
                int currentOption = 0;
                DrawMenu(0);
                while (!_inputManager.GetKeyDown(ConsoleKey.Escape) && !_inputManager.GetKeyDown(ConsoleKey.Enter))
                {
                    _inputManager.Update();
                    if (_inputManager.IsAnyKeyPressed())
                    {
                        if (_inputManager.GetKeyDown(ConsoleKey.DownArrow))
                        {
                            if (currentOption == 0 || currentOption == 2)
                            {
                                currentOption += 1;
                            }
                        }
                        else if (_inputManager.GetKeyDown(ConsoleKey.UpArrow))
                        {
                            if (currentOption == 1 || currentOption == 3)
                            {
                                currentOption -= 1;
                            }
                        }
                        else if (_inputManager.GetKeyDown(ConsoleKey.RightArrow))
                        {
                            if (currentOption == 0 || currentOption == 1)
                            {
                                currentOption += 2;
                            }
                        }
                        else if (_inputManager.GetKeyDown(ConsoleKey.LeftArrow))
                        {
                            if (currentOption == 2 || currentOption == 3)
                            {
                                currentOption -= 2;
                            }
                        }
                        DrawMenu(currentOption);
                    }
                }
                if (_inputManager.GetKeyDown(ConsoleKey.Enter))
                {
                    return currentOption;
                }
            }
            return -1;
        }

        #endregion Methods
    }
}
