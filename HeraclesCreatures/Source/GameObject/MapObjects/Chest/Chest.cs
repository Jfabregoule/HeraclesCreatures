﻿using HeraclesCreatures;

namespace HeraclesCreatures
{
    public class Chest : MapObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        ChestData _data;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public ChestData Data { get => _data; set => _data = value; }

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

        public Chest()
        {
            _data = new ChestData();
        }

        public Chest(int x, int y, List<Items> content, List<int> quantities, List<Creatures> creatures, ChestData chestData)
        {
            X = x;
            Y = y;
            chestData.Content = content;
            chestData.Quantity = quantities;
            chestData.Creatures = creatures;
            _data = chestData;
        }

        public void AddItem(Items item, int quantity)
        {
            _data.AddItem(item, quantity);
        }

        public void RemoveItem(Items item)
        {
            _data.RemoveItem(item);
        }

        public void EditQuantity(Items item, int newQuantity)
        {
            _data.EditQuantity(item, newQuantity);
        }

        public List<Items> GetItems()
        {
            List<Items> items = new List<Items>();
            for (int i = 0; i < Data.Content.Count; i++)
            {
                for (int j = 0; j < Data.Quantity[i]; j++)
                {
                    if (Data.Content[i] is Potion)
                    {
                        items.Add(new Potion());
                    }
                    else if (Data.Content[i] is Revive)
                    {
                        items.Add(new Revive());
                    }
                    else if (Data.Content[i] is AttackPlus)
                    {
                        items.Add(new AttackPlus());
                    }
                    else if (Data.Content[i] is SpeedPlus)
                    {
                        items.Add(new SpeedPlus());
                    }
                }
            }
            return items;
        }

        public void OpenChest()
        {
            Data.Content.Clear();
            Data.Quantity.Clear();
            Data.Creatures.Clear();
            IsActive = false;
        }

        public override void PlayDialogue(Scene currentScene)
        {
            InputManager inputManager = new InputManager();
            base.PlayDialogue(currentScene);
            for (int i = 0;i < Data.Content.Count; i++)
            {
                bool check = false;
                Console.Write("You Obtained : " + Data.Content[i].name + " X " + Data.Quantity[i] + "!");
                do
                {
                    inputManager.Update();
                    check = inputManager.GetKeyDown(ConsoleKey.Enter);
                } while (check == false);
                ClearDialogue(currentScene);
            }
            for (int i = 0; i < Data.Creatures.Count; i++)
            {
                bool check = false;
                Console.Write(Data.Creatures[i].CreatureName + " joined your team !");
                do
                {
                    inputManager.Update();
                    check = inputManager.GetKeyDown(ConsoleKey.Enter);
                } while (check == false);
                ClearDialogue(currentScene);
            }
        }


        #endregion Methods

    }
}
