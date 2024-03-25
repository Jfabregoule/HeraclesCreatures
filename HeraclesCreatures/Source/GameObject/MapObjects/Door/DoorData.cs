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

        string _targetSceneName;
        bool _isLocked;
        int _arrivalX;
        int _arrivalY;
        MapObjectData _mapData;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public string TargetSceneName { get => _targetSceneName; set => _targetSceneName = value; }
        public bool IsLocked { get => _isLocked; set => _isLocked = value; }
        public int ArrivalX { get => _arrivalX; set => _arrivalX = value; }
        public int ArrivalY { get => _arrivalY; set => _arrivalY = value; }
        internal MapObjectData MapData { get => _mapData; set => _mapData = value; }

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
            _targetSceneName = "";
            _isLocked = false;
            _arrivalX = 0;
            _arrivalY = 0;
            _mapData = new MapObjectData();
        }

        #endregion Methods

    }
}
