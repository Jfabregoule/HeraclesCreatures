using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

    enum CreatureState
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
    [Serializable]
    internal class Creatures
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
        int                 _mana;

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
        internal List<Moves> Moves { get => _moves; private set => _moves = value; }
        public CreatureStats Stats { get => _stats; set => _stats = value; }
        public CreatureState State { get => _state; set => _state = value; }
        public int Mana { get => _mana; set => _mana = value; }

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
            Mana = stats.maxMana;
            foreach (Moves move in moves)
            {
                if (move is Spell)
                {
                    Spell spell = (Spell)move;
                    spell.UsedSpell += RemoveMana;
                }
            }
        }
        public void AddMove(Moves move) 
        {
            _moves.Add(move);
        }

        public void RemoveMana(int manaCost)
        {
            Mana -= manaCost;
        }

        public void TakeDamage(float damage)
        {
            _stats.Damaged(damage);
        }

        public void Heal(float value)
        {
            _stats.Regen(value);
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


        #endregion Methods

    }

    #endregion Creatures Class

}
