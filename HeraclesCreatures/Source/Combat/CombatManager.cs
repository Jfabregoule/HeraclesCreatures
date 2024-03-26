using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            FightEnd();
            AutoSwap(_player, _currentPlayerCreature);
            Console.WriteLine(" ");
            Console.WriteLine(_currentPlayerCreature.CreatureName + " : " + _currentPlayerCreature.Stats.health + "/" + _currentPlayerCreature.Stats.maxHealth);
            Console.WriteLine(" ");
            if (_isOver == false)
            {
                if (_isPlayerTurn)
                {
                    PlayerTurn();
                    
                }
            }
                
                
            CheckForStatusEffects(_currentPlayerCreature, _currentEnemyCreature);
            CurrentTurn += 1;

            FightEnd();
            AutoSwap(_enemy, _currentEnemyCreature);
            Console.WriteLine(" ");
            Console.WriteLine(_currentEnemyCreature.CreatureName + " : " + _currentEnemyCreature.Stats.health + "/" + _currentEnemyCreature.Stats.maxHealth);
            Console.WriteLine(" ");
            if (_isOver == false)
            {
                if (_isPlayerTurn == false)
                {
                    EnemyTurn();
                }
                
            } 
            
        }

        public void CheckForStatusEffects(Creatures allyCreature, Creatures enemyCreature)
        {
            if (allyCreature.State != CreatureState.ALIVE && allyCreature.State != CreatureState.DEAD) 
            {
                Random random = new Random();
                int chance = random.Next(1, 101);

                if (chance > 20)
                {
                    if (allyCreature.Stats.health > 0)
                    {
                        allyCreature.State = CreatureState.ALIVE;
                        Console.Write(allyCreature.CreatureName);
                        Console.WriteLine(" is now cured.");
                    }
                    else
                    {
                        allyCreature.State = CreatureState.DEAD;
                    }
                }
            }
            switch (allyCreature.State)
            {
                case CreatureState.BURNED:
                    CreatureStats creatureStats = allyCreature.Stats;
                    creatureStats.health -= allyCreature.Stats.maxHealth * 0.10f;
                    allyCreature.Stats = creatureStats;
                    Console.Write(allyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = allyCreature.Stats;
                    creatureStats2.health -= allyCreature.Stats.maxHealth * 0.10f;
                    allyCreature.Stats = creatureStats2;
                    Console.Write(allyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    break;
            }
            if (enemyCreature.State != CreatureState.ALIVE && enemyCreature.State != CreatureState.DEAD)
            {
                Random random = new Random();
                int chance = random.Next(1, 101);

                if (chance > 20)
                {
                    if (enemyCreature.Stats.health > 0)
                    {
                        enemyCreature.State = CreatureState.ALIVE;
                        Console.Write(enemyCreature.CreatureName);
                        Console.WriteLine(" is now cured.");
                    }
                    else
                    {
                        enemyCreature.State = CreatureState.DEAD;
                    }
                }
            }
            switch (enemyCreature.State)
            {
                case CreatureState.BURNED:
                    CreatureStats creatureStats = enemyCreature.Stats;
                    creatureStats.health -= enemyCreature.Stats.maxHealth * 0.10f;
                    enemyCreature.Stats = creatureStats;
                    Console.Write(enemyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = enemyCreature.Stats;
                    creatureStats2.health -= enemyCreature.Stats.maxHealth * 0.10f;
                    enemyCreature.Stats = creatureStats2;
                    Console.Write(enemyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    break;
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
            if (_enemy.Creatures.All(Creatures => Creatures.State == CreatureState.DEAD))
            {
                IsOver = true;
                Win(_player, _enemy);
            }
            else if (_player.Creatures.All(Creatures => Creatures.State == CreatureState.DEAD))
            {
                IsOver = true;
                Win(_enemy, _player);
            }

        }

        public void PlayerTurn()
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
                    int moveID = int.Parse(combatVal);
                    float effectiveness = _currentPlayerCreature.Moves[moveID].GetEffectiveness(_currentEnemyCreature.Stats.type, _types, _typeTable);
                    _currentPlayerCreature.Moves[moveID].Use(_currentPlayerCreature, _currentEnemyCreature, effectiveness);
                    _isPlayerTurn = false;
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

        public void EnemyTurn()
        {
            Console.WriteLine("Au tour d'" + _currentEnemyCreature.CreatureName);
            _enemy.Turn(_currentEnemyCreature, _currentPlayerCreature);
            _isPlayerTurn = true;
        }

        public void AutoSwap(Fighter fighter,Creatures currentCreature)
        {
            currentCreature.CheckIsDead();
            if (currentCreature.State == CreatureState.DEAD)
            {
                for (int i = 0; i < fighter.Creatures.Count(); i++)
                {
                    if (currentCreature == fighter.Creatures[i] && i + 1 < fighter.Creatures.Count())
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
