using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly AccountsRepository _repo;

        public ProfilesService(AccountsRepository repo)
        {
            _repo = repo;
        }
        internal Account GetById(string id)
        {
            Account account = _repo.GetById(id);
            if (account == null)
            {
                throw new Exception("No profile at that id");
            }
            return account;
        }

        internal List<Keeps> GetKeeps(int id, string userId)
        {
            Account account = GetOne(id, userId);
            return _repo.GetKeepsByProfile(id);
        }

        internal Account GetOne(int id, string userId)
        {
            Account account = _repo.GetById(userId);
            if (account == null)
            {
                throw new Exception("No profile at that id");
            }
            return account;
        }
    }
}