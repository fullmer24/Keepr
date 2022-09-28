namespace Keepr.Models
{
    public class VaultKeeps : RepoItem<int>
    {
        public string CreatorId { get; set; }
        public int vaultId { get; set; }
        public int keepId { get; set; }
        public Account Creator { get; set; }
    }
}