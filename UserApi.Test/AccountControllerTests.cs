using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserApi.Authentication;
using UserApi.Core.Data.Repositories;
using UserApi.Models;
using UserApi.Test.Stubs;
using Xunit;

namespace UserApi.Test
{
    public class AccountControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public AccountControllerTests(TestWebApplicationFactory factory)
        {
            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IUserRepository, UserRepositoryStub>();
                    services.AddScoped<IAuthenticationManager, AuthenticationManagerStub>();
                });
            }).CreateClient();

            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
        }

        [Fact]
        public async Task when_invalid_email_Is_provide_while_registering_user()
        {
            var url = "api/account/register";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "amritaz",
                Password = "secret123"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_first_name_is_missing_while_registering_user()
        {
            var url = "api/account/register";

            var newUser = new AddUserModel
            {
                LastName = "User",
                UserName = "amritaz@abc.com",
                Password = "secret123"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_user_details_are_provided_while_registering_user()
        {
            var url = "api/account/register";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "amrp@abc.com",
                Password = "secret123"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task when_duplicate_username_is_provided_while_registering_user()
        {
            var url = "api/account/register";

            var newUser = new AddUserModel
            {
                FirstName = "Demo",
                LastName = "User",
                UserName = "amritaz@abc.com",
                Password = "secret123"
            };

            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task when_valid_login_creadentials_are_provided()
        {
            var url = "api/account/login";

            var newUser = new AddUserModel
            {
                UserName = "amritaz@abc.com",
                Password = "demo123"
            };
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var token = JsonConvert.DeserializeObject<LoginResponse>(await result.Content.ReadAsStringAsync());
            Assert.NotNull(token);
        }

        [Fact]
        public async Task when_invalid_login_creadentials_are_provided()
        {
            var url = "api/account/login";

            var newUser = new AddUserModel
            {
                UserName = "sample@abc.com",
                Password = "demo123"
            };
            var result = await _httpClient.PostAsync(url, HttpUtils.GetContent(newUser));
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }

}
