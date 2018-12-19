using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L5_U5_12
{
    class Program
    {
        public const int TankHealth = 100; //Tanko gyvybės pagal kurias suranda tanką
        public const int TankDefence = 30; // Tanko gynyba pagal kurias suranda tanką

        static void Main(string[] args)
        {
            Console.ReadKey();
            Console.OutputEncoding = Encoding.UTF8; //Konsolėje rašo lietuviškas raides
            Program p = new Program();
            const string DataDir = @"..\..\Data";
            BranchContainer branchContainer = new BranchContainer();
            p.ReadData(DataDir, ref branchContainer);
            CreateReportTable(branchContainer, "ReportTable.txt");
            PrintMostPopularRole(branchContainer);
            Console.WriteLine("2.Išspausdina pasikartojančius veikėjų vardus į Klaidos.csv");
            WriteFilteredPlayersData(branchContainer, "Klaidos.csv");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("3.Išspausdina tankus pagal gyvybės ir gynybos taškus į Tankai.csv");
            var filteredTanksHeroes = new PlayerContainer();
            var filteredTanksNPCs = new PlayerContainer();
            PrintTanks(branchContainer, "Tankai.csv", out filteredTanksHeroes, out filteredTanksNPCs);
            Console.WriteLine();
            int intelligenceLimit = ReadInt("4.Įveskite sveikąją reikšmę, kuria norėtumete, kad Herojai viršytų intelekto dydį: ");
            int damagePoint = ReadInt("4.Įveskite sveikąją reikšmę, kuria norėtumėte, kad NPCs neviršytų žalos taškų dydžio: ");
            Console.WriteLine("4.Išspausdina rinktinę pagal Herojų intelektą ir NPCs žalos taškus į Rinktine.csv");
            var selectionHeroes = new PlayerContainer();
            var selectionNPCs = new PlayerContainer();
            PrintGeneralSelection(branchContainer, "Rinktine.csv", intelligenceLimit, damagePoint, out selectionHeroes, out selectionNPCs);
            Console.ReadKey();
        }

        /// <summary>
        /// Skaito veikėjų duomenis iš failo
        /// </summary>
        /// <param name="file">Failas</param>
        /// <param name="branchContainer">Filialų konteineris</param>
        private void ReadData(string file, ref BranchContainer branchContainer)
        {
            string[] filePaths = Directory.GetFiles(file, "*.csv");

            foreach (string path in filePaths)
            {
                branchContainer.AddBranch(ReadPlayerData(path));
            }
        }

        /// <summary>
        /// Skaito veikejų duomenis iš failo
        /// </summary>
        /// <param name="file">Failas</param>
        /// <returns>Filialą</returns>
        private static Branch ReadPlayerData(string file)
        {
            Branch branch;

            using (StreamReader reader = new StreamReader(@file, Encoding.GetEncoding(1257)))
            {
                var line = reader.ReadLine();
                var race = line;
                var city = reader.ReadLine();
                line = reader.ReadLine();
                branch = new Branch(race, city);

                while (line != null)
                {
                    switch (line[0])
                    {
                        case 'H':
                            branch.AddPlayer(new Hero(line));
                            break;
                        case 'N':
                            branch.AddPlayer(new NPC(line));
                            break;
                    }
                    line = reader.ReadLine();
                }
            }
            return branch;
        }

        /// <summary>
        /// Išspausdina veikėjų lentelę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        private static void CreateReportTable(BranchContainer branchContainer, string file)
        {
            for (int i = 0; i < branchContainer.Count; i++)
            {
                using (var writer = new StreamWriter(file, true, Encoding.UTF8))
                {
                    writer.WriteLine("Duomenys apie rasę ir jos veikėjus");
                    writer.WriteLine(new string('-', 199));
                    writer.WriteLine("| Rasė: {0, -189} |", branchContainer.GetBranch(i).Race);
                    writer.WriteLine(new string('-', 199));
                    writer.WriteLine("| Miestas: {0, -186} |", branchContainer.GetBranch(i).Town);
                    writer.WriteLine(new string('-', 199));
                    writer.WriteLine("| {0, -15} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15} | {6,-15} | {7,-15} | {8,-15} | {9,-15} | {10,-15} |",
                           "Vardas", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai ", "Gynybos taškai", "Jėga", "Vikrumas", "Intelektas", "Ypatinga galia", "Gildija");
                    writer.WriteLine(new string('-', 199));

                    for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                    {
                        writer.WriteLine(branchContainer.GetBranch(i).Players.GetPlayer(j));
                    }
                    writer.WriteLine(new string('-', 199));
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Suranda populiariausią klasę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <returns>Populiariausią klasę</returns>
        private static Dictionary<string, int> FindMostPopular(BranchContainer branchContainer)
        {
            var mostPopularRole = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (var j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)    
                {
                    if (mostPopularRole.ContainsKey(branchContainer.GetBranch(i).GetPlayer(j).Role))
                    {
                        mostPopularRole[branchContainer.GetBranch(i).GetPlayer(j).Role]++;
                    }
                    else
                    {
                        mostPopularRole.Add(branchContainer.GetBranch(i).GetPlayer(j).Role, 1);
                    }
                }
            }
            return mostPopularRole;
        }

        /// <summary>
        /// Išspausdina populiariausią klasę
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        private static void PrintMostPopularRole(BranchContainer branchContainer)
        {
            var mostPopularRole = FindMostPopular(branchContainer);
            var maxValue = mostPopularRole.Values.Max();
            var role = mostPopularRole.FirstOrDefault(f => f.Value == maxValue).Key;

            Console.WriteLine($"1.Daugiausiai šios klasės veikėjų: {role} | Pasikartoja: {maxValue} kartus(ų)");
            Console.WriteLine();
        }

        /// <summary>
        /// Suranda vienodus veikėjus
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <returns>Vienodus veikėjus</returns>
        private static Dictionary<string, int> FilterPlayers(BranchContainer branchContainer)
        {
            var samePlayers = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                {
                    if (samePlayers.ContainsKey(branchContainer.GetBranch(i).GetPlayer(j).Name))
                    {
                        samePlayers[branchContainer.GetBranch(i).GetPlayer(j).Name]++;
                    }
                    else
                    {
                        samePlayers.Add(branchContainer.GetBranch(i).GetPlayer(j).Name, 1);
                    }
                }
            }
            return samePlayers;
        }

        /// <summary>
        /// Įrašo pasikartojančius rasių vardus į failą
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        public static void WriteFilteredPlayersData(BranchContainer branchContainer, string file)
        {
            var filteredPlayers = FilterPlayers(branchContainer);
            var filteredPlayersCount = filteredPlayers.Values.Max();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Vardas;");
                foreach (KeyValuePair<string, int> samePlayers in filteredPlayers)
                {
                    if (samePlayers.Value >= 2)
                    {
                        writer.WriteLine($"{samePlayers.Key}");
                    }
                }
            }
        }

        /// <summary>
        /// Suranda veikėjus tankus ir ideda juos į konteinerius
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="filteredTanksHeroes">Herojų tankų konteineris</param>
        /// <param name="filteredTanksNPCs">Herojų NPCs konteineris</param>
        public static void FindTanks(BranchContainer branchContainer, out PlayerContainer filteredTanksHeroes, out PlayerContainer filteredTanksNPCs)
        {
            filteredTanksHeroes = new PlayerContainer();
            filteredTanksNPCs = new PlayerContainer();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                {
                    if ((branchContainer.GetBranch(i).GetPlayer(j) is Hero) && branchContainer.GetBranch(i).GetPlayer(j).IsTank(TankHealth, TankDefence))
                    {
                        filteredTanksHeroes.AddPlayer(branchContainer.GetBranch(i).GetPlayer(j));
                    }
                }
                for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                {
                    if ((branchContainer.GetBranch(i).GetPlayer(j) is NPC) && branchContainer.GetBranch(i).GetPlayer(j).IsTank(TankHealth, TankDefence))
                    {
                        filteredTanksNPCs.AddPlayer(branchContainer.GetBranch(i).GetPlayer(j));
                    }
                }
            }
        }

        /// <summary>
        /// Įrašo tankus į failą.
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        /// <param name="filteredTanksHeroes">Herojų tankų konteineris</param>
        /// <param name="filteredTanksNPCs">NPCs tankų konteineris</param>
        public static void PrintTanks(BranchContainer branchContainer, string file, out PlayerContainer filteredTanksHeroes, out PlayerContainer filteredTanksNPCs)
        {
            FindTanks(branchContainer, out filteredTanksHeroes, out filteredTanksNPCs);

            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Herojai");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Jėga;Vikrumas;Intelektas;Ypatinga galia");
                for (int i = 0; i < filteredTanksHeroes.Count; i++)
                {
                    writer.WriteLine(filteredTanksHeroes.GetPlayer(i).ToText());
                }
                writer.WriteLine();
                writer.WriteLine("NPC");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Gildija");
                for (int i = 0; i < filteredTanksNPCs.Count; i++)
                {
                    writer.WriteLine(filteredTanksNPCs.GetPlayer(i).ToText());
                }
            }
        }

        /// <summary>
        /// Suranda veikėjų rinktinę ir ideda į konteinerius
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="intelligenceLimit">Intelekto nurodytas dydis</param>
        /// <param name="damagePoint">Žalos taškų nurodytas dydis</param>
        /// <param name="selectionHeroes">Herojų rinktinės konteineris</param>
        /// <param name="selectionNPCs">NPCs rinktinės konteineris</param>
        public static void GeneralSelection(BranchContainer branchContainer, int intelligenceLimit, int damagePoint, out PlayerContainer selectionHeroes, out PlayerContainer selectionNPCs)
        {
            selectionHeroes = new PlayerContainer();
            selectionNPCs = new PlayerContainer();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                {
                    if ((branchContainer.GetBranch(i).GetPlayer(j) is Hero) && (branchContainer.GetBranch(i).GetPlayer(j) as Hero).Intelligence > intelligenceLimit)
                    {
                        selectionHeroes.AddPlayer(branchContainer.GetBranch(i).GetPlayer(j));
                    }
                }
                for (int j = 0; j < branchContainer.GetBranch(i).Players.Count; j++)
                {
                    if ((branchContainer.GetBranch(i).GetPlayer(j) is NPC) && (branchContainer.GetBranch(i).GetPlayer(j) as NPC).Damage <= damagePoint)
                    {
                        selectionNPCs.AddPlayer(branchContainer.GetBranch(i).GetPlayer(j));
                    }
                }
            }
        }

        /// <summary>
        /// Įrašo rinktinę į failą
        /// </summary>
        /// <param name="branchContainer">Filialų konteineris</param>
        /// <param name="file">Failas</param>
        /// <param name="intelligenceLimit">Intelekto nurodytas dydis</param>
        /// <param name="damagePoint">Žalos taškų nurodytas dydis</param>
        /// <param name="selectionHeroes">Herojų rinktinės konteineris</param>
        /// <param name="selectionNPCs">NPCs rinktinės konteineris</param>
        public static void PrintGeneralSelection(BranchContainer branchContainer, string file, int intelligenceLimit, int damagePoint, out PlayerContainer selectionHeroes, out PlayerContainer selectionNPCs)
        {
            GeneralSelection(branchContainer, intelligenceLimit, damagePoint, out selectionHeroes, out selectionNPCs);
            selectionHeroes.SortPlayers();
            selectionNPCs.SortPlayers();

            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Herojai");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Jėga;Vikrumas;Intelektas;Ypatinga galia");
                for (int i = 0; i < selectionHeroes.Count; i++)
                {
                    writer.WriteLine(selectionHeroes.GetPlayer(i).ToText());
                }
                writer.WriteLine();
                writer.WriteLine("NPC");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Gildija");
                for (int i = 0; i < selectionNPCs.Count; i++)
                {
                    writer.WriteLine(selectionNPCs.GetPlayer(i).ToText());
                }
            }
        }

        /// <summary>
        /// Skaito ar teisingai įvestas sveikas skaičius
        /// </summary>
        /// <param name="prompt">Antraštė</param>
        /// <returns>Teisingai įvestą sveiką skaičių</returns>
        public static int ReadInt(string prompt)
        {
            bool ifException = false;
            int limit = 0;
            do
            {
                ifException = false;
                try
                {
                    Console.WriteLine(prompt);
                    limit = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Įvedėte neteisingai!");
                    Console.WriteLine();
                    ifException = true;
                }
            } while (limit < -100000 || limit > 100000 || ifException == true);

            return limit;
        }
    }
}