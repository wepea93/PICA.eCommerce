using Authorizer.Core.Contracts.Services;
using Authorizer.Core.Exceptions;
using Authorizer.Core.Objects.Dtos;
using eCommerce.Commons.Objects.Requests.Authorizer;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Authorizer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApiAuthorizer.Controllers;
using Xunit;

namespace WebApiAuthorizer.UnitTest
{
    public class AuthorizeTests
    {
        private string _apiCode { get; set; } = "CODE00";
        private string _apiUser { get; set; } = "user";
        private string _apiKey { get; set; } = "APIKEY$";
        private string _accessToken { get; set; } = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxOTUwNDEyNjkxIiwibmJmIjoxNjQ4MzAyNTg0LCJleHAiOjE2NDgzMDYxODQsImlhdCI6MTY0ODMwMjU4NH0.ADtZLJzutLmIRT0NNmoRw2BMrgpAgSW6qmG0It686Xo";


        /// <summary>
        /// Valida la generación de un nuevo access token - test
        /// </summary>
        /// <returns>Exitoso</returns>
        [Fact]
        public async Task Create_AccessToken_Successful()
        {
            var authorizeService = new Mock<IAuthorizeService>();
            var expiresAt = DateTime.Now.AddSeconds(60);
            var request = new TokenRequest
            {
                ApiUser = _apiUser,
                ApiKey = _apiKey
            };
            var response = new AccessTokenDto(_apiUser, _accessToken, expiresAt.ToString());

            authorizeService.Setup(x => x.GenerateAccessToken(_apiUser, _apiKey)).ReturnsAsync(response);
            AuthorizeController controlller = new (authorizeService.Object);

            var servResult = await controlller.GenerateAccessToken(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);  
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<TokenResponse>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
            Assert.Equal(objectResponse.Response.ApiUser, _apiUser);
            Assert.Equal(objectResponse.Response.AccessToken, _accessToken);
            Assert.Equal(objectResponse.Response.ExpiresAt, expiresAt.ToString());
        }

        /// <summary>
        /// Valida respuesta al intentar generar un access token con un usuario no autorizado
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_AccessToken_With_Unauthorized_User_Fail()
        {
            var authorizeService = new Mock<IAuthorizeService>();
            var request = new TokenRequest
            {
                ApiUser = _apiUser,
                ApiKey = _apiKey
            };

            authorizeService.Setup(x => x.GenerateAccessToken(_apiUser, _apiKey)).Throws(new UnauthorizedUserException());
            AuthorizeController controlller = new(authorizeService.Object);

            var servResult = await controlller.GenerateAccessToken(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<TokenResponse>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.Unauthorized), servResponse.StatusCode);
        }

        /// <summary>
        /// Valida repuesta de un access token valido
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Validate_AccessToken_Successful()
        {
            var authorizeService = new Mock<IAuthorizeService>();
            var request = new ValidateTokenRequest
            {
                AccessToken = _accessToken,
                AppCode = _apiCode
            };

            authorizeService.Setup(x => x.ValidateAccessToken(_accessToken, _apiCode)).ReturnsAsync(true);
            AuthorizeController controlller = new(authorizeService.Object);

            var servResult = await controlller.ValidateAccessToken(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<ValidateTokenResponse>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse.Message);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
            Assert.True(objectResponse.Response.IsValid);
        }

        /// <summary>
        /// Valida respuesta de un access token no autorizado
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Validate_Invalid_AccessToken_Fail()
        {
            var authorizeService = new Mock<IAuthorizeService>();
            var request = new ValidateTokenRequest
            {
                AccessToken = _accessToken,
                AppCode = _apiCode
            };

            authorizeService.Setup(x => x.ValidateAccessToken(_accessToken, _apiCode)).ReturnsAsync(false);
            AuthorizeController controlller = new(authorizeService.Object);

            var servResult = await controlller.ValidateAccessToken(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<ValidateTokenResponse>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.Unauthorized), servResponse.StatusCode);
        }

    }
}