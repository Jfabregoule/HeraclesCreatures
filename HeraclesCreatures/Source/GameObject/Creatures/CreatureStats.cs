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
        int     _level;
        int     _mana;
        int     _xpNeeded;
        int     _currentXp;

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
        public int Level { get => _level; set => _level = value; }
        public int XpNeeded { get => _xpNeeded; set => _xpNeeded = value; }
        public int CurrentXp { get => _currentXp; set => _currentXp = value; }
        public int Mana { get => _mana; set => _mana = value; }




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
            _mana = 0;
            _currentXp = 0;
            _level = 0;
            _xpNeeded = 0;
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
            else if (amount == maxHealth)
            {
                health = amount;
            }
            else
            {
                health += amount;
            }
        }

        public void RemoveMana(int amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            Mana -= amount;
        }

        public void SetMana(int amount)
        {
            Mana = amount;
        }

        public void FullRegen()
        {
            health = maxHealth;
        }
        public void GetXp(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
            _currentXp += amount;
        }
        public void ResetCurrentXp()
        {
            _currentXp = 0;
        }
        public void LevelUp()
        {
            if (_level  < 3)
            {
                _level++;
                _attack *= 1.2f;
                _attackSpeed *= 1.2f;
                _defense *= 1.2f;
                _maxhp *= 1.3f;
                _hp = maxHealth;
                _xpNeeded *= 2;
                _maxmana *= 2;
                _mana = maxMana;
                _magicpower *= 1.3f;
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