using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class CombatManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Player              _player;
        Enemy               _enemy;
        int                 _currentTurn;
        bool                _isPlayerTurn;
        Creatures           _currentPlayerCreature;
        Creatures           _currentEnemyCreature;
        List<string>        _types;
        float[,]            _typeTable;

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

        public CombatManager(Player player, Enemy enemy, List<string> types, float[,] typeTable) 
        {
            _player = player;
            _enemy = enemy;
            _currentTurn = 0;
            _isPlayerTurn = false;
            _currentPlayerCreature = player.Creatures[0];
            _currentEnemyCreature = enemy.Creatures[0];
            _types = types;
            _typeTable = typeTable;
        }

        public void StartFight()
        {
            if (_currentPlayerCreature.Stats.AttackSpeed > _currentEnemyCreature.Stats.AttackSpeed)
            {
                _isPlayerTurn = true;
            }

        }

        public void Fighting()
        {
            if (_isPlayerTurn)
            {
                Console.WriteLine("-Combat");
                Console.WriteLine("-Objets");
                Console.WriteLine("-Swap");
                string val = Console.ReadLine();
                if (val == "Combat")
                {
                    Console.WriteLine("Choisissez votre attaque");
                    for (int i = 0; i < _currentPlayerCreature.Moves.Count(); i++)
                    {
                        Console.WriteLine(i + " : " + _currentPlayerCreature.Moves[i].MoveName);
                    }
                    string combatVal = Console.ReadLine();
                    if (combatVal != null)
                    {
                        if (combatVal == "0")
                        {
                            _currentPlayerCreature.Moves[0].Use(_currentPlayerCreature, _currentEnemyCreature);
                            _isPlayerTurn = false;

                        }
                        if (combatVal == "1")
                        {
                            _currentPlayerCreature.Moves[1].Use(_currentPlayerCreature, _currentEnemyCreature);
                            _isPlayerTurn = false;

                        }
                    }
                    Console.Write(_currentEnemyCreature.CreatureName + " : ");
                    Console.WriteLine(_currentEnemyCreature.Stats.health);
                    Console.Write(_currentPlayerCreature.CreatureName + " : ");
                    Console.WriteLine(_currentPlayerCreature.Stats.health);

                }
                else if (val == "Objets")
                {
                    Console.WriteLine("Objets : ");
                    for (int i = 0; i < _player.Items.Count(); i++)
                    {
                        Console.WriteLine(i + " : " + _player.Items[i].name);
                    }

                    string objval = Console.ReadLine();
                    if (objval != null)
                    {
                        if (objval == "0")
                        {
                            _player.Items[0].Use(ref _currentPlayerCreature);
                            _isPlayerTurn = false;
                        }
                        else if (objval == "1")
                        {
                            _player.Items[1].Use(ref _currentPlayerCreature);
                            _isPlayerTurn = false;
                        }
                    }

                }
                else if (val == "Swap")
                {
                    for (int i = 0; i < _player.Creatures.Count(); i++)
                    {
                        if (_player.Creatures[i] == _currentPlayerCreature) { }
                        else
                        {
                            Console.WriteLine(i + " : " + _player.Creatures[i].CreatureName);
                        }
                    }
                    string swapval = Console.ReadLine();
                    if (swapval != null)
                    {
                        if (swapval == "1")
                        {
                            SwapCreature(_player, _player.Creatures[1].CreatureName);
                        }
                    }
                }
            }

            if (_isPlayerTurn == false)
            {
                Console.WriteLine("Au tour d'" + _currentEnemyCreature.CreatureName);
                _enemy.Turn(_currentEnemyCreature, _currentPlayerCreature);
                Console.Write(_currentEnemyCreature.CreatureName + " : ");
                Console.WriteLine(_currentEnemyCreature.Stats.health);
                Console.Write(_currentPlayerCreature.CreatureName + " : ");
                Console.WriteLine(_currentPlayerCreature.Stats.health);
                _isPlayerTurn = true;
            }
        }

        public void SwapCreature(Fighter fighter,string creatureName) 
        {
            for (int i = 0; i < fighter.Creatures.Count(); i++)
            {
                if (fighter.Creatures[i].CreatureName == creatureName)
                {
                    if(fighter == _player)
                    {
                        _currentPlayerCreature = fighter.Creatures[i];
                        _isPlayerTurn = false;
                    }
                    else if(fighter == _enemy)
                    {
                        _currentEnemyCreature = fighter.Creatures[i];
                        _isPlayerTurn = true;
                    }
                }
            }
        }



        #endregion Methods

    }
}
