using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
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

            Creatures OrangOutant = new Creatures("OrangOutant");
            List<Creatures> Singe = new List<Creatures>();
            Attack COUPDECAILLOU = new Attack("COUPDECAILLOU");
            Spell COUP2TETE = new Spell("COUP2TETE");
            OrangOutant.AddMove(COUPDECAILLOU);
            Singe.Add(OrangOutant);
            Enemy Ougabouga = new Enemy(Singe, 3);
            Creatures Tiger = new Creatures("Tiger");
            Tiger.AddMove(COUPDECAILLOU);
            Tiger.AddMove(COUP2TETE);
            Creatures Viper = new Creatures("Viper");
            Viper.AddMove(COUPDECAILLOU);
            Potion popo = new Potion();
            AttackPlus attP = new AttackPlus();
            Player Hercule = new Player();
            Hercule.AddCreature(Tiger);
            Hercule.AddCreature(Viper);
            Hercule.AddItems(popo);
            Hercule.AddItems(attP);
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

        private void GetCreaturesStats() 
        {

        }

        private void GetMoves() 
        {
            
        }

        #endregion Methods

    }
}
