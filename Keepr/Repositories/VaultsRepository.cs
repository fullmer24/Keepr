using System.Data;
using System.Linq;
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
        internal Vaults GetById(int id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v 
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vaults, Account, Vaults>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).FirstOrDefault();
        }
        internal Vaults Edit(Vaults update)
        {
            string sql = @"
            UPDATE vaults SET
            name = @name,
            description = @description,
            isPrivate = isPrivate
            WHERE id = @id;
            ";
            _db.Execute(sql, update);
            return update;
        }
        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}