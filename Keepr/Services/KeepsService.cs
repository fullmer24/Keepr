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
        internal object GetById(int id, string userId)
        {
            Keeps keeps = _keepsRepo.GetById(id);
            if (keeps == null)
            {
                throw new Exception("No keeps by that ID");
            }
            keeps.Views++;
            _keepsRepo.Update(keeps);
            return keeps;
        }

        internal object Update(Keeps update, Account user)
        {
            Keeps original = (Keeps)GetById(update.Id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot update {original.Name} you are not the creator");
            }
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.Img = update.Img ?? original.Img;
            return _keepsRepo.Update(original);
        }

        internal string Delete(int id, Account user)
        {
            Keeps original = (Keeps)GetById(id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot delete {original.Name}, not yours");
            }
            _keepsRepo.Delete(id);
            return $"{original.Name} was deleted";
        }

        internal List<Keeps> getKeepsByProfileId(string id)
        {
            return _keepsRepo.GetKeepsByProfileId(id);
        }
    }
}