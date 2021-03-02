using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player.Interface;
using Player.Models;

namespace Player.Controllers
{
    public class UserController : IUserController
    {
        //User.Id.User.Nickname.sav
        /*
         *Id: 0;
         *Nickname: Hollow;
         *DateBirth: 06-16-1999;
         *Role: Owner;
         *IsDeleted: false;
         *Password: PassForTest;
         */


        private int CurrentLastId { get; } = -1;
        private string PathToUsers { get; }

        public UserController(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            PathToUsers = path;
            var users = GetAllUsers();
            if (!users.Any())
            {
                return;
            }
            users.Sort((x, y) => y.Id - x.Id);
            CurrentLastId = users[0].Id;
        }

        public User GetUserInfo()
        {
            Console.WriteLine("Enter your nickname: ");
            var nick = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            var pass = Console.ReadLine();
            Console.WriteLine("Would you like to enter your date of birth? (y?n)");
            DateTime? birth = null;
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.WriteLine("Enter in format dd.mm.yyyy: ");
                if (DateTime.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, DateTimeStyles.None,
                    out var birth1))
                {
                    birth = birth1;
                }
            }

            return new User
            {
                Id = CurrentLastId+1,
                NickName = nick,
                Password = pass,
                Role = "Common",
                DateBirth = birth,
                IsDeleted = false
            };
        }

        public bool TrySaveUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public User ReadUserInfo(int id)
        {
            throw new NotImplementedException();
        }

        public User ReadUserInfo(string nickname)
        {
            throw new NotImplementedException();
        }

        public bool TryClearAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteUser(string nickname)
        {
            throw new NotImplementedException();
        }

        public User ChangeUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            var files = Directory.GetFiles(PathToUsers).ToList();


            throw new NotImplementedException();
        }
    }
}
