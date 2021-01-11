using System.Threading.Tasks;

namespace Facebook_targeting
{
    public interface IFacebookService
    {
        Task AddAudienceAsync(string accessToken, string endpoint, object payload);
        Task DeleteAudienceAsync(string accessToken, string endpoint, object payload);
    }

    public partial class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task AddAudienceAsync(string accessToken, string endpoint, object payload)
            => await _facebookClient.PostAsync(accessToken, endpoint, payload);

        public async Task DeleteAudienceAsync(string accessToken, string endpoint, object payload)
            => await _facebookClient.DeleteAsync(accessToken, endpoint, payload);            
        
            

    }
}
