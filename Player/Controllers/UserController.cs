using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Player.Interface;
using Player.Models;

namespace Player.Controllers
{
    public class UserController : IUserController
    {
        //User.Id.Nickname.sav
        /*
         *Id: 0;
         *Nickname: Hollow;
         *DateBirth: 06-16-1999;
         *Role: Owner;
         *IsDeleted: false;
         *Password: PassForTest;
         */
         //path/to/file/User.0.Hollow.sav
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
            try
            {
                var fileName = $"User.{user.Id}.{user.NickName}.sav";

                using var stream = new FileStream($"{PathToUsers}\\{fileName}", FileMode.CreateNew);
                using var writer = new StreamWriter(stream, Encoding.Unicode);

                writer.WriteLine($"Id: {user.Id}");
                writer.WriteLine($"NickName: {user.NickName}");
                writer.WriteLine($"DateBirth: {user.DateBirth}");
                writer.WriteLine($"Password: {user.Password}");
                writer.WriteLine($"Role: {user.Role}");
                writer.WriteLine($"IsDeleted: {user.IsDeleted}");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public User ReadUserInfo(int id)
        {
            var files = Directory.GetFiles(PathToUsers).ToList();
            var currentUser = files.FirstOrDefault(x => x.Contains($"{PathToUsers}\\User.{id}") && x.EndsWith(".sav"));
            if (string.IsNullOrWhiteSpace(currentUser))
            {
                return null;
            }
            using var stream = new FileStream($"{PathToUsers}\\{currentUser}", FileMode.Open);
            using var reader = new StreamReader(stream, Encoding.Unicode);
            var text = reader.ReadToEnd();
            return ParseStringToUser(text);
        }
        //ReadUserInfo("Hollow") //*Hollow.sav
        public User ReadUserInfo(string nickname)
        {
            var files = Directory.GetFiles(PathToUsers).ToList();
            var currentUser = files.FirstOrDefault(x => x.EndsWith($"{nickname}.sav"));
            if (string.IsNullOrWhiteSpace(currentUser))
            {
                return null;
            }
            using var stream = new FileStream($"{PathToUsers}\\{currentUser}", FileMode.Open);
            using var reader = new StreamReader(stream, Encoding.Unicode);
            var text = reader.ReadToEnd();
            return ParseStringToUser(text);
        }

        public bool TryClearAllUsers()
        {
            try
            {
               Directory.Delete(PathToUsers, true);
               return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool TryDeleteUser(int id)
        {
            try
            {
                var files = Directory.GetFiles(PathToUsers).ToList();
                var currentUser = files.FirstOrDefault(x => x.Contains($"{PathToUsers}\\User.{id}") && x.EndsWith(".sav"));
                if (string.IsNullOrWhiteSpace(currentUser))
                {
                    return false;
                }
                File.Delete(currentUser);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool TryDeleteUser(string nickname)
        {
            try{ 
                var files = Directory.GetFiles(PathToUsers).ToList();
                var currentUser = files.FirstOrDefault(x => x.EndsWith($"{nickname}.sav"));
                if (string.IsNullOrWhiteSpace(currentUser))
                {
                    return false;
                }
                File.Delete(currentUser);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public User ChangeUserInfo(User user)
        {
            var files = Directory.GetFiles(PathToUsers).ToList();
            var currentUser = files.FirstOrDefault(x => x.EndsWith($"User.{user.Id}.{user.NickName}"));
            if (string.IsNullOrWhiteSpace(currentUser))
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            using var stream = new FileStream(currentUser, FileMode.Open);
            using var writer = new StreamWriter(stream, Encoding.Unicode);

            writer.WriteLine($"Id: {user.Id}");
            writer.WriteLine($"NickName: {user.NickName}");
            writer.WriteLine($"DateBirth: {user.DateBirth}");
            writer.WriteLine($"Password: {user.Password}");
            writer.WriteLine($"Role: {user.Role}");
            writer.WriteLine($"IsDeleted: {user.IsDeleted}");

            return user;
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
                users.Add(ParseStringToUser(text));

            });
            return users;
        }

        private static User ParseStringToUser(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;

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

            return  new User
            {
                Id = id,
                NickName = nameString,
                DateBirth = dateBirth,
                Password = passwordString,
                Role = roleString,
                IsDeleted = isDeleted
            };
        }

    }
}
