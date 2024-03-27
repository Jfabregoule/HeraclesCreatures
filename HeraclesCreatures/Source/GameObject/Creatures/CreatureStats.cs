namespace HeraclesCreatures
{
    [Serializable]
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

        float   _hp;
        float   _maxhp;
        float   _attack;
        float   _magicpower;
        float   _defense;
        int     _maxmana;
        float   _attackSpeed;
        string  _type;

        #endregion Fields


        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public float health { get { return _hp; } set { _hp = value; } }
        public float maxHealth { get => _maxhp; set => _maxhp = value; }
        public float attack { get { return _attack; } set { _attack = value; } }
        public float magicpower { get { return _magicpower; } set { _magicpower = value; } }
        public float defense { get { return _defense; } set { _defense = value; } }
        public int maxMana { get => _maxmana; set => _maxmana = value; }
        public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
        public string type { get { return _type; } set { _type = value; } }




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
            _attackSpeed = 0;
            _type = "Water";
        }


        public void Regen(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            if (health + amount > maxHealth)
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

        public void Damaged(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            health -= amount;
            if (health < 0)
            {
                health = 0;
            }
        }

        public void AttackBoost(float amount) 
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            attack += amount;
        }

        public void SpeedBoost(float amount)
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