using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HeraclesCreatures
{
    enum PlayerChoices
    {
        Combat,
        Item,
        Swap
    }
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
        bool                _isOver;
        List<string>        _types;
        float[,]            _typeTable;
        PlayerChoices       _choiceType;
        int                 _choiceIndex;


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
            _enemy.CurrentCreature = enemy.CurrentCreature;
            _types = types;
            _typeTable = typeTable;
        }

        public void Fighting()
        {
            bool playerSwap = false;
            bool enemySwap = false;
            Console.WriteLine("Ally Creature :");
            Console.WriteLine(_player.CurrentCreature.CreatureName + " : " + _player.CurrentCreature.Stats.health + "/" + _player.CurrentCreature.Stats.maxHealth + "\n");
            Console.WriteLine("Enemy Creature :");
            Console.WriteLine(_enemy.CurrentCreature.CreatureName + " : " + _enemy.CurrentCreature.Stats.health + "/" + _enemy.CurrentCreature.Stats.maxHealth + "\n");
            CurrentTurn += 1;
            PlayerChoice();
            if (_player.CurrentCreature.Stats.AttackSpeed > _enemy.CurrentCreature.Stats.AttackSpeed)
            {
                PlayerTurn();
                if (_enemy.CurrentCreature.State == CreatureState.DEAD)
                {
                    Console.WriteLine("{0} is dead.", _enemy.CurrentCreature.CreatureName);
                    Console.WriteLine();
                    enemySwap = true;
                }
                else
                {
                    EnemyTurn();
                }
            }
            else
            {
                EnemyTurn();
                if (_player.CurrentCreature.State == CreatureState.DEAD)
                {
                    Console.WriteLine("{0} is dead.", _player.CurrentCreature.CreatureName);
                    Console.WriteLine();
                    playerSwap = true;
                }
                else
                {
                    PlayerTurn();
                }
            }
            if (playerSwap == true)
            {
                AutoSwap(_player, _player.CurrentCreature);
            }
            if (enemySwap == true)
            {
                AutoSwap(_enemy, _enemy.CurrentCreature);
            }
            CheckForStatusEffects(_player.CurrentCreature, _enemy.CurrentCreature);
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
                        Console.WriteLine("{0} is now cured.", allyCreature.CreatureName);
                        Console.WriteLine();
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
                    Console.WriteLine();
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = allyCreature.Stats;
                    creatureStats2.health -= allyCreature.Stats.maxHealth * 0.10f;
                    allyCreature.Stats = creatureStats2;
                    Console.Write(allyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
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
                        Console.WriteLine("{0} is now cured.", enemyCreature.CreatureName);
                        Console.WriteLine();
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
                    Console.WriteLine();
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = enemyCreature.Stats;
                    creatureStats2.health -= enemyCreature.Stats.maxHealth * 0.10f;
                    enemyCreature.Stats = creatureStats2;
                    Console.Write(enemyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
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

        public void PlayerChoice()
        {
            Console.WriteLine("-Combat");
            Console.WriteLine("-Items");
            Console.WriteLine("-Swap");
            Console.WriteLine();
            string val = Console.ReadLine();
            Console.WriteLine();
            if (val == "Combat")
            {
                Console.WriteLine("Choisissez votre attaque \n");
                for (int i = 0; i < _player.CurrentCreature.Moves.Count(); i++)
                {
                    if (_player.CurrentCreature.Moves[i] is Attack)
                    {
                        Console.WriteLine(i + " : " + _player.CurrentCreature.Moves[i].MoveName + " - " + _player.CurrentCreature.Moves[i].PP + " / " + _player.CurrentCreature.Moves[i].Stats.MaxPP);
                    }
                    else
                    {
                        Console.WriteLine(i + " : " + _player.CurrentCreature.Moves[i].MoveName + " - " +  "Manacost : " + _player.CurrentCreature.Moves[i].Stats.ManaCost);
                    }
                }
                Console.WriteLine();
                string combatVal = Console.ReadLine();
                Console.WriteLine("");
                if (combatVal != null)
                {
                    if (AreAllDigits(combatVal) == true && combatVal.Length > 0)
                    {
                        int moveID = int.Parse(combatVal);

                        _choiceType = PlayerChoices.Combat;
                        _choiceIndex = moveID;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not an existing move ID", combatVal);
                    }
                }
            }
            else if (val == "Items")
            {
                Console.WriteLine("Items : ");
                for (int i = 0; i < _player.Items.Count(); i++)
                {
                    Console.WriteLine(i + " : " + _player.Items[i].name);
                }

                string objval = Console.ReadLine();
                if (objval != null)
                {
                    if (AreAllDigits(objval) == true && objval.Length > 0)
                    {
                        int objID = int.Parse(objval);

                        _choiceType = PlayerChoices.Item;
                        _choiceIndex = objID;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not an existing item ID", objval);
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
                    if (AreAllDigits(swapval) == true && swapval.Length > 0)
                    {
                        int swapID = int.Parse(swapval);

                        _choiceType = PlayerChoices.Swap;
                        _choiceIndex = swapID;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not an existing creature ID", swapval);
                    }
                }
            }
        }

        public void PlayerTurn()
        {
            if (_choiceType == PlayerChoices.Combat)
            {
                
                if (_player.CurrentCreature.Moves[_choiceIndex].PP > 0 || (_player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost != 0.0f && _player.CurrentCreature.Mana >= _player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost))
                {
                    float effectiveness = Moves.GetEffectiveness(_enemy.CurrentCreature.Stats.type, _player.CurrentCreature.Moves[_choiceIndex].Stats.Type, _types, _typeTable);
                    _player.CurrentCreature.Moves[_choiceIndex].Use(_player.CurrentCreature, _enemy.CurrentCreature, effectiveness);
                }
                else
                {
                    if (_player.CurrentCreature.Moves[_choiceIndex].PP <= 0 && _player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost == 0.0f)
                    {
                        Console.WriteLine("{0} has no PP left.", _player.CurrentCreature.Moves[_choiceIndex].MoveName);
                    }
                    else
                    {
                        Console.WriteLine("{0} has not enough mana to cast {1}.", _player.CurrentCreature.CreatureName, _player.CurrentCreature.Moves[_choiceIndex].MoveName);
                    }
                }
            }
            else if (_choiceType == PlayerChoices.Item)
            {
                _player.Items[_choiceIndex].Use(_player.CurrentCreature);

            }
            else if (_choiceType == PlayerChoices.Swap)
            {
                SwapCreature(_player, _choiceIndex);
            }
        }

        public void EnemyTurn()
        {
            _enemy.Turn(_enemy.CurrentCreature, _player.CurrentCreature);
        }

        public void AutoSwap(Fighter fighter,Creatures currentCreature)
        {
            for (int i = 0; i < fighter.Creatures.Count(); i++)
            {
                if (currentCreature == fighter.Creatures[i] && i + 1 < fighter.Creatures.Count() && fighter.Creatures[i + 1].State != CreatureState.DEAD)
                {
                    Console.WriteLine("{0} swapped {1} with {2}.", fighter.Name, currentCreature.CreatureName, fighter.Creatures[i + 1].CreatureName);
                    Console.WriteLine();
                    SwapCreature(fighter, i+1);
                    break;
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
