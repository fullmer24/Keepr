using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _profileRepo;
        public ProfilesService(ProfilesRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }
        internal Profile GetById(int id, string userId)
        {
            Profile profile = _profileRepo.GetById(id);
            if (profile == null)
            {
                throw new Exception("No profile at that id");
            }
            return profile;
        }
    }
}