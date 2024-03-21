namespace HeraclesCreatures
{
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
        int _maxhp;
        int _attack;
        int _magicpower;
        int _defense;
        int _mana;
        int _maxmana;
        int _attackSpeed;
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

        public int health { get { return _hp; } set { _hp = value; } }
        public int maxHealth { get => _maxhp; set => _maxhp = value; }
        public int attack { get { return _attack; } set { _attack = value; } }
        public int magicpower { get { return _magicpower; } private set { _magicpower = value; } }
        public int defense { get { return _defense; } private set { _defense = value; } }
        public int maxMana { get => _maxmana; set => _maxmana = value; }
        public int mana { get { return _mana; } private set { _mana = value; } }
        public int AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
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
            _hp = 100;
            _maxhp = 100;
            _attack = 0;
            _magicpower = 0;
            _defense = 0;
            _maxmana = 0;
            _mana = 0;
            _attackSpeed = 0;
            _type = string.Empty;
        }


        public void Regen(int amount)
        {
            if(health + amount > maxHealth)
            {
                health = maxHealth;
            }
            else if(amount == maxHealth)
            {
                health = amount;
            }
            else
            {
                health += amount;
            }
        }

        public void Damaged(int amount)
        {
            health -= amount;
        }

        public void AttackBoost(int amount) 
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            attack += amount;
        }

        public void SpeedBoost(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
           AttackSpeed += amount;
        }
        #endregion Methods
    }
}