using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure;
using Infrastructure.Data;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using UI.Menus;

    namespace UI
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                var menu = ServiceContainer.CreateMainMenu();

                menu.Show();
            }
        }
    }