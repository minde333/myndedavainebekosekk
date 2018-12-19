using System;

namespace L5_U5_12
{
    /// <summary>
    /// Klasė skirta veikėjo duomenims aprašyti
    /// </summary>
    abstract class Player : Object
    {
        public string Name { get; set; } //Vardas
        public string Role { get; set; } //Klasė
        public int HitPoints { get; set; } //Gyvybės taškai
        public int Mana { get; set; } //Mana
        public int Damage { get; set; } //Žalos taškai
        public int Defence { get; set; } //Gynybos taškai

        /// <summary>
        /// Konstruktorius su parametrais
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="hitPoints"></param>
        /// <param name="mana"></param>
        /// <param name="damage"></param>
        /// <param name="defence"></param>
        public Player(string name, string role, int hitPoints, int mana, int damage, int defence)
        {
            Name = name;
            Role = role;
            HitPoints = hitPoints;
            Mana = mana;
            Damage = damage;
            Defence = defence;
        }

        /// <summary>
        /// Konstruktorius kuris kviečia veikėjų bendrųjų duomenų nuskaitymą
        /// </summary>
        /// <param name="data">Duomenys</param>
        public Player(string data)
        {
            SetData(data);
        }

        /// <summary>
        /// Visų veikėjų bendrųjų duomenų nuskaitymas
        /// </summary>
        /// <param name="line">Eilute</param>
        public virtual void SetData(string line)
        {
            string[] values = line.Split(',');
            Name = values[1];
            Role = values[2];
            HitPoints = int.Parse(values[3]);
            Mana = int.Parse(values[4]);
            Damage = int.Parse(values[5]);
            Defence = int.Parse(values[6]);
        }

        public abstract bool IsTank(int tankHealth, int tankDefence);
        public abstract bool Compare(Player lhs, Player rhs);
        public abstract string ToText();


        /// <summary>
        /// Perašytas daugiau arba lygu operatorius
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator >=(Player lhs, Player rhs)
        {
            int ip = String.Compare(lhs.Role, rhs.Role, StringComparison.CurrentCulture);
            int ip2 = String.Compare(lhs.Name, rhs.Name, StringComparison.CurrentCulture);
            return ip > 0 || ip == 0 && ip2 > 0;
        }

        /// <summary>
        /// Perašytas mažiau arba lygu operatorius
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator <=(Player lhs, Player rhs)
        {
            int ip = String.Compare(lhs.Role, rhs.Role, StringComparison.CurrentCulture);
            int ip2 = String.Compare(lhs.Name, rhs.Name, StringComparison.CurrentCulture);
            return ip < 0 || ip == 0 && ip2 < 0;
        }
    }
}
