using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.GameObject.Creatures.Moves
{
    internal struct AttackStats
    {

        /*------------------------------------------------------------------------------------------*\
       |                                                                                            |
       |                                                                                            |
       |                                          Fields                                            |
       |                                                                                            |
       |                                                                                            |
       \*------------------------------------------------------------------------------------------*/

        #region Fields

        int _power;
        int _accuracy;
        int _critRate;
        int _maxPP;
        string _type;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int Power { get => _power; private set => _power = value; }

        public int Accuracy { get => _accuracy; private set => _accuracy = value; }

        public int CritRate { get => _critRate; private set => _critRate = value; }

        public int MaxPP { get => _maxPP; private set => _maxPP = value; }

        public string Type { get => _type; private set => _type = value; }

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

        public AttackStats() 
        {
            _power = 0;
            _accuracy = 0;
            _critRate = 0;
            _maxPP = 0;
        }

        #endregion Methods

    }
}
