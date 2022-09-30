using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsService _vaultsService;
        private readonly KeepsRepository _keepsRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService, KeepsRepository keepsRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultsService = vaultsService;
            _keepsRepo = keepsRepo;
        }
        internal VaultKeeps Create(VaultKeeps newVaultKeep)
        {
            // TODO get the keep from the keeps repo

            Keeps keep = _keepsRepo.GetById(newVaultKeep.keepId);
            keep.Kept++;
            _keepsRepo.Update(keep);

            return _vaultKeepsRepo.Create(newVaultKeep);
        }
        internal List<VaultKeepsVM> GetKeepByVaultId(int id, string userId)
        {
            List<VaultKeepsVM> vaultKeeps = _vaultKeepsRepo.GetKeepsByVaultId(id);
            if (vaultKeeps == null)
            {
                throw new Exception("nothing at that id");
            }
            return vaultKeeps;
        }

        internal string Delete(int id, Account userInfo)
        {
            VaultKeeps vaultKeeps = _vaultKeepsRepo.GetOne(id);
            if (vaultKeeps == null)
            {
                throw new Exception("no vaultKeep by that id");
            }
            Vaults vaults = _vaultsService.GetById(vaultKeeps.vaultId);
            if (vaults.CreatorId != userInfo.Id)
            {
                throw new Exception($"You can't do that {userInfo.Name}");
            }
            _vaultKeepsRepo.Delete(id);
            return $"{vaultKeeps.Id} Deleted";
        }
    }
}