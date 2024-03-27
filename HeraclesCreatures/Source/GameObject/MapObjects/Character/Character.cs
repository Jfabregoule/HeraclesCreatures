using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class Character : MapObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Scene _currentScene;
        CharacterData _data;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public Scene CurrentScene { get => _currentScene; set => _currentScene = value; }
        public CharacterData Data { get => _data; set => _data = value; }

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

        public Character()
        {
            _currentScene = new();
            _data = new();
        }

        public Character(Dictionary<string,Scene> scenes, CharacterData data)
        {
            _currentScene = scenes["FirstScene"];
            _data = data;
        }

        public void Move(int dir)
        {
            switch (dir)
            {
                case 0:
                    if (X - 1 >= 0 && CurrentScene.Cells[X - 1, Y].Tile.IsWalkable)
                        X -= 1;
                    break;
                case 1:
                    if (Y + 1 < CurrentScene.Width && CurrentScene.Cells[X,Y + 1].Tile.IsWalkable)
                        Y += 1;
                    break;
                case 2:
                    if (X + 1 < CurrentScene.Height && CurrentScene.Cells[X + 1, Y].Tile.IsWalkable)
                        X += 1;
                    break;
                case 3:
                    if (Y - 1 >= 0 && CurrentScene.Cells[X, Y - 1].Tile.IsWalkable)
                        Y -= 1;
                    break;
            }
        }

        #endregion Methods

    }
}
