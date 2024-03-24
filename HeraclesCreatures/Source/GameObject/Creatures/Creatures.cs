using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

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
        bool                _isDead;
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
        internal List<Moves> Moves { get => _moves; private set => _moves = value; }
        public CreatureStats Stats { get => _stats; private set => _stats = value; }
        public bool IsDead { get => _isDead; private set => _isDead = value; }

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
        }
        public void AddMove(Moves move) 
        {
            _moves.Add(move);
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
                IsDead = true;
            }
            else
            {
                IsDead = false;
            }
        }


        #endregion Methods

    }

    #endregion Creatures Class

}
