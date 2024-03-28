using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class Npc : MapObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        NpcData _data;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public NpcData Data { get => _data; set => _data = value; }

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

        public Npc()
        {
            _data = new();
        }

        public Npc(int x, int y, NpcData data)
        {
            X = x;
            Y = y;
            _data = data;
        }

        public override void PlayDialogue(Scene currentScene)
        {
            InputManager inputManager = new InputManager();
            base.PlayDialogue(currentScene);
            for (int i = 0; i < Data.Dialogue.Count; i++)
            {
                bool check = false;
                Console.Write(Data.Dialogue[i]);
                do
                {
                    inputManager.Update();
                    check = inputManager.GetKeyDown(ConsoleKey.Enter);
                } while (check == false);
                ClearDialogue(currentScene);
            }
        }


        #endregion Methods

    }
}
