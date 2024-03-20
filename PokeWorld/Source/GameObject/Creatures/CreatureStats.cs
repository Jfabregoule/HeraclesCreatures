public struct CreatureStats
{
    /*------------------------------------------------------------------------------------------*\
    |                                                                                            |
    |                                                                                            |
    |                                          Fields                                            |
    |                                                                                            |
    |                                                                                            |
    \*------------------------------------------------------------------------------------------*/

    #region Fields

    int _hp;
    int _attack;
    int _magicpower;
    int _defense;
    int _mana;
    string _type;

    #endregion Fields


    /*------------------------------------------------------------------------------------------*\
    |                                                                                            |
    |                                                                                            |
    |                                        Properties                                          |
    |                                                                                            |
    |                                                                                            |
    \*------------------------------------------------------------------------------------------*/

    #region Properties

    public int health {  get { return _hp; } private set { _hp = value; } }
    public int attack { get { return _attack; } private set { _attack = value; } }
    public int magicpower { get { return _magicpower; } private set { _magicpower = value; } }
    public int defense { get { return _defense; } private set { _defense = value; } }
    public int mana { get { return _mana; } private set { _mana = value; } }
    public string type { get { return _type; } private set { _type = value; } }

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

    public CreatureStats()
    {
        _hp = 0;
        _attack = 0;
        _magicpower = 0;
        _defense = 0;
        _mana = 0;
        _type = string.Empty;
    }

    #endregion Methods
}