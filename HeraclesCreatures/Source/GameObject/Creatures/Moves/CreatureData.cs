using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public struct CreatureData
    {
        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        [JsonInclude] public string _creatureName;
        [JsonInclude] public List<MoveData> _moveData;
        [JsonInclude] public CreatureState _state;
        [JsonInclude] public CreatureStats _stats;
        [JsonInclude] public int _mana;

        #endregion Fields

    }
}
