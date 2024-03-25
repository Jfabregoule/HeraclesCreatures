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

        float _power;
        float _accuracy;
        float _critRate;
        float _maxPP;
        float _PP;
        float _manaCost;
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

        public float    Power { get => _power; set => _power = value; }

        public float    Accuracy { get => _accuracy; set => _accuracy = value; }

        public float    CritRate { get => _critRate; set => _critRate = value; }

        public float    MaxPP { get => _maxPP; set => _maxPP = value; }

        public float    PP { get => _PP; set => _PP = value; }

        public float    ManaCost { get => _manaCost; set => _manaCost = value; }

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
            _PP = 6;
            _manaCost = 10;
            _type = "Fire";
        }

        #endregion Methods

    }
}
