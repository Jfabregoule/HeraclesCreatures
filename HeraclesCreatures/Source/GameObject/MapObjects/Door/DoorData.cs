using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal struct DoorData
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Scene _targetScene;
        bool _isLocked;
        int _arrivalX;
        int _arrivalY;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public Scene TargetScene { get => _targetScene; set => _targetScene = value; }
        public bool IsLocked { get => _isLocked; set => _isLocked = value; }
        public int ArrivalX { get => _arrivalX; set => _arrivalX = value; }
        public int ArrivalY { get => _arrivalY; set => _arrivalY = value; }

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

        public DoorData()
        {
            _targetScene = new Scene();
            _isLocked = false;
            _arrivalX = 0;
            _arrivalY = 0;
        }

        #endregion Methods

    }
}
