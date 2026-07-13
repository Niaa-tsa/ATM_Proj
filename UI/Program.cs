using Application.Services;
using Domain.Interfaces;
using Infrastructure;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserDataManager repository = new UserRepository();
            UserService userService = new UserService(repository);

            userService.RegisterUser("nia tsa", "email@gmail.compassword123", "email@gmail.com");

        }
    }
}
