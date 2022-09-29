namespace Keepr.Models
{
    public class Profile : RepoItem<string>
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public Account accountId { get; set; }
    }
}