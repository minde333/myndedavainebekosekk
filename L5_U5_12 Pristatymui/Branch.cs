namespace L5_U5_12
{
    /// <summary>
    /// Klasė, skirta duomenims apie filialus  aprašyti
    /// </summary>
    class Branch
    {
        public const int MaxNumberOfPlayers = 100; //Maksimalus žaidėjų skaičius
        public string Race { get; set; } //Rasė
        public string Town { get; set; } //Miestas
        public PlayerContainer Players { get; set; } //Veikėjų konteineris


        /// <summary>
        /// Filialo konstruktorius
        /// </summary>
        public Branch()
        {
            Players = new PlayerContainer();
        }

        /// <summary>
        /// Filialo konstruktorius su parametrais
        /// </summary>
        /// <param name="race">Rasė</param>
        /// <param name="town">Miestas</param>
        public Branch(string race, string town)
        {
            Race = race;
            Town = town;
            Players = new PlayerContainer();
        }

        /// <summary>
        /// Prideda veikėją
        /// </summary>
        /// <param name="player">Veikėjas</param>
        public void AddPlayer(Player player)
        {
            Players.AddPlayer(player);
        }

        /// <summary>
        /// Paimamas veikėjas pagal nurodytą indeksą
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <returns></returns>
        public Player GetPlayer(int index)
        {
            return Players.GetPlayer(index);
        }
    }
}
