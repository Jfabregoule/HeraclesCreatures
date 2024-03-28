using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

    public enum CreatureState
    {
        ALIVE,
        NUMB,
        BURNED,
        SOAKED,
        WITHERED,
        RUSTED,
        DIZZY,
        SHAKEN,
        BLINDED,
        ERODED,
        POISONED,
        SCARED,
        DEAD
    }

    #region Creatures Class
    public class Creatures
    {
        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        string              _creatureName;
        List<Moves>         _moves;
        CreatureState       _state;
        CreatureStats       _stats;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public string CreatureName { get => _creatureName; private set => _creatureName = value; }
        public List<Moves> Moves { get => _moves; private set => _moves = value; }
        public CreatureStats Stats { get => _stats; set => _stats = value; }
        public CreatureState State { get => _state; set => _state = value; }

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

        public Creatures(string CreatureName, CreatureStats stats, List<Moves> moves)
        {
            _creatureName = CreatureName;
            _stats = stats;
            _moves = moves;
            _state = CreatureState.ALIVE;
            Stats.SetMana(stats.maxMana);
            foreach (Moves move in moves)
            {
                if (move is Spell)
                {
                    Spell spell = (Spell)move;
                    spell.UsedSpell += RemoveMana;
                }
            }
        }

        public Creatures(CreatureData creatureData)
        {
            _creatureName = creatureData._creatureName;
            _stats = creatureData._stats;
            _state= creatureData._state;
            List<Moves> moves = new List<Moves>();
            for (int i = 0; i < creatureData._moveData.Count; i++)
            {
                Moves move;
                if (creatureData._moveData[i]._stats.ManaCost != 0)
                {
                    move = new Spell(creatureData._moveData[i]);
                }
                else
                {
                    move = new Attack(creatureData._moveData[i]);
                }
                
                moves.Add(move);
            }
            _moves = moves;
        }

        public void ResetCreature()
        {
            State = CreatureState.ALIVE;
            FullHeal();
            RefillMana();
        }

        public void XpGain(int gain)
        {
            Stats.GetXp(gain);
            Console.WriteLine(CreatureName + " has gained " + gain + " XP");
            Console.WriteLine(Stats.CurrentXp + " / " + Stats.XpNeeded);
            CheckLvlUp();
        }

        public void CheckLvlUp()
        {
            if(Stats.CurrentXp >= Stats.XpNeeded)
            {
                Stats.LevelUp();
                Console.WriteLine(CreatureName + " has leveled up");
                Console.WriteLine(CreatureName + "Current level : " + Stats.Level);
                Stats.ResetCurrentXp();
                Console.WriteLine("Xp needed :" + Stats.CurrentXp + " / " + Stats.XpNeeded);
            }
        }

        public void AddMove(Moves move) 
        {
            _moves.Add(move);
        }

        public void RemoveMana(int manaCost)
        {
            Stats.RemoveMana(manaCost);
        }

        public void AddMana(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException(nameof(value));
            }
            if (Stats.Mana + value > Stats.maxMana)
            {
                Stats.SetMana(Stats.maxMana);
            }
            else if (value == Stats.maxMana)
            {
                Stats.SetMana(value);
            }
            else
            {
                Stats.SetMana(Stats.Mana + value);
            }
        }

        private void RefillMana()
        {
            Stats.SetMana(Stats.maxMana);
        }

        public void TakeDamage(int damage)
        {
            _stats.Damaged(damage);
            CheckIsDead();
        }

        public void Heal(float value)
        {
            _stats.Regen(value);
        }

        public void FullHeal()
        {
            _stats.FullRegen();
        }

        public void BoostAttack(float attack)
        {
            _stats.AttackBoost(attack);
        }

        public void BoostSpeed(float speed)
        {
            _stats.SpeedBoost(speed);
        }


        public void CheckIsDead()
        {
            if(_stats.health<= 0)
            {
                _state = CreatureState.DEAD;
            }
        }

        public CreatureData GetCreatureData()
        {
            CreatureData data = new CreatureData();

            data._creatureName = _creatureName;
            data._stats = _stats;
            data._state = _state;
            List<MoveData> movesData = new List<MoveData>();
            foreach(Moves move in _moves)
            {
                movesData.Add(move.GetMoveData());
            }
            data._moveData = movesData;
            return data;
        }

        #endregion Methods

    }

    #endregion Creatures Class

}
