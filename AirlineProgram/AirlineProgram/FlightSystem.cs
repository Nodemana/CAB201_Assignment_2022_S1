using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AirlineProgram
{
    class FlightSystem
    {
        //Initialisation of Lists or Dictionarys of Class Instances
        private Dictionary<string, Employee> EmployeeDict = new Dictionary<string, Employee>();
        private Dictionary<string, Customer> CustomerDict = new Dictionary<string, Customer>();
        public List<Flight> FlightList = new List<Flight>();
        public Boolean loggedin = false;


        static void Main(string[] args)
        {
            FlightSystem program = new FlightSystem();
           // program.Maximise_Console();
            program.MainMenu();
        }
        
        /// <summary>
        /// Main menu for the system.
        /// </summary>
        /// <returns>void.</returns>
        public void MainMenu()
        {
            //The code for the menus is used repetively thoughout the flight system. So for ease it will be explained once here.
            // The menu system uses a case statment which is linked to a number of constant values each associated with a different function.
            // The user enters a value between 1 and the Nth option. This value correlates to one of the case statments which then perform its given function.
            const int LOGIN = 0, REGISTER = 1, BOOKINGS = 2, LOGOUT = 3, EXIT = 4;

            bool running = true;

            while (running)
            {
                int option = UserInterface.GetOption("Select an option to proceed", "Login", "Register", "Bookings", "Logout", "Logout and Exit");

                switch (option)
                {
                    case LOGIN:
                        Login();
                        break;
                    case REGISTER:
                        if (loggedin)
                        {
                            Console.WriteLine("You must logout first.");
                        }
                        else
                        {
                            Registration();
                        }
                        break;
                    case BOOKINGS:
                        if (loggedin)
                        {
                            BookingMenu();
                        }
                        else
                        {
                            Console.WriteLine("You must Login to use this feature.");
                        }
                        break;
                    case LOGOUT:
                        Logout();
                        break;
                    case EXIT:
                        Logout();
                        running = false;
                        break;

                }
            }
        }
        /// <summary>
        /// Booking management menu.
        /// </summary>
        /// <returns>void.</returns>
        public void BookingMenu()
        {
            const int NEW_BOOKING = 0, VIEW_FLIGHTS = 1, ADD_FLIGHT = 2, BACK = 3;
            bool bookingrunning = true;

            while (bookingrunning)
            {
                int option = UserInterface.GetOption("Select an option to proceed", "New Booking", "View Flights", "Add Flights", "Back");

                switch (option)
                {
                    case NEW_BOOKING:               
                        View_Flights();
                        Flight flight = Select_Flight("Select an option to proceed");
                        Booking(flight);
                        break;
                    case VIEW_FLIGHTS:
                        View_Flights();
                        flight = Select_Flight("See Flight Details");
                        if (flight != null)
                        {
                            flight.Detail_Display();
                            flight.Calculate_Flight_Time();
                            flight.Passenger_Display();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Flight Number");
                        }
                        break;
                    case ADD_FLIGHT:
                        AddFlight();
                        break;
                    case BACK:
                        bookingrunning = false;
                        break;
                }
            }
        }
        /// <summary>
        /// Books flight for customer. Creates new customer if given one doesn't exist.
        /// </summary>
        /// <param name="flight">The flight object to be booked for.</param>
        /// <returns>void.</returns>
        public void Booking(Flight flight)
        {
            //The code checks that the chosen flight exists and if there is remaining capacity before attempting to book it.
            //It then asks the user for the customers email to check if they exist, if they exist it will book the flight and add the customer to the passenger list,
            //otherwise it will create a new customer.
            if (flight == null)
            {
                UserInterface.Error("Invalid Flight Number");              
            }
            if (flight.Rem_capacity > 0)
            {
                string C_Email = UserInterface.GetInput("Enter Customer Email");
                if (Check_Customer(C_Email))
                {
                    flight.PassengerList.Add(CustomerDict[C_Email]);
                    flight.Rem_capacity -= 1;
                    Console.WriteLine("Passenger found, booking flight...");
                }
                else
                {
                    // If customer doesn't exist in the dictionary, then the following code asks for the customers details and creates a new customer.
                    // Then it adds them to the already specified flight.
                    Console.WriteLine("Customer not found, please enter following details.");
                    string C_Name = UserInterface.GetInput("Enter your name");
                    C_Email = UserInterface.GetInput("Enter your email");
                    string C_Address = UserInterface.GetInput("Enter your address");
                    int C_Mobile = UserInterface.GetInteger("Enter your mobile number");
                    NewCustomer(C_Name, C_Email, C_Address, C_Mobile);
                    flight.PassengerList.Add(CustomerDict[C_Email]);
                    flight.Rem_capacity -= 1;
                }
            }
            else
            {
                UserInterface.Error("Flight is at capacity.");

            }
        }
        /// <summary>
        /// Checks if customer exists.
        /// </summary>
        /// <param name="check">The email of the customer in question.</param>
        /// <returns>Boolean if customer exists.</returns>
        public Boolean Check_Customer(string check)
        {
            if (CustomerDict.ContainsKey(check))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Prompts user if they want to select a flight, if yes then prompts the user for a flight number.
        /// </summary>
        /// <param name="msg">The text to display for the initial prompt.</param>
        /// <returns>The selected flight or null.</returns>
        public Flight Select_Flight(string msg)
        {
            const int SELECT_FLIGHT = 0, BACK = 1;
            int option = UserInterface.GetOption(msg, "Select Flight", "Back");
            // The following code uses a small menu to allow the user to either select a flight or go back to the previous menu.
            switch (option)
            {
                case SELECT_FLIGHT:
                    int flightnum = UserInterface.GetInteger("Enter desired flight number");
                    foreach (Flight flight in FlightList) //Checks if flight number exists.
                    {
                        if (flight.FlightNum == flightnum) // Need error trapping here
                        {
                            return flight;
                        }
                    }
                    Console.WriteLine("Invalid Flight Number");
                    break;
                case BACK:
                    return null;
            }
            return null;
        }
        /// <summary>
        /// Displays flight details for all flights.
        /// </summary>
        /// <returns>void.</returns>
        public void View_Flights()
        {
            Console.WriteLine("|| {0, -13} || {1, -14} || {2,-9} || {3,-11} || {4,-14} || {5,-14} || {6,-11} || {7,-8} || {8,-18} ||", "Flight Number", "Aircraft", "Departure", "Destination", "Departure Time", "Total Distance", "Flight Time", "Cost", "Remaining Capacity");
            foreach (var flight in FlightList)
            {
                flight.Calculate_Flight_Time();
                flight.Display();
            }
        }
        /// <summary>
        /// Asks for details and registers a new employee.
        /// </summary>
        /// <returns>void.</returns>
        public void Registration()
        {
            string MyName = UserInterface.GetInput("Enter your name");
            string MyEmail = UserInterface.GetInput("Enter your email");
            string MyPassword = UserInterface.GetPassword("Enter your password");
            if (EmployeeDict.ContainsKey(MyEmail))
            {
                Console.WriteLine("");
                Console.WriteLine("Sorry, this email is already connected to an existing account.");
                Console.WriteLine("Use a different email or Login.");
                Console.WriteLine("");
            }
            else
            {
                Employee newEmployee = new Employee(MyName, MyEmail, MyPassword);
                EmployeeDict.Add(newEmployee.Email, newEmployee);
            }

        }
        /// <summary>
        /// Allows employee to login to the system.
        /// </summary>
        /// <returns>void.</returns>
        public void Login()
        {
            string email = UserInterface.GetInput("Enter your email");
            string password = UserInterface.GetPassword("Enter your password");
            if (EmployeeDict.ContainsKey(email))
            {
                if (EmployeeDict[email].Password == password)
                {
                    loggedin = true;
                    Console.WriteLine("Welcome, you are now logged in.");
                }
                else
                {
                    Console.WriteLine("Email or Password is incorrect.");
                }
            }
            else
            {
                Console.WriteLine("Email or Password is incorrect.");
            }
        }
        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <param name="aName">Customers Name.</param>
        /// <param name="aEmail">Customers Email.</param>
        /// <param name="aAddress">Customers Address.</param>
        /// <param name="aNumber">Customers Phone Number.</param>
        /// <returns>void.</returns>
        public void NewCustomer(string aName, string aEmail, string aAddress, int aNumber)
        {
            if (CustomerDict.ContainsKey(aEmail))
            {
                Console.WriteLine("");
                Console.WriteLine("Sorry, this email is already connected to an existing customer.");
                Console.WriteLine("Use a different email.");
                Console.WriteLine("");
            }
            else
            {
                Customer newCustomer = new Customer(aName, aEmail, aAddress, aNumber);
                CustomerDict.Add(newCustomer.Email, newCustomer);
            }
        }
        /// <summary>
        /// Allows user to add flights to the system.
        /// </summary>
        /// <returns>void.</returns>
        public void AddFlight()
        {
            Boolean is_heli = UserInterface.GetBoolean("Is the flight a Helicopter? Enter True of False");
            string aDeparture_place = UserInterface.GetInput("Flight Departure Place");
            string aArrival_place = UserInterface.GetInput("Flight Destination");
            string aDeparture_time = UserInterface.GetInput("Flight Departure Time");
            int aDistance = UserInterface.GetInteger("Flight Distance");
            if (is_heli == true)
            {
                HeliFlight newHeliFlight = new HeliFlight(aDeparture_place, aArrival_place, aDeparture_time, aDistance);
                FlightList.Add(newHeliFlight);

            }
            else
            {
                PlaneFlight newFlight = new PlaneFlight(aDeparture_place, aArrival_place, aDeparture_time, aDistance);
                FlightList.Add(newFlight);
            }
        }
        /// <summary>
        /// Allows employee to logout of the system.
        /// </summary>
        /// <returns>void.</returns>
        public void Logout()
        {
            if (loggedin == false)
            {
                Console.WriteLine("You are already logged out.");
            }
            else
            {
                loggedin = false;
                Console.WriteLine("You have been logged out.");
            }
        }
    }
}