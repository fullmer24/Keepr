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
            vKeeps.*,
            k.*,
            a.*
            FROM vaultKeep vKeeps
            JOIN keeps k ON vKeeps.keepId = k.id
            JOIN accounts a ON k.creatorId = a.id
            WHERE vKeeps.vaultId = @id;
            ";
            List<VaultKeepsVM> vaultKeeps = _db.Query<VaultKeeps, VaultKeepsVM, Account, VaultKeepsVM>(sql, (vKeeps, k, a) =>
            {
                k.Creator = a;
                k.VaultKeepId = vKeeps.Id;
                return k;
            }, new { id }).ToList();
            return vaultKeeps;
        }

        internal VaultKeeps GetOne(int id)
        {
            string sql = @"
            SELECT * FROM vaultKeep WHERE id = @id";
            return _db.Query<VaultKeeps>(sql, new { id }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = @"DELETE FROM vaultKeep  WHERE id = @id;";
            _db.Execute(sql, new { id });
        }
    }
}