namespace L5_U5_12
{
    /// <summary>
    /// Klasė skirta duomenims apie žaidėjus aprašyti
    /// </summary>
    class NPC : Player
    {
        public string Guild { get; set; } //Gildija

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="hitPoints"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defence"></param>
        /// <param name="guild"></param>
        public NPC(string name, string role, int hitPoints, int mana, int damage, int defence, string guild)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Guild = guild;
        }

        /// <summary>
        /// Konstruktorius kuris kviečia NPCs duomenų nuskaitymą
        /// </summary>
        /// <param name="data">Duomenys</param>
        public NPC(string data)
        : base(data)
        {
            SetData(data);
        }

        /// <summary>
        /// NPCs duomenų nuskaitymas
        /// </summary>
        /// <param name="line">Eilute</param>
        public override void SetData(string line)
        {
            base.SetData(line);
            string[] values = line.Split(',');
            Guild = values[7];
        }

        /// <summary>
        /// Lygina 2 NPCs pagal žalos taškus
        /// </summary>
        /// <param name="lhs">Pirmas NPC</param>
        /// <param name="rhs">Antras NPC</param>
        /// <returns>Tiesą, jeigu pirmo NPC žalos taškai >= už antro NPC žalos taškus</returns>
        public override bool Compare(Player lhs, Player rhs)
        {
            return (lhs as NPC).Damage >= (rhs as NPC).Damage;
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
            return $"| {Name,-15} | {Role,-15} | {HitPoints,15} | {Mana,15} | {Damage,15} | {Defence,15} | {"",15} | {"",15} | {"",15} | {"",-15} | {Guild,-15} |";
        }

        public override string ToText()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Guild,-10}";
        }
    }
}
