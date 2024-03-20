using HeraclesCreatures.GameObject;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Items;
using HeraclesCreatures.Source.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source
{
    internal class GameManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        InputManager                        _inputManager;
        MapClass                            _map;
        Dictionary<string, CreatureStats>   _creaturesStats;
        bool                                _isRunning;

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

        public GameManager() { }

        public void InitializeGame()
        {
            _isRunning = true;
            _inputManager = new InputManager();

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

        }
        public void GameLoop()
        {
            while(_isRunning)
            {
                _inputManager.Update();
                if (_inputManager.IsAnyKeyPressed())
                {
                    Console.WriteLine("Oueoue");
                }
            }

        }

        private void GetCreaturesStats() 
        {

        }

        private void GetMoves() 
        {
            
        }

        #endregion Methods

    }
}
