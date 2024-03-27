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
        public List<Moves> Moves { get => _moves; private set => _moves = value; }
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

        public Creatures(CreatureData creatureData)
        {
            _creatureName = creatureData._creatureName;
            _stats = creatureData._stats;
            _state= creatureData._state;
            _mana = creatureData._mana;
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
            CheckIsDead();
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

        public CreatureData GetCreatureData()
        {
            CreatureData data = new CreatureData();

            data._creatureName = _creatureName;
            data._mana = _mana;
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
