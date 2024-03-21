using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
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
        CombatManager                       _currentFight;
        MapClass                            _map;
        Dictionary<string, CreatureStats>   _creaturesStats;
        Dictionary<string, Moves>           _moveStats;
        bool                                _isRunning;
        List<string>                        _types;
        double[,]                           _tableauValeurs;

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

        public GameManager() 
        {
            _isRunning = true;
            _inputManager = new InputManager();

            GenerateTypes();

            Creatures OrangOutant = new Creatures("OrangOutant");
            List<Creatures> Singe = new List<Creatures>();
            Attack COUPDECAILLOU = new Attack();
            OrangOutant.AddMove(COUPDECAILLOU);
            Singe.Add(OrangOutant);
            Enemy Ougabouga = new Enemy(Singe, 3);
            Creatures Tiger = new Creatures("Tiger");
            Tiger.AddMove(COUPDECAILLOU);
            Player Hercule = new Player();
            Hercule.AddCreature(Tiger);
            CombatManager test = new CombatManager(Hercule, Ougabouga);
            _currentFight = test;
            test.StartFight();
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
                if (_currentFight != null)
                {
                    _currentFight.Fighting();
                }
            }

        }

        private void GenerateTypes() 
        {
            _tableauValeurs = new double[,]
            {
                { 1, 1, 1, 1, 0.5, 1, 1, 1, 0.5, 1, 0 },
                { 1, 0.5, 0.5, 2, 2, 1, 1, 1, 0.5, 1, 1 },
                { 1, 2, 0.5, 0.5, 1, 1, 2, 1, 2, 1, 1 },
                { 1, 0.5, 2, 0.5, 0.5, 0.5, 2, 1, 2, 0.5, 1 },
                { 1, 0.5, 1, 0.5, 0.5, 1, 1, 1, 2, 1, 1 },
                { 1, 1, 1, 2, 0.5, 0.5, 2, 1, 0.5, 1, 1 },
                { 1, 2, 1, 0.5, 2, 0, 0.5, 1, 2, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 0.5, 1, 1, 2 },
                { 1, 1, 1, 1, 0.5, 2, 0.5, 1, 0.5, 1, 1 },
                { 1, 1, 1, 2, 0, 1, 0.5, 1, 0.5, 0.5, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 0.5, 1, 1, 2 }
            };
            _types = new List<string>
            {
                "Normal", 
                "Fire", 
                "Water", 
                "Plant", 
                "Steel", 
                "Flying", 
                "Ground", 
                "Dark", 
                "Rock", 
                "Poison", 
                "Ghost"
            };

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
