using System;
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

        public void AddItems(Items items)
        {
            _items.Add(items);
        }

        #endregion Methods

    }
}
