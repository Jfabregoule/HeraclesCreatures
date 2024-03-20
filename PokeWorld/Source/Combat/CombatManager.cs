using HeraclesCreatures.Source.Combat.Figthers.Enemy;
using HeraclesCreatures.Source.Combat.Figthers.Player;
using HeraclesCreatures.Source.GameObject.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.Combat
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

        public CombatManager(Player player, Enemy enemy) 
        {
            _player = player;
            _enemy = enemy;
            _currentTurn = 0;
            _isPlayerTurn = false;
            _currentPlayerCreature = player.Creatures[0];
            _currentEnemyCreature = enemy.Creatures[0];
        }

        public void StartFight() 
        {
            if(_currentPlayerCreature.Stats.AttackSpeed > _currentEnemyCreature.Stats.AttackSpeed) 
            { 
                _isPlayerTurn |= true;
            }
            if(_isPlayerTurn)
            {
                for (int i = 0; i < _currentPlayerCreature.Moves.Count(); i++)
                {
                    Console.WriteLine(_currentPlayerCreature.Moves[i]);
                }
                Console.ReadLine();
                _isPlayerTurn = false;
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
