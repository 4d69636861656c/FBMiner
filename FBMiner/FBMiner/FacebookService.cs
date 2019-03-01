using System.Threading.Tasks;

namespace ConsoleApplication
{
    public interface IFacebookService
    {
        Task<Account> GetAccountAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<Account> GetAccountAsync(string accessToken)
        {
            string identity = "me";

            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, identity, "fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale,location,website,likes.limit(512){id,name,fan_count,about,created_time},feed.limit(512){story,created_time,description,link},friends");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name,
                Gender = result.gender,
                Birthday = result.birthday,
                Locale = result.locale,
                Location = result.location.name,
                Website = result.website,
                Likes = result.likes.data,
                Feed = result.feed.data,
                Friends = result.friends.data
            };
            return account;
        }

        public async Task PostOnWallAsync(string accessToken, string message)
            => await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
    }
}