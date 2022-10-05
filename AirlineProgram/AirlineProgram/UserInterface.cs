using System;
using System.Collections.Generic;

namespace AirlineProgram
{
    /// <summary>
    /// Static support methods for a simple interactive menu system.
    /// </summary>
    public class UserInterface
    {
        /// <summary>
        /// Choose an item from a list.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="title">The text to display at the start of the list.</param>
        /// <param name="itemList">A list of items which will be displayed.</param>
        /// <returns>The selected item.</returns>
        public static T ChooseFromList<T>(string title, IList<T> itemList)
        {
            System.Diagnostics.Debug.Assert(itemList.Count > 0);
            DisplayList(title, itemList);
            var option = UserInterface.GetOption(1, itemList.Count);
            return itemList[option];
        }

        /// <summary>
        /// Display a list of items.
        /// </summary>
        /// <typeparam name="T">The kind of items stored in the list.</typeparam>
        /// <param name="title">The text to display at the start of the list.</param>
        /// <param name="list">A list of items which will be displayed.</param>
        public static void DisplayList<T>(string title, IList<T> list)
        {
            Console.WriteLine(title);

            if (list.Count == 0)
            {
                Console.WriteLine("  None");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("  {0}) {1}", i + 1, list[i].ToString());
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prompts the user for an integer between lower and upper bounds, inclusive.
        /// </summary>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>Returns ('value entered by user' - 1).</returns>
        public static int GetOption(int min, int max)
        {
            while (true)
            {
                var key = GetInput($"Please enter an option between {min} and {max}");

                if (int.TryParse(key, out var option))
                {
                    if (min <= option && option <= max)
                        return option - 1;
                }

                UserInterface.Error("Invalid option");
            }
        }


        /// <summary>
        /// Displays a menu, with the options numbered from 1 to options.Length,
        /// the gets a validated integer in the range 1..options.Length. 
        /// Subtracts 1, then returns the result. If the supplied list of options 
        /// is empty, returns an error value (-1).
        /// </summary>
        /// <param name="title">A heading to display before the menu is listed.</param>
        /// <param name="options">The list of objects to be displayed.</param>
        /// <returns>Return value is either -1 (if no options are provided) or a 
        /// value in 0 .. (options.Length-1).</returns>
        public static int GetOption(string title, params object[] options)
        {
            if (options.Length == 0)
            {
                return -1;
            }

            int digitsNeeded = (int)(1 + Math.Floor(Math.Log10(options.Length)));

            Console.WriteLine(title);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{(i + 1).ToString().PadLeft(digitsNeeded)} {options[i]}");
            }

            int option = GetInteger($"Please enter a choice between 1 and {options.Length}", 1, options.Length);

            return option - 1;
        }


        /// <summary>
        /// Prompts user to enter a line of text.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream.</returns>
        public static string GetInput(string prompt)
        {
            Console.Write("{0}: ", prompt);
            return Console.ReadLine();
        }


        /// <summary>
        /// Prompts user to get an integer value, blocking until valid input is obtained.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream, parsed into an int.</returns>
        public static int GetInteger(string prompt)
        {
            while (true)
            {
                var response = UserInterface.GetInput(prompt);

                if (int.TryParse(response, out int intVal))
                {
                    return intVal;
                }
                else
                {
                    Error("Invalid number");
                }
            }
        }


        /// <summary>
        /// Gets a validated integer between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
        public static int GetInteger(string prompt, int min, int max)
        {
            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                int result = GetInteger(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is out of range");
                }
            }
        }

        /// <summary>
        /// Gets a validated Boolean value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A Boolean value supplied by the user.</returns>
        public static bool GetBoolean(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                bool result;

                if (bool.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not a boolean");
                }
            }
        }


