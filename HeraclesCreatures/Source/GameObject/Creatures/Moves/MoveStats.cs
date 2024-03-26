using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    internal struct MoveStats
    {

        /*------------------------------------------------------------------------------------------*\
       |                                                                                            |
       |                                                                                            |
       |                                          Fields                                            |
       |                                                                                            |
       |                                                                                            |
       \*------------------------------------------------------------------------------------------*/

        #region Fields

        int     _power;
        float   _accuracy;
        int     _critRate;
        int     _maxPP;
        int     _manaCost;
        string  _type;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int    Power { get => _power; set => _power = value; }

        public float    Accuracy { get => _accuracy; set => _accuracy = value; }

        public int    CritRate { get => _critRate; set => _critRate = value; }

        public int    MaxPP { get => _maxPP; set => _maxPP = value; }

        public int    ManaCost { get => _manaCost; set => _manaCost = value; }

        public string   Type { get => _type; set => _type = value; }


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

        public MoveStats() 
        {
            _power = 10;
            _accuracy = 10;
            _critRate = 5;
            _maxPP = 6;
            _manaCost = 10;
            _type = "Fire";
        }

        #endregion Methods

    }
}
