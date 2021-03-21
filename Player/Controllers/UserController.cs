using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Player.Enums;
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
                    Role = Roles.Player,
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
                Role = Roles.Player,
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

                var json = JsonConvert.SerializeObject(user);
                writer.Write(json);

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
            catch (Exception)
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

            var json = JsonConvert.SerializeObject(user);
            writer.Write(json);
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
            return string.IsNullOrWhiteSpace(text) ? null : JsonConvert.DeserializeObject<User>(text);
        }

    }
}
