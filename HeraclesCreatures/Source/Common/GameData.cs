using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public struct GameData
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        [JsonInclude] public PlayerData  _playerData;
        [JsonInclude] public GamePhase   _phase;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Methods                                           |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public GameData(Player player, GamePhase phase) 
        {
            _playerData = player.GetPlayerData();
            _phase = phase;
        }

        #endregion Methods

    }
}
