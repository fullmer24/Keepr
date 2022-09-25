using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Keeps> GetAll()
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId;
            ";
            List<Keeps> keeps = _db.Query<Keeps, Account, Keeps>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }).ToList();
            return keeps;
        }

        internal Keeps GetById(int id)
        {
            string sql = @"
            SELECT * FROM keeps
            WHERE id = @id;
            ";
            Keeps keeps = _db.Query<Keeps>(sql, new { id }).FirstOrDefault();
            return keeps;
        }

        internal Keeps Create(Keeps newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@name, @description, @img, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }



    }
}