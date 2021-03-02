using System.Collections.Generic;
using Player.Models;

namespace Player.Interface
{
    public interface IUserController
    {
        User GetUserInfo();
        bool TrySaveUserInfo(User user);
        User ReadUserInfo(int id);
        User ReadUserInfo(string nickname);
        bool TryClearAllUsers();
        bool TryDeleteUser(int id);
        bool TryDeleteUser(string nickname);
        User ChangeUserInfo(User user);
        List<User> GetAllUsers();
    }
}