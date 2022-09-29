using System.Data;
using Keepr.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Keepr.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetByEmail(string userEmail)
        {
            string sql = "SELECT * FROM accounts WHERE email = @userEmail";
            return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }

        internal List<Keeps> GetKeepsByProfile(int id)
        {
            string sql = @"
            SELECT 
            keeps.*,
            a.*
            FROM keeps
            JOIN accounts a ON keeps.creatorId = a.id
            WHERE keeps.creatorId = @id
            ";
            return _db.Query<Keeps, Account, Keeps>(sql, (keeps, profile) =>
            {
                keeps.Creator = profile;
                return keeps;
            }, new { id }).ToList();
        }

        internal Account Edit(Account update)
        {
            string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }
    }
}
