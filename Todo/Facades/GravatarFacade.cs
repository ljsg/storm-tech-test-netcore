using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Facades.Models;
using Todo.Models;

namespace Todo.Facades
{
    public class GravatarFacade
    {
        private readonly HttpClient httpClient;

        public GravatarFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserProfile> GetProfile(string emailHash)
        {
            string profileEndpoint = "https://www.gravatar.com/" + emailHash + ".json";
            HttpResponseMessage response = await httpClient.GetAsync(profileEndpoint);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                GravatarProfile gravatarProfile = JsonConvert.DeserializeObject<GravatarProfile>(content);
                var userProfle = new UserProfile(gravatarProfile.Entry[0].DisplayName, gravatarProfile.Entry[0].ThumbnailUrl);
                return userProfle;
            }
            return null;
        }

        public async Task<string> GetAvatarURL(string emailHash)
        {
            string profileEndpoint = "https://www.gravatar.com/avatar/" + emailHash + "?s=30";
            return await Task.FromResult(profileEndpoint);
        }
    }
}
