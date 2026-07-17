using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.IO;
namespace Infrastructure
{
    public class UserRepository : IUserDataManager
    {
        private readonly string _filePath = "C:\\Users\\Nia Tsalkalamanidze\\Desktop\\N\\Step\\ATM_Project\\Infrastructure\\Data\\Users.txt";
        public void CreateUser(User user)
        {
            string line = JsonSerializer.Serialize(user);
            File.AppendAllLines(_filePath, new[] {line});
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            string[] lines = File.ReadAllLines(_filePath);

            List<User> users = new List<User>();

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                User user = JsonSerializer.Deserialize<User>(line);
                users.Add(user);
            }

            return users;
        }


        public User GetUserById(int id)
        {
            List<User> users = GetAllUsers();
            User user = users.FirstOrDefault(x => x.Id == id);

            return user;
        }
        public User GetLastLoggedInUser()
        {
            var users = GetAllUsers();
            User user = users.OrderBy( x => x.LastLogin).LastOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(x => x.Email == email);
        }

        public List<User> GetUsers()
        {
            return new List<User>();
        }

        public void SaveChanges(List<User> users)
        {
            File.Delete(_filePath);
            File.AppendAllLines(_filePath, users.Select(x => JsonSerializer.Serialize(x)));

        }

        public void UpdateUser(User user)
        {
            List<User> users = GetAllUsers();
            int index = users.FindIndex(x => x.Id == user.Id);
            if (index != -1)
            {
                users[index] = user;
                SaveChanges(users);
            }
        }
    }
}
