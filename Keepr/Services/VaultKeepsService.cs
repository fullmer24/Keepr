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


    }
}