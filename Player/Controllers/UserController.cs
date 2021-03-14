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
            if (Console.ReadLine()?.ToLower() != "y")
            {
                return new User
                {
                    Id = CurrentLastId + 1,
                    NickName = nick,
                    Password = pass,
                    Role = "Common",
                    DateBirth = birth,
                    IsDeleted = false
                };
            }
            Console.WriteLine("Enter in format dd.mm.yyyy: ");
            if (DateTime.TryParse(Console.ReadLine(), CultureInfo.CurrentCulture, DateTimeStyles.None,
                out var birth1))
            {
                birth = birth1;
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
            var users = new List<User>();
            files.ForEach(x =>
            {
                var text = File.ReadAllText(x);
                if (string.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                var idIndexStart = text.IndexOf("Id:", StringComparison.CurrentCultureIgnoreCase);
                var nameIndexStart = text.IndexOf("Nickname:", StringComparison.CurrentCultureIgnoreCase);
                var dateBirthIndexStart = text.IndexOf("DateBirth:", StringComparison.CurrentCultureIgnoreCase);
                var roleIndexStart = text.IndexOf("Role:", StringComparison.CurrentCultureIgnoreCase);
                var isDeletedIndexStart = text.IndexOf("IsDeleted:", StringComparison.CurrentCultureIgnoreCase);
                var passwordIndexStart = text.IndexOf("Password:", StringComparison.CurrentCultureIgnoreCase);

                var idIndexEnd = text.Substring(idIndexStart).IndexOf(';');
                var nameIndexEnd = text.Substring(nameIndexStart).IndexOf(';');
                var dateBirthIndexEnd = text.Substring(dateBirthIndexStart).IndexOf(';');
                var roleIndexEnd = text.Substring(roleIndexStart).IndexOf(';');
                var isDeletedIndexEnd = text.Substring(isDeletedIndexStart).IndexOf(';');
                var passwordIndexEnd = text.Substring(passwordIndexStart).IndexOf(';');

                var idString = text.Substring(idIndexStart + 3 + 1, idIndexEnd - 3 - 1);
                var nameString = text.Substring(nameIndexStart + 9 + 1, nameIndexEnd - 9 - 1);
                var dateBirthString = text.Substring(dateBirthIndexStart + 10 + 1, dateBirthIndexEnd - 10 - 1);
                var roleString = text.Substring(roleIndexStart + 5 + 1, roleIndexEnd - 5 - 1);
                var isDeletedString = text.Substring(isDeletedIndexStart + 10 + 1, isDeletedIndexEnd - 10 - 1);
                var passwordString = text.Substring(passwordIndexStart + 9 + 1, passwordIndexEnd - 9 - 1);

                if (!int.TryParse(idString, out var id))
                {
                    throw new Exception("Не удалось прочитать ID!");
                }

                DateTime? dateBirth = null;
                if (DateTime.TryParse(dateBirthString, out var dateBirthRes))
                {
                    dateBirth = dateBirthRes;
                }

                if (!bool.TryParse(isDeletedString, out var isDeleted))
                {
                    throw new Exception("Не удалось прочитать IsDeleted!");
                }
                
                users.Add(new User
                {
                    Id = id,
                    NickName = nameString,
                    DateBirth = dateBirth,
                    Password = passwordString,
                    Role = roleString,
                    IsDeleted = isDeleted
                });

            });
            return users;
        }
    }
}
