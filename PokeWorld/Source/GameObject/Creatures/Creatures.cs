using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

    #region Creatures Class

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
        bool                _isDead;
        CreatureStats       _Stats;

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
        public CreatureStats Stats { get => _stats; private set => _stats = value; }

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

        public Creatures(string CreatureName)
        {
            _creatureName = CreatureName;
            _stats = new CreatureStats();
            _moves = new List<Moves>();
        }
        public void AddMove(Moves move) 
        {
            _moves.Add(move);
        }

        public void TakeDamage(int damage)
        {
            _Stats.Damaged(damage);
        }

        public void Heal(int value)
        {
            _Stats.Regen(value);
        }

        public void BoostAttack(int attack)
        {
            _Stats.AttackBoost(attack);
        }

        public void BoostSpeed(int speed)
        {
            _Stats.SpeedBoost(speed);
        }


        public void IsDead()
        {
            if(_stats.health<= 0)
            {
                _isDead = true;
            }
            else
            {
                _isDead = false;
            }
        }


        #endregion Methods

    }

    #endregion Creatures Class

}
