using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class CombatManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Player              _player;
        Enemy               _enemy;
        int                 _currentTurn;
        bool                _isPlayerTurn;
        Creatures           _currentPlayerCreature;
        Creatures           _currentEnemyCreature;
        List<string>        _types;
        float[,]            _typeTable;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties



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

        public CombatManager(Player player, Enemy enemy, List<string> types, float[,] typeTable) 
        {
            _player = player;
            _enemy = enemy;
            _currentTurn = 0;
            _isPlayerTurn = false;
            _currentPlayerCreature = player.Creatures[0];
            _currentEnemyCreature = enemy.Creatures[0];
            _types = types;
            _typeTable = typeTable;
        }

        public void StartFight() 
        {
            if(_currentPlayerCreature.Stats.AttackSpeed > _currentEnemyCreature.Stats.AttackSpeed) 
            { 
                _isPlayerTurn = true;
            }

        }

        public void Fighting()
        {
            if (_isPlayerTurn)
            {
                for (int i = 0; i < _currentPlayerCreature.Moves.Count(); i++)
                {
                    Console.WriteLine(_currentPlayerCreature.Moves[i].MoveName);
                }
                Console.ReadLine();
                _isPlayerTurn = false;
            }
            if (_isPlayerTurn == false)
            {
                Console.WriteLine(_currentEnemyCreature.Stats.health);
                Console.WriteLine(_currentPlayerCreature.Stats.health);
                _enemy.EasyTurn(_currentEnemyCreature, _currentPlayerCreature);
                Console.WriteLine(_currentEnemyCreature.Stats.health);
                Console.WriteLine(_currentPlayerCreature.Stats.health);
                _isPlayerTurn = true;
            }
        }

        public void SwapCreature(string creatureName) 
        {
            for (int i = 0; i < _player.Creatures.Count(); i++)
            {
                if (_player.Creatures[i].CreatureName == creatureName)
                {
                    _currentPlayerCreature = _player.Creatures[i];
                }
            }
        }



        #endregion Methods

    }
}
