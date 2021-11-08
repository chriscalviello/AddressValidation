using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Api.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services.Authentication
{
    public class LocalAuthenticationService : IAuthenticationService
    {
        private string JsonPath { get; set; }
        private string JwtSecret { get; set; }
        private double JwtDurationInSeconds { get; set; }

        public LocalAuthenticationService(string jsonPath, string jwtSecret, double jwtDurationInSeconds)
        {
            JsonPath = jsonPath;
            JwtSecret = jwtSecret;
            JwtDurationInSeconds = jwtDurationInSeconds;
        }

        public LoggedUser Signin(Credentials credentials)
        {
            var existingCredentials = GetCredentials();

            var storedCredential = existingCredentials.FirstOrDefault(x => x.Username == credentials.Username);
            if (storedCredential == null)
            {
                throw new Exception("This username doesn't exist");
            }

            if(storedCredential.Password != credentials.Password)
            {
                throw new Exception("Password incorrect");
            }

            var token = GenerateJwtToken(credentials);

            return new LoggedUser(credentials.Username, token);
        }

        public LoggedUser Signup(Credentials credentials)
        {
            var existingCredentials = GetCredentials();
            if(existingCredentials.Any(x => x.Username == credentials.Username))
            {
                throw new Exception("Username is already used");
            }

            existingCredentials.Add(credentials);

            try
            {
                var jsonString = JsonSerializer.Serialize(existingCredentials);
                System.IO.File.WriteAllText(JsonPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when writing the json file. " + ex.Message);
            }

            var token = GenerateJwtToken(credentials);

            return new LoggedUser(credentials.Username, token);
        }

        public User GetUserByUsername(string username)
        {
            var existingCredentials = GetCredentials();

            var storedCredential = existingCredentials.FirstOrDefault(x => x.Username == username);
            if (storedCredential == null)
            {
                throw new Exception("This username doesn't exist");
            }

            return new User() { Username = username };
        }

        private List<Credentials> GetCredentials()
        {
            List<Credentials> result;
            try
            {
                var jsonString = System.IO.File.ReadAllText(JsonPath);
                result = JsonSerializer.Deserialize<List<Credentials>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when reading the json file. " + ex.Message);
            }
            return result;
        }

        private string GenerateJwtToken(Credentials credentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", credentials.Username) }),
                Expires = DateTime.UtcNow.AddSeconds(JwtDurationInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
