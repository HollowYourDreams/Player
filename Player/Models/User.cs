using System;
using System.Globalization;

namespace Player.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public DateTime? DateBirth { get; set; }
        public string Role { get; set; }
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