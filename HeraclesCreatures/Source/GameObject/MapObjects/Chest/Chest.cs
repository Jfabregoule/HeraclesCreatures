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
            Dialogue = new string[_data.Content.Count];
        }

        public Chest(int x, int y, List<Items> content, List<int> quantities, ChestData chestData)
        {
            X = x;
            Y = y;
            chestData.Content = content;
            chestData.Quantity = quantities;
            _data = chestData;
            Dialogue = new string[_data.Content.Count];
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
            IsActive = false;
        }

        public override void PlayDialogue(Scene currentScene)
        {
            base.PlayDialogue(currentScene);
            for (int i = 0;i < Data.Content.Count; i++)
            {
                Console.Write("You Obtained : " + Data.Content[i].Name + " X " + Data.Quantity[i]+"\n");
            }

        }


        #endregion Methods

    }
}
