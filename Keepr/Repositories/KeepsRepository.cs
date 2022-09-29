using System;
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
            SELECT
            keeps.*,
            a.*
            FROM keeps 
            JOIN accounts a ON keeps.creatorId = a.id
            WHERE keeps.id = @id;
            ";
            return _db.Query<Keeps, Account, Keeps>(sql, (keeps, account) =>
            {
                keeps.Creator = account;
                return keeps;
            }, new { id }).FirstOrDefault();
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
        internal object Update(Keeps keepsData)
        {
            string sql = @"
            UPDATE keeps SET
                name = @name,
                description = @description,
                img = @img
            WHERE id = @id
            ";
            int rowsAffected = _db.Execute(sql, keepsData);
            if (rowsAffected == 0)
            {
                throw new Exception("unable to edit keep");
            }
            return keepsData;
        }

        internal List<Keeps> GetKeepsByProfileId(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k 
            JOIN accounts a ON k.creatorId = a.id
            WHERE a.id = @id;
            ";
            return _db.Query<Keeps>(sql, new { id }).ToList();
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}