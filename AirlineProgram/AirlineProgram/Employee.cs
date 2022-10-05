using System;

namespace AirlineProgram
{
	public class Employee
	{
		private string name, email, password;
		public string Name { get { return name; } set { name = value; } }
		public string Email { get { return email; } set { email = value; } }
		public string Password { get { return password; } set { password = value; } }

		public Employee(string MyName, string MyEmail, string MyPassword)
		{
			Name = MyName;
			Email = MyEmail;
			Password = MyPassword;
		}
	}

}