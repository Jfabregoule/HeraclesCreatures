using HeraclesCreatures;

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

        bool _isOpen;
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

        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

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
            _isOpen = false;
            _data = new ChestData();
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

        #endregion Methods

    }
}
