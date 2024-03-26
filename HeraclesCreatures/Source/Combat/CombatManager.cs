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
            _enemy.CurrentCreature = enemy.CurrentCreature;
            _types = types;
            _typeTable = typeTable;
        }

        public void StartFight()
        {
            if (_player.CurrentCreature.Stats.AttackSpeed > _enemy.CurrentCreature.Stats.AttackSpeed)
            {
                _isPlayerTurn = true;
            }

        }

        public void Fighting()
        {
            
            AutoSwap(_player, _player.CurrentCreature);
            Console.WriteLine(" ");
            Console.WriteLine(_player.CurrentCreature.CreatureName + " : " + _player.CurrentCreature.Stats.health + "/" + _player.CurrentCreature.Stats.maxHealth);
            Console.WriteLine(" ");
            if (_isOver == false)
            {
                if (_isPlayerTurn)
                {
                    PlayerTurn();
                    
                }
            }
                
                
            CheckForStatusEffects(_player.CurrentCreature, _enemy.CurrentCreature);
            CurrentTurn += 1;

            FightEnd();
            AutoSwap(_enemy, _enemy.CurrentCreature);
            Console.WriteLine(" ");
            Console.WriteLine(_enemy.CurrentCreature.CreatureName + " : " + _enemy.CurrentCreature.Stats.health + "/" + _enemy.CurrentCreature.Stats.maxHealth);
            Console.WriteLine(" ");
            if (_isOver == false)
            {
                if (_isPlayerTurn == false)
                {
                    EnemyTurn();
                }

            }

            FightEnd();
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

        public void SwapCreature(Fighter fighter,int index) 
        {
           if(index <= fighter.Creatures.Count)
            {
                fighter.CurrentCreature = fighter.Creatures[index];
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

        public bool AreAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
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
                for (int i = 0; i < _player.CurrentCreature.Moves.Count(); i++)
                {
                    Console.WriteLine(i + " : " + _player.CurrentCreature.Moves[i].MoveName);
                }
                string combatVal = Console.ReadLine();
                if (combatVal != null)
                {
                    if (AreAllDigits(combatVal) == true && combatVal.Length > 0)
                    {
                        int moveID = int.Parse(combatVal);
                        if (_player.CurrentCreature.Moves[moveID].PP > 0 || (_player.CurrentCreature.Moves[moveID].Stats.ManaCost != 0.0f && _player.CurrentCreature.Mana >= _player.CurrentCreature.Moves[moveID].Stats.ManaCost))
                        {
                            float effectiveness = Moves.GetEffectiveness(_enemy.CurrentCreature.Stats.type, _player.CurrentCreature.Moves[moveID].Stats.Type, _types, _typeTable);
                            _player.CurrentCreature.Moves[moveID].Use(_player.CurrentCreature, _enemy.CurrentCreature, effectiveness);
                            _isPlayerTurn = false;
                        }
                        else
                        {
                            if (_player.CurrentCreature.Moves[moveID].PP <= 0 && _player.CurrentCreature.Moves[moveID].Stats.ManaCost == 0.0f)
                            {
                                Console.WriteLine("{0} has no PP left.", _player.CurrentCreature.Moves[moveID].MoveName);
                            }
                            else
                            {
                                Console.WriteLine("{0} has not enough mana to cast {1}.", _player.CurrentCreature.CreatureName, _player.CurrentCreature.Moves[moveID].MoveName);
                            }
                        }
                    }
                    else
                    {
                        //
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
                        _player.Items[0].Use(_player.CurrentCreature);
                        _isPlayerTurn = false;
                    }
                    else if (objval == "1")
                    {
                        _player.Items[1].Use(_player.CurrentCreature);
                        _isPlayerTurn = false;
                    }
                }

            }
            else if (val == "Swap")
            {
                for (int i = 0; i < _player.Creatures.Count(); i++)
                {
                    if (_player.Creatures[i] == _player.CurrentCreature) { }
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
                        SwapCreature(_player,1);
                        _isPlayerTurn = false;
                    }
                }
            }
        }

        public void EnemyTurn()
        {
            Console.WriteLine("Au tour d'" + _enemy.CurrentCreature.CreatureName);
            _enemy.Turn(_enemy.CurrentCreature, _player.CurrentCreature);
            _isPlayerTurn = true;
        }

        public void AutoSwap(Fighter fighter,Creatures currentCreature)
        {
            currentCreature.CheckIsDead();
            if (currentCreature.State == CreatureState.DEAD)
            {
                for (int i = 0; i < fighter.Creatures.Count(); i++)
                {
                    if (currentCreature == fighter.Creatures[i] && i + 1 < fighter.Creatures.Count() && fighter.Creatures[i + 1].State != CreatureState.DEAD)
                    {
                        SwapCreature(fighter, i+1);
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