        /// <summary>
        /// Gets a validated floating point value.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <returns>A floating point value supplied by the user.</returns>
        public static double GetDouble(string prompt)
        {
            while (true)
            {
                string response = GetInput(prompt);

                double result;

                if (double.TryParse(response, out result))
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is not numeric");
                }
            }
        }


        /// <summary>
        /// Gets a validated floating point between the designated lower and upper bounds.
        /// </summary>
        /// <param name="prompt">Text used to ask the user for input.</param>
        /// <param name="min">The lower bound.</param>
        /// <param name="max">The upper bound.</param>
        /// <returns>A value x such that lowerBound <= x <= upperBound.</returns>
        public static double GetDouble(string prompt, double min, double max)
        {
            if (min > max)
            {
                double t = min;
                min = max;
                max = t;
            }

            while (true)
            {
                double result = GetDouble(prompt);

                if (min <= result && result <= max)
                {
                    return result;
                }
                else
                {
                    Error("Supplied value is out of range");
                }
            }
        }



        /// <summary>
        /// Prompts user to get password.
        /// </summary>
        /// <param name="prompt">The label displayed to request input.</param>
        /// <returns>The next line of input from the standard input stream.</returns>
        public static string GetPassword(string prompt)
        {
            Console.Write("{0}: ", prompt);
            var password = new System.Text.StringBuilder();
            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                var key = keyInfo.Key;

                if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password.Remove(password.Length - 1, 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    password.Append(keyInfo.KeyChar);
                }
            }

            Console.WriteLine();
            return password.ToString();
        }

        /// <summary>
        /// Displays a message followed by the text ", please try again".
        /// </summary>
        /// <param name="msg">The message to display.</param>
        public static void Error(string msg)
        {
            Console.WriteLine($"{msg}, please try again");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a message.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        public static void Message(object msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Class which represents a menu item. Use subclass to represent an actual
    /// menu item.
    /// </summary>
    public abstract class MenuItem
    {
        /// <summary>
        /// The text to be displayed in the menu item.
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Initialise a new MenuItem,
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        public MenuItem(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Get the text of the menu item for display.
        /// </summary>
        /// <returns>The text of the menu item for display.</returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Override the WhenSelected method to do the action associated with the menu item.
        /// </summary>
        /// <param name="parentMenu">The parent menu (may be unused in many cases).</param>
        public abstract void WhenSelected(Menu parentMenu);
    }

    /// <summary>
    /// Built-in class representing the "Finish processing this menu" menu item.
    /// </summary>
    public class ExitMenuItem : MenuItem
    {
        /// <summary>
        /// Initialise the menu item.
        /// </summary>
        /// <param name="text">The text to display for the menu item.</param>
        public ExitMenuItem(string text = "Return to previous menu") : base(text)
        {
        }

        /// <summary>
        /// Action to carry out -- asks parent menu to exit.
        /// </summary>
        /// <param name="parentMenu">The menu which includes this menuItem.</param>
        public override void WhenSelected(Menu parentMenu)
        {
            parentMenu.Exit();
        }
    }

    /// <summary>
    /// Implements an interactive menu.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// The items to display.
        /// </summary>
        public List<MenuItem> Items { get; private set; }

        /// <summary>
        /// The text to display at the top of the menu.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Is the menu running?
        /// </summary>
        public bool Running { get; private set; }

        /// <summary>
        /// Initialise a new Menu.
        /// </summary>
        /// <param name="title">The text to display at the top of the menu.</param>
        /// <param name="menuItems">The items to display.</param>
        public Menu(string title, params MenuItem[] menuItems)
        {
            Title = title;
            Items = new List<MenuItem>();
            Add(menuItems);
        }

        /// <summary>
        /// Add items to the menu.
        /// </summary>
        /// <param name="menuItems">The items to display.</param>
        public void Add(params MenuItem[] menuItems)
        {
            Items.AddRange(menuItems);
        }

        /// <summary>
        /// Terminate the menu, returning control to the caller.
        /// </summary>
        public void Exit()
        {
            Running = false;
        }

        /// <summary>
        /// Run the menu, displaying the option list, and carrying out the selected option until
        /// Exit has been called.
        /// </summary>
        public void Display()
        {
            Running = true;

            while (Running)
            {
                MenuItem selectedItem = UserInterface.ChooseFromList(Title, Items);

                if (selectedItem != null)
                {
                    selectedItem.WhenSelected(this);
                }
            }

            Console.WriteLine();
        }
    }
}
