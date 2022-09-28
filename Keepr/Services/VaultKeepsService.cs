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
        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultsService)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultsService = vaultsService;
        }
        internal VaultKeeps Create(VaultKeeps newVaultKeep)
        {
            return _vaultKeepsRepo.Create(newVaultKeep);
        }
        internal List<VaultKeeps> GetKeepByVaultId(int id)
        {
            List<VaultKeeps> vaultKeeps = _vaultKeepsRepo.GetKeepsByVaultId(id);
            if (vaultKeeps == null)
            {
                throw new Exception("nothing at that id");
            }
            return vaultKeeps;
        }
    }
}