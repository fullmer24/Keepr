using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeeps Create(VaultKeeps newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultKeep
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }
    }
}