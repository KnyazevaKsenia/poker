using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using OmahaPocker;
namespace ConsolePlayer
{
    public static class CommandReader
    {
        public async static Task LoginOrRegist(Client client)
        {
            while (true)
            {
                Console.WriteLine("Do you want to login or register?");
                var command = Console.ReadLine()?.Trim().ToLower();

                if (command == "login")
                {
                    Console.WriteLine("Enter nickname");
                    var nickname = Console.ReadLine();
                    Console.WriteLine("Enter password");
                    var password = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(password))
                    {
                        await client.Login(nickname, password);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nickname and password cannot be empty.");
                    }
                }
                else if (command == "register")
                {
                    Console.WriteLine("Enter nickname");
                    var nickname = Console.ReadLine();
                    Console.WriteLine("Enter password");
                    var password = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(password))
                    {
                        await client.Regist(nickname, password);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nickname and password cannot be empty.");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong command. Please enter 'login' or 'register'.");
                }
            }
        }
        public async static Task CreateSessionOrJoin(Client client)
        {
            while (true) 
            {
                Console.WriteLine("Do you want to create session or join?");
                var command = Console.ReadLine()?.Trim().ToLower();
                if (command == "create")
                {
                    Console.WriteLine("Name of session");
                    var name = Console.ReadLine()?.Trim().ToLower();
                    Console.WriteLine("Amount of players");
                    var amount = Console.ReadLine()?.Trim().ToLower();
                    Console.WriteLine("Bank");
                    var bank = Console.ReadLine()?.Trim().ToLower();
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(amount) && !string.IsNullOrEmpty(bank))
                    {
                        await client.CreateSession(name, int.Parse(amount), int.Parse(bank));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong data");
                    }
                }
                if (command == "join")
                {

                }
                else
                {
                    Console.WriteLine("You can only 'create' or 'join'");
                }
            }
        }
    }
}
