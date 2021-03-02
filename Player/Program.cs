using System;
using Player.Extensions;
using Player.Models;

namespace Player
{
    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new User
            {
                Id = 0,
                NickName = "Hollow",
                Password = "Passwordwsyugdhfjaskdgjasd",
                Role = "Role1",
                DateBirth = new DateTime(1999, 06, 16),
                IsDeleted = false
            };
            var user2 = new User
            {
                Id = 3,
                NickName = "Hollow",
                Password = "asdfasdfasd",
                Role = "Role2",
                DateBirth = new DateTime(1999, 06, 16),
                IsDeleted = true
            };

            Console.WriteLine(user1.IsEqual(user2));
            Console.WriteLine(user1.ToString());
            Console.ReadKey();
        }
    }
}
