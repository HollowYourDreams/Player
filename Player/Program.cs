using System;
using Player.Controllers;
using Player.Enums;
using Player.Extensions;
using Player.Models;

namespace Player
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new UserController(".\\Saves");

            var user = controller.GetUserInfo();
            controller.TrySaveUserInfo(user);

            Console.ReadKey();
        }
    }
}
