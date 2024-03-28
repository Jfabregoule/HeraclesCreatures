using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Character(int x, int y, Scene scene, CharacterData data, Player player)
        {
            X = x;
            Y = y;
            _currentScene = scene;
            data.Player = player;
            Data = data;
        }

        public MapObject Move(int dir)
        {
            switch (dir)
            {
                case 0:
                    if (X - 1 >= 0 && CurrentScene.Cells[X - 1, Y].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X-1, Y))
                        {
                            MapObject mapObject = CurrentScene.SceneObjects[X - 1, Y];
                            return mapObject;
                        }
                        else
                        {
                            CurrentScene.ToRemove.Add(new int[] { X, Y });
                            X -= 1;
                        }
                    break;
                case 1:
                    if (Y + 1 < CurrentScene.Width && CurrentScene.Cells[X,Y + 1].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X, Y + 1))
                        {
                            MapObject mapObject = CurrentScene.SceneObjects[X, Y + 1];
                            return mapObject;
                        }
                        else
                        {
                            CurrentScene.ToRemove.Add(new int[] { X, Y });
                            Y += 1;
                        }
                    break;
                case 2:
                    if (X + 1 < CurrentScene.Height && CurrentScene.Cells[X + 1, Y].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X + 1, Y))
                        {
                            MapObject mapObject = CurrentScene.SceneObjects[X + 1, Y];
                            return mapObject;
                        }
                        else
                        {
                            CurrentScene.ToRemove.Add(new int[] { X, Y });
                            X += 1;
                        }
                    break;
                case 3:
                    if (Y - 1 >= 0 && CurrentScene.Cells[X, Y - 1].Tile.IsWalkable)
                        if (CurrentScene.IsMapObject(X, Y - 1))
                        {
                            MapObject mapObject = CurrentScene.SceneObjects[X, Y - 1];
                            return mapObject;
                        }
                        else
                        {
                            CurrentScene.ToRemove.Add(new int[] { X, Y });
                            Y -= 1;
                        }
                    break;
                default:
                    break;
            }
            return null;
        }

        public object Interact(MapObject mapObject, Dictionary<string, Scene> scenes, List<string> types, float[,] typeTable)
        {
            if (mapObject.IsActive)
            {
                if (mapObject is Chest)
                {
                    Chest chest = (Chest)mapObject;
                    Data.Player.Items.AddRange(chest.GetItems());
                    Data.Player.Creatures.AddRange(chest.Data.Creatures);
                    chest.OpenChest();
                    return chest;
                }
                else if (mapObject is Door)
                {
                    Door door = (Door)mapObject;
                    CurrentScene.RemoveAllCharacters();
                    ChangeScene(scenes[door.Data.TargetSceneName], door.Data.ArrivalX, door.Data.ArrivalY);
                    return scenes[door.Data.TargetSceneName];
                }
                else if (mapObject is Opponent)
                {
                    Opponent opponent = (Opponent)mapObject;
                    CombatManager combatManager = new CombatManager(Data.Player, opponent.Data.Enemy, types, typeTable);
                    return combatManager;
                }
                else if (mapObject is HealingStand)
                {
                    HealingStand healingStand = (HealingStand)mapObject;
                    return healingStand;
                }
                else if (mapObject is Grass)
                {
                    Grass grass = (Grass)mapObject;
                    bool triggerFight = grass.IsEncounter();
                    CurrentScene.ToAdd.Add(grass);
                    CurrentScene.ToRemove.Add(new int[] { X, Y });
                    X = grass.X; 
                    Y = grass.Y;
                    if (triggerFight)
                    {
                        return grass;
                    }
                }
            }
            return new();
        }

        public void ChangeScene(Scene newScene, int x, int y)
        {
            CurrentScene = newScene;
            X = x;
            Y = y;
        }

        #endregion Methods

    }
}
