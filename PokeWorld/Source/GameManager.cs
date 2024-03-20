﻿using HeraclesCreatures.Source.Combat.Figthers.Enemy;
using HeraclesCreatures.Source.Combat.Figthers.Player;
using HeraclesCreatures.Source.Combat;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Creatures.Moves;
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

            Creatures OrangOutant = new Creatures("OrangOutant");
            List<Creatures> Singe = new List<Creatures>();
            Attack COUPDECAILLOU = new Attack();
            OrangOutant.AddMove(ref COUPDECAILLOU);
            Singe.Add(OrangOutant);
            Enemy Ougabouga = new Enemy(Singe, 3);
            Creatures Tiger = new Creatures("Tiger");
            Tiger.AddMove(ref COUPDECAILLOU);
            Player Hercule = new Player();
            Hercule.AddCreature(Tiger);
            CombatManager test = new CombatManager(Hercule, Ougabouga);
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