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



    }
}