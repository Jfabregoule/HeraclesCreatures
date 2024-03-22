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
        float[,]                            _typeTable;

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

            CreatureStats OrangOutanStats = new CreatureStats();
            OrangOutanStats.type = "Plant";
            Creatures OrangOutant = new Creatures("OrangOutant", OrangOutanStats);
            List<Creatures> Singe = new List<Creatures>();
            Attack COUPDECAILLOU = new Attack("COUPDECAILLOU");
            Spell COUP2TETE = new Spell("COUP2TETE");
            OrangOutant.AddMove(COUPDECAILLOU);
            Singe.Add(OrangOutant);
            Enemy Ougabouga = new Enemy(Singe, 2, _types, _typeTable);
            CreatureStats TigerStats = new CreatureStats();
            Creatures Tiger = new Creatures("Tiger", TigerStats);
            Tiger.AddMove(COUPDECAILLOU);
            Tiger.AddMove(COUP2TETE);
            CreatureStats ViperStats = new CreatureStats();
            Creatures Viper = new Creatures("Viper", ViperStats);
            Viper.AddMove(COUPDECAILLOU);
            Potion popo = new Potion();
            AttackPlus attP = new AttackPlus();
            Player Hercule = new Player();
            Hercule.AddCreature(Tiger);
            Hercule.AddCreature(Viper);
            Hercule.AddItems(popo);
            Hercule.AddItems(attP);
            CombatManager test = new CombatManager(Hercule, Ougabouga, _types, _typeTable);
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
            _typeTable = new float[,]
            {
                { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 0.0f },
                { 1.0f, 0.5f, 0.5f, 2.0f, 2.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 2.0f, 0.5f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 0.5f, 2.0f, 0.5f, 0.5f, 0.5f, 2.0f, 1.0f, 2.0f, 0.5f, 1.0f },
                { 1.0f, 0.5f, 1.0f, 0.5f, 0.5f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 0.5f, 2.0f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 2.0f, 1.0f, 0.5f, 2.0f, 0.0f, 0.5f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 2.0f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 2.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.5f, 0.5f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f }
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
