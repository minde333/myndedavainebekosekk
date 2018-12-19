using System.Linq;

namespace L5_U5_12
{
    /// <summary>
    /// Filialų konteineris
    /// </summary>
    class BranchContainer
    {
        private const int MaxBranches = 50;
        private Branch[] Branches;
        public int Count { get; private set; }

        /// <summary>
        /// Konstruktorius
        /// </summary>
        public BranchContainer()
        {
            Branches = new Branch[MaxBranches];
            Count = 0;
        }

        /// <summary>
        /// Prideda filialus
        /// </summary>
        /// <param name="branch">Filialas</param>
        public void AddBranch(Branch branch)
        {
            Branches[Count++] = branch;
        }

        /// <summary>
        /// Prideda filialus
        /// </summary>
        /// <param name="branch">Filialas</param>
        /// <param name="index">Indeksas</param>
        public void AddBranch(Branch branch, int index)
        {
            Branches[index] = branch;
        }

        /// <summary>
        /// Paimamas filialas
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <returns></returns>
        public Branch GetBranch(int index)
        {
            return Branches[index];
        }

        /// <summary>
        /// Ieško ar yra nurodytas filialas
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public bool Contains(Branch branch)
        {
            return Branches.Contains(branch);
        }
    }
}
