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

        public void TakeDamage(int damage)
        {
            _stats.health -= damage;
        }

        public void Heal(int value)
        {
            if(_stats.health + value >= _stats.maxHealth)
            {
                _stats.health = _stats.maxHealth;
            }
            else
            {
                _stats.health += value; 
            }
        }

        public void AddMove(Moves move) 
        {
            _moves.Add(move);
        }

        //internal void AddMove(ref Attack attack)
        //{
        //    _moves.Add(attack);
        //}

        public bool IsDead()
        {
            if(_stats.health<= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion Methods

    }

    #endregion Creatures Class

}
