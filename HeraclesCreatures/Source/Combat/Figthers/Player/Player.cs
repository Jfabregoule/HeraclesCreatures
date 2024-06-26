﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public class Player : Fighter
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        List<Items> _items;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public List<Items> Items { get => _items; set => _items = value; }
       

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Player()
        {
            _items = new();
        }

        public Player(string name, List<Creatures> team)
        {
            Name  = name;
            _items = new List<Items>();
            Creatures = team;
            if (Creatures.Count > 0)
            {
                CurrentCreature = Creatures[0];
            }
        }

        public Player(PlayerData playerData) 
        {
            List<Creatures> creaturesList = new List<Creatures>();
            for (int i = 0; i < playerData._creatures.Count; i++) 
            {
                Creatures creature = new Creatures(playerData._creatures[i]);
                creaturesList.Add(creature);
            }
            List<Items> itemsList = new List<Items>();
            for (int i = 0;i < playerData._items.Count;i++)
            {
                Type t = Type.GetType("HeraclesCreatures." + playerData._items[i]._itemName);
                Items item = (Items)Activator.CreateInstance(t);
                itemsList.Add(item);
            }
            Creatures = creaturesList;
            Items = itemsList;
            CurrentCreature = Creatures[playerData._currentCreatureID];
            Name = playerData._name;
        }

        public void AddItems(Items items)
        {
            _items.Add(items);
        }

        public PlayerData GetPlayerData()
        {
            PlayerData playerData = new PlayerData();

            playerData._name = Name;
            playerData._currentCreatureID = Creatures.IndexOf(CurrentCreature);
            List<CreatureData> creatureDatas = new List<CreatureData>();
            foreach (Creatures creature in Creatures)
            {
                creatureDatas.Add(creature.GetCreatureData());
            }
            List<ItemData> itemDatas = new List<ItemData>();
            foreach(Items item in Items)
            {
                itemDatas.Add(item.GetItemsData());
            }
            playerData._creatures = creatureDatas;
            playerData._items = itemDatas;
            return playerData;
        }

        #endregion Methods

    }
}
