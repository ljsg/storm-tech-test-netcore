using System.Threading.Tasks;
using Todo.Facades;
using Todo.Models;

namespace Todo.Services
{
    public class ProfileService
    {
        private readonly GravatarFacade gravatarFacade;
        
        public ProfileService(GravatarFacade gravatarFacade)
        {
            this.gravatarFacade = gravatarFacade;
        }

        public async Task<UserProfile> GetProfile(string email)
        {
            string profile = await gravatarFacade.GetAvatarURL(MD5Hasher.GetHash(email));
            return new UserProfile(email, profile);
        }
    }
}
