using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;
        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }
        internal List<Keeps> GetAll(string id)
        {
            List<Keeps> keeps = _keepsRepo.GetAll();
            return keeps;
        }
        internal Keeps Create(Keeps newKeep)
        {
            return _keepsRepo.Create(newKeep);
        }
        internal object GetById(int id)
        {
            Keeps keeps = _keepsRepo.GetById(id);
            if (keeps == null)
            {
                throw new Exception("No keeps by that ID");
            }
            return keeps;
        }




    }
}