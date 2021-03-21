using System;
using System.Globalization;
using Newtonsoft.Json;
using Player.Enums;

namespace Player.Models
{
    public class User
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("NickName")]
        public string NickName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("DateBirth")]
        public DateTime? DateBirth { get; set; }
        [JsonProperty("Role")]
        public Roles Role { get; set; }
        [JsonProperty("IsDeleted")]
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, \n" +
                   $"Nickname: {NickName}, \n" +
                   $"DateBirth: {DateBirth.GetValueOrDefault().ToString(CultureInfo.CurrentCulture)}, \n" +
                   $"Role: {Role}";
        }
    }
}