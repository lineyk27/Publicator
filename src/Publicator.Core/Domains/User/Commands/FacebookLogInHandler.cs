using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Publicator.Core.Services;

namespace Publicator.Core.Domains.User.Commands
{
    class FacebookLogInHandler : IRequestHandler<FacebookLogIn, LogInResult>
    {
        private static string AppAccessTokenAddressTemplate = 
            "https://graph.facebook.com/oauth/access_token?client_id={0}" +
            "&client_secret={1}" +
            "&grant_type=client_credentials";

        private static string AccessTokenValidationAddressTemplate =
            "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}";

        private static string UserDataAccessAddressTemplate =
            "https://graph.facebook.com/v2.8/me" +
            "?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture" +
            "&access_token={0}";

        private readonly UserManager<Infrastructure.Models.User> _userManager;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public FacebookLogInHandler(
            UserManager<Infrastructure.Models.User> userManager,
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public async Task<LogInResult> Handle(FacebookLogIn request, CancellationToken cancellationToken)
        {
            var loginResult = new LogInResult();

            var appId = _configuration["OAuth:Facebook:AppSecret"];
            var appSecret = _configuration["OAuth:Facebook:AppId"];
            //getting app access token
            var client = _clientFactory.CreateClient();
            var appAccessResponseBody = await client
                .GetStringAsync(String.Format(AppAccessTokenAddressTemplate, appId, appSecret));

            var appAccessToken = JsonSerializer
                .Deserialize<FacebookAppAccessToken>(appAccessResponseBody)
                .AccessToken;
            
            //validating user access token

            var validationResponse = await client.GetStringAsync(
                    String.Format(AccessTokenValidationAddressTemplate, request.AccessToken, appAccessToken)
                    );
            
            var validationResult = JsonSerializer
                .Deserialize<FacebookUserAccessTokenValidation>(validationResponse);

            if(!validationResult.Data.IsValid)
            {
                loginResult.Result = LoginResultEnum.BadCredentials;
                return loginResult;
            }

            var userDataResponse = await client
                .GetStringAsync(String.Format(UserDataAccessAddressTemplate, request.AccessToken));

            var userData = JsonSerializer.Deserialize<FacebookUserData>(userDataResponse);

            var existedUser = await _userManager.FindByEmailAsync(userData.Email);

            if(existedUser == null)
            {
                var newUser = new Infrastructure.Models.User()
                {
                    UserName = userData.Email.Split("@")[0],
                    Email = userData.Email,
                    PictureUrl = userData.Picture.Data.Url,
                    JoinDate = DateTime.Now,
                    EmailConfirmed = true
                };

                var creationRes = await _userManager.CreateAsync(newUser);
                
                if(!creationRes.Succeeded)
                {
                    loginResult.Result = LoginResultEnum.BadCredentials;
                    return loginResult;
                }
            }
            
            var localUser = await _userManager.FindByEmailAsync(userData.Email);

            var jwtToken = _tokenService.GenerateToken(localUser);

            loginResult.Result = LoginResultEnum.Succesfull;
            loginResult.Token = jwtToken;

            return loginResult;
        }
    }
}
