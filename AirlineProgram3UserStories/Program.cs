using CAB201_UserInterfaceTest;
using System;
using System.Collections.Generic;

namespace AirlineProgram3UserStories
{
    class Program
    {
        Dictionary<string, Employee> EmployeeDict = new Dictionary<string, Employee>();
        public Boolean loggedin = false;
        static void Main(string[] args)
        {
            Program program = new Program();
            MainMenu();
        }

        static void MainMenu()
        {
            const int LOGIN = 0, REGISTER = 1, LOGOUT = 2, EXIT = 3;
            Program program = new Program();

            bool running = true;

            while (running)
            {
                int option = UserInterface.GetOption("Select an option to proceed", "Login", "Register", "Logout", "Logout and Exit");

                switch (option)
                {
                    case LOGIN:
                        string username = UserInterface.GetInput("Enter your email.");
                        string password = UserInterface.GetPassword("Enter your password.");
                        program.Login(username, password);
                        break;
                    case REGISTER:
                        string aName = UserInterface.GetInput("Enter your name.");
                        string aEmail = UserInterface.GetInput("Enter your email.");
                        string aPassword = UserInterface.GetPassword("Enter your password.");
                        program.Registration(aName, aEmail, aPassword);
                        break;
                    case LOGOUT:
                        program.Logout();
                        break;
                    case EXIT:
                        program.Logout();
                        running = false;
                        break;

                }
            }
        }

        public void Registration(string MyName, string MyEmail, string MyPassword)
        {
           Employee newEmployee = new Employee(MyName, MyEmail, MyPassword);
           EmployeeDict.Add(newEmployee.Email, newEmployee);
        }

        public void Login(string email, string password)
        {
            if (EmployeeDict.ContainsKey(email))
            {
                if (EmployeeDict[email].Password == password)
                {
                    loggedin = true;
                    Console.WriteLine(loggedin);
                }
                else
                {
                    Console.WriteLine("Email or Password is incorrect");
                }
            }
            else
            {
                Console.WriteLine("Email or Password is incorrect");
            }
        }
        public void Logout()
        {
            if (loggedin == false)
            {
                Console.WriteLine("You are already logged out.");
            }
            else
            {
                loggedin = false;
            }
        }
    }
}