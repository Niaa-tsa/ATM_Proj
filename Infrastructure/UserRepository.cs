using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
namespace Infrastructure
{
    // პასუხისმგებელია მომხმარებლების ფაილში შენახვასა და წაკითხვაზე.
    public class UserRepository : IUserDataManager
    {
        private readonly string _filePath = "C:\\Users\\Nia Tsalkalamanidze\\Desktop\\N\\Step\\ATM_Project\\Infrastructure\\Data\\Users.txt";
        public void CreateUser(User user)
        {
            string line = JsonSerializer.Serialize(
                user,
                user.GetType()
            );

            File.AppendAllLines(
                _filePath,
                new[] { line }
            );
        }
        public void DeleteUser(int id)
        {
            List<User> users = GetAllUsers();

            User user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("User not found");
            }


            users.Remove(user);


            SaveChanges(users);
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
                if (string.IsNullOrWhiteSpace(line))
                    continue;


                try
                {
                    using JsonDocument json = JsonDocument.Parse(line);


                    var role = json.RootElement
                        .GetProperty("Role")
                        .GetInt32();


                    User user;


                    if (role == (int)Role.Client)
                    {
                        user = JsonSerializer.Deserialize<ClientUser>(line);
                    }
                    else if (role == (int)Role.Admin)
                    {
                        user = JsonSerializer.Deserialize<AdminUser>(line);
                    }
                    else
                    {
                        continue;
                    }


                    users.Add(user);
                }
                catch
                {
                    continue;
                }
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

            File.AppendAllLines(
                _filePath,
                users.Select(x =>
                    JsonSerializer.Serialize(
                        x,
                        x.GetType()
                    ))
            );
        }
        public void DeleteUserByEmail(string email)
        {
            var users = GetAllUsers();

            var user = users.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                users.Remove(user);
                SaveChanges(users);
            }
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
