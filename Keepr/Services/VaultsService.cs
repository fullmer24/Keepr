using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepo;
        public VaultsService(VaultsRepository vaultsRepo)
        {
            _vaultsRepo = vaultsRepo;
        }
        internal Vaults Create(Vaults newVault)
        {
            return _vaultsRepo.Create(newVault);
        }

        internal Vaults GetById(int id, string userId)
        {
            Vaults vaults = _vaultsRepo.GetById(id);
            if (vaults == null)
            {
                throw new Exception($"No vaults at that id");
            }
            if (vaults.isPrivate == true && vaults.CreatorId != userId)
            {
                throw new Exception($"{vaults.Name} is currently private");
            }
            return vaults;
        }
        internal Vaults Edit(Vaults update, Account user)
        {
            Vaults original = GetById(update.Id, user.Id);
            if (original.CreatorId != user.Id)
            {
                throw new Exception($"cannot edit {original.Name} you are not the creator");
            }
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.isPrivate = update.isPrivate ?? original.isPrivate;
            return _vaultsRepo.Edit(original);
        }



    }
}