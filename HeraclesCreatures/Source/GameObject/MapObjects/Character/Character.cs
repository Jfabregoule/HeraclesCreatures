﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public Character(Scene scene, CharacterData data)
        {
            _currentScene = scene;
            _data = data;
        }

        public MapObject Move(int dir)
        {
            MapObject mapObject = new MapObject();
            switch (dir)
            {
                case 0:
                    if (X - 1 >= 0 && CurrentScene.Cells[X - 1, Y].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X-1, Y))
                        {
                            return CurrentScene.SceneObjects[X-1, Y];
                        }
                        else
                        {
                            X -= 1;
                        }
                    break;
                case 1:
                    if (Y + 1 < CurrentScene.Width && CurrentScene.Cells[X,Y + 1].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X, Y + 1))
                        {
                            return CurrentScene.SceneObjects[X, Y + 1];
                        }
                        else
                        {
                            Y += 1;
                        }
                    break;
                case 2:
                    if (X + 1 < CurrentScene.Height && CurrentScene.Cells[X + 1, Y].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X + 1, Y))
                        {
                            return CurrentScene.SceneObjects[X + 1, Y];
                        }
                        else
                        {
                            X += 1;
                        }
                    break;
                case 3:
                    if (Y - 1 >= 0 && CurrentScene.Cells[X, Y - 1].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X, Y - 1))
                        {
                            return CurrentScene.SceneObjects[X, Y - 1];
                        }
                        else
                        {
                            Y -= 1;
                        }
                    break;
                default:
                    break;
            }
            return new();
        }

        public object Interact(MapObject mapObject, List<string> types, float[,] typeTable)
        {
            if (mapObject.IsActive)
            {
                if (mapObject is Chest)
                {
                    //Return Chest
                }
                else if (mapObject is Door)
                {
                    //Return Scene
                }
                else if (mapObject is Opponent)
                {
                    Opponent opponent = (Opponent)mapObject;
                    CombatManager combatManager = new CombatManager(Data.Player, opponent.Data.Enemy, types, typeTable);
                    return combatManager;
                }
            }
            return new();
        }

        #endregion Methods

    }
}
