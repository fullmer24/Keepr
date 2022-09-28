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

        internal List<VaultKeepsVM> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            vKeeps.*
            v.id,
            k.*
            a.*
            FROM vaultKeeps vKeeps
            JOIN vaults v ON v.id = vKeeps.vaultId
            JOIN keeps k ON k.vaultId = v.id
            JOIN accounts a ON v.creatorId = a.id
            WHERE vKeeps.vaultId = @id;
            ";
            List<VaultKeepsVM> vaultKeeps = _db.Query<VaultKeeps, VaultKeepsVM, Keeps, Account, VaultKeeps>(sql, (vKeeps, v, k, a) =>
            {
                v.Creator = a;
                k.VaultKeepId = vKeeps.Id;
                return v;
            }, new { id }).ToList();
            return vaultKeeps;
        }
    }
}