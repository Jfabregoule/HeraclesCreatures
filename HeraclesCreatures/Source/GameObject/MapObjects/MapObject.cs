using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class MapObject : GameObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        int _x;
        int _y;
        bool _isActive;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public string[] Dialogue { get => _dialogue; set => _dialogue = value; }

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        // Constructor
        public MapObject()
        {
            _x = 0;
            _y = 0;
            _isActive = true;
            _dialogue = new string[0];
        }

        public virtual void PlayDialogue(Scene currentScene) {
            Console.SetCursorPosition(0, currentScene.Height * 5 + 5);
        }

        public virtual void ClearDialogue(Scene currentScene)
        {
            Console.SetCursorPosition(0, currentScene.Height * 5 + 5);
            for (int i = 0; i < 150; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');
            }
            Console.SetCursorPosition(0, currentScene.Height * 5 + 5);
        }

        #endregion Methods

    }
}
