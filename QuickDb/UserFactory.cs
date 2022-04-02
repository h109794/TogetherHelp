using BLL.Entity;
using BLL.Repository;
using Global;
using System.Collections.Generic;

namespace QuickDb
{
    static class UserFactory
    {
        private static readonly SqlDbContext sqlDbContext;

        static UserFactory() => sqlDbContext = SqlDbContext.GetInstance();

        internal static User Admin { get; set; }
        internal static User UserNumberOne { get; set; }
        internal static User UserNumberTwo { get; set; }
        internal static User UserNumberThree { get; set; }

        internal static void GenerateUsers()
        {
            foreach (var item in GenerateUserList())
            {
                sqlDbContext.Users.Add(item);
            }
        }

        private static List<User> GenerateUserList()
        {
            Admin = CreateUser("Admin");
            Admin.PersonalData.Nickname = "管理员";

            UserNumberOne = CreateUser("UserNumberOne", Admin);
            UserNumberTwo = CreateUser("UserNumberTwo", Admin);
            UserNumberThree = CreateUser("UserNumberThree", Admin);

            List<User> registerModels = new List<User>
            {
                Admin,
                UserNumberOne,
                UserNumberTwo,
                UserNumberThree,
            };
            return registerModels;
        }

        private static User CreateUser(string username, User inviter = null)
        {
            return new User
            {
                Username = username,
                Password = Utility.MD5Encrypt("qwer"),
                Inviter = inviter,
                InvitationCode = "1234",
                PersonalData = new PersonalData { Nickname = username },
            };
        }
    }
}
