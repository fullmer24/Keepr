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
            vKeep.*,
            v.*,
            k.*
            FROM vaultKeep vKeep
            JOIN vaults v ON vKeep.vaultId = v.id 
            JOIN keeps k ON vKeep.id = k.vaultKeepId
            WHERE vKeep.vaultId = @id;
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



