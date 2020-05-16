namespace Todo.Models
{
    public class UserProfile
    {
        public UserProfile(string displayName, string pictureUrl)
        {
            DisplayName = displayName;
            PictureUrl = pictureUrl;
        }
        public string PictureUrl { get; private set; }
        public string DisplayName { get; private set; }
    }
}
