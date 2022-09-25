namespace Keepr.Models
{
    public class Vaults : RepoItem<int>
    {
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? isPrivate { get; set; }
        public Account Creator { get; set; }
    }
}