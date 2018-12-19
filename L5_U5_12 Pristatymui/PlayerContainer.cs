using System.Linq;

namespace L5_U5_12
{
    class PlayerContainer
    {
        public const int MaxNumberOfPlayers = 100; //Maksimalus žaidėjų skaičius
        private Player[] Players;
        public int Count { get; private set; }


        /// <summary>
        /// Filialo konstruktorius
        /// </summary>
        public PlayerContainer()
        {
            Players = new Player[MaxNumberOfPlayers];
        }



        /// <summary>
        /// Prideda veikėją
        /// </summary>
        /// <param name="player">Veikėjas</param>
        public void AddPlayer(Player player)
        {
            Players[Count] = player;
            Count++;
        }

        /// <summary>
        /// Paimamas veikėjas
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <returns></returns>
        public Player GetPlayer(int index)
        {
            return Players[index];
        }

        /// <summary>
        /// Ieško ar yra nurodytas veikėjas
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool Contains(Player player)
        {
            return Players.Contains(player);
        }

        /// <summary>
        /// Surušiuoja veikėjus
        /// </summary>
        public void SortPlayers()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                int m = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (Players[j].Compare(Players[j], Players[m]))
                        m = j;
                }
                Player a = Players[i];
                Players[i] = Players[m];
                Players[m] = a;
            }
        }
    }
}
