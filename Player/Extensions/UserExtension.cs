using Player.Models;

namespace Player.Extensions
{
    public static class UserExtension
    {
        public static bool IsEqual(this User user, User toCompare)
        {
            return user.NickName == toCompare.NickName;
        }

    }
}
