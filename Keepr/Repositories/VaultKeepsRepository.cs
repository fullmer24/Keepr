using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        internal List<VaultKeeps> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            vKeeps.*
            v.id
            FROM vaultKeeps vKeeps
            JOIN vaults v ON v.id = vKeeps.vaultId
            WHERE vKeeps.vaultId = @id;
            ";
            List<VaultKeeps> vaultKeeps = _db.Query<VaultKeeps, Vaults, VaultKeeps>(sql, (vaultKeep, vault) =>
            {
                vaultKeep.vaultId = vault.Id;
                return vaultKeep;
            }, new { id }).ToList();
            return vaultKeeps;
        }
    }
}