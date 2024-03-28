using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class HealingStand : MapObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        HealingStandData _data;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public HealingStandData Data { get => _data; set => _data = value; }

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

        public HealingStand()
        {
            _data = new();
        }

        public HealingStand(int x, int y, HealingStandData data)
        {
            X = x;
            Y = y;
            _data = data;
        }

        public override void PlayDialogue(Scene currentScene)
        {
            InputManager inputManager = new InputManager();
            base.PlayDialogue(currentScene);
            bool check = false;
            Console.Write("Your team healed to full !");
            do
            {
                inputManager.Update();
                check = inputManager.GetKeyDown(ConsoleKey.Enter);
            } while (check == false);
            ClearDialogue(currentScene);
        }


        #endregion Methods

    }
}
