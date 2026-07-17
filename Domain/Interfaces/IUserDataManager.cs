using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUserDataManager
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void CreateUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
        void SaveChanges(List<User> users);
        public User GetLastLoggedInUser();
    }

}
