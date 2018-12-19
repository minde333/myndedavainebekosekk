namespace L5_U5_12
{
    /// <summary>
    /// Klasė skirta duomenims apie herojus aprašyti
    /// </summary>
    class Hero : Player
    {
        public int Strength { get; set; } //Jėga
        public int Agility { get; set; } //Vikrumas
        public int Intelligence { get; set; } //Intelektas
        public string Power { get; set; } //Ypatinga galia

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="hitPoints"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defence"></param>
        /// <param name="strength"></param>
        /// <param name="agility"></param>
        /// <param name="intelligence"></param>
        /// <param name="power"></param>
        public Hero(string name, string role, int hitPoints, int mana, int damage, int defence, int strength, int agility, int intelligence, string power)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Power = power;
        }

        /// <summary>
        /// Konstruktorius kuris kviečia herojų duomenų nuskaitymą
        /// </summary>
        /// <param name="data">Duomenys</param>
        public Hero(string data)
        : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// Herojų duomenų nuskaitymas
        /// </summary>
        /// <param name="line">Eilute</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Strength = int.Parse(values[7]);
            Agility = int.Parse(values[8]);
            Intelligence = int.Parse(values[9]);
            Power = values[10];
        }

        /// <summary>
        /// Lygina 2 herojus pagal intelektą
        /// </summary>
        /// <param name="lhs">Pirmas herojus</param>
        /// <param name="rhs">Antras herojus</param>
        /// <returns>Tiesą, jeigu pirmo herojaus intelektas >= už antro herojaus intelektą</returns>
        public override bool Compare(Player lhs, Player rhs)
        {
            return (lhs as Hero).Intelligence >= (rhs as Hero).Intelligence;
        }

        /// <summary>
        /// Tikrina ar atitinka nurodytus parametrus
        /// </summary>
        /// <param name="tankHealth">Nurodyti gyvybės taškai</param>
        /// <param name="tankDefence">Nurodyti gynybos taškai</param>
        /// <returns></returns>
        public override bool IsTank(int tankHealth, int tankDefence)
        {
            if (HitPoints >= tankHealth && Defence >= tankDefence)
                return true;
            return false;
        }

        /// <summary>
        /// Pakeičia ToString metodą
        /// </summary>
        /// <returns>Pakeistą ToString šabloną</returns>
        public override string ToString()
        {
            return $"| {Name,-15} | {Role,-15} | {HitPoints,15} | {Mana,15} | {Damage,15} | {Defence,15} | {Strength,15} | {Agility,15} | {Intelligence,15} | {Power,-15} | {"",-15} |";
        }

        public override string ToText()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Strength,-10};{Agility,-10};{Intelligence,-10};{Power,10}";
        }
    }
}