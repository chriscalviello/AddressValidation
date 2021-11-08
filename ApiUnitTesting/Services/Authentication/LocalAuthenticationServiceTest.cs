using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Api.Models.User;
using Api.Services.Authentication;
using Xunit;

namespace ApiUnitTesting.Services.Authentication
{
    public class LocalAuthenticationServiceTest : IDisposable
    {
        private readonly LocalAuthenticationService sut;
        private readonly string filePath = "config/usersfortest.json";

        public LocalAuthenticationServiceTest()
        {
            sut = new LocalAuthenticationService(filePath, "secret key for unit tests", 600);

            string json = @"[
                {
                    ""Username"": ""test"",
                    ""Password"": ""123456""
                },
                {
                    ""Username"": ""johndoe"",
                    ""Password"": ""QWERTY""
                }
            ]";

            System.IO.File.WriteAllText(filePath, json);
        }

        public void Dispose()
        {
            System.IO.File.Delete(filePath);
        }

        [Fact]
        public void GivenNotExistingUsername_WhenSignin_ShouldThrow()
        {
            var credential = new Credentials("fake", "123456");
            Assert.Throws<Exception>(() => sut.Signin(credential));
        }

        [Fact]
        public void GivenWrongPassword_WhenSignin_ShouldThrow()
        {
            var credential = new Credentials("test", "111111");
            Assert.Throws<Exception>(() => sut.Signin(credential));
        }

        [Fact]
        public void GivenValidCredentials_WhenSignin_ShouldReturnValue()
        {
            var credential = new Credentials("test", "123456");
            var loggedUser = sut.Signin(credential);

            Assert.NotNull(loggedUser);
            Assert.Equal("test", loggedUser.Username);
            Assert.NotNull(loggedUser.Token);
            Assert.NotEmpty(loggedUser.Token);
        }

        [Fact]
        public void GivenValidCredentials_WhenSignup_ShouldReturnValue()
        {
            var credential = new Credentials("newuser", "ASDFGH");
            var loggedUser = sut.Signup(credential);

            Assert.NotNull(loggedUser);
            Assert.Equal("newuser", loggedUser.Username);
            Assert.NotNull(loggedUser.Token);
            Assert.NotEmpty(loggedUser.Token);
        }

        [Fact]
        public void GivenValidCredentials_WhenSignup_ShouldStoreCredentials()
        {
            var credential = new Credentials("newuser", "ASDFGH");
            var loggedUser = sut.Signup(credential);

            var jsonString = System.IO.File.ReadAllText(filePath);
            var users = JsonSerializer.Deserialize<List<Credentials>>(jsonString);

            Assert.Contains(users, x => x.Username == credential.Username && x.Password == credential.Password);
        }

        [Fact]
        public void GivenExistingUsername_WhenSignup_ShouldThrow()
        {
            var credential = new Credentials("fake", "123456");

            Assert.Throws<Exception>(() => sut.Signin(credential));
        }
    }
}
