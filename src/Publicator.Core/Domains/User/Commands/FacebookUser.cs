using System.Text.Json.Serialization;

namespace Publicator.Core.Domains.User.Commands
{
    class FacebookUserData
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public FacebookPictureData Picture { get; set; }
    }

    class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    class FacebookPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        [JsonPropertyName("is_silhouette")]
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
