using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService profileService;

        public ProfileController(ProfileService profileService)
        {
            this.profileService = profileService;
        }
        public async Task<ActionResult> GetProfile([FromQuery]string email)
        {
            UserProfile profile = await profileService.GetProfile(email);
            return PartialView("_ProfilePartial", profile);
        }
    }
}