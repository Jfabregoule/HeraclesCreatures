using HeraclesCreatures;

namespace HeraclesCreatures
{
    internal struct AttackStats
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        // Stats
        int _power;
        int _accuracy;
        int _critRate;
        int _maxPP;
        string _type;

        // Buffs
        int _bAttack;
        int _bMagicPower;
        int _bDefense;
        int _bMana;
        int _bSpeed;

        // Debuffs
        int _dAttack;
        int _dMagicPower;
        int _dDefense;
        int _dMana;
        int _dSpeed;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int Power { get => _power; set => _power = value; }
        public int Accuracy { get => _accuracy; set => _accuracy = value; }
        public int CritRate { get => _critRate; set => _critRate = value; }
        public int MaxPP { get => _maxPP; set => _maxPP = value; }
        public string Type { get => _type; set => _type = value; }
        public int BAttack { get => _bAttack; set => _bAttack = value; }
        public int BMagicPower { get => _bMagicPower; set => _bMagicPower = value; }
        public int BDefense { get => _bDefense; set => _bDefense = value; }
        public int BMana { get => _bMana; set => _bMana = value; }
        public int BSpeed { get => _bSpeed; set => _bSpeed = value; }
        public int DAttack { get => _dAttack; set => _dAttack = value; }
        public int DMagicPower { get => _dMagicPower; set => _dMagicPower = value; }
        public int DDefense { get => _dDefense; set => _dDefense = value; }
        public int DMana { get => _dMana; set => _dMana = value; }
        public int DSpeed { get => _dSpeed; set => _dSpeed = value; }

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

        public AttackStats() 
        {
        }

        #endregion Methods

    }
}
