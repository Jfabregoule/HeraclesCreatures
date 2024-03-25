using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal struct ChestData
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        List<Items> _content;
        List<int> _quantity;
        MapObjectData _mapData;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        internal List<Items> Content { get => _content; set => _content = value; }
        internal List<int> Quantity { get => _quantity; set => _quantity = value; }
        internal MapObjectData MapData { get => _mapData; set => _mapData = value; }

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

        public ChestData()
        {
            _content = new List<Items> {};
            _quantity = new List<int> {};
            _mapData = new MapObjectData();
        }

        public void AddItem(Items item, int quantity)
        {
            _content.Add(item);
            _quantity.Add(quantity);
        }

        public void RemoveItem(Items item)
        {
            int index = _content.IndexOf(item);

            if (index != -1)
            {
                _content.Remove(item);
                _quantity.RemoveAt(index);
            }
            else
            {
                return;
            }
        }

        public void EditQuantity(Items item, int newQuantity)
        {
            int index = _content.IndexOf(item);

            if (index != -1)
            {
                _quantity[index] = newQuantity;
            }
            else
            {
                return;
            }
        }

        #endregion Methods

    }
}
