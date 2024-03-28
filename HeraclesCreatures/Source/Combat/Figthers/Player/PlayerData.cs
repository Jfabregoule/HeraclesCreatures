using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HeraclesCreatures
{
    [Serializable]
    public struct PlayerData
    {

        /*------------------------------------------------------------------------------------------*\
       |                                                                                            |
       |                                                                                            |
       |                                          Fields                                            |
       |                                                                                            |
       |                                                                                            |
       \*------------------------------------------------------------------------------------------*/

        #region Fields

        [JsonInclude] public string                 _name;
        [JsonInclude] public int                    _currentCreatureID;
        [JsonInclude] public List<CreatureData>     _creatures;
        [JsonInclude] public List<ItemData>            _items;

        #endregion Fields

    }
}
