namespace Keepr.Models
{
    public class Keeps : RepoItem<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Kept { get; set; }
        public string creatorId { get; set; }
        public Account Creator { get; set; }
    }
    public class VaultKeepsVM : Keeps
    {
        public int VaultKeepsId { get; set; }
    }
}