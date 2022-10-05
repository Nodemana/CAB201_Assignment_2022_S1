using System;

public class Employee
{
	private string name, email, password;
	public string Name { get { return name;} set { name = Name; } }

	public string Email { get { return email; } set { email = Email; } }

	public string Password { get { return password; } set { password = Password; } }
	public Employee(string MyName, string MyEmail, string MyPassword)
	{
		Name = MyName;
		Email = MyEmail;
		Password = MyPassword;
	}
}
