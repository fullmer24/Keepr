using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Vaults Create(Vaults newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, creatorId)
            VALUES
            (@name, @description, @isPrivate, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int Id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = Id;
            return newVault;
        }



    }
}