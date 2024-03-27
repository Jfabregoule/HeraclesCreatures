using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    class InputManager
    {
        /*------------------------------------------------------------------------------------------*\
         |                                                                                            |
         |                                                                                            |
         |                                          Fields                                            |
         |                                                                                            |
         |                                                                                            |
         \*------------------------------------------------------------------------------------------*/

        #region Fields

        Dictionary<string, bool> keyStates = new Dictionary<string, bool>();

        HashSet<ConsoleKey> pressedKeys = new HashSet<ConsoleKey>();

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public void Update()
        {
            pressedKeys.Clear();

            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                pressedKeys.Add(keyInfo.Key);
            }
        }

        public HashSet<ConsoleKey> GetKeysPressed()
        {
            return pressedKeys;
        }
        public bool GetKeyDown(ConsoleKey key)
        {
            return pressedKeys.Contains(key);
        }

        public bool IsAnyKeyPressed() { return pressedKeys.Count > 0; }

        #endregion Methods
    }
}
