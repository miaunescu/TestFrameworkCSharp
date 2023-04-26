using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.Models.PetsModels
{
    public class User
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("userStatus")]
        public int UserStatus { get; set; }

    }
}
