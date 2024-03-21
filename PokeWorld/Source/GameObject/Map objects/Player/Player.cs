using HeraclesCreatures;

namespace HeraclesCreatures
{
    internal class Player
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        int _x;
        int _y;
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

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
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
            _x = 0;
            _y = 0;
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
