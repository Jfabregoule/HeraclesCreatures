using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraclesCreatures.Source.GameObject;
using HeraclesCreatures.Source.GameObject.Creatures;
using HeraclesCreatures.Source.GameObject.Items;

namespace HeraclesCreatures.GameObject
{
    internal class Player : MapObject
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
        List<Creatures> _creatures;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        internal List<Items> Items { get => _items; set => _items = value; }
        internal List<Creatures> Creatures { get => _creatures; set => _creatures = value; }


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
            _items = new List<Items>();
            _creatures = new List<Creatures>();
        }
        public void AddCreature(Creatures creatures)
        {
            _creatures.Add(creatures);
        }

        public void AddItems(Items items)
        {
            _items.Add(items);
        }

        #endregion Methods

    }
}
