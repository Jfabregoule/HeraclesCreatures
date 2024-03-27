using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public struct MoveData
    {
        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        [JsonInclude] public string _moveName;
        [JsonInclude] public MoveStats _stats;
        [JsonInclude] public int _pp;

        #endregion Fields

    }
}
