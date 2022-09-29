using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;
        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Profile GetById(int id)
        {
            string sql = @"
            SELECT * FROM profile WHERE profile.accountId = @id;
        ";
            return _db.Query<Profile, Account, Profile>(sql, (profile, account) =>
            {
                profile.Id = account.Id;
                return profile;
            }, new { id }).FirstOrDefault();
        }
    }
}