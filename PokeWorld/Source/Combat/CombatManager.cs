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
        bool                _isOver;
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

        public bool IsOver { get => _isOver; private set => _isOver = value; }
        public int CurrentTurn { get => _currentTurn; private set => _currentTurn = value; }


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
            _isOver = false;
            CurrentTurn = 0;
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
            if(_isOver == false) 
            {
                AutoSwap(_player, _currentPlayerCreature);
            }
            Console.WriteLine(" ");
            Console.WriteLine(_currentPlayerCreature.CreatureName + " : " + _currentPlayerCreature.Stats.health + "/" + _currentPlayerCreature.Stats.maxHealth);
            Console.WriteLine(" ");

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
                            float effectiveness = _currentPlayerCreature.Moves[0].GetEffectiveness(_currentEnemyCreature.Stats.type, _types, _typeTable);
                            _currentPlayerCreature.Moves[0].Use(_currentPlayerCreature, _currentEnemyCreature, effectiveness);
                            _isPlayerTurn = false;

                        }
                        if (combatVal == "1")
                        {
                            float effectiveness = _currentPlayerCreature.Moves[1].GetEffectiveness(_currentEnemyCreature.Stats.type, _types, _typeTable);
                            _currentPlayerCreature.Moves[1].Use(_currentPlayerCreature, _currentEnemyCreature, effectiveness);
                            _isPlayerTurn = false;

                        }
                    }

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
                            _isPlayerTurn = false;
                        }
                    }
                }
            }
            

            if (_isPlayerTurn == false)
            {
                if (_isOver == false)
                {
                    AutoSwap(_enemy, _currentEnemyCreature);
                }
                Console.WriteLine("Au tour d'" + _currentEnemyCreature.CreatureName);
                _enemy.Turn(_currentEnemyCreature, _currentPlayerCreature);
                _isPlayerTurn = true;
            }
            CurrentTurn += 1;
            Console.WriteLine(" ");
            Console.WriteLine(_currentEnemyCreature.CreatureName + " : " + _currentEnemyCreature.Stats.health + "/" + _currentEnemyCreature.Stats.maxHealth);
            Console.WriteLine(" ");

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
                    }
                    else if(fighter == _enemy)
                    {
                        _currentEnemyCreature = fighter.Creatures[i];
                    }
                }
            }
        }

        public void FightEnd()
        {
            int playerCreatureDead = 0;
            int enemyCreatureDead = 0;
            for (int i = 0; i < _player.Creatures.Count(); i++)
            {
                _player.Creatures[i].CheckIsDead();
                if (_player.Creatures[i].IsDead)
                {
                    playerCreatureDead += 1;
                }
            }
            for (int i = 0; i < _enemy.Creatures.Count(); i++)
            {
                _enemy.Creatures[i].CheckIsDead();
                if (_enemy.Creatures[i].IsDead)
                {
                    enemyCreatureDead += 1;
                }
            }
            if (playerCreatureDead == _player.Creatures.Count())
            {

                Win(_enemy, _player);
                IsOver = true;
            }
            else if (enemyCreatureDead == _enemy.Creatures.Count())
            {
                Win(_player, _enemy);
                IsOver = true ;
            }
        }

        public void AutoSwap(Fighter fighter,Creatures currentCreature)
        {
            currentCreature.CheckIsDead();
            if (currentCreature.IsDead)
            {
                for (int i = 0; i < fighter.Creatures.Count(); i++)
                {
                    if (currentCreature == fighter.Creatures[i] && fighter.Creatures[i + 1] != null)
                    {
                        SwapCreature(fighter, fighter.Creatures[i + 1].CreatureName);
                        break;
                    }
                }
            }
        }

        public void Win(Fighter Winner, Fighter Looser)
        {
            Console.WriteLine("The winner is " +  Winner.Name);
            Console.WriteLine(Looser.Name + " has lost.");
        }



        #endregion Methods

    }
}
